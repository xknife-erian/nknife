using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using Jeelu.Win;

namespace Jeelu.Win
{
    public partial class InsertPicCodeForm : BaseForm
    {
        string insertIamgeHTML = "";//返回要插入到HTML编辑器中的HTML代码
        HTMLDesignerContrl _htmldesigner = null;
        InsertMode _insertUserMode;
        HTMLDesignerContrl htmlDesigner = null;


        public HTMLDesignerContrl HtmlDesigner
        {
            get { return htmlDesigner; }
            set { htmlDesigner = value; }
        }
        public string InsertImageHTML
        {
            get { return insertIamgeHTML; }
            set { insertIamgeHTML = value; }
        }

        XhtmlTags.Img _imgEle = null;

        public XhtmlTags.Img ImgEle
        {
            get { return _imgEle; }
            set { _imgEle = value; }
        }
        public string AbsoluteFilePath
        {
            get;
            set;
        }

        decimal ratio = 1;

        #region 构造函数

        public InsertPicCodeForm(XhtmlTagElement imgEle, string path)
        {
            InitializeComponent();
            _htmldesigner = htmlDesigner;
            //_insertUserMode = htmlDesigner.InsertUseMode;

            AbsoluteFilePath = path;

            _imgEle = (XhtmlTags.Img)imgEle;
            picIDTextBox.Text=_imgEle.GetAttribute("src");
            picPathTextBox.Text = path;
            replaceWordTextBox.Text = _imgEle.GetAttribute("title");
            if (!string.IsNullOrEmpty(_imgEle.GetAttribute("width")))
                PicWidthNumericUpDown.Value = Convert.ToInt32(_imgEle.GetAttribute("width"));
            if (!string.IsNullOrEmpty(_imgEle.GetAttribute("height")))
                PicHeightNumericUpDown.Value = Convert.ToInt32(_imgEle.GetAttribute("height"));
            this.ImeMode = ImeMode.On;

            if (!string.IsNullOrEmpty(path))
            {
                Size sizeofpic = GetSizeOfPic(path);
                PicWidthNumericUpDown.Value = sizeofpic.Width;
                PicHeightNumericUpDown.Value = sizeofpic.Height;
                ratio = Convert.ToDecimal(sizeofpic.Width) / Convert.ToDecimal(sizeofpic.Height);
                widthUnitComBox.SelectedIndex = heightUnitComboBox.SelectedIndex = 0;

                //this.widthCheckBox.Checked = true;
                //this.heightCheckBox.Checked = true;
                //this.limitCheckBox.Enabled = true;
                //this.limitCheckBox.Checked = true;
            }
        }

        void InitControlsValue()
        {
            //GetCHSTextforInsertImage getCHSforIn = new GetCHSTextforInsertImage();
            //string[] unit = getCHSforIn.Unit;
            //string[] calign = getCHSforIn.Align;

            //foreach (string u in unit)
            //{
            //    widthUnitComBox.Items.Add(u);
            //    heightUnitComboBox.Items.Add(u);
            //}
            //foreach (string a in calign)
            //    picAlignComboBox.Items.Add(a);
            //picAlignComboBox.SelectedIndex = 0;
            //widthUnitComBox.SelectedIndex = heightUnitComboBox.SelectedIndex = 0;


            /////加载默认值
            //DesignDataXmlDocument.InsertPicData data = Service.Sdsite.DesignDataDocument.HTMLDesignerInsertPicData;
            //if (data != null)
            //{
            //    //对齐
            //    picAlignComboBox.SelectedIndex = data.PicAlign;
            //    picHspaceNumericUpDown.Value = data.PicHSpace;
            //    picVspaceNumericUpDown.Value = data.PicVSpace;
            //    picBorderNumericUpDown.Value = data.PicFrameWidth;
            //}
        }

        #endregion

        private void picPathBtn_Click(object sender, EventArgs e)
        {
            string filepath = "";
            string picId = ""; _htmldesigner.OpenPicDialog();
            if (File.Exists(picId))
                filepath = picId;
            //else 
            //   filepath = _htmldesigner.GetResourceAbsolutePath(picId);
            if (!string.IsNullOrEmpty(filepath))
            {
                picIDTextBox.Text = picId;
                picPathTextBox.Text = filepath;
                Size sizeofpic = GetSizeOfPic(filepath);
                PicWidthNumericUpDown.Value = sizeofpic.Width;
                PicHeightNumericUpDown.Value = sizeofpic.Height;
                ratio = Convert.ToDecimal(sizeofpic.Width) / Convert.ToDecimal(sizeofpic.Height);
                widthUnitComBox.SelectedIndex = heightUnitComboBox.SelectedIndex = 0;

                this.widthCheckBox.Checked = true;
                this.heightCheckBox.Checked = true;
                this.limitCheckBox.Enabled = true;
                this.limitCheckBox.Checked = true;
            }
            MediaPath = filepath;
        }

        private void pic2PathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open file";
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "PIC files (*.jpg,*.png,*.gif)|*.jpg;*.png;*.gif";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.pic2PathTextBox.Text = dlg.FileName;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            SaveConfig();

            string widthStr = PicWidthNumericUpDown.Value.ToString()+ widthUnitComBox.Text;
            string heightStr= PicHeightNumericUpDown.Value.ToString() + heightUnitComboBox.Text;
            ImgEle.SetStyle(widthStr, heightStr);
            ImgEle.Alt = replaceWordTextBox.Text;
            ImgEle.Align = picAlignComboBox.Text;
            ImgEle.Border = picBorderNumericUpDown.Value.ToString();
            ImgEle.HSpace = picHspaceNumericUpDown.Value.ToString();
            ImgEle.VSpace = picVspaceNumericUpDown.Value.ToString();
            #region
            //不管如何,只要存在此文件则加入到HTML编辑器中
           /* string picwidth = PicWidthNumericUpDown.Value.ToString();
            string picheight = PicHeightNumericUpDown.Value.ToString();
            string picpath = picIDTextBox.Text;
            string pic2path = pic2PathTextBox.Text;
            string widthunit = (widthUnitComBox.SelectedIndex == 1) ? "%" : "px";
            string heightunit = (heightUnitComboBox.SelectedIndex == 1) ? "%" : "px";
            Align picalign = PicAlign;
            string borderwidth = picBorderNumericUpDown.Value.ToString();
            string vspace = picVspaceNumericUpDown.Value.ToString();
            string hspace = picHspaceNumericUpDown.Value.ToString();
            //string linkurl = linkTextBox.Text;
            //string linkTitle = LinkTipValue;
            //string linkAccess = LinkAccessKeyValue;
            //string linkTarget = LinkTargetValue;
            string mediaID = MediaPath;
            image img = new image();
            insertIamgeHTML = img.ImageHtml(_insertUserMode, "pic", picpath, picwidth, widthunit, picheight.ToString(), heightunit,
            picalign, borderwidth, vspace, hspace, "", "", pic2path, "", "", "", mediaID);
            //linkTarget, linkTitle, linkAccess*/
            //_imgEle.Builder(pic2PathTextBox.Text, "",Convert.ToInt32(PicWidthNumericUpDown.Value),Convert.ToInt32(PicWidthNumericUpDown.Value), "");
            #endregion
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveConfig()
        {
            //DesignDataXmlDocument.InsertPicData data = new DesignDataXmlDocument.InsertPicData();
            //data.PicAlign = this.picAlignComboBox.SelectedIndex;
            //data.PicHSpace = this.picHspaceNumericUpDown.Value;
            //data.PicVSpace = this.picVspaceNumericUpDown.Value;
            //data.PicFrameWidth = this.picBorderNumericUpDown.Value;

            //Service.Sdsite.DesignDataDocument.HTMLDesignerInsertPicData = data;
        }


        #region 属性
        public string MediaPath { get; set; }
        public Align PicAlign
        {
            get
            {
                if (picAlignComboBox.SelectedIndex == 1) return Align.baseline;
                else if (picAlignComboBox.SelectedIndex == 2) return Align.top;
                else if (picAlignComboBox.SelectedIndex == 3) return Align.middle;
                else if (picAlignComboBox.SelectedIndex == 4) return Align.bottom;
                else if (picAlignComboBox.SelectedIndex == 5) return Align.texttop;
                else if (picAlignComboBox.SelectedIndex == 6) return Align.absolutemiddle;
                else if (picAlignComboBox.SelectedIndex == 7) return Align.absolutebottom;
                else if (picAlignComboBox.SelectedIndex == 8) return Align.left;
                else if (picAlignComboBox.SelectedIndex == 9) return Align.right;
                else return Align.Default;
            }
        }


        #endregion

        private void resizeButton_Click(object sender, EventArgs e)
        {
            Size sizeofpic = GetSizeOfPic(picIDTextBox.Text.Trim());
            if (sizeofpic.Width <= 0 || sizeofpic.Height <= 0) return;

            PicWidthNumericUpDown.Value = sizeofpic.Width;
            PicHeightNumericUpDown.Value = sizeofpic.Height;
            widthUnitComBox.SelectedIndex = heightUnitComboBox.SelectedIndex = 0;
        }

        private void widthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (widthCheckBox.Checked)
            {
                PicWidthNumericUpDown.Enabled = widthUnitComBox.Enabled = true;
            }
            else
            {
                Size sizeofpic = GetSizeOfPic(picIDTextBox.Text.Trim());
                PicWidthNumericUpDown.Value = sizeofpic.Width;
                widthUnitComBox.SelectedIndex = 0;
                PicWidthNumericUpDown.Enabled = widthUnitComBox.Enabled = false;
            }

        }

        private void heightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (heightCheckBox.Checked)
            {
                PicHeightNumericUpDown.Enabled = heightUnitComboBox.Enabled = true;
            }
            else
            {
                Size sizeofpic = GetSizeOfPic(picIDTextBox.Text.Trim());
                PicHeightNumericUpDown.Value = sizeofpic.Height;
                heightUnitComboBox.SelectedIndex = 0;
                PicHeightNumericUpDown.Enabled = heightUnitComboBox.Enabled = false;
            }
        }

        private void scaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (limitCheckBox.Checked)
                {
                    Size sizeofpic = GetSizeOfPic(picIDTextBox.Text.Trim());
                    decimal rate = Convert.ToDecimal(sizeofpic.Width) / Convert.ToDecimal(sizeofpic.Height);
                    PicHeightNumericUpDown.Value = Convert.ToInt32(PicWidthNumericUpDown.Value / rate);
                    heightUnitComboBox.SelectedIndex = widthUnitComBox.SelectedIndex;
                }
            }
            catch
            {

            }
        }

        Size GetSizeOfPic(string filename)
        {
            Size picSize = new Size(0, 0);
            if (!File.Exists(filename))
                MessageBox.Show("请先加载图片！");
            else
            {
                try
                {
                    Image upImage = Image.FromFile(filename);
                    picSize.Width = upImage.Width;
                    picSize.Height = upImage.Height;
                }
                catch (OutOfMemoryException e)
                {
                    MessageBox.Show("无效的图像格式");
                    return picSize;
                }
            }
            return picSize;
        }

        private void PicWidthNumericUpDown_Validated(object sender, EventArgs e)
        {
            if (limitCheckBox.Checked)
            {
                heightUnitComboBox.SelectedIndex = widthUnitComBox.SelectedIndex;
                PicHeightNumericUpDown.Value = Convert.ToInt32(PicWidthNumericUpDown.Value * ratio);
            }
        }

        private void PicHeightNumericUpDown_Validated(object sender, EventArgs e)
        {
            if (limitCheckBox.Checked)
            {
                widthUnitComBox.SelectedIndex = heightUnitComboBox.SelectedIndex;
                PicWidthNumericUpDown.Value = Convert.ToInt32(PicHeightNumericUpDown.Value / ratio);
            }
        }

        public static XhtmlTags.Img GetDefaultPicHtml(string picPath, string customerId)
        {
            XhtmlSection _section = new XhtmlSection();
            XhtmlTags.Img img = _section.CreateXhtmlImg();

            Image upImage = Image.FromFile(picPath);
            string width = upImage.Width.ToString();
            string height = upImage.Height.ToString();
            CssSection css = new CssSection();
            css.Properties["width"] = width;
            css.Properties["height"] = height;
            img.Builder(css, "0", "0", customerId, Xhtml.Align.left, "0", "0", "img");
            return img;
        }

    }
}
