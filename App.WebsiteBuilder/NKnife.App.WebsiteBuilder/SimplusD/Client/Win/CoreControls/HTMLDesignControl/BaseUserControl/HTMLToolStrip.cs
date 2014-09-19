using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using mshtml;
using ICSharpCode.TextEditor;
using System.Web;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 正文设计器的工具栏
    /// </summary>
    public partial class HTMLToolStrip : ToolStrip
    {
        WebBrowser _designWebBrouser = null;
        HTMLDesignControl _htmlDesigner = null;

        ToolStripSeparator sp1 = new ToolStripSeparator();
        ToolStripSeparator sp2 = new ToolStripSeparator();
        ToolStripSeparator sp3 = new ToolStripSeparator();
        ToolStripSeparator sp4 = new ToolStripSeparator();
        ToolStripSeparator sp5 = new ToolStripSeparator();

        ToolStripButton designToolStripButton = null;
        ToolStripButton codeToolStripButton = null;
        ToolStripButton splitToolStripButton = null;

        ToolStripButton tableToolStripButton = null;
        ToolStripButton imageToolStripButton = null;
        ToolStripButton flashToolStripButton = null;
        ToolStripButton audioToolStripButton = null;
        ToolStripButton mediaToolStripButton = null;
        ToolStripButton emailToolStripButton = null;
        ToolStripButton linkToolStripButton = null;
        ToolStripButton dateToolStripButton = null;
        ToolStripButton hrToolStripButton = null;
        ToolStripLabel titleToolStripLabel = null;
        ToolStripTextBox titleToolStripTextBox = null;
        ToolStripButton propertyToolStripButton = null;

        ToolStripButton signToolStripButton = null;

        ToolStripDropDownButton brToolStripDropDownButton = null;
        ToolStripMenuItem brToolStripMenuItem = null;
        ToolStripMenuItem nbspToolStripMenuItem = null;
        ToolStripMenuItem L_quotationToolStripMenuItem = null;
        ToolStripMenuItem R_quotationToolStripMenuItem = null;
        ToolStripMenuItem deshToolStripMenuItem = null;
        ToolStripMenuItem poundToolStripMenuItem = null;
        ToolStripMenuItem EurocurrencyToolStripMenuItem = null;
        ToolStripMenuItem yenToolStripMenuItem = null;
        ToolStripMenuItem copyrightToolStripMenuItem = null;
        ToolStripMenuItem reg_trad_markToolStripMenuItem = null;
        ToolStripMenuItem brandToolStripMenuItem = null;
        ToolStripMenuItem osignToolStripMenuItem = null;

        public ToolStripLabel TitleToolStripLabel
        {
            get { return titleToolStripLabel; }
            set { titleToolStripLabel = value; }
        }


        public ToolStripTextBox TitleToolStripTextBox
        {
            get { return titleToolStripTextBox; }
            set { titleToolStripTextBox = value; }
        }

        public ToolStripButton PropToolStripButton
        {
            get { return propertyToolStripButton; }
            set { propertyToolStripButton = value; }
        }

        public HTMLToolStrip(HTMLDesignControl HTMLPanel)
        {
            InitializeComponent();

            _htmlDesigner = HTMLPanel;
            _designWebBrouser = HTMLPanel.DesignWebBrowser;
            InitMy(_designWebBrouser, HTMLPanel);
            this.ItemClicked += new ToolStripItemClickedEventHandler(HTMLToolStrip_ItemClicked);
            brToolStripDropDownButton.DropDownItemClicked += new ToolStripItemClickedEventHandler(brToolStripDropDownButton_DropDownItemClicked);
            titleToolStripTextBox.Validated += new EventHandler(titleToolStripTextBox_Validated);
        }

        void titleToolStripTextBox_Validated(object sender, EventArgs e)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            PageSimpleExXmlElement ele = doc.GetPageElementById(_htmlDesigner.PageId);
            ele.Title = titleToolStripTextBox.Text;
            doc.Save();

            PageXmlDocument pageDoc = doc.GetPageDocumentById(_htmlDesigner.PageId);
            pageDoc.Title = titleToolStripTextBox.Text;
            pageDoc.Save();
        }

        public void InitMy(WebBrowser wbs, HTMLDesignControl HTMLPanel)
        {
            designToolStripButton = new ToolStripButton();
            codeToolStripButton = new ToolStripButton();
            splitToolStripButton = new ToolStripButton();
            tableToolStripButton = new ToolStripButton();
            imageToolStripButton = new ToolStripButton();
            flashToolStripButton = new ToolStripButton();
            audioToolStripButton = new ToolStripButton();
            mediaToolStripButton = new ToolStripButton();
            emailToolStripButton = new ToolStripButton();
            linkToolStripButton = new ToolStripButton();
            dateToolStripButton = new ToolStripButton();
            hrToolStripButton = new ToolStripButton();
            titleToolStripLabel = new ToolStripLabel();
            titleToolStripTextBox = new ToolStripTextBox();
            propertyToolStripButton = new ToolStripButton();

            signToolStripButton = new ToolStripButton();
//ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.br")
            brToolStripMenuItem = new ToolStripMenuItem("", ResourceService.GetResourceImage("br"), null, "br");
            nbspToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.nbsp"), ResourceService.GetResourceImage("tab"), null, "nbsp");
            L_quotationToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.L_quotation_marks"), ResourceService.GetResourceImage("leftQuotation"), null, "L_quotation");
            R_quotationToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.R_quotation_marks"), ResourceService.GetResourceImage("rightQuotation"), null, "R_quotation");
            deshToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.dash"), ResourceService.GetResourceImage("line"), null, "desh");

            poundToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.pound"), ResourceService.GetResourceImage("pound"), null, "pound");
            EurocurrencyToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.Eurocurrency"), ResourceService.GetResourceImage("medlar"), null, "Eurocurrency");
            yenToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.yen"), ResourceService.GetResourceImage("yen"), null, "yen");

            copyrightToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.copyright"), ResourceService.GetResourceImage("right"), null, "copyright");
            reg_trad_markToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.reg_trad_mark"), ResourceService.GetResourceImage("reg_logo"), null, "reg_trad_mark");
            brandToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.brand"), ResourceService.GetResourceImage("logo"), null, "brand");

            osignToolStripMenuItem = new ToolStripMenuItem(ResourceService.GetResourceText("HTMLToolStripText.menuitemtext.osign"), ResourceService.GetResourceImage("other_sign"), null, "osign");

            designToolStripButton.Name = "DesignToolStripButton";
            designToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            designToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.design");
            designToolStripButton.CheckOnClick = true;
            designToolStripButton.Image = ResourceService.GetResourceImage("design");

            codeToolStripButton.Name = "HtmlToolStripButton";
            codeToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.code");
            codeToolStripButton.CheckOnClick = true;
            codeToolStripButton.Image = ResourceService.GetResourceImage("code");

            splitToolStripButton.Name = "SplitToolStripButton";
            splitToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.Split");
            splitToolStripButton.CheckOnClick = true;
            splitToolStripButton.Image = ResourceService.GetResourceImage("split");

            tableToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tableToolStripButton.Name = "tableToolStripButton";
            tableToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.table");
            tableToolStripButton.Image = ResourceService.GetResourceImage("table");

            imageToolStripButton.Name = "imageToolStripButton";
            imageToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imageToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.img");
            imageToolStripButton.Image = ResourceService.GetResourceImage("pictrue");

            flashToolStripButton.Name = "flashToolStripButton";
            flashToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            flashToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.flash");
            flashToolStripButton.Image = ResourceService.GetResourceImage("flash");

            audioToolStripButton.Name = "audioToolStripButton";
            audioToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            audioToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.audio");
            audioToolStripButton.Image = ResourceService.GetResourceImage("music");

            mediaToolStripButton.Name = "mediaToolStripButton";
            mediaToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mediaToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.media");
            mediaToolStripButton.Image = ResourceService.GetResourceImage("flash_video");

            emailToolStripButton.Name = "emailToolStripButton";
            emailToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            emailToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.email");
            emailToolStripButton.Image = ResourceService.GetResourceImage("E_mail");

            linkToolStripButton.Name = "linkToolStripButton";
            linkToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.link");
            linkToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            linkToolStripButton.Image = ResourceService.GetResourceImage("link");

            dateToolStripButton.Name = "dateToolStripButton";
            dateToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            dateToolStripButton.ToolTipText = ResourceService.GetResourceText("HTMLToolStripText.buttontext.date");
            dateToolStripButton.Image = ResourceService.GetResourceImage("date");

            hrToolStripButton.Name = "hrToolStripButton";
            hrToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            hrToolStripButton.Text = ResourceService.GetResourceText("HTMLToolStripText.buttontext.hr");
            hrToolStripButton.Image = ResourceService.GetResourceImage("line");

            signToolStripButton.Name = "signToolStripButton";

            titleToolStripTextBox.Name = "titleToolStripTextBox";
            titleToolStripTextBox.Text = ResourceService.GetResourceText("HTMLToolStripText.buttontext.titletextbox");
            titleToolStripTextBox.Size = new Size(200, 25);
            titleToolStripLabel.Name = "titleToolStripLabel";
            titleToolStripLabel.Text = ResourceService.GetResourceText("HTMLToolStripText.buttontext.titlelabel");

            propertyToolStripButton.Name = "propertyToolStripTextBox";
            propertyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            propertyToolStripButton.ToolTipText = ResourceService.GetResourceText("pageproperty.caption");
            propertyToolStripButton.Image = ResourceService.GetResourceImage("MainMenu.view.property");

            this.Location = new Point(0, 0);
            this.Dock = DockStyle.Top;
            this.Name = "tsMain";

            //空格下拉工具按钮加入
            brToolStripDropDownButton = new ToolStripDropDownButton(brToolStripMenuItem.Text, null,
brToolStripMenuItem, nbspToolStripMenuItem,
L_quotationToolStripMenuItem, R_quotationToolStripMenuItem, deshToolStripMenuItem,
sp1,
poundToolStripMenuItem, EurocurrencyToolStripMenuItem, yenToolStripMenuItem,
sp2,
copyrightToolStripMenuItem, reg_trad_markToolStripMenuItem, brandToolStripMenuItem,
osignToolStripMenuItem);

            //整个工具栏的按钮加入
            this.Items.AddRange(new ToolStripItem[] { //需以属性方式加入，按钮，否则按钮的属性无法载入
                    designToolStripButton,splitToolStripButton,codeToolStripButton,
                    sp3,
                    tableToolStripButton,imageToolStripButton,
                    flashToolStripButton,audioToolStripButton, mediaToolStripButton,                     
                    sp4,
                    emailToolStripButton, linkToolStripButton,dateToolStripButton,hrToolStripButton,   
                    sp5, 
                    signToolStripButton,
                brToolStripDropDownButton,titleToolStripLabel,titleToolStripTextBox,propertyToolStripButton
                });
        }

        string currentSign = "";
        /// <summary>
        /// BR事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void brToolStripDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            IHTMLDocument2 _idoc2 = _designWebBrouser.Document.DomDocument as IHTMLDocument2;
            ToolStripMenuItem toolMenuItem = sender as ToolStripMenuItem;
            switch (e.ClickedItem.Name)
            {
                case "br":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "<br />", "");
                        currentSign = "<br />";
                        break;
                    }
                case "nbsp":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&nbsp;", "");
                        currentSign = "&nbsp;";
                        break;
                    }
                case "L_quotation":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&#8220;", "");
                        currentSign = "&#8220;";
                        break;
                    }
                case "R_quotation":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&#8221;", "");
                        currentSign = "&#8221;";
                        break;
                    }
                case "desh":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&#8212;", "");
                        currentSign = "&#8212;";
                        break;
                    }
                case "pound":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&pound;", "");
                        currentSign = "&pound;";
                        break;
                    }
                case "Eurocurrency":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&#8364;", "");
                        currentSign = "&#8364;";
                        break;
                    }
                case "yen":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&yen;", "");
                        currentSign = "&yen;";
                        break;
                    }
                case "copyright":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&copy;", "");
                        currentSign = "&copy;";
                        break;
                    }
                case "reg_trad_mark":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&reg;", "");
                        currentSign = "&reg;";
                        break;
                    }
                case "brand":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "&#8482;", "");
                        currentSign = "&#8482;";
                        break;
                    }
                case "osign":
                    {
                        InsertOtherForm sign = new InsertOtherForm();
                        if (sign.ShowDialog() == DialogResult.OK)
                        {
                            InsertElementHelper.AddToSelection(_idoc2, sign.InsertSign, "");
                            currentSign = sign.InsertSign;
                        }
                        break;
                    }
            }

            signToolStripButton.Text  = HttpUtility.HtmlDecode(currentSign);// e.ClickedItem.Text;
            brToolStripDropDownButton.Text = "";

        }

        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HTMLToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            IHTMLDocument2 _idoc2 = _htmlDesigner.Idoc2;
            TextEditorControl codeEdit = _htmlDesigner.CodeTextEditorControl;
            ToolStripButton thisBtn = e.ClickedItem as ToolStripButton;
            if (thisBtn == null)
                return;
            switch (thisBtn.Name)
            {
                case "propertyToolStripTextBox":
                    {
                        string pageId = _htmlDesigner.PageId;
                        PagePropertyForm pageTextPropertyForm = new PagePropertyForm(pageId);
                        pageTextPropertyForm.ShowDialog(Service.Workbench.MainForm);
                        break;
                    }
                case "tableToolStripButton":
                    {
                        InsertElementHelper.Inserttable(_idoc2);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "linkToolStripButton":
                    {
                        InsertElementHelper.Insertlink(_idoc2, _htmlDesigner);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "imageToolStripButton":
                    {
                        InsertElementHelper.InsertImage(_idoc2);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "flashToolStripButton":
                    {
                        InsertElementHelper.InsertFlash(_idoc2);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "mediaToolStripButton":
                    {
                        InsertElementHelper.InsertVideo(_idoc2);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "emailToolStripButton":
                    {
                        InsertElementHelper.InsertEmail(_idoc2);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "dateToolStripButton":
                    {
                        InsertElementHelper.InsertDateTime(_idoc2);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "brToolStripButton":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "<BR />", "");
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "hrToolStripButton":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, "<HR />", "");
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                case "audioToolStripButton":
                    {
                        InsertElementHelper.InsertAudio(_idoc2);
                        _htmlDesigner.DesignToCode();
                        break;
                    }
                #region  design,html,split
                case "DesignToolStripButton":
                    {
                        GeneralMethods.SetForModeChage(_htmlDesigner, DesignerOpenType.Design,1);
                        break;
                    } 
                case "HtmlToolStripButton":
                    {
                        GeneralMethods.SetForModeChage(_htmlDesigner, DesignerOpenType.Code,1);
                    } break;
                case "SplitToolStripButton":
                    {
                        GeneralMethods.SetForModeChage(_htmlDesigner, DesignerOpenType.Spliter, 1);
                        break;
                    }
                case "signToolStripButton":
                    {
                        InsertElementHelper.AddToSelection(_idoc2, currentSign, "");
                        break;
                    }

                #endregion
            }

            if (!string.IsNullOrEmpty(e.ClickedItem.Name))
            {
                if (_htmlDesigner.DesignWebBrowser.Focused)
                {
                    _htmlDesigner.DesignToCode();
                    _htmlDesigner.CodeToDesign();
                }

                else
                {
                    _htmlDesigner.CodeToDesign();
                }
            }
        }

        #region 外抛属性
        public string ContentTitle
        {
            get { return titleToolStripTextBox.Text; }
            set { titleToolStripTextBox.Text = value; }
        }

        public ToolStripButton DesignToolStripButton
        {
            get { return designToolStripButton; }
        }


        public ToolStripButton CodeToolStripButton
        {
            get { return codeToolStripButton; }
        }



        public ToolStripButton SplitToolStripButton
        {
            get { return splitToolStripButton; }
        }
        #endregion
    }
}