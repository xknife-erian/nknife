using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using NKnife.ChannelKnife.ViewModel;
using NKnife.ChannelKnife.ViewModel.Common;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.ChannelKnife.Views
{
    public partial class SerialPortView : DockContent
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialPortView>();

        /// <summary>
        ///     通道状态
        /// </summary>
        private ChannelMode _ChannelMode = ChannelMode.Stop;

        public SerialPortView()
        {
            InitializeComponent();
            InitializeViewData();

            ControlStatusManage();
            ControlEventManage();
        }

        public SerialPortViewModel ViewModel { get; } = DI.Get<SerialPortViewModel>();

        private void InitializeViewData()
        {
            //_IsDisplayQuestionCheckbox.CheckBox.Checked = ViewModel.IsDisplayQuestion;
            _SerialConfigPanel.ViewData = ViewModel.Config;
            _QuestionsEditorPanel.ViewData = ViewModel.Questions;
            _ListView.SerialDataListViewViewData = ViewModel.DataView;
            ViewModel.PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(ViewModel.RxCount):
                        this.ThreadSafeInvoke(() => _RxCountLabel.Text = ViewModel.RxCount.ToString());
                        break;
                    case nameof(ViewModel.TxCount):
                        this.ThreadSafeInvoke(() => _TxCountLabel.Text = ViewModel.TxCount.ToString());
                        break;
                }
            };
        }

        private void ControlStatusManage()
        {
            this.ThreadSafeInvoke(() =>
            {
                switch (_ChannelMode)
                {
                    case ChannelMode.Start:
                        _StartButton.Enabled = false;
                        _PauseButton.Enabled = true;
                        _StopButton.Enabled = true;
                        break;
                    case ChannelMode.Pause:
                        _StartButton.Enabled = true;
                        _PauseButton.Enabled = false;
                        _StopButton.Enabled = true;
                        break;
                    case ChannelMode.Stop:
                        _StartButton.Enabled = true;
                        _PauseButton.Enabled = false;
                        _StopButton.Enabled = false;
                        break;
                }
                _SwitchSyncSplitButton.Enabled = (_ChannelMode != ChannelMode.Start && _ChannelMode != ChannelMode.Pause);
            });
        }

        private void ControlEventManage()
        {
            Closing += (sender, args) =>
            {
                new Thread(() => { ViewModel.SerialClose(); }).Start();
                _logger.Info("停止端口监听..");
            };
            _StartButton.Click += (sender, args) =>
            {
                //_BaudRateLabel.Text = $"BaudRate: {SerialUtils.BaudRates[ViewModel.Config.SelectBaudRate]}";
                switch (_ChannelMode)
                {
                    case ChannelMode.Stop:
                        ViewModel.SerialOpen(false);
                        break;
                    case ChannelMode.Pause: //当暂停时被按下
                        ViewModel.SerialOpen(true);
                        break;
                }
                _logger.Info("启动端口监听..");
                _ChannelMode = ChannelMode.Start;
                ControlStatusManage();
            };
            _PauseButton.Click += (sender, args) =>
            {
                ViewModel.SerialPause();
                _logger.Info("暂停端口监听..");
                _ChannelMode = ChannelMode.Pause;
                ControlStatusManage();
            };
            _StopButton.Click += (sender, args) =>
            {
                new Thread(() => { ViewModel.SerialClose(); }).Start();
                _BaudRateLabel.Text = string.Empty;
                _logger.Info("停止端口监听..");
                _ChannelMode = ChannelMode.Stop;
                ControlStatusManage();
            };
            _AsyncMenuItem.Click += (sender, args) =>
            {
                ViewModel.IsSync = false;
                _SwitchSyncSplitButton.Image = _AsyncMenuItem.Image;
                _SwitchSyncSplitButton.Text = _AsyncMenuItem.Text;
            };
            _SyncMenuItem.Click += (sender, args) =>
            {
                ViewModel.IsSync = true;
                _SwitchSyncSplitButton.Image = _SyncMenuItem.Image;
                _SwitchSyncSplitButton.Text = _SyncMenuItem.Text;
            };
            //_IsDisplayQuestionCheckbox.CheckBox.CheckedChanged += (s, e) => { ViewModel.IsDisplayQuestion = _IsDisplayQuestionCheckbox.CheckBox.Checked; };
            _DataClearButton.Click += (sender, args) => { _ListView.ThreadSafeInvoke(() => _ListView.Clear()); };

            _QuestionsEditorPanel.Asked += (sender, args) => { ViewModel.Ask(); };
            _QuestionsEditorPanel.EndAsked += (sender, args) => { ViewModel.EndAsk(); };
        }
    }
}