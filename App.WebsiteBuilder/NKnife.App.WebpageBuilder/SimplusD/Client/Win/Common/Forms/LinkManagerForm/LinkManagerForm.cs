using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class LinkManagerForm : BaseForm
    {
        #region 构造函数
        public LinkManagerForm()
        {
            InitializeComponent();
        }

        public LinkManagerForm(XmlElement aElement)
        {
            InitializeComponent();
            _aElement = aElement;
            if (_aElement != null && _aElement.Attributes.Count > 0)
            {
                //通知界面
            }
        }

        #endregion

        #region 内部变量

        private XmlElement _aElement;

        #endregion

        #region 公共属性

        #endregion

        #region 公共方法

        #endregion

        #region 内部方法

        #endregion

        #region 事件响应

        /// <summary>
        /// 插入资源链接或者本地页面链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLinkResButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement xmlElement = xmlDoc.CreateElement("Name");

            ResourcesManagerForm resForm = new ResourcesManagerForm(xmlElement,true);

            if (((Button)sender).Name.Equals("openLinkResButton"))
            {
                resForm.CurrentPageIndex = 1;
            }
            else if (((Button)sender).Name.Equals("openLinkPageButton"))
            {
                resForm.CurrentPageIndex = 2;
            }
            if (resForm.ShowDialog() == DialogResult.OK)
            {
                if (xmlElement.HasAttribute("src"))
                {
                    linkAddressTextBox.Text = xmlElement.Attributes["src"].Value;
                }
            }
        }

        /// <summary>
        /// OK事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            XhtmlSection section = new XhtmlSection();
            XhtmlTags.A aTag = section.CreateXhtmlA();
            if (mainTabControl.SelectedIndex == 0)
            {
                string strUrl = "[url:]" + linkTitleTextBox.Text;
                if (!String.IsNullOrEmpty(bookmarkComboBox.Text.Trim()))
                {
                    strUrl += "#" + bookmarkComboBox.Text.Trim();
                }
                aTag.Href = strUrl;
                aTag.LinkText = linkTextTextBox.Text;
                aTag.Title = linkTitleTextBox.Text;
                aTag.Target = targetComboBox.Text;
                aTag.TabIndex = tabKeyIndexTextBox.Text;
                aTag.Accesskey = accesskeyTextBox.Text;

                if (_aElement == null) return;

                XmlElement ele = (XmlElement)aTag.ToXmlNode();
                //将原来属性删除掉,重新赋值
                _aElement.Attributes.RemoveAll();
                foreach (XmlAttribute att in ele.Attributes)
                {
                    _aElement.SetAttribute(att.Name, att.Value);
                }
            }
            else if (mainTabControl.SelectedIndex == 1)
            {
                aTag.Href = "mailto:" + emailTextTextBox.Text;

                XmlElement ele = (XmlElement)aTag.ToXmlNode();
                //将原来属性删除掉,重新赋值
                _aElement.Attributes.RemoveAll();
                if (ele.HasAttribute("href"))
                {
                    _aElement.SetAttribute("href", ele.Attributes["href"].Value);
                    XmlDocument xmlDoc = new XmlDocument();
                    //XmlText xmlText = _aElement.OwnerDocument.CreateTextNode(emailSubjectTextBox.Text);
                    XmlText xmlText = xmlDoc.CreateTextNode(emailSubjectTextBox.Text);
                    _aElement.AppendChild(xmlText);
                }
            }
            
        }

        #endregion



        #region 自定义事件

        #endregion
       
    }
}
