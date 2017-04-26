using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Channels.Channels.Base
{
    public class AnswerBase<T> : IAnswer<T>
    {
        protected AnswerBase(IChannel<T> channel, IDevice device, IExhibit exhibit, T data)
        {
            Channel = channel;
            Device = device;
            Exhibit = exhibit;
            Data = data;
        }

        #region Implementation of IDeviceSwap

        /// <summary>
        /// 本次交换的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 数据采集通道
        /// </summary>
        public IChannel<T> Channel { get; }

        /// <summary>
        /// 数据采集的仪器
        /// </summary>
        public IDevice Device { get; }

        /// <summary>
        /// 被采集、观察的对象，如电阻源，电压源等。
        /// </summary>
        public IExhibit Exhibit { get; }

        #endregion
    }
}
