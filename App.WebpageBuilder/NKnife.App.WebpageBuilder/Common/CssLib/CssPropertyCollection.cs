using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Jeelu
{
    public class CssPropertyCollection : IList<CssProperty>
    {
        private List<CssProperty> _innerList = new List<CssProperty>();
        private Dictionary<string, CssProperty> _innerDic = new Dictionary<string, CssProperty>();

        #region IList<CssProperty> 成员

        public int IndexOf(CssProperty item)
        {
            return _innerList.IndexOf(item);
        }

        public int IndexOf(string name)
        {
            for (int i = 0; i < _innerList.Count; i++)
            {
                if (string.Equals(_innerList[i].Name, name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, CssProperty item)
        {
            _innerList.Insert(index, item);
            _innerDic.Add(item.Name, item);
        }

        public void RemoveAt(int index)
        {
            CssProperty prop = _innerList[index];
            Remove(prop);
        }

        public CssProperty this[int index]
        {
            get
            {
                return _innerList[index];
            }
            set
            {
                CssProperty temp = _innerList[index];
                _innerList[index] = value;
                _innerDic.Remove(temp.Name);
                _innerDic.Add(value.Name, value);
            }
        }

        public string this[string name]
        {
            get
            {
                ///直接找这个名字的Css属性
                CssProperty outValue = TryGetValueEx(name);
                if (outValue != null)
                {
                    return outValue.Value;
                }

                ///此名字没有找到属性，则检查其是否对应简写属性
                return ParseShortcutProperty(name);
            }
            set
            {
                if (_innerDic.ContainsKey(name))
                {
                    _innerDic[name].Value = value;
                }
                else
                {
                    Add(name, value);
                }
            }
        }

        private CssProperty TryGetValueEx(string name)
        {
            CssProperty outProperty;
            if (!_innerDic.TryGetValue(name, out outProperty))
            {
                return null;
            }
            return outProperty;
        }

        #endregion

        #region ICollection<CssProperty> 成员

        public void Add(CssProperty item)
        {
            ///若有相同的，将原来的替换成无用型，放新的
            if (_innerDic.ContainsKey(item.Name))
            {
                ///取原来的
                CssProperty oldProperty = _innerDic[item.Name];
                int oldIndex = IndexOf(oldProperty);

                ///删除
                Remove(oldProperty);

                ///放无用型
                oldProperty = new CssOtherProperty(oldProperty.ToString());
                Insert(oldIndex, oldProperty);
            }

            _innerList.Add(item);
            _innerDic.Add(item.Name, item);
        }

        public void Clear()
        {
            _innerList.Clear();
            _innerDic.Clear();
        }

        public bool Contains(CssProperty item)
        {
            return _innerList.Contains(item);
        }

        public void CopyTo(CssProperty[] array, int arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CssProperty item)
        {
            bool bl = _innerList.Remove(item);
            if (bl)
            {
                _innerDic.Remove(item.Name);
            }
            return bl;
        }

        #endregion

        #region IEnumerable<CssProperty> 成员

        public IEnumerator<CssProperty> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_innerList).GetEnumerator();
        }

        #endregion

        public void AddRange(IEnumerable<CssProperty> enumera)
        {
            foreach (CssProperty prop in enumera)
            {
                if (prop == null)
                {
                    continue;
                }
                _innerList.Add(prop);
                _innerDic.Add(prop.Name, prop);
            }
        }

        public void Add(string name, string value)
        {
            Add(new CssProperty(name, value));
        }

        public void Remove(string name)
        {
            CssProperty property;
            if (_innerDic.TryGetValue(name, out property))
            {
                Remove(property);
            }
        }

        public bool ContainsName(string name)
        {
            return _innerDic.ContainsKey(name);
        }

        public bool TryGetValue(string name, out string value)
        {
            value = this[name];
            if (!string.IsNullOrEmpty(value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 查找此Css属性名字的简写属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ParseShortcutProperty(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            string returnValue = null;
            name = name.Trim().ToLower();

            switch (name)
            {
                case "padding-top":
                case "padding-right":
                case "padding-bottom":
                case "padding-left":
                case "margin-top":
                case "margin-right":
                case "margin-bottom":
                case "margin-left":
                    {
                        returnValue = GetDirectionShortcut(name);
                        break;
                    }

                case "list-style-type":
                case "list-style-position":
                case "list-style-image":
                    {
                        returnValue = GetListShortcut(name);
                        break;
                    }

                case "background-color":
                case "background-position":
                case "background-attachment":
                case "background-repeat":
                case "background-image":
                    {
                        returnValue = GetBackgroundShortcut(name);
                        break;
                    }

                case "border-top-width":
                case "border-right-width":
                case "border-bottom-width":
                case "border-left-width":
                case "border-top-color":
                case "border-right-color":
                case "border-bottom-color":
                case "border-left-color":
                case "border-top-style":
                case "border-right-style":
                case "border-bottom-style":
                case "border-left-style":
                    {
                        returnValue = GetBorderShortcut(name);
                        break;
                    }
            }

            return returnValue;
        }

        private string GetFourOne(string value, Direction direction)
        {
            Debug.Assert(value != null);
            string returnValue = null;

            value = value.Trim();
            if (value == "")
            {
                return "";
            }

            string[] values = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (values.Length)
            {
                case 1:
                    ///1个字段，则所有的都一样
                    returnValue = values[0];
                    break;
                case 2:
                    ///2个字段，则第一个表示顶和底，第二个表示左和右
                    switch (direction)
                    {
                        case Direction.Top:
                        case Direction.Bottom:
                            returnValue = values[0];
                            break;
                        case Direction.Right:
                        case Direction.Left:
                            returnValue = values[1];
                            break;
                        default:
                            Debug.Fail("未处理的Direction:" + direction);
                            break;
                    }
                    break;
                case 3:
                    ///3个字段，则第一个表示顶，第二个表示右和左，第三个表示底
                    switch (direction)
                    {
                        case Direction.Top:
                            returnValue = values[0];
                            break;
                        case Direction.Right:
                        case Direction.Left:
                            returnValue = values[1];
                            break;
                        case Direction.Bottom:
                            returnValue = values[2];
                            break;
                        default:
                            Debug.Fail("未处理的Direction:" + direction);
                            break;
                    }
                    break;
                case 4:
                default:
                    ///4个字段，则第一个表示顶，第二个表示右，第三个表示底，第四个表示左
                    switch (direction)
                    {
                        case Direction.Top:
                            returnValue = values[0];
                            break;
                        case Direction.Right:
                            returnValue = values[1];
                            break;
                        case Direction.Bottom:
                            returnValue = values[2];
                            break;
                        case Direction.Left:
                            returnValue = values[3];
                            break;
                        default:
                            Debug.Fail("未处理的Direction:" + direction);
                            break;
                    }
                    break;
            }

            return returnValue;
        }

        static private Regex _regexListType = new Regex(@"(?<=\s|^)(none|disc|circle|square|decimal|decimal-leading-zero|lower-roman|upper-roman|lower-alpha|upper-alpha|lower-greek|lower-latin|upper-latin|hebrew|armenian|georgian|cjk-ideographic|hiragana|katakana|hiragana-iroha|katakana-iroha)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexListPosition = new Regex(@"(?<=\s|^)(inside|outside)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexListImage = new Regex(@"(?<=\s|^)(none|url\(.*\))(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

        static private Regex _regexBorderWidth = new Regex(@"(?<=\s|^)(thin|medium|thick|[.0-9]*(px|pt|in|cm|mm|pc|em|ex|%))(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexBorderStyle = new Regex(@"(?<=\s|^)(none|hidden|dotted|dashed|solid|double|groove|ridge|inset|outset)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexBorderColor = new Regex(@"(?<=\s|^)(\#?[\da-f]+)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

        static private Regex _regexBackgroundRepeat = new Regex(@"(?<=\s|^)(repeat\-x|repeat\-y|repeat|no\-repeat)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
        static private Regex _regexBackgroundAttachment = new Regex(@"(?<=\s|^)(scroll|fixed)(?=\s|$)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

        private string GetBackgroundDetail(string cssSrcName, string value)
        {
            ///分析name字段
            string type = cssSrcName.Substring(cssSrcName.LastIndexOf('-') + 1);   //(background-color)中的color

            switch (type)
            {
                case "color":
                    return _regexBorderColor.Match(value).Value;

                case "image":
                    return _regexListImage.Match(value).Value;

                case "repeat":
                    return _regexBackgroundRepeat.Match(value).Value;

                case "attachment":
                    return _regexBackgroundAttachment.Match(value).Value;

                case "position":
                    KeyValuePair<string, string> hv = CssUtility.ParseBackgroundPosition(value);
                    return hv.Key + " " + hv.Value;

                default:
                    Debug.Fail("未知的type:" + type);
                    break;
            }
            return null;
        }

        private string GetBackgroundShortcut(string name)
        {
            string propertyValue = this["background"];
            if (!string.IsNullOrEmpty(propertyValue))
            {
                return GetBackgroundDetail(name, propertyValue);
            }

            return null;
        }

        private string GetListDetail(string cssSrcName, string value)
        {
            ///分析name字段
            string type = cssSrcName.Substring(cssSrcName.LastIndexOf('-') + 1);   //(list-style-image)中的image

            switch (type)
            {
                case "type":
                    return _regexListType.Match(value).Value;

                case "position":
                    return _regexListPosition.Match(value).Value;

                case "image":
                    return _regexListImage.Match(value).Value;

                default:
                    Debug.Fail("未知的type:" + type);
                    break;
            }
            return null;
        }

        private string GetListShortcut(string name)
        {
            string propertyValue = this["list-style"];
            if (!string.IsNullOrEmpty(propertyValue))
            {
                return GetListDetail(name, propertyValue);
            }

            return null;
        }

        private string GetBorderDetail(string cssSrcName, string value)
        {
            string type = cssSrcName.Substring(cssSrcName.LastIndexOf('-') + 1);   //(border-top-color)中的color
            switch (type)
            {
                case "width":
                    return _regexBorderWidth.Match(value).Value;

                case "style":
                    return _regexBorderStyle.Match(value).Value;

                case "color":
                    return _regexBorderColor.Match(value).Value;

                default:
                    Debug.Fail("未知的type:" + type);
                    break;
            }
            return null;
        }

        static private Regex _regexBorderExpress = new Regex(@"border\-(?<direction>\w+)\-(?<type>\w+)",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">name便是cssName，如border-bottom-color</param>
        /// <returns></returns>
        private string GetBorderShortcut(string name)
        {
            ///分析name字段
            Match m = _regexBorderExpress.Match(name);
            string direction = m.Groups["direction"].Value;   //border-bottom-color中的bottom
            string type = m.Groups["type"].Value;   //border-bottom-color中的color

            ///先检查border-[type]
            string cssName = "border-" + type;
            string propertyValue = this[cssName];
            if (!string.IsNullOrEmpty(propertyValue))
            {
                return GetFourOne(propertyValue, (Direction)Enum.Parse(typeof(Direction), direction, true));
            }

            ///再检查border-[direction]和border
            cssName = "border-" + direction;
            propertyValue = this[cssName];
            if (string.IsNullOrEmpty(propertyValue))
            {
                cssName = "border";
                propertyValue = this[cssName];
            }

            if (!string.IsNullOrEmpty(propertyValue))
            {
                return GetBorderDetail(name, propertyValue);
            }

            return null;
        }

        /// <summary>
        /// 解析位置的简写属性。padding和margin。
        /// </summary>
        /// <param name="name">如padding-top</param>
        /// <returns></returns>
        private string GetDirectionShortcut(string name)
        {
            ///解析。如padding-top
            int index = name.IndexOf('-');
            string mainName = name.Substring(0, index);   //即(padding-top)中的padding
            string direction = name.Substring(index + 1);   //即(padding-top)中的top 

            string propertyValue = this[mainName];
            if (!string.IsNullOrEmpty(propertyValue))
            {
                GetFourOne(propertyValue, (Direction)Enum.Parse(typeof(Direction), propertyValue, true));
            }
            return null;
        }
    }

    /// <summary>
    /// Top，Right，Bottom，Left四中位置之一。
    /// </summary>
    public enum Direction
    {
        Top,
        Right,
        Bottom,
        Left,
    }
}