using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Web;
using Jeelu.Win;

namespace Jeelu.Win
{
    /// <summary>
    /// HTML编辑器工具栏
    /// </summary>
    public partial class HTMLToolStrip : ToolStrip
    {
        WebBrowser _designWebBrouser = null;
        HTMLDesignerContrl _ownerHtmlDesigner = null;
        IHTMLDocument2 idoc2 = null;

        public HTMLDesignerContrl OwnerHtmlDesigner
        {
            get { return _ownerHtmlDesigner; }
            set
            {
                _ownerHtmlDesigner = value;
            }
        }

        ToolStripSeparator sp1 = new ToolStripSeparator();
        ToolStripSeparator sp2 = new ToolStripSeparator();
        ToolStripSeparator sp3 = new ToolStripSeparator();
        ToolStripSeparator sp4 = new ToolStripSeparator();
        ToolStripSeparator sp5 = new ToolStripSeparator();
        

        ToolStripButton designToolStripButton = null;
        ToolStripButton codeToolStripButton = null;
        ToolStripButton boldToolStripButton = null;
        ToolStripButton italicToolStripButton = null;
        ToolStripButton underlineToolStripButton = null;

        ToolStripButton leftToolStripButton = null;
        ToolStripButton rightToolStripButton = null;
        ToolStripButton allToolStripButton = null;

        FontToolStripComboBox fontToolStripComboBox = null;
        FontSizeToolStripComboBox sizeToolStripComboBox = null;
        ColorToolStripButton colorToolStripButton = null;
        ParaToolStripComboBox paraToolStripComboBox = null;
        ToolStripButton listToolStripButton = null;
        ToolStripButton numToolStripButton = null;
        ToolStripButton outToolStripButton = null;
        ToolStripButton inToolStripButton = null;

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

        ToolStripDropDownButton signToolStripDropDownButton = null;
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

 

        public HTMLToolStrip()
        {
            InitMy();
            this.ItemClicked += HTMLToolStrip_ItemClicked;

            signToolStripDropDownButton.DropDownItemClicked +=signToolStripDropDownButton_DropDownItemClicked;
            titleToolStripTextBox.Validated += titleToolStripTextBox_Validated;
            fontToolStripComboBox.SelectedIndexChanged += ToolStripComboBox_SelectedIndexChanged;
            sizeToolStripComboBox.SelectedIndexChanged += ToolStripComboBox_SelectedIndexChanged;
            paraToolStripComboBox.SelectedIndexChanged += ToolStripComboBox_SelectedIndexChanged;
            colorToolStripButton.MyColorChanged += new EventHandler(colorToolStripButton_MyColorChanged);
        
        }

        void colorToolStripButton_MyColorChanged(object sender, EventArgs e)
        {
            IHTMLDocument2 _idoc2 = _ownerHtmlDesigner.DesignWebBrowser.idoc2;
            idoc2.execCommand("ForeColor", true, colorToolStripButton.MyColor.ToArgb());
        }

        void ToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IHTMLDocument2 _idoc2 = _ownerHtmlDesigner.DesignWebBrowser.idoc2;
            ToolStripComboBox item = sender as ToolStripComboBox;
            switch (item.Name)
            {
                case "fontToolStripComboBox":
                    {
                        _idoc2.execCommand("FontName", true, item.Text);
                        break;
                    }
                case "sizeToolStripComboBox":
                    {
                        _idoc2.execCommand("FontSize", true, item.Text);
                        break;
                    }
                case "paraToolStripComboBox":
                    {
                        string beginStr = "";
                        string endStr = "";
                        switch (item.ComboBox.SelectedIndex)
                        {
                            case 0: { beginStr = "<P>"; endStr = "</P>"; break; }
                            case 1: { beginStr = "<H1>"; endStr = "</H1>"; break; }
                            case 2: { beginStr = "<H2>"; endStr = "</H2>"; break; }
                            case 3: { beginStr = "<H3>"; endStr = "</H3>"; break; }
                            case 4: { beginStr = "<H4>"; endStr = "</H4>"; break; }
                            case 5: { beginStr = "<H5>"; endStr = "</H5>"; break; }
                            case 6: { beginStr = "<H6>"; endStr = "</H6>"; break; }
                        }
                        InsertElementHelper.AddToSelection(_idoc2, beginStr, endStr);
                        break;
                    }
                default:
                    break;
            }
        }


        void titleToolStripTextBox_Validated(object sender, EventArgs e)
        {
            //SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            //PageSimpleExXmlElement ele = doc.GetPageElementById(_htmlDesigner.PageId);
            //ele.Title = titleToolStripTextBox.Text;
            //doc.Save();

            //PageXmlDocument pageDoc = doc.GetPageDocumentById(_htmlDesigner.PageId);
            //pageDoc.Title = titleToolStripTextBox.Text;
            //pageDoc.Save();
        }

        public void InitMy()
        {
            designToolStripButton = new ToolStripButton();
            codeToolStripButton = new ToolStripButton();

            boldToolStripButton = new ToolStripButton();
            italicToolStripButton = new ToolStripButton();
            underlineToolStripButton = new ToolStripButton();

            leftToolStripButton = new ToolStripButton();
            rightToolStripButton = new ToolStripButton();
            allToolStripButton = new ToolStripButton();

            fontToolStripComboBox = new FontToolStripComboBox();
            sizeToolStripComboBox = new FontSizeToolStripComboBox();
            colorToolStripButton = new ColorToolStripButton();
            paraToolStripComboBox = new ParaToolStripComboBox();
            listToolStripButton = new ToolStripButton();
            numToolStripButton = new ToolStripButton();
            inToolStripButton = new ToolStripButton();
            outToolStripButton = new ToolStripButton();

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
            brToolStripMenuItem = new ToolStripMenuItem("", null, null, "br");
            nbspToolStripMenuItem = new ToolStripMenuItem("", null, null, "nbsp");
            L_quotationToolStripMenuItem = new ToolStripMenuItem("", null, null, "L_quotation");
            R_quotationToolStripMenuItem = new ToolStripMenuItem("", null, null, "R_quotation");
            deshToolStripMenuItem = new ToolStripMenuItem("", null, null, "desh");

            poundToolStripMenuItem = new ToolStripMenuItem("", null, null, "pound");
            EurocurrencyToolStripMenuItem = new ToolStripMenuItem("", null, null, "Eurocurrency");
            yenToolStripMenuItem = new ToolStripMenuItem("", null, null, "yen");

            copyrightToolStripMenuItem = new ToolStripMenuItem("", null, null, "copyright");
            reg_trad_markToolStripMenuItem = new ToolStripMenuItem("", null, null, "reg_trad_mark");
            brandToolStripMenuItem = new ToolStripMenuItem("", null, null, "brand");

            osignToolStripMenuItem = new ToolStripMenuItem("", null, null, "osign");

            leftToolStripButton.Name = "leftToolStripButton";
            rightToolStripButton.Name = "rightToolStripButton";
            allToolStripButton.Name = "allToolStripButton";
            leftToolStripButton.Text = "居左";
            rightToolStripButton.Text = "居右";
            allToolStripButton.Text = "居中";
            fontToolStripComboBox.Text = "字体";
            sizeToolStripComboBox.Text = "大小";
            fontToolStripComboBox.Name = "fontToolStripComboBox";
            sizeToolStripComboBox.Name = "sizeToolStripComboBox";
            paraToolStripComboBox.Name = "paraToolStripComboBox";
            listToolStripButton.Name = "listToolStripButton";
            numToolStripButton.Name = "numToolStripButton";
            listToolStripButton.Text ="列表";
            numToolStripButton.Text = "编号";
            inToolStripButton.Name = "inToolStripButton";
            outToolStripButton.Name = "outToolStripButton";
            inToolStripButton.Text = "缩进";
            outToolStripButton.Text = "突出";

            designToolStripButton.Name = "DesignToolStripButton";
            //designToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            designToolStripButton.Text = "设计";

            codeToolStripButton.Name = "CodeToolStripButton";
            //codeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            codeToolStripButton.Text="代码";

            boldToolStripButton.Name = "boldToolStripButton";
            boldToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            boldToolStripButton.Text = "粗体";

            italicToolStripButton.Name = "italicToolStripButton";
            italicToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            italicToolStripButton.Text = "斜体";

            underlineToolStripButton.Name = "underlineToolStripButton";
            underlineToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            underlineToolStripButton.Text = "下划线";

            tableToolStripButton.Name = "tableToolStripButton";
            //tableToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tableToolStripButton.Text = "表格";
            
            imageToolStripButton.Name = "imageToolStripButton";
            //imageToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imageToolStripButton.Text = "图片";

            flashToolStripButton.Name = "flashToolStripButton";
            //flashToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            flashToolStripButton.Text = "动画";

            audioToolStripButton.Name = "audioToolStripButton";
            audioToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            //audioToolStripButton.Text = "音频";

            mediaToolStripButton.Name = "mediaToolStripButton";
            //mediaToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mediaToolStripButton.Text = "媒体";

            emailToolStripButton.Name = "emailToolStripButton";
            //emailToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            emailToolStripButton.Text = "邮件";

            linkToolStripButton.Name = "linkToolStripButton";
            //linkToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            linkToolStripButton.Text = "链接";

            dateToolStripButton.Name = "dateToolStripButton";
            //dateToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            dateToolStripButton.Text = "日期";

            hrToolStripButton.Name = "hrToolStripButton";
            //hrToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            hrToolStripButton.Text = "直线";

            signToolStripButton.Name = "signToolStripButton";

            titleToolStripTextBox.Name = "titleToolStripTextBox";
            titleToolStripTextBox.Size = new Size(200, 25);
            titleToolStripLabel.Name = "titleToolStripLabel";

            propertyToolStripButton.Name = "propertyToolStripTextBox";
            propertyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;

            this.Name = "tsMain";

            //空格下拉工具按钮加入
            signToolStripDropDownButton = new ToolStripDropDownButton(brToolStripMenuItem.Text, null,
brToolStripMenuItem, nbspToolStripMenuItem,
L_quotationToolStripMenuItem, R_quotationToolStripMenuItem, deshToolStripMenuItem,
sp1,
poundToolStripMenuItem, EurocurrencyToolStripMenuItem, yenToolStripMenuItem,
sp2,
copyrightToolStripMenuItem, reg_trad_markToolStripMenuItem, brandToolStripMenuItem,
osignToolStripMenuItem);

            //整个工具栏的按钮加入
            this.Items.AddRange(new ToolStripItem[] { //需以属性方式加入，按钮，否则按钮的属性无法载入
                   designToolStripButton ,codeToolStripButton,paraToolStripComboBox,
            fontToolStripComboBox,sizeToolStripComboBox,colorToolStripButton,
                    sp3,
                    boldToolStripButton,italicToolStripButton,underlineToolStripButton,
                    leftToolStripButton,rightToolStripButton,allToolStripButton,
                    listToolStripButton,numToolStripButton,
                    inToolStripButton,outToolStripButton,
                    tableToolStripButton,imageToolStripButton,//colorButton,
                    flashToolStripButton,audioToolStripButton, mediaToolStripButton, 
                    sp4,
                    emailToolStripButton, linkToolStripButton,dateToolStripButton,hrToolStripButton,   
                    sp5, 
                    signToolStripButton,
                signToolStripDropDownButton,titleToolStripLabel,titleToolStripTextBox,propertyToolStripButton
                });
        }

        string currentSign = "";
        /// <summary>
        /// 插入符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void signToolStripDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            IHTMLDocument2 _idoc2 = _ownerHtmlDesigner.DesignWebBrowser.idoc2;
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

            signToolStripButton.Text = HttpUtility.HtmlDecode(currentSign);
            signToolStripDropDownButton.Text = "";
        }

        /// <summary>
        /// 插入HTML元素，窗口切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HTMLToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            idoc2 = _ownerHtmlDesigner.DesignWebBrowser.idoc2;
            ToolStripButton thisBtn = e.ClickedItem as ToolStripButton;
            if (thisBtn == null)
                return;
            switch (thisBtn.Name)
            {
                case "leftToolStripButton":
                    {
                        idoc2.execCommand("JustifyLeft", true, true);
                        break;
                    }
                case "rightToolStripButton":
                    {
                        idoc2.execCommand("JustifyRight", true, true);
                        break;
                    }
                case "allToolStripButton":
                    {
                        idoc2.execCommand("JustifyCenter", true, true);
                        break;
                    }
                case "boldToolStripButton":
                    {
                        idoc2.execCommand("bold", true, true);
                        break;
                    }
                case "inToolStripButton":
                    {
                        idoc2.execCommand("Indent", true, true);
                        break;
                    }
                case "outToolStripButton":
                    {
                        idoc2.execCommand("Outdent", true, true);
                        break;
                    }
                case "numToolStripButton":
                    {
                        idoc2.execCommand("InsertOrderedList", true, true);
                        break;
                    }
                case "listToolStripButton":
                    {
                        idoc2.execCommand("InsertUnorderedList", true, true);
                        break;
                    }
                case "italicToolStripButton":
                    {
                        idoc2.execCommand("italic", true, true);
                        break;
                    }
                case "underlineToolStripButton":
                    {
                        idoc2.execCommand("underline", true, true);
                        break;
                    }
                case "DesignToolStripButton":
                    {
                        if (OwnerHtmlDesigner.splitCon.Panel1Collapsed)
                        {
                            OwnerHtmlDesigner.splitCon.Panel2Collapsed = true;
                            OwnerHtmlDesigner.DesignWebBrowser.Document.Body.InnerHtml = OwnerHtmlDesigner.CodeRichText.Text;
                            designToolStripButton.Checked = true;
                            codeToolStripButton.Checked = false;
                        }
                        break;
                    }
                case "CodeToolStripButton":
                    {
                        if (OwnerHtmlDesigner.splitCon.Panel2Collapsed)
                        {
                            OwnerHtmlDesigner.splitCon.Panel1Collapsed = true;
                            OwnerHtmlDesigner.CodeRichText.Text =OwnerHtmlDesigner.DesignWebBrowser.Document.Body.InnerHtml ;
                            codeToolStripButton.Checked = true;
                            designToolStripButton.Checked = false;
                        }
                        break;
                    }

                case "signToolStripButton":
                    {
                        InsertElementHelper.AddToSelection(idoc2, currentSign, "");
                        break;
                    }
                case "propertyToolStripTextBox":
                    {
                        //string pageId = _htmlDesigner.PageId;
                        //PagePropertyForm pageTextPropertyForm = new PagePropertyForm(pageId);
                        //pageTextPropertyForm.ShowDialog(Service.Workbench.MainForm);
                        break;
                    }
                case "tableToolStripButton":
                    {
                        InsertElementHelper.Inserttable(idoc2);
                        break;
                    }

                case "linkToolStripButton":
                    {
                        InsertElementHelper.Insertlink(_ownerHtmlDesigner);
                        break;
                    }
                case "imageToolStripButton":
                    {
                        InsertElementHelper.InsertImage(_ownerHtmlDesigner);
                        break;
                    }
                case "flashToolStripButton":
                    {
                        InsertElementHelper.InsertFlash(_ownerHtmlDesigner);
                        break;
                    }
                case "mediaToolStripButton":
                    {
                        InsertElementHelper.InsertVideo(_ownerHtmlDesigner);
                        break;
                    }
                case "emailToolStripButton":
                    {
                        InsertElementHelper.InsertEmail(idoc2);
                        break;
                    }
                case "dateToolStripButton":
                    {
                        InsertElementHelper.InsertDateTime(idoc2);
                        break;
                    }
                case "brToolStripButton":
                    {
                        InsertElementHelper.AddToSelection(idoc2, "<BR />", "");
                        break;
                    }
                case "hrToolStripButton":
                    {
                        InsertElementHelper.AddToSelection(idoc2, "<HR />", "");
                        break;
                    }
                case "audioToolStripButton":
                    {
                        InsertElementHelper.InsertAudio(_ownerHtmlDesigner);
                        break;
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
        #endregion
    }
}