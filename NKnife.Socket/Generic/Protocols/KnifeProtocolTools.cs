using System;
using System.Diagnostics;
using System.Xml;
using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Protocols
{
    /// <summary>协议工具接口集合
    /// </summary>
    public class KnifeProtocolTools : IProtocolTools
    {
        public IProtocolHead Head { get; private set; }
        public IProtocolTail Tail { get; private set; }
        public IProtocolPackager Packager { get; private set; }
        public IProtocolUnPackager UnPackager { get; private set; }
        [Inject]

        #region ICloneable Members

        public object Clone()
        {
            var t = new KnifeProtocolTools {Head = Head, Tail = Tail, Packager = Packager, UnPackager = UnPackager};
            return t;
        }

        #endregion
    }
}