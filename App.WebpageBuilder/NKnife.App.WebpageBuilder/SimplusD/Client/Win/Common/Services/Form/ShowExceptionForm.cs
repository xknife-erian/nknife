using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class ShowExceptionForm : System.Windows.Forms.Form
    {
        Exception _ex;
        public ShowExceptionForm(Exception ex)
        {
            _ex = ex;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            txtException.AutoWordSelection = false;

            txtException.Text = "出现未知异常:" + _ex.Message +
                "\r\n\r\n堆栈信息:\r\n" + _ex.StackTrace + "\r\n";

            ///在非调试状态时，“忽略”按钮不可见。
#if !DEBUG
            btnIgnore.Visible = false;
#endif

            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {

            ///在非调试状态时，窗口关闭则关闭软件。
#if !DEBUG
            Application.Exit();
#endif

            base.OnFormClosed(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

            Application.Exit();
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            //if (Debugger.IsAttached)
            //{
            //    Debugger.Break();
            //}

            this.Close();
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            txtException.Copy();
        }

        private void menuItemSelectAll_Click(object sender, EventArgs e)
        {
            txtException.SelectAll();
        }

        private void menuItemWordWrap_Click(object sender, EventArgs e)
        {
            txtException.WordWrap = !txtException.WordWrap;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            menuItemWordWrap.Checked = txtException.WordWrap;
        }
    }
}
