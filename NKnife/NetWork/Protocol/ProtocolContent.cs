using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using NKnife.NetWork.Interfaces;

namespace NKnife.NetWork.Protocol
{
    /// <summary>
    /// 一个协议的具体内容。其中：
    /// Datas,Tags,Infomations均属于协议的内容。
    /// Datas:一般的简单数据，（一般较短）。
    /// Tags:内容较大的数据，如序列化的对象等。
    /// Infomations:固定数据，按协议规定的必须每次携带的数据。
    /// </summary>
    public class ProtocolContent : IProtocolContent
    {
        public ProtocolContent()
        {
            this.Datas = new NameValueCollection();
            this.Infomations = new Dictionary<string, string>(0);
            this.Tags = new List<object>(0);
        }

        /// <summary>
        /// 协议命令字
        /// </summary>
        /// <value></value>
        public string Command { get; set; }
        /// <summary>
        /// 协议命令字参数
        /// </summary>
        /// <value>The command param.</value>
        public string CommandParam { get; set; }
        /// <summary>
        /// 获取协议的大数据
        /// Datas,Tags,Infomations均属于协议的内容
        /// Datas:一般的简单数据，（一般较短）
        /// Tags:内容较大的数据
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// </summary>
        /// <value></value>
        public List<object> Tags { get; set; }
        /// <summary>
        /// 获取协议的简单数据
        /// Datas,Tags,Infomations均属于协议的内容
        /// Datas:一般的简单数据，（一般较短）
        /// Tags:内容较大的数据
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// </summary>
        /// <value></value>
        public NameValueCollection Datas { get; private set; }
        /// <summary>
        /// 获取协议的固定数据
        /// Datas,Tags,Infomations均属于协议的内容
        /// Datas:一般的简单数据，（一般较短）
        /// Tags:内容较大的数据
        /// Infomations:固定数据，按协议规定的必须每次携带的数据
        /// </summary>
        /// <value></value>
        public Dictionary<string, string> Infomations { get; private set; }

        /// <summary>
        /// 描述Socket服务协议的一条消息的消息头。
        /// </summary>
        /// <value></value>
        public IProtocolHead Head { get; private set; }
        /// <summary>
        /// 描述Socket服务协议的一条消息的消息头。
        /// </summary>
        /// <value></value>
        public IProtocolTail Tail { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Command:").Append(this.Command).Append(" >>> ");
            IEnumerator myEnumerator = Datas.GetEnumerator();
            while((myEnumerator.MoveNext()))
            {
                sb.Append("[").Append(myEnumerator.Current).Append("]");
            }
            return base.ToString();
        }

    }
}
