using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class PagePropertyPanel : BaseUserControl
    {
        /// <summary>
        /// 内容页面资料处理
        /// </summary>
        /// <param name="filePath">文件路径, string pageText</param>
        /// <param name="fileContent">文件的正文</param>
        public PagePropertyPanel(string pageId)
        {
            InitializeComponent();
            _pageID = pageId;
            PageSimpleExXmlElement pageEle = (PageSimpleExXmlElement)Service.Sdsite.CurrentDocument.GetElementById(pageId);
            string filePath = pageEle.AbsoluteFilePath;
            PageXmlDocument pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById(pageId);
            if (pageDoc == null)
            {
                ///读取文章正文]
                mgr = new PagePropertyHelper(pageId);
                _pageText = mgr.ReadPageText();
            }
            else
            {
                mgr = new PagePropertyHelper(pageId);
                _pageText = pageDoc.PageText;
            }
        }

        private PagePropertyHelper mgr;

        private string _pageID;
        private string _pageText;
        public string PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }
        public string PageText
        {
            get { return _pageText; }
            set { _pageText = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadingForm();
        }

        #region 函数实现
        /// <summary>
        /// 加载窗体时前
        /// </summary>
        private void LoadingForm()
        {
            PageTextPropertyItem property = mgr.ReadPageTextProp();
            WriteControlValue(property);
            ReadAuthorAlias();
            ReadModifyBy();
            ReadContentSource();

        }
        /// <summary>
        /// 读出当前用户所使用到的作者别名
        /// </summary>
        private void ReadAuthorAlias()
        {
            List<string> lt = Service.HistoryInput.GetValues(HistoryInputRecordType.CreaterBy);
            this.authorByNameCombo.Items.AddRange(lt.ToArray());
        }
        /// <summary>
        /// 读出当前用户所使用到的修改者别名
        /// </summary>
        private void ReadModifyBy()
        {
            List<string> lt = Service.HistoryInput.GetValues(HistoryInputRecordType.ModifyBy);
            this.modifyByComboBox.Items.AddRange(lt.ToArray());
        }
        /// <summary>
        /// 读出文章的所有来源
        /// </summary>
        private void ReadContentSource()
        {
            List<string> lt = Service.HistoryInput.GetValues(HistoryInputRecordType.ContentSource);
            this.contentSourceComboBox.Items.AddRange(lt.ToArray());
        }
        /// <summary>
        /// 写入控件的值
        /// </summary>
        private void WriteControlValue(PageTextPropertyItem prop)
        {
            this.txtTitle.Text = prop.Title;
            this.txtTitleAlias.Text = prop.TitleAlias;

            this.authorByNameCombo.Text = prop.AuthorAlias;
            this.modifyByComboBox.Text = prop.ModifyBy;
            this.txtDesignSummary.Text = prop.DesignSummary;
            this.txtSummary.Text = prop.Summary;
            this.contentSourceComboBox.Text = prop.ContentSource;
            ///创建时间
            if (prop.DeliverTime != "")
            {
                DateTime dateTime;
                if (!DateTime.TryParseExact(prop.DeliverTime, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out dateTime)) ///验证时间的格式是否正确
                {
                    dateTime = DateTime.Now;
                }
                this.creatDTPicker.Value = dateTime; ///此时的创建时间都已经开始比较了，与另外两个时间
            }
            else
            {
                this.creatDTPicker.Value = DateTime.Now;
            }

            ///将读出的关键字显示在列表框中
            StringBuilder sb = new StringBuilder();
            if (prop.tag.Count > 0)
            {
                foreach (string str in prop.tag)
                {
                    sb.Append(str);
                    sb.Append(",");
                }
                string tags = sb.ToString().Remove(sb.Length - 1);
                this.txtContentTag.Text = tags;
            }
        }
        /// <summary>
        /// 读取控件的值
        /// </summary>
        private void ReadControlValue(PageTextPropertyItem prop)
        {
            prop.Title = this.txtTitle.Text;
            prop.TitleAlias = this.txtTitleAlias.Text;
            prop.DeliverTime = this.creatDTPicker.Text;
            prop.AuthorAlias = this.authorByNameCombo.Text;
            prop.ModifyBy = this.modifyByComboBox.Text;
            prop.DesignSummary = this.txtDesignSummary.Text;
            prop.Summary = this.txtSummary.Text;

            prop.ContentSource = this.contentSourceComboBox.Text;

            ///读取关键字
            prop.tag = ReadControlKeyWords();


        }
        /// <summary>
        /// 读取控件中的关键字 细分析
        /// </summary>
        /// <param name="propTag"></param>
        private List<string> ReadControlKeyWords()
        {
            List<string> tags = new List<string>();
            string getKeys = this.txtContentTag.Text;

            //分析并返回
            string[] charSeparators = new string[] { "，", ",", "\r\n", " ", "" };
            string[] words = getKeys.Split(charSeparators, StringSplitOptions.None);
            List<string> list = new List<string>();
            foreach (var item in words)
            {
                if (item != "")
                {
                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// 保存所有的操作
        /// </summary>
        public void SubmitPageContentInfo()
        {
            PageTextPropertyItem property = new PageTextPropertyItem();
            ReadControlValue(property);
            Service.HistoryInput.AddValue(this.authorByNameCombo.Text, HistoryInputRecordType.CreaterBy);
            Service.HistoryInput.AddValue(this.modifyByComboBox.Text, HistoryInputRecordType.ModifyBy);
            Service.HistoryInput.AddValue(this.contentSourceComboBox.Text, HistoryInputRecordType.ContentSource);
            mgr.WritePageTextProp(property, _pageID);
            mgr.SaveXmlDocument();
        }

        #endregion


    }
}
