using System;
using System.Collections.Generic;

namespace NKnife.TouchInput.Common.PinyinIme
{
    public interface ISeparator
    {
        List<string> Separate(string pinyin);
    }
}