using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit.Services
{
    public class ChannelService
    {
        public void Initialize()
        {
            SerialUtils.RefreshSerialPorts();
        }
    }
}