using System;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.Serials
{
    /// <summary>
    /// 描述设备向PC串口返回的交换数据
    /// </summary>
    public class SerialQuestion : QuestionBase<byte[]>
    {
        private readonly Guid _Id;
        /// <summary>
        /// 描述设备向PC串口返回的交换数据
        /// </summary>
        public SerialQuestion(IChannel<byte[]> channel, IDevice device, IId target, bool isLoop, byte[] data) 
            : base(channel, device, target, isLoop, data)
        {
            _Id = Guid.NewGuid();
        }

        #region Overrides of Object

        /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
        /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
        public override bool Equals(object obj)
        {
            if (!(obj is SerialQuestion))
                return false;
            return Equals((SerialQuestion) obj);
        }

        #region Equality members

        protected bool Equals(SerialQuestion other)
        {
            return _Id.Equals(other._Id);
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return _Id.GetHashCode();
        }

        #endregion

        #endregion
    }
}