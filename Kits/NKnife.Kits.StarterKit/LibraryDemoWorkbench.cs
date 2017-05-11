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
        private static readonly ILog _logger = LogManager.GetLogger<LibraryDemoWorkbench>();
        private DockPanel _DockPanel;

        public LibraryDemoWorkbench()
        {
            InitializeComponent();
            DockPanelManager();
            MenuItemClickEventManager();
            Text = $"{Text} - {DI.Get<IAbout>().AssemblyVersion}";
            _logger.Info($"{Name}-{GetType().Name}");
        }

        private void DockPanelManager()
        {
            _DockPanel = new DockPanel
            {
                Dock = DockStyle.Fill,
                Theme = new VS2015DarkTheme()
            };
            Controls.Add(_DockPanel);
            _DockPanel.BringToFront();
            var loggerView = DI.Get<LoggingDockView>();
            loggerView.Show(_DockPanel, DockState.DockBottom);
        }

        private void MenuItemClickEventManager()
        {
            _ChineseCharUseFrequencyMenuItem.Click += (s, e) =>
            {
                var form = DI.Get<ChineseCharUseFrequencyDockView>();
                form.Show(_DockPanel, DockState.Document);
            };
        }

        public class CurrentAbout : About
        {
        }
    }
}