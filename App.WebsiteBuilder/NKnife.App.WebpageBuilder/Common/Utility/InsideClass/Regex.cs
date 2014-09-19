using System.Text.RegularExpressions;
using System;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Regex
        {
            static public System.Text.RegularExpressions.Regex Number = new System.Text.RegularExpressions.Regex(@"^\d*$");
            static public System.Text.RegularExpressions.Regex AnyNumber = new System.Text.RegularExpressions.Regex(@"\d");
            static public System.Text.RegularExpressions.Regex Hanzi = new System.Text.RegularExpressions.Regex(@"[\u4e00-\u9fa5]+");

            static public bool IsHanzi(char ch)
            {
                if (ch >= '\u4e00' && ch < '\u9fa5')
                {
                    return true;
                }
                return false;
            }

            static public string GetFileName(string fileName)
            {
                //用正则表达式删除原有无用符号
                string strRegex = "[\\w\u4e00-\u9fa5\uf900-\ufa2d]+";
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
                Match m = re.Match(fileName);
                string trueFileName = m.Value;
                fileName = trueFileName;
                //+DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                return fileName;
            }
            static public System.Text.RegularExpressions.Regex HtmlUrl = new System.Text.RegularExpressions.Regex(
                @"src\=\""(file\:\/\/\/)?(?<path>[a-z\:][^""]*)\""",
                RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);
            static public System.Text.RegularExpressions.Regex HtmlMediaUrl = new System.Text.RegularExpressions.Regex(
                         @"value\=\""(file\:\/\/\/)?(?<path>[a-z\:][^""]*)\""",
                         RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);


            static public bool HasAnyNumber(string input)
            {
                return AnyNumber.IsMatch(input);
            }
            static public System.Text.RegularExpressions.Regex ActiveClsId = new System.Text.RegularExpressions.Regex(
                "classid=\"{id}\"", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);

            /// <summary>
            /// 为html里的从设计视图转到代码视图时将本地路径转换为资源文件id
            /// </summary>
            /// <param name="input"></param>
            /// <param name="path"></param>
            /// <param name="id"></param>
            /// <returns></returns>
            static public string ReplaceAbsFileFilter(string input,string path,string id)
            {
                int i = 0;
                string oldStr = "file:///" + path;
                string strResult = input;
                while ((i = input.IndexOf(oldStr,i,StringComparison.CurrentCultureIgnoreCase)) > -1)
                {
                    strResult = strResult.Remove(i, oldStr.Length);
                    strResult = strResult.Insert(i, "${srs_" + id + "}");
                    input = strResult;
                }
                return strResult;
            }

            /// <summary>
            /// 将资源文件格式化
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            static public string FormatResourceId(string id)
            {
                return "${srs_" + id + "}";
            }
        }
    }
}