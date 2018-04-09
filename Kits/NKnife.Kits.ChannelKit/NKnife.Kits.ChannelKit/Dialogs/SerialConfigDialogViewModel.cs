using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using GalaSoft.MvvmLight;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit.Dialogs
{
    public class SerialConfigDialogViewModel : ViewModelBase
    {
        private bool _HexShowEnable = true;
        private bool _DisplayFormatTextEnable = false;
        private int _BufferSpace = 64;
        private ushort _SelectBaudRate = SerialUtils.DefaultBaudRate;
        private ushort _SelectStopBit = SerialUtils.DefaultStopBit;
        private ushort _SelectParity = SerialUtils.DefaultParity;
        private ushort _SelectDataBit = SerialUtils.DefaultDataBit;
        private bool _IsDTR = false;
        private bool _IsRTS = true;

        public ushort SelectBaudRate
        {
            get => _SelectBaudRate;
            set { Set(() => SelectBaudRate, ref _SelectBaudRate, value); }
        }

        public ushort SelectStopBit
        {
            get => _SelectStopBit;
            set { Set(() => SelectStopBit, ref _SelectStopBit, value); }
        }

        public ushort SelectParity
        {
            get => _SelectParity;
            set { Set(() => SelectParity, ref _SelectParity, value); }
        }

        public ushort SelectDataBit
        {
            get => _SelectDataBit;
            set { Set(() => SelectDataBit, ref _SelectDataBit, value); }
        }

        public bool IsDTR
        {
            get => _IsDTR;
            set { Set(() => IsDTR, ref _IsDTR, value); }
        }

        public bool IsRTS
        {
            get => _IsRTS;
            set { Set(() => IsRTS, ref _IsRTS, value); }
        }

        public int BufferSpace
        {
            get => _BufferSpace;
            set { Set(() => BufferSpace, ref _BufferSpace, value); }
        }

        /// <summary>
        /// 非串口配置。是否以16进制显示。
        /// </summary>
        public bool HexShowEnable
        {
            get => _HexShowEnable;
            set { Set(() => HexShowEnable, ref _HexShowEnable, value); }
        }

        /// <summary>
        /// 非串口配置。是否以格式化文本显示。
        /// </summary>
        public bool DisplayFormatTextEnable
        {
            get => _DisplayFormatTextEnable;
            set { Set(() => DisplayFormatTextEnable, ref _DisplayFormatTextEnable, value); }
        }

        private SerialConfig _Config;
        /// <summary>
        ///     导出Serial配置
        /// </summary>
        public SerialConfig Export()
        {
            var list0 = new List<object>();
            list0.AddRange(SerialUtils.BaudRates);
            _Config.BaudRate = int.Parse(list0[SelectBaudRate].ToString());

            var list1 = new List<object>();
            list1.AddRange(SerialUtils.StopBits);
            _Config.StopBit = (StopBits)list1[SelectStopBit];

            var list2 = new List<object>();
            list2.AddRange(SerialUtils.Parities);
            _Config.Parity = (Parity)list2[SelectParity];

            var list3 = new ushort[] { 5, 6, 7, 8 };
            _Config.DataBit = list3[SelectDataBit];

            _Config.DtrEnable = IsDTR;
            _Config.RtsEnable = IsRTS;
            _Config.ReadBufferSize = BufferSpace;

            return _Config;
        }

        public void Import(SerialConfig config)
        {
            _Config = config;
            SelectBaudRate = (ushort) SerialUtils.BaudRates.ToList().IndexOf(config.BaudRate);
            SelectStopBit = (ushort) SerialUtils.StopBits.ToList().IndexOf(config.StopBit);
            SelectParity = (ushort) SerialUtils.Parities.ToList().IndexOf(config.Parity);
            SelectDataBit = (ushort) SerialUtils.DataBits.ToList().IndexOf(config.DataBit);

            BufferSpace = config.ReadBufferSize;
            IsDTR = config.DtrEnable;
            IsRTS = config.RtsEnable;
        }
    }
}