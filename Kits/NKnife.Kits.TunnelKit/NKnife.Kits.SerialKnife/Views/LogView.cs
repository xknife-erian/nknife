using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.NLog.WinForm;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife.Views
{
    public class LogView : DockContent
    {
        private LoggerListView _logPanel;

        public LogView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this._logPanel = LoggerListView.Instance;
            this.SuspendLayout();
            // 
            // _LogPanel
            // 
            this._logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logPanel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this._logPanel.Location = new System.Drawing.Point(0, 0);
            this._logPanel.Name = "_logPanel";
            this._logPanel.Size = new System.Drawing.Size(673, 227);
            this._logPanel.TabIndex = 0;
            this._logPanel.ToolStripVisible = true;
            // 
            // LogView
            // 
            this.ClientSize = new System.Drawing.Size(673, 227);
            this.Controls.Add(this._logPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "LogView";
            this.Text = "日志";
            this.ResumeLayout(false);

        }
    }
}
