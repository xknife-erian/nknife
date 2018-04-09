using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit.Dialogs
{
    public partial class SerialConfigDialog : Form
    {
        private readonly SerialConfigDialogViewModel _ViewData = new SerialConfigDialogViewModel();

        public SerialConfigDialog()
        {
            InitializeComponent();

            _BaudRatesComboBox.Items.AddRange(SerialUtils.BaudRates);
            _ParitysComboBox.Items.AddRange(SerialUtils.Parities);
            _StopBitsesComboBox.Items.AddRange(SerialUtils.StopBits);
            _DatabitComboBox.Items.AddRange(SerialUtils.DataBits);

            ViewModelDataBinding();
            ViewModelEventManage();
            ControlEventManage();
        }

        public void ImportConfig(SerialConfig config)
        {
            _ViewData.Import(config);
        }

        private void ControlEventManage()
        {
            _BaudRatesComboBox.SelectedValueChanged += (s, e) => { _ViewData.SelectBaudRate = (ushort)_BaudRatesComboBox.SelectedIndex; };
            _ParitysComboBox.SelectedValueChanged += (s, e) => { _ViewData.SelectParity = (ushort)_ParitysComboBox.SelectedIndex; };
            _StopBitsesComboBox.SelectedValueChanged += (s, e) => { _ViewData.SelectStopBit = (ushort)_StopBitsesComboBox.SelectedIndex; };
            _DatabitComboBox.SelectedValueChanged += (s, e) => { _ViewData.SelectDataBit = (ushort)_DatabitComboBox.SelectedIndex; };

            _BufferSpaceBox.ValueChanged += (s, e) => { _ViewData.BufferSpace = (int)_BufferSpaceBox.Value; };

            _IsDTRCheckBox.CheckedChanged += (s, e) => { _ViewData.IsDTR = _IsDTRCheckBox.Checked; };
            _IsRTSCheckBox.CheckedChanged += (s, e) => { _ViewData.IsRTS = _IsRTSCheckBox.Checked; };
            _IsFormatTextCheckBox.CheckedChanged += (s, e) => { _ViewData.DisplayFormatTextEnable = _IsFormatTextCheckBox.Checked; };
            _IsHexViewCheckBox.CheckedChanged += (s, e) =>
            {
                _IsFormatTextCheckBox.Enabled = !_IsHexViewCheckBox.Checked;
                _ViewData.HexShowEnable = _IsHexViewCheckBox.Checked;
            };
        }

        private void ViewModelDataBinding()
        {
            _BaudRatesComboBox.SelectedIndex = _ViewData.SelectBaudRate;
            _ParitysComboBox.SelectedIndex = _ViewData.SelectParity;
            _StopBitsesComboBox.SelectedIndex = _ViewData.SelectStopBit;
            _DatabitComboBox.SelectedIndex = _ViewData.SelectDataBit;

            _BufferSpaceBox.Value = _ViewData.BufferSpace;
            _IsDTRCheckBox.Checked = _ViewData.IsDTR;
            _IsRTSCheckBox.Checked = _ViewData.IsRTS;

            _IsHexViewCheckBox.Checked = _ViewData.HexShowEnable;
            _IsFormatTextCheckBox.Checked = _ViewData.DisplayFormatTextEnable;
        }

        private void ViewModelEventManage()
        {
            _ViewData.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(_ViewData.BufferSpace):
                        _BufferSpaceBox.Value = _ViewData.BufferSpace;
                        break;
                    case nameof(_ViewData.IsDTR):
                        _IsDTRCheckBox.Checked = _ViewData.IsDTR;
                        break;
                    case nameof(_ViewData.IsRTS):
                        _IsRTSCheckBox.Checked = _ViewData.IsRTS;
                        break;
                    case nameof(_ViewData.SelectBaudRate):
                        _BaudRatesComboBox.SelectedIndex = _ViewData.SelectBaudRate;
                        break;
                    case nameof(_ViewData.SelectDataBit):
                        _DatabitComboBox.SelectedIndex = _ViewData.SelectDataBit;
                        break;
                    case nameof(_ViewData.SelectParity):
                        _ParitysComboBox.SelectedIndex = _ViewData.SelectParity;
                        break;
                    case nameof(_ViewData.SelectStopBit):
                        _StopBitsesComboBox.SelectedIndex = _ViewData.SelectStopBit;
                        break;
                    case nameof(_ViewData.DisplayFormatTextEnable):
                        _IsFormatTextCheckBox.Checked = _ViewData.DisplayFormatTextEnable;
                        break;
                    case nameof(_ViewData.HexShowEnable):
                        _IsHexViewCheckBox.Checked = _ViewData.HexShowEnable;
                        break;
                }
            };
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
