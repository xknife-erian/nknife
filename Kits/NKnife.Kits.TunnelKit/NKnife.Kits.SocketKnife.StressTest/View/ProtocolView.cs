using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public class ProtocolView:DockContent
    {
        public ProtocolView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProtocolView
            // 
            this.ClientSize = new System.Drawing.Size(284, 469);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ProtocolView";
            this.Text = "协议编辑面板";
            this.ResumeLayout(false);

        }

    }
}
