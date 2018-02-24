using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using YYProject.Algorithms;
using System.Runtime.InteropServices;

namespace uPDB2ePubChs
{
    public sealed class UnicodePDB
    {
        #region P/invoke


        [DllImport("Kernel32", CharSet = CharSet.Unicode)]
        private static extern Int32 LCMapStringEx(String lpLocaleName, UInt32 dwMapFlags, Byte[] lpSrcStr, Int32 cchSrc, [Out] Byte[] lpDestStr, Int32 cchDest, IntPtr lpVersionInformation, IntPtr lpReserved, IntPtr sortHandle);

        private const String LOCALE_NAME_INVARIANT = "";
        private const UInt32 LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        #endregion


        #region fields


        private FileStream _BookStream = null;

        internal Byte[] InnerAuthor;
        internal Byte[] InnerTitle = null;

        private CatalogueInfo _Catalogue;
        private Byte[] _MD5;

        private static readonly Byte[] TERMINATINGNULL = new Byte[] { 0, 0 };
        private static readonly Byte[] CRNL = new Byte[] { 13, 0, 10, 0 };
        private static readonly Byte[] ESC = new Byte[] { 27, 0 };
        private static readonly Byte[] THREEESC = new Byte[] { 27, 0, 27, 0, 27, 0 };

        private static readonly Byte[] VericalBytes = new Byte[] { 0x3F, 0xFE, 0x40, 0xFE, 0x3D, 0xFE, 0x3E, 0xFE, 0x39, 0xFE, 0x3A, 0xFE, 0x3B, 0xFE, 0x3C, 0xFE, 0x43, 0xFE, 0x44, 0xFE, 0x41, 0xFE, 0x42, 0xFE, 0x37, 0xFE, 0x38, 0xFE, 0x35, 0xFE, 0x36, 0xFE, 0x5C, 0xFF, 0x02, 0x25, 0x19,0xFE },
                                       HorizontalBytesChs = new Byte[] { 0x08, 0x30, 0x09, 0x30, 0x0A, 0x30, 0x0B, 0x30, 0x14, 0x30, 0x15, 0x30, 0x10, 0x30, 0x11, 0x30, 0x18, 0x20, 0x19, 0x20, 0x1C, 0x20, 0x1D, 0x20, 0x5B, 0xFF, 0x5D, 0xFF, 0x08, 0xFF, 0x09, 0xFF, 0x14, 0x20, 0x26, 0x20,0x26,0x20 };

        #endregion

        /// <summary>
        /// 获取作品作者
        /// </summary>
        public String Author => InnerAuthor != null ? Encoding.Unicode.GetString(InnerAuthor) : null;

        /// <summary>
        /// 获取作品标题
        /// </summary>
        public String Title => Encoding.Unicode.GetString(InnerTitle);

        /// <summary>
        /// 获取作品章节信息
        /// </summary>
        public CatalogueInfo Catalogue => _Catalogue;



        /// <summary>
        /// 书籍信息块部分的MD5摘要。
        /// </summary>
        public Byte[] MD5 => _MD5;


        public Byte[] GetChapter(Int32 index)
        {

            var DataBuffer = new Byte[_Catalogue[index].Length];
            _BookStream.Seek(_Catalogue[index].Offset, SeekOrigin.Begin);
            _BookStream.Read(DataBuffer, 0, _Catalogue[index].Length);
            var q = Encoding.Unicode.GetString(ProcessText(DataBuffer));
            return ProcessText(DataBuffer);
        }

        public UnicodePDB(String fileName)
        {
            Open(fileName);
        }


        public void Close()
        {

            if (_BookStream != null)
            {
                _BookStream.Dispose();
                _BookStream = null;
            }
            InnerAuthor = null;
            InnerTitle = null;
            _Catalogue = null;
            _MD5 = null;
        }


        public Boolean ToEPub(String fileName, Boolean cleanMode = false)
        {
            try
            {
                EPubCreater.CreatEPub(this, fileName, cleanMode);
                return true;
            }
            catch
            {
                return false;
            }
           
        }


        private void Open(String path)
        {
            try
            {
                _BookStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                var infoBuffer = new Byte[78];
                if (_BookStream.Read(infoBuffer, 0, 78) != 78 && infoBuffer[64] != 0x4D || infoBuffer[65] != 0x54 || infoBuffer[66] != 0x49 || infoBuffer[67] != 0x55)
                {
                    throw new NotSupportedException();
                }
                InnerAuthor = GetInfo(infoBuffer, 0, 34, true);
                var record = ByteArrayToNumber(infoBuffer, 76, 2);
                _Catalogue = new CatalogueInfo(record - 2);

                //读取文件索引
                infoBuffer = new Byte[record << 3];
                if (_BookStream.Read(infoBuffer, 0, infoBuffer.Length) != infoBuffer.Length)
                {
                    throw new NotSupportedException();
                }

                //根据索引读取电子书属性区内容
                var bookInfoStop = ByteArrayToNumber(infoBuffer, 8, 4);
                var dataBuffer = new Byte[bookInfoStop - ByteArrayToNumber(infoBuffer, 0, 4)];
                if (_BookStream.Read(dataBuffer, 0, dataBuffer.Length) != dataBuffer.Length)
                {
                    throw new NotSupportedException();
                }
                var startIndex = 10;
                var flag = dataBuffer.QSIndexOf(THREEESC, startIndex);//书名,必须有，所以8+2
                if (flag == -1)
                {
                    throw new NotSupportedException();
                }

                InnerTitle = GetInfo(dataBuffer, 8, flag - 8);

                startIndex = flag + 6;    //章节数
                flag = dataBuffer.QSIndexOf(ESC, startIndex);
                if (flag == -1)
                {
                    throw new NotSupportedException();
                }
                var numStr = Encoding.ASCII.GetString(dataBuffer, startIndex, flag - startIndex);
                if (!Int32.TryParse(numStr, out var num) || num != record - 2)
                {
                    throw new NotSupportedException();
                }

                startIndex = flag + 2; //目录
                var indexBuffer = GetInfo(dataBuffer, startIndex, dataBuffer.Length - startIndex);

                // var tempStringArray =Encoding.Unicode.GetString(indexBuffer).Split('\r');

                //目录
                Int32 PosA = bookInfoStop, PosB;
                var maxIndex = _Catalogue.Capacity - 1;
                var bookmark = (Int32)_BookStream.Length - 2;
                var indexStartPos = 0;
                var indexStopPos = 0;

                //读取目录信息
                for (Int32 i = 0; i < _Catalogue.Capacity; i++)
                {
                    PosB = (i < maxIndex) ? ByteArrayToNumber(infoBuffer, 16 + (i << 3), 4) : bookmark;
                    indexStopPos = indexBuffer.QSIndexOf(CRNL, indexStartPos);
                    if (indexStopPos < 0)
                    {
                        indexStopPos = indexBuffer.Length;
                    }
                    _Catalogue.Add(new ChapterInfo(PosA, PosB - PosA, Encoding.Convert(Encoding.Unicode, Encoding.UTF8, indexBuffer, indexStartPos, indexStopPos - indexStartPos)));
                    //    var q= Encoding.Unicode.GetString(indexBuffer,indexStartPos, indexStopPos - indexStartPos);
                    indexStartPos = indexStopPos + 4;
                    PosA = PosB;
                }
                //判断书籍长度是否合格
                if (_Catalogue[maxIndex].Length + _Catalogue[maxIndex].Offset > bookmark)
                {
                    throw new NotSupportedException();
                }

                // 计算摘要
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    _MD5 = md5.ComputeHash(dataBuffer, 0, dataBuffer.Length);
                }

            }
            catch { this.Close(); }

        }



        /// <summary>
        /// 字节数组反转后转化为uint数字
        /// </summary>
        private static Int32 ByteArrayToNumber(Byte[] source, Int32 startindex, Int32 count)
        {
            var ByteBuffer = new Byte[4];
            for (Int32 i = startindex + count - 1, j = 0; i > startindex - 1; i--, j++)
            {
                ByteBuffer[j] = source[i];
            }
            return BitConverter.ToInt32(ByteBuffer, 0);
        }

        private Byte[] GetInfo(Byte[] source, Int32 start, Int32 count, Boolean trimZero = false)
        {

            if (trimZero)
            {
                var end = source.QSIndexOf(TERMINATINGNULL, start, count);
                if (end == 0)
                {
                    return null;
                }
                count = source.QSIndexOf(TERMINATINGNULL, start, count) + start;
            }
            var buffer = new Byte[count];
            Buffer.BlockCopy(source, start, buffer, 0, count);
            return ProcessText(buffer);
        }

        private Byte[] ProcessText(Byte[] source)
        {
            for (var i = 1; i < source.Length; i += 2)
            {
                for (var j = 1; j < HorizontalBytesChs.Length; j += 2)
                {
                    if (VericalBytes[j] == source[i] && VericalBytes[j - 1] == source[i - 1])
                    {
                        source[i] = HorizontalBytesChs[j];
                        source[i - 1] = HorizontalBytesChs[j - 1];
                    }
                }
            }
            var chsBytes = new Byte[source.Length];
            var length = (source.Length >> 1);
            LCMapStringEx(LOCALE_NAME_INVARIANT, LCMAP_SIMPLIFIED_CHINESE, source, length, chsBytes, length, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            return chsBytes;
        }

    }
}
