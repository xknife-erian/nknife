using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        public static class Property
        {
            static DefaultProperties defaultProperties = new DefaultProperties();

            public static void Initialize()
            {
            }

            public static IProperties Properties
            {
                get
                {
                    return defaultProperties;
                }
            }
        }

        public interface IProperties
        {
            object GetProperty(string key);
            void SetProperty(string key, object val);

            T GetProperty<T>(string key);
            void SetProperty<T>(string key, T val);
        }

        class DefaultProperties : IProperties
        {
            Dictionary<string, object> _dictionary = new Dictionary<string, object>();
            string _xmlElementName;

            public DefaultProperties()
            {
                SetProperty<string>("serverip", "192.168.1.180");
                SetProperty<string>("serverport", "8888");
                SetProperty<string>("language", "chs");
                SetProperty<string>("checkUpdateAddress", "http://localhost/");
            }

            #region IProperties 成员

            public object GetProperty(string key)
            {
                return _dictionary[key];
            }

            public void SetProperty(string key, object val)
            {
                _dictionary[key] = val;
            }

            public T GetProperty<T>(string key)
            {
                return (T)_dictionary[key];
            }

            public void SetProperty<T>(string key, T val)
            {
                _dictionary[key] = val;
            }

            #endregion
            #region IXmlConvertable 成员

            public void FromXmlElement(XmlElement element)
            {
                _dictionary.Clear();
                _xmlElementName = element.Name;
                foreach (XmlAttribute att in element.Attributes)
                {
                    _dictionary.Add(att.Name, att.Value);
                }
            }

            public XmlElement ToXmlElement()
            {
                XmlDocument doc = new XmlDocument();
                XmlElement element = doc.CreateElement(_xmlElementName);
                foreach (KeyValuePair<string, object> pair in _dictionary)
                {
                    XmlAttribute att = doc.CreateAttribute(pair.Key);
                    att.Value = Convert.ToString(pair.Value);
                    element.Attributes.Append(att);
                }
                return element;
            }

            #endregion
        }
    }
}