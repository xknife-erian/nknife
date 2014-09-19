using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 单词的信息
    /// </summary>
    public class WordInfo
    {
        public WordInfo()
        {

        }
        public WordInfo(string word, int pos, int rank, object tag)
        {
            this.Word = word;
            this.Position = pos;
            this.Rank = rank;
            this.Tag = tag;
        }
        /// <summary>
        /// 单词
        /// </summary>
        public string Word;

        /// <summary>
        /// 单词首字符在全文中的位置
        /// </summary>
        public int Position;

        /// <summary>
        /// 单词的权重级别
        /// </summary>
        public int Rank;

        /// <summary>
        /// 单词对应的标记
        /// </summary>
        public object Tag;
    }
}
