using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.Serials
{
    public class SerialAnswer : AnswerBase<byte[]>
    {
        public SerialAnswer(IChannel<byte[]> channel, IDevice device, IId target, byte[] data) 
            : base(channel, device, target, data)
        {
        }
    }
}
