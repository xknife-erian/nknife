using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class Util
        {
            public const string TimeFormat = "yyyy-MM-dd HH:mm:ss";
            public const string ChannelRootId = "00000000";

            static private bool _inited;

            static public void Initialize()
            {
                _inited = true;
            }

            static public bool DesignMode
            {
                get { return !_inited; }
            }

            static private Image _imgSignExcalmatory;
            static private Image _imgSignQuestion;
            /// <summary>
            /// 给图片打标记。目前根据SignType的不同可以打两种：感叹号、问号
            /// </summary>
            static public void DrawSign(Image image, SignType type)
            {
                Image signImg = null;

                ///获取打标记的标记图片
                switch (type)
                {
                    case SignType.ExcalmatoryPoint:
                        {
                            if (_imgSignExcalmatory == null)
                            {
                                string strSignImg = Path.Combine(PathService.SoftwarePath, @"Image\Expand\ExcalmatoryPoint.png");
                                _imgSignExcalmatory = Image.FromFile(strSignImg);
                                //_imgSignExcalmatory = ResourceService.GetResourceImage("sd.img.ExcalmatoryPoint"); 
                            }
                            signImg = _imgSignExcalmatory;
                            break;
                        }
                    case SignType.QuestionPoint:
                        {
                            if (_imgSignQuestion == null)
                            {
                                string strSignImg = Path.Combine(PathService.SoftwarePath, @"Image\Expand\QuestionPoint.png");
                                _imgSignQuestion = Image.FromFile(strSignImg);
                                //_imgSignQuestion = ResourceService.GetResourceImage("sd.img.QuestionPoint");
                            }
                            signImg = _imgSignQuestion;
                            break;
                        }
                    default:
                        Debug.Assert(false);
                        break;
                }

                ///将标记图片Draw上去
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(signImg, 11, 4);
                g.Flush();
                g.Dispose();
            }
            static public string GetSignKey(string srcKey, SignType type)
            {
                return srcKey + "&" + type;
            }
            static public bool StringIsAllNumber(string str)
            {
                foreach (char ch in str)
                {
                    if (!char.IsNumber(ch))
                    {
                        return false;
                    }
                }
                return true;
            }

            static public string GetUnitByInt(int index)
            {
                switch (index)
                {
                    case 0:
                        return "px";
                    case 1:
                        return "in";
                    case 2:
                        return "pt";
                    case 3:
                        return "cm";
                    case 4:
                        return "mm";
                    case 5:
                        return "pc";
                    case 6:
                        return "em";
                    case 7:
                        return "ex";
                    case 8:
                        return "%";
                    default:
                        return "px";
                }
            }
            static public KeyValuePair<string, int> GetSizeAndUnit(string size)
            {
                string _key = "";
                int _value = 0;
                if (string.IsNullOrEmpty(size))
                {
                    return new KeyValuePair<string, int>(_key, _value);
                }
                if (size.Length == 1 && int.TryParse(size, out _value))
                {
                    return new KeyValuePair<string, int>(_value.ToString(), 0);
                }
                if (size[size.Length - 1] == '%')
                {
                    _key = size.Remove(size.Length - 1);
                    _value = 1;
                }
                else
                {
                    string _unit = size.Substring(size.Length - 2, 2);
                    switch (_unit)
                    {
                        case "px":
                            _key = size.Remove(size.Length - 2);
                            _value = 0;
                            break;
                        case "pt":
                            _key = size.Remove(size.Length - 2);
                            _value = 1;
                            break;
                        case "in":
                            _key = size.Remove(size.Length - 2);
                            _value = 2;
                            break;
                        case "cm":
                            _key = size.Remove(size.Length - 2);
                            _value = 3;
                            break;
                        case "mm":
                            _key = size.Remove(size.Length - 2);
                            _value = 4;
                            break;
                        case "pc":
                            _key = size.Remove(size.Length - 2);
                            _value = 5;
                            break;
                        case "em":
                            _key = size.Remove(size.Length - 2);
                            _value = 6;
                            break;
                        case "ex":
                            _key = size.Remove(size.Length - 2);
                            _value = 7;
                            break;
                        default:
                            _key = size;
                            break;
                    }
                }
                return new KeyValuePair<string, int>(_key, _value);
            }
        }
    }
}