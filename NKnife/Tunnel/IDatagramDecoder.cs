namespace NKnife.Tunnel
{
    /// <summary>一个解码器工具接口
    /// </summary>
    /// <typeparam name="TOriginal">内容在编程过程所使用的数据形式</typeparam>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>
    public interface IDatagramDecoder<out TOriginal, in TData>
    {
        /// <summary>
        /// 解码。将字节数组解析成指定的泛型结果。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="finishedIndex">已完成解码的数组的长度.</param>
        /// <returns>结果数组</returns>
        TOriginal[] Execute(TData data, out int finishedIndex);
    }
}
