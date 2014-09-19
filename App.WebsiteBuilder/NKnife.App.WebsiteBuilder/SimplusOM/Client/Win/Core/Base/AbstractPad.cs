using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusOM.Client.Win
{
    /// <summary>
    /// �������Ļ���
    /// </summary>
    abstract public class AbstractPad : DockContent
    {
        protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
        protected AbstractPad()
        {
            //�����������Ĳ˵�
            ToolStripMenuItem hideMenuItem = new ToolStripMenuItem("����(&H)");
            _contextMenuStrip.Items.Add(hideMenuItem);
            this.TabPageContextMenuStrip = _contextMenuStrip;

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
