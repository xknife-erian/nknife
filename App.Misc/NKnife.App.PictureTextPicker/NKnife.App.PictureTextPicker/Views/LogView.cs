using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Ioc;
using NKnife.NLog3.Logging.LogPanel;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class LogView : DockContent
    {
        private LogPanel _LogPanel;

        public LogView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this._LogPanel = DI.Get<LogPanel>();
            this.SuspendLayout();
            // 
            // _LogPanel
            // 
            this._LogPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._LogPanel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this._LogPanel.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
            this._LogPanel.Location = new System.Drawing.Point(0, 0);
            this._LogPanel.Name = "_LogPanel";
            this._LogPanel.Size = new System.Drawing.Size(673, 227);
            this._LogPanel.TabIndex = 0;
            this._LogPanel.ToolStripVisible = true;
            // 
            // LogView
            // 
            this.ClientSize = new System.Drawing.Size(673, 227);
            this.Controls.Add(this._LogPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "LogView";
            this.ResumeLayout(false);

        }
    }
}
