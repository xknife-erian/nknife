using System;
using NKnife.Channels.Base;
using NKnife.Interface;

namespace NKnife.Channels.Serials
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
        public SerialQuestion(IId instrument, bool isLoop, int loopInterval, byte[] data)
            : base(instrument, isLoop, loopInterval, data)
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

        #region Overrides of QuestionBase<byte[]>

        /// <summary>
        /// ����ѯ�ʵĳ�ʱʱ��
        /// </summary>
        public override int Timeout { get; set; }

        #endregion
    }
}