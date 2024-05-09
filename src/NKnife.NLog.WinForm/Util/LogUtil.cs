using System;

namespace NKnife.NLog.WinForm.Util
{
    static class LogUtil
    {
        public static string ParseLoggerName(string nameSource)
        {
            try
            {
                if (nameSource.Contains("[["))
                {
                    var ns = nameSource.Substring(0, nameSource.LastIndexOf("`1", StringComparison.Ordinal));
                    var className = ns.Substring(ns.LastIndexOf('.') + 1);

                    var fxIndex = nameSource.LastIndexOf("[[", StringComparison.Ordinal) + 2;
                    var fx = nameSource.Substring(fxIndex, nameSource.IndexOf(',', fxIndex) - fxIndex);
                    return $"{className}<{fx.Substring(fx.LastIndexOf('.') + 1)}>";
                }
                return nameSource.Substring(nameSource.LastIndexOf('.') + 1);
            }
            catch (Exception)
            {
                return nameSource;
            }
        }
    }
}
