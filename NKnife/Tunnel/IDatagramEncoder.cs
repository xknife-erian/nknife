namespace NKnife.Tunnel
{
    /// <summary>
    /// 一个编码器接口。结果码是将被传输的数据类型，比如Socket与Serial的byte[]。
    /// </summary>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>
    /// <typeparam name="TSource">内容在原始状态时所使用的数据形式</typeparam>
    public interface IDatagramEncoder<in TData, out TSource>
    {
        /// <summary>
        /// 执行编码
        /// </summary>
        /// <param name="data">需被编码的内容</param>
        /// <returns>结果码是将被传输的数据类型，比如Socket与Serial的byte[]。</returns>
        TSource Execute(TData data);
    }
}
