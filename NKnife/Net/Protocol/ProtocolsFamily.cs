using System;
using System.Collections.Generic;

namespace NKnife.Net.Protocol
{
    /// <summary>
    /// 协议族
    /// </summary>
    [Serializable]
    public class ProtocolsFamily : Dictionary<string, Type>
    {
        public ProtocolsFamily(string name, Type type)
        {
            this.Family = name;
            this.DefaultContentType = type;
            this.DefaultTools = new ProtocolTools();
        }
        /// <summary>
        /// Gets or sets 协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; private set; }
        /// <summary>
        /// Gets or sets 默认协议工具接口集合
        /// </summary>
        /// <value>The default tool.</value>
        internal ProtocolTools DefaultTools { get; private set; }
                
        internal Type DefaultContentType { get; private set; }
    }
}
