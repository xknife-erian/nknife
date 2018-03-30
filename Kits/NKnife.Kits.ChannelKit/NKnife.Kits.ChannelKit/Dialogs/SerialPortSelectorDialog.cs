﻿using System.Windows.Forms;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit.Dialogs
{
    public partial class SerialPortSelectorDialog : Form
    {
        public SerialPortSelectorDialog()
        {
            InitializeComponent();

            _ListView.Items.Clear();
            var i = 1;
            foreach (var p in SerialUtils.LocalSerialPorts)
            {
                var listitem = new ListViewItem(new[] {i.ToString(), p.Key, p.Value});
                _ListView.Items.Add(listitem);
                i++;
            }

            _AcceptButton.Click += (sender, args) =>
            {
                if (_ListView.SelectedItems.Count > 0)
                {
                    var item = _ListView.SelectedItems[0];
                    var port = item.SubItems[1].Text.ToUpper().Trim().TrimStart('C', 'O', 'M');
                    ushort p = 0;
                    ushort.TryParse(port, out p);
                    SerialPort = p;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, "请从本机串口列表中选择一个串口。", "未进行串口选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };
            _CancelButton.Click += (sender, args) =>
            {
                SerialPort = 0;
                DialogResult = DialogResult.Cancel;
            };
        }

        public ushort SerialPort { get; private set; } = 1;
    }
}
