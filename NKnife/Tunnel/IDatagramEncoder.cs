﻿namespace NKnife.Tunnel
{
    /// <summary>
    /// 一个最常用的 字符串 => 字节数组 转换器。
    /// </summary>
    public interface IDatagramEncoder<in T>
    {
        byte[] Execute(T replay);
    }
}
