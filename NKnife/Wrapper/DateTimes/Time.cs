using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    [Serializable]
    public struct Time : IComparable, IFormattable, IConvertible, ISerializable, IComparable<Time>, IEquatable<Time> 
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }
        public int Millisecond { get; private set; }

        public Time(DateTime dateTime)
            :this(dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond)
        {
        }

        public Time(int hour, int minute, int second, int millisecond) 
            : this()
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;
        }

        #region Implementation of IComparable

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IFormattable

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IConvertible

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IComparable<in Time>

        public int CompareTo(Time other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IEquatable<Time>

        public bool Equals(Time other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
