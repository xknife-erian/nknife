using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Jeelu.Win;
using System.IO;
using System.Xml;
namespace Jeelu.SimplusD.Client.Win
{
    public partial class textUserControl : UserControl
    {
        GetCHSTextforTextPropertyPanel getCHSText = new GetCHSTextforTextPropertyPanel();
        ColorGeneralButton FontColorButton = null;
        public textUserControl()
        {
            InitializeComponent();
            taglabel.Text = ResourceService.GetResourceText("TextpropertyPanel.label.tag");
            linklabel.Text = ResourceService.GetResourceText("TextpropertyPanel.label.link");
            sizelabel.Text = ResourceService.GetResourceText("TextpropertyPanel.label.size");
            styllabel.Text = ResourceService.GetResourceText("TextpropertyPanel.label.style");
            fontlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.label.font");
            fmatlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.label.format");
            leftbutton.Image = ResourceService.GetResourceImage("left_align");
            rightbutton.Image = ResourceService.GetResourceImage("right_align");
            middlebutton.Image = ResourceService.GetResourceImage("middle_align");
            bothbutton.Image = ResourceService.GetResourceImage("both_align");
            listButton.Image = ResourceService.GetResourceImage("project_list");
            numButton.Image = ResourceService.GetResourceImage("number_list");
            linkButton.Image = ResourceService.GetResourceImage("MainMenu.file.open");
            bgpicbutton.Image = ResourceService.GetResourceImage("MainMenu.file.open");
            InitComboBox(fontcomboBox);
            Hlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.celllabel.halign");
            Vlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.celllabel.valign");
            Widthlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.celllabel.width");
            Heightlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.celllabel.height");
            bgpiclabel.Text = ResourceService.GetResourceText("TextpropertyPanel.celllabel.background");
            bgclrlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.celllabel.bgcolor");
            borderlabel.Text = ResourceService.GetResourceText("TextpropertyPanel.celllabel.border");
           // pagPropertyButton.Text = ResourceService.GetResourceText("TextpropertyPanel.button.pagepro");

            string[] halign = getCHSText.HAlign;
            string[] valign = getCHSText.VAlign;
            string[] Format = getCHSText.Format;
            string[] Size = getCHSText.Size;
            string[] SizeUnit = getCHSText.SizeUnit;
            string[] Style = getCHSText.Style;
            InitComboBox(HaligncomboBox, halign);
            InitComboBox(ValigncomboBox, valign);
            InitComboBox(formatcomboBox, Format);
           // InitComboBox(sizecomboBox, Size);
            InitComboBox(unitcomboBox, SizeUnit);
            InitComboBox(stylecomboBox, Style);
        }

        /// <summary>
        /// 填充工具栏字体ComboBox
        /// </summary>
        /// <param name="tsComboBox"></param>
        private void InitComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            string defstr =ResourceService.GetResourceText("TextpropertyPanel.fontname.default");  
            string editfontstr = ResourceService.GetResourceText("TextpropertyPanel.fontname.editfontlist");

            comboBox.Items.Add(defstr);

            string path = Path.Combine(Application.StartupPath, "Config/fontlist.xml");
            XmlDocument fontListDoc = new XmlDocument();
            if (File.Exists(path))
            {
                try
                {
                    fontListDoc.Load(path);
                    XmlNodeList xnl = fontListDoc.DocumentElement.ChildNodes;
                    if (xnl != null)
                    {
                        foreach (XmlNode node in xnl)
                        {
                            comboBox.Items.Add(node.InnerText);
                        }
                    }
                }
                
                catch
                {
                    File.Delete(path);
                    throw;
                }
            }
            comboBox.Items.Add(editfontstr);
        }

        /// <summary>
        /// 填充工具栏字体ComboBox
        /// </summary>
        /// <param name="tsComboBox"></param>
        private void InitComboBox(ComboBox comboBox, string[] str)
        {
            foreach (string stritem in str)
            {
                if (str.Length > 0)
                    comboBox.Items.Add(stritem);
            }
        }

        private void textUserControl_Resize(object sender, EventArgs e)
        {
                linkcomboBox.Width = (Width - linkcomboBox.Left) / 10 * 7;
                if (linkcomboBox.Width < tagcomboBox.Width)
                    linkcomboBox.Width = tagcomboBox.Width;
                linkButton.Left = linkcomboBox.Left + linkcomboBox.Width + 10;
        }

        private void textUserControl_Paint(object sender, PaintEventArgs e)
        {
            GeneralMethods.drawLineForProPanel(this, Color.Blue, 1, DashStyle.Solid, Width);
        }

        private void sizecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (sizecomboBox.SelectedIndex > 0 && sizecomboBox.SelectedIndex < 9)
            //{
            //    unitcomboBox.Enabled = true;
            //    //if (unitcomboBox.Text == "")
            //    //    unitcomboBox.SelectedIndex = 0;
            //}
            //else
            //    unitcomboBox.Enabled = false;
        }

        private void tagcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tagcomboBox.Text=="")
                tagcomboBox.Enabled = false;
            else
                tagcomboBox.Enabled = true;
        }

        #region 属性

        public string TextFormat
        {
            get
            {
                switch (formatcomboBox.SelectedIndex)
                {
                    case 1: return "p";
                    case 2: return "h1";
                    case 3: return "h2";
                    case 4: return "h3";
                    case 5: return "h4";
                    case 6: return "h5";
                    case 7: return "h6";
                    case 8: return "pre";
                    default: return "";
                }
            }
            set
            {
                switch (value)
                {
                    case "": formatcomboBox.SelectedIndex = 0; break;
                    case "p": formatcomboBox.SelectedIndex = 1; break;
                    case "h1": formatcomboBox.SelectedIndex = 2; break;
                    case "h2": formatcomboBox.SelectedIndex = 3; break;
                    case "h3": formatcomboBox.SelectedIndex = 4; break;
                    case "h4": formatcomboBox.SelectedIndex = 5; break;
                    case "h5": formatcomboBox.SelectedIndex = 6; break;
                    case "h6": formatcomboBox.SelectedIndex = 7; break;
                    case "pre": formatcomboBox.SelectedIndex = 8; break;
                }
            }
        }
        public string TextFont
        {
            get
            {
                return fontcomboBox.Text;
            }
            set
            {
                fontcomboBox.Text = value;
            }
        }
        public string TextSize
        {
            get
            {
                switch (sizecomboBox.SelectedIndex)
                {
                    case -1:
                        return "";
                    default: return (sizecomboBox.SelectedIndex+1).ToString();
                    /* case 1: return "9"; 
                     case 2: return "10"; 
                     case 3: return "12"; 
                     case 4: return "14"; 
                     case 5: return "16"; 
                     case 6: return "18"; 
                     case 7: return "24"; 
                     case 8: return "36"; 
                     case 9: return "xx-small"; 
                     case 10: return "x-small"; 
                     case 11: return "small"; 
                     case 12: return "medium"; 
                     case 13: return "large"; 
                     case 14: return "x-large"; 
                     case 15: return "xx-large"; 
                     case 16: return "smaller"; 
                     case 17: return "larger"; 
                     default: return "";*/
                }
            }
            set
            {
                switch (value)
                {
                    case "1": sizecomboBox.SelectedIndex = 0; break;
                    case "2": sizecomboBox.SelectedIndex = 1; break;
                    case "3": sizecomboBox.SelectedIndex = 2; break;
                    case "4": sizecomboBox.SelectedIndex = 3; break;
                    case "5": sizecomboBox.SelectedIndex = 4; break;
                    case "6": sizecomboBox.SelectedIndex = 5; break;
                    case "7": sizecomboBox.SelectedIndex = 6; break;
                    case "": sizecomboBox.SelectedIndex = -1; break;
                    /* case "9": sizecomboBox.SelectedIndex= 1; break;
                     case "10": sizecomboBox.SelectedIndex= 2; break;
                     case "12": sizecomboBox.SelectedIndex=3 ; break;
                     case "14": sizecomboBox.SelectedIndex=4 ; break;
                     case "16": sizecomboBox.SelectedIndex=5 ; break;
                     case "18": sizecomboBox.SelectedIndex=6 ; break;
                     case "24": sizecomboBox.SelectedIndex=7 ; break;
                     case "36": sizecomboBox.SelectedIndex=8 ; break;
                     case "xx-small": sizecomboBox.SelectedIndex=9 ; break;
                     case "x-small": sizecomboBox.SelectedIndex=10 ; break;
                     case "small": sizecomboBox.SelectedIndex=11 ; break;
                     case "medium": sizecomboBox.SelectedIndex=12 ; break;
                     case "large": sizecomboBox.SelectedIndex=13 ; break;
                     case "x-large": sizecomboBox.SelectedIndex=14 ; break;
                     case "xx-large": sizecomboBox.SelectedIndex=15 ; break;
                     case "smaller": sizecomboBox.SelectedIndex=16 ; break;
                     case "larger": sizecomboBox.SelectedIndex=17 ; break;*/
                }
            }
        }
        public string TextSizeUnit
        {
            get
            {
                return "px";
                //switch (unitcomboBox.SelectedIndex)
                //{
                //    case 0: 
                //    //case 1: return "pt";
                //    //case 2: return "in";
                //    //case 3: return "cm";
                //    //case 4: return "mm";
                //    //case 5: return "pc";
                //    //case 6: return "em";
                //    //case 7: return "ex";
                //    //case 8: return "%";
                //    //default: return "px";
                //}
            }
            set
            {
                /*switch (value)
                {
                    case "px": unitcomboBox.SelectedIndex = 0; break;
                    case "pt": unitcomboBox.SelectedIndex = 1; break;
                    case "in": unitcomboBox.SelectedIndex = 2; break;
                    case "cm": unitcomboBox.SelectedIndex = 3; break;
                    case "mm": unitcomboBox.SelectedIndex = 4; break;
                    case "pc": unitcomboBox.SelectedIndex = 5; break;
                    case "em": unitcomboBox.SelectedIndex = 6; break;
                    case "ex": unitcomboBox.SelectedIndex = 7; break;
                    case "%": unitcomboBox.SelectedIndex = 8; break;
                    case "": unitcomboBox.SelectedIndex = -1; break;
                }*/
            }
        }
        public string TextStyle
        {
            get { return stylecomboBox.Text; }
            set { stylecomboBox.Text = value; }
        }
        public string TextColor
        {
            get { return "#" + FontColorButton.MyColor.ToArgb().ToString("x").Remove(0, 2); }
            set
            {
                if (colortextBox.Text != value)
                {
                    colortextBox.Text = value;
                    FontColorButton.MyColor = GeneralMethods.GetColor(value.Substring(1, 6));
                }
            }
        }
        public string TextLink
        {
            get
            {
                return linkcomboBox.Text;
            }
            set
            {
                linkcomboBox.Text = value;
            }
        }
        public string TextTarget
        {
            get
            {
                return tagcomboBox.Text;
            }
            set
            {
                switch (value)
                {
                    case "": { tagcomboBox.SelectedIndex = -1; break; }
                    case "_blank": { tagcomboBox.SelectedIndex = 0; break; }
                    case "_parent": { tagcomboBox.SelectedIndex = 1; break; }
                    case "_self": { tagcomboBox.SelectedIndex = 2; break; }
                    case "_top": { tagcomboBox.SelectedIndex = 3; break; }
                }
            }
        }

        public string CellHalign
        {
            get
            {
                switch (HaligncomboBox.SelectedIndex)
                {
                    case 0: return "";
                    case 1: return "left";
                    case 2: return "center";
                    case 3: return "right";
                    default: return "";
                }
            }
            set
            {
                switch (value)
                {
                    case null:
                    case "": HaligncomboBox.SelectedIndex = 0; break;
                    case "left": HaligncomboBox.SelectedIndex = 1; ; break;
                    case "center": HaligncomboBox.SelectedIndex = 2; ; break;
                    case "right": HaligncomboBox.SelectedIndex = 3; ; break;

                }
            }
        }
        public string CellValign
        {
            get
            {
                switch (ValigncomboBox.SelectedIndex)
                {
                    case 0: return "";
                    case 1: return "top";
                    case 2: return "middle";
                    case 3: return "bottom";
                    case 4: return "baseline";
                    default: return "";
                }
            }
            set
            {
                switch (value)
                {
                    case null:
                    case "": ValigncomboBox.SelectedIndex = 0; break;
                    case "top": ValigncomboBox.SelectedIndex = 1; ; break;
                    case "middle": ValigncomboBox.SelectedIndex = 2; ; break;
                    case "bottom": ValigncomboBox.SelectedIndex = 3; ; break;
                    case "baseline": ValigncomboBox.SelectedIndex = 4; ; break;
                }
            }
        }
        public string CellWidth
        {
            get
            {
                return WidthtextBox.Text;
            }
            set
            {
                WidthtextBox.Text = value;
            }
        }
        public string CellHeight
        {
            get
            {
                return HeighttextBox.Text;
            }
            set
            {
                HeighttextBox.Text = value;
            }
        }
        public string Cellbgpic
        {
            get
            {
                return bgpictextBox.Text;
            }
            set
            {
                bgpictextBox.Text = value;
            }
        }
        public Color Cellbgcolor
        {
            get
            {
                return bgColorButton.MyColor;
            }
            set
            {
                bgColorButton.MyColor = value;
            }
        }
        public Color Cellbordercolor
        {
            get
            {
                return bordclrColorButton.MyColor;
            }
            set
            {
                bordclrColorButton.MyColor = value;
            }
        }

        public bool Bold
        {
            get
            {
                return Boldbutton.BackColor != Color.Empty;
            }
            set
            {
                if (value)
                    Boldbutton.BackColor = Color.AliceBlue;
                else
                    Boldbutton.BackColor = Color.Empty;
            }
        }

        #endregion
        private void textUserControl_Enter(object sender, EventArgs e)
        {//从文件读出来的字体 保存到LIST中 更新时候做个比较 如果相同 则不修改 
            InitComboBox(fontcomboBox);
        }
    }
}
