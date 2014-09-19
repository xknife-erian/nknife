using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 单词与词性
    /// </summary>
    [Serializable]
    internal class WordPos
    {
        internal WordPos(string word, int pos)
        {
            this.Word = word;
            this.Pos = pos;
        }
        /// <summary>
        /// 单词
        /// </summary>
        internal String Word { get; set; }

        /// <summary>
        /// 词性
        /// </summary>
        internal int Pos { get; set; }

        public override bool Equals(object obj)
        {
            WordPos wp = (WordPos)obj;
            if (this.Word != wp.Word)
            {
                return false;
            }
            if (this.Pos != wp.Pos)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode() ^ Pos.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Word).Append("[").Append(this.Pos.ToString()).Append("]");
            return sb.ToString();
        }
    }
}
