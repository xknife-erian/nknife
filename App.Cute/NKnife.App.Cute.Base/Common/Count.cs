using System;

namespace Didaku.Engine.Timeaxis.Base.Common
{
    /// <summary>ͳ������
    /// </summary>
    public struct Count : IEquatable<Count>
    {
        /// <summary>���ڵȴ�
        /// </summary>
        public int OnWait { get; set; }

        /// <summary>���ڷ���
        /// </summary>
        public int OnServe { get; set; }

        /// <summary>δ����
        /// </summary>
        public int UnServe { get; set; }

        /// <summary>�������
        /// </summary>
        public int Finished { get; set; }

        /// <summary>��Ʊ
        /// </summary>
        public int GivenUp { get; set; }

        /// <summary>���׹رա�(������״̬��δ������Ʊ���������)
        /// </summary>
        public int Closed
        {
            get { return UnServe + GivenUp + Finished; }
        }

        /// <summary>���������С���������״̬�����ڵȴ������ڷ���
        /// </summary>
        public int Running
        {
            get { return OnServe + OnWait; }
        }

        #region Implementation of IEquatable<Count>

        public override string ToString()
        {
            return string.Format("OnWait:{0},OnServe:{1},UnServe:{2},Finished:{3},GivenUp:{4},Closed:{5},Running:{6}",
                                 OnWait, OnServe, UnServe, Finished, GivenUp, Closed, Running);
        }

        public bool Equals(Count other)
        {
            return other.OnWait == OnWait &&
                   other.OnServe == OnServe &&
                   other.UnServe == UnServe &&
                   other.Finished == Finished &&
                   other.GivenUp == GivenUp;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            if (obj.GetType() != typeof (Count)) 
                return false;
            return Equals((Count) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = OnWait;
                result = (result*397) ^ OnServe;
                result = (result*397) ^ UnServe;
                result = (result*397) ^ Finished;
                result = (result*397) ^ GivenUp;
                return result;
            }
        }

        public static bool operator ==(Count left, Count right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Count left, Count right)
        {
            return !left.Equals(right);
        }

        public static Count operator +(Count left, Count right)
        {
            var count = new Count
            {
                OnWait = left.OnWait + right.OnWait, 
                OnServe = left.OnServe + right.OnServe, 
                Finished = left.Finished + right.Finished, 
                GivenUp = left.GivenUp + right.GivenUp, 
                UnServe = left.UnServe + right.UnServe
            };
            return count;
        }

        public static Count operator -(Count left, Count right)
        {
            var count = new Count
            {
                OnWait = left.OnWait - right.OnWait, 
                OnServe = left.OnServe - right.OnServe, 
                Finished = left.Finished - right.Finished, 
                GivenUp = left.GivenUp - right.GivenUp, 
                UnServe = left.UnServe - right.UnServe
            };
            return count;
        }

        public static Count Sum(params Count[] counts)
        {
            var count = new Count();
            foreach (var c in counts)
            {
                count.OnWait = count.OnWait + c.OnWait;
                count.OnServe = count.OnServe + c.OnServe; 
                count.Finished = count.Finished + c.Finished; 
                count.GivenUp = count.GivenUp + c.GivenUp;
                count.UnServe = count.UnServe + c.UnServe;
            }
            return count;
        }

        #endregion
    }
}