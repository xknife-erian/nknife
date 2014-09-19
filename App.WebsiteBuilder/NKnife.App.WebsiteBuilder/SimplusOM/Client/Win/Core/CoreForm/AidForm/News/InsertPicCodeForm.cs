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

namespace Jeelu.SimplusOM.Client
{
    public partial class frmInsertPicCode : Form
    {
        string linkUrlValue = "";
        string linkTargetValue = "";

        public string LinkTargetValue
        {
            get { return linkTargetValue; }
            set { linkTargetValue = value; }
        }
        string linkAccessKeyValue = "";

        public string LinkAccessKeyValue
        {
            get { return linkAccessKeyValue; }
            set { linkAccessKeyValue = value; }
        }
        string linkTipValue = "";

        public string LinkTipValue
        {
            get { return linkTipValue; }
            set { linkTipValue = value; }
        }
        string ContFrmNam = string.Empty;
        decimal ratio = 1;

        #region 构造函数

        public frmInsertPicCode()
        {
            InitializeComponent();

           // picAlignComboBox.SelectedIndex = 0;
            widthUnitComBox.SelectedIndex = heightUnitComboBox.SelectedIndex = 0;

            this.ImeMode = ImeMode.On;
        }

        public frmInsertPicCode(string frmNam)
        {
            InitializeComponent();
            ContFrmNam = frmNam;

        }

        #endregion

        private void picPathBtn_Click(object sender, EventArgs e)
        {

            OpenFileDialog openD = new OpenFileDialog();
            openD.Filter = "PIC files (*.jpg,*.png,*.gif)|*.jpg;*.png;*.gif;";
            if (openD.ShowDialog() == DialogResult.OK)
            {
                picPathTextBox.Text = openD.FileName;
                Size sizeofpic = GetSizeOfPic(picPathTextBox.Text.Trim());
                PicWidthNumericUpDown.Value = sizeofpic.Width;
                PicHeightNumericUpDown.Value = sizeofpic.Height;
                ratio = Convert.ToDecimal(sizeofpic.Width) / Convert.ToDecimal(sizeofpic.Height);
                widthUnitComBox.SelectedIndex = heightUnitComboBox.SelectedIndex = 0;

                this.widthCheckBox.Checked = true;
                this.heightCheckBox.Checked = true;
                this.limitCheckBox.Enabled = true;
                this.limitCheckBox.Checked = true;
            }

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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void picLinkBtn_Click(object sender, EventArgs e)
        {
            showLinkUrl();
        }

        private void showLinkUrl()
        {
            //frmInsertLinkCode linkFrm = new frmInsertLinkCode();
            //linkFrm.ShowDialog();
            //if (linkFrm.DialogResult == DialogResult.OK)
            //{
            //    linkUrlValue = linkFrm.linkUrl;
            //    this.linkTextBox.Text = linkUrlValue;
            //    linkTargetValue = linkFrm.linkTarget;
            //    linkAccessKeyValue = linkFrm.AccessKey;
            //    linkTipValue = linkFrm.LinkTip;
            //}
        }

        #region 属性
        public string MediaID { get; set; }
        public int PicBorder
        {
            get { return Convert.ToInt32(picBorderNumericUpDown.Value); }
            set { picBorderNumericUpDown.Value = value; }
        }
        public int PicVspace
        {
            get { return Convert.ToInt32(picVspaceNumericUpDown.Value); }
            set { picVspaceNumericUpDown.Value = value; }
        }
        public int PicHspace
        {
            get { return Convert.ToInt32(picHspaceNumericUpDown.Value); }
            set { picHspaceNumericUpDown.Value = value; }
        }

        public string HeightUnit
        {
            get
            {
                if (heightUnitComboBox.SelectedIndex == 1) return "%";
                else return "px";
            }
            set 
            {
                switch (value)
                {
                    case "%": heightUnitComboBox.SelectedIndex = 1; break;
                    default: heightUnitComboBox.SelectedIndex = 0; break;
                        
                }
            }
        }
        public string WidthUnit
        {
            get
            {
                if (widthUnitComBox.SelectedIndex == 1) return "%";
                else return "px";
            }
            set
            {
                switch (value)
                {
                    case "%": widthUnitComBox.SelectedIndex = 1; break;
                    default: widthUnitComBox.SelectedIndex = 0; break;

                }
            }
        }
        public int PicHeight
        {
            get { return Convert.ToInt32(PicHeightNumericUpDown.Value); }
            set { PicHeightNumericUpDown.Value = value; }
        }
        public int PicWidth
        {
            get { return Convert.ToInt32(PicWidthNumericUpDown.Value); }
            set { PicWidthNumericUpDown.Value = value; }
        }
        public string Link
        {
            get { return linkTextBox.Text; }
            set { linkTextBox.Text = value; }
        }
        public string ReplaceWord
        {
            get { return replaceWordTextBox.Text; }
            set { replaceWordTextBox.Text = value; }
        }
        public string PicPath
        {
            get { return picPathTextBox.Text; }
            set { picPathTextBox.Text = value; }
        }

        public string Pic2Path
        {
            get { return pic2PathTextBox.Text; }
            set { pic2PathTextBox.Text = value; }
        }

        public image.ImageAlign PicAlign
        {
            get
            {
                if (picAlignComboBox.SelectedIndex == 1) return image.ImageAlign.BaseLine;
                else if (picAlignComboBox.SelectedIndex == 2) return image.ImageAlign.Top;
                else if (picAlignComboBox.SelectedIndex == 3) return image.ImageAlign.Middle;
                else if (picAlignComboBox.SelectedIndex == 4) return image.ImageAlign.Bottom;
                else if (picAlignComboBox.SelectedIndex == 5) return image.ImageAlign.TextTop;
                else if (picAlignComboBox.SelectedIndex == 6) return image.ImageAlign.AbsoluteMiddle;
                else if (picAlignComboBox.SelectedIndex == 7) return image.ImageAlign.AbsoluteBottom;
                else if (picAlignComboBox.SelectedIndex == 8) return image.ImageAlign.Left;
                else if (picAlignComboBox.SelectedIndex == 9) return image.ImageAlign.Right;
                else return image.ImageAlign.Default;
            }
            /* set {
                 switch (value)
                 {
                     case "":
                 picAlignComboBox.Text = value.ToString(); 
             }}*/
        }
        #endregion

        private void resizeButton_Click(object sender, EventArgs e)
        {
            Size sizeofpic = GetSizeOfPic(picPathTextBox.Text.Trim());
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
                Size sizeofpic = GetSizeOfPic(picPathTextBox.Text.Trim());
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
                Size sizeofpic = GetSizeOfPic(picPathTextBox.Text.Trim());
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
                    Size sizeofpic = GetSizeOfPic(picPathTextBox.Text.Trim());
                    decimal rate = Convert.ToDecimal(sizeofpic.Width) / Convert.ToDecimal(sizeofpic.Height);
                    PicHeightNumericUpDown.Value = Convert.ToInt32(PicWidthNumericUpDown.Value * rate);
                    heightUnitComboBox.SelectedIndex = widthUnitComBox.SelectedIndex;
                }
            }
            catch
            {

            }
        }

        Size GetSizeOfPic(string filename)
        {
            Size s = new Size(0, 0);
            if (!File.Exists(filename))
                //MessageBox.Show("请先加载图片！");
                MessageService.Show("${res:tmpltDesignTip.loadPic}", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
            {
                try
                {
                    Image upImage = Image.FromFile(filename);
                    s.Width = upImage.Width;
                    s.Height = upImage.Height;
                }
                catch (OutOfMemoryException e)
                {
                    MessageBox.Show("无效的图像格式");
                    return s;
                }
            }
            return s;
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

    }
}
