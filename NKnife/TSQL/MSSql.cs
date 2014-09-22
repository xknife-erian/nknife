using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NKnife.TSQL
{
    public class MSSql
    {
        public static IEnumerable<string> GetCommands(string script)
        {
            Regex regex = new Regex(@"\r{0,1}\nGO\r{0,1}\n");
            string[] commands = regex.Split(script);
            return commands.Where(s => s.Trim().Length > 0);
        }
    }
}
