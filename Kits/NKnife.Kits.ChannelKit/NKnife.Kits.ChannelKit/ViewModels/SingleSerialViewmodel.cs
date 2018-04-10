using GalaSoft.MvvmLight;
using NKnife.Channels.Serials;
using NKnife.IoC;
using NKnife.Kits.ChannelKit.Dialogs;
using NKnife.Kits.ChannelKit.Services;

namespace NKnife.Kits.ChannelKit.ViewModels
{
    public class SingleSerialViewmodel : ViewModelBase
    {
        private ChannelService _ChannelService = DI.Get<ChannelService>();

        private bool _HexShowEnable;
        private bool _DisplayFormatTextEnable;

        public bool IsOpen { get; private set; } = false;
        public ushort Port { get; set; } = 0;

        /// <summary>
        /// 操作串口，并返回操作的动作成功状态
        /// </summary>
        /// <returns>返回操作的动作成功状态</returns>
        public bool OpenOrClosePort()
        {
            var channel = _ChannelService.GetChannel(Port);
            //会根据实际对串口的操作状态来描述串口的当前状态
            var success = !IsOpen ? channel.Open() : channel.Close();

            if (success)
                IsOpen = !IsOpen;
            return success;
        }

        public SerialConfigDialog.SelfModel FromConfig()
        {
            var config = _ChannelService.GetConfig(Port);
            var model = new SerialConfigDialog.SelfModel();
            model.Commons.BaudRate = config.BaudRate;
            model.Commons.DataBit = config.DataBit;
            model.Commons.Parity = config.Parity;
            model.Commons.StopBit = config.StopBit;
            model.Commons.DtrEnable = config.DtrEnable;
            model.Commons.RtsEnable = config.RtsEnable;
            model.Commons.ReadBufferSize = config.ReadBufferSize;

            model.Professions.ReadTotalTimeoutConstant = config.ReadTotalTimeoutConstant;
            model.Professions.ReadWait = config.ReadWait;
            model.Professions.ReceivedBytesThreshold = config.ReceivedBytesThreshold;
            model.Professions.WriteTotalTimeoutConstant = config.WriteTotalTimeoutConstant;

            model.AppSettings.DisplayFormatTextEnable = _DisplayFormatTextEnable;
            model.AppSettings.HexShowEnable = _HexShowEnable;
            return model;
        }

        public void UpdataConfig(SerialConfigDialog.SelfModel model)
        {
            var config = _ChannelService.GetConfig(Port);

            config.BaudRate = model.Commons.BaudRate;
            config.DataBit = model.Commons.DataBit;
            config.Parity = model.Commons.Parity;
            config.StopBit = model.Commons.StopBit;
            config.DtrEnable = model.Commons.DtrEnable;
            config.RtsEnable = model.Commons.RtsEnable;
            config.ReadBufferSize = model.Commons.ReadBufferSize;

            config.ReadTotalTimeoutConstant = model.Professions.ReadTotalTimeoutConstant;
            config.ReadWait = model.Professions.ReadWait;
            config.ReceivedBytesThreshold = model.Professions.ReceivedBytesThreshold;
            config.WriteTotalTimeoutConstant = model.Professions.WriteTotalTimeoutConstant;

            _DisplayFormatTextEnable = model.AppSettings.DisplayFormatTextEnable;
            _HexShowEnable = model.AppSettings.HexShowEnable;
        }
    }
}