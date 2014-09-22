using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 所有面板的基类
    /// </summary>
    abstract public class PadContent : DockContent
    {
        protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
        protected PadContent()
        {
            this.ImeMode = ImeMode.On;

            //顶部的上下文菜单
            ToolStripMenuItem hideMenuItem = new ToolStripMenuItem("隐藏(&H)");
            _contextMenuStrip.Items.Add(hideMenuItem);
            this.TabPageContextMenuStrip = _contextMenuStrip;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);

            hideMenuItem.Click += delegate
            {
                this.Hide();
            };
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            ///当点击右上角的叉进行关闭时,应该隐藏此窗口
            if (m.Msg == 16)
            {
                this.Hide();
                return;
            }
            base.WndProc(ref m);
        }
    }
}
