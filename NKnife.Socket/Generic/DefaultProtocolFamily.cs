using System;
using System.Collections.Generic;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Generic
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

        public Type DefaultContentType { get; private set; }

        public IProtocol Get(string familyType, string command)
        {
            throw new NotImplementedException();
        }
    }
}