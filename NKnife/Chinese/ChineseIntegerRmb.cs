using System.Text;

namespace NKnife.Chinese
{
    /// <summary>
    /// 一个描述整数人民币的结构
    /// </summary>
    public struct ChineseIntegerRmb
    {
        private readonly int _Digit;
        private readonly int _Number;
        private readonly char _NumberChar;
        private readonly char _UnitChar;

        public ChineseIntegerRmb(int digit, char number)
            : this(digit, int.Parse(number.ToString()))
        {
        }

        public ChineseIntegerRmb(int digit, string number)
            : this(digit, int.Parse(number))
        {
        }

        public ChineseIntegerRmb(int digit, int number)
        {
            _Number = number;
            _Digit = digit;
            _NumberChar = GetNumberChar(number);
            _UnitChar = GetUnitChar(digit);
        }

        /// <summary>
        /// Gets 位的数字.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return _Number; }
        }

        /// <summary>
        /// Gets 第几位.
        /// </summary>
        /// <value>The digit.</value>
        public int Digit
        {
            get { return _Digit; }
        }

        /// <summary>
        /// Gets 转换成的大写字符.
        /// </summary>
        /// <value>The number char.</value>
        public char NumberChar
        {
            get { return _NumberChar; }
        }

        /// <summary>
        /// Gets 转换成的单位字符.
        /// </summary>
        /// <value>The unit char.</value>
        public char UnitChar
        {
            get { return _UnitChar; }
        }

        /// <summary>
        /// Gets 0-9所对应的汉字
        /// </summary>
        public static string ChineseNumber
        {
            get { return "零壹贰叁肆伍陆柒捌玖"; }
        }

        /// <summary>
        /// Gets 数字位所对应的汉字 
        /// </summary>
        public static string ChineseUnit
        {
            get { return "分角元拾佰仟万拾佰仟亿拾佰仟万"; }
        }

        public static bool operator ==(ChineseIntegerRmb a, ChineseIntegerRmb b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ChineseIntegerRmb a, ChineseIntegerRmb b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            var r = (ChineseIntegerRmb) (obj);
            if (!_Number.Equals(r._Number)) return false;
            if (!_Digit.Equals(r._Digit)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 27 ^ _Digit ^ _Number;
        }

        /// <summary>
        /// 返回字符串,即使用“F”作为格式符输出。
        /// </summary>
        public override string ToString()
        {
            return ToString("F");
        }

        /// <summary>
        /// 根据格式符要求返回字符串。
        /// 格式符：
        /// D:输出完整意义的人民币大写字符串；
        /// F:输出仅当前位人民币大写字符串，即两位。
        /// </summary>
        /// <param name="format">
        /// 格式符：
        /// D:输出完整意义的人民币大写字符串；
        /// F:输出仅当前位人民币大写字符串，即两位。
        ///</param>
        public string ToString(string format)
        {
            switch (format)
            {
                case "D":
                {
                    if (_Number == 0)
                        return "零元整";
                    var sb = new StringBuilder();
                    sb.Append(_NumberChar).Append(_UnitChar);
                    switch (_Digit)
                    {
                        case 1: //分整
                        case 2: //角整
                        case 3: //元整
                            sb.Append("整");
                            break;
                        case 4: //拾元整
                        case 5: //百元整
                        case 6: //千元整
                        case 7: //万元整
                            sb.Append("元整");
                            break;
                        case 8: //拾万元整
                        case 9: //百万元整
                        case 10: //千万元整
                            sb.Append("万元整");
                            break;
                        case 11: //亿元整
                            sb.Append("元整");
                            break;
                        case 12: //拾亿元整
                        case 13: //百亿元整
                        case 14: //千亿元整
                        case 15: //万亿元整
                            sb.Append("亿元整");
                            break;
                    }
                    return sb.ToString();
                }
                default:
                {
                    if (_Number == 0)
                        return "零";
                    var sb = new StringBuilder(2);
                    sb.Append(_NumberChar).Append(_UnitChar);
                    return sb.ToString();
                }
            }
        }

        public static char GetNumberChar(int number)
        {
            return ChineseNumber[number];
        }

        public static char GetUnitChar(int digit)
        {
            return ChineseUnit[digit - 1];
        }
    }
}