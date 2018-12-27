using System.Windows.Forms;
using Ninject;
using NKnife.ChannelKnife.ViewModel;
using NKnife.ChannelKnife.Views;
using ReactiveUI;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.ChannelKnife.View
{
    public sealed partial class Workbench : Form, IViewFor<WorkbenchViewModel>
    {
        private readonly DockPanel _DockPanel;

        public Workbench()
        {
            _DockPanel = new DockPanel();

            InitializeComponent();
            InitializeDockPanel();
            this.WhenActivated(b =>
            {
                //b(this.OneWayBind(ViewModel, vm => vm.ApplicationTitle, v => v.Text));
                //b(this.OneWayBind(ViewModel, vm => vm.SoftwareVersion, v => v._VersionLabel.Text));
            });
#if !DEBUG
            WindowState = FormWindowState.Maximized;
#endif
        }

        #region Implementation of IViewFor

        /// <inheritdoc />
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as WorkbenchViewModel;
        }

        #endregion

        #region Implementation of IViewFor<WorkbenchViewModel>

        /// <inheritdoc />
        [Inject]
        public WorkbenchViewModel ViewModel { get; set; }

        #endregion

        private void InitializeDockPanel()
        {
            SuspendLayout();
            Controls.Add(_DockPanel);
            _DockPanel.Theme = new VS2015BlueTheme();
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

            var loggerView = new LoggerView();
#if DEBUG
            loggerView.Show(_DockPanel, DockState.DockBottom);
#else
            loggerView.Show(_DockPanel, DockState.DockBottomAutoHide);
#endif
            ResumeLayout(false);
            PerformLayout();
        }
    }
}