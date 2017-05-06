using System.Drawing;
using System.Windows.Forms;
using NKnife.NLog.WinForm;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.StarterKit
{
    public partial class LoggingDockView : DockContent
    {
        public LoggingDockView()
        {
            InitializeComponent();
            SuspendLayout();
            var logPanel = LoggerListView.Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);         
            Controls.Add(logPanel);
            ResumeLayout(false);
        }
    }
}
