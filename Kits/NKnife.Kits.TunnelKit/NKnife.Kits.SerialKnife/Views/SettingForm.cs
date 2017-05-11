using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.Draws.WinForm;

namespace NKnife.Kits.SerialKnife.Views
{
    public partial class SettingForm : SimpleForm
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SaveSettingButton_Click(object sender, EventArgs e)
        {
            if (PortNumberComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择端口号");
                return;
            }

            int portNumber = int.Parse(PortNumberComboBox.SelectedItem.ToString().Substring(3, 1));
            Properties.Settings.Default.PortNumber = portNumber;
            Properties.Settings.Default.EnableMock = EnableMockDataConnectorRadioButton.Checked;
            Properties.Settings.Default.Save();

            MessageBox.Show("设置成功，重启程序生效");
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            PortNumberComboBox.SelectedItem = string.Format("COM{0}", Properties.Settings.Default.PortNumber);
            EnableMockDataConnectorRadioButton.Checked = Properties.Settings.Default.EnableMock;
            EnableRealDataConnectorRadioButton.Checked = !Properties.Settings.Default.EnableMock;
        }
    }
}
