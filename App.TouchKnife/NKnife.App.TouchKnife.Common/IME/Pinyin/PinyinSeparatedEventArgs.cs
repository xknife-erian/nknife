using System;
using System.Collections.Generic;

namespace NKnife.App.TouchKnife.Common.IME.Pinyin
{
    public class PinyinSeparatedEventArgs : EventArgs
    {
        public PinyinSeparatedEventArgs(List<string> pinyin)
        {
            Pinyin = pinyin;
        }

        public List<string> Pinyin { get; set; }
    }
}
