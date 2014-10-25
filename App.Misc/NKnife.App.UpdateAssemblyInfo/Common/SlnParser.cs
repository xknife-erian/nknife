using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NKnife.App.UpdateAssemblyInfo.Properties;
using NKnife.Interface;

namespace NKnife.App.UpdateAssemblyInfo.Common
{
    public class SlnParser : IParser<FileInfo, string[]>
    {
        public string[] Parse(FileInfo source)
        {
            var lines = File.ReadAllLines(source.FullName);
            return (from line in lines let mv = GetMatchValue(line) where string.IsNullOrEmpty(mv) select GetMatchValue(line)).ToArray();
        }

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