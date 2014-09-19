using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;

namespace Jeelu.Billboard
{
    public class WordSqlXml : CollectionBase
    {
        public WordSqlXml()
        {
            this._InnerDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><SqlXml />");
            this._Id = 0;
            this._InnerDocument.DocumentElement.SetAttribute("id", this._Id.ToString());
        }
        public WordSqlXml(string file)
        {
            this.Load(file);
        }

        public void LoadXml(string xmlcode)
        {
            Debug.Assert(!string.IsNullOrEmpty(xmlcode), "LoadXml(string xmlcode), 'xmlcode' is empty!");
            this._InnerDocument = new XmlDocument();
            if (string.IsNullOrEmpty(xmlcode))
            {
                this._InnerDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><SqlXml id=\"0\" />");
            }
            else
            {
                this._InnerDocument.LoadXml(xmlcode);
            }
            this._IsLoadInit = true;
            foreach (XmlNode node in this.ElementList)
            {
                XmlElement ele = (XmlElement)node;
                WordSqlXmlAction action = new WordSqlXmlAction(ele);
                this.List.Add(action);
            }
            this._IsLoadInit = false;
            this._Id = ulong.Parse(this._InnerDocument.DocumentElement.GetAttribute("id"));
        }

        public void Load(string file)
        {
            this._File = file;
            this._InnerDocument= new XmlDocument();
            this._InnerDocument.Load(file);
            this._IsLoadInit = true;
            foreach (XmlNode node in this.ElementList)
            {
                XmlElement ele = (XmlElement)node;
                WordSqlXmlAction action = new WordSqlXmlAction(ele);
                this.List.Add(action);
            }
            this._IsLoadInit = false;
            this._Id = ulong.Parse(this._InnerDocument.DocumentElement.GetAttribute("id"));
        }

        private XmlDocument _InnerDocument = new XmlDocument();
        private string _File;
        private bool _IsLoadInit;
        private ulong _Id;
        private ulong GetId()
        {
            _Id++;
            this.DocumentElement.SetAttribute("id", this._Id.ToString());
            return _Id;
        }

        public void Save()
        {
            Debug.Assert(File.Exists(this._File), "this._File isn't setting!!!");
            this._InnerDocument.Save(_File);
        }
        public void Save(string file)
        {
            this._File = file;
            this._InnerDocument.Save(_File);
        }

        public WordSqlXmlAction CreatAction(DictAction dictAction, string word, ulong frequency, string dictName)
        {
            WordSqlXmlAction action = new WordSqlXmlAction(this.GetId(), dictAction, word, frequency, dictName);
            return action;
        }
        protected XmlElement DocumentElement { get { return this._InnerDocument.DocumentElement; } }
        protected XmlNodeList ElementList { get { return this._InnerDocument.DocumentElement.ChildNodes; } }

        public WordSqlXmlAction this[int index]
        {
            get { return ((WordSqlXmlAction)List[index]); }
            set { List[index] = value; }
        }

        public int Add(WordSqlXmlAction value)
        {
            return (List.Add(value));
        }

        public int IndexOf(WordSqlXmlAction value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, WordSqlXmlAction value)
        {
            List.Insert(index, value);
        }

        public void Remove(WordSqlXmlAction value)
        {
            List.Remove(value);
        }

        public void Remove(int i)
        {
            this.List.RemoveAt(i);
        }

        public bool Contains(WordSqlXmlAction value)
        {
            return (List.Contains(value));
        }

        protected override void OnClear()
        {
            base.OnClear();
            if (!_IsLoadInit)
            {
                while (this.DocumentElement.HasChildNodes)
                {
                    this.DocumentElement.RemoveChild(this.DocumentElement.FirstChild);
                }
            }
        }

        protected override void OnInsert(int index, Object value)
        {
            base.OnInsert(index, value);
            if (!_IsLoadInit)
            {
                WordSqlXmlAction sql = (WordSqlXmlAction)value;
                XmlNode node = this.ElementList[index];
                this.DocumentElement.InsertBefore(sql.ToXmlElement(this._InnerDocument), node);
            }
        }

        protected override void OnRemove(int index, Object value)
        {
            base.OnRemove(index, value);
            if (!_IsLoadInit)
            {
                WordSqlXmlAction sql = (WordSqlXmlAction)value;
                XmlElement ele = (XmlElement)this.DocumentElement.SelectSingleNode(string.Format(".//sql[@id='{0}']", sql.Id));
                this.DocumentElement.RemoveChild(ele);
            }
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            base.OnSet(index, oldValue, newValue);
            if (!_IsLoadInit)
            {
                WordSqlXmlAction oldSql = (WordSqlXmlAction)oldValue;
                WordSqlXmlAction newSql = (WordSqlXmlAction)newValue;
                XmlElement oldEle = (XmlElement)this.DocumentElement.SelectSingleNode(string.Format(".//sql[@id='{0}']", oldSql.Id));
                this.DocumentElement.ReplaceChild(newSql.ToXmlElement(this._InnerDocument), oldEle);
            }
        }

        protected override void OnValidate(Object value)
        {
            base.OnValidate(value);
            //Debug.Assert(value.GetType() == typeof(WordSqlXmlAction), value.GetType() + " is error type!");
            //WordSqlXmlAction sql = (WordSqlXmlAction)value;
            //Debug.Assert(!string.IsNullOrEmpty(sql.Word));
        }

        public override string ToString()
        {
            return this._InnerDocument.OuterXml;
        }

    }

    /// <summary>
    /// 针对词典操作的动作类型枚举
    /// </summary>
    public enum DictAction
    { 
        /// <summary>
        /// 增加一个新词
        /// </summary>
        Add,
        /// <summary>
        /// 删除一个新词
        /// </summary>
        Delete,
        /// <summary>
        /// 修改一个已有词
        /// </summary>
        Updata,
        /// <summary>
        /// 修改一个已有词的词性
        /// </summary>
        UpdataPos,
        /// <summary>
        /// 修改一个已有词的词频
        /// </summary>
        UpdataFrequency,
        /// <summary>
        /// 嘛也不干
        /// </summary>
        None 
    }
}
