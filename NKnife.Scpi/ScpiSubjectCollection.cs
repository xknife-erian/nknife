using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace ScpiKnife
{
    /// <summary>
    ///     指令主题集合
    /// </summary>
    public class ScpiSubjectCollection : List<ScpiSubject>
    {
        /// <summary>
        ///     指令主题所属的仪器品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        ///     指令主题所属的仪器型号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     指令集合所在的文件
        /// </summary>
        public FileInfo File { get; set; }

        public bool Save()
        {
            var xml = new XmlDocument();
            if (File.Exists)
            {
                xml.Load(File.FullName);
                Debug.Assert(xml.DocumentElement != null, "xml.DocumentElement != null");
                foreach (XmlNode childNode in xml.DocumentElement.ChildNodes)
                {
                    xml.DocumentElement.RemoveChild(childNode);
                }
            }

            foreach (var ss in this)
            {
                Debug.Assert(xml.DocumentElement != null, "xml.DocumentElement != null");
                xml.DocumentElement.AppendChild(ss.BuildXmlElement(xml));
            }
            xml.Save(File.FullName);
            return true;
        }

        public bool TryParse(FileInfo fileInfo)
        {
            if (fileInfo == null)
                throw new ArgumentNullException("fileInfo");

            if (!fileInfo.Exists)
            {
                return false;
            }
            var xmldoc = new XmlDocument();
            xmldoc.Load(fileInfo.FullName);

            var meterinfoElement = xmldoc.SelectSingleNode("//meterinfo") as XmlElement;
            if (meterinfoElement == null)
            {
                return false;
            }
            Brand = meterinfoElement.GetAttribute("brand");
            Name = meterinfoElement.GetAttribute("name");
            File = fileInfo;

            var node = xmldoc.SelectSingleNode("//scpigroups");
            var scpigroups = node as XmlElement;
            if (scpigroups == null)
            {
                return false;
            }

            foreach (var subjectNode in scpigroups.ChildNodes)
            {
                if (!(subjectNode is XmlElement))
                    continue;
                var scpiSubject = new ScpiSubject();
                scpiSubject.OwnerCollection = this;
                var ele = subjectNode as XmlElement;
                scpiSubject.Description = ele.GetAttribute("description");

                var initGroupElement = ele.SelectSingleNode("group[@way='init']") as XmlElement;
                scpiSubject.Preload = new ScpiGroup();
                if (initGroupElement != null)
                {
                    foreach (XmlElement scpiElement in initGroupElement.ChildNodes)
                    {
                        var scpiCommand = ScpiCommand.Parse(scpiElement);
                        if (scpiCommand == null)
                            continue;
                        scpiSubject.Preload.AddLast(scpiCommand);
                    }
                }

                var collectGroupElement = ele.SelectSingleNode("group[@way='collect']") as XmlElement;
                scpiSubject.Collect = new ScpiGroup();
                if (collectGroupElement != null)
                {
                    foreach (XmlElement scpiElement in collectGroupElement.ChildNodes)
                    {
                        var scpiCommand = ScpiCommand.Parse(scpiElement);
                        if (scpiCommand == null)
                            continue;
                        scpiSubject.Collect.AddLast(scpiCommand);
                    }
                }
                Add(scpiSubject);
            }

            return true;
        }
    }
}