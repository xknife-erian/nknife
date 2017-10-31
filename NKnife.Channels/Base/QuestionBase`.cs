using NKnife.Channels.Interfaces;
using NKnife.Interface;

namespace NKnife.Channels.Base
{
    /// <summary>
    /// 描述仪器向PC采集端返回的交换数据的基类
    /// </summary>
    public abstract class QuestionBase<T> : IQuestion<T>
    {
        protected QuestionBase(IId instrument, bool isLoop, int loopInterval, T data)
        {
            //Channel = channel;
            Instrument = instrument;
            LoopInterval = loopInterval;
            //Target = target;
            IsLoop = isLoop;
            Data = data;
        }

        #region Implementation of IDeviceSwap

        /// <summary>
        /// 该询问是否需要循环。true时需要,false时仅问询一次。
        /// </summary>
        public bool IsLoop { get; set; }

        /// <summary>
        /// 当循环时的间隔
        /// </summary>
        public int LoopInterval { get; set; }

        /// <summary>
        /// 本次询问的超时时长
        /// </summary>
        public abstract int Timeout { get; set; }

        /// <summary>
        /// 本次交换的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     被问询的设备
        /// </summary>
        public IId Instrument { get; }

        #endregion
    }
}
