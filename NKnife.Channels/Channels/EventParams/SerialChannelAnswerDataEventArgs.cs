using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.EventParams
{
    public class SerialChannelAnswerDataEventArgs : ChannelAnswerDataEventArgs<byte[]>
    {
        public SerialChannelAnswerDataEventArgs(IChannel<byte[]> channel, IDevice device, IId target, byte[] data) 
            : base(new SerialAnswer(channel, device, target, data))
        {
        }
    }
}