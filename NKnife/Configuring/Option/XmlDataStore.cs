using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Gean.Configuring.CoderSetting;
using Gean.Configuring.Common;
using Gean.Configuring.Interfaces;
using Gean.Configuring.OptionCase;
using NLog;

namespace Gean.Configuring.Option
{
    public class XmlDataStore : IOptionDataStore
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        #region IOptionDataStore实现

        private static readonly OptionServiceCoderSetting _Setting = OptionServiceCoderSetting.ME;
        private DataSet _DataSet;
        private ConcurrentDictionary<string, OptionDataTable> _DataTables;
        private XmlDocument _Document;

        protected FileInfo FileInfo
        {
            get { return StoreObject as FileInfo; }
        }

        protected XmlDocument Document
        {
            get
            {
                if (_Document == null)
                {
                    lock (FileInfo)
                    {
                        _Document = new XmlDocument();
                        if (!FileInfo.Exists)
                        {
                            _Document = XmlHelper.CreatNewDoucmnet(FileInfo.FullName, "Options");
                            XmlElement ele = _Document.CreateElement("CaseList");
                            XmlElement subEle = _Document.CreateElement("Item");
                            subEle.SetAttribute("Name", "Default");
                            ele.AppendChild(subEle);
                            if (_Document.DocumentElement != null)
                                _Document.DocumentElement.AppendChild(ele);
                            BuildNewDocument(_Document, FileInfo.FullName);
                        }
                        //激活即将开始载入选项事件
                        OnOptionLoading(new OptionLoadEventArgs(FileInfo));

                        _Document = new XmlDocument();
                        _Document.Load(FileInfo.FullName);

                        //激活选项载入后事件
                        OnOptionLoaded(new OptionLoadEventArgs(FileInfo));
                    }
                }
                return _Document;
            }
        }

        /// <summary>更新解决方案的保存(当已存在时就更新，否则添加)
        /// </summary>
        public void AddOrUpdateCaseStore(OptionCase.OptionCaseItem optionCase)
        {
            var serializer = new XmlSerializer(typeof(OptionCase.OptionCaseItem));
            if (Document.DocumentElement != null)
            {
                var srcElement = Document.DocumentElement.SelectSingleNode("CaseList");
                if (srcElement != null && srcElement.OwnerDocument != null)
                {
                    XmlElement ele = srcElement.OwnerDocument.CreateElement("Item");
                    ele.SetAttribute("Name", optionCase.Name);
                    var sb = new StringBuilder();
                    XmlWriter xw = XmlWriter.Create(sb);
                    serializer.Serialize(xw, optionCase);
                    ele.SetCDataElement(sb.ToString());
                    srcElement.AppendChild(ele);
                    Save();
                }
            }
        }

        /// <summary>删除一个解决方案的保存
        /// </summary>
        public void RemoveCaseStore(OptionCase.OptionCaseItem optionCase)
        {
            if (Document.DocumentElement != null)
            {
                var srcElement = Document.DocumentElement.SelectSingleNode("CaseList");
                if (srcElement != null)
                {
                    XmlNode node = srcElement.SelectSingleNode(string.Format("./Item[@Name='{0}']", optionCase.Name));
                    if (node != null)
                    {
                        srcElement.RemoveChild(node);
                        Save();
                    }
                }
            }
        }

        /// <summary>应用程序选项信息存储空间
        /// </summary>
        /// <value>The store.</value>
        public object StoreObject { get; private set; }

        /// <summary>
        /// 多个子选项信息的集合
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<string, OptionDataTable> DataTables
        {
            get
            {
                if (_DataSet == null)
                {
                    _DataSet = new DataSet {DataSetName = _Setting.OptionFileName};
                    if (Document.DocumentElement != null)
                    {
                        foreach (XmlNode childNode in Document.DocumentElement.ChildNodes)
                        {
                            if (childNode.NodeType != XmlNodeType.Element)
                                continue;
                            if (!childNode.LocalName.Equals(_Setting.OptionDataTableFlagName))
                                continue;
                            _DataSet.Tables.Add(OptionDataTable.ParseTableNode(this, childNode));
                        }
                    }
                    if (_DataTables == null)
                        _DataTables = new ConcurrentDictionary<string, OptionDataTable>();
                    foreach (OptionDataTable table in _DataSet.Tables)
                    {
                        _DataTables.TryAdd(table.TableName, table);
                    }
                }
                return _DataTables;
            }
        }

        /// <summary>
        /// 保存一个节点的选项信息
        /// </summary>
        /// <returns></returns>
        public bool Update(OptionDataTable table)
        {
            table.AcceptChanges();
            try
            {
                lock (Document)
                {
                    string xpath = string.Format("//{1}[@name='{0}']", table.TableName, _Setting.OptionDataTableFlagName);
                    var ele = (XmlElement) Document.SelectSingleNode(xpath);
                    if (ele != null)
                    {
                        XmlCDataSection xcds = Document.CreateCDataSection(table.AsXml.ToString());
                        ele.RemoveAllElements();
                        ele.AppendChild(xcds);
                        return true;
                    }
                    else
                    {
                        _Logger.Warn(string.Format("找不到{0}表的节点。", table.TableName));
                        BuildNewNode(Document, table);
                        _Logger.Info(string.Format("创建Option的{0}表的新节点。", table.TableName));
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("更新选项信息节点时异常。{0}，{1}", table.TableName, e.Message), e);
                return false;
            }
        }

        /// <summary>
        /// 保存整个存储器
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                lock (FileInfo)
                {
                    Document.Save(FileInfo.FullName);
                    foreach (DataTable dataTable in _DataSet.Tables)
                    {
                        ((OptionDataTable) dataTable).IsModified = false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("保存选项XML时异常.{0}", e.Message), e);
                return false;
            }
        }

        public bool ReLoad()
        {
            return true;
        }

        public bool Clean()
        {
            return true;
        }

        public object Backup()
        {
            return true;
        }

        /// <summary>
        /// 选项信息即将载入前发生的事件
        /// </summary>
        public event OptionLoadingEventHandler OptionLoadingEvent;

        /// <summary>
        /// 选项信息载入后发生的事件
        /// </summary>
        public event OptionLoadedEventHandler OptionLoadedEvent;

        /// <summary>是否已经初始化完成
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialize success; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitializeSuccess
        {
            get { return true; }
        }

        /// <summary>初始化选项的存储目标
        /// </summary>
        /// <param name="target">The target.</param>
        public void InitializeStoreTarget(object target)
        {
            var basepath = (string)target;
            var filePath = Path.Combine(basepath, OptionServiceCoderSetting.ME.OptionFileName);
            StoreObject = new FileInfo((filePath));
        }

        /// <summary>
        /// 获得一份选项实例的管理器。
        /// 往往应用程序的选项可以是多份，每一份在匹配的场景或时段下被使用，在这里我们理解一份选项是一个广义的实例。
        /// </summary>
        public IOptionCaseManager CaseManager
        {
            get { return _Setting.OptionCaseManager; }
        }

        protected virtual void OnOptionLoading(OptionLoadEventArgs e)
        {
            if (OptionLoadingEvent != null)
                OptionLoadingEvent(this, e);
        }

        protected virtual void OnOptionLoaded(OptionLoadEventArgs e)
        {
            if (OptionLoadedEvent != null)
                OptionLoadedEvent(this, e);
        }

        #endregion

        #region 一些内部方法

        private static IEnumerable<IOptionDataTableSchema> GetSchemas()
        {
            IEnumerable<Type> types = null;
            try
            {
                string path = CoderSettingService.ApplicationStartPath;
                types = UtilityType.FindTypesByDirectory(path, typeof (IOptionDataTableSchema));
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("从目录中搜索选项类时异常。{0}", e.Message), e);
            }
            _Logger.Info(string.Format("搜索所有的程序集，并找到 {0} 个IOption类型(包括基本类型和抽象类型)。", types.Count()));
            var schemas = new List<IOptionDataTableSchema>();
            foreach (Type type in types)
            {
                object obj = Activator.CreateInstance(type);
                schemas.Add((IOptionDataTableSchema) obj);
            }
            return schemas;
        }

        private static void BuildNewDocument(XmlDocument document, string filepath)
        {
            IEnumerable<IOptionDataTableSchema> schemas = GetSchemas();
            foreach (IOptionDataTableSchema schema in schemas)
            {
                XmlElement element = document.CreateElement(_Setting.OptionDataTableFlagName);
                element.SetAttribute("name", schema.TableName);
                XmlCDataSection xcds = document.CreateCDataSection(schema.OptionDataTableSchema.AsXml.ToString());
                element.AppendChild(xcds);
                if (document.DocumentElement != null)
                    document.DocumentElement.AppendChild(element);
            }
            document.Save(filepath);
        }

        private static void BuildNewNode(XmlDocument document, OptionDataTable table)
        {
            XmlElement element = document.CreateElement(_Setting.OptionDataTableFlagName);
            element.SetAttribute("name", table.TableName);
            XmlCDataSection xcds = document.CreateCDataSection(table.AsXml.ToString());
            element.AppendChild(xcds);
            if (document.DocumentElement != null)
                document.DocumentElement.AppendChild(element);
        }

        #endregion
    }
}