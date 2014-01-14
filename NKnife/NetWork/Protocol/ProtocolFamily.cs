using System;
using System.Collections.Generic;

namespace NKnife.NetWork.Protocol
{
    /// <summary>
    /// 协议族
    /// </summary>
    [Serializable]
    public class ProtocolFamily : Dictionary<string, Type>
    {
        public ProtocolFamily(string name, Type type)
        {
            Family = name;
            DefaultContentType = type;
            DefaultTools = new ProtocolTools();
        }

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; private set; }

        /// <summary>默认协议工具接口集合
        /// </summary>
        /// <value>The default tool.</value>
        internal ProtocolTools DefaultTools { get; private set; }

        internal Type DefaultContentType { get; private set; }
    }
}