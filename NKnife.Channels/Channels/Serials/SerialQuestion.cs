using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Channels.Channels.Serials
{
    /// <summary>
    /// 描述设备向PC串口返回的交换数据
    /// </summary>
    public class SerialQuestion : QuestionBase<byte[]>
    {
        /// <summary>
        /// 描述设备向PC串口返回的交换数据
        /// </summary>
        public SerialQuestion(IChannel<byte[]> channel, IDevice device, IExhibit exhibit, bool isLoop, byte[] data) 
            : base(channel, device, exhibit, isLoop, data)
        {
        }
    }
}