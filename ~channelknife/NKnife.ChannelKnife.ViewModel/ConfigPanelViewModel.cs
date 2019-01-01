using System.Collections.Generic;
using System.IO.Ports;
using NKnife.ChannelKnife.Model;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class ConfigPanelViewModel : ReactiveObject
    {
        private bool _IsHexShow = true;
        private bool _IsFormatText = false;

        public ConfigPanelViewModel()
        {
            SelectBaudRate = SerialInfoService.DefaultBaudRate;
            SelectStopBit = SerialInfoService.DefaultStopBit;
            SelectDataBit = SerialInfoService.DefaultDataBit;
            SelectParity = SerialInfoService.DefaultParity;
        }

        public ushort SelectBaudRate { get; set; }

        public ushort SelectStopBit { get; set; } = 1;

        public ushort SelectParity { get; set; } = 1;

        public ushort SelectDataBit { get; set; } = 3;

        public bool IsDTR { get; set; } = false;

        public bool IsRTS { get; set; } = false;

        public bool IsHexShow
        {
            get => _IsHexShow;
            set => this.RaiseAndSetIfChanged(ref _IsHexShow, value);
        }

        public bool IsFormatText
        {
            get => _IsFormatText;
            set => this.RaiseAndSetIfChanged(ref _IsFormatText, value);
        }

        public int BufferSpace { get; set; } = 64;

        /*
        /// <summary>
        ///     导出Serial配置
        /// </summary>
        public SerialConfig Export(ushort port)
        {
            var config = new SerialConfig(port);

            var list0 = new List<object>();
            list0.AddRange(SerialUtils.BaudRates);
            config.BaudRate = int.Parse(list0[SelectBaudRate].ToString());

            var list1 = new List<object>();
            list1.AddRange(SerialUtils.StopBits);
            config.StopBits = (StopBits)list1[SelectStopBit];

            var list2 = new List<object>();
            list2.AddRange(SerialUtils.Paritys);
            config.Parity = (Parity)list2[SelectParity];

            var list3 = new ushort[] { 5, 6, 7, 8 };
            config.DataBits = list3[SelectDataBit];

            config.DtrEnable = IsDTR;
            config.RtsEnable = IsRTS;
            config.ReadBufferSize = BufferSpace;

            return config;
        }
        */
    }
}