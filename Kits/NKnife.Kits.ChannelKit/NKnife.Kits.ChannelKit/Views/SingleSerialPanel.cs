using System;
using System.Windows.Forms;
using NKnife.Channels.Serials;
using NKnife.IoC;
using NKnife.Kits.ChannelKit.Dialogs;
using NKnife.Kits.ChannelKit.ViewModels;

namespace NKnife.Kits.ChannelKit.Views
{
    public partial class SingleSerialPanel : UserControl
    {
        private readonly SingleSerialViewmodel _Viewmodel = DI.Get<SingleSerialViewmodel>();

        public SingleSerialPanel()
        {
            InitializeComponent();
            InitializeControlEnable();
            _PortLabel.Text = string.Empty;
        }

        private void InitializeControlEnable()
        {
            if (_Viewmodel.Port == 0)
            {
                _OpenOrClosePortButton.Enabled = false;
                _ConfigurePortButton.Enabled = false;
            }
            else
            {
                _OpenOrClosePortButton.Enabled = true;
                _ConfigurePortButton.Enabled = true;
            }
            _StartButton.Enabled = _Viewmodel.IsOpen;
            _PauseButton.Enabled = _Viewmodel.IsOpen;
            _StopButton.Enabled = _Viewmodel.IsOpen;
        }

        /// <summary>
        /// 选择端口的动作函数
        /// </summary>
        private void _ChoosePortButton_Click(object sender, EventArgs e)
        {
            var dialog = new SerialPortSelectorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var port = dialog.SerialPort;
                _Viewmodel.Port = port;
                _PortLabel.Text = $"COM{port}";
                InitializeControlEnable();
            }
        }

        /// <summary>
        /// 配置串口的一些参数动作
        /// </summary>
        private void _ConfigurePortButton_Click(object sender, EventArgs e)
        {
            var dialog = new SerialConfigDialog {SelfModels = _Viewmodel.FromConfig()};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _Viewmodel.UpdataConfig(dialog.SelfModels);
            }
        }

        /// <summary>
        /// 操作串口(打开或关闭)的动作
        /// </summary>
        private void _OperatingPortButton_Click(object sender, EventArgs e)
        {
            if (_Viewmodel.OpenOrClosePort())
            {
                MessageBox.Show(this, $"{_OpenOrClosePortButton.Text}串口端口操作成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _OpenOrClosePortButton.Text = _Viewmodel.IsOpen ? "关闭" : "打开";
            }
            else
            {
                MessageBox.Show(this, $"{_OpenOrClosePortButton.Text}串口端口操作失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            InitializeControlEnable();
        }

        private void _StartButton_Click(object sender, EventArgs e)
        {

        }

        private void _PauseButton_Click(object sender, EventArgs e)
        {

        }

        private void _StopButton_Click(object sender, EventArgs e)
        {

        }

        private void _QuestionsEditorButton_Click(object sender, EventArgs e)
        {
            var dialog = new QuestionsEditorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
            }
        }

    }
}
