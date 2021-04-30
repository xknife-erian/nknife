using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace NKnife.Upgrade4Github.Util
{
    public class CommandLineArgsHelper
    {
        private static readonly Regex _SplitterRegex = new Regex("^-{1,2}|^\\/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex _CleanerRegex = new Regex("^['\"]?(.*?)['\"]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public IReadOnlyDictionary<string, string> Parameters { get; }

        public CommandLineArgsHelper()
          : this(Environment.GetCommandLineArgs())
        {
        }

        public CommandLineArgsHelper(string[] argsArray)
        {
            this.Parameters = (IReadOnlyDictionary<string, string>)new ReadOnlyDictionary<string, string>((IDictionary<string, string>)this.ParseArgumentsHelper(argsArray));
        }

        private Dictionary<string, string> ParseArgumentsHelper(string[] argsArray)
        {
            Dictionary<string, string> paramDictionary = new Dictionary<string, string>();
            if (argsArray == null || argsArray.Length == 0)
                return paramDictionary;
            string parameter = (string)null;
            foreach (string args in argsArray)
            {
                string[] strArray = _SplitterRegex.Split(args, 3);
                switch (strArray.Length)
                {
                    case 1:
                        TryInitParamInDictionary(paramDictionary, parameter, strArray[0]);
                        parameter = null;
                        break;
                    case 2:
                        this.TryInitParamInDictionary(paramDictionary, parameter, "true");
                        parameter = strArray[1];
                        break;
                    case 3:
                        TryInitParamInDictionary(paramDictionary, parameter,
                            !TryInitParamInDictionary(paramDictionary, strArray[1], strArray[2]) ? args : "true");
                        parameter = null;
                        break;
                }
            }
            this.TryInitParamInDictionary(paramDictionary, parameter, "true");
            return paramDictionary;
        }

        private bool TryInitParamInDictionary(
          Dictionary<string, string> paramDictionary,
          string parameter,
          string value = "true")
        {
            if (string.IsNullOrEmpty(parameter) || paramDictionary.ContainsKey(parameter))
                return false;
            string str = _CleanerRegex.Replace(value, "$1");
            paramDictionary.Add(parameter, str);
            return true;
        }

        public string this[string name] => Parameters.TryGetValue(name, out string result) ? result : string.Empty;
    }

}
