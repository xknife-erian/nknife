using System;

namespace NKnife.NLog.WinForm.Util
{
    [Flags]
    internal enum Level : byte
    {
        None = 0,
        Trace = 1,
        Debug = 2,
        Info = 4,
        Warn = 8,
        Error = 16,
        Fatal = 32
    }
}