using System.Collections.Generic;
using System.Collections.Specialized;

namespace NKnife.Net.Protocol
{
    /// <summary>
    /// 一个协议的具体内容。其中：
    /// Datas,Tags,Infomations均属于协议的内容。
    /// Datas:一般的简单数据，（一般较短）。
    /// Tags:内容较大的数据，如序列化的对象,一个压缩文件的Base64码等。
    /// Infomations:固定数据，按协议规定的必须每次携带的数据。
    /// </summary>
    public interface IProtocolContent
    {
        /// <summary>
        /// 描述Socket服务协议的一条消息的消息头。
        /// </summary>
        IProtocolHead Head { get; }

        /// <summary>
        /// 协议命令字
        /// </summary>
        string Command { get; set; }
        
        /// <summary>
        /// 协议命令字参数
        /// </summary>
        /// <value>The command param.</value>
        string CommandParam { get; set; }

        /// <summary>获取协议的简单数据
        /// Datas,Tags,Infomations均属于协议的内容
        /// Datas:一般的简单数据，（一般较短）
        /// Tags:内容较大的数据
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// </summary>
        NameValueCollection Datas { get; }

        /// <summary>获取协议的大数据
        /// Datas,Tags,Infomations均属于协议的内容
        /// Datas:一般的简单数据，（一般较短）
        /// Tags:内容较大的数据
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// </summary>
        List<object> Tags { get; set; }

        /// <summary>获取协议的固定数据
        /// Datas,Tags,Infomations均属于协议的内容
        /// Datas:一般的简单数据，（一般较短）
        /// Tags:内容较大的数据
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// </summary>
        Dictionary<string, string> Infomations { get; }

        /// <summary>描述Socket服务协议的一条消息的消息头。
        /// </summary>
        IProtocolTail Tail { get; }
    }
}
