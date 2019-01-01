using System.Windows.Forms;
using NKnife.ChannelKnife.ViewModel;

namespace NKnife.ChannelKnife.Views.Controls
{
    public partial class ConfigPanel : UserControl
    {
        private ConfigPanelViewModel _ViewData;

        public ConfigPanel()
        {
            InitializeComponent();

//            _BaudRatesComboBox.Items.AddRange(SerialInfoService.BaudRates);
//            _ParitysComboBox.Items.AddRange(SerialInfoService.Paritys);
//            _StopBitsesComboBox.Items.AddRange(SerialInfoService.StopBits);
//            _DatabitComboBox.Items.AddRange(SerialInfoService.DataBits);

            ControlEventManage();
        }

        public ConfigPanelViewModel ViewData
        {
            get { return _ViewData; }
            set
            {
                _ViewData = value;
                if (value != null)
                    FromViewData(value);
            }
        }

        private void ControlEventManage()
        {
            _BaudRatesComboBox.SelectedValueChanged += (sender, args) => { _ViewData.SelectBaudRate = (ushort) _BaudRatesComboBox.SelectedIndex; };
            _ParitysComboBox.SelectedValueChanged += (sender, args) => { _ViewData.SelectParity = (ushort) _ParitysComboBox.SelectedIndex; };
            _StopBitsesComboBox.SelectedValueChanged += (sender, args) => { _ViewData.SelectStopBit = (ushort) _StopBitsesComboBox.SelectedIndex; };
            _DatabitComboBox.SelectedValueChanged += (sender, args) => { _ViewData.SelectDataBit = (ushort) _DatabitComboBox.SelectedIndex; };

            _BufferSpaceBox.ValueChanged += (sender, args) => { _ViewData.BufferSpace = (int) _BufferSpaceBox.Value; };

            _IsDTRCheckBox.CheckedChanged += (sender, args) => { _ViewData.IsDTR = _IsDTRCheckBox.Checked; };
            _IsRTSCheckBox.CheckedChanged += (sender, args) => { _ViewData.IsRTS = _IsRTSCheckBox.Checked; };
            _IsFormatTextCheckBox.CheckedChanged += (sender, args) => { _ViewData.IsFormatText = _IsFormatTextCheckBox.Checked; };
            _IsHexViewCheckBox.CheckedChanged += (sender, args) =>
            {
                _IsFormatTextCheckBox.Enabled = !_IsHexViewCheckBox.Checked;
                _ViewData.IsHexShow = _IsHexViewCheckBox.Checked;
            };
        }

        private void FromViewData(ConfigPanelViewModel data)
        {
            _BaudRatesComboBox.SelectedIndex = data.SelectBaudRate;
            _ParitysComboBox.SelectedIndex = data.SelectParity;
            _StopBitsesComboBox.SelectedIndex = data.SelectStopBit;
            _DatabitComboBox.SelectedIndex = data.SelectDataBit;
            _BufferSpaceBox.Value = data.BufferSpace;
            _IsDTRCheckBox.Checked = data.IsDTR;
            _IsRTSCheckBox.Checked = data.IsRTS;
            _IsHexViewCheckBox.Checked = data.IsHexShow;
            _IsFormatTextCheckBox.Checked = data.IsFormatText;
        }
    }
}