using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Jeelu
{
    /// <summary>
    /// Css属性值的集合。Values是值的集合(真正的Css值)。Values是名称的集合(用来给用户显示)。
    /// </summary>
    public class CssResourceList
    {
        private List<string> _innerValues = new List<string>();
        private List<string> _innerTexts = new List<string>();

        private string[] _values;
        /// <summary>
        /// 值的集合(真正的Css值)
        /// </summary>
        public string[] Values
        {
            get
            {
                if (_values == null)
                {
                    _values = _innerValues.ToArray();
                }
                return _values;
            }
        }

        private string[] _texts;
        /// <summary>
        /// 名称的集合(用来给用户显示)
        /// </summary>
        public string[] Texts
        {
            get
            {
                if (_texts == null)
                {
                    _texts = _innerTexts.ToArray();
                }
                return _texts;
            }
        }

        public int Count
        {
            get { return _innerValues.Count; }
        }
        
        public CssResourceList()
        {
        }

        /// <summary>
        /// 返回KeyValuePair,Key是Name,Value是Text
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> this[int index]
        {
            get
            {
                string value = _innerValues[index];
                string text = _innerTexts[index];

                return new KeyValuePair<string, string>(value, text);
            }
        }

        internal void Add(string value, string text)
        {
            _innerValues.Add(value);
            _innerTexts.Add(text);
        }

        private int IndexOfValue(string value)
        {
            return _innerValues.IndexOf(value);
        }

        private int IndexOfText(string text)
        {
            return _innerTexts.IndexOf(text);
        }

        /// <summary>
        /// 通过名称取值。若没有则返回null
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string TextToValue(string text)
        {
            int index = IndexOfText(text);
            if (index == -1)
            {
                return null;
            }

            return Values[index];
        }

        /// <summary>
        /// 通过值取名称。若没有则返回null
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string ValueToText(string value)
        {
            int index = IndexOfValue(value);
            if (index == -1)
            {
                return null;
            }

            return Texts[index];
        }

        /// <summary>
        /// 通过字符串，取值。若没有则返回null
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetValue(string item)
        {
            ///先在Texts里取
            int index = IndexOfText(item);

            ///没有则继续在Valus里取
            if (index == -1)
            {
                index = IndexOfValue(item);
            }

            ///有则返回此值
            if (index != -1)
            {
                return Values[index];
            }

            return null;
        }

        /// <summary>
        /// 指定字符串是否存在于Texts或Values中
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool HasValue(string item)
        {
            ///先在Texts里取
            int index = IndexOfText(item);

            ///没有则继续在Valus里取
            if (index == -1)
            {
                index = IndexOfValue(item);
            }

            return index != -1;
        }

        /// <summary>
        /// 通过字符串，取值。若没有则返回null
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetValueAny(string item)
        {
            ///先在Texts里取
            int index = IndexOfText(item);

            ///没有则继续在Valus里取
            if (index == -1)
            {
                index = IndexOfValue(item);
            }

            ///有则返回此值
            if (index != -1)
            {
                return Values[index];
            }

            return item;
        }
    }
}
