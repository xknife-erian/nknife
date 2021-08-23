using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using NKnife.ShareResources;

namespace NKnife.Util
{
    /// <summary>
    /// 转换抽象类
    /// </summary>
    public static class UtilConvert
    {
        #region ConvertMode enum

        /// <summary>
        ///     转换时的模式，一般指是严格转换还是宽松的转换
        /// </summary>
        public enum ConvertMode
        {
            /// <summary>
            ///     严格的
            /// </summary>
            Strict,

            /// <summary>
            ///     宽松的
            /// </summary>
            Relaxed
        }

        #endregion

        #region 基础转换

        /// <summary>
        /// 字符串转换成枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumString">字符串</param>
        /// <returns>枚举值</returns>
        public static T StringToEnum<T>(string enumString) where T : struct
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("类型必须是枚举", "T");
            }
            Enum e;
            try
            {
                e = (Enum)Enum.Parse(type, enumString, true);
            }
            catch
            {
                e = default(T) as Enum;
            }
            return (T)System.Convert.ChangeType(e, type);
        }

        public static T EnumParse<T>(object obj, T defaultEnum) where T : struct
        {
            if (obj != null)
                return EnumParse(obj.ToString(), defaultEnum);
            return defaultEnum;
        }

        public static T EnumParse<T>(string value, T defaultEnum) where T : struct
        {
            return Enum.TryParse(value, out T rtn) ? rtn : defaultEnum;
        }

        public static Guid GuidParse(object obj)
        {
            if (obj is DBNull || obj == null) return Guid.Empty;
            return GuidParse(obj.ToString());
        }

        /// <summary>
        ///     解析一个可能是Guid的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Guid GuidParse(string value)
        {
            return !Guid.TryParse(value, out var n) ? Guid.Empty : n;
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        public static int Int32Parse(object obj, int whenParseFail = 0)
        {
            if (obj is DBNull) return whenParseFail;
            if (obj == null) return whenParseFail;
            return Int32Parse(obj.ToString(), whenParseFail);
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        private static int Int32Parse(string value, int whenParseFail)
        {
            if (!int.TryParse(value, out var n))
                return whenParseFail;
            return n;
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        public static short Int16Parse(object obj, short whenParseFail = 0)
        {
            if (obj is DBNull || obj == null) return whenParseFail;
            return Int16Parse(obj.ToString(), whenParseFail);
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        private static short Int16Parse(string value, short whenParseFail)
        {
            if (!short.TryParse(value, out var n))
                return whenParseFail;
            return n;
        }

        /// <summary>
        ///     考虑得比较全面的字符串向Bool值的解析方法(如果是Int值，大于0均为True)
        /// </summary>
        public static bool BoolParse(object obj)
        {
            if (obj is DBNull) return false;
            if (obj == null) return false;
            return BoolParse(obj.ToString());
        }

        /// <summary>
        ///     考虑得比较全面的字符串向Bool值的解析方法(如果是Int值，大于0均为True)
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static bool BoolParse(string v)
        {
            if (string.IsNullOrWhiteSpace(v))
                return false;
            if (v.ToLower().Equals("true"))
                return true;
            int.TryParse(v, out var i);
            return IntToBoolean(i);
        }

        /// <summary>
        ///     填充数据表时将为Null的对象转换为DBNull，如果不是，原样返回原值
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static object NullToDBNull(object obj)
        {
            object value = DBNull.Value;
            if (obj != null)
                value = obj;
            return value;
        }

        #endregion

        #region 各进制数间转换

        /// <summary>
        ///     实现各进制数间的转换。如：ConvertBase(10, "15", 16)表示将10进制数15转换为16进制的数。
        /// </summary>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(int from, string value, int to)
        {
            // 检查参数
            if (from != 2 && from != 8 && from != 10 && from != 16) 
                throw new ArgumentOutOfRangeException(nameof(@from));
            if (to != 2 && to != 8 && to != 10 && to != 16) 
                throw new ArgumentOutOfRangeException(nameof(to));

            //将要转换的原值尝试转换成一个Int值
            if (!int.TryParse(value, out _)) 
                throw new ArgumentNullException(nameof(value));

            var intValue = Convert.ToInt32(value, from); //先转成10进制
            var result = Convert.ToString(intValue, to); //再转成目标进制

            //if (to == 2)
            //{
            //    StringBuilder sb = new StringBuilder(8);
            //    switch (result.Length)
            //    {
            //        case 8:
            //            sb.Append(result);
            //            break;
            //        case 7:
            //            sb.Append("0").Append(result);
            //            break;
            //        case 6:
            //            sb.Append("00").Append(result);
            //            break;
            //        case 5:
            //            sb.Append("000").Append(result);
            //            break;
            //        case 4:
            //            sb.Append("0000").Append(result);
            //            break;
            //        case 3:
            //            sb.Append("00000").Append(result);
            //            break;
            //    }
            //    return sb.ToString();
            //}
            return result;
        }

        #endregion

        #region FromString ToString

        /// <summary>
        ///     转换指定的字符串为指定的类型，如转换不成功，将返回指定的类型的默认值
        ///     <param name="v">指定的字符串</param>
        ///     <param name="defaultValue">指定的类型的默认值</param>
        /// </summary>
        public static T FromString<T>(string v, T defaultValue)
        {
            if (string.IsNullOrEmpty(v))
                return defaultValue;
            if (typeof(T) == typeof(string))
                return (T)(object)v;
            try
            {
                var c = TypeDescriptor.GetConverter(typeof(T));
                return (T)c.ConvertFromInvariantString(v);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     转换指定的类型为字符串，如转换不成功，将返回Null
        /// </summary>
        public static string ToString<T>(T val)
        {
            if (typeof(T) == typeof(string))
            {
                var s = (string)(object)val;
                return string.IsNullOrEmpty(s) ? null : s;
            }

            try
            {
                var c = TypeDescriptor.GetConverter(typeof(T));
                var s = c.ConvertToInvariantString(val);
                return string.IsNullOrEmpty(s) ? null : s;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region ConvertTo 将数据转换为指定类型

        #region 重载一

        /// <summary>
        ///     将数据转换为指定类型，一般用在实现了IConvertible接口的类型
        /// </summary>
        /// <param name="data">转换的数据</param>
        /// <param name="targetType">转换的目标类型</param>
        public static object ConvertTo(object data, Type targetType)
        {
            //如果数据为空，则返回
            if (data.IsNullOrEmpty()) return null;

            try
            {
                //如果数据实现了IConvertible接口，则转换类型
                if (data is IConvertible) return Convert.ChangeType(data, targetType);
                return data;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 重载二

        /// <summary>
        ///     将数据转换为指定类型，一般用在实现了IConvertible接口的类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
        public static T ConvertTo<T>(object data)
        {
            //如果数据为空，则返回
            if (data.IsNullOrEmpty()) return default(T);

            try
            {
                //如果数据是T类型，则直接转换
                if (data is T) return (T)data;

                //如果目标类型是枚举
                if (typeof(T).BaseType == typeof(Enum)) return UtilEnum.GetInstance<T>(data);

                //如果数据实现了IConvertible接口，则转换类型
                if (data is IConvertible) return (T)Convert.ChangeType(data, typeof(T));
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #endregion

        #region 将整型变量转化为布尔变量(True或False).

        /// <summary>
        ///     将整型变量转化为布尔变量(True或False).
        ///     规则：如果整型数值大于0,返回True,否则返回False.（非严格模式）
        /// </summary>
        /// <param name="intParam">整型数</param>
        /// <returns>如果整型数值大于0,返回True,否则返回False.</returns>
        public static bool IntToBoolean(int intParam)
        {
            return intParam > 0;
        }

        /// <summary>
        ///     将整型变量转化为布尔变量(True或False).
        ///     规则：如果整型数值大于0,返回True,否则返回False.
        /// </summary>
        /// <param name="intParam">The int param.</param>
        /// <param name="mode">严格模式：只能转换0或1；宽松模式：大于0,返回True,否则返回False.</param>
        /// <returns></returns>
        public static bool IntToBoolean(int intParam, ConvertMode mode)
        {
            switch (mode)
            {
                case ConvertMode.Strict:
                    {
                        switch (intParam)
                        {
                            case 0:
                                return false;
                            case 1:
                                return true;
                        }

                        throw new ArgumentOutOfRangeException(string.Format(ArgumentValidationString.ValueMustIs0or1, "intParam"));
                    }
                case ConvertMode.Relaxed:
                    return IntToBoolean(intParam);
                default:
                    Debug.Fail(mode.ToString());
                    return false;
            }
        }

        #endregion

        #region 将char转化为布尔变量(True或False).

        /// <summary>
        ///     将char转化为布尔变量(True或False).
        /// </summary>
        /// <param name="charParam">char值</param>
        /// <returns>如果char是0,返回False；如果char是1,返回True</returns>
        public static bool CharToBoolean(char charParam)
        {
            return CharToBoolean(charParam, ConvertMode.Relaxed);
        }

        /// <summary>
        ///     将char转化为布尔变量(True或False).
        /// </summary>
        /// <param name="charParam">char值</param>
        /// <param name="mode">选择是否严格转换模式，当宽松模式下，非0或1的char都将返回false</param>
        /// <returns>如果char是0,返回False；如果char是1,返回True</returns>
        public static bool CharToBoolean(char charParam, ConvertMode mode)
        {
            switch (charParam)
            {
                case '1':
                    return true;
                case '0':
                    return false;
                default:
                    if (mode == ConvertMode.Relaxed) return false;
                    throw new ArgumentException(charParam + " , 参数应严格是1或0.");
            }
        }

        #endregion

        #region object和base64之间的转换

        public static string FileToBase64(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        public static byte[] Base64ToByteArray(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        #endregion
    }
}
