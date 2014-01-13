using System;
using System.Collections.Generic;
using System.Linq;
using Gean.Net.Config;
using NLog;
using System.IO;

namespace Gean.Net.Protocol
{
    /// <summary>
    /// 协议的抽象实现
    /// </summary>
    public class AbstractProtocol : IProtocol
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();
        private IProtocolContent _Content;

        /// <summary>构造函数
        /// </summary>
        /// <param name="family">协议家族名</param>
        /// <param name="command">协议命令字</param>
        public AbstractProtocol(string family, string command)
        {
            Family = family;
            Command = command;
            Tools = new ProtocolTools();            
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

        public IProtocolContent Content
        {
            get { return _Content ?? (_Content = Protocols.GetBlankContent(Family, Command)); }
        }

        public ProtocolTools Tools { get; set; }

        public KeyValuePair<string, string> FirstData { get; private set; }

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
                    _Logger.WarnE(string.Format("无法从协议中正确获取指定键({0})的值。", key), e);
                    return null;
                }
            }
        }

        public string Get(string key)
        {
            return this[key];
        }

        public void AddTag(object value)
        {
            Content.Tags.Add(value);
        }

        public void ClearTag()
        {
            Content.Tags.Clear();
        }

        public void RemoveTag(int index)
        {
            Content.Tags.RemoveAt(index);
        }

        public void SetCommandParam(string obj)
        {
            Content.CommandParam = obj;
        }

        public void ClearCommandParam()
        {
            Content.CommandParam = null;
        }

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

        public void RemoveData(string key)
        {
            Content.Datas.Remove(key);
        }

        public void ClearData()
        {
            Content.Datas.Clear();
        }

        public void AddInfo(string key, string value)
        {
            Content.Infomations.Add(key, value);
        }

        public void RemoveInfo(string key)
        {
            Content.Infomations.Remove(key);
        }

        public void ClearInfo()
        {
            Content.Infomations.Clear();
        }

        public string Protocol()
        {

            if (Content != null)
            {
                //File.WriteAllText(@"C:\Users\yys\Desktop\2.xml", Tools.Package.Combine(Content));
                return Tools.Package.Combine(Content);
            }
            return string.Empty;
        }

        public virtual void Parse(string datagram)
        {
            if (string.IsNullOrWhiteSpace(datagram))
            {
                return;
            }
            try
            {
                _Content = Tools.Parser.Execute(datagram, Family, Command);
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("协议字符串无法解析.{0}..{1}", e.Message, datagram), e);
            }
            if (null != _Content && _Content.Datas.Count > 0)
            {
                FirstData = new KeyValuePair<string, string>(_Content.Datas.GetKey(0), _Content.Datas.Get(0));
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
                _Logger.Warn(string.Format("调用协议的ToString方法联动Protocol()方法时异常。{0}", e.Message), e);
            }
            return str;
        }
    }
}