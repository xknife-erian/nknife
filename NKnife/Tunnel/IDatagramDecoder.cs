﻿namespace NKnife.Tunnel
{
    /// <summary>一个解码器工具接口
    /// </summary>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>
    /// <typeparam name="TSource">内容在原始状态时所使用的数据形式</typeparam>
    public interface IDatagramDecoder<out TData, in TSource>
    {
        /// <summary>
        /// 解码。将字节数组解析成指定的泛型结果。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="finishedIndex">已完成解码的数组的长度.</param>
        /// <returns>结果数组</returns>
        TData[] Execute(TSource data, out int finishedIndex);
    }
}
