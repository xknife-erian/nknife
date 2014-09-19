using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.Win
{
    public partial class CssSettingForm : BaseForm
    {
        #region 构造函数

        public CssSettingForm(string css)
        {
            _cssSection = CssSection.Parse(css);

            InitializeComponent();

            ///初始化时便生成所有面板
            foreach (object item in listBoxLeftType.Items)
            {
                CssMainControl newMainControl = CssMainControl.Create(item.ToString(), _cssSection);
                this.splitContainerUI.Panel2.Controls.Add(newMainControl);
                newMainControl.Visible = false;

                _mainControls.Add(item.ToString(), newMainControl);
            }

            ///显示第一个Css主面板
            listBoxLeftType.SelectedIndex = 0;
        }

        #endregion

        #region 公共属性
        
        public string CssText
        {
            get { return _cssSection.ToString(); }
        }

        public bool IsCodeView
        {
            get
            {
                return sbtnCode.Checked;
            }
        }

        public CssMainControl CurrentMainControl { get; private set; }

        #endregion

        #region 内部变量

        private CssSection _cssSection;

        private Dictionary<string, CssMainControl> _mainControls = new Dictionary<string, CssMainControl>();
        
        private string _oldListBoxLeftTypeValue;

        #endregion

        #region 内部方法

        /// <summary>
        /// 转到指定的Css主面板
        /// </summary>
        /// <param name="listBoxValue"></param>
        /// <returns></returns>
        private void GotoMainControl(string listBoxValue)
        {
            ///找到将要显示的新主面板
            CssMainControl willGotoMainControl = _mainControls[listBoxValue];

            ///若新的和原来的是一个，则不执行
            if (CurrentMainControl == willGotoMainControl)
            {
                return;
            }

            ///退出原主面板
            if (CurrentMainControl != null)
            {
                ///验证失败，则不进入新主面板
                bool result = CurrentMainControl.LeaveValidate();
                if (!result)
                {
                    listBoxLeftType.SelectedItem = _oldListBoxLeftTypeValue;
                    return;
                }
                CurrentMainControl.Visible = false;
            }

            ///进入新主面板
            willGotoMainControl.EnterLoad();
            willGotoMainControl.Visible = true;
            CurrentMainControl = willGotoMainControl;
            _oldListBoxLeftTypeValue = listBoxValue;
        }

        #endregion

        #region 控件事件

        private void listBoxLeftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GotoMainControl(listBoxLeftType.SelectedItem.ToString());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ///代码视图
            if (IsCodeView)
            {
                _cssSection.LoadText(txtCssText.Text);
            }
            ///设计视图
            else
            {
                ///调用当前主面板的离开
                bool result = CurrentMainControl.LeaveValidate();
                if (!result)
                {
                    return;
                }
            }

            ///关闭Form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sbtnDesign_Click(object sender, EventArgs e)
        {
            ///从RichTextBox载入到CssSection
            _cssSection.LoadText(txtCssText.Text);

            ///调用当前主面板的EnterLoad载入数据
            CurrentMainControl.EnterLoad();

            ///折叠代码面板
            splitContainerMain.Panel2Collapsed = true;

            ///更新按钮状态
            sbtnDesign.Checked = true;
            sbtnCode.Checked = false;
        }

        private void sbtnCode_Click(object sender, EventArgs e)
        {
            ///调用当前主面板的LeaveValidate保存数据
            if (!CurrentMainControl.LeaveValidate())
            {
                return;
            }

            ///将CssSection的的内容载入到RichTextBox
            txtCssText.Text = _cssSection.ToString(false);

            ///折叠设计面板
            splitContainerMain.Panel1Collapsed = true;

            ///更新按钮状态
            sbtnDesign.Checked = false;
            sbtnCode.Checked = true;
        }

        private void txtCssText_KeyPress(object sender, KeyPressEventArgs e)
        {
            ///特殊处理TAB键。用4个空格替代TAB键。
            if (e.KeyChar == '	')
            {
                txtCssText.SelectedText = "    ";

                e.Handled = true;
                return;
            }
        }

        private void txtCssText_KeyDown(object sender, KeyEventArgs e)
        {
            ///处理Ctrl+A全选
            if (e.Control && e.KeyCode == Keys.A)
            {
                txtCssText.SelectAll();
            }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
//#if DEBUG
//            TestForm1 form = new TestForm1();
//            form.ShowDialog();
//#endif
        }

        private void CssSettingForm_DoubleClick(object sender, EventArgs e)
        {
#if DEBUG
            TestForm1 form = new TestForm1();
            form.ShowDialog();
#endif
        }
    }
}
