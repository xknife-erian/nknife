namespace NKnife.Tunnel
{
    /// <summary>
    /// 一个编码器接口。
    /// </summary>
    /// <typeparam name="TOriginal">内容在编程过程所使用的数据形式</typeparam>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>

    public interface IDatagramEncoder<in TOriginal, out TData>
    {
        TData Execute(TOriginal data);
    }
}
