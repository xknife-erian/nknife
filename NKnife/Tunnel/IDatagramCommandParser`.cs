namespace NKnife.Tunnel
{
    /// <summary>
    /// 从原生消息体中获取命令字
    /// </summary>
    public interface IDatagramCommandParser<T>
    {
        /// <summary>从原生消息体中获取命令字
        /// </summary>
        /// <param name="datagram">The datagram.</param>
        /// <returns></returns>
        T GetCommand(T datagram);
    }
}
