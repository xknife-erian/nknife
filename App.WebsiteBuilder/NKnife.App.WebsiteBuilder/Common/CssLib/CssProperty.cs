using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu
{
    /// <summary>
    /// 一个Css属性。如width:12px;
    /// </summary>
    public class CssProperty
    {
        protected string _name;
        /// <summary>
        /// Css属性里的名称。如width
        /// </summary>
        public string Name
        {
            get { return _name;
            }
        }

        protected string _value;
        /// <summary>
        /// Css属性里的值。如12px
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
        /// 将名字和值组合起来，输出为width:12px;的形式
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

            ///检查参数有效性
            if (colonIndex < 0 || cssText.StartsWith("<!--"))
            {
                ///当为注释或格式不合法时用CssOtherProperty存储，不做其他处理。
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
        /// 传入一个CssProperty的Value，如"12px"。返回一个KeyValuePair，Key为值如"12"，Value为单位，如"px"。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public KeyValuePair<string, string> ParseFieldUnit(string value)
        {
            Debug.Assert(value != null);

            ///去除首尾空格
            value = value.Trim();

            string strField = "";
            string strUnit = "";

            ///先处理%
            if (value.EndsWith("%"))
            {
                strUnit = "%";
                strField = value.Substring(0, value.Length - 1);
            }
            ///非%的处理
            else
            {
                ///若value小于2个字符，则认为没有单位
                if (value.Length < 2)
                {
                    strUnit = "";
                    strField = value;
                }
                ///value大于2个字符，判断后两位是否指定的单位
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

                        ///不是则认为都是值，没有单位
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
