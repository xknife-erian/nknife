using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NKnife.App.UpdateAssemblyInfo.Properties;
using NKnife.Interface;

namespace NKnife.App.UpdateAssemblyInfo.Common
{
    /// <summary>
    /// 对VisualStudio的解决方案文件（*.sln）进行解析的解析器。
    /// </summary>
    public class SlnParser : IParser<FileInfo, string[]>
    {
        public string[] Parse(FileInfo source)
        {
            var lines = File.ReadAllLines(source.FullName);
            return (from line in lines let mv = GetMatchValue(line) where string.IsNullOrEmpty(mv) select GetMatchValue(line)).ToArray();
        }

        /// <summary>
        /// 通过对SLN文件解析得到所有的项目文件及其路径
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        protected static string GetMatchValue(string line)
        {
            line = line.Replace("  ", " ");
            if (Regex.IsMatch(line, OwnResources.GetProjectBySlnFile))
            {
                var match = Regex.Match(line, OwnResources.GetProjectBySlnResult);
                var matchValue = match.Value;
                matchValue = matchValue.TrimEnd('"');
                matchValue = matchValue.Replace(", \"", "");
                return matchValue;
            }
            return null;
        }
    }
}