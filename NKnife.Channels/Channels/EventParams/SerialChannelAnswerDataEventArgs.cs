using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.EventParams
{
    public class SerialChannelAnswerDataEventArgs : ChannelAnswerDataEventArgs<byte[]>
    {
        public SerialChannelAnswerDataEventArgs(IId instrument, byte[] data) 
            : base(new SerialAnswer(instrument, data))
        {
        }
    }
}