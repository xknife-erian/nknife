using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SegmentQuickDemo
{
    /// <summary>
    /// KvnluSoft：一个已设置为多行显示的TextBox，有简单的copy,paste等右键菜单
    /// </summary>
    public class QuickTextBox : TextBox
    {
        ContextMenuStrip _contextMenuStrip;
        ToolStripMenuItem _toolStripMenuItem;

        public QuickTextBox()
        {
            this.SuspendLayout();

            this.Multiline = true;
            this.MaxLength = 640000;
            this.ScrollBars = ScrollBars.Vertical;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);

            _contextMenuStrip = new ContextMenuStrip();
            _toolStripMenuItem = new ToolStripMenuItem("全选(&A)");
            _toolStripMenuItem.Click += new EventHandler(SelectAllEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            _toolStripMenuItem = new ToolStripMenuItem("拷贝(&C)");
            _toolStripMenuItem.Click += new EventHandler(CopyEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            _toolStripMenuItem = new ToolStripMenuItem("粘贴(&P)");
            _toolStripMenuItem.Click += new EventHandler(PasteEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            _toolStripMenuItem = new ToolStripMenuItem("剪切(&T)");
            _toolStripMenuItem.Click += new EventHandler(CutEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            ToolStripSeparator separator = new ToolStripSeparator();
            _contextMenuStrip.Items.Add(separator);
            _toolStripMenuItem = new ToolStripMenuItem("还原(&F)");
            _toolStripMenuItem.Click += new EventHandler(ClearEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            this.ContextMenuStrip = _contextMenuStrip;
            this.ResumeLayout(false);

        }
        private void PasteEx(object sender, EventArgs e)
        {
            this.Paste();
        }
        private void CutEx(object sender, EventArgs e)
        {
            this.Cut();
        }
        private void ClearEx(object sender, EventArgs e)
        {
            this.Clear();
        }
        private void SelectAllEx(object sender, EventArgs e)
        {
            this.SelectAll();
        }
        private void CopyEx(object sender, EventArgs e)
        {
            this.Copy();
        }
    }
}
