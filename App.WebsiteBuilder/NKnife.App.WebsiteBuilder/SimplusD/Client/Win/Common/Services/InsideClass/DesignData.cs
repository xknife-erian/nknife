using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class DesignData
        {
            static private string _configFilePath;
            static private XmlDocument _config;
            static private XmlElement _singleData;

            static void Save()
            {
                _config.Save(_configFilePath);
            }

            static public void Load(string configFilePath)
            {
                ///加载配置文件
                _configFilePath = configFilePath;
                _config = new XmlDocument();

            StartLoad:
                if (File.Exists(configFilePath))
                {
                    try
                    {
                        _config.Load(configFilePath);
                        _singleData = (XmlElement)_config.DocumentElement.SelectSingleNode(@"singleData");
                        if (_singleData == null)
                        {
                            throw new System.Exception();
                        }
                    }
                    catch (System.Exception)
                    {
                        File.Delete(configFilePath);
                        goto StartLoad;
                    }
                }
                else
                {
                    _config.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
<designData>
 <singleData></singleData>
</designData>");
                    _singleData = (XmlElement)_config.DocumentElement.SelectSingleNode(@"singleData");
                }
            }

            static Regex _regex = new Regex(@"^[\._a-zA-Z0-9]+$");
            /// <summary>
            /// 通过指定key值，找存储的value值(参数key的取值只能是"_.a-zA-Z0-9")
            /// </summary>
            static public string GetValue(string key)
            {
                ///验证参数的合法性
                Debug.Assert(!string.IsNullOrEmpty(key));
                if (!_regex.IsMatch(key))
                {
                    throw new ArgumentException(@"参数不合法。取值范围是:""._a-zA-Z0-9""", "key");
                }

                ///在_singleData节点里找指定key的值
                XmlElement ele = _singleData[key];
                if (ele == null)
                {
                    return null;
                }
                return ele.InnerText;
            }

            /// <summary>
            /// 为指定的key值设置value
            /// </summary>
            static public void SetValue(string key, string value)
            {
                ///验证参数的合法性
                Debug.Assert(!string.IsNullOrEmpty(key));
                if (!_regex.IsMatch(key))
                {
                    throw new ArgumentException(@"参数不合法。取值范围是:""._a-zA-Z0-9""", "key");
                }

                ///在_singleData节点找指定的key，并设置，若没有此key，则创建
                XmlElement ele = _singleData[key];
                if (ele == null)
                {
                    ele = _singleData.OwnerDocument.CreateElement(key);
                    _singleData.AppendChild(ele);
                }
                ele.InnerText = value;

                Save();
            }
        }
    }
}