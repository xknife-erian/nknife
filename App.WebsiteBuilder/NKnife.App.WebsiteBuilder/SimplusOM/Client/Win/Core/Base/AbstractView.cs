using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusOM.Client.Win
{
    /// <summary>
    /// ������ͼ���ڵĻ���
    /// </summary>
    abstract public class AbstractView : DockContent
    {
        protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
        protected AbstractView()
        {
            //�����������Ĳ˵�
            ToolStripMenuItem closeMenuItem = new ToolStripMenuItem("�ر�");
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
