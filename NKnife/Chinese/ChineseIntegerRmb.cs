using System.Text;

namespace NKnife.Chinese
{
    /// <summary>
    /// һ��������������ҵĽṹ
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
        /// Gets λ������.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return _Number; }
        }

        /// <summary>
        /// Gets �ڼ�λ.
        /// </summary>
        /// <value>The digit.</value>
        public int Digit
        {
            get { return _Digit; }
        }

        /// <summary>
        /// Gets ת���ɵĴ�д�ַ�.
        /// </summary>
        /// <value>The number char.</value>
        public char NumberChar
        {
            get { return _NumberChar; }
        }

        /// <summary>
        /// Gets ת���ɵĵ�λ�ַ�.
        /// </summary>
        /// <value>The unit char.</value>
        public char UnitChar
        {
            get { return _UnitChar; }
        }

        /// <summary>
        /// Gets 0-9����Ӧ�ĺ���
        /// </summary>
        public static string ChineseNumber
        {
            get { return "��Ҽ��������½��ƾ�"; }
        }

        /// <summary>
        /// Gets ����λ����Ӧ�ĺ��� 
        /// </summary>
        public static string ChineseUnit
        {
            get { return "�ֽ�Ԫʰ��Ǫ��ʰ��Ǫ��ʰ��Ǫ��"; }
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
        /// �����ַ���,��ʹ�á�F����Ϊ��ʽ�������
        /// </summary>
        public override string ToString()
        {
            return ToString("F");
        }

        /// <summary>
        /// ���ݸ�ʽ��Ҫ�󷵻��ַ�����
        /// ��ʽ����
        /// D:����������������Ҵ�д�ַ�����
        /// F:�������ǰλ����Ҵ�д�ַ���������λ��
        /// </summary>
        /// <param name="format">
        /// ��ʽ����
        /// D:����������������Ҵ�д�ַ�����
        /// F:�������ǰλ����Ҵ�д�ַ���������λ��
        ///</param>
        public string ToString(string format)
        {
            switch (format)
            {
                case "D":
                {
                    if (_Number == 0)
                        return "��Ԫ��";
                    var sb = new StringBuilder();
                    sb.Append(_NumberChar).Append(_UnitChar);
                    switch (_Digit)
                    {
                        case 1: //����
                        case 2: //����
                        case 3: //Ԫ��
                            sb.Append("��");
                            break;
                        case 4: //ʰԪ��
                        case 5: //��Ԫ��
                        case 6: //ǧԪ��
                        case 7: //��Ԫ��
                            sb.Append("Ԫ��");
                            break;
                        case 8: //ʰ��Ԫ��
                        case 9: //����Ԫ��
                        case 10: //ǧ��Ԫ��
                            sb.Append("��Ԫ��");
                            break;
                        case 11: //��Ԫ��
                            sb.Append("Ԫ��");
                            break;
                        case 12: //ʰ��Ԫ��
                        case 13: //����Ԫ��
                        case 14: //ǧ��Ԫ��
                        case 15: //����Ԫ��
                            sb.Append("��Ԫ��");
                            break;
                    }
                    return sb.ToString();
                }
                default:
                {
                    if (_Number == 0)
                        return "��";
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