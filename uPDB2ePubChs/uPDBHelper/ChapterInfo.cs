using System;

namespace uPDB2ePubChs
{
    /// <summary>
    /// 章节信息
    /// </summary>
    public class ChapterInfo
    {
        private Int32 _Offset;
        private Int32 _Length;
        private Byte[] _UTF8Name;

 
        /// <summary>
        /// 偏移量
        /// </summary>
        public Int32 Offset   => _Offset;  

        /// <summary>
        /// 长度
        /// </summary>
        public Int32 Length => _Length;

        public Byte[] UTF8Name=>_UTF8Name;

        /// <summary>
        /// ChapterInfo构造函数
        /// </summary>
        public ChapterInfo(Int32 offset, Int32 length,Byte[] utf8Name)
        {
            _Offset = offset;
            _Length = length;
            _UTF8Name = utf8Name;
        }
    }
}
