using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class HTMLDesignerEx : HTMLDesignerContrl
    {
        string PageId = "";
        PagePropertyPanel pageProPanel = null;

        public PagePropertyPanel PageProPanel
        {
            get { return pageProPanel; }
            set { pageProPanel = value; }
        }
        public HTMLDesignerEx(string pageId)
        {
            InitializeComponent();

            InsertUseMode = InsertMode.Id;
            PageId = pageId;
            pageProPanel = new PagePropertyPanel(PageId);

            this.DesignWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DesignWebBrowser_DocumentCompleted);
        
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        void DesignWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SdsiteXmlDocument sdDoc = Service.Sdsite.CurrentDocument;
            PageXmlDocument pageDoc = sdDoc.GetPageDocumentById(PageId);
            this.DesignWebBrowser.Document.Body.InnerHtml = pageDoc.PageText;
        }

        
        /// <summary>
        /// 页面标题
        /// </summary>
        public string PageTitle
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public bool IsModified
        {
            get 
            {
                return true;
            }
            set 
            {
            }
        }


        public string PageText
        {
            get
            {
                return this.CodeRichText.Text;
            }
            set
            {
                this.CodeRichText.Text = value;
            }
        }

        public override string OpenPicDialog()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement ele = xmlDoc.CreateElement("img");
            ResourcesManagerForm res = new ResourcesManagerForm(ele,false);

            if (res.ShowDialog() == DialogResult.OK)
            {
                return ele.OuterXml;
            }
            else
                return "";
        }

        public override string OpenFlashDialog()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement ele = xmlDoc.CreateElement("object");
            ResourcesManagerForm res = new ResourcesManagerForm(ele, false);
            //res.
            if (res.ShowDialog() == DialogResult.OK)
            {
                return ele.OuterXml;
            }
            else
                return "";
        }

        public override string OpenAudioDialog()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement ele = xmlDoc.CreateElement("object");
            ResourcesManagerForm res = new ResourcesManagerForm(ele, false);
            //res.
            if (res.ShowDialog() == DialogResult.OK)
            {
                return ele.OuterXml;
            }
            else
                return "";
        }

        public override object InsertLink()
        {
            LinkManagerForm linkForm = new LinkManagerForm();
            if (linkForm.ShowDialog() == DialogResult.OK)
            {
            }
            return ""; ;
        }

        public override string OpenMediaDialog()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement ele = xmlDoc.CreateElement("object");
            ResourcesManagerForm res = new ResourcesManagerForm(ele, false);
            //res.
            if (res.ShowDialog() == DialogResult.OK)
            {
                return ele.OuterXml;
            }
            else
                return "";
        }

        public override string GetResourceAbsolutePath(string picId)
        {
            SdsiteXmlDocument sdDoc = Service.Sdsite.CurrentDocument;
            AnyXmlElement anyEle = sdDoc.GetElementById(picId);
            FileSimpleExXmlElement fileEle = anyEle as FileSimpleExXmlElement;
            return fileEle.AbsoluteFilePath;
        }
    }
}