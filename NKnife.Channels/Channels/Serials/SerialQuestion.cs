using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Channels.Channels.Serials
{
    /// <summary>
    /// �����豸��PC���ڷ��صĽ�������
    /// </summary>
    public class SerialQuestion : QuestionBase<byte[]>
    {
        /// <summary>
        /// �����豸��PC���ڷ��صĽ�������
        /// </summary>
        public SerialQuestion(IChannel<byte[]> channel, IDevice device, IExhibit exhibit, bool isLoop, byte[] data) 
            : base(channel, device, exhibit, isLoop, data)
        {
        }
    }
}