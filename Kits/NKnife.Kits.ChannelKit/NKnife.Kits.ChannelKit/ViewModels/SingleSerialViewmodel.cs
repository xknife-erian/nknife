using GalaSoft.MvvmLight;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit.ViewModels
{
    public class SingleSerialViewmodel : ViewModelBase
    {
        private SerialConfig _Config;
        private SerialChannel _Channel;

        public SerialConfig Config
        {
            get => _Config;
            set { Set(() => Config, ref _Config, value); }
        }

        public bool IsOpen { get; private set; } = false;

        /// <summary>
        /// 操作串口，并返回操作的动作成功状态
        /// </summary>
        /// <returns>返回操作的动作成功状态</returns>
        public bool OperatingPort()
        {
            bool success;//会根据实际对串口的操作状态来描述串口的当前状态
            if (!IsOpen)
            {
                _Channel = new SerialChannel(_Config);
                success = _Channel.Open();
            }
            else
            {
                success = _Channel.Close();
            }
            if (success)
                IsOpen = !IsOpen;
            return success;
        }
    }
}