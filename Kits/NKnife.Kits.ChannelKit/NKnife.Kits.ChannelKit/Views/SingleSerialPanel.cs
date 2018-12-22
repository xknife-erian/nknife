using System;
using System.Windows.Forms;
using NKnife.Channels.Serials;
using NKnife.IoC;
using NKnife.Kits.ChannelKit.Dialogs;
using NKnife.Kits.ChannelKit.Properties;
using NKnife.Kits.ChannelKit.ViewModels;

namespace NKnife.Kits.ChannelKit.Views
{
    public partial class SingleSerialPanel : UserControl
    {
        private readonly SingleSerialViewmodel _viewmodel = Di.Get<SingleSerialViewmodel>();

        public SingleSerialPanel()
        {
            InitializeComponent();
            RefreshControlEnable();
            _PortLabel.Text = string.Empty;
        }

        private void RefreshControlEnable()
        {
            SuspendLayout();
            _ChoosePortButton.Enabled = _viewmodel.Port == 0;
            _ConfigurePortButton.Enabled = _viewmodel.Port != 0;
            _QuestionsEditorButton.Enabled = _viewmodel.Port != 0;
            _OpenPortButton.Enabled = _viewmodel.Port != 0 && !_viewmodel.IsOpen;
            _ClosePortButton.Enabled = _viewmodel.IsOpen;

            _StartButton.Enabled = _viewmodel.IsOpen;
            _PauseButton.Enabled = _viewmodel.IsOpen;
            _StopButton.Enabled = _viewmodel.IsOpen;
            ResumeLayout(false);
            PerformLayout();
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
                _viewmodel.Port = port;
                _PortLabel.Text = $"COM{port}";
                RefreshControlEnable();
            }
        }

        /// <summary>
        /// 配置串口的一些参数动作
        /// </summary>
        private void _ConfigurePortButton_Click(object sender, EventArgs e)
        {
            var dialog = new SerialConfigDialog {SelfModels = _viewmodel.FromConfig()};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _viewmodel.UpdateConfig(dialog.SelfModels);
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        private void _OpenPortButton_Click(object sender, EventArgs e)
        {
            if (_viewmodel.OpenPort())
                MessageBox.Show(this, $"串口(COM{_viewmodel.Port})端口打开成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, $"串口(COM{_viewmodel.Port})端口打开失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RefreshControlEnable();
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        private void _ClosePortButton_Click(object sender, EventArgs e)
        {
            var port = _viewmodel.Port;
            if (_viewmodel.ClosePort())
                MessageBox.Show(this, $"串口(COM{port})端口关闭成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, $"串口(COM{port})端口关闭失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RefreshControlEnable();
        }

        private void _StartButton_Click(object sender, EventArgs e)
        {
            _viewmodel.Start();
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