using System;
using System.IO;
using System.Threading;
using NKnife.Base;
using NKnife.Collections;

namespace NKnife.AutoUpdater.Common
{
    internal static class Logger
    {
        private static readonly string _BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\Logs");
        private static readonly SyncQueue<Pair<bool, string>> _Infos = new SyncQueue<Pair<bool, string>>();
        private static readonly LogFile _LogFile = new LogFile(_BaseDirectory, "Pansoft.Updater");

        static Logger()
        {
            IsRun = true;
            var thread = new Thread(Write) {IsBackground = true};
            thread.Start();
        }

        public static bool IsRun { get; set; }

        public static void WriteLine(string info, bool isCoreInfo = false)
        {
            WriteLine(info, null, isCoreInfo);
        }

        public static void WriteLine(string info, Exception e, bool isCoreInfo = false)
        {
            string formateInfo = FormatLogInfo(info, e);
            Console.WriteLine(formateInfo);
            _Infos.Enqueue(new Pair<bool, string> {First = isCoreInfo, Second = formateInfo});
        }

        private static string FormatLogInfo(string info, Exception e = null)
        {
            string line;
            if (e != null)
                line = string.Format("{0}  #  {1}, Exception:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), info, e.Message);
            else
                line = string.Format("{0}  #  {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), info);
            return line;
        }

        private static void Write()
        {
            _LogFile.Write("===========================================");
            while (IsRun)
            {
                if (_Infos.Count > 0)
                {
                    Pair<bool, string> info = _Infos.Dequeue();
                    _LogFile.Write(info.Second, info.First);
                }
                _Infos.AutoResetEvent.WaitOne();
            }
        }
    }
}