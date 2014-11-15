using System;

namespace NKnife.App.Cute.Base.Attributes
{
    /// <summary>һ����������͵�ʵ�����Ͷ�������
    /// �����ǻ���Լ����
    /// 1.0000 - 1999�ľ�ΪBooking���͵Ľ��ף�
    /// 2.2000 - 5999�ľ�ΪRunning���͵Ľ��ף�
    /// 3.6000 - 8999�ľ�ΪCommenting���͵Ľ��ף�7000����Commented���͵ģ���
    /// 4.9000 - 9999�ľ�ΪFinished���͵Ľ��ף�
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ActivityImplAttribute : Attribute, IEquatable<ActivityImplAttribute>, IComparable<ActivityImplAttribute>
    {
        /// <summary>���캯��:һ����������͵�ʵ�����Ͷ�������
        /// </summary>
        /// <param name="id">��Activity��ϵͳ��Ψһ�ɱ��ķ�����룬�������ظ���
        /// 1.0000 - 1999�ľ�ΪBooking���͵Ľ��ף�
        /// 2.2000 - 5999�ľ�ΪRunning���͵Ľ��ף�
        /// 3.6000 - 8999�ľ�ΪCommenting���͵Ľ��ף�
        /// 4.9000 - 9999�ľ�ΪFinished���͵Ľ��ף�
        /// </param>
        /// <param name="description">������Ϣ</param>
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