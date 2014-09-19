using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{
    public partial class NewSnipSetupForm : BaseForm
    {
        #region 内部变量

        string _initSnipWidth;

        XmlDocument _doc;

        SnipRect _snipRect;

        bool _parentHasContentSnip = false;

        #endregion

        #region 属性

        /// <summary>
        /// 获取页面片的类型 
        /// </summary>
        public PageSnipType SnipType { get; private set; }

        /// <summary>
        /// 获取生成的页面片节点
        /// </summary>
        public XmlElement SnipElement { get; private set; }

        /// <summary>
        /// 页面片名
        /// </summary>
        public string SnipName { get; set; }

        /// <summary>
        /// 获取或设置是否为正文型页面片
        /// </summary>
        public bool IsContent { get; set; }

        /// <summary>
        /// 获取要打开的页面片的ID    
        /// </summary>
        public string SnipID { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造页面片
        /// </summary>
        /// <param name="tmpltID">当前模板ID</param>
        /// <param name="rect">当前矩形（区域）</param>
        /// <param name="parentHasContentSnip">当前模板是否已有正文型页面片</param>
        public NewSnipSetupForm(XmlDocument doc,SnipRect rect, bool parentHasContentSnip)
        {
            InitializeComponent();
            _doc = doc;
            TmpltType _type = (TmpltType)Enum.Parse(typeof(TmpltType), doc.DocumentElement.GetAttribute("type"));
            if (_type == TmpltType.Home)
            {
                groupBoxDoWhat.Visible = false;
                this.Height = 94;
            }
            _snipRect = rect;
            _parentHasContentSnip = parentHasContentSnip;
            _initSnipWidth = rect.Width.ToString() + "px";
            Init(rect);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rect"></param>
        private void Init(SnipRect rect)
        {
            if (rect == null)
                return;
            SnipID = rect.SnipID;
            SnipName = rect.SnipName;
        }

        #endregion

        #region 控件事件

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            SnipName = textBoxSnipName.Text;
            SaveSnipElement(_snipRect);
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 选择生成正文型页面片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonContent_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContent.Checked)
            {
                if (_parentHasContentSnip)
                {
                    MessageService.Show("${res:tmpltDesign.tmpltDrawPanel.message.hasContentSnip}");
                    radioButtonGenerel.Checked = true;
                }
                else
                {
                    textBoxSnipName.Text = StringParserService.Parse("${res:tmpltDesign.tmpltDrawPanel.newSnipForm.contentText}");
                    IsContent = true;
                }
            }
        }

        /// <summary>
        /// 选择生成普通型页面片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonGenerel_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGenerel.Checked)
            {
                radioButtonContent.Checked = false;
                IsContent = false;
            }
        }

        #endregion

        #region 内部函数

        /// <summary>
        /// 保存页面片的节点
        /// </summary>
        /// <param name="rect">页面片所在的矩形</param>
        private void SaveSnipElement(SnipRect rect)
        {
            if (radioButtonContent.Checked)
            {
                SnipType = PageSnipType.Content;
                IsContent = true;
            }
            else
            {
                SnipType = PageSnipType.General;
                IsContent = false;
            }
        }

        #endregion
    }
}