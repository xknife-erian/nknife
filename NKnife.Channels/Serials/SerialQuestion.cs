using System;
using NKnife.Channels.Base;
using NKnife.Interface;

namespace NKnife.Channels.Serials
{
    /// <summary>
    /// �����豸��PC���ڷ��صĽ�������
    /// </summary>
    public class SerialQuestion : ChannelJobBase<byte[]>
    {
        private readonly Guid _id;

        /// <summary>
        /// �����豸��PC���ڷ��صĽ�������
        /// </summary>
        public SerialQuestion(byte[] data)
            : this(data, false, -1)
        {
        }

        /// <summary>
        /// �����豸��PC���ڷ��صĽ�������
        /// </summary>
        public SerialQuestion(byte[] data, bool isLoop, int loopInterval)
            : base(data, isLoop, loopInterval)
        {
            _id = Guid.NewGuid();
        }

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