using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Xml;
using NKnife.Events;
using NKnife.Util;
using XmlDocument = System.Xml.XmlDocument;
using XmlElement = System.Xml.XmlElement;

namespace NKnife.XML
{
    /// <summary>
    /// 便于快速使用的将Key和Value存储为XML的文件操作类
    /// </summary>
    public abstract class QuickXml
    {
        private readonly Timer _timer = new(10 * 1000);
        private readonly XmlDocument _xml;
        private readonly string _xmlFile;
        private XmlElement _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileFullName"></param>
        protected QuickXml(string fileFullName)
        {
            _xmlFile = fileFullName;

            if(!File.Exists(_xmlFile))
            {
                _xml = XmlUtil.CreateNewDocument(_xmlFile, "configuration");
            }
            else
            {
                _xml = new XmlDocument();
                _xml.Load(_xmlFile);
            }

            _configuration = _xml.DocumentElement!;
            var firstUse = true;

            if(!_configuration.HasAttribute("firstUse"))
            {
                UpdateFirstUseState(_configuration);
            }
            else if(!bool.TryParse(_configuration.GetAttribute("firstUse"), out var value) || value)
            {
                UpdateFirstUseState(_configuration);
            }
            else
            {
                firstUse = false;
            }

            if(firstUse) //当第一次启动软件时
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                FirstUse();
            }

            if(AutoLoad)
            {
                _timer.Elapsed += (sender, args) =>
                {
                    _xml.Load(_xmlFile);
                    _configuration = _xml.DocumentElement!;
                };
            }

            var watcher = new FileSystemWatcher
            {
                Path         = _xmlFile,                // 指定要监视的目录  
                NotifyFilter = NotifyFilters.LastWrite, // 监视文件写入操作  
                Filter       = "*.txt"                  // 只监视.txt文件，如果需要监视所有文件，可以设置为"*.*"  
            };

            watcher.Changed             += OnChanged; // 注册文件更改事件的处理程序  
            watcher.EnableRaisingEvents =  true;      // 开始监视  

            // 事件处理程序  
            void OnChanged(object source, FileSystemEventArgs e)
            {
                // 处理文件更改事件  
                Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
            }
        }

        public bool AutoLoad { get; set; } = false;

        protected virtual string EleName => "item";
        protected virtual string OptionAttr => "key";

        private void UpdateFirstUseState(XmlElement root)
        {
            root.SetAttribute("firstUse", false.ToString());
            Save();
        }

        protected virtual void Save()
        {
            _xml.Save(_xmlFile);
        }

        protected virtual void FirstUse() { }

        /// <summary>
        ///     获取所有选项的索引项列表
        /// </summary>
        /// <returns>选项的索引项列表</returns>
        public IEnumerable<string> GetKeys()
        {
            var keys  = new List<string>();
            var nodes = _configuration.SelectNodes($"//{EleName}");

            if(nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    switch (node.NodeType)
                    {
                        case XmlNodeType.Element:
                            keys.Add(((XmlElement)node).GetAttribute(OptionAttr));

                            break;
                    }
                }
            }

            return keys;
        }

        /// <summary>
        ///     获取指定Key的值，如果不能获取到且<see cref="defaultValue" />值不为空时，设置值为<see cref="defaultValue" />
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">默认值</param>
        public T GetValue<T>(string key, T defaultValue)
        {
            if(!TryGetValue(key, out T? value))
            {
                value = defaultValue;
                SetValue(key, defaultValue!);
            }

            return value!;
        }

        /// <summary>
        ///     尝试获取指定Key的值
        /// </summary>
        public bool TryGetValue<T>(string key, out T? value)
        {
            value = default;
            var ele   = SelectOptionElement(key);
            var cdata = ele?.GetCDataElement();

            if(ele == null
               || cdata == null)
                return false;
            var json = cdata.Value;
            if(!string.IsNullOrWhiteSpace(json))
                value = Deserialize<T>(json);

            return true;
        }

        /// <summary>
        ///     设置指定Key的值，值对象序列化成Json保存
        /// </summary>
        public void SetValue(string key, object value)
        {
            var ele = SelectOptionElement(key);

            if(ele == null)
            {
                ele = CreateOptionElement(key, value);
                _configuration.AppendChild(ele);
            }
            else
            {
                ele.RemoveAllCDataSection();
                var json = Serialize(value);
                ele.SetCDataElement(json);
            }

            Save();
            OnValueUpdated(new DataChangedEventArgs<KeyValuePair<string, object>>(new KeyValuePair<string, object>(key, value)));
        }

        protected virtual XmlElement? SelectOptionElement(string key)
        {
            return _configuration.SelectSingleNode($"//{EleName}[@{OptionAttr}='{key}']") as XmlElement;
        }

        protected virtual XmlElement CreateOptionElement(string key, object value)
        {
            var ele = _xml.CreateElement(EleName);
            ele.SetAttribute(OptionAttr, key);
            var json = Serialize(value.ToString);
            ele.SetCDataElement(json);

            return ele;
        }

        protected abstract string Serialize(object value);

        protected abstract T? Deserialize<T>(string json);

        /// <summary>
        ///     当选项值发生更新后
        /// </summary>
        public event EventHandler<DataChangedEventArgs<KeyValuePair<string, object>>>? ValueUpdated;

        private void OnValueUpdated(DataChangedEventArgs<KeyValuePair<string, object>> e)
        {
            ValueUpdated?.Invoke(this, e);
        }
    }
}