using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Jeelu
{
    public class XmlProjectCreator
    {
        public XmlProjectCreator(string initDir, string file)
        {
            Debug.Assert(!string.IsNullOrEmpty(initDir), "");
            this.InitDir = PraseInitDir(initDir);
            this.ProjectXml = new XmlDocument();
            Debug.Assert(File.Exists(file), "");
            this.ProjectXml.Load(file);
        }

        public XmlProjectCreator(string initDir, byte[] bytes)
        {
            Debug.Assert(!string.IsNullOrEmpty(initDir), "");
            this.InitDir = PraseInitDir(initDir);
            this.ProjectXml = new XmlDocument();
            this.ProjectXml.LoadXml(BytesToString(bytes, Encoding.UTF8));
        }
        private string PraseInitDir(string initDir)
        {
            string str = initDir.Substring(initDir.Length - 1, 1);
            if (str != @"\")
            {
                return initDir + @"\";
            }
            return initDir;
        }

        public string InitDir { get; set; }
        public XmlDocument ProjectXml { get; private set; }

        public void Create()
        {
            this.SubCreate(this.InitDir, this.ProjectXml.DocumentElement);
        }
        private void SubCreate(string initDir, XmlNode mynode)
        {
            foreach (XmlNode node in mynode.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;
                if (ele.LocalName.ToLower() == "dir")
                {
                    string subdir = Path.Combine(initDir, ele.GetAttribute("name"));
                    Directory.CreateDirectory(subdir);
                    if (ele.HasChildNodes)
                    {
                        this.SubCreate(this.PraseInitDir(subdir), ele);
                    }
                }
                if (ele.LocalName.ToLower() == "file")
                {
                    string file = Path.Combine(this.PraseInitDir(initDir), ele.GetAttribute("name"));
                    if (ele.HasChildNodes)
                    {
                        foreach (XmlNode subnode in ele.ChildNodes)
                        {
                            if (subnode.NodeType != XmlNodeType.CDATA)
                            {
                                continue;
                            }
                            XmlCDataSection cdata = (XmlCDataSection)subnode;
                            string str = cdata.Value;
                            byte[] buffer = Convert.FromBase64String(cdata.Value);
                            File.WriteAllBytes(file, buffer);
                        }
                    }
                    else
                    {
                        File.Create(file);
                    }
                    string[] attributes = ele.GetAttribute("attribute").Split('|');
                    foreach (string str in attributes)
                    {
                        switch (str.ToLower())
                        {
                            case "hidden":
                                File.SetAttributes(file, FileAttributes.Hidden);
                                break;
                            case "readonly":
                                File.SetAttributes(file, FileAttributes.ReadOnly);
                                break;
                            case "system":
                                File.SetAttributes(file, FileAttributes.System);
                                break;
                            case "temporary":
                                File.SetAttributes(file, FileAttributes.Temporary);
                                break;
                            case "normal":
                                File.SetAttributes(file, FileAttributes.Normal);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }





        public static string BytesToString(byte[] data, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                if (data[0] == 239 && data[1] == 187 && data[2] == 191)
                {
                    return encoding.GetString(data, 3, data.Length - 3);
                }
            }
            return encoding.GetString(data);
        }
    }
}
