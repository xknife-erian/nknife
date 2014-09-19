using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu
{
    /// <summary>
    /// һ��Css���ԡ���width:12px;
    /// </summary>
    public class CssProperty
    {
        protected string _name;
        /// <summary>
        /// Css����������ơ���width
        /// </summary>
        public string Name
        {
            get { return _name;
            }
        }

        protected string _value;
        /// <summary>
        /// Css�������ֵ����12px
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                Debug.Assert(value != null);
                if (_value != value)
                {
                    _value = value;

                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        public CssProperty(string name)
            : this(name, "")
        {
        }

        public CssProperty(string name,string value)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            Debug.Assert(value != null);

            _name = name;
            _value = value;
        }

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        /// <summary>
        /// �����ֺ�ֵ������������Ϊwidth:12px;����ʽ
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + ":" + Value + ";";
        }

        static public CssProperty Parse(string cssText)
        {
            cssText = cssText.Trim();

            int colonIndex = cssText.IndexOf(':');

            ///��������Ч��
            if (colonIndex < 0 || cssText.StartsWith("<!--"))
            {
                ///��Ϊע�ͻ��ʽ���Ϸ�ʱ��CssOtherProperty�洢��������������
                return new CssOtherProperty(cssText);
            }
            if (cssText[cssText.Length - 1] != ';')
            {
                cssText += ";";
            }

            string strName = cssText.Substring(0, colonIndex).Trim().ToLower();
            string strValue = cssText.Substring(colonIndex + 1, cssText.Length - colonIndex - 2).Trim();

            return new CssProperty(strName, strValue);
        }

        /// <summary>
        /// ����һ��CssProperty��Value����"12px"������һ��KeyValuePair��KeyΪֵ��"12"��ValueΪ��λ����"px"��
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public KeyValuePair<string, string> ParseFieldUnit(string value)
        {
            Debug.Assert(value != null);

            ///ȥ����β�ո�
            value = value.Trim();

            string strField = "";
            string strUnit = "";

            ///�ȴ���%
            if (value.EndsWith("%"))
            {
                strUnit = "%";
                strField = value.Substring(0, value.Length - 1);
            }
            ///��%�Ĵ���
            else
            {
                ///��valueС��2���ַ�������Ϊû�е�λ
                if (value.Length < 2)
                {
                    strUnit = "";
                    strField = value;
                }
                ///value����2���ַ����жϺ���λ�Ƿ�ָ���ĵ�λ
                else
                {
                    string lastTwoWorld = value.Substring(value.Length - 2, 2).ToLower();
                    switch (lastTwoWorld)
                    {
                        case "px":
                        case "pt":
                        case "in":
                        case "cm":
                        case "mm":
                        case "pc":
                        case "em":
                        case "ex":
                            strUnit = lastTwoWorld;
                            strField = value.Substring(0, value.Length - 2);
                            break;

                        ///��������Ϊ����ֵ��û�е�λ
                        default:
                            strUnit = "";
                            strField = value;
                            break;
                    }
                }
            }

            return new KeyValuePair<string, string>(strField, strUnit);
        }
    }

    internal class CssOtherProperty : CssProperty
    {
        public CssOtherProperty(string value)
            :base(Guid.NewGuid().ToString("N"),value)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
