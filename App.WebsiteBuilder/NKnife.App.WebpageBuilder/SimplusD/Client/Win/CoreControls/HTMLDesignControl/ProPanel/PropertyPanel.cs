using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ICSharpCode.TextEditor;
using mshtml;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class PropertyPanels : Panel
    {
        imgUserControl imgU = new imgUserControl();
        tableUserControl tableU = new tableUserControl();
        textUserControl textU = new textUserControl();
        lineUserControl lineU = new lineUserControl();
        MediaUserControl mediaU = new MediaUserControl();
        List<UserControl> proControls = new List<UserControl>();

        HTMLEditHelper htmlEdit = new HTMLEditHelper();

        HTMLDesignControl HTMLDesign = null;
        WebBrowser designWB=null;
        TextEditorControl tec = null;
        IHTMLElement currentEle = null;

        private PropertyPanels() { }

        /// <summary>
        ///  因为是静态方法,所以相关初始化工作放到下面
        /// </summary>
        private static PropertyPanels _instance = null;
        public static PropertyPanels Instance(string HTMLType,HTMLDesignControl htmlDesignControl)
        {
            if (_instance == null)
                _instance = new PropertyPanels();
            _instance.InitPropertyPanel(HTMLType,htmlDesignControl);
            return _instance;
        }

        public void InitPropertyPanel(string HTMLType,HTMLDesignControl htmlDesign)
        {
            HTMLDesign = htmlDesign;
            designWB=htmlDesign.DesignWebBrowser;
            tec=htmlDesign.CodeTextEditorControl;
            currentEle = htmlDesign.CurrentElement;

            proControls.Add(textU);
            proControls.Add(imgU);
            proControls.Add(tableU);
            proControls.Add(lineU);
            proControls.Add(mediaU);

            _instance.Name = "propertyPanel";
            _instance.Dock = DockStyle.Fill;
            _instance.BringToFront();
            #region
            switch (HTMLType)
            {
                case "":
                case "H1":
                case "H2":
                case "H3":
                case "H4":
                case "H5":
                case "H6":
                case "PRE":
                    {
                        textU.TextFormat = HTMLType.ToLower();
                        break;
                    }
                case "SPAN":
                case "P":
                case "A":
                case "BODY":
                case "FONT":
                    {
                        if (HTMLType == "SPAN")
                        {
                            #region
                            mshtml.IHTMLStyle eleStyle = htmlDesign.PhraseBe.Element.style;
                            string fs = "";
                            if (eleStyle.fontSize!=null)
                            {
                                fs = eleStyle.fontSize.ToString();
                            }
                            string sizenum = "";
                            string sizeunt = "";
                            try
                            {
                                 sizenum = fs.Substring(0, fs.Length - 2);
                                 int num = Convert.ToInt32(sizenum);
                                 sizeunt = fs.Substring(2, fs.Length - 2);
                            }
                            catch
                            {
                                sizenum = fs;
                                sizeunt = "";
                            }
                            finally
                            {
                                textU.TextSize =sizenum;
                                textU.TextSizeUnit=sizeunt;
                            }
                            if (eleStyle.fontFamily != null)
                                textU.TextFont = eleStyle.fontFamily;
                            else
                                textU.TextFont = "默认字体";
                            if (eleStyle.color != null)
                                textU.TextColor = eleStyle.color.ToString();
                            else
                                textU.TextColor = "#000000";
                            if (eleStyle.fontWeight != null)
                                textU.Bold = true;
                            else
                                textU.Bold = false;
                            textU.TextTarget = "";
                            #endregion
                        }
                        else if (HTMLType == "FONT")
                        {
                            #region
                            string face ="";
                            string color ="";
                            string size ="";
                            
                            HTMLFontElementClass paretEle=htmlDesign.CurrentElement as HTMLFontElementClass;
                            while (paretEle != null && (string.IsNullOrEmpty(face) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(size)))
                            {
                                if (string.IsNullOrEmpty(face) && paretEle.face!=null) face = paretEle.face;
                                if (string.IsNullOrEmpty(color) && paretEle.color!=null) color = paretEle.color.ToString();
                                if (string.IsNullOrEmpty(size) && paretEle.size!=null) size = paretEle.size.ToString();
                                paretEle = paretEle.parentElement as HTMLFontElementClass;
                            }

                           // HTMLFontElementClass ele = htmlDesign.CurrentElement.parentElement is HTMLFontElementClass;

                            //face = (HTMLDesign.FontBe.Element.face != null) ? HTMLDesign.FontBe.Element.face.ToString() : "默认字体";
                            //color = (htmlDesign.FontBe.Element.color != null) ? htmlDesign.FontBe.Element.color.ToString() : "#000000";
                            // size = (htmlDesign.FontBe.Element.size != null) ? htmlDesign.FontBe.Element.size.ToString() : "1";

                            textU.TextFont =string.IsNullOrEmpty(face)?"默认字体":face;
                            textU.TextSize = string.IsNullOrEmpty(size) ? "" : size;
                            textU.TextColor = string.IsNullOrEmpty(color) ? "#000000" : color;
                            #endregion
                        }
                        else if ((HTMLType == "A"))
                        {
                            #region
                            if (HTMLDesign.LinkBe != null)
                            {
                                HTMLAnchorElementClass linkEle = htmlDesign.CurrentElement as HTMLAnchorElementClass;
                                string strLink = linkEle.href;
                                textU.TextLink = strLink.Replace("about:", "");
                                textU.TextTarget = linkEle.target;
                            }

                            string face = "";
                            string color = "";
                            string size = "";

                            HTMLFontElementClass paretEle = htmlDesign.CurrentElement.parentElement as HTMLFontElementClass;
                            while (paretEle != null && (string.IsNullOrEmpty(face) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(size)))
                            {
                                if (string.IsNullOrEmpty(face) && paretEle.face != null) face = paretEle.face;
                                if (string.IsNullOrEmpty(color) && paretEle.color != null) color = paretEle.color.ToString();
                                if (string.IsNullOrEmpty(size) && paretEle.size != null) size = paretEle.size.ToString();
                                paretEle = paretEle.parentElement as HTMLFontElementClass;
                            }

                            textU.TextFont = string.IsNullOrEmpty(face) ? "默认字体" : face;
                            textU.TextSize = string.IsNullOrEmpty(size) ? "" : size;
                            textU.TextColor = string.IsNullOrEmpty(color) ? "#000000" : color;
                            #endregion
                        }
                        else
                        {
                            #region
                            {
                                textU.TextFormat = "";
                                textU.TextFont = "默认字体";
                                textU.TextStyle = "";
                                textU.TextSize = "";
                                textU.TextSizeUnit = "";
                                textU.TextColor = "#000000";
                                textU.TextTarget = "";
                                textU.TextLink = "";
                                textU.TextTarget = "";
                            }
                            #endregion
                        }


                        SetForTableCell(false);
                        BindingEventHandler("text");
                        ChangePropertyPanel(textU);
                    } break;
                case "TD":
                    {
                        textU.CellHalign =htmlDesign.TableCellBe.Element.align;
                        textU.CellValign = htmlDesign.TableCellBe.Element.vAlign;
                        textU.CellWidth = (htmlDesign.TableCellBe.Element.width != null) ? (htmlDesign.TableCellBe.Element.width.ToString()) : ("");
                        textU.CellHeight = (htmlDesign.TableCellBe.Element.height != null) ? (htmlDesign.TableCellBe.Element.height.ToString()) : ("");
                        textU.Cellbgpic = htmlDesign.TableCellBe.Element.background;
                        textU.Cellbgcolor = (htmlDesign.TableCellBe.Element.bgColor != null) ? (GeneralMethods.GetColor(htmlDesign.TableCellBe.Element.bgColor.ToString().Substring(1))) : (Color.White);
                        textU.Cellbordercolor = (htmlDesign.TableCellBe.Element.borderColor != null) ? (GeneralMethods.GetColor(htmlDesign.TableCellBe.Element.borderColor.ToString().Substring(1))) : (Color.White);
                        
                        SetForTableCell(true);
                        BindingEventHandler("tablecell");
                        ChangePropertyPanel(textU);
                    } break;
                case "TABLE":
                    {
                        IHTMLTable table = null;
                        if (HTMLType == "TD")
                            table = htmlEdit.GetParentTable(currentEle);
                        else
                            table = (IHTMLTable)HTMLDesign.CurrentElement;//.parentElement.parentElement.parentElement as IHTMLTable;
                        tableU.TableRowNum = htmlEdit.GetRowCount(table).ToString();
                        tableU.TableColNum = htmlEdit.GetColCount(table, Convert.ToInt32(tableU.TableRowNum) - 1).ToString();
                        tableU.TableWidth = table.width.ToString();
                        tableU.TableBorder = table.border.ToString();
                        tableU.TableFill = table.cellPadding.ToString();
                        tableU.TableSpace = table.cellPadding.ToString();
                        tableU.TableAlign = table.align;
                        tableU.TableBgPic =htmlDesign.TableBe.Element.background;
                        if (table.bgColor == null)
                            tableU.TableBgColor = Color.White;
                        else
                            tableU.TableBgColor = GeneralMethods.GetColor(table.bgColor.ToString().Substring(1, 6));
                        if (table.borderColor == null)
                            tableU.TableBorderColor = Color.White;
                        else
                            tableU.TableBorderColor = GeneralMethods.GetColor(table.borderColor.ToString().Substring(1, 6));
                        
                        BindingEventHandler("table");
                        ChangePropertyPanel(tableU);
                    } break;
                case "IMG":
                    {
                        XmlDocument doc = GetDocByHTML(htmlDesign.CurrentElement.outerHTML);
                        XmlElement imgEle = doc.DocumentElement;
                        string styleStr = imgEle.GetAttribute("style");
                        CssSection section = CssSection.Parse(styleStr);
                        string width = section.Properties["width"];
                        string height = section.Properties["height"];
                        if (width.IndexOf("%") > 0)
                        {
                            imgU.ImgWidth = width.Substring(0, width.IndexOf("%"));
                            imgU.ImgWidthUnit = "1";

                        }
                        else
                        {
                            imgU.ImgWidth = width.Substring(0, width.IndexOf("px"));
                            imgU.ImgWidthUnit = "0";
                        }
                        if (height.IndexOf("%") > 0)
                        {
                            imgU.ImgHeight = height.Substring(0, height.IndexOf("%"));
                            imgU.ImgHeightUnit = "1";
                        }
                        else
                        {
                            imgU.ImgHeight = height.Substring(0, height.IndexOf("px"));
                            imgU.ImgHeightUnit = "0";
                        }
                        string fullFilePath=htmlDesign.ImageBe.Element.src;
                        imgU.ImgURL = fullFilePath.Substring(fullFilePath.IndexOf("Root"));
                        imgU.ImgVspace = htmlDesign.ImageBe.Element.vspace.ToString();
                        imgU.ImgHspace = htmlDesign.ImageBe.Element.hspace.ToString();
                        imgU.ImgBorderWidth = htmlDesign.ImageBe.Element.border.ToString();
                        imgU.ImgAlign = htmlDesign.ImageBe.Element.align;
                        string fullLinkPath = htmlDesign.ImageBe.Element.href;
                        if (!string.IsNullOrEmpty(fullLinkPath))
                            imgU.ImgLinkURL = fullLinkPath.Substring(fullLinkPath.IndexOf("Root"));
                        else
                            imgU.ImgLinkURL = "";

                        BindingEventHandler("image");
                        ChangePropertyPanel(imgU);
                    } break;
                case "HR":
                    {
                        if (htmlDesign.LineBe.Element.width != null)
                        {
                            string lineW = htmlDesign.LineBe.Element.width.ToString();
                            if (lineW == "")
                            {
                                lineU.LineWidth = designWB.Width.ToString();
                                lineU.LineWidthUnit = "0";
                            }
                        }
                        else
                        {
                            lineU.LineWidth = designWB.Width.ToString();
                            lineU.LineWidthUnit = "0";
                        }

                        lineU.LineHeight = ((htmlDesign.LineBe.Element.size == null) ? "" : htmlDesign.LineBe.Element.size.ToString());

                        lineU.LineAlign = htmlDesign.LineBe.Element.align;

                        BindingEventHandler("line");
                        ChangePropertyPanel(lineU);
                    }
                    break;

                case "object":
                    {
                        XmlDocument doc = GetDocByHTML(HTMLDesign.CurrentElement.outerHTML);
                        XmlElement objectEle = doc.DocumentElement;
                        XmlElement embedEle = objectEle.GetElementsByTagName("embed").Item(0) as XmlElement;
                        XmlElement pathEle = GetElementByNameValue(objectEle, "SRC");
                        if (pathEle==null)
                            pathEle = GetElementByNameValue(objectEle, "URL");
                        XmlElement autoEle = GetElementByNameValue(objectEle, "AUTOSTART");
                        XmlElement loopEle = GetElementByNameValue(objectEle, "LOOP");
                        XmlElement qualityEle = GetElementByNameValue(objectEle, "quality");
                        XmlElement scaleEle = GetElementByNameValue(objectEle, "scale");
                        string styleStr = objectEle.GetAttribute("style");

                        CssSection section = CssSection.Parse(styleStr);
                        string fwidth=section.Properties["width"];
                        string fheight = section.Properties["height"];
                        if (string.IsNullOrEmpty(fwidth))
                        {
                            fwidth = objectEle.GetAttribute("width");
                        }
                        if (string.IsNullOrEmpty(fheight))
                        {
                            fheight = objectEle.GetAttribute("height");
                        }

                        string path = "";
                        if (pathEle != null)
                        {
                            path = pathEle.GetAttribute("value");
                            mediaU.MediaPath = path.Substring(path.IndexOf("Root"));
                        }
                        if (autoEle != null)
                        {
                            mediaU.MediaAutoplay = (autoEle.GetAttribute("value") != "0");
                        }
                        if (loopEle != null)
                        {
                            mediaU.MediaLoop = (loopEle.GetAttribute("value") != "0");
                        }
                        mediaU.MediaHspace = objectEle.GetAttribute("hspace");// htmlDesign.FlashBe.Element.hspace.ToString();
                        mediaU.MediaVspace = objectEle.GetAttribute("vspace");//htmlDesign.FlashBe.Element.vspace.ToString();
                        mediaU.MediaAlign = objectEle.GetAttribute("align");//htmlDesign.FlashBe.Element.align;
                        if (qualityEle != null) mediaU.MediaQuality = qualityEle.GetAttribute("value");
                        else if (embedEle != null) mediaU.MediaQuality = embedEle.GetAttribute("quality");
                        else mediaU.MediaQuality = "";
                        if (scaleEle != null) mediaU.MediaScale = scaleEle.GetAttribute("value");
                        else if (embedEle != null) mediaU.MediaScale = embedEle.GetAttribute("scale");
                        else mediaU.MediaScale = "";
                       if (fwidth.IndexOf('%') == -1)
                        {
                            if (fwidth.IndexOf("px") > 0)
                                mediaU.MediaWidth = fwidth.Substring(0, fwidth.IndexOf("px"));
                            else
                                mediaU.MediaWidth = fwidth;
                            mediaU.MediaWidthUnit = "0";
                        }
                        else
                        {
                            mediaU.MediaWidth = fwidth.Substring(0, fwidth.Length - 1);
                            mediaU.MediaWidthUnit = "1";
                        }
                        if (fheight.IndexOf("%") == -1)
                        {
                            if (fheight.IndexOf("px") > 0)
                                mediaU.MediaHeight = fheight.Substring(0, fheight.IndexOf("px"));
                            else
                                mediaU.MediaHeight = fheight;
                            mediaU.MediaHeightUnit = "0";
                        }
                        else
                        {
                            mediaU.MediaHeight = fheight.Substring(0, fheight.Length - 1);
                            mediaU.MediaHeightUnit = "1";
                        }
                        BindingEventHandler("media");
                        ChangePropertyPanel(mediaU);

                    }
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 给属性面板绑定事件处理
        /// </summary>
        /// <param name="currentCtl"></param>
        void BindingEventHandler(string type)
        {
            switch (type)
            {
                case "text":
                    {
                        #region 事件
                        foreach (Control Con in textU.Controls)
                        {
                            string contype = Con.GetType().Name;
                            switch (contype)
                            {
                                case "TextBox":
                                    ((TextBox)Con).KeyPress += new KeyPressEventHandler(HTMLDesign_KeyPress);
                                    break;
                                case "Button":
                                    {
                                        ((Button)Con).Click -= new EventHandler(HTMLDesign_Click);
                                        ((Button)Con).Click += new EventHandler(HTMLDesign_Click);
                                        break;
                                    }
                                case "ComboBox":
                                    {
                                        ((ComboBox)Con).DropDown += new EventHandler(HTMLDesign_DropDown);
                                        ((ComboBox)Con).SelectedIndexChanged -= new EventHandler(HTMLDesign_SelectedIndexChanged);
                                        ((ComboBox)Con).SelectedIndexChanged += new EventHandler(HTMLDesign_SelectedIndexChanged);
                                        ((ComboBox)Con).KeyPress +=new KeyPressEventHandler(HTMLDesign_KeyPress);
                                        break;
                                    }
                                case "ColorGeneralButton":
                                    {
                                        ((ColorGeneralButton)Con).MyColorChanged -= new EventHandler(HTMLDesign_MyColorChanged);
                                        ((ColorGeneralButton)Con).MyColorChanged += new EventHandler(HTMLDesign_MyColorChanged);
                                        break;
                                    }
                            }
                        }
                        #endregion
                        break;
                    }
                case "media":
                    {
                        #region 事件
                        foreach (Control Con in mediaU.Controls)
                        {
                            string contype = Con.GetType().Name;
                            switch (contype)
                            {
                                case "TextBox":
                                    {
                                        ((TextBox)Con).Validated -= new EventHandler(MediaTextBox_Validated);
                                        ((TextBox)Con).KeyPress -= new KeyPressEventHandler(MediaTextBox_KeyPress);
                                        ((TextBox)Con).Validated += new EventHandler(MediaTextBox_Validated);
                                        ((TextBox)Con).KeyPress += new KeyPressEventHandler(MediaTextBox_KeyPress);
                                        break;
                                    }
                                case "ValidateTextBox":
                                    {
                                        ((ValidateTextBox)Con).Validated -= new EventHandler(MediaTextBox_Validated);
                                        ((ValidateTextBox)Con).KeyPress -= new KeyPressEventHandler(MediaTextBox_KeyPress);
                                        ((ValidateTextBox)Con).Validated += new EventHandler(MediaTextBox_Validated);
                                        ((ValidateTextBox)Con).KeyPress += new KeyPressEventHandler(MediaTextBox_KeyPress);
                                        break;
                                    }

                                case "CheckBox":
                                    {
                                        ((CheckBox)Con).CheckedChanged -= new EventHandler(MediaCheckBox_CheckedChanged);
                                        ((CheckBox)Con).CheckedChanged += new EventHandler(MediaCheckBox_CheckedChanged);
                                        break;
                                    }
                                case "Button":
                                    {
                                        ((Button)Con).Click -= new EventHandler(MediaButton_click);
                                        ((Button)Con).Click += new EventHandler(MediaButton_click);
                                        break;
                                    }
                                case "ComboBox":
                                    {
                                        ((ComboBox)Con).SelectedIndexChanged -= new EventHandler(MediaComboBox_SelectedIndexChaged);
                                        ((ComboBox)Con).SelectedIndexChanged += new EventHandler(MediaComboBox_SelectedIndexChaged);
                                        break;
                                    }
                                case "ColorGeneralButton":
                                    {
                                        ((ColorGeneralButton)Con).MyColorChanged -= new EventHandler(MediaMyColorButton_MyColorChanged);
                                        ((ColorGeneralButton)Con).MyColorChanged += new EventHandler(MediaMyColorButton_MyColorChanged);
                                        break;
                                    }
                            }
                        }
                        #endregion
                        break;
                    }
                case "line":
                    {
                        #region 事件
                        foreach (Control Con in lineU.Controls)
                        {
                            string contype = Con.GetType().Name;
                            switch (contype)
                            {
                                case "TextBox":
                                    {
                                        ((TextBox)Con).Validated -= new EventHandler(LineText_Validated);
                                        ((TextBox)Con).KeyPress -= new KeyPressEventHandler(linePanel_KeyPress);
                                        ((TextBox)Con).Validated += new EventHandler(LineText_Validated);
                                        ((TextBox)Con).KeyPress += new KeyPressEventHandler(linePanel_KeyPress);
                                        break;
                                    }
                                case "ValidateTextBox":
                                    {
                                        ((ValidateTextBox)Con).Validated -= new EventHandler(LineText_Validated);
                                        ((ValidateTextBox)Con).KeyPress -= new KeyPressEventHandler(linePanel_KeyPress);
                                        ((ValidateTextBox)Con).Validated += new EventHandler(LineText_Validated);
                                        ((ValidateTextBox)Con).KeyPress += new KeyPressEventHandler(linePanel_KeyPress);
                                        break;
                                    }
                                case "ComboBox":
                                    {
                                        ((ComboBox)Con).SelectedIndexChanged -= new EventHandler(linePanel_SelectedIndexChanged);
                                        ((ComboBox)Con).SelectedIndexChanged += new EventHandler(linePanel_SelectedIndexChanged);
                                        break;
                                    }
                                case "CheckBox":
                                    {
                                        ((CheckBox)Con).CheckedChanged -= new EventHandler(linePanel_CheckedChanged);
                                        ((CheckBox)Con).CheckedChanged += new EventHandler(linePanel_CheckedChanged);
                                        break;
                                    }
                            }
                        }
                        #endregion 
                        break;
                    }
                case "image":
                    {
                        #region 事件
                        foreach (Control Con in imgU.Controls)
                        {
                            string contype = Con.GetType().Name;
                            switch (contype)
                            {
                                case "TextBox":
                                    {
                                        ((TextBox)Con).Validated -= new EventHandler(imgTextBox_Validated);
                                        ((TextBox)Con).KeyPress -= new KeyPressEventHandler(imgtextBox_KeyPress);
                                        ((TextBox)Con).Validated += new EventHandler(imgTextBox_Validated);
                                        ((TextBox)Con).KeyPress += new KeyPressEventHandler(imgtextBox_KeyPress);
                                        
                                        break;
                                    }
                                case "ValidateTextBox":
                                    {
                                        ((ValidateTextBox)Con).Validated -= new EventHandler(imgTextBox_Validated);
                                        ((ValidateTextBox)Con).KeyPress -= new KeyPressEventHandler(imgtextBox_KeyPress);
                                        ((ValidateTextBox)Con).Validated += new EventHandler(imgTextBox_Validated);
                                        ((ValidateTextBox)Con).KeyPress += new KeyPressEventHandler(imgtextBox_KeyPress);
                                        
                                        break;
                                    }
                                case "Button":
                                    {
                                        ((Button)Con).Click -= new EventHandler(imgPanelButton_Click);
                                        ((Button)Con).Click += new EventHandler(imgPanelButton_Click);
                                    }
                                    break;
                                case "ComboBox":
                                    {
                                        ((ComboBox)Con).SelectedIndexChanged -= new EventHandler(imgComboBox_SelectedIndexChanged);
                                        ((ComboBox)Con).SelectedIndexChanged += new EventHandler(imgComboBox_SelectedIndexChanged);
                                        break;
                                    }
                            }
                        }
                        #endregion
                        break;
                    }
                case "table":
                    {
                        #region 事件
                        foreach (Control Con in tableU.Controls)
                        {
                            string contype = Con.GetType().Name;
                            switch (contype)
                            {
                                case "TextBox":
                                    {
                                        ((TextBox)Con).Validated -= new EventHandler(tbtextBox_Validated);
                                        ((TextBox)Con).Validated += new EventHandler(tbtextBox_Validated);
                                        ((TextBox)Con).KeyPress -= new KeyPressEventHandler(tbtextBox_KeyPress);
                                        ((TextBox)Con).KeyPress += new KeyPressEventHandler(tbtextBox_KeyPress);
                                        break;
                                    }
                                case "ValidateTextBox":
                                    {
                                        ((ValidateTextBox)Con).Validated += new EventHandler(tbtextBox_Validated);
                                        ((ValidateTextBox)Con).KeyPress += new KeyPressEventHandler(tbtextBox_KeyPress);
                                        break;
                                    }
                                case "Button":
                                    {
                                        ((Button)Con).Click -= new EventHandler(tbbutton_click);
                                        ((Button)Con).Click += new EventHandler(tbbutton_click);
                                        break;
                                    }
                                case "ComboBox":
                                    {
                                        ((ComboBox)Con).SelectedIndexChanged += new EventHandler(tbcomboBox_SelectedIndexChaged);
                                    } break;
                                case "ColorGeneralButton":
                                    {
                                        ((ColorGeneralButton)Con).MyColorChanged += new EventHandler(tbMyColorButton_MyColorChanged);
                                    } break;
                            }
                        }
                        #endregion
                        break;
                    }
                case "tablecell":
                    {
                        #region 事件
                        foreach (Control Con in textU.Controls)
                        {
                            string contype = Con.GetType().Name;
                            switch (contype)
                            {
                                case "TextBox":
                                    {
                                        ((TextBox)Con).Validated += new EventHandler(tdTextBox_Validated);
                                        ((TextBox)Con).KeyPress += new KeyPressEventHandler(tdtextBox_KeyPress);
                                        break;
                                    }
                                case "ValidateTextBox":
                                    {
                                        ((ValidateTextBox)Con).Validated += new EventHandler(tdTextBox_Validated);
                                        ((ValidateTextBox)Con).KeyPress += new KeyPressEventHandler(tdtextBox_KeyPress);
                                        break;
                                    }
                                case "Button":
                                    {
                                        ((Button)Con).Click -= new EventHandler(tdbutton_click);//撤销前次
                                        ((Button)Con).Click += new EventHandler(tdbutton_click);
                                    }
                                    break;
                                case "ComboBox":
                                    {
                                        ((ComboBox)Con).SelectedIndexChanged += new EventHandler(tdcomboBox_SelectedIndexChaged);

                                    } break;
                                case "ColorGeneralButton":
                                    {
                                        ((ColorGeneralButton)Con).MyColorChanged += new EventHandler(tdMyColorButton_MyColorChanged);
                                    } break;
                            }
                        }
                        #endregion
                        break;
                    }
                default:
                    break;
            }
        }

        //加入需要的属性面板
        void ChangePropertyPanel(UserControl currentCtl)
        {
            foreach (UserControl uc in proControls)
            {
                if (uc != currentCtl)
                    _instance.Controls.Remove(uc);
            }
            _instance.Controls.Add(currentCtl);
            currentCtl.Dock = DockStyle.Fill;
            currentCtl.BringToFront();
        }

        XmlElement GetElementByNameValue(XmlElement parentEle,string name)
        {
            foreach (XmlElement ele in parentEle.ChildNodes)
            {
                if (ele.GetAttribute("name").ToLower() == name.ToLower())
                    return ele;
            }
            return null;
        }

        /// <summary>
        /// 将元素对应的HTML转化为XMLDocment
        /// </summary>
        /// <param name="HTMLStr"></param>
        /// <returns></returns>
        XmlDocument GetDocByHTML(string HTMLStr)
        {
            string xmlStr = "<body>" + HTMLStr + "</body>";
            xmlStr = GeneralMethods.tidy(xmlStr);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            return doc;
        }

        string _preFont = "";
        void HTMLDesign_DropDown(object sender, EventArgs e)
        {
            ComboBox mycombox = sender as ComboBox;
            switch (mycombox.Name)
            {
                case "fontcomboBox": _preFont = mycombox.Text; break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        void SetForTableCell(bool b)
        {
            foreach (Control con in textU.Controls)
            {
                if (object.Equals(con.Tag,"1"))
                    con.Visible=b;
            }
        }
        
        #region 事件
        #region 线属性面板事件
        void linePanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && mytextBox.Text.Trim() != "")
            {
                lineTextSet(mytextBox);
                tec.Text = GeneralMethods.tidy(designWB.Document.Body.InnerHtml);
            }
        }

        void LineText_Validated(object sender, EventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            lineTextSet(mytextBox);
        }

        void lineTextSet(TextBox mytextBox)
        {
            switch (mytextBox.Name)
            {
                case "widthtextBox":
                    {
                        string lineW = (((ComboBox)lineU.Controls["widthUnitcomboBox"]).SelectedIndex < 1) ? lineU.LineWidth : Convert.ToString(Convert.ToInt32(lineU.LineWidth) * designWB.Width / 100);
                        HTMLDesign.LineBe.Element.width = lineW;
                    }
                    break;
                case "heighttextBox":
                    {
                        HTMLDesign.LineBe.Element.size = mytextBox.Text;
                        break;
                    }
            }
        }

        void linePanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox myComboBox = sender as ComboBox;
            switch (myComboBox.Name)
            {
                case "AligncomboBox":
                    {
                        if (myComboBox.SelectedIndex > -1)
                        {
                            switch (myComboBox.SelectedIndex)
                            {
                                case 0: HTMLDesign.LineBe.Element.align = null;  break;//"left";
                                case 1: HTMLDesign.LineBe.Element.align = "left"; break;
                                case 2: HTMLDesign.LineBe.Element.align = "center"; break;
                                case 3: HTMLDesign.LineBe.Element.align = "right"; break;
                            }
                        }
                    } break;
                case "widthUnitcomboBox":
                    {
                        if (myComboBox.SelectedIndex > -1)
                        {
                            switch (myComboBox.SelectedIndex)
                            {
                                case 0: HTMLDesign.LineBe.Element.width = lineU.Controls["widthtextBox"].Text; break;
                                case 1: HTMLDesign.LineBe.Element.width = lineU.Controls["widthtextBox"].Text + "%"; break;
                            }
                        }
                    } break;
            }
            IHTMLDocument2 idoc2 = (IHTMLDocument2)designWB.Document.DomDocument;
            tec.Text = GeneralMethods.tidy(idoc2.body.innerHTML);
        }

        void linePanel_CheckedChanged(object sender, EventArgs e)
        {
            HTMLDesign.LineBe.Element.noShade = ((CheckBox)sender).Checked;
            IHTMLDocument2 idoc2 = (IHTMLDocument2)designWB.Document.DomDocument;
            tec.Text = GeneralMethods.tidy(idoc2.body.innerHTML);
        }

        #endregion

        #region 默认属性面板事件
        void HTMLDesign_KeyPress(object sender, KeyPressEventArgs e)
        {

            Control mytextBox = sender as Control;
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && mytextBox.Text.Trim() != "")
            {
                IHTMLDocument2 idoc2 = (IHTMLDocument2)designWB.Document.DomDocument;
                switch (mytextBox.Name)
                {
                    case "colortextBox":
                        {
                            textU.TextColor = mytextBox.Text; break;
                        }
                    case "linkcomboBox":
                        {
                            if (mytextBox.Text != string.Empty)
                            {
 
                            }
                            break;
                        }
                }
            }
        }

        void HTMLDesign_Click(object sender, EventArgs e)
        {
            Button mybutton = sender as Button;
            IHTMLDocument2 idoc2 = (IHTMLDocument2)designWB.Document.DomDocument;
            InsertElementHelper insert = new InsertElementHelper();
            switch (mybutton.Name)
            {
                case "fontButton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_FONT, idoc2);
                        break;
                    }
                case "underButton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_UNDERLINE, idoc2);
                        break;
                    }
                case "outButton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_OUTDENT, idoc2);
                        //InsertElementHelper.AddToSelection(idoc2, "<blockquote>", "</blockquote>");
                        break;
                    }
                case "inButton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_INDENT, idoc2);
                        //InsertElementHelper.AddToSelection(idoc2, "</blockquote>", "<blockquote>");
                        break;
                    }
                case "leftbutton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYLEFT, idoc2);
                       // idoc2.execCommand("JustifyLeft", true, true);
                        break;
                    } 
                case "rightbutton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYRIGHT, idoc2);
                        
                       // idoc2.execCommand("JustifyRight", true, true);
                    } break;
                case "middlebutton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYCENTER, idoc2);
                        
                       // idoc2.execCommand("JustifyCenter", true, true);
                        break;
                    } 
                case "bothbutton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYFULL, idoc2);
                        
                        //idoc2.execCommand("JustifyFull", true, true);
                        break;
                    } 
                case "Boldbutton":
                    {
                        if (mybutton.BackColor != Color.AliceBlue)
                        {
                            
                            insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_BOLD, idoc2);

 
                            //idoc2.execCommand("Bold", true, true);
                           // mybutton.BackColor = Color.AliceBlue;
                        }
                       /* else
                        {
                            idoc2.execCommand("Bold", true, false);
                            mybutton.BackColor = Color.Empty;
                        }*/
                      /*  if (pb== null)
                        {
                            if (mybutton.BackColor != Color.AliceBlue)
                            {
                                InsertElementHelper.AddToSelection(idoc2, "<SPAN STYLE='FONT-WEIGHT:BOLD;'>", "</SPAN>");
                                mybutton.BackColor = Color.AliceBlue;
                            }
                            else
                            {
                                if (HTMLDesign.PhraseBe != null)
                                {
                                    string str = HTMLDesign.PhraseBe.Element.outerHTML.Replace("FONT-WEIGHT: bold", "FONT-WEIGHT:;");
                                    HTMLDesign.PhraseBe.Element.outerHTML = str;
                                    mybutton.BackColor = Color.Empty;
                                }
                            }
                        }

                         tec.Text = GeneralMethods.tidy(idoc2.body.innerHTML);
                          idoc2.execCommand("Bold", true, true);*/
                    } break;
                case "Italicbutton":
                    {
                        if (mybutton.BackColor != Color.AliceBlue)
                        {
                            insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_ITALIC, idoc2); 
                           
                           /* idoc2.execCommand("Italic", true, true);
                            mybutton.BackColor = Color.AliceBlue;
                        }
                        else
                        {
                            idoc2.execCommand("Italic", true, false);
                            mybutton.BackColor = Color.Empty;*/
                        }
                    } break;
                case "listButton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_UNORDERLIST, idoc2);
                        
                       //InsertElementHelper.AddToSelection(idoc2, "<ul><li>", "</li></ul>");
                    } break;
                case "numButton":
                    {
                        insert.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_ORDERLIST,idoc2);
                       
                        //InsertElementHelper.AddToSelection(idoc2, "<ol><li>", "</li><ol>");
                    } break;
                case "linkButton":
                    {
                        OpenFileDialog op = new OpenFileDialog();
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            string filename = op.FileName;
                            if (HTMLDesign.LinkBe != null)
                                HTMLDesign.LinkBe.Element.href = filename;
                            textU.TextLink = filename;
                        }
                        break;
                    }
            }
        }

        void HTMLDesign_MyColorChanged(object sender, EventArgs e)
        {
            ColorGeneralButton mycolor = sender as ColorGeneralButton;
            if (!(designWB.Focused || tec.Focused))
            {
                IHTMLDocument2 idoc2 = (IHTMLDocument2)designWB.Document.DomDocument;
                switch (mycolor.Name)
                {
                    case "FontColorButton":
                        {
                            InsertElementHelper insert = new InsertElementHelper();
                            insert.ExecCommand(true, "ForeColor", false, GeneralMethods.ColorToRGB(((ColorGeneralButton)sender).MyColor), idoc2);
                            ((TextBox)textU.Controls["colortextBox"]).Text = GeneralMethods.ColorToRGB(((ColorGeneralButton)sender).MyColor);
                            
                            break;
                           /* string str = "color:" + "#" + mycolor.MyColor.ToArgb().ToString("x").Remove(0, 2);
                            InsertElementHelper.AddToSelection(idoc2, str, "</span>");
                            break;*/
                        }
                }
            }
        }

        void HTMLDesign_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox myComboBox = sender as ComboBox;
            IHTMLDocument2 idoc2 = (IHTMLDocument2)designWB.Document.DomDocument;
            InsertElementHelper insert = new InsertElementHelper();
            IHTMLTxtRange searchRange = (IHTMLTxtRange)idoc2.selection.createRange();

            string innerText = searchRange.htmlText;//获取文本的html代码
            if (myComboBox.Name == "fontcomboBox" && myComboBox.SelectedIndex == myComboBox.Items.Count - 1)
            {
                FontListEditorForm fontList = new FontListEditorForm();
                if (fontList.ShowDialog() == DialogResult.OK)
                {
                    myComboBox.Items.Clear();
                    string defstr = ResourceService.GetResourceText("TextpropertyPanel.fontname.default");
                    string editfontstr = ResourceService.GetResourceText("TextpropertyPanel.fontname.editfontlist");
                    myComboBox.Items.Add(defstr);

                    List<string> fontlist = fontList.listStr;
                    foreach (string font in fontlist)
                    {
                        myComboBox.Items.Add(font);
                    }
                    //myComboBox.Items.AddRange(fontlist);
                    myComboBox.Items.Add(editfontstr);
                }
                //myComboBox.Text = _preFont;
            }
            if (!string.IsNullOrEmpty(innerText))
            {
                if (myComboBox.Focused)
                {
                    switch (myComboBox.Name)
                    {
                        case "fontcomboBox":
                            {
                                string resstr = myComboBox.Text;
                                if (myComboBox.SelectedIndex > -1 && myComboBox.SelectedIndex < myComboBox.Items.Count - 1)
                                {
                                    if (idoc2.queryCommandEnabled("Copy"))
                                    {
                                        string str = "font-family:" + myComboBox.Text;
                                        //InsertElementHelper.AddToSelection(idoc2, str, "</span>");
                                        if (!string.IsNullOrEmpty(innerText))
                                            insert.ExecCommand(true, "FontName", false, myComboBox.Text, idoc2);
                                    }
                                }
                                break;
                            }
                        case "sizecomboBox":
                            {
                                if (myComboBox.SelectedIndex > -1)
                                {
                                    if (!string.IsNullOrEmpty(innerText))
                                        insert.ExecCommand(true, "FontSize", true, myComboBox.Text, idoc2);

                                    /* else
                                     {
                                         string sizestr = textU.TextSize;
                                         int sizeu = 0;
                                         try
                                         {
                                             sizeu = Convert.ToInt32(sizestr);
                                         }
                                         catch
                                         {
                                             sizeu = 0;
                                         }
                                         finally
                                         {
                                             string sizeunitstr = textU.TextSizeUnit;
                                             if (sizeu > 8 && sizeu < 40)
                                             {

                                             }
                                             string fontsize = "";
                                             if (sizeu == 0)
                                                 fontsize = sizestr;
                                             else
                                                 fontsize = sizestr + sizeunitstr;
                                             if (idoc2.queryCommandEnabled("Copy"))
                                             {
                                                 string str = "font-size:" + fontsize;

                                                 InsertElementHelper.AddToSelection(idoc2, str, "</span>");
                                             }
                                         }
                                     }*/
                                }
                                break;
                            }
                        case "formatcomboBox":
                            {
                                switch (myComboBox.SelectedIndex)
                                {
                                    case 0: ; break;
                                    case 1: InsertElementHelper.AddToSelection(idoc2, "<p>", "</p>"); break;
                                    case 2: InsertElementHelper.AddToSelection(idoc2, "<h1>", "</h1>"); ; break;
                                    case 3: InsertElementHelper.AddToSelection(idoc2, "<h2>", "</h2>"); ; break;
                                    case 4: InsertElementHelper.AddToSelection(idoc2, "<h3>", "</h3>"); ; break;
                                    case 5: InsertElementHelper.AddToSelection(idoc2, "<h4>", "</h4>"); ; break;
                                    case 6: InsertElementHelper.AddToSelection(idoc2, "<h5>", "</h5>"); ; break;
                                    case 7: InsertElementHelper.AddToSelection(idoc2, "<h6>", "</h6>"); ; break;
                                    case 8: InsertElementHelper.AddToSelection(idoc2, "<pre>", "</pre>"); ; break;
                                }
                            } break;
                        case "stylecomboBox":
                            {
                            } break;
                        case "unitcomboBox":
                            {
                                if (myComboBox.SelectedIndex > -1)
                                {
                                    string sizestr = textU.TextSize;
                                    int sizeu = 0;
                                    try
                                    {
                                        sizeu = Convert.ToInt32(sizestr);
                                    }
                                    catch
                                    {
                                        sizeu = 0;
                                    }
                                    finally
                                    {
                                        string sizeunitstr = textU.TextSizeUnit;
                                        if (sizeu > 8 && sizeu < 40)
                                        {

                                        }
                                        string fontsize = "";
                                        if (sizeu == 0)
                                            fontsize = sizestr;
                                        else
                                            fontsize = sizestr + sizeunitstr;
                                        if (idoc2.queryCommandEnabled("Copy"))
                                        {
                                            string str = "font-size:" + fontsize;
                                            //insert.ExecCommand(true, "FontSize", false, fontsize, idoc2);
                                            InsertElementHelper.AddToSelection(idoc2, str, "</span>");
                                        }
                                    }
                                }
                            } break;
                        case "linkcomboBox":
                            {
                                HTMLDesign.LinkBe.Element.href = textU.TextLink;
                                break;
                            }
                        case "tagcomboBox":
                            {
                                if (HTMLDesign.LinkBe!=null)
                                HTMLDesign.LinkBe.Element.target = textU.TextTarget;
                                break;
                            }
                    }
                }
            }
            HTMLDesign.DesignToCode();
        }
                                
        #endregion

        #region 图片属性面板事件
        void imgtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && mytextBox.Text.Trim() != "")
            {
                imgTextSet(mytextBox);
            }
        }

        void imgTextBox_Validated(object sender, EventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            imgTextSet(mytextBox);
        }

        void imgTextSet(TextBox mytextBox)
        {
            XmlDocument doc = GetDocByHTML(HTMLDesign.CurrentElement.outerHTML);
            XmlElement imgEle = doc.DocumentElement;
            switch (mytextBox.Name)
            {
                case "highttextBox":
                    {
                        string imgH = imgU.ImgHeight;
                        int selectedindex = ((ComboBox)imgU.Controls["hightUnitComboBox"]).SelectedIndex;
                        imgH += (selectedindex < 1) ? "px" : "%";
                        string styleStr = imgEle.GetAttribute("style");
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["height"] = imgH;
                        string sty = section.ToString();
                        imgEle.SetAttribute("style", sty);
                        HTMLDesign.CurrentElement.outerHTML = imgEle.OuterXml;
                        break;
                    }
                case "widthtextBox":
                    {
                        string imgW = imgU.ImgWidth;
                        int selectedindex = ((ComboBox)imgU.Controls["widthUnitComboBox"]).SelectedIndex;
                        imgW += (selectedindex < 1) ? "px" : "%";
                        string styleStr = imgEle.GetAttribute("style");
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["width"] = imgW;
                        string sty = section.ToString();
                        imgEle.SetAttribute("style", sty);
                        HTMLDesign.CurrentElement.outerHTML = imgEle.OuterXml;
                        break;
                    }
                case "vspacetextBox": HTMLDesign.ImageBe.Element.vspace = Utility.Convert.StringToInt(imgU.ImgVspace.Trim()); break;
                case "hspacetextBox": HTMLDesign.ImageBe.Element.hspace = Utility.Convert.StringToInt(imgU.ImgHspace.Trim()); break;
                case "bordertextBox": HTMLDesign.ImageBe.Element.border = imgU.ImgBorderWidth.Trim(); break;
                case "resURLtextBox": HTMLDesign.ImageBe.Element.src = mytextBox.Text; break;
                //  case "linkFiletextBox": imgb.Element. = mytextBox.Text; break;
            }
            HTMLDesign.DesignToCode();
        }

        void imgPanelButton_Click(object sender, EventArgs e)
        {
            Button mybutton = sender as Button;
            switch (mybutton.Name)
            {
                case "leftbutton":
                    {
                        HTMLDesign.ImageBe.Element.align = "left";
                    } break;
                case "rightbutton":
                    {
                        HTMLDesign.ImageBe.Element.align = "right";
                    } break;
                case "middlebutton":
                    {
                        HTMLDesign.ImageBe.Element.align = "center";
                    } break;
                case "resourceFilebutton":
                    {
                        string resId = SiteResourceService.SelectResource(MediaFileType.Pic, Service.Workbench.MainForm);
                        if (resId!=null)
                        {
                            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
                            SimpleExIndexXmlElement ele= doc.GetElementById(resId) as SimpleExIndexXmlElement;
                            if (ele != null)
                            {
                                if (!HTMLDesign.ResourcesIdPaths.ContainsKey(resId))
                                {
                                    HTMLDesign.ResourcesIdPaths.Add(resId, ele.AbsoluteFilePath);
                                }
                                imgU.Controls["resURLtextBox"].Text = ele.RelativeFilePath;
                                HTMLDesign.ImageBe.Element.src ="file:///" +ele.AbsoluteFilePath;
                            }
                        }
                        break;
                    }
                case "linkFilebutton":
                    {
                       /* OpenFileDialog op = new OpenFileDialog();
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            imgU.Controls["linkFiletextBox"].Text = op.FileName;
                          //  imgb.Element.href = op.FileName;
                        }*/

                        string resId = SiteResourceService.SelectResource(MediaFileType.Pic, Service.Workbench.MainForm);
                        if (resId != null)
                        {
                            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
                            SimpleExIndexXmlElement ele = doc.GetElementById(resId) as SimpleExIndexXmlElement;
                            if (ele != null)
                            {
                                if (!HTMLDesign.ResourcesIdPaths.ContainsKey(resId))
                                {
                                    HTMLDesign.ResourcesIdPaths.Add(resId, ele.AbsoluteFilePath);
                                }
                                imgU.Controls["linkFiletextBox"].Text = ele.RelativeFilePath;
                                //imgb.Element.href = "file:///" + ele.AbsoluteFilePath;只读的
                            }
                        }
                        break;
                    }
                    

            }
            IHTMLDocument2 idoc2 = (IHTMLDocument2)designWB.Document.DomDocument;
            tec.Text = GeneralMethods.tidy(idoc2.body.innerHTML);
        }

        void imgComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            XmlDocument doc = GetDocByHTML(HTMLDesign.CurrentElement.outerHTML);
            XmlElement imgEle = doc.DocumentElement;
            ComboBox mycombobox = sender as ComboBox;
            int selectedindex = mycombobox.SelectedIndex;
            switch (mycombobox.Name)
            {

                case "hightUnitComboBox":
                    {
                        string imgH = imgU.ImgHeight;
                        imgH += (selectedindex < 1) ? "px" : "%";
                        string styleStr = imgEle.GetAttribute("style");
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["height"] = imgH;
                        string sty = section.ToString();
                        imgEle.SetAttribute("style", sty);
                        HTMLDesign.CurrentElement.outerHTML = imgEle.OuterXml;//出现右边值赋不到左边，存在问题,但其宽度和高度记录在style中，只能如此做
                        break;
                    }
                case "widthunitcomboBox":
                    {
                        string imgW = imgU.ImgWidth;
                        imgW += (selectedindex < 1) ? "px" : "%";
                        string styleStr = imgEle.GetAttribute("style");
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["width"] = imgW;
                        string sty = section.ToString();
                        imgEle.SetAttribute("style", sty);
                        HTMLDesign.CurrentElement.outerHTML = imgEle.OuterXml;
                        break;
                    }
                case "aligncomboBox":
                    {
                        string align = "";
                        switch (selectedindex)
                        {
                            case 0: align = ""; break;
                            case 1: align = "baseline"; break;
                            case 2: align = "top"; break;
                            case 3: align = "middle"; break;
                            case 4: align = "bottom"; break;
                            case 5: align = "texttop"; break;
                            case 6: align = "absmiddle"; break;
                            case 7: align = "absbottom"; break;
                            case 8: align = "left"; break;
                            case 9: align = "right"; break;
                        }
                        HTMLDesign.ImageBe.Element.align = align;
                    } break;
            }
        }
        #endregion

        #region 表格属性面板事件
        IHTMLTable table = null;
        void tbtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && mytextBox.Text.Trim() != "")
            {
                table = currentEle as IHTMLTable;
                tbTextSet(mytextBox);
            }
        }

        void tbtextBox_Validated(object sender, EventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            tbTextSet(mytextBox);
        }

        void tbTextSet(TextBox mytextBox)
        {
            table = currentEle as IHTMLTable;
            switch (mytextBox.Name)
            {
                case "bgpicsrctextBox":
                    {
                        if (mytextBox.Text.Trim().Length > 0)
                        {
                            if (File.Exists(mytextBox.Text))
                                HTMLDesign.TableBe.Element.background = mytextBox.Text;
                            else
                                MessageBox.Show("File not find!");
                        }
                        break;
                    }
                case "filltextBox":
                    {
                        HTMLDesign.TableBe.Element.cellPadding = mytextBox.Text;
                        break;
                    }
                case "spacetextBox":
                    {
                        HTMLDesign.TableBe.Element.cellSpacing = mytextBox.Text;
                    } break;
                case "bordertextBox":
                    {
                        HTMLDesign.TableBe.Element.border = mytextBox.Text;
                    } break;
                case "rownumtextBox":
                    {
                        GeneralMethodsForDesign.changeRows(table, Convert.ToInt32(mytextBox.Text));
                    } break;
                case "colnumtextBox":
                    {
                        GeneralMethodsForDesign.changeColumns(table, Convert.ToInt32(mytextBox.Text));
                    } break;
                case "widthtextBox":
                    {
                        HTMLDesign.TableBe.Element.width = mytextBox.Text + ((((ComboBox)tableU.Controls["widthunitcomboBox"]).SelectedIndex == 0) ? "" : "%");
                    } break;
                case "bgcolortextBox":
                    {
                        try
                        {
                            tableU.TableBgColor = GeneralMethods.GetColor(mytextBox.Text.Substring(1, 6));
                            HTMLDesign.TableBe.Element.bgColor = mytextBox.Text;
                        }
                        catch
                        {
                            MessageBox.Show("Wrong Color");
                        }
                        break;
                    }
                case "bordercolortextBox":
                    {
                        try
                        {
                            tableU.TableBorderColor = GeneralMethods.GetColor(mytextBox.Text.Substring(1, 6));
                            HTMLDesign.TableBe.Element.borderColor = mytextBox.Text;
                        }
                        catch
                        {
                            MessageBox.Show("Wrong Color");
                        }
                        break;
                    }
            }
        }

        void tbbutton_click(object sender, EventArgs e)
        {
            Button mybutton = sender as Button;
            switch (mybutton.Name)
            {
                case "browserbutton":
                    {
                        frmInsertPicCode insertpic = new frmInsertPicCode();
                        if (insertpic.ShowDialog() == DialogResult.OK)
                        {
                            ((TextBox)tableU.Controls["bgpicsrctextBox"]).Text = insertpic.PicPath;
                            HTMLDesign.TableBe.Element.background = insertpic.PicPath;
                        }
                    } break;
            }
        }

        void tbcomboBox_SelectedIndexChaged(object sender, EventArgs e)
        {
            IHTMLDocument2 idoc2 = designWB.Document.DomDocument as IHTMLDocument2;
            ComboBox myComboBox = sender as ComboBox;
            switch (myComboBox.Name)
            {
                case "fontcomboBox":
                    {
                        if (myComboBox.SelectedIndex > -1)
                        {
                            idoc2.execCommand("FontName", true, myComboBox.Text);
                        }
                    } break;
                case "sizecomboBox":
                    {
                        if (myComboBox.SelectedIndex > -1)
                        {
                            object obj = myComboBox.SelectedIndex + 1;
                            idoc2.execCommand("FontSize", true, obj);
                        }
                    } break;
                case "formatcomboBox":
                    {
                    } break;
                case "stylecomboBox":
                    {
                    } break;
                case "unitcomboBox":
                    {
                    } break;
                case "linkcomboBox":
                    {
                    } break;
                case "tagcomboBox":
                    {
                    } break;
                case "widthunitcomboBox":
                    {
                        HTMLDesign.TableBe.Element.width = ((TextBox)tableU.Controls["widthtextBox"]).Text + ((myComboBox.SelectedIndex == 0) ? "" : "%"); 
                    } break;
                case "aligncomboBox":
                    {
                        string alignstr = string.Empty;
                        switch (myComboBox.SelectedIndex)
                        {
                            case 0:
                            case 1: alignstr = "left"; break;
                            case 2: alignstr = "center"; break;
                            case 3: alignstr = "right"; break;
                        }
                        HTMLDesign.TableBe.Element.align = alignstr;
                    } break;
            }
        }

        void tbMyColorButton_MyColorChanged(object sender, EventArgs e)
        {
            ColorGeneralButton mycolor = sender as ColorGeneralButton;
            if (!(designWB.Focused || tec.Focused))
            {
                switch (mycolor.Name)
                {
                    case "bgColorButton":
                        {
                            ((TextBox)tableU.Controls["bgcolortextBox"]).Text = GeneralMethods.ColorToRGB(mycolor.MyColor);
                            HTMLDesign.TableBe.Element.bgColor = "#" + mycolor.MyColor.ToArgb().ToString("x").Remove(0, 2);
                        } break;
                    case "borderColorButton":
                        {
                            ((TextBox)tableU.Controls["bordercolortextBox"]).Text = GeneralMethods.ColorToRGB(((ColorGeneralButton)sender).MyColor);
                            HTMLDesign.TableBe.Element.borderColor = "#" + mycolor.MyColor.ToArgb().ToString("x").Remove(0, 2);
                        }
                        break;
                }
            }
        }
        #endregion

        #region 表格单元格属性面板事件
        void tdtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && mytextBox.Text.Trim() != "")
            {
                tdTextSet(mytextBox);
            }
        }

        void tdTextBox_Validated(object sender, EventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            tdTextSet(mytextBox);
        }

        void tdTextSet(TextBox mytextBox)
        {
            switch (mytextBox.Name)
            {
                case "bgpictextBox":
                    {
                        HTMLDesign.TableCellBe.Element.background = mytextBox.Text;
                        break;
                    }
                case "bgTextBox":
                    {
                        try
                        {
                            textU.Cellbgcolor = GeneralMethods.GetColor(mytextBox.Text.Substring(1, 6));
                            HTMLDesign.TableCellBe.Element.bgColor = mytextBox.Text;
                        }
                        catch
                        {
                            MessageBox.Show("Wrong Color");
                        }
                        break;
                    }
                case "HeighttextBox":
                    {
                        HTMLDesign.TableCellBe.Element.height = mytextBox.Text;
                    } break;
                case "bordertextBox":
                    {
                    } break;
                case "WidthtextBox":
                    {
                        HTMLDesign.TableCellBe.Element.width = mytextBox.Text;
                    } break;
            }
        }

        void tdbutton_click(object sender, EventArgs e)
        {
            Button mybutton = sender as Button;
            switch (mybutton.Name)
            {
                case "bgpicbutton":
                    {
                        frmInsertPicCode insertpic = new frmInsertPicCode();
                        if (insertpic.ShowDialog() == DialogResult.OK)
                        {
                            ((TextBox)textU.Controls["bgpictextBox"]).Text = insertpic.PicPath;
                            HTMLDesign.TableCellBe.Element.background = insertpic.PicPath;
                        }
                    } break;
            }
        }

        void tdcomboBox_SelectedIndexChaged(object sender, EventArgs e)
        {
            IHTMLDocument2 idoc2 = designWB.Document.DomDocument as IHTMLDocument2;
            ComboBox myComboBox = sender as ComboBox;
            InsertElementHelper insert = new InsertElementHelper();
            IHTMLTxtRange searchRange = (IHTMLTxtRange)idoc2.selection.createRange();
            string innerText = searchRange.htmlText;//获取文本的html代码
            IHTMLElement ele = htmlEdit.FindParent(HTMLDesign.CurrentElement, "Table");
            switch (myComboBox.Name)
            {
                case "fontcomboBox":
                    {
                        if (myComboBox.SelectedIndex > -1)
                        {
                           // idoc2.execCommand("FontName", true, myComboBox.Text);
                            
                            if (ele!=null && !string.IsNullOrEmpty(innerText))
                                insert.ExecCommand(true, "FontName", true, myComboBox.Text, idoc2);
                        }
                        break;
                    } 
                case "sizecomboBox":
                    {
                        if (myComboBox.SelectedIndex > -1)
                        {
                            if (ele!=null && !string.IsNullOrEmpty(innerText))
                                insert.ExecCommand(true, "FontSize", true, myComboBox.Text, idoc2);
                        }
                        break;
                    }
                case "formatcomboBox":
                    {
                    } break;
                case "stylecomboBox":
                    {
                    } break;
                case "unitcomboBox":
                    {
                    } break;
                case "linkcomboBox":
                    {
                    } break;
                case "tagcomboBox":
                    {
                    } break;
                case "widthunitcomboBox":
                    {
                        HTMLDesign.TableCellBe.Element.width = ((TextBox)tableU.Controls["widthtextBox"]).Text + ((myComboBox.SelectedIndex == 0) ? "" : "%");
                    } break;
                case "HaligncomboBox":
                    HTMLDesign.TableCellBe.Element.align = textU.CellHalign;
                    break;
                case "ValigncomboBox":
                    HTMLDesign.TableCellBe.Element.vAlign = textU.CellValign;
                    break;
            }
        }

        void tdMyColorButton_MyColorChanged(object sender, EventArgs e)
        {
            ColorGeneralButton mycolor = sender as ColorGeneralButton;
            if (!(designWB.Focused || tec.Focused))
            {
                switch (mycolor.Name)
                {
                    case "bgColorButton":
                        {
                            ((TextBox)textU.Controls["bgTextBox"]).Text = GeneralMethods.ColorToRGB(mycolor.MyColor);
                            HTMLDesign.TableCellBe.Element.bgColor = "#" + mycolor.MyColor.ToArgb().ToString("x").Remove(0, 2);
                        } break;
                    case "bordclrColorButton":
                        {
                            ((TextBox)textU.Controls["boldclrTextBox"]).Text = GeneralMethods.ColorToRGB(((ColorGeneralButton)sender).MyColor);
                            HTMLDesign.TableCellBe.Element.borderColor = "#" + mycolor.MyColor.ToArgb().ToString("x").Remove(0, 2);
                        }
                        break;
                }
            }
        }
        #endregion

        #region 动画属性面板事件

        void MediaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && mytextBox.Text.Trim() != "")
            {
                MediaTextSet(mytextBox);
            }
        }

        void MediaTextBox_Validated(object sender, EventArgs e)
        {
            TextBox mytextBox = sender as TextBox;
            MediaTextSet(mytextBox);
        }

        void MediaTextSet(TextBox mytextBox)
        {
            HTMLObjectElementClass currentObjEle = HTMLDesign.CurrentElement as HTMLObjectElementClass;
            XmlDocument doc = GetDocByHTML(HTMLDesign.CurrentElement.outerHTML);
            XmlElement objectEle = doc.DocumentElement;
            string styleStr = objectEle.GetAttribute("style");
            XmlElement embedEle = objectEle.SelectSingleNode("embed") as XmlElement;
            switch (mytextBox.Name)
            {

                case "widthTextBox":
                    {
                        string width = mytextBox.Text + ((((ComboBox)mediaU.Controls["flashWidUintComboBox"]).SelectedIndex == 1) ? "%" : "px");
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["width"] = width;
                        string sty = section.ToString();
                        objectEle.SetAttribute("style", sty);
                        embedEle.SetAttribute("width", width);
                    }
                    break;
                case "heightTextBox":
                    {
                        string height = mytextBox.Text + ((((ComboBox)mediaU.Controls["flashHeigUintComboBox"]).SelectedIndex == 1) ? "%" : "px");
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["height"] = height;
                        string sty = section.ToString();
                        objectEle.SetAttribute("style", sty);
                        embedEle.SetAttribute("height", height);
                    }
                    break;
                case "vspacetextBox":
                    {
                        string vspace = string.IsNullOrEmpty(mytextBox.Text) ? "0" : mytextBox.Text;
                        //HTMLDesign.FlashBe.Element.vspace = Convert.ToInt32(vspace);
                        objectEle.SetAttribute("vspace", vspace);
                        embedEle.SetAttribute("vspace", vspace);
                        break;
                    }
                case "hspacetextBox":
                    {
                        string hspace = (mytextBox.Text == "") ? "0" : mytextBox.Text;
                        //HTMLDesign.FlashBe.Element.hspace = Convert.ToInt32(hspace);
                        objectEle.SetAttribute("hspace", hspace);
                        if (embedEle != null)
                            embedEle.SetAttribute("hspace", hspace);
                        break;
                    }
            }
            if (currentObjEle != null)
                currentObjEle.outerHTML = objectEle.OuterXml;
        }

        void MediaButton_click(object sender, EventArgs e)
        {

        }

        void MediaComboBox_SelectedIndexChaged(object sender, EventArgs e)
        {
            HTMLObjectElementClass currentObjEle = HTMLDesign.CurrentElement as HTMLObjectElementClass;
            XmlDocument doc = GetDocByHTML(HTMLDesign.CurrentElement.outerHTML);
            XmlElement objectEle = doc.DocumentElement;
            XmlElement embedEle = objectEle.SelectSingleNode("embed") as XmlElement;
            string styleStr = objectEle.GetAttribute("style");
            ComboBox mycombobox = sender as ComboBox;
            int selectedindex = mycombobox.SelectedIndex;
            switch (mycombobox.Name)
            {
                case "flashWidUintComboBox":
                    {
                        string width = mediaU.Controls["widthtextBox"].Text;
                        width += (selectedindex == 0) ? "px" : "%";
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["width"] = width;
                        string sty = section.ToString();
                        objectEle.SetAttribute("style", sty);
                        embedEle.SetAttribute("width", width);
                        currentObjEle.outerHTML = objectEle.OuterXml;
                        break;
                    }
                case "flashHeigUintComboBox":
                    {
                        string height = mediaU.Controls["heighttextBox"].Text;
                        height += (selectedindex == 0) ? "px" : "%";
                        CssSection section = CssSection.Parse(styleStr);
                        section.Properties["height"] = height;
                        string sty = section.ToString();
                        objectEle.SetAttribute("style", sty);
                        embedEle.SetAttribute("height", height);
                        currentObjEle.outerHTML = objectEle.OuterXml;
                        break;
                    }
                case "qualitycomboBox":
                    {
                        XmlElement quaEle = GetElementByNameValue(objectEle, "quality");
                        string quality = "";
                        switch (selectedindex)
                        {
                            case 0: quality = "high"; break;
                            case 1: quality = "low"; break;
                            case 2: quality = ""; break;
                            case 3: quality = "autolow"; break;
                        }
                        if (quaEle != null)
                            quaEle.SetAttribute("value", quality);
                        embedEle.SetAttribute("quality", quality);
                        currentObjEle.outerHTML = objectEle.OuterXml;
                        break;
                    }
                case "scalecomboBox":
                    {
                        XmlElement scaleEle = GetElementByNameValue(objectEle, "scale");
                        string scale = "";
                        switch (selectedindex)
                        {
                            case 0: scale = ""; break;
                            case 1: scale = "noborder"; break;
                            case 2: scale = "exactfit"; break;
                        }
                        if (scaleEle != null)
                            scaleEle.SetAttribute("value", scale);
                        embedEle.SetAttribute("scale", scale);
                        currentObjEle.outerHTML = objectEle.OuterXml;
                        break;
                    } 
                case "aligncomboBox":
                    {
                        XmlElement alignEle = GetElementByNameValue(objectEle, "align");
                        string align = "";
                        switch (selectedindex)
                        {
                            case 0: align = ""; break;
                            case 1: align = "baseline"; break;
                            case 2: align = "top"; break;
                            case 3: align = "middle"; break;
                            case 4: align = "bottom"; break;
                            case 5: align = "texttop"; break;
                            case 6: align = "absMiddle"; break;
                            case 7: align = align = "absbottom"; break;
                            case 8: align = "left"; break;
                            case 9: align = "right"; break;
                        }
                        objectEle.SetAttribute("align", align);
                        embedEle.SetAttribute("align", align);
                        currentObjEle.outerHTML = objectEle.OuterXml;
                        break;
                    }
            }
        }

        void MediaMyColorButton_MyColorChanged(object sender, EventArgs e)
        {
            ColorGeneralButton mycolor = sender as ColorGeneralButton;
            switch (mycolor.Name)
            {
                case "bgColorButton":
                    {
                        string colorstr = GeneralMethods.ColorToRGB(mycolor.MyColor);
                        ((TextBox)mediaU.Controls["bgtextBox"]).Text = colorstr;
                    } break;
            }
        }

        void MediaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            XmlDocument doc = GetDocByHTML(HTMLDesign.CurrentElement.outerHTML);
            XmlElement objectEle = doc.DocumentElement;
            XmlElement embedEle = objectEle.SelectSingleNode("embed") as XmlElement;
            string styleStr = objectEle.GetAttribute("style");
            XmlElement loopEle = GetElementByNameValue(objectEle, "LOOP");
            XmlElement autoEle = GetElementByNameValue(objectEle, "AUTOSTART");
            if (autoEle==null)
                autoEle = GetElementByNameValue(objectEle, "play");

            CheckBox mycheckbox = sender as CheckBox;
            string chkstr = mycheckbox.Checked ? "1" : "0";
            switch (mycheckbox.Name)
            {
                case "loopcheckBox":
                    {
                        if (loopEle != null)
                            loopEle.SetAttribute("value", chkstr);
                        embedEle.SetAttribute("loop", chkstr);
                        break;
                    }
                case "autoplaycheckBox":
                    {
                        if (autoEle != null)
                            autoEle.SetAttribute("value", chkstr);
                        embedEle.SetAttribute("autostart", chkstr);
                        break;
                    }
            }
            HTMLDesign.CurrentElement.outerHTML = objectEle.OuterXml;
        }
        #endregion
        #endregion
    }
}
