using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusOM.Client.Win
{
    /// <summary>
    /// 所有视图窗口的基类
    /// </summary>
    abstract public class AbstractView : DockContent
    {
        protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
        protected AbstractView()
        {
            //顶部的上下文菜单
            ToolStripMenuItem closeMenuItem = new ToolStripMenuItem("关闭");
            _contextMenuStrip.Items.Add(closeMenuItem);
            this.TabPageContextMenuStrip = _contextMenuStrip;
            closeMenuItem.Click += delegate
            {
                this.Close();
            };

            this.DockAreas = DockAreas.Document;
        }
    }
}
