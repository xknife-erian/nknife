using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NKnife.Util
{
    /// <summary>
    /// 针对.net的Random随机数生成器的扩展。
    /// 2008年9月9日16时46分
    /// </summary>
    public static class RandomUtil
    {
        #region RandomCharType enum

        /// <summary>
        /// 枚举：生成的随机字符串（数字与大小写字母）的组合类型。
        /// </summary>
        public enum RandomCharType
        {
            /// <summary>
            /// 任意。数字与大小写字母。
            /// </summary>
            All,

            /// <summary>
            /// 数字。
            /// </summary>
            Number,

            /// <summary>
            /// 大写字母。
            /// </summary>
            Uppercased,

            /// <summary>
            /// 小写字母。
            /// </summary>
            Lowercased,

            /// <summary>
            /// 数字与大写字母。
            /// </summary>
            NumberAndUppercased,

            /// <summary>
            /// 数字与小写字母。
            /// </summary>
            NumberAndLowercased,

            /// <summary>
            /// 小写字母与大写字母。
            /// </summary>
            UppercasedAndLowercased,

            /// <summary>
            /// 嘛也不是
            /// </summary>
            None,
        }

        #endregion

        /// <summary>
        /// 大小写字母与数字(以英文逗号相隔)
        /// </summary>
        private const string CHAR_TO_SPLIT = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        /// <summary>构造函数
        /// </summary>
        static RandomUtil()
        {
            int seed = (int) DateTime.Now.Ticks & 0x0000FFFF;
            Random = new Random((int)seed);
        }

        /// <summary>
        /// 表示伪随机数生成器。静态属性。
        /// </summary>
        public static Random Random { get; set; }

        /// <summary>返回非负随机数。
        /// </summary>
        /// <returns>返回大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数。</returns>
        public static int Next()
        {
            return Random.Next();
        }

        /// <summary>返回一个小于所指定最大值的非负随机数。
        /// </summary>
        /// <param name="maxValue">要生成的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于零。</param>
        /// <returns>大于等于零且小于 maxValue 的 32 位带符号整数，即：返回值的范围通常包括零但不包括 maxValue。不过，如果 maxValue 等于零，则返回maxValue。</returns>
        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        /// <summary>返回一个指定范围内的随机数。
        /// </summary>
        /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
        /// <param name="maxValue">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。</param>
        /// <returns>一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。如果minValue 等于 maxValue，则返回 minValue。</returns>
        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        /// <summary>获取一定数量的随机整数，可能会有重复。
        /// </summary>
        /// <param name="count">需获得随机整数的数量</param>
        /// <param name="minValue">随机整数的最小值</param>
        /// <param name="maxValue">随机整数的最大值</param>
        /// <returns></returns>
        public static IEnumerable<int> GetRandomNumbersWithRepeats(int count, int minValue, int maxValue)
        {
            var numList = new int[count];
            for (int i = 0; i < count; i++)
            {
                numList[i] = Random.Next(minValue, maxValue);
            }
            return numList;
        }

        /// <summary>获取一定数量不重复的随机整数。
        /// </summary>
        /// <param name="count">需获得随机整数的数量</param>
        /// <param name="minValue">随机整数的最小值</param>
        /// <param name="maxValue">随机整数的最大值</param>
        /// <returns></returns>
        public static IEnumerable<int> GetRandomNumbersWithoutRepeats(int count, int minValue = 0, int maxValue = 100)
        {
            if (minValue > maxValue || count > maxValue - minValue + 1)
            {
                throw new ArgumentException("Count must be less than or equal to maxValue - minValue + 1");
            }

            List<int> allNumbers = Enumerable.Range(minValue, maxValue - minValue + 1).ToList();
            allNumbers.Shuffle(Random);
            return allNumbers.GetRange(0, count);
        }

        ///<summary>洗牌算法，打乱列表中的元素顺序</summary>  
        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        /// <summary>获取指定长度的(单字节)字符串
        /// </summary>
        /// <param name="length">所需字符串的长度</param>
        /// <param name="type">字符串中的字符的类型</param>
        /// <returns></returns>
        public static string GetRandomStringByLength(int length, RandomCharType type)
        {
            StringBuilder sb = new StringBuilder(length);

            string valid = string.Empty;

            switch (type)
            {
                case RandomCharType.All:
                    valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    break;
                case RandomCharType.Number:
                    valid = "0123456789";
                    break;
                case RandomCharType.Uppercased:
                    valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case RandomCharType.Lowercased:
                    valid = "abcdefghijklmnopqrstuvwxyz";
                    break;
                case RandomCharType.NumberAndUppercased:
                    valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    break;
                case RandomCharType.NumberAndLowercased:
                    valid = "abcdefghijklmnopqrstuvwxyz0123456789";
                    break;
                case RandomCharType.UppercasedAndLowercased:
                    valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case RandomCharType.None:
                    throw new ArgumentException("Cannot generate a string of type None.");
                default:
                    throw new ArgumentException("Invalid RandomCharType.");
            }
            
            for (int i = 0; i < length; i++)
            {
                sb.Append(valid[Random.Next(valid.Length)]);
            }

            return sb.ToString();
        }

        /// <summary>生成随机的银行卡卡号
        /// </summary>
        /// <param name="prefix"> </param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetFixNumberString(int prefix = 0, int length = 16)
        {
            if (length < 1)
                throw new ArgumentException("卡号长度不能小于1");
            var prefixLength = MathUtil.GetLength(prefix);
            var sb = new StringBuilder(length);
            if (prefix > 0)
            {
                if (prefixLength > length)
                    sb.Append(prefix.ToString().Substring(0, length));
                else
                    sb.Append(prefix);
            }
            for (int i = 0; i < length - prefixLength; i++)
            {
                sb.Append(Random.Next(0, 9));
            }
            return sb.ToString();
        }
    }
}