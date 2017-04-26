using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Channels.Channels.Serials
{
    public class SerialAnswer : AnswerBase<byte[]>
    {
        public SerialAnswer(IChannel<byte[]> channel, IDevice device, IExhibit exhibit, byte[] data) 
            : base(channel, device, exhibit, data)
        {
        }
    }
}
