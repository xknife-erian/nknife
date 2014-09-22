using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 所有面板的基类
    /// </summary>
    abstract public class AbstractPad : DockContent
    {
        protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
        protected AbstractPad()
        {
            this.ImeMode = ImeMode.On;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);

            //顶部的上下文菜单
            ToolStripMenuItem hideMenuItem = new ToolStripMenuItem("隐藏(&H)");
            _contextMenuStrip.Items.Add(hideMenuItem);
            this.TabPageContextMenuStrip = _contextMenuStrip;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                ResourcesReader.SetControlPropertyHelper(this);
            }
        }

        #region ResourceGet

        public string GetText(string key)
        {
            return ResourcesReader.GetText(key, this);
        }

        public Icon GetIcon(string key)
        {
            return ResourcesReader.GetIcon(key, this);
        }

        public Image GetImage(string key)
        {
            return ResourcesReader.GetImage(key, this);
        }

        public Cursor GetCursor(string key)
        {
            return ResourcesReader.GetCursor(key, this);
        }

        public byte[] GetBytes(string key)
        {
            return ResourcesReader.GetBytes(key, this);
        }

        #endregion

    }
}
