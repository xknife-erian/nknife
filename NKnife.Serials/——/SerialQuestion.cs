using System;
using System.Collections.Generic;
using NKnife.Channels.Base;

namespace NKnife.Channels.Serial
{
    /// <summary>
    /// 描述设备向PC串口返回的交换数据
    /// </summary>
    public class SerialQuestion : ChannelJobBase<IEnumerable<byte>>
    {
        private readonly Guid _id;

        /// <summary>
        /// 描述设备向PC串口返回的交换数据
        /// </summary>
        public SerialQuestion(IEnumerable<byte> data)
            : this(data, false, -1)
        {
        }

        /// <summary>
        /// 描述设备向PC串口返回的交换数据
        /// </summary>
        public SerialQuestion(IEnumerable<byte> data, bool isLoop, int loopInterval)
            : base(data, isLoop, loopInterval)
        {
            _id = Guid.NewGuid();
        }

        /// <inheritdoc />
        public override IEnumerable<byte> Answer { get; set; }

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
            return _id.Equals(other._id);
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        #endregion

        #endregion

    }
}