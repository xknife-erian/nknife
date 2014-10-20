using System;
using NKnife.Tunnel;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    /// <summary>
    /// 一个最常用的 字符数组 => 字符串 转换器。
    /// </summary>
    public class EndMarkDecoder : KnifeSocketDatagramDecoder
    {
        public override string[] Execute(byte[] data, out int done)
        {
            done = data.Length;
            string src = UtilityString.TidyUTF8(data);
            string[] result = src.Contains("ö") ? src.Split(new[] { 'ö' }, StringSplitOptions.RemoveEmptyEntries) : new[] {src};
            if (result.Length > 1)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = UtilityString.TidyUTF8(result[i]).Trim();
                }
            }
            return result;
        }
    }
}