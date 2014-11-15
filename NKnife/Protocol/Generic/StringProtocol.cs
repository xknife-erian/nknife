﻿using System;
using System.Diagnostics;
using System.Text;
using Ninject;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;

namespace NKnife.Protocol.Generic
{
    /// <summary>协议的抽象实现
    /// </summary>
    public class StringProtocol : IProtocol<string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        public Func<StringProtocol> BuildMethod { get; set; }

        public StringProtocol()
        {
            Content = DI.Get<StringProtocolContent>();
            Packer = DI.Get<StringProtocolPacker>();
            UnPacker = DI.Get<StringProtocolUnPacker>();
            BuildMethod = NewInstance;
        }

        protected StringProtocol(string family, string command)
        {
            Content = DI.Get<StringProtocolContent>();
            Packer = DI.Get<StringProtocolPacker>();
            UnPacker = DI.Get<StringProtocolUnPacker>();
            Family = family;
            Command = command;
        }

        public StringProtocolPacker Packer { get; set; }

        public StringProtocolUnPacker UnPacker { get; set; }

        public StringProtocolContent Content { get; set; }

        #region IProtocol Members

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; set; }

        public string Command
        {
            get { return Content.Command; }
            set { Content.Command = value; } 
        }

        IProtocolPacker<string> IProtocol<string>.Packer
        {
            get { return Packer; }
            set { Packer = (StringProtocolPacker) value; }
        }

        IProtocolUnPacker<string> IProtocol<string>.UnPacker
        {
            get { return UnPacker; }
            set { UnPacker = (StringProtocolUnPacker) value; }
        }

        /// <summary>协议的具体内容的容器
        /// </summary>
        IProtocolContent<string> IProtocol<string>.Content
        {
            get { return Content; }
            set { Content = (StringProtocolContent) value; }
        }

        /// <summary>
        /// 根据当前实例生成协议的原生字符串表达
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            if (Content != null)
                return Packer.Combine(Content);
            return string.Empty;
        }

        /// <summary>
        /// 根据远端得到的数据包解析，将数据填充到本实例中
        /// </summary>
        /// <param name="datagram">The datas.</param>
        public void Parse(string datagram)
        {
            if (string.IsNullOrWhiteSpace(datagram))
            {
                Debug.Fail("空数据无法进行协议的解析");
                return;
            }
            try
            {
                StringProtocolContent c = Content;
                UnPacker.Execute(c, datagram, Family, Command);
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("协议字符串无法解析.{0}..{1}", e.Message, datagram));
            }
        }

        public StringProtocol NewInstance()
        {
            var protocol = DI.Get<StringProtocol>();
            protocol.Family = Family;
            protocol.Command = Command;
            return protocol;
        }

        IProtocol<string> IProtocol<string>.NewInstance()
        {
            return NewInstance();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StringProtocol) obj);
        }

        protected bool Equals(StringProtocol other)
        {
            return string.Equals(Family, other.Family) &&
                Packer.GetType() == other.Packer.GetType() &&
                UnPacker.GetType() == other.UnPacker.GetType() && 
                Content.Equals(other.Content) &&
                Equals(BuildMethod, other.BuildMethod);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (BuildMethod != null ? BuildMethod.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Packer != null ? Packer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UnPacker != null ? UnPacker.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Content != null ? Content.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Family != null ? Family.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            string str = string.Empty;
            try
            {
                str = Generate();
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("调用协议的ToString方法联动Generate()方法时异常。{0}", e.Message));
            }
            return str;
        }

        #endregion
    }
}