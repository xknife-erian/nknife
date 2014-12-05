using System;
using System.Collections.Generic;

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

        /// <summary>
        ///     协议命令字参数
        /// </summary>
        /// <value>The command param.</value>
        T CommandParam { get; set; }

        /// <summary>
        ///     获取协议的固定数据
        ///     Infomations,Tags均属于协议的内容
        ///     Infomations:固定数据，按协议规定的必须每次携带的数据
        ///     Tags:内容较大的数据,一般为可序列化的对象
        /// </summary>
        Dictionary<string, T> Infomations { get; }

        /// <summary>
        ///     获取协议的大数据
        ///     Infomations,Tags均属于协议的内容
        ///     Infomations:固定数据，按协议规定的必须每次携带的数据
        ///     Tags:内容较大的数据,一般为可序列化的对象
        /// </summary>
        List<object> Tags { get; set; }

        /// <summary>本协议的家族.
        /// </summary>
        /// <value>The family.</value>
        string Family { get; set; }

        IProtocol<T> NewInstance();
    }
}