using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.NLog.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public class LogView : DockContent
    {
        public LogView()
        {
            InitializeComponent();
            LogPanel.AppendLogPanelToContainer(this);
        }
        private void InitializeComponent()
        {
            //this._LogPanel = LogPanel.Instance;
            this.SuspendLayout();
            // 
            // LogView
            // 
            this.ClientSize = new System.Drawing.Size(673, 227);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "LogView";
            this.Text = "日志";
            this.ResumeLayout(false);

        }
    }
}
