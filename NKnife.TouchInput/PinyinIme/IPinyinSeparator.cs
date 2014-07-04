using System;
using System.Collections.Generic;

namespace NKnife.TouchInput.Common.PinyinIme
{
    /// <summary>
    /// 拼音分割器接口
    /// </summary>
    public interface IPinyinSeparator
    {
        /// <summary>
        /// 分割指定的拼音字符串
        /// </summary>
        /// <param name="pinyin">指定的拼音字符串</param>
        /// <returns></returns>
        List<string> Separate(string pinyin);
    }
}