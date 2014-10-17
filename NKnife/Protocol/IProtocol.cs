namespace NKnife.Protocol
{
    /// <summary>描述一个通讯会话中一次交易的内容的封装。进出协议的将都是组好的字符串，字节数组的解码与编码在通讯器中完成。
    /// </summary>
    public interface IProtocol
    {
        /// <summary>本协议的命令字.
        /// </summary>
        /// <value>The command.</value>
        string Command { get; set; }

        /// <summary>本协议的家族.
        /// </summary>
        /// <value>The family.</value>
        string Family { get; set; }

        IProtocolPackager Packager { get; set; }
        IProtocolUnPackager UnPackager { get; set; }

        /// <summary>协议的具体内容.
        /// </summary>
        /// <value>The content.</value>
        IProtocolContent Content { get; set; }

        /// <summary>根据当前实例生成协议的原生字符串表达
        /// </summary>
        string Generate();

        /// <summary>
        /// 根据远端得到的数据包解析，将数据填充到本实例中
        /// </summary>
        void Parse(string datagram);

        IProtocol NewInstance();
    }
}