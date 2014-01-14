using System;
using Gean.Net.Protocol;
using NKnife.Utility;

namespace NKnife.Net.Protocol.General
{
    /// <summary>
    /// 一个最常用的 字符数组 => 字符串 转换器。
    /// </summary>
    public class GeneralDatagramDecoder : IDatagramDecoder
    {
        #region IDatagramDecoder Members

        public bool HasLengthOnHead
        {
            get { return false; }
        }

        public string[] Execute(byte[] data, out int done)
        {
            done = data.Length;
            string[] result;
            string src = UtilityString.TidyUTF8(data);
            if (src.Contains("ö"))
                result = src.Split(new[] { 'ö' }, StringSplitOptions.RemoveEmptyEntries);
            else
                result = new[] {src};
            if (result.Length > 1)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = UtilityString.TidyUTF8(result[i]).Trim();
                }
            }
            return result;
        }

        #endregion
    }
}