using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class CustomTextForm : BaseForm
    {
        /// <summary>
        /// add by zhenghao at 2008-05-30 : 可以返回一个编辑好的字符串的小窗体
        /// </summary>
        public CustomTextForm( Image image)
        {
            InitializeComponent();
            if (image == null)
            {
                panelImg.Visible = false;
                Height = 108;
            }
            else
            { 

            }
        }

        #region 内部变量

        private string _orderText = "输入名称:";

        #endregion

        #region 公共属性

        /// <summary>
        /// add by zhenghao at 2008-05-30 : 获取或设置想要显示的目的文本：就是文本框上边的标签内容
        /// </summary>
        public string OrderText
        {
            get { return _orderText; }
            set
            {
                _orderText = value;
                labelName.Text = value;
            }
        }

        /// <summary>
        /// add by zhenghao at 2008-05-30 : 获取或设置想要得到的字符串
        /// </summary>
        public string Value { get; private set; }

        #endregion

        #region 公共方法

        #endregion

        #region 内部方法

        #endregion

        #region 事件响应

        /// <summary>
        /// add by zhenghao at 2008-05-30 : 点击“确定”按钮时......
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Value = textBoxCustomText.Text;
        }

        /// <summary>
        /// add by zhenghao at 2008-05-30 : 当文本框失去焦点时......
        /// </summary>
        private void textBoxCustomText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCustomText.Text))
            {
                MessageService.Show("这里不能为空！");
                textBoxCustomText.Text = "newText";
                Value = "newText";
            }
            else
            {
                Value = textBoxCustomText.Text;
            }
        }

        #endregion

        #region 自定义事件

        #endregion

    }
}
