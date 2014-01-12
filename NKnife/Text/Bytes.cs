using System;
using System.Collections.Generic;
using System.Text;

namespace NKnife.Text
{
    /// <summary>
    /// FileName: Bytes.cs
    /// CLRVersion: 4.0.30319.18052
    /// Author: 黄阳 HuangYang@p-an.com
    /// Corporation:p-an
    /// Description:
    /// DateTime: 2013/7/29 星期一 13:59:51
    /// 该类是byte[] 常用操作工具类
    /// </summary>
    public static class Bytes
    {
        /// <summary>将字节数组转换为十六进制字符串
        /// </summary>
        public static string ToHexString(this IEnumerable<byte> bytes, char spliter = ' ')
        {
            if (bytes == null) throw new NullReferenceException("字节数组为空");
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2")).Append(spliter);
            return sb.ToString().TrimEnd(spliter);
        }
    }
}
