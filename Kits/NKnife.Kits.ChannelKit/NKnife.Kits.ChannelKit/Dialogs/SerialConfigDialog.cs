using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit.Dialogs
{
    public partial class SerialConfigDialog : Form
    {
        public SerialConfigDialog()
        {
            InitializeComponent();

            _BaudRatesComboBox.Items.AddRange(SerialUtils.BaudRates);
            _ParitysComboBox.Items.AddRange(SerialUtils.Parities);
            _StopBitsesComboBox.Items.AddRange(SerialUtils.StopBits);
            _DatabitComboBox.Items.AddRange(SerialUtils.DataBits);

            ControlEventManage();
        }

        private void ControlEventManage()
        {
            _IsHexViewCheckBox.CheckedChanged += (s, e) => { _IsFormatTextViewCheckBox.Enabled = !_IsHexViewCheckBox.Checked; };
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public SelfModel SelfModels
        {
            get
            {
                var model = new SelfModel();
                var list0 = new List<object>();
                list0.AddRange(SerialUtils.BaudRates);
                model.Commons.BaudRate = int.Parse(list0[_BaudRatesComboBox.SelectedIndex].ToString());

                var list1 = new List<object>();
                list1.AddRange(SerialUtils.StopBits);
                model.Commons.StopBit = (StopBits) list1[_StopBitsesComboBox.SelectedIndex];

                var list2 = new List<object>();
                list2.AddRange(SerialUtils.Parities);
                model.Commons.Parity = (Parity) list2[_ParitysComboBox.SelectedIndex];

                var list3 = new ushort[] {5, 6, 7, 8};
                model.Commons.DataBit = list3[_DatabitComboBox.SelectedIndex];

                model.Commons.DtrEnable = _IsDTRCheckBox.Checked;
                model.Commons.RtsEnable = _IsRTSCheckBox.Checked;
                model.Commons.ReadBufferSize = (int) _BufferSpaceBox.Value;

                model.AppSettings.HexShowEnable = _IsHexViewCheckBox.Checked;
                model.AppSettings.DisplayFormatTextEnable = _IsFormatTextViewCheckBox.Checked;

                return model;
            }
            set
            {
                _IsHexViewCheckBox.Checked = value.AppSettings.HexShowEnable;
                _IsFormatTextViewCheckBox.Checked = value.AppSettings.DisplayFormatTextEnable;

                _BaudRatesComboBox.SelectedIndex = (ushort) SerialUtils.BaudRates.ToList().IndexOf(value.Commons.BaudRate);
                _StopBitsesComboBox.SelectedIndex = (ushort) SerialUtils.StopBits.ToList().IndexOf(value.Commons.StopBit);
                _ParitysComboBox.SelectedIndex = (ushort) SerialUtils.Parities.ToList().IndexOf(value.Commons.Parity);
                _DatabitComboBox.SelectedIndex = (ushort) SerialUtils.DataBits.ToList().IndexOf(value.Commons.DataBit);

                _BufferSpaceBox.Value = value.Commons.ReadBufferSize;
                _IsDTRCheckBox.Checked = value.Commons.DtrEnable;
                _IsRTSCheckBox.Checked = value.Commons.RtsEnable;
            }
        }

        public class SelfModel
        {
            public Common Commons { get; } = new Common();
            public Profession Professions { get; } = new Profession();
            public AppSetting AppSettings { get; } = new AppSetting();

            public class Common
            {
                public int BaudRate { get; set; } = 9600;
                public int DataBit { get; set; } = 8;

                public int ReadBufferSize { get; set; } = 64;
                public bool DtrEnable { get; set; } = false;
                public Parity Parity { get; set; } = Parity.None;
                public bool RtsEnable { get; set; } = true;
                public StopBits StopBit { get; set; } = StopBits.One;
            }

            public class Profession
            {
                /// <summary>
                /// 当 ReceivedBytesThreshold 引发 DataReceived 事件后，等待 ReadWait 的时间，待串口数据接收到阶段性时再进行读取
                /// </summary>
                public int ReadWait { get; set; } = 0;

                /// <summary>
                ///     读串口数据的固定超时。
                /// </summary>
                public int ReadTotalTimeoutConstant { get; set; } = 1 * 1000;

                /// <summary>
                ///     写串口数据的固定超时。
                /// </summary>
                public int WriteTotalTimeoutConstant { get; set; } = 1 * 1000;

                /// <summary>
                /// 事件触发前内部输入缓冲区中的字节数。默认值为 1。
                /// 只要内部缓冲区有大于 ReceivedBytesThreshold 个字节的时候，就会引发 DataReceived 事件。
                /// 一般情况下无需更改。
                /// </summary>
                public int ReceivedBytesThreshold { get; set; } = 1;
            }

            public class AppSetting
            {
                public bool HexShowEnable { get; set; }
                public bool DisplayFormatTextEnable { get; set; }
            }
        }
    }
}