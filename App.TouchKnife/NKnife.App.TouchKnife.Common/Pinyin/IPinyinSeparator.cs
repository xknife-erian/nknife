using System.Collections.Generic;

namespace NKnife.App.TouchKnife.Common.Pinyin
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