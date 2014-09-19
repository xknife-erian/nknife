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
using System.Threading;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class PagePropertyForm : BaseForm
    {
        private PagePropertyHelper mgr;

        private string _pageID;

        public string PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }
        private string _pageText;
        public string PageText
        {
            get { return _pageText; }
            set { _pageText = value; }
        }

        public PagePropertyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 内容页面资料处理
        /// </summary>
        /// <param name="filePath">文件路径, string pageText</param>
        /// <param name="fileContent">文件的正文</param>
        public PagePropertyForm(string pageId)
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

        private void ContentPropertyForm_Load(object sender, EventArgs e)
        {
            isFirst = true;
            LoadingForm();
        }

        private void txtContentTag_TextChanged(object sender, EventArgs e)
        {
            if (this.txtContentTag.Text != "")
            {
                //TipService.HideErrorTip(this.txtContentTag);
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            SubmitPageContentInfo();
        }

        bool isTip;
        private void creatDTPicker_ValueChanged(object sender, EventArgs e)
        {
            string getStartPubTime = this.txtBeginPubTime.Text;
            if (getStartPubTime != "" && string.Compare(getStartPubTime, "0000-00-00 00:00:00") != 0)
            {
                ///开始发布时间已 存在
                if (this.creatDTPicker.Value > Convert.ToDateTime(getStartPubTime))
                {
                    //TipService.ShowErrorTip(GetTextResource("timeTip"), this.creatDTPicker);
                    isTip = true;
                }
                else
                {
                    //TipService.HideErrorTip(this.creatDTPicker);
                    isTip = false;
                }
            }
            else
            {
                //没有开始发布时间,此时让结束发布时间与开始时间比较
                if (this.creatDTPicker.Value > this.contPubEndDTPicker.Value)
                {
                    //TipService.ShowErrorTip(GetTextResource("timeTip"), this.contPubEndDTPicker);
                    isTip = true;
                }
                else
                {
                    //TipService.HideErrorTip(this.contPubEndDTPicker);
                    isTip = false;
                }
            }
        }
        bool isFirst;
        private void isAlwaysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.contPubEndDTPicker.Enabled = this.isAlwaysPubCheckBox.Checked ? false : true;

            if (this.contPubEndDTPicker.Enabled == false)
            {
                isTip = false;
                //TipService.HideErrorTip(this.contPubEndDTPicker);
            }
            else
            {
                EndPubTime();
            }

        }

        private void contPubEndDTPicker_ValueChanged(object sender, EventArgs e)
        {
            EndPubTime();
        }

        /// <summary>
        /// "结束发布时间"验证
        /// </summary>
        private void EndPubTime()
        {
            if (this.contPubEndDTPicker.Enabled == true)
            {
                if (this.contPubEndDTPicker.Value <= this.creatDTPicker.Value
                    || (string.Compare(this.txtBeginPubTime.Text, "0000-00-00 00:00:00") != 0
                    && this.contPubEndDTPicker.Value <= Convert.ToDateTime(this.txtBeginPubTime.Text)))
                {
                    //文本提示
                    //TipService.ShowErrorTip(GetTextResource("timeTip"), this.contPubEndDTPicker);
                    isTip = true;
                }
                else
                {
                    if (isFirst)
                    {
                        //TipService.HideErrorTip(contPubEndDTPicker);
                        isTip = false;
                        isFirst = false;
                    }
                    else
                    {
                        //TipService.CloseErrorTip(this.contPubEndDTPicker);
                        isTip = false;
                    }
                }
            }
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

            this.isAlwaysPubCheckBox.Checked = prop.IsAlwaysPub ? true : false;

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
            prop.IsAlwaysPub = this.isAlwaysPubCheckBox.Checked ? true : false;

            if (!isAlwaysPubCheckBox.Checked)
            {
                prop.EndPubTime = this.contPubEndDTPicker.Text.ToString();
            }
            else
            {
                prop.EndPubTime = contPubEndDTPicker.MaxDate.ToString();
            }
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
        private void SubmitPageContentInfo()
        {
            PageTextPropertyItem property = new PageTextPropertyItem();
            ReadControlValue(property);

            ///查看是否出现时间的错误提示
            //if (isTip)
            //{
            //   // MessageBox.Show(GetTextResource("timeErrorTip"));
            //}
            //else
            {
                ///在进行控件的验证
                //Control errorControl = null;
                //if (this.ValidateForm(out errorControl))
                //{
                //    Service.HistoryInput.AddValue(this.authorByNameCombo.Text, HistoryInputRecordType.CreaterBy);
                //    Service.HistoryInput.AddValue(this.modifyByComboBox.Text, HistoryInputRecordType.ModifyBy);
                //    Service.HistoryInput.AddValue(this.contentSourceComboBox.Text, HistoryInputRecordType.ContentSource);
                //    mgr.WritePageTextProp(property, _pageID);
                //    mgr.SaveXmlDocument();

                //    this.Close();
                //}
                //else
                //{
                //    Control.ControlCollection ctrls = this.Controls;
                //    if (ctrls.Contains(errorControl))
                //    {
                //        errorControl.Focus();
                //    }
                //}
            }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }



    }

}