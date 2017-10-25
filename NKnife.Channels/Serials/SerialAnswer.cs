using NKnife.Channels.Base;
using NKnife.Interface;

namespace NKnife.Channels.Serials
{
    public class SerialAnswer : AnswerBase<byte[]>
    {
        public SerialAnswer(IId instrument, byte[] data) 
            : base(instrument, data)
        {
        }
    }
}
