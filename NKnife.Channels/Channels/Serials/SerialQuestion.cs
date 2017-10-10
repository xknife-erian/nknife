using System;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;
using NKnife.Interface;

namespace NKnife.Channels.Channels.Serials
{
    /// <summary>
    /// �����豸��PC���ڷ��صĽ�������
    /// </summary>
    public class SerialQuestion : QuestionBase<byte[]>
    {
        private readonly Guid _Id;
        /// <summary>
        /// �����豸��PC���ڷ��صĽ�������
        /// </summary>
        public SerialQuestion(IChannel<byte[]> channel, IDevice device, IId target, bool isLoop, byte[] data) 
            : base(channel, device, target, isLoop, data)
        {
            _Id = Guid.NewGuid();
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
            return _Id.Equals(other._Id);
        }

        /// <summary>�����ض����͵Ĺ�ϣ������</summary>
        /// <returns>��ǰ <see cref="T:System.Object" /> �Ĺ�ϣ���롣</returns>
        public override int GetHashCode()
        {
            return _Id.GetHashCode();
        }

        #endregion

        #endregion
    }
}