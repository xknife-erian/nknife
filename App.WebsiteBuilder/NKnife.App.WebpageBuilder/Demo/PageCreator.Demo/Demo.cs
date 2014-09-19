using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Jeelu.SimplusD;
using Jeelu;

namespace PageCreator.Demo
{
    public partial class Demo : Form
    {

        public Demo()
        {
            InitializeComponent();

            #region 便于快速打开一个SDSITE

            XmlDocument doc = new XmlDocument();
            doc.Load(Log.LogFile);
            XmlElement ele = (XmlElement)doc.DocumentElement.SelectSingleNode("//sdsiteFile");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开一个Sdsite项目文件";
            ofd.Filter = "Sdsite项目文件(*.sdsite)|*.sdsite";
            if (!string.IsNullOrEmpty(ele.InnerText))
            {
                ofd.InitialDirectory = Path.GetDirectoryName(ele.InnerText);
            }
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                ele.InnerText = ofd.FileName;
                this._SdFile = ofd.FileName;
                this.tmpltComboBox.SelectedIndexChanged += new EventHandler(tmpltComboBox_SelectedIndexChanged);
                this.pageComboBox.SelectedIndexChanged += new EventHandler(pageComboBox_SelectedIndexChanged);
                /// 初始化一个SD项目以供测试
                this.InitSdSite();
            }
            else if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                this.Close();
            }

            #endregion

        }
        /// <summary>
        /// 测试的SD项目的SdsiteXmlDocument的实体文件
        /// </summary>
        string _SdFile = "";
        /// <summary>
        /// 测试的SD项目的SdsiteXmlDocument
        /// </summary>
        public SdsiteXmlDocument InnerDoc { get; set; }

        /// <summary>
        /// 初始化一个SD项目以供测试
        /// </summary>
        private void InitSdSite()
        {
            PageAttributeService.Initialize();
            ToHtmlHelper htmlhelper = new ToHtmlHelper(_SdFile, @"d:\_abc\myabc");
            this._htmlHelper = htmlhelper;
            this.InnerDoc = htmlhelper.SdsiteXmlDocument;

            string[] pageIds = this.InnerDoc.GetAllPageId();
            foreach (var item in pageIds)
            {
                this.pageComboBox.Items.Add(new PageObject(item, htmlhelper));
            }

            string[] tmpltIds = this.InnerDoc.GetAllTmpltId();
            foreach (string id in tmpltIds)
            {
                this.tmpltComboBox.Items.Add(new TmpltObject(id, htmlhelper));
            }

            this.sdsiteLabel.Text = _SdFile;
        }

        private ToHtmlHelper _htmlHelper;

        void tmpltComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TmpltHtmlCreator(((TmpltObject)this.tmpltComboBox.SelectedItem).GetTmplt());
        }


        void pageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.codeTextBox.Clear();
            this.outXmlTextBox.Clear();
            this.tree.Nodes.Clear();

            string id = ((PageObject)this.pageComboBox.SelectedItem).Id;
            string title = ((PageObject)this.pageComboBox.SelectedItem).title;

            string tmpltid = this.InnerDoc.GetPageElementById(id).TmpltId;
            SnipXmlElement snip = this.InnerDoc.GetTmpltDocumentById(tmpltid).GetContentSnipEle();
            //snip.ToHtmlPage = this.InnerDoc.GetPageDocumentById(id);

            bool b = this.InnerDoc.GetPageDocumentById(id).SaveXhtml(this._htmlHelper);

            MessageBox.Show("Save Page:\r\n(" + id + ") \r\n" + title + "\r\n" + b.ToString());
        }


        private void TmpltHtmlCreator(TmpltXmlDocument tmpltdoc)
        {
            this.codeTextBox.Clear();
            this.outXmlTextBox.Clear();
            this.tree.Nodes.Clear();

            string source = tmpltdoc.GetRectsElement().OuterXml.Replace(">", ">\r\n");
            if (tmpltdoc.SaveXhtml(this._htmlHelper))
            {
                string html = File.ReadAllText(tmpltdoc.HtmlFile).Replace(">", ">\r\n");
                this.codeTextBox.Text = html;
            }

            this.outXmlTextBox.Text = source;

            TreeNode treenode = new TreeNode();
            this.SetTmpltTree(tmpltdoc.GetRectsElement(), treenode);
            this.tree.Nodes.Add(treenode);
            this.tree.ExpandAll();
        }

        int i = 0;

        private void SetTmpltTree(AnyXmlElement anyXmlElement, TreeNode treenode)
        {
            ToHtmlXmlElement ele = (ToHtmlXmlElement)anyXmlElement;
            i++;
            TreeNode newNode = new TreeNode(ele.LocalName);
            treenode.Nodes.Add(newNode);
            if (ele.SaveXhtml(this._htmlHelper))
            {
                SnipXmlElement snip = (SnipXmlElement)anyXmlElement;
                StreamReader sr = new StreamReader(snip.HtmlFile);
                string content = sr.ReadToEnd().Replace(">", ">\r\n");
                sr.Close();
                sr.Dispose();


                newNode.ToolTipText = content;
            }
            else
            {
                newNode.ToolTipText = "Don't must be creat file!";
            }
            foreach (XmlNode node in ele.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                if (!(node is ToHtmlXmlElement))
                {
                    continue;
                }
                SetTmpltTree((AnyXmlElement)node, newNode);
            }
        }

        private void 快速测试F5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XhtmlSection sec = new XhtmlSection();
            XhtmlTags.Img img = sec.CreateXhtmlImg();
            img.Builder(new CssSection("css"), "alt", "hspace", "src", Xhtml.Align.bottom, "vspace", "1", "name");
            this.codeTextBox.Text = img.ToString();


            XhtmlTags.A a = sec.CreateXhtmlA();
            a.Builder("a", "title", Xhtml.Target._parent, 1, 't');
            this.outXmlTextBox.Text = a.ToString();
        }

        #region MyRegion
        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }

    static class Log
    {
        static FileStream _fileStream;
        static public string LogFile
        {
            get
            {
                string file = Application.ExecutablePath + ".log";
                if (!File.Exists(file))
                {
                    _fileStream = File.Create(file);
                    XmlTextWriter xtw = new XmlTextWriter(_fileStream, Encoding.UTF8);
                    xtw.Formatting = Formatting.Indented;
                    xtw.WriteStartDocument();
                    xtw.WriteStartElement("root");
                    xtw.WriteElementString("sdsiteFile", "");
                    xtw.WriteEndElement();

                    xtw.Close();
                    _fileStream.Close();
                }
                return file;
            }
        }
        static public void ProcessException(Exception e)
        {
            WriteLine(GetExceptionMsg(e));
        }
        static public string GetExceptionMsg(Exception e)
        {
            string msg = string.Format("异常信息:{0}\r\n异常类型:{1}\r\n堆栈信息:\r\n{2}\r\n",
                e.Message, e.GetType().FullName, e.StackTrace);
            return msg;
        }
        static public void Open()
        {
            _fileStream = new FileStream(LogFile, FileMode.Append, FileAccess.Write);
        }
        static public void WriteLine(string msg)
        {
            lock (_fileStream)
            {
                try
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(msg + "\r\n");
                    _fileStream.Write(buffer, 0, buffer.Length);
                }
                catch { }
            }
        }
        static public void Close()
        {
            _fileStream.Flush();
            _fileStream.Close();
        }
        static public void Flush()
        {
            try
            {
                _fileStream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetExceptionMsg(ex));
            }
        }
    }

    class TmpltObject
    {
        public TmpltObject(string id, ToHtmlHelper htmlhelper)
        {
            this._ToHtmlHelper = htmlhelper;
            this.Id = id;
        }
        private ToHtmlHelper _ToHtmlHelper;
        public string Id { get; private set; }
        public string title
        {
            get { return this._ToHtmlHelper.SdsiteXmlDocument.GetTmpltDocumentById(this.Id).Title; }
        }
        public TmpltXmlDocument GetTmplt()
        {
            return this._ToHtmlHelper.SdsiteXmlDocument.GetTmpltDocumentById(this.Id);
        }
        public override string ToString()
        {
            return title;
        }
    }
    class PageObject
    {
        public PageObject(string id, ToHtmlHelper htmlhelper)
        {
            this._ToHtmlHelper = htmlhelper;
            this.Id = id;
        }
        private ToHtmlHelper _ToHtmlHelper;
        public string Id { get; private set; }
        public string title
        {
            get { return this._ToHtmlHelper.SdsiteXmlDocument.GetPageDocumentById(this.Id).Title; }
        }
        public PageXmlDocument GetPage()
        {
            return this._ToHtmlHelper.SdsiteXmlDocument.GetPageDocumentById(this.Id);
        }
        public override string ToString()
        {
            return title;
        }
    }
}
