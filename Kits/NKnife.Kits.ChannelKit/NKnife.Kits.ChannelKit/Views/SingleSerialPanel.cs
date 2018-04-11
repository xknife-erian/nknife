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
        private readonly SingleSerialViewmodel _Viewmodel = DI.Get<SingleSerialViewmodel>();

        public SingleSerialPanel()
        {
            InitializeComponent();
            RefreshControlEnable();
            _PortLabel.Text = string.Empty;
        }

        private void RefreshControlEnable()
        {
            SuspendLayout();
            _ChoosePortButton.Enabled = _Viewmodel.Port == 0;
            _ConfigurePortButton.Enabled = _Viewmodel.Port != 0;
            _QuestionsEditorButton.Enabled = _Viewmodel.Port != 0;
            _OpenPortButton.Enabled = _Viewmodel.Port != 0 && !_Viewmodel.IsOpen;
            _ClosePortButton.Enabled = _Viewmodel.IsOpen;

            _StartButton.Enabled = _Viewmodel.IsOpen;
            _PauseButton.Enabled = _Viewmodel.IsOpen;
            _StopButton.Enabled = _Viewmodel.IsOpen;
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
                _Viewmodel.Port = port;
                _PortLabel.Text = $"COM{port}";
                RefreshControlEnable();
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
                _Viewmodel.UpdateConfig(dialog.SelfModels);
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        private void _OpenPortButton_Click(object sender, EventArgs e)
        {
            if (_Viewmodel.OpenPort())
                MessageBox.Show(this, $"串口(COM{_Viewmodel.Port})端口打开成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, $"串口(COM{_Viewmodel.Port})端口打开失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RefreshControlEnable();
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        private void _ClosePortButton_Click(object sender, EventArgs e)
        {
            if (_Viewmodel.ClosePort())
                MessageBox.Show(this, $"串口(COM{_Viewmodel.Port})端口关闭成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, $"串口(COM{_Viewmodel.Port})端口关闭失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RefreshControlEnable();
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