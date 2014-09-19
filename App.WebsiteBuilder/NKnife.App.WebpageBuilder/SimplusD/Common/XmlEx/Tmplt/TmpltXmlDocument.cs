using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Drawing;

namespace Jeelu.SimplusD
{
    public partial class TmpltXmlDocument : IndexXmlDocument
    {
        protected TmpltXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(sdsiteElement)
        {
            this.CreateXhtmlElement();
        }

        #region 内部变量

        Dictionary<string, string> _cssDic = new Dictionary<string, string>();
        string _strCss = @"{1}#ee_mainDiv{{margin:0 auto;float:none;{0}}}div{{float:left;}}";
        string _strCssLink;
        //string _strFalshJs = "http://192.168.1.190/wangm/swfobject.js";
        //string _selectNoPicture = "//Box|//List|//NavigationBox";
        private int _emptyId = 0;

        /// <summary>
        /// edit by zhenghao at 2008-06-05 11:30 ： 当前查找到的索引
        /// </summary>
        private int _currentSearchIndex = 0;

        /// <summary>
        /// edit by zhenghao at 2008-06-05 11:30 ： 当前结果的
        /// </summary>
        private int _resultIndex = -1;

        /// <summary>
        /// edit by zhenghao at 2008-06-05 11:30 ： 可以被查找的节点
        /// </summary>
        private List<AnyXmlElement> _canSearchedElements = new List<AnyXmlElement>();

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取或设置模板的CSS
        /// </summary>
        public string TmpltCss { get; set; }

        /// <summary>
        /// 获取或设置是否有自动关键自排序
        /// </summary>
        public bool HasAutoKeyWordsSequenceType
        {
            get { return Utility.Convert.StringToBool(DocumentElement.GetAttribute("hasAutoKeyWords")); }
            set { DocumentElement.SetAttribute("hasAutoKeyWords", value.ToString()); }
        }

        public new TmpltSimpleExXmlElement SdsiteElement
        {
            get
            {
                return base.SdsiteElement as TmpltSimpleExXmlElement;
            }
        }

        /// <summary>
        /// 获取或设置是否拥有正文型页面片
        /// </summary>
        public bool HasContentSnip
        {
            get { return SdsiteElement.HasContentSnip; }
            set
            {
                DocumentElement.SetAttribute("hasContent", value.ToString());
                SdsiteElement.HasContentSnip = value;
            }
        }

        /// <summary>
        /// 获取或设置模板的宽度
        /// </summary>
        public string Width
        {
            get { return DocumentElement.GetAttribute("width"); }
            set { DocumentElement.SetAttribute("width", value); }
        }

        /// <summary>
        /// 获取或设置模板的高度
        /// </summary>
        public string Height
        {
            get { return DocumentElement.GetAttribute("height"); }
            set { DocumentElement.SetAttribute("height", value); }
        }

        /// <summary>
        /// 获取或设置模板类型
        /// </summary>
        public TmpltType TmpltType
        {
            get { return (TmpltType)Enum.Parse(typeof(TmpltType), DocumentElement.GetAttribute("type")); }
            set { DocumentElement.SetAttribute("type", value.ToString()); }
        }

        public bool HasBackImage
        {
            get { return Utility.Convert.StringToBool(DocumentElement.GetAttribute("hasBackImg")); }
            set { DocumentElement.SetAttribute("hasBackImg", value.ToString()); }
        }

        /// <summary>
        /// 获取或设置背景图片//zhenghao:2008-06-25
        /// </summary>
        public Image BackImage 
        {
            get
            {
                if (!HasBackImage)
                    return null;
                AnyXmlElement imgEle = (AnyXmlElement)DocumentElement.SelectSingleNode("backImage");
                if (imgEle == null)
                {
                    return null;
                }
                return Utility.Convert.Base64ToImage(imgEle.CDataValue);
            }
            set 
            {
                AnyXmlElement imgEle = (AnyXmlElement)DocumentElement.SelectSingleNode("backImage");
                if (imgEle == null)
                {
                    imgEle = new AnyXmlElement("backImage", this);
                    DocumentElement.AppendChild(imgEle);
                }
                imgEle.CDataValue = Utility.Convert.ImageToBase64(value);
            }
        }

        public List<BorderLineXmlElement> BorderLineElements
        {
            get { return _borderLineElements; }
            set { _borderLineElements = value; }
        }
        private List<BorderLineXmlElement> _borderLineElements;


        public List<LineXmlElement> LineElements
        {
            get { return _lineElements; }
            set { _lineElements = value; }
        }
        private List<LineXmlElement> _lineElements;

        private bool _reseted = false;
        /// <summary>
        /// 是否被刷新
        /// </summary>
        public bool Reseted 
        {
            get { return _reseted; }
            set 
            { 
                _reseted = value;
                OnReseted(EventArgs.Empty);
            }
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// 在模板Document初始化的时候创建XhtmlElement及他的父XhtmlElement
        /// design by lukan, 2008-6-20 15:11:35
        /// </summary>
        protected virtual void CreateXhtmlElement()
        {
            /// 对于模板来父级就页面本身，故初始时即为实例一个XhtmlPage
            this._ParentXhtmlElement = new XhtmlPage(Xhtml.Version.Xhtml11);
            /// 对于模板来讲他的XhtmlElement在用的时候一般就是Body节点
            this._XhtmlElement = ((XhtmlPage)this._ParentXhtmlElement).Body;
        }

        /// <summary>
        /// 设置所有的线段的元素
        /// </summary>
        private void SetLineElement()
        {
            if (_lineElements == null || _lineElements.Count <= 0)
                return;
            XmlElement linesElement = (XmlElement)this.DocumentElement.SelectSingleNode("lines");
            foreach (LineXmlElement ele in _lineElements)
            {
                linesElement.AppendChild(ele);
            }
        }

        /// <summary>
        /// 设置边界线
        /// </summary>
        private void SetBorderLines()
        {
            if (_borderLineElements == null || _borderLineElements.Count <= 0)
                return;
            XmlElement linesElement = (XmlElement)this.DocumentElement.SelectSingleNode("lines");
            foreach (BorderLineXmlElement ele in _borderLineElements)
            {
                linesElement.AppendChild(ele);
            }
        }

        /// <summary>
        /// 设置所有的矩形元素
        /// </summary>
        private void SetRectElement()
        {
            if (RectElements == null)
            {
                return;
            }
            XmlElement rectsElement = (XmlElement)this.DocumentElement.SelectSingleNode("rects");
            XmlElement ele = RectElements;
            rectsElement.AppendChild(ele);
        }

        /// <summary>
        /// Tmplt的"rect"节点。
        /// modify:将原返回类型XmlElement改成AnyXmlElement。
        /// modify by lukan
        /// </summary>
        public AnyXmlElement RectElements { get; set; }
        
        /// <summary>
        /// edit by zhenghao at 2008-06-04 13:30 ： 获得可以被查找的所有节点
        /// </summary>
        /// <returns>没有节点时为false</returns>
        private bool GetCanSearchNodes()
        {
            _canSearchedElements.Clear();
            XmlNode rectsNode = DocumentElement.SelectSingleNode("rects");
            if (rectsNode == null)
            {
                return false;
            }

            XmlNodeList snipNodes = rectsNode.SelectNodes("//snip");
            if (snipNodes.Count > 0)
            {
                foreach (XmlNode sn in snipNodes)
                {
                    _canSearchedElements.Add(sn as AnyXmlElement);
                }
            }

            XmlNodeList partNodes = rectsNode.SelectNodes("//part");
            if (partNodes.Count > 0)
            {
                foreach (XmlNode pn in partNodes)
                {
                    _canSearchedElements.Add(pn as AnyXmlElement);
                }
            }

            if (_canSearchedElements.Count < 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获得所有频道节点集合
        /// </summary>
        /// <returns></returns>
        private List<AnyXmlElement> GetAllChannelElement()
        {
            List<AnyXmlElement> value = new List<AnyXmlElement>();
            XmlNodeList channelNodes = SelectNodes("//channel");
            Dictionary<string, AnyXmlElement> dic = new Dictionary<string, AnyXmlElement>();
            foreach (XmlNode node in channelNodes)
            {
                AnyXmlElement ele = node as AnyXmlElement;
                if (!string.IsNullOrEmpty(ele.GetAttribute("id")))
                {
                    dic[ele.GetAttribute("id")] = ele;
                }
            }
            foreach (AnyXmlElement aEle in dic.Values)
            {
                value.Add(aEle);
            }

            return value;
        }


        #endregion

        #region 重写公共方法

        public override void Save()
        {
            SaveDoc();
            List<AnyXmlElement> channelEles = GetAllChannelElement();
            XmlUtilService.RemoveAllChilds(SdsiteElement);
            foreach (AnyXmlElement ele in channelEles)
            {
                AnyXmlElement cEle = (AnyXmlElement)CreateElement("channelofsnip");
                cEle.SetAttribute("id", ele.GetAttribute("id"));
                SdsiteElement.AppendChild(cEle);
            }
            base.Save();
        }

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            switch (localName)
            {
                case "rect":// modify by lukan, 2008年6月19日10时55分
                    {
                        SnipRectXmlElement ele = new SnipRectXmlElement(this);
                        return ele;
                    }
                case "snip":
                    {
                        SnipXmlElement ele = new SnipXmlElement(this);
                        return ele;
                    }
                case "parts":// modify by lukan, 2008年6月19日10时55分
                    {
                        SnipPartsXmlElement ele = new SnipPartsXmlElement(this);
                        return ele;
                    }
                case "part":
                    {
                        SnipPartXmlElement ele = new SnipPartXmlElement(this);
                        return ele;
                    }
                case "rects":
                    {
                        SnipRectsXmlElement ele = new SnipRectsXmlElement(this);
                        return ele;
                    }
                case "backImage":
                    {
                        AnyXmlElement ele = new AnyXmlElement("backImage", this);
                        return ele;
                    }
                default:
                    return base.CreateElement(prefix, localName, namespaceURI);
            }
        }

        #endregion

        #region 公共静态方法

        /// <summary>
        /// 创建一个TmpltXmlDocument对象。
        /// </summary>
        internal static TmpltXmlDocument CreateInstance(string docPath, string tmpltId, SimpleExIndexXmlElement sdsiteElement)
        {
            Debug.Assert(!string.IsNullOrEmpty(docPath));
            Debug.Assert(!string.IsNullOrEmpty(tmpltId));

            TmpltXmlDocument doc = new TmpltXmlDocument(docPath, sdsiteElement);
            //doc.HasAutoKeyWordsSequenceType = false;
            return doc;
        }

        #endregion

        #region 公共方法

        public void SaveDoc()
        {
            SetBorderLines();
            SetLineElement();
            SetRectElement();

        }

        /// <summary>
        /// 重置所有的属性，包括Id。一般情况下是从外部导入或复制时调用。
        /// </summary>
        public void ResetAllProperty(bool isRemoveContent, bool isHomeType)
        {
            XmlNodeList snipNodes = DocumentElement.SelectNodes("//snip");
            foreach (XmlNode snipNode in snipNodes)
            {
                XmlElement snipEle = (XmlElement)snipNode;
                snipEle.SetAttribute("id", Utility.Guid.NewGuid());
                if (snipEle.GetAttribute("type").ToLower() == PageSnipType.Content.ToString().ToLower())
                {
                    if (isHomeType)
                    {
                        snipEle.SetAttribute("type", PageSnipType.None.ToString());
                        snipEle.SetAttribute("hasSnip", false.ToString());
                        XmlUtilService.RemoveAllChilds(snipEle);
                        continue;
                    }
                    if (isRemoveContent)
                    {
                        XmlUtilService.RemoveAllChilds((XmlElement)snipEle.SelectSingleNode("parts"));
                        continue;
                    }
                }
                XmlNodeList partNodes = snipEle.SelectNodes("//part");
                foreach (XmlNode partNode in partNodes)
                {
                    XmlElement partEle = (XmlElement)partNode;
                    partEle.SetAttribute("partId", XmlUtilService.CreateIncreaseId());
                }
            }
        }

        public SnipXmlElement CreateSnip(string snipId)
        {
            SnipXmlElement ele = new SnipXmlElement(this);
            ele.Id = snipId;
            ele.SnipName = "";
            ele.Title = "";
            ele.Css = "";
            ele.HasSnip = false; ;
            ele.SnipType = PageSnipType.General;
            ele.X = 0;
            ele.Y = 0;
            ele.Width = "500px";
            ele.Height = "0px";
            ele.IsLocked = false;
            ele.IsSelected = false;
            XmlElement _partsEle = CreateElement("parts");
            ele.AppendChild(_partsEle);
            return ele;
        }

        public SnipXmlElement GetContentSnipEle()
        {
            return (SnipXmlElement)this.SelectSingleNode("//snip[@type='Content']");
        }

        public XmlNodeList GetSnipElementList()
        {
            XmlNodeList snipXmlEleList = this.GetRectsElement().SelectNodes("//snip");//SelectNodes("//snip[@hasSnip='True']");
            return snipXmlEleList;
        }

        public SnipXmlElement GetKeyListSnip()
        {
            SnipXmlElement snipKeyElement = this.SelectSingleNode("//part[@type='ListBox' and @sequenceType='AutoKeyWord']").ParentNode.ParentNode.ParentNode as SnipXmlElement;
            return snipKeyElement;
        }

        /// <summary>
        /// 获取“Rects”节点，该节点包含所有矩形和页面片树
        /// Design by Lukan,2008年6月19日9时47分
        /// </summary>
        public AnyXmlElement GetRectsElement()
        {
            return this.DocumentElement.SelectSingleNode("rects") as AnyXmlElement;
        }
        /// <summary>
        /// edit by zhenghao at 2008-06-24 9:30
        /// 获得所有的rect节点拷贝
        /// </summary>
        public Dictionary<string, XmlElement> GetAllRectElementClone()
        {
            Dictionary<string, XmlElement> _cloneDic = new Dictionary<string, XmlElement>();
            XmlNodeList rectNodes = this.DocumentElement.SelectNodes("//rect");
            foreach (XmlNode node in rectNodes)
            {
                XmlElement ele = (XmlElement)node;
                XmlElement cloneEle = ele.OwnerDocument.CreateElement(ele.Name);
                XmlUtilService.CopyXmlElement(ele, cloneEle);

                string id = ele.GetAttribute("id");
                Debug.Assert(!string.IsNullOrEmpty(id));

                _cloneDic.Add(id, cloneEle);
            }

            return _cloneDic;
        }
        /// <summary>
        /// 获得所有的snip节点拷贝
        /// </summary>
        public Dictionary<string, XmlElement> GetAllSnipElementClone()
        {
            Dictionary<string, XmlElement> _cloneDic = new Dictionary<string, XmlElement>();
            XmlNodeList snipNodes = this.DocumentElement.SelectNodes(@"//snip[@hasSnip='True']");
            foreach (XmlNode node in snipNodes)
            {
                XmlElement ele = (XmlElement)node;
                XmlElement cloneEle = ele.OwnerDocument.CreateElement(ele.Name);
                XmlUtilService.CopyXmlElement(ele, cloneEle);

                string id = ele.GetAttribute("id");
                Debug.Assert(!string.IsNullOrEmpty(id));

                _cloneDic.Add(id, cloneEle);
            }

            return _cloneDic;
        }

        /// <summary>
        /// 通过页面片ID获取页面片节点
        /// </summary>
        /// <param name="snipID"></param>
        /// <returns></returns>
        public SnipXmlElement GetSnipElementById(string snipID)
        {
            return GetElementById(snipID) as SnipXmlElement;
        }

        public void SetAllSnipsIsModified(bool isModified)
        {
            XmlNodeList snips = this.GetSnipElementList();
            foreach (SnipXmlElement snip in snips)
            {
                snip.IsModified = isModified;
            }
            this.Save();
        }


        ///add by zhangling on 2008年7月4日
        /// <summary>
        /// 得到一组part,此节点下会有channelId的节点
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public XmlNode[] GetPartElementArray(string channelId)
        {
            List<XmlNode> list = new List<XmlNode>();
            list.Clear();
            string xpath = string.Format("//channel[@id='{0}']", channelId);
            XmlNodeList nodeList = this.DocumentElement.SelectNodes(xpath);
            foreach (XmlNode node in nodeList)
            {
                XmlNode partNode = node.ParentNode.ParentNode;
                list.Add(partNode);
            }
            return list.ToArray ();
        }
        /// <summary>
        /// 返回包含此part节点的snip节点的ID
        /// </summary>
        /// <param name="partNode">part节点</param>
        /// <returns></returns>
        public string GetSnipId(XmlNode partNode)
        {
            XmlNode snipNode = partNode.ParentNode.ParentNode;
            string snipId = ((XmlElement)snipNode).GetAttribute("id");
            return snipId;
        }


        #endregion

        #region 自定义事件

        /// <summary>
        /// 被刷新时
        /// </summary>
        public event EventHandler Reset;
        protected internal void OnReseted(EventArgs e)
        {
            if (Reset != null)
            {
                Reset(this, e);
            }
        }

        #endregion
    }
}
