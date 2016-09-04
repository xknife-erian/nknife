using System;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>交易信息。
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

        /// <summary>用户Id
        /// </summary>
        public string User { get; set; }

        /// <summary>生成这条交易的Activity的Kind
        /// </summary>
        public int Owner { get; set; }

        /// <summary>前置的Transaction的ID的集合
        /// </summary>
        public string[] Previous { get; set; }

        /// <summary>识别码。这个识别码仅限定在短暂的时间内(当天，中午等)是不重复的。在排队系统中，这是一个排队的票号。
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>预约的队列的Id
        /// </summary>
        public string Queue { get; set; }

        /// <summary>记录交易的开始时间。
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