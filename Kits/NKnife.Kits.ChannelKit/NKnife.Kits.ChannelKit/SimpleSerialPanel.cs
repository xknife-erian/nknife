using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using NKnife.Channels.Serials;
using NKnife.Kits.ChannelKit.Dialogs;

namespace NKnife.Kits.ChannelKit
{
    public partial class SimpleSerialPanel : UserControl
    {
        private readonly SimpleSerialPanelViewmodel _Viewmodel = new SimpleSerialPanelViewmodel();

        public SimpleSerialPanel()
        {
            InitializeComponent();
            _PortLabel.Text = string.Empty;
        }

        private void _PortButton_Click(object sender, EventArgs e)
        {
            var dialog = new SerialPortSelectorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var port = dialog.SerialPort;
                _Viewmodel.Config = new SerialConfig(port);
                _PortLabel.Text = $"COM{dialog.SerialPort}";
            }
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

        private void _ConfigButton_Click(object sender, EventArgs e)
        {
            var dialog = new SerialConfigDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void _QuestionsEditorButton_Click(object sender, EventArgs e)
        {
            var dialog = new QuestionsEditorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }

    public class SimpleSerialPanelViewmodel : ViewModelBase
    {
        public SerialConfig Config { get; set; }
    }
}
