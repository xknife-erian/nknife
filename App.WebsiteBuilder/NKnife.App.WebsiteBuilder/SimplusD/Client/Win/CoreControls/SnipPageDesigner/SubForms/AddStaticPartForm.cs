using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{
    public partial class AddStaticPartForm : BaseForm
    {
        #region 控件

        private HTMLDesignControl _htmlDesigner;

        #endregion

        #region 内部变量

        #endregion

        #region 属性

        /// <summary>
        /// 要显示的文本
        /// </summary>
        public string PageText { get;private set; }

        #endregion

        #region 构造函数

        public AddStaticPartForm( string pageText)
        {
            InitializeComponent();
            PageText = pageText;
            _htmlDesigner = new HTMLDesignControl(pageText);
            this.designerBox.Controls.Add(_htmlDesigner.GetMainToolStrip());
            this.designerBox.Controls.Add(_htmlDesigner.GetHtmlPanel());
            this._htmlDesigner.SetHtmlPanel().BringToFront();
            btnOK.Click += new EventHandler(btnOK_Click);
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            PageText = _htmlDesigner.PageText;
        }

        #endregion
    }
}