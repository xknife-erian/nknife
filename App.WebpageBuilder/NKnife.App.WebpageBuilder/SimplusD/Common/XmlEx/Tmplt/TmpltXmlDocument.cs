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

        #region �ڲ�����

        Dictionary<string, string> _cssDic = new Dictionary<string, string>();
        string _strCss = @"{1}#ee_mainDiv{{margin:0 auto;float:none;{0}}}div{{float:left;}}";
        string _strCssLink;
        //string _strFalshJs = "http://192.168.1.190/wangm/swfobject.js";
        //string _selectNoPicture = "//Box|//List|//NavigationBox";
        private int _emptyId = 0;

        /// <summary>
        /// edit by zhenghao at 2008-06-05 11:30 �� ��ǰ���ҵ�������
        /// </summary>
        private int _currentSearchIndex = 0;

        /// <summary>
        /// edit by zhenghao at 2008-06-05 11:30 �� ��ǰ�����
        /// </summary>
        private int _resultIndex = -1;

        /// <summary>
        /// edit by zhenghao at 2008-06-05 11:30 �� ���Ա����ҵĽڵ�
        /// </summary>
        private List<AnyXmlElement> _canSearchedElements = new List<AnyXmlElement>();

        #endregion

        #region ��������

        /// <summary>
        /// ��ȡ������ģ���CSS
        /// </summary>
        public string TmpltCss { get; set; }

        /// <summary>
        /// ��ȡ�������Ƿ����Զ��ؼ�������
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
        /// ��ȡ�������Ƿ�ӵ��������ҳ��Ƭ
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
        /// ��ȡ������ģ��Ŀ��
        /// </summary>
        public string Width
        {
            get { return DocumentElement.GetAttribute("width"); }
            set { DocumentElement.SetAttribute("width", value); }
        }

        /// <summary>
        /// ��ȡ������ģ��ĸ߶�
        /// </summary>
        public string Height
        {
            get { return DocumentElement.GetAttribute("height"); }
            set { DocumentElement.SetAttribute("height", value); }
        }

        /// <summary>
        /// ��ȡ������ģ������
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
        /// ��ȡ�����ñ���ͼƬ//zhenghao:2008-06-25
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
        /// �Ƿ�ˢ��
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

        #region �ڲ�����

        /// <summary>
        /// ��ģ��Document��ʼ����ʱ�򴴽�XhtmlElement�����ĸ�XhtmlElement
        /// design by lukan, 2008-6-20 15:11:35
        /// </summary>
        protected virtual void CreateXhtmlElement()
        {
            /// ����ģ����������ҳ�汾���ʳ�ʼʱ��Ϊʵ��һ��XhtmlPage
            this._ParentXhtmlElement = new XhtmlPage(Xhtml.Version.Xhtml11);
            /// ����ģ����������XhtmlElement���õ�ʱ��һ�����Body�ڵ�
            this._XhtmlElement = ((XhtmlPage)this._ParentXhtmlElement).Body;
        }

        /// <summary>
        /// �������е��߶ε�Ԫ��
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
        /// ���ñ߽���
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
        /// �������еľ���Ԫ��
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
        /// Tmplt��"rect"�ڵ㡣
        /// modify:��ԭ��������XmlElement�ĳ�AnyXmlElement��
        /// modify by lukan
        /// </summary>
        public AnyXmlElement RectElements { get; set; }
        
        /// <summary>
        /// edit by zhenghao at 2008-06-04 13:30 �� ��ÿ��Ա����ҵ����нڵ�
        /// </summary>
        /// <returns>û�нڵ�ʱΪfalse</returns>
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
        /// �������Ƶ���ڵ㼯��
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

        #region ��д��������

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
                case "rect":// modify by lukan, 2008��6��19��10ʱ55��
                    {
                        SnipRectXmlElement ele = new SnipRectXmlElement(this);
                        return ele;
                    }
                case "snip":
                    {
                        SnipXmlElement ele = new SnipXmlElement(this);
                        return ele;
                    }
                case "parts":// modify by lukan, 2008��6��19��10ʱ55��
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

        #region ������̬����

        /// <summary>
        /// ����һ��TmpltXmlDocument����
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

        #region ��������

        public void SaveDoc()
        {
            SetBorderLines();
            SetLineElement();
            SetRectElement();

        }

        /// <summary>
        /// �������е����ԣ�����Id��һ��������Ǵ��ⲿ�������ʱ���á�
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
        /// ��ȡ��Rects���ڵ㣬�ýڵ�������о��κ�ҳ��Ƭ��
        /// Design by Lukan,2008��6��19��9ʱ47��
        /// </summary>
        public AnyXmlElement GetRectsElement()
        {
            return this.DocumentElement.SelectSingleNode("rects") as AnyXmlElement;
        }
        /// <summary>
        /// edit by zhenghao at 2008-06-24 9:30
        /// ������е�rect�ڵ㿽��
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
        /// ������е�snip�ڵ㿽��
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
        /// ͨ��ҳ��ƬID��ȡҳ��Ƭ�ڵ�
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


        ///add by zhangling on 2008��7��4��
        /// <summary>
        /// �õ�һ��part,�˽ڵ��»���channelId�Ľڵ�
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
        /// ���ذ�����part�ڵ��snip�ڵ��ID
        /// </summary>
        /// <param name="partNode">part�ڵ�</param>
        /// <returns></returns>
        public string GetSnipId(XmlNode partNode)
        {
            XmlNode snipNode = partNode.ParentNode.ParentNode;
            string snipId = ((XmlElement)snipNode).GetAttribute("id");
            return snipId;
        }


        #endregion

        #region �Զ����¼�

        /// <summary>
        /// ��ˢ��ʱ
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
