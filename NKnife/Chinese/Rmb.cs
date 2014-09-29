using System;
using System.Text;
using NKnife.Utility.Maths;

namespace NKnife.Chinese
{
    /// <summary>
    /// һ����������ҵĽṹ
    /// </summary>
    public struct Rmb
    {
        private readonly int _Digit;
        private readonly int _Number;
        private readonly char _NumberChar;
        private readonly char _UnitChar;

        public Rmb(int digit, char number)
            : this(digit, int.Parse(number.ToString()))
        {
        }

        public Rmb(int digit, string number)
            : this(digit, int.Parse(number))
        {
        }

        public Rmb(int digit, int number)
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

        public static bool operator ==(Rmb a, Rmb b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Rmb a, Rmb b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            var r = (Rmb) (obj);
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

        public static string ToUpperChineseRmb(string numString, UtilityMath.RoundingMode roundMode = UtilityMath.RoundingMode.Rounding4She5Ru)
        {
            decimal num;
            if (!decimal.TryParse(numString, out num))
                throw new ArgumentException("�����ֵ��ַ�����");
            return ToUpperChineseRmb(num, roundMode);
        }

        public static string ToUpperChineseRmb(decimal num, UtilityMath.RoundingMode roundMode = UtilityMath.RoundingMode.Rounding4She5Ru)
        {
            return ToUpperChineseRmb(num);
        }

        /// <summary> 
        /// ��ָ��������ת��������ҵĴ�д��ʽ 
        /// </summary> 
        /// <param name="num">���.������,С��9����,����-9����</param>
        /// <returns>���ش�д��ʽ</returns> 
        private static string ToUpperChineseRmb(decimal num)
        {
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("Ӧ���ý��Ϊ����"));
            }
            num = System.Math.Round(num, 2); //��numȡ����ֵ����������ȡ2λС�� 
            string sourceNum = ((long)(num * 100)).ToString();
            if (sourceNum.Length > 15)
            {
                throw new ArgumentOutOfRangeException(string.Format("���.������,С��9����"));
            }

            const string shuzi = "��Ҽ��������½��ƾ�"; //0-9����Ӧ�ĺ��� 
            string danwei = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԪ�Ƿ�"; //����λ����Ӧ�ĺ��� 
            string result = ""; //����Ҵ�д�����ʽ 
            string weiCn = ""; //����λ�ĺ��ֶ��� 
            int nZero = 0; //����������������ֵ�Ǽ��� 

            int index = sourceNum.Length;
            danwei = danwei.Substring(15 - index); //ȡ����Ӧλ����str2��ֵ���磺200.55,jΪ5����str2=��ʰԪ�Ƿ� 

            //ѭ��ȡ��ÿһλ��Ҫת����ֵ 
            for (int i = 0; i < index; i++)
            {
                string tmpStringNum = sourceNum.Substring(i, 1); //��ԭnumֵ��ȡ����ֵ 
                int tmpIntNum = Convert.ToInt32(tmpStringNum); //��ԭnumֵ��ȡ����ֵ 
                string numCn; //���ֵĺ������ 
                if (i != (index - 3) && i != (index - 7) && i != (index - 11) && i != (index - 15))
                {
                    //����ȡλ����ΪԪ�����ڡ������ϵ�����ʱ 
                    if (tmpStringNum == "0")
                    {
                        numCn = "";
                        weiCn = "";
                        nZero = nZero + 1;
                    }
                    else
                    {
                        if (tmpStringNum != "0" && nZero != 0)
                        {
                            numCn = "��" + shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                        else
                        {
                            numCn = shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                    }
                }
                else
                {
                    //��λ�����ڣ��ڣ���Ԫλ�ȹؼ�λ 
                    if (tmpStringNum != "0" && nZero != 0)
                    {
                        numCn = "��" + shuzi.Substring(tmpIntNum * 1, 1);
                        weiCn = danwei.Substring(i, 1);
                        nZero = 0;
                    }
                    else
                    {
                        if (tmpStringNum != "0" && nZero == 0)
                        {
                            numCn = shuzi.Substring(tmpIntNum * 1, 1);
                            weiCn = danwei.Substring(i, 1);
                            nZero = 0;
                        }
                        else
                        {
                            if (tmpStringNum == "0" && nZero >= 3)
                            {
                                numCn = "";
                                weiCn = "";
                                nZero = nZero + 1;
                            }
                            else
                            {
                                if (index >= 11)
                                {
                                    numCn = "";
                                    nZero = nZero + 1;
                                }
                                else
                                {
                                    numCn = "";
                                    weiCn = danwei.Substring(i, 1);
                                    nZero = nZero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (index - 11) || i == (index - 3))
                {
                    //�����λ����λ��Ԫλ�������д�� 
                    weiCn = danwei.Substring(i, 1);
                }
                result = result + numCn + weiCn;

                if (i == index - 1 && tmpStringNum == "0")
                {
                    //���һλ���֣�Ϊ0ʱ�����ϡ����� 
                    result = result + '��';
                }
            }
            if (num == 0)
            {
                result = "��Ԫ��";
            }
            return result;
        }

    }
}