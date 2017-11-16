using NKnife.Channels.Serials;
using NKnife.Interface;

namespace NKnife.Channels.EventParams
{
    public class SerialChannelAnswerDataEventArgs : ChannelAnswerDataEventArgs<byte[]>
    {
        public SerialChannelAnswerDataEventArgs(IId instrument, byte[] data) 
            : base(new SerialAnswer(instrument, data))
        {
        }
    }
}