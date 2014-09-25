namespace SocketKnife.Interfaces.Protocols
{
    /// <summary>
    ///     协议的尾部标记
    /// </summary>
    public interface IProtocolTail
    {
        byte[] Tail { get; }
    }
}