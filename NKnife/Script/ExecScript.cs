using System;
using System.IO;
using MSScriptControl;

namespace NKnife.Script
{
    public class ExecScript
    {
        private static ExecScript _Instance;
        private readonly ScriptControlClass _Script = new ScriptControlClass();

        private ExecScript()
        {
            _Script.Language = "javascript";
        }

        public static ExecScript Instance()
        {
            if (_Instance == null)
            {
                _Instance = new ExecScript();
            }
            return _Instance;
        }

        public string ExecJsScript(string scriptfile, string methodName, object[] args)
        {
            string result;
            _Script.Reset();
            //string path = Path.Combine(AppPath.ScriptPath, scriptfile);
            string code = "";
            using (var fs = new FileStream(scriptfile, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (var sr = new StreamReader(fs))
                {
                    code = sr.ReadToEnd();
                    _Script.AddCode(code);
                    result = Convert.ToString(_Script.Run(methodName, args));
                }
            }
            return result;
        }
    }
}