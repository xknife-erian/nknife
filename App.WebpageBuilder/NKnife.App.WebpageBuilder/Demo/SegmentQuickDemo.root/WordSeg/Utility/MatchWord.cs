using System;
namespace Jeelu.WordSeg
{
    public class MatchWord
    {
        /// <summary>
        /// ƥ��Ĵ�
        /// </summary>
        public String Word { get; set; }
        /// <summary>
        /// �������ַ���ȫ���е�λ��
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
