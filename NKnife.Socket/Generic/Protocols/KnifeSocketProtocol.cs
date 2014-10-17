using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Protocol;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Protocols
{
    /// <summary>协议的抽象实现
    /// </summary>
    public class KnifeSocketProtocol : IProtocol
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        /// <summary>构造函数
        /// </summary>
        /// <param name="family">协议家族名</param>
        /// <param name="command">协议命令字</param>
        protected KnifeSocketProtocol(string family, string command)
        {
            Family = family;
            Command = command;
        }

        #region IProtocol Members

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; set; }

        public string Command { get; set; }

        [Inject]
        public IProtocolPackager Packager { get; set; }

        [Inject]
        public IProtocolUnPackager UnPackager { get; set; }

        /// <summary>协议的具体内容的容器
        /// </summary>
        [Inject]
        IProtocolContent IProtocol.Content
        {
            get { return Content; }
            set { Content = (KnifeSocketProtocolContent) value; }
        }

        public KnifeSocketProtocolContent Content { get; set; }

        /// <summary>
        /// 根据当前实例生成协议的原生字符串表达
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            if (Content != null)
                return Packager.Combine(Content);
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
                IProtocolContent c = Content;
                UnPackager.Execute(ref c, datagram, Family, Command);
            }
            catch (Exception e)
            {
                _logger.Error(() => string.Format("协议字符串无法解析.{0}..{1}", e.Message, datagram));
            }
        }

        public KnifeSocketProtocol NewInstance()
        {
            var p = new KnifeSocketProtocol(Family, Command);
            p.Packager = Packager;
            p.UnPackager = UnPackager;
            p.Content = Content.NewInstance();
            return p;
        }

        IProtocol IProtocol.NewInstance()
        {
            return NewInstance();
        }

        #endregion

        public override string ToString()
        {
            string str = string.Empty;
            try
            {
                str = Generate();
            }
            catch (Exception e)
            {
                _logger.Warn(() => string.Format("调用协议的ToString方法联动Generate()方法时异常。{0}", e.Message));
            }
            return str;
        }

    }
}