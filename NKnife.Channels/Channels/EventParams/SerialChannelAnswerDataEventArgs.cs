using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.EventParams
{
    public class SerialChannelAnswerDataEventArgs : ChannelAnswerDataEventArgs<byte[]>
    {
        public SerialChannelAnswerDataEventArgs(IChannel<byte[]> channel, IId instrument, IId target, byte[] data) 
            : base(new SerialAnswer(channel, instrument, target, data))
        {
        }
    }
}