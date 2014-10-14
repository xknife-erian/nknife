using System.Collections.Generic;
using System.Collections.Specialized;

namespace SocketKnife.Interfaces
{
    /// <summary>
    /// 一个协议的具体内容。其中：
    /// Infomations,Tags均属于协议的内容
    /// Infomations:固定数据，按协议规定的必须每次携带的数据
    /// Tags:内容较大的数据,一般为可序列化的对象
    /// </summary>
    public interface IProtocolContent
    {
        /// <summary>
        /// 协议命令字
        /// </summary>
        string Command { get; set; }

        /// <summary>
        /// 协议命令字参数
        /// </summary>
        /// <value>The command param.</value>
        string CommandParam { get; set; }

        /// <summary>获取协议的固定数据
        /// Infomations,Tags均属于协议的内容
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// Tags:内容较大的数据,一般为可序列化的对象
        /// </summary>
        Dictionary<string, string> Infomations { get; }

        /// <summary>获取协议的大数据
        /// Infomations,Tags均属于协议的内容
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// Tags:内容较大的数据,一般为可序列化的对象
        /// </summary>
        List<object> Tags { get; set; }

    }
}