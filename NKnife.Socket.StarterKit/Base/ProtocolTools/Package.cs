using System;
using System.Collections.Generic;
using System.Xml;
using SocketKnife.Interfaces;

namespace NKnife.Socket.StarterKit.Base.ProtocolTools
{
    public class Package : IProtocolPackage
    {
        protected IProtocolContent Content { get; set; }

        #region IProtocolPackage Members

        public short Version
        {
            get { return 1; }
        }

        public string Combine(IProtocolContent content)
        {
            Content = content;
            var doc = new XmlDocument();
            doc.LoadXml(string.Format("<ROOT><REQUEST>{0}</REQUEST></ROOT>", content.Command));
            doc.DocumentElement.SetAttribute("Version", Version.ToString());

            SetCommandParms(doc.DocumentElement);

            XmlElement datas = doc.CreateElement("DATAS");
            SetDatasItem(doc, datas);
            doc.DocumentElement.AppendChild(datas);

            XmlElement session = doc.CreateElement("SESSION");
            session.InnerText = Guid.NewGuid().ToString();
            doc.DocumentElement.AppendChild(session);

            XmlElement date = doc.CreateElement("TIMESTAMP");
            date.InnerText = DateTime.Now.ToString("yyyyMMddHHmmss");
            doc.DocumentElement.AppendChild(date);

            return doc.OuterXml;
        }

        #endregion

        protected virtual void SetCommandParms(XmlElement element)
        {
            if (!string.IsNullOrWhiteSpace(Content.CommandParam))
            {
                var commandEle = (XmlElement) element.SelectSingleNode("//REQUEST");
                if (commandEle != null) 
                    commandEle.SetAttribute("PARM", Content.CommandParam);
            }
        }

        protected virtual void SetDatasItem(XmlDocument doc, XmlElement datas)
        {
            if (Content.Tags == null || Content.Tags.Count <= 0)
            {
                var ele = doc.CreateElement("DATA");
                foreach (string key in Content.Datas.AllKeys)
                    ele.SetAttribute(key, Content.Datas[key]);
                datas.AppendChild(ele);
            }
            else
            {
                //当批量补传流水时，一天的流水做一条协议
                foreach (object tag in Content.Tags)
                {
                    var data = (Dictionary<string, string>) tag;
                    var ele = doc.CreateElement("DATA");
                    foreach (KeyValuePair<string, string> pair in data)
                    {
                        switch (pair.Key)
                        {
                            case "PRINT_TIME":
                            case "CALL_TIME":
                            case "ASKEVAL_TIME":
                            case "END_TIME":
                                if (string.IsNullOrWhiteSpace(pair.Value))
                                    ele.SetAttribute(pair.Key, "");
                                else
                                    ele.SetAttribute(pair.Key, DateTime.Parse(pair.Value).ToString("yyyy-MM-dd HH:mm:ss"));
                                break;
                            default:
                                ele.SetAttribute(pair.Key, pair.Value);
                                break;
                        }
                    }
                    datas.AppendChild(ele);
                }
            }
        }
    }
}