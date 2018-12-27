using System.Drawing;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Kits.StarterKit.Forms;
using NKnife.Wrapper;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.StarterKit
{
    public sealed partial class LibraryDemoWorkbench : Form
    {
        private static readonly ILog _Logger = LogManager.GetLogger<LibraryDemoWorkbench>();
        private DockPanel _dockPanel;

        public LibraryDemoWorkbench()
        {
            InitializeComponent();
            DockPanelManager();
            MenuItemClickEventManager();
            Text = $"{Text} - {DI.Get<IAbout>().AssemblyVersion}";
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
            var loggerView = DI.Get<LoggingDockView>();
            loggerView.Show(_dockPanel, DockState.DockBottom);
        }

        private void MenuItemClickEventManager()
        {
            _ChineseCharUseFrequencyMenuItem.Click += (s, e) =>
            {
                var form = DI.Get<ChineseCharUseFrequencyDockView>();
                form.Show(_dockPanel, DockState.Document);
            };
            _ThreadTimerToolStripMenuItem.Click += (sender, args) =>
            {
                var form = new ThreadTimerTestForm();
                form.Show(_dockPanel, DockState.Document);
            };
        }

        public class CurrentAbout : About
        {
        }

        private void _ThreadTimerToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }
    }
}