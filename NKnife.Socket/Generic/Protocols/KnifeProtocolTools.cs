using System;
using System.Diagnostics;
using System.Xml;
using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Protocols
{
    /// <summary>协议工具接口集合
    /// </summary>
    public abstract class KnifeProtocolTools : IProtocolTools
    {
        public IProtocolPackager Packager { get; set; }
        public IProtocolUnPackager UnPackager { get; set; }

        #region ICloneable Members

        public abstract object Clone();

        #endregion
    }
}