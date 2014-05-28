using System.Windows.Forms;
using NKnife.Window.Views.MdiForms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Window.Views
{
    public sealed partial class Workbench : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();

        public Workbench()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Cursor = Cursors.WaitCursor;
            InitializeDockPanel();
            InitializeFormCommand();
            Cursor = Cursors.Default;

            var welcome = WelcomeMdi.ME;
            welcome.Show(_DockPanel);
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_DockPanel);
            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();
        }

        public void AddMenuItem(params ToolStripItem[] items)
        {
            _MenuStrip.Items.AddRange(items);
        }

        private void InitializeFormCommand()
        {
        }
    }
}