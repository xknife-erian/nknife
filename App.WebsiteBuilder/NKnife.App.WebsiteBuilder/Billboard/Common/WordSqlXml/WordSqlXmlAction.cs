using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.Billboard
{
    public class WordSqlXmlAction
    {
        internal WordSqlXmlAction(ulong id)
        {
            this.Id = id;
        }
        internal WordSqlXmlAction(ulong id, DictAction dictAction, string word, ulong frequency, string dictName)
        {
            this.Id = id;
            this.ActionName = dictAction;
            this.Word = word;
            this.Frequency = frequency;
            this.DictName = dictName;
        }
        public WordSqlXmlAction(XmlElement ele)
        {
            this.Id = ulong.Parse(ele.GetAttribute("id"));
            this.ActionName = (DictAction)Enum.Parse(typeof(DictAction), ele.GetAttribute("action"));
            this.Frequency = ulong.Parse(ele.GetAttribute("frequency"));
            this.DictName = ele.GetAttribute("dict");

            if (!ele.HasChildNodes)
            {
                this.Word = "";
            }
            else
            {
                foreach (XmlNode node in ele.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.CDATA)
                    {
                        this.Word = node.Value;
                        return;
                    }
                }
                this.Word = "";
            }
        }

        public ulong Id { get; set; }
        public DictAction ActionName { get; set; }
        public string Word { get; set; }
        public ulong Frequency { get; set; }
        public int Pos { get { return 0; } }
        public string DictName { get; set; }

        public void Run()
        {
            switch (this.ActionName)
            {
                case DictAction.Add:
                    JWordSegmentorService.JWordDictManager.InsertWord(this.Word, (double)(this.Frequency), this.Pos);
                    break;
                case DictAction.Delete:
                    JWordSegmentorService.JWordDictManager.DeleteWord(this.Word);
                    break;
                case DictAction.Updata:
                case DictAction.UpdataPos:
                case DictAction.UpdataFrequency:
                    JWordSegmentorService.JWordDictManager.UpdateWord(this.Word, (double)(this.Frequency), this.Pos);
                    break;
                case DictAction.None:
                default:
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Id).Append('|').
                Append(this.ActionName.ToString()).
                Append('|').
                Append(this.Word).
                Append('|').
                Append(this.Frequency).
                Append('|').
                Append(this.DictName).
                Append('|').Append(this.Pos);
            return sb.ToString();
        }
        public XmlElement ToXmlElement(XmlDocument doc)
        {
            XmlElement ele = doc.CreateElement("sql");
            ele.SetAttribute("id", this.Id.ToString());
            ele.SetAttribute("action", this.ActionName.ToString());
            ele.SetAttribute("frequency", this.Frequency.ToString());
            ele.SetAttribute("pos", this.Pos.ToString());
            ele.SetAttribute("dict", this.Pos.ToString());
            XmlCDataSection cdata = doc.CreateCDataSection(this.Word);
            ele.AppendChild(cdata);
            return ele;
        }
    }
}
