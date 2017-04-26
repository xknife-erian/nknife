using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Channels.Channels.EventParams
{
    public class SerialChannelAnswerDataEventArgs : ChannelAnswerDataEventArgs<byte[]>
    {
        public SerialChannelAnswerDataEventArgs(IChannel<byte[]> channel, IDevice device, IExhibit exhibit, byte[] data) 
            : base(new SerialAnswer(channel, device, exhibit, data))
        {
        }
    }
}