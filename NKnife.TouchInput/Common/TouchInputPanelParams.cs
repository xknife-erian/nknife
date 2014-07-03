using System;
using System.Drawing;

namespace NKnife.TouchInput.Common
{
    public class TouchInputPanelParams
    {
        /// <summary>
        /// 输入方式
        /// </summary>
        public enum InputMode
        {
            Pinyin,
            Letter,
            HandWriter,
            Number
        }

        public TouchInputPanelParams()
        {
            WordsStripLocation = new Point(0, 0);
            InputPanelType = InputMode.Pinyin;
        }

        /// <summary>
        /// 中文语言Id
        /// </summary>
        public static readonly int SimplifiedChineseLanguageId = 0x0804;

        /// <summary>
        /// 空候选词数组
        /// </summary>
        public static readonly string[] EmptyAlternates = new string[0];

        public Point InputPanelLocation { get; set; }
        public Point WordsStripLocation { get; set; }
        public InputMode InputPanelType { get; set; }
        public String CurrentWord { get; set; }
    }
}