using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.DockForm.Views
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
            Cursor = Cursors.Default;
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

        public void AddStatusItem(params ToolStripItem[] items)
        {
            _StatusStrip.Items.AddRange(items);
        }

        public void AddLeftView(DockContent view)
        {
            view.Show(_DockPanel, DockState.DockLeft);
        }

        public void AddRightView(DockContent view)
        {
            view.Show(_DockPanel, DockState.DockRight);
        }

        public void AddTopView(DockContent view)
        {
            view.Show(_DockPanel, DockState.DockTop);
        }

        public void AddBottomView(DockContent view)
        {
            view.Show(_DockPanel, DockState.DockBottom);
        }
    }
}