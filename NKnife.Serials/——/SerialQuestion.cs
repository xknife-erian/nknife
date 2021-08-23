using System;
using System.Collections.Generic;
using NKnife.Channels.Base;

namespace NKnife.Channels.Serial
{
    /// <summary>
    /// �����豸��PC���ڷ��صĽ�������
    /// </summary>
    public class SerialQuestion : ChannelJobBase<IEnumerable<byte>>
    {
        private readonly Guid _id;

        /// <summary>
        /// �����豸��PC���ڷ��صĽ�������
        /// </summary>
        public SerialQuestion(IEnumerable<byte> data)
            : this(data, false, -1)
        {
        }

        /// <summary>
        /// �����豸��PC���ڷ��صĽ�������
        /// </summary>
        public SerialQuestion(IEnumerable<byte> data, bool isLoop, int loopInterval)
            : base(data, isLoop, loopInterval)
        {
            _id = Guid.NewGuid();
        }

        /// <inheritdoc />
        public override IEnumerable<byte> Answer { get; set; }

        #region Overrides of Object

        /// <summary>ȷ��ָ���� <see cref="T:System.Object" /> �Ƿ���ڵ�ǰ�� <see cref="T:System.Object" />��</summary>
        /// <returns>���ָ���� <see cref="T:System.Object" /> ���ڵ�ǰ�� <see cref="T:System.Object" />����Ϊ true������Ϊ false��</returns>
        /// <param name="obj">�뵱ǰ�� <see cref="T:System.Object" /> ���бȽϵ� <see cref="T:System.Object" />��</param>
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

        /// <summary>�����ض����͵Ĺ�ϣ������</summary>
        /// <returns>��ǰ <see cref="T:System.Object" /> �Ĺ�ϣ���롣</returns>
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        #endregion

        #endregion

    }
}