using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.Win
{
    public partial class InsertLinkCodeForm : BaseForm
    {
        private string getLinkUrl = "";
        private string getBookMark = "";
        private string getTarget = "";
        private string getAccessKey = "";
        private string getLinkTip = "";

        public InsertLinkCodeForm()
        {
            InitializeComponent();
            openHTMLfileBtn.Image =null;// ResourceService.GetResourceImage("link");
            openDocumentBtn.Image = null;// ResourceService.GetResourceImage("MainMenu.file.open");
            openEmailBtn.Image = null;// ResourceService.GetResourceImage("E_mail");

            LinkTargetComboBox.SelectedIndex = 0;
            this.ImeMode = ImeMode.On;
        }

        private void frmInsertLinkCode_Load(object sender, EventArgs e)
        {
            initializeLinkUI();
        }

        private void initializeLinkUI()
        {
        }

        private void picLinkPathTextBox_TextChanged(object sender, EventArgs e)
        {
            this.getLinkUrl = this.picLinkPathTextBox.Text;
        }

        private void bookmarkComboBox_TextChanged(object sender, EventArgs e)
        {
            getBookMark = bookmarkComboBox.Text;
            if (getBookMark != "")
            {
                getLinkUrl = picLinkPathTextBox.Text + "#" + getBookMark;
            }
            else 
            {
                getLinkUrl = picLinkPathTextBox.Text;
            }
        }

        private void LinkTargetComboBox_TextChanged(object sender, EventArgs e)
        {
            this.getTarget = this.LinkTargetComboBox.Text;
        }

        private void linkAccesskeyComboBox_TextChanged(object sender, EventArgs e)
        {
            this.getAccessKey = this.linkAccesskeyComboBox.Text;
        }

        private void linkTipTextBox_TextChanged(object sender, EventArgs e)
        {
            this.getLinkTip = this.linkTipTextBox.Text;
        }

        public string linkUrl
        {
            get 
            {
                return getLinkUrl;
            }
            set
            {
                picLinkPathTextBox.Text = value;
            }
        }

        public string linkTarget
        {
            get
            {
                return LinkTargetComboBox.Text;
            }
            set
            {
                LinkTargetComboBox.Text = value;
            }
        }

        public string AccessKey
        {
            get
            {
                return getAccessKey;
            }
            set
            {
                linkAccesskeyComboBox.Text = value;
            }
        }

        public string LinkTip
        {
            get
            {
                return getLinkTip;
            }
            set
            {
                linkTipTextBox.Text = value;
            }
        }

        public string BookMark
        {
            get
            {
                return bookmarkComboBox.Text;
            }
            set
            {
                bookmarkComboBox.Text = value;
            }
        }

        private void newOpenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                LinkTargetComboBox.SelectedIndex = 2;
                newformBtn.Enabled = true;
            }
            else
            {
                newformBtn.Enabled = false;
            }

        }

        private void newformBtn_Click(object sender, EventArgs e)
        {
            //frmNewWinOption newwin = new frmNewWinOption();
            //if (newwin.ShowDialog() == DialogResult.OK)
            //{
 
            //}
        }

        private void picPathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            string str = "";
            str+="Web Docment(*.htm;*.html;*.shtm;*.shtml)|*.htm;*.html;*.shtm;*.shtml|";
            str+="HTML Files(*.htm;*.html)|*.htm;*.html|";
            str+="SHTML Files(*.shtm;*.html)|*.shtm;*.html|";
            str += "MHTML Files(*.mht)|*.mht|";
            str += "XHTML Files(*.htm,*.html,*.xhtml)|*.htm,*.html,*.xhtml";
            op.Filter = str; 
                //txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                picLinkPathTextBox.Text = op.FileName;
            }
        }

        private void openDocumentBtn_Click(object sender, EventArgs e)
        {
            //string resourceId = SiteResourceService.SelectResource(MediaFileType.Pic,this);

            //if (!string.IsNullOrEmpty(resourceId))
            //{
            //    SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            //    FileSimpleExXmlElement fileEle = doc.GetElementById(resourceId) as FileSimpleExXmlElement;
            //    picLinkPathTextBox.Text = fileEle.RelativeFilePath;

            //}
        }

        private void openEmailBtn_Click(object sender, EventArgs e)
        {
            InsertEmailCodeForm insertemail = new InsertEmailCodeForm();
            if (insertemail.ShowDialog() == DialogResult.OK)
            {
                string addr = insertemail.TextMailto;
                string sub = insertemail.TextSubject;
                string urlstr = "";
                if (addr != "")
                {
                    urlstr += "mailto:" + addr;
                    if (sub != "")
                    {
                        urlstr += "?subject=" + sub;
                    }
                }
                picLinkPathTextBox.Text = urlstr;
            }

        }



    }
}