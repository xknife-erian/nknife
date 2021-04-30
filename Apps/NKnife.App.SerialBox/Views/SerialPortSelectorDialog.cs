using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using NKnife.MeterKnife.Util.Serial;

namespace NKnife.SerialBox.Views
{
    public partial class SerialPortSelectorDialog : Form
    {
        public SerialPortSelectorDialog()
        {
            InitializeComponent();
            InitializeListView();
            InitializeSerialParamsGroup();
            _AcceptButton.Click += delegate
            {
                if (_SerialPortListView.SelectedItems.Count > 0)
                {
                    SetSerialPortProperty();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, "请从本机串口列表中选择一个串口。", "未进行串口选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };
            _CancelButton.Click += delegate
            {
                SerialPort = null;
                DialogResult = DialogResult.Cancel;
            };
            _RefreshButton.Click += delegate
            {
                SerialHelper.RefreshSerialPorts();
                SerialPort = new SerialPort();
                InitializeListView();
                InitializeSerialParamsGroup();
            };
        }

        public SerialPort SerialPort { get; private set; } = new SerialPort();

        private void InitializeSerialParamsGroup()
        {
            _BaudRateComboBox.Items.Clear();
            _StopBitsComboBox.Items.Clear();
            _DataBitsComboBox.Items.Clear();
            _ParityComboBox.Items.Clear();

            _BaudRateComboBox.Items.AddRange(SerialHelper.BaudRates);
            _StopBitsComboBox.Items.AddRange(SerialHelper.StopBits);
            _DataBitsComboBox.Items.AddRange(SerialHelper.DataBits);
            _ParityComboBox.Items.AddRange(SerialHelper.Parities);

            _BaudRateComboBox.SelectedItem = 115200;
            _StopBitsComboBox.SelectedItem = StopBits.One;
            _DataBitsComboBox.SelectedItem = 8;
            _ParityComboBox.SelectedItem = Parity.None;
        }

        private void InitializeListView()
        {
            _SerialPortListView.SuspendLayout();
            _SerialPortListView.Items.Clear();
            foreach (var p in SerialHelper.LocalSerialPorts)
            {
                var item = new ListViewItem(new[] {"", p.Key, p.Value});
                _SerialPortListView.Items.Add(item);
            }

            _SerialPortListView.PerformLayout();
            _SerialPortListView.ResumeLayout(false);
        }

        private void SetSerialPortProperty()
        {
            SerialPort.Encoding = Encoding.GetEncoding("GB18030");
            var item = _SerialPortListView.SelectedItems[0];
            var port = item.SubItems[1].Text.ToUpper().Trim();
            SerialPort.PortName = port;
            SerialPort.BaudRate = (int) _BaudRateComboBox.SelectedItem;
            SerialPort.StopBits = (StopBits) _StopBitsComboBox.SelectedItem;
            SerialPort.DataBits = (int) _DataBitsComboBox.SelectedItem;
            SerialPort.Parity = (Parity) _ParityComboBox.SelectedItem;
            SerialPort.RtsEnable = _RTSCheckBox.Checked;
            SerialPort.DtrEnable = _DTRCheckBox.Checked;
            SerialPort.ReadTimeout = -1;
        }
    }
}