using System;

namespace NKnife.App.Cute.Base.Attributes
{
    /// <summary>一个描述活动类型的实现类型定制特性
    /// 以下是基本约定：
    /// 1.0000 - 1999的均为Booking类型的交易；
    /// 2.2000 - 5999的均为Running类型的交易；
    /// 3.6000 - 8999的均为Commenting类型的交易（7000后是Commented类型的）；
    /// 4.9000 - 9999的均为Finished类型的交易；
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ActivityImplAttribute : Attribute, IEquatable<ActivityImplAttribute>, IComparable<ActivityImplAttribute>
    {
        /// <summary>构造函数:一个描述活动类型的实现类型定制特性
        /// </summary>
        /// <param name="id">该Activity在系统中唯一可辨别的分类编码，不允许重复。
        /// 1.0000 - 1999的均为Booking类型的交易；
        /// 2.2000 - 5999的均为Running类型的交易；
        /// 3.6000 - 8999的均为Commenting类型的交易；
        /// 4.9000 - 9999的均为Finished类型的交易；
        /// </param>
        /// <param name="description">描述信息</param>
        public ActivityImplAttribute(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }

        #region IComparable<ActivityImplAttribute> Members

        public int CompareTo(ActivityImplAttribute other)
        {
            return Id - other.Id;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ActivityImplAttribute && Equals((ActivityImplAttribute) obj);
        }

        #region Equality members

        public bool Equals(ActivityImplAttribute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ Id;
            }
        }

        public static bool operator ==(ActivityImplAttribute left, ActivityImplAttribute right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ActivityImplAttribute left, ActivityImplAttribute right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}