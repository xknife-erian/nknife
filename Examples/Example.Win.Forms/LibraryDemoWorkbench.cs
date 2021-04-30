using System.Windows.Forms;
using NKnife.Win.Forms.Kit.Forms;
using NKnife.Interface;
using NLog;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Forms.Kit
{
    public sealed partial class LibraryDemoWorkbench : Form
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        private DockPanel _dockPanel;
        private readonly CustomStripControlTestDockView _customStripControlTestDockView;
        private readonly ImagesPanelControlTestDockView _imagesPanelControlTestDockView;

        public LibraryDemoWorkbench(IAbout about, 
            CustomStripControlTestDockView customStripControlTestDockView, 
            ImagesPanelControlTestDockView imagesPanelControlTestDockView)
        {
            _customStripControlTestDockView = customStripControlTestDockView;
            _imagesPanelControlTestDockView = imagesPanelControlTestDockView;

            InitializeComponent();

            DockPanelManager();
            MenuItemClickEventManager();

            Text = $"{Text} - {about.AssemblyVersion}";
            _Logger.Info($"{Name}-{GetType().Name}");
        }

        private void DockPanelManager()
        {
            _dockPanel = new DockPanel
            {
                Dock = DockStyle.Fill,
                Theme = new VS2015DarkTheme()
            };
            Controls.Add(_dockPanel);
            _dockPanel.BringToFront();
        }

        private void MenuItemClickEventManager()
        {
            _CustomStripControlTestMenuItem.Click += (s, e) =>
            {
                _customStripControlTestDockView.Show(_dockPanel, DockState.Document);
            };
            _ImagesPanelMenuItem.Click += (s, e) =>
            {
                _imagesPanelControlTestDockView.Show(_dockPanel, DockState.Document);
            };
        }

        private void 颜色选择器ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var dialog = new Colors.ColorPickerDialog();
            dialog.ShowDialog(this);
            MessageBox.Show($"{dialog.SelectedColor}");
        }
    }

    public class CurrentAbout : About
    {
    }
}