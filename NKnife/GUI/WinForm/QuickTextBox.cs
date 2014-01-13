using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gean.Gui.WinForm
{
    /// <summary>
    /// 一个已设置为多行显示的TextBox，有简单的copy,paste等右键菜单
    /// </summary>
    public sealed class QuickTextBox : TextBox
    {
        private readonly ContextMenuStrip _ContextMenuStrip;
        private readonly ToolStripMenuItem _ToolStripMenuItem;

        public QuickTextBox()
        {
            SuspendLayout();

            Multiline = true;
            MaxLength = 640000;
            ScrollBars = ScrollBars.Vertical;
            Font = new Font("Tahoma", 8.25F);

            _ContextMenuStrip = new ContextMenuStrip();
            _ToolStripMenuItem = new ToolStripMenuItem("全选(&A)");
            _ToolStripMenuItem.Click += SelectAllEx;
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            _ToolStripMenuItem = new ToolStripMenuItem("拷贝(&C)");
            _ToolStripMenuItem.Click += CopyEx;
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            _ToolStripMenuItem = new ToolStripMenuItem("粘贴(&P)");
            _ToolStripMenuItem.Click += PasteEx;
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            _ToolStripMenuItem = new ToolStripMenuItem("剪切(&T)");
            _ToolStripMenuItem.Click += CutEx;
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            var separator = new ToolStripSeparator();
            _ContextMenuStrip.Items.Add(separator);
            _ToolStripMenuItem = new ToolStripMenuItem("还原(&F)");
            _ToolStripMenuItem.Click += ClearEx;
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            ContextMenuStrip = _ContextMenuStrip;
            ResumeLayout(false);
        }

        public override sealed bool Multiline
        {
            get { return base.Multiline; }
            set { base.Multiline = value; }
        }

        private void PasteEx(object sender, EventArgs e)
        {
            Paste();
        }

        private void CutEx(object sender, EventArgs e)
        {
            Cut();
        }

        private void ClearEx(object sender, EventArgs e)
        {
            Clear();
        }

        private void SelectAllEx(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void CopyEx(object sender, EventArgs e)
        {
            Copy();
        }
    }
}