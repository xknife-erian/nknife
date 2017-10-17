using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.Base
{
    public class AnswerBase<T> : IAnswer<T>
    {
        //protected AnswerBase(IChannel<T> channel, IId device, IId target, T data)
        protected AnswerBase(IId device, T data)
        {
            //Channel = channel;
            Instrument = device;
            //Target = target;
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
        //public IChannel<T> Channel { get; }

        /// <summary>
        /// 执行数据采集的仪器
        /// </summary>
        public IId Instrument { get; }

        /// <summary>
        /// 被采集、观察的对象，如电阻源，电压源等。
        /// </summary>
        //public IId Target { get; }

        #endregion
    }
}
