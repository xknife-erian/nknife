namespace NKnife.Tunnel
{
    /// <summary>
    /// 协议的编解码器,源数据一般是字节数组
    /// </summary>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>
    public interface ITunnelCodecBase<TData> : ITunnelCodec<TData, byte[]>
    {
    }
}