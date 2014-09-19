using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using mshtml;

namespace Jeelu.SimplusOM.Client
{
    public partial class NewsForm : Form
    {
        IHTMLDocument2 idoc2 = null;
        string _content = string.Empty;

        public NewsForm()
        {
            InitializeComponent();
        }

        private void NewsForm_Load(object sender, EventArgs e)
        {
            InitDesignWebBrowser();
            newColComboBox.SelectedIndex = 0;

            idoc2.execCommand("FontName", true, "宋体");
            idoc2.execCommand("FontSize", true, "12px");
        }


        /// <summary>
        /// 标题
        /// </summary>
        public string NewsTitle
        {
            get
            {
                return titleTextBox.Text;
            }
            set
            {
                titleTextBox.Text=value;
            }
        }
        /// <summary>
        /// 栏目
        /// </summary>
        public int NewsCol
        {
            get
            {
                return newColComboBox.SelectedIndex + 1;
            }
            set
            {
                newColComboBox.SelectedIndex = value - 1;
            }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime NewsPublishTime
        {
            get
            {
                return pulishTimeDTP.Value;
            }
            set
            {
                pulishTimeDTP.Value = value;
            }
        }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent
        {
            get
            {
                return idoc2.body.innerHTML;
            }
            set
            {
               _content = value;
            }
        }


        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }



        public void InitDesignWebBrowser()
        {
            string strDocumentText = "<html><head>";
            strDocumentText += "<link href=\"http://css.jeelu.com/Style/custom/jeelu/all.css\" rel=\"stylesheet\" type=\"text/css\" />";
            strDocumentText += "<link href=\"http://css.jeelu.com/Style/custom/jeelu/news/style.css\" rel=\"stylesheet\" type=\"text/css\" />";
            strDocumentText += "</head><body></body></html>";
            contentWebBrowser.DocumentText = strDocumentText;
            idoc2 = (IHTMLDocument2)contentWebBrowser.Document.DomDocument;
            idoc2.designMode = "on";
            contentWebBrowser.IsWebBrowserContextMenuEnabled = false;
            ((Control)contentWebBrowser).ImeMode = ImeMode.On;
            contentWebBrowser.DocumentText = _content;
        }




        public bool AddToSelection(IHTMLDocument2 m_pDoc2, string s_BeginHtml, string s_EndHtml)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLSelectionObject sel = m_pDoc2.selection as IHTMLSelectionObject;
            IHTMLElement ele = m_pDoc2.activeElement as IHTMLElement;
            if (sel == null)
                return false;
            IHTMLTxtRange range = sel.createRange() as IHTMLTxtRange;
            if (range == null)
                return false;
            string shtml = string.Empty;
            if (!string.IsNullOrEmpty(s_BeginHtml))
            {
                shtml = s_BeginHtml + range.htmlText + s_EndHtml;
            }

            range.pasteHTML(shtml);
            range.select();

            return true;
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddImgBtn_Click(object sender, EventArgs e)
        {
            InsertImage(idoc2);
        }

        /// <summary>
        /// 添加图片方法
        /// </summary>
        /// <param name="m_pDoc2"></param>
        public void InsertImage(IHTMLDocument2 m_pDoc2)
        {
            frmInsertPicCode insertpic = new frmInsertPicCode();
            if (insertpic.ShowDialog() == DialogResult.OK)
            {
                FtpUpDown ftpUpDown = new FtpUpDown("218.246.34.205", "test", "abcd1234");
                ftpUpDown.Upload(insertpic.PicPath);

                //不管如何,只要存在此文件则加入到HTML编辑器中
                string picwidth =Path.Combine("http://www.jobusy.cn/" ,Path.GetFileName(insertpic.PicPath));
                string picheight = insertpic.PicHeight.ToString();
                string picpath = insertpic.PicPath;
                string pic2path = insertpic.Pic2Path;
                string widthunit = insertpic.WidthUnit;
                string heightunit = insertpic.HeightUnit;
                image.ImageAlign picalign = insertpic.PicAlign;
                string borderwidth = insertpic.PicBorder.ToString();
                string vspace = insertpic.PicVspace.ToString();
                string hspace = insertpic.PicHspace.ToString();
                string linkurl = insertpic.Link;
                string linkTitle = insertpic.LinkTipValue;
                string linkAccess = insertpic.LinkAccessKeyValue;
                string linkTarget = insertpic.LinkTargetValue;
                string mediaID = insertpic.MediaID;
                image img = new image();
                string insertimghtml = img.ImageHtml("pic", picpath, picwidth, widthunit, picheight.ToString(), heightunit,
                picalign, borderwidth, vspace, hspace, "", linkurl, pic2path, linkTarget, linkTitle, linkAccess, mediaID);
                AddToSelection(m_pDoc2, insertimghtml, "");
            }
        }

        /// <summary>
        /// 添加<P>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPBtn_Click(object sender, EventArgs e)
        {
            AddToSelection(idoc2, "<p>", "</p>");
        }

        /// <summary>
        /// 添加<B>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBBtn_Click(object sender, EventArgs e)
        {
            AddToSelection(idoc2, "<b>", "</b>");
        }



    }
}
