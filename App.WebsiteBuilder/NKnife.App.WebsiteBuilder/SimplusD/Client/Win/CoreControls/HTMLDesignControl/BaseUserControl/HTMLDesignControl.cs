using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using mshtml;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public class HTMLDesignControl : UserControl, IHTMLEventCallBack, IElementBehaviorFactory
    {
        private IHTMLElement currentEle = null;
        private HTMLEditHelper htmlEdit = new HTMLEditHelper();
        public string PageId = string.Empty;

        /// <summary>
        /// 资源文件及path
        /// </summary>
        public Dictionary<string, string> ResourcesIdPaths { get; private set; }

        #region 接口声明：IHTMLDocument
        IHTMLDocument idoc
        {
            get { return (IHTMLDocument)designWebBrowser.Document.DomDocument; }
        }
        IHTMLDocument2 idoc2
        {
            get
            {
                if (designWebBrowser.IsDisposed)
                    return null;
                else
                    return (IHTMLDocument2)designWebBrowser.Document.DomDocument;
            }
        }
        IHTMLDocument3 idoc3
        {
            get { return (IHTMLDocument3)designWebBrowser.Document.DomDocument; }
        }
        IHTMLDocument4 idoc4
        {
            get { return (IHTMLDocument4)designWebBrowser.Document.DomDocument; }
        }
        IHTMLDocument5 idoc5
        {
            get { return (IHTMLDocument5)designWebBrowser.Document.DomDocument; }
        }
        #endregion

        #region 初始化和解析释放
        private string _articleText = string.Empty;
        public HTMLDesignControl(string articleText)
        {
            this.Name = "HTMLDesign";
            _articleText = articleText;
            codeTextEditorControl.Text = _articleText;

            this.ImeMode = ImeMode.On;
        }

        #endregion

        #region 工具栏按钮（属性方式）
        /// <summary>
        /// 主工具栏，属性调用
        /// </summary>
        HTMLToolStrip mainToolStrip = null;
        public HTMLToolStrip GetMainToolStrip()
        {
            mainToolStrip = new HTMLToolStrip(this);
            mainToolStrip.Dock = DockStyle.Top;
            mainToolStrip.BringToFront();
            this.Controls.Add(mainToolStrip);

            return mainToolStrip;
        }

        #endregion

        #region 设计控件
        /// <summary>
        /// 设计控件包含设计器，代码编辑器和属性面版
        /// </summary>
        public Panel htmlPanel = new Panel();
        public SplitContainer splitCon = new SplitContainer();
        public Panel GetHtmlPanel()
        {
            ResourcesIdPaths = new Dictionary<string, string>();
            htmlPanel.Name = "htmlPanel";

            splitCon.Panel1.Controls.Add(InitDesignWebBrowser());
            splitCon.Panel2.Controls.Add(InitCodeTextEditorControl());
            splitCon.Orientation = Orientation.Horizontal;
            htmlPanel.Controls.Add(splitCon);

            splitCon.Dock = DockStyle.Fill;
            htmlPanel.Dock = DockStyle.Fill;
            htmlPanel.BringToFront();

            this.mainToolStrip.DesignToolStripButton.Checked = true;
            GeneralMethods.SetForModeChage(this, SoftwareOption.HtmlDesigner.ShowView,0);
            return htmlPanel;
        }

        public Panel SetHtmlPanel()
        {
            string strDocumentText = "<html><head><link href='" + System.Windows.Forms.Application.StartupPath + @"\style.css' rel='stylesheet' type='text/css' /></head><body></body></html>";
            designWebBrowser.DocumentText = strDocumentText;
            designWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(designWebBrowser_DocumentCompleted);
            return htmlPanel;
        }

        #region 设计窗口
        /// <summary>
        /// 设计WebBrowser
        /// </summary>
        WebBrowser designWebBrowser = new WebBrowser();
        cHTMLElementEvents m_elemEvents = null;
        public WebBrowser InitDesignWebBrowser()
        {
            designWebBrowser.Dock = DockStyle.Fill;
            designWebBrowser.Name = "designWebBrowser";
            string strDocumentText = "<html><head><link href='" + System.Windows.Forms.Application.StartupPath + @"\style.css' rel='stylesheet' type='text/css' /></head><body></body></html>";
            designWebBrowser.DocumentText = strDocumentText;
            int[] dispids = {(int)HTMLEventDispIds.ID_ONKEYUP,
                    (int)HTMLEventDispIds.ID_ONCLICK,
                    (int)HTMLEventDispIds.ID_ONCONTEXTMENU,
                    (int)HTMLEventDispIds.ID_ONDRAG,
                    (int)HTMLEventDispIds.ID_ONDRAGSTART,
                    (int)HTMLEventDispIds.ID_ONDRAGEND,
                    (int)HTMLEventDispIds.ID_ONFOCUS,
                    (int)HTMLEventDispIds.ID_ONMOUSEDOWN,
                    (int)HTMLEventDispIds.ID_ONMOUSEUP};
            m_elemEvents = new cHTMLElementEvents();
            m_elemEvents.InitHTMLEvents(this, dispids, Iid_Clsids.DIID_HTMLElementEvents2);
            idoc2.designMode = "on";
            designWebBrowser.IsWebBrowserContextMenuEnabled = false;
            designWebBrowser.GotFocus += new EventHandler(designWebBrowser_GotFocus);
            designWebBrowser.Validated += new EventHandler(designWebBrowser_Validated);
            designWebBrowser.Document.MouseOver+=new HtmlElementEventHandler(Document_MouseOver);
            designWebBrowser.Document.MouseUp += new HtmlElementEventHandler(Document_MouseUp);
            designWebBrowser.DocumentCompleted +=new WebBrowserDocumentCompletedEventHandler(designWebBrowser_DocumentCompleted);
            ((Control)designWebBrowser).ImeMode = ImeMode.On;
            return designWebBrowser;
         }



        /// <summary>
        /// 设计器获得焦点时将属性面板激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void designWebBrowser_GotFocus(object sender, EventArgs e)
        {
            if (_proPanel!=null)
            _proPanel.Enabled = true;
        }

        /// <summary>
        /// 设计窗口到代码窗口的同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void designWebBrowser_Validated(object sender, EventArgs e)
        {
            DesignToCode(); 
        }

        /// <summary>
        /// 元素绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void designWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            IHTMLDocument3 pBody = ((IWebBrowser2)designWebBrowser.ActiveXInstance).Document as IHTMLDocument3;
            m_elemEvents.ConnectToHtmlEvents(pBody.documentElement);

            if (idoc2.body != null)
            {
                idoc2.body.innerHTML = _articleText;
                codeTextEditorControl.Text = GeneralMethods.tidy(idoc2.body.innerHTML);

                string htmlCode = codeTextEditorControl.Text;
                string ostr = "${srs_";
                while (htmlCode.IndexOf(ostr) > 0)
                {
                    string mediaId = htmlCode.Substring(htmlCode.IndexOf(ostr) + 6, 32);

                    SimpleExIndexXmlElement ele = Service.Sdsite.CurrentDocument.GetElementById(mediaId) as SimpleExIndexXmlElement;
                    if (ele != null)
                    {
                        if (!ResourcesIdPaths.ContainsKey(ele.Id))
                        {
                            ResourcesIdPaths.Add(ele.Id, ele.AbsoluteFilePath);
                        }

                        string PATH = SiteResourceService.ParseFormatId(ele.Id, true);
                        htmlCode = htmlCode.Replace(htmlCode.Substring(htmlCode.IndexOf(ostr), 39), "file:///" + ele.AbsoluteFilePath.Replace("\\", "/"));
                    }
                    else
                        break;
                }
            }
            DesignToCode();
            CodeToDesign();
        }

        #region 对表格的鼠标移动特殊处理
         void Document_MouseUp(object sender, HtmlElementEventArgs e)
        {
            HtmlElement[] elearr = new HtmlElement[100];
            string[] a = new string[100];
            elearr[0] = ele;
            a[0] = "<" + elearr[0].TagName.ToLower() + ">";
            int i = 0;
            while (elearr[i].Parent != null)
            {
                i++;
                elearr[i] = elearr[i - 1].Parent;
                a[i] = "<" + elearr[i].TagName.ToLower() + ">";
            }
            for (int j = i-1; j >-1 ; j--)
            {
                ToolStripDropDownButton t1 = new ToolStripDropDownButton();
                t1.Name = elearr[j].TagName;
                t1.Text = a[j];
                t1.ShowDropDownArrow = false;
                t1.Click += new EventHandler(t1_Click);
            }

            string strrrr = ele.OuterHtml;
        }

        void t1_Click(object sender, EventArgs e)
        {
            ToolStripDropDownButton myb = sender as ToolStripDropDownButton;
            IHTMLElement CurrentTD = currentEle;//TD
            IHTMLElement CurrentTable = currentEle;//TABLE
            IHTMLElement CurrentTR = null;
            if (currentEle.tagName == "TD")
            {
                CurrentTable = CurrentElement.parentElement.parentElement.parentElement;
                CurrentTR = CurrentElement.parentElement;
            }

            
            switch (myb.Name)
            {
                case "SPAN":
                    {
                        break;
                    }
                case "TABLE":
                    {
                        SetElementForSelect(CurrentTable, true);
                        SetElementForSelect(CurrentTR, false);
                        SetElementForSelect(CurrentTD, false);
                        
                        EleAddBehavior(CurrentTable as IHTMLElement2);
                        _proPanel.Controls.Add(AddPanel("TABLE"));
                    } break;
                case "BODY": idoc2.execCommand("SelectAll", true, true); break;
                case "TD":
                    {
                        SetElementForSelect(CurrentTable,false);
                        SetElementForSelect(CurrentTR, false);
                        SetElementForSelect(CurrentTD,true);
                        EleAddBehavior(CurrentTD as IHTMLElement2);
                        _proPanel.Controls.Add(AddPanel("TD"));
                        break;
                    }
                case "TR":
                    {
                        SetElementForSelect(CurrentTR, true);
                        SetElementForSelect(CurrentTable, false);
                        SetElementForSelect(CurrentTD, false);
                        break;
                    }
                // case "IMG":designWebBrowser.Document.ExecCommand break;// designWebBrowser.Document.ActiveElement = ele; break;
            }
        }
        void SetElementForSelect(IHTMLElement ele, bool selected)
        {
            if (selected)
            {
                ele.setAttribute("bordercolorlight", "#000000", 0);
                ele.setAttribute("bordercolordark", "#000000", 0);
            }
            else
            {
                ele.setAttribute("bordercolordark", "", 0);
                ele.setAttribute("bordercolorlight", "", 0);
            }
        }

        HtmlElement ele = null;
        HtmlElement eleP = null;
        HtmlElement eleE = null;
        void Document_MouseOver(object sender, HtmlElementEventArgs e)
        {
            ele = e.ToElement;

            eleP = ele;
            switch (eleP.TagName)
            {
                case "TABLE":
                case "TD":
                    {
                        string colorstr=ColorTranslator.ToHtml(SoftwareOption.HtmlDesigner.TableBorderColor);
                        eleP.SetAttribute("bordercolordark", colorstr);//todo待选项设置其颜色
                        eleP.SetAttribute("bordercolorlight", colorstr);
                        break;
                    }
                //case "IMG":
                    //eleP.SetAttribute("border", "10"); break;
            }
            if (eleE != null && !Object.ReferenceEquals(eleE, eleP))
                switch (eleE.TagName)
                {
                    case "TABLE":
                    case "TD":
                        {
                            eleE.SetAttribute("bordercolordark", "");
                            eleE.SetAttribute("bordercolorlight", ""); 
                            break;
                        }
                    //case "IMG":
                     //   eleE.SetAttribute("border", "0"); break;
                }
            eleE = eleP;
        }
        #endregion

        #endregion

        #region 代码窗口
        /// <summary>
        /// 代码编辑器
        /// </summary>
        TextEditorControl codeTextEditorControl = new TextEditorControl();
        public TextEditorControl InitCodeTextEditorControl()
        {
            
            codeTextEditorControl.Dock = DockStyle.Fill;
            InitEditorParams(codeTextEditorControl, "HTML", false);
            codeTextEditorControl.ContextMenuStrip = new ContextPopMenu( codeTextEditorControl);
            codeTextEditorControl.Validated += new EventHandler(codeTextEditorControl_Validated);
            codeTextEditorControl.Enter += new EventHandler(codeTextEditorControl_Enter);
            codeTextEditorControl.ImeMode = ImeMode.On;

            this.codeTextEditorControl.ShowLineNumbers =  SoftwareOption.HtmlDesigner.IsShowLineNum;
            this.codeTextEditorControl.ShowEOLMarkers =  SoftwareOption.HtmlDesigner.TecShowEOLMarkers;
            this.codeTextEditorControl.ShowHRuler =  SoftwareOption.HtmlDesigner.TecShowHRuler;
            this.codeTextEditorControl.ShowVRuler = SoftwareOption.HtmlDesigner.TecShowVRuler;
            this.codeTextEditorControl.Font = new Font(SoftwareOption.HtmlDesigner.TextFont, float.Parse(SoftwareOption.HtmlDesigner.TextSize));
            this.codeTextEditorControl.BorderStyle = SoftwareOption.HtmlDesigner.TecBorderStyle;
            return codeTextEditorControl;
        }

        /// <summary>
        /// 代码编辑窗口进入时完成同步,并将属性面板置为不可用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void codeTextEditorControl_Enter(object sender, EventArgs e)
        {
           // if (idoc2.body != null)
            //    codeTextEditorControl.Text = GeneralMethods.tidy(idoc2.body.innerHTML);
            if (_proPanel != null)
                _proPanel.Enabled = false;
        }

        /// <summary>
        /// 初始化编辑器状态
        /// </summary>
        /// <param name="tec">编辑器控件</param>
        /// <param name="language">编辑器支持的Code语言种类</param>
        /// <param name="isTestText">是否显示Test文本</param>
        protected void InitEditorParams(TextEditorControl tec, string language, bool isTestText)
        {
            tec.Document.HighlightingStrategy =
                HighlightingStrategyFactory.CreateHighlightingStrategy(language);
            tec.Encoding = System.Text.Encoding.Default;
            tec.ShowEOLMarkers = false;
            tec.ShowHRuler = false;
            tec.ShowInvalidLines = false;
            tec.ShowSpaces = false;
            tec.ShowTabs = false;
            tec.ShowVRuler = false;
            tec.TextEditorProperties.AutoInsertCurlyBracket = true;
            tec.TextEditorProperties.IndentStyle = IndentStyle.Smart;
            tec.ShowMatchingBracket = true;
            tec.IsIconBarVisible = true;
            tec.Font = new Font("Courier New", 8.25F);
            tec.ActiveTextAreaControl.TextArea.TextEditorProperties.EnableFolding = true;

            tec.Dock = DockStyle.Fill;

            if (isTestText)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                if (ofd.FileName != null)
                {
                    string fileContents;
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        fileContents = sr.ReadToEnd();
                        sr.Close();
                    }
                    tec.Text = fileContents;
                }
            }
        }

        /// <summary>
        /// 代码窗口到设计窗口的同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void codeTextEditorControl_Validated(object sender, EventArgs e)
        {
            CodeToDesign();
        }

        #endregion

        #region 窗口切换实现代码同步
        public void DesignToCode()
        {
            if (idoc2.body != null)
            {
                string codestr =idoc2.body.outerHTML;// GeneralMethods.tidy();
                codestr = codestr.Replace(@"about:blank", "").Replace(@"about:", "");

                //绝对路径转ID
                MatchCollection msImage = Utility.Regex.HtmlUrl.Matches(codestr);
                MatchCollection msMedia = Utility.Regex.HtmlMediaUrl.Matches(codestr);
                List<string> listFilePaths = new List<string>();
                foreach (Match item in msImage)
                {
                    listFilePaths.Add(item.Groups["path"].Value);
                }

                foreach (Match item in msMedia)
                {
                    listFilePaths.Add(item.Groups["path"].Value);
                }

                foreach (string item in listFilePaths)
                {
                    string ss = item.Replace("/", "\\").Replace("%20", " ");

                    Debug.Assert(!string.IsNullOrEmpty(ss));

                    if (ResourcesIdPaths.ContainsValue(ss))
                    {
                        string id = "";

                        foreach (KeyValuePair<string, string> idpath in ResourcesIdPaths)
                        {
                            if (idpath.Value == ss)
                            {
                                id = idpath.Key;
                                break;
                            }
                        }
                        Debug.Assert(!string.IsNullOrEmpty(id));
                        codestr = Utility.Regex.ReplaceAbsFileFilter(codestr, item, id);
                    }
                }

                codestr = codestr.Replace("<BODY>", "");
                codestr = codestr.Replace("</BODY>", "");
                codeTextEditorControl.Text = codestr;
               // GeneralMethods.tidySpan(idoc2);
            }
        }

        public void CodeToDesign()
        {
            if (idoc2 != null && idoc2.body != null)
            {
                //相对路径转绝对路径
                string htmlCode = codeTextEditorControl.Text;
                string ostr = "${srs_";
                while (htmlCode.IndexOf(ostr) > 0)
                {
                    string mediaId = htmlCode.Substring(htmlCode.IndexOf(ostr) + 6, 32);

                    SimpleExIndexXmlElement ele = Service.Sdsite.CurrentDocument.GetElementById(mediaId) as SimpleExIndexXmlElement;
                    if (ele != null)
                    {
                        if (!ResourcesIdPaths.ContainsKey(ele.Id))
                        {
                            ResourcesIdPaths.Add(ele.Id, ele.AbsoluteFilePath);
                        }

                        string PATH = SiteResourceService.ParseFormatId(ele.Id, true);
                        htmlCode = htmlCode.Replace(htmlCode.Substring(htmlCode.IndexOf(ostr), 39), "file:///" + ele.AbsoluteFilePath.Replace("\\", "/"));
                    }
                    else
                        break;
                }
                
                idoc2.body.innerHTML = htmlCode;
            }
        }
        #endregion
        
        #region 属性面版
        private static Panel _proPanel = null;

        public Panel PropertyPanel
        {
            get
            {
                if (_proPanel == null)
                    _proPanel = new Panel();
                _proPanel.Name = "propertyPanel";
                _proPanel.Dock = DockStyle.Fill;
                _proPanel.Controls.Add(AddPanel(""));
                _proPanel.BringToFront();
                return _proPanel;
            }
        }

        #endregion

        #endregion

        #region 接口实现

        void EleAddBehavior(string tagsName)
        {
            IHTMLElementCollection elements = (IHTMLElementCollection)(idoc2).all.tags(tagsName);
            foreach (IHTMLElement2 element in elements)
            {
                object obj = this;
                element.addBehavior(null, ref obj);
            }
        }

        void EleAddBehavior(IHTMLElement2 element)
        {
            object obj = this;
            element.addBehavior(null, ref obj);
        }

        bool IHTMLEventCallBack.HandleHTMLEvent(HTMLEventType EventType, HTMLEventDispIds EventDispId, IHTMLEventObj pEvtObj)
        {
            if (_proPanel == null)
            {
                return false;
            }
            bool bret = true;
            IHTMLElement2 element = pEvtObj.SrcElement as IHTMLElement2;
            if ((EventDispId == HTMLEventDispIds.ID_ONMOUSEDOWN))
            {
                #region OnMouseDown
                currentEle = pEvtObj.SrcElement;

                switch (pEvtObj.SrcElement.tagName)
                {
                    case "H1":
                    case "H2":
                    case "H3":
                    case "H4":
                    case "H5":
                    case "H6":
                    case "P":
                    case "FONT":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel(pEvtObj.SrcElement.tagName));
                            break;
                        }
                    case "PRE":
                        {
                            _proPanel.Controls.Add(AddPanel(pEvtObj.SrcElement.tagName));
                            break;
                        }
                    case "TD":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("TD"));
                            break;
                        }
                    case "TABLE":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("TABLE"));
                            break;
                        }
                    case "IMG":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("IMG"));
                            break;
                        }
                    case "EMBED":
                    case "OBJECT":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("object"));
                            break;
                        }
                    case "HR":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("HR"));
                            break;
                        }
                    case "SPAN":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("SPAN"));
                            break;
                        }
                    case "A":
                        {
                            _proPanel.Controls.Add(AddPanel("A"));
                            break;
                        }
                    case "BODY":
                        {
                            phraseb = null;
                            if (_proPanel != null)
                            {
                                _proPanel.Controls.Add(AddPanel("BODY"));
                            }
                        } break;
                }
                #endregion
            }
            if ((EventDispId == HTMLEventDispIds.ID_ONCLICK) || (EventDispId == HTMLEventDispIds.ID_ONKEYUP))
            {
                #region OnClick && OnKeyUp
                switch (pEvtObj.SrcElement.tagName)
                {
                    case "IMG": break;
                    case "OBJECT": break;
                    case "TD"://表格单元格
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("TD"));
                            break;
                        }
                    case "":
                        {
                            _proPanel.Controls.Add(AddPanel("")); 
                            break;
                        }
                }
                #endregion
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONCONTEXTMENU)
            {
                #region ONContextMenu
                bret = false;

                if (pEvtObj != null)
                {
                    if (pEvtObj.SrcElement != null)
                    {
                        ContextMenuStrip designContextMenuStrip = new DesignPopMenu( pEvtObj.SrcElement.tagName, this);
                        designContextMenuStrip.Show(pEvtObj.ScreenX, pEvtObj.ScreenY);
                    }
                }
                #endregion
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONMOUSEUP)
            {
                switch (pEvtObj.SrcElement.tagName)
                {
                    case "BODY":
                        {
                            if (_proPanel != null)
                            {
                                _proPanel.Controls.Add(AddPanel("BODY"));// PropertyPanels.Instance("BODY",m_oHTMLCtxMenu,designWebBrowser,codeTextEditorControl,null,null,null,null));
                            }
                            break;
                        }
                    case "SPAN":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("SPAN"));
                            break;
                        }
                    case "A":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("A"));
                            break;
                        }
                    case "TH":
                    case "TD":
                        {
                            EleAddBehavior(element);
                            _proPanel.Controls.Add(AddPanel("TD"));
                            break;
                        }
                }
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONDRAG)
            {
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONDRAGSTART)
            {
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONDRAGEND)
            {
            }
            return bret;
        }
        

        #region IElementBehaviorFactory 成员

        ImageBehavior imgb = null;
        FlashBehavior flashb = null;
        TableBehavior tableb = null;
        TableCellBehavior tablecellb = null;
        LineBehavior lineb = null;
        LINKBehavior linkb = null;
        PhraseBehavior phraseb = null;
        HeaderBehavior headerb = null;
        PBehavior pb = null;
        EmbedBehavior embedb = null;
        FontBehavior fontb = null;
        public IElementBehavior FindBehavior(string bstrBehavior, string bstrBehaviorUrl, IElementBehaviorSite pSite)
        {
            string s = pSite.GetElement().ToString();
            if (pSite.GetElement() is IHTMLImgElement)
            {
                imgb = new ImageBehavior();
                return imgb;
            }
            else if (pSite.GetElement() is IHTMLEmbedElement)
            {
                embedb = new EmbedBehavior();
                return embedb;
            }
            else if (pSite.GetElement() is IHTMLObjectElement)
            {
                flashb = new FlashBehavior();
                return flashb;
            }
            else if (pSite.GetElement() is mshtml.IHTMLTable)
            {
                tableb = new TableBehavior();
                return tableb;
            }
            else if ((pSite.GetElement() is mshtml.IHTMLTableCell))
            {
                tablecellb = new TableCellBehavior();
                return tablecellb;
            }
            else if (pSite.GetElement() is mshtml.IHTMLHRElement)
            {
                lineb = new LineBehavior();
                return lineb;
            }
            else if (pSite.GetElement() is mshtml.IHTMLAnchorElement)
            {
                linkb = new LINKBehavior();
                return linkb;
            }
            else if (pSite.GetElement() is mshtml.IHTMLPhraseElement)
            {
                phraseb = new PhraseBehavior();
                return phraseb;
            }
            else if (pSite.GetElement() is mshtml.IHTMLHeaderElement)
            {
                headerb = new HeaderBehavior();
                return headerb;
            }
            else if (pSite.GetElement() is mshtml.IHTMLParaElement)
            {
                pb = new PBehavior();
                return pb;
            }
            else if (pSite.GetElement() is mshtml.IHTMLFontElement)
            {
                fontb = new FontBehavior();
                return fontb;
            }
            else
                return null;
        }

        #endregion

        PropertyPanels AddPanel(string htmlType)
        {
            return PropertyPanels.Instance(htmlType, this);
        }

        #endregion

        #region 控件抛出属性
        #region Behavior

        public ImageBehavior ImageBe
        {
            get
            {
                return imgb;
            }
            set
            {
                imgb = value;
            }
        }
        public FlashBehavior FlashBe
        {
            get
            {
                return flashb;
            }
            set
            {
                flashb = value;
            }
        }

        public EmbedBehavior EmbedBe
        {
            get
            {
                return embedb;
            }
            set
            {
                embedb = value;
            }
        }
        public TableBehavior TableBe
        {
            get
            {
                return tableb;
            }
            set
            {
                tableb = value;
            }
        }
        public TableCellBehavior TableCellBe
        {
            get
            {
                return tablecellb;
            }
            set
            {
                tablecellb = value;
            }
        }
        public LineBehavior LineBe
        {
            get
            {
                return lineb;
            }
            set
            {
                lineb = value;
            }
        }
        public LINKBehavior LinkBe
        {
            get
            {
                return linkb;
            }
            set
            {
                linkb = value;
            }
        }
        public PhraseBehavior PhraseBe
        {
            get
            {
                return phraseb;
            }
            set
            {
                phraseb = value;
            }
        }
        public HeaderBehavior HeaderBe
        {
            get
            {
                return headerb;
            }
            set
            {
                headerb = value;
            }
        }
        public FontBehavior FontBe
        {
            get
            {
                return fontb;
            }
            set
            {
                fontb = value;
            }
        }

        #endregion

        public WebBrowser DesignWebBrowser
        {
            get
            {
                return designWebBrowser;
            }
            set
            {
                designWebBrowser = value;
            }
        }
        public TextEditorControl CodeTextEditorControl
        {
            get
            {
                return codeTextEditorControl;
            }
            set
            {
                codeTextEditorControl = value;
            }
        }
        public HTMLToolStrip MainToolStrip 
        {
            get
            {
                return mainToolStrip;
            }
            set
            {
                mainToolStrip = value;
            }
        }
        public IHTMLElement CurrentElement
        {
            get
            {
                return currentEle;
            }
            set
            {
                currentEle = value;
            }
        }

        public bool DesignToolButtomChecked
        {
            get
            {
                return mainToolStrip.DesignToolStripButton.Checked;
            }
            set
            {
                if (mainToolStrip != null)
                    mainToolStrip.DesignToolStripButton.Checked = value;
            }
        }

        public bool CodeToolButtomChecked
        {
            get
            {
                return mainToolStrip.CodeToolStripButton.Checked;
            }
            set
            {
                if (mainToolStrip!=null)
                mainToolStrip.CodeToolStripButton.Checked = value;
            }
        }
        public bool SplitToolButtomChecked
        {
            get
            {
                return mainToolStrip.SplitToolStripButton.Checked;
            }
            set
            {
                if (mainToolStrip != null)
                    mainToolStrip.SplitToolStripButton.Checked = value;
            }
        }

        public IHTMLDocument2 Idoc2
        {
            get
            {
                return idoc2;
            }
        }
        public string PageText
        {
            get
            {
                return codeTextEditorControl.Text;
            }
            set
            {
                codeTextEditorControl.Text = value;// GeneralMethods.tidy(value);
                _articleText = codeTextEditorControl.Text;
            }
        }
        public string PageTitle
        {
            get
            {
                return mainToolStrip.ContentTitle;
            }
            set
            {
                if (mainToolStrip != null)
                    mainToolStrip.ContentTitle = value;
            }
        }

        public string wbTitle
        {
            get
            {
                return designWebBrowser.Document.Title;
            }
            set
            {
                designWebBrowser.Document.Title = value;
            }
        }

        public bool IsModified
        {
            get
            {
              /*  string bodyText = "";
                object bodyObj = this.idoc2.body.innerHTML;
                if (bodyObj != null)
                    bodyText = this.idoc2.body.innerHTML;*/
                if (designWebBrowser.Focused)
                    DesignToCode();
                //else
                //    _htmldesign.CodeToDesign();
                if (_articleText != this.codeTextEditorControl.Text)// || _articleText != bodyText)
                    return true;
                else
                    return false;
            }
        }
        #endregion
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            codeTextEditorControl.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
        }
        public event EventHandler Changed;
        void Document_DocumentChanged(object sender, DocumentEventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }
    }
}