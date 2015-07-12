using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public partial class ServiceSettingForm : Form
    {
        public ServiceSettingForm()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            int port = 0;
            if (string.IsNullOrEmpty(ServerPortTextBox.Text))
            {
                MessageBox.Show("端口不能为空");
                return;
            }
            if (!int.TryParse(ServerPortTextBox.Text, out port))
            {
                MessageBox.Show("端口必须为整数");
                return;
            }
            if (port <= 0)
            {
                MessageBox.Show("端口必须为正整数");
                return;
            }
            Properties.Settings.Default.ServerPort = port;
            Properties.Settings.Default.Save();
            MessageBox.Show("保存成功，重启服务生效");
            Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ServiceSettingFormLoad(object sender, EventArgs e)
        {
            ServerPortTextBox.Text = Properties.Settings.Default.ServerPort.ToString();
        }
    }
}
