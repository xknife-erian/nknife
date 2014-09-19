using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Pinyin
        {
            static private Dictionary<char, string> _dicPinyin = new Dictionary<char, string>();

            /// <summary>
            /// 传入拼音库文件的路径，初始化当前类
            /// </summary>
            /// <param name="pinyinFile">拼音库文件。.txt</param>
            static public void Initialize(string pinyinFile)
            {
                string[] lines = System.IO.File.ReadAllLines(pinyinFile, Encoding.GetEncoding("gb2312"));

                foreach (string line in lines)
                {
                    if (line.Trim() == "")
                    {
                        continue;
                    }

                    ///取汉字
                    char hanzi = line[0];

                    ///取拼音
                    string pinyin = line.Substring(1);
                    int spaceIndex = pinyin.IndexOf(' ');
                    if (spaceIndex != -1)   //若拼音中有空格，则取前面部分(多音字会出现这种情况)
                    {
                        pinyin = pinyin.Substring(0, spaceIndex);
                    }

                    ///添加到Dictionary中
                    if (!_dicPinyin.ContainsKey(hanzi))
                    {
                        _dicPinyin.Add(hanzi, pinyin);
                    }
                }
            }

            /// <summary>
            /// 将一段中文文字转换成拼音，未在库中找到的则原样输出。
            /// </summary>
            /// <param name="hanzi"></param>
            /// <returns></returns>
            static public string ToPinyin(string hanzi)
            {
                Debug.Assert(_dicPinyin.Count > 0, "拼音转换模块未初始化！");

                StringBuilder sb = new StringBuilder();
                foreach (char ch in hanzi)
                {
                    string pinyin = ToPinyinSingle(ch);

                    if (!string.IsNullOrEmpty(pinyin))
                    {   
                        sb.Append(pinyin);
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }
                return sb.ToString();
            }

            /// <summary>
            /// 转换一个中文。若未在库中找到则返回null。
            /// </summary>
            /// <param name="hanziSingle"></param>
            /// <returns></returns>
            static public string ToPinyinSingle(char hanziSingle)
            {
                Debug.Assert(_dicPinyin.Count > 0, "拼音转换模块未初始化！");

                string pinyin;
                if (_dicPinyin.TryGetValue(hanziSingle, out pinyin))
                {
                    //首字母大写
                    //http://support.microsoft.com/kb/312890/zh-cn
                    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    pinyin = textInfo.ToTitleCase(pinyin);

                    return pinyin;
                }

                return null;
            }

        }
    }
}