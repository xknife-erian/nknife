using System;
namespace Jeelu.WordSeg
{
    public class MatchWord
    {
        /// <summary>
        /// 匹配的词
        /// </summary>
        public String Word { get; set; }
        /// <summary>
        /// 单词首字符在全文中的位置
        /// </summary>
        public int Position { get; set; }

        public int Length { get; set; }

        public MatchWord(string word, int postition, int length)
        {
            this.Word = word; 
            this.Position = postition; 
            this.Length = length;
        }
    }
}
