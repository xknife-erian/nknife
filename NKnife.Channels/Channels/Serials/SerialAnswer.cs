using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.Serials
{
    public class SerialAnswer : AnswerBase<byte[]>
    {
        public SerialAnswer(IChannel<byte[]> channel, IId instrument, IId target, byte[] data) 
            : base(channel, instrument, target, data)
        {
        }
    }
}
