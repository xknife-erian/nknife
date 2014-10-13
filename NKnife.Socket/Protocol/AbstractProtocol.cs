using System;
using System.Collections.Generic;
using System.Linq;
using NKnife.Adapters;
using NKnife.Interface;
using SocketKnife.Generic;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Protocol
{
    /// <summary>协议的抽象实现
    /// </summary>
    public abstract class AbstractProtocol : IProtocol
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        /// <summary>构造函数
        /// </summary>
        /// <param name="family">协议家族名</param>
        /// <param name="command">协议命令字</param>
        protected AbstractProtocol(string family, string command)
        {
            Family = family;
            Command = command;
            Tools = new DefaultProtocolTools();
        }

        #region IProtocol Members

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; protected set; }

        /// <summary>Gets or sets 协议命令字
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; set; }

        /// <summary>协议的具体内容的容器
        /// </summary>
        public IProtocolContent Content { get; set; }

        /// <summary>针对协议工作的工具
        /// </summary>
        public IProtocolTools Tools { get; set; }

        /// <summary>获取第一个数据(往往协议中数据不多，当只有一个数据时用该属性比较方便)
        /// </summary>
        /// <value>
        /// The first data.
        /// </value>
        public KeyValuePair<string, string> FirstData { get; private set; }

        /// <summary>Gets the <see cref="System.String"/> with the specified key.
        /// </summary>
        public string this[string key]
        {
            get
            {
                try
                {
                    string value = Content.Datas[key];
                    if (!string.IsNullOrEmpty(value))
                        return value;
                    if (Content.Infomations.ContainsKey(key))
                        return Content.Infomations[key];
                    return null;
                }
                catch (Exception e)
                {
                    _logger.Warn(string.Format("无法从协议中正确获取指定键({0})的值。", key), e);
                    return null;
                }
            }
        }

        /// <summary>根据指定的键获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return this[key];
        }

        /// <summary>
        /// 增加一个对象做为协议数据
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddTag(object value)
        {
            Content.Tags.Add(value);
        }

        /// <summary>
        /// 清除所有做为协议数据的对象。
        /// </summary>
        public void ClearTag()
        {
            Content.Tags.Clear();
        }

        /// <summary>
        /// 移除指定索引的协议数据
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveTag(int index)
        {
            Content.Tags.RemoveAt(index);
        }

        /// <summary>
        /// 设置命令字参数.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public void SetCommandParam(string obj)
        {
            Content.CommandParam = obj;
        }

        /// <summary>
        /// 清除命令字参数
        /// </summary>
        public void ClearCommandParam()
        {
            Content.CommandParam = null;
        }

        /// <summary>增加类似键值对样式的数据
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddData(string key, string value)
        {
            if (Content.Datas.Keys.Cast<string>().Any(tmpKey => tmpKey.Equals(key)))
            {
                Content.Datas.Remove(key);
            }
            Content.Datas.Add(key, value);

            if (Content.Datas.Count == 1)
            {
                FirstData = new KeyValuePair<string, string>(key, value);
            }
        }

        /// <summary>
        /// 移除指定键值的数据
        /// </summary>
        /// <param name="key">The key.</param>
        public void RemoveData(string key)
        {
            Content.Datas.Remove(key);
        }

        /// <summary>
        /// 清除所有数据
        /// </summary>
        public void ClearData()
        {
            Content.Datas.Clear();
        }

        /// <summary>
        /// 增加固定信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddInfo(string key, string value)
        {
            Content.Infomations.Add(key, value);
        }

        /// <summary>
        /// 移除指定的信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        /// <param name="key">The key.</param>
        public void RemoveInfo(string key)
        {
            Content.Infomations.Remove(key);
        }

        /// <summary>
        /// 清除所有信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        public void ClearInfo()
        {
            Content.Infomations.Clear();
        }

        /// <summary>
        /// 根据当前实例生成协议的原生字符串表达
        /// </summary>
        /// <returns></returns>
        public string Protocol()
        {
            if (Content != null)
                return Tools.Packager.Combine(Content);
            return string.Empty;
        }

        /// <summary>
        /// 根据远端得到的数据包解析，将数据填充到本实例中
        /// </summary>
        /// <param name="datagram">The datas.</param>
        public virtual void Parse(string datagram)
        {
            if (Content == null)
                throw new NullReferenceException("协议数据容器为空:IProtocolContent");
            if (string.IsNullOrWhiteSpace(datagram))
                return;
            try
            {
                IProtocolContent c = Content;
                Tools.UnPackager.Execute(ref c, datagram, Family, Command);
            }
            catch (Exception e)
            {
                _logger.Error(() => string.Format("协议字符串无法解析.{0}..{1}", e.Message, datagram));
            }
            if (null != Content && Content.Datas.Count > 0)
            {
                FirstData = new KeyValuePair<string, string>(Content.Datas.GetKey(0), Content.Datas.Get(0));
            }
        }

        #endregion

        public override string ToString()
        {
            string str = string.Empty;
            try
            {
                str = Protocol();
            }
            catch (Exception e)
            {
                _logger.Warn(() => string.Format("调用协议的ToString方法联动Protocol()方法时异常。{0}", e.Message));
            }
            return str;
        }
    }
}