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
        protected ScpisXmlFile _ScpiFile;

        public Version Version { get; set; }

        /// <summary>
        ///     指令主题所属的仪器品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        ///     指令主题所属的仪器型号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     指令集合所在的Xml文件
        /// </summary>
        public void BuildScpiFile(string fileFullName)
        {
            _ScpiFile = new ScpisXmlFile(fileFullName);
        }

        public bool Save()
        {
            Debug.Assert(_ScpiFile.DocumentElement != null, "xml.DocumentElement != null");
            foreach (XmlNode childNode in _ScpiFile.DocumentElement.ChildNodes)
            {
                _ScpiFile.DocumentElement.RemoveChild(childNode);
            }

            foreach (var ss in this)
            {
                Debug.Assert(_ScpiFile.DocumentElement != null, "xml.DocumentElement != null");
                _ScpiFile.DocumentElement.AppendChild(ss.BuildXmlElement(_ScpiFile));
            }
            _ScpiFile.Save();
            return true;
        }

        public bool TryParse()
        {
            if (_ScpiFile == null)
            {
                throw new FileNotFoundException();
            }

            Version = Version.Parse(_ScpiFile.DocumentElement.GetAttribute("version"));

            var meterinfoElement = _ScpiFile.DocumentElement.SelectSingleNode("//information") as XmlElement;
            if (meterinfoElement == null)
            {
                return false;
            }
            Brand = meterinfoElement.GetAttribute("brand");
            Name = meterinfoElement.GetAttribute("name");

            var node = _ScpiFile.DocumentElement.SelectSingleNode("//scpigroups");
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