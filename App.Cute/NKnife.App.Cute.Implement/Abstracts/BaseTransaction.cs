using System;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>������Ϣ��
    /// </summary>
    public abstract class BaseTransaction : ITransaction, IEquatable<BaseTransaction>
    {
        protected BaseTransaction()
        {
            Id = Guid.NewGuid().ToString("N");
            Time = DateTime.Now;
        }

        #region ITransaction Members

        public string Id { get; set; }

        /// <summary>�û�Id
        /// </summary>
        public string User { get; set; }

        /// <summary>�����������׵�Activity��Kind
        /// </summary>
        public int Owner { get; set; }

        /// <summary>ǰ�õ�Transaction��ID�ļ���
        /// </summary>
        public string[] Previous { get; set; }

        /// <summary>ʶ���롣���ʶ������޶��ڶ��ݵ�ʱ����(���죬�����)�ǲ��ظ��ġ����Ŷ�ϵͳ�У�����һ���Ŷӵ�Ʊ�š�
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>ԤԼ�Ķ��е�Id
        /// </summary>
        public string Queue { get; set; }

        /// <summary>��¼���׵Ŀ�ʼʱ�䡣
        /// </summary>
        public DateTime Time { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}",
                                 Identifier, 
                                 Queue, 
                                 Time.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff"), 
                                 User, 
                                 Id.Substring(0, 12), 
                                 Previous != null ? Previous.Length.ToString() : string.Empty);
        }

        #region Equality members

        public bool Equals(BaseTransaction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseTransaction) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public static bool operator ==(BaseTransaction left, BaseTransaction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseTransaction left, BaseTransaction right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}