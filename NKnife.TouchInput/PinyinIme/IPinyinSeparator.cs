using System;
using System.Collections.Generic;

namespace NKnife.TouchInput.Common.PinyinIme
{
    /// <summary>
    /// ƴ���ָ����ӿ�
    /// </summary>
    public interface IPinyinSeparator
    {
        /// <summary>
        /// �ָ�ָ����ƴ���ַ���
        /// </summary>
        /// <param name="pinyin">ָ����ƴ���ַ���</param>
        /// <returns></returns>
        List<string> Separate(string pinyin);
    }
}