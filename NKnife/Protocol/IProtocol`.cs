using System;

namespace NKnife.Protocol
{
    /// <summary>描述一个通讯会话中一次交易的内容的封装。进出协议的将都是组好的字符串，字节数组的解码与编码在通讯器中完成。
    /// </summary>
    public interface IProtocol<T>// : IEquatable<T>
    {
        /// <summary>本协议的命令字.
        /// </summary>
        /// <value>The command.</value>
        T Command { get; set; }

        /// <summary>本协议的家族.
        /// </summary>
        /// <value>The family.</value>
        string Family { get; set; }

        IProtocolPacker<T> Packer { get; set; }
        IProtocolUnPacker<T> UnPacker { get; set; }

        /// <summary>协议的具体内容.
        /// </summary>
        /// <value>The content.</value>
        IProtocolContent<T> Content { get; set; }

        /// <summary>根据当前实例生成协议的原生字符串表达
        /// </summary>
        T Generate();

        /// <summary>
        /// 根据远端得到的数据包解析，将数据填充到本实例中
        /// </summary>
        void Parse(T datagram);

        IProtocol<T> NewInstance();
    }
}