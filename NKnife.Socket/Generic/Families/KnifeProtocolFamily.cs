﻿using System;
using System.Collections.Generic;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    /// <summary>
    /// 协议族
    /// </summary>
    [Serializable]
    public class KnifeProtocolFamily : Dictionary<string, IProtocol>, IProtocolFamily
    {
        public KnifeProtocolFamily()
        {
        }

        public KnifeProtocolFamily(string name)
        {
            Family = name;
        }

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; private set; }

        public virtual IDatagramCommandParser CommandParser { get; set; }
        public virtual IDatagramDecoder Decoder { get; set; }
        public virtual IDatagramEncoder Encoder { get; set; }
        public void Add(IProtocol protocol)
        {
            Add(protocol.Command, protocol);
        }

        public IProtocol NewProtocol(string command)
        {
            return this[command].NewInstance();
        }
    }
}