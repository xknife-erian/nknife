using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// ������ͼ���ڵĻ���
    /// </summary>
    public class AbstractView : DockContent
    {
        protected ContextMenuStrip _ContextMenuStrip = new ContextMenuStrip();
        protected AbstractView()
        {
            this.ImeMode = ImeMode.On;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);

            //�����������Ĳ˵�
            ToolStripMenuItem closeMenuItem = new ToolStripMenuItem("�ر�");
            _ContextMenuStrip.Items.Add(closeMenuItem);
            this.TabPageContextMenuStrip = _ContextMenuStrip;
            closeMenuItem.Click += delegate
            {
                this.Close();
            };

            this.DockAreas = DockAreas.Document;
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
