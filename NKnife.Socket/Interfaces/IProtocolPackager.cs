namespace SocketKnife.Interfaces
{
    /// <summary>
    /// 描述一个将协议内容按指定的格式组装成一个指定类型(一般是字符串，但也可以是任何，如文件)
    /// </summary>
    public interface IProtocolPackager
    {
        short Version { get; }
        string Combine(IProtocolContent iProtocolContent);
    }
}
