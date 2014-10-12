using System;
using System.Collections.Generic;
using SocketKnife.Protocol;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Default
{
    /// <summary>
    /// 协议族
    /// </summary>
    [Serializable]
    public class DefaultProtocolFamily : Dictionary<string, Type>, IProtocolFamily
    {
        public DefaultProtocolFamily()
        {
            
        }

        public DefaultProtocolFamily(string name, Type type)
        {
            Family = name;
            DefaultContentType = type;
        }

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; private set; }

        /// <summary>默认协议工具接口集合
        /// </summary>
        /// <value>The default tool.</value>
        //internal ProtocolTools DefaultTools { get; private set; }

        public Type DefaultContentType { get; private set; }
    }
}