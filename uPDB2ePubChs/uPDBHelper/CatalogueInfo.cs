using System;
using System.Collections.Generic;

namespace uPDB2ePubChs
{
    /// <summary>
    /// 目录信息
    /// </summary>
    public class CatalogueInfo
    {
  

        private List<ChapterInfo> _ChapterList;
        
         /// <summary>
        /// 获取ChapterInfo合集
        /// </summary>
        public ChapterInfo this[Int32 index] => _ChapterList[index];

        /// <summary>
        /// 获取预设容量
        /// </summary>
        public Int32 Capacity => _ChapterList.Capacity;

        /// <summary>
        /// 获取ChapterInfo的数量
        /// </summary>
        public Int32 Count => _ChapterList.Count;

        /// <summary>
        /// 添加ChapterInfo
        /// </summary>
        /// <param name="chapter"></param>
        public void Add(ChapterInfo chapter)
        {
            _ChapterList.Add(chapter);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">预设容量</param>
        public CatalogueInfo(Int32 capacity)
        {
            _ChapterList = new List<ChapterInfo>((Int32)capacity);
        }
    }
}
