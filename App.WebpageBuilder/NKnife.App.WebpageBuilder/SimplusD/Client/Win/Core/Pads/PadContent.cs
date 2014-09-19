using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// �������Ļ���
    /// </summary>
    abstract public class PadContent : DockContent
    {
        protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
        protected PadContent()
        {
            this.ImeMode = ImeMode.On;

            //�����������Ĳ˵�
            ToolStripMenuItem hideMenuItem = new ToolStripMenuItem("����(&H)");
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
            ///��������ϽǵĲ���йر�ʱ,Ӧ�����ش˴���
            if (m.Msg == 16)
            {
                this.Hide();
                return;
            }
            base.WndProc(ref m);
        }
    }
}
