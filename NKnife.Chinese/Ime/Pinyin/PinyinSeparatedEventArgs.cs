using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Chinese.Ime.Pinyin
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
