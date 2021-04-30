using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using Timer = System.Windows.Forms.Timer;

namespace NKnife.NLog.WinForm.Example
{
    class LeftToolboxViewViewModel
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        private const string LogText1 =
            "NLog is a flexible and free logging platform for various .NET platforms, including .NET standard. NLog makes it easy to write to several targets. (database, file, console) and change the logging configuration on-the-fly. ";

        public LeftToolboxViewViewModel()
        {
            Application.ApplicationExit += (s, e) =>
            {
                _loopTimer?.Stop();
                _loop1 = false;
            };
        }

        /// <summary>
        /// 生成指定数量组所有Level日志
        /// </summary>
        /// <param name="count">指定数量</param>
        /// <param name="text">标记文本</param>
        private void BuildGroupLogs(int count, string text = "")
        {
            Parallel.For(0, count, (i) =>
            {
                _Logger.Trace($"{i}>{text} 1>>{GetLogText(30)}");
                _Logger.Debug($"{i}>{text} 2>>{GetLogText(30)}");
                _Logger.Info ($"{i}>{text} 3>>{GetLogText(30)}");
                _Logger.Warn ($"{i}>{text} 4>>{GetLogText(30)}");
                _Logger.Error($"{i}>{text} 5>>{GetLogText(30)}");
                _Logger.Fatal($"{i}>{text} 6>>{GetLogText(30)}");
            });
        }

        private static string GetLogText(ushort count)
        {
            return Guid.NewGuid().ToString("n").Substring(0, count);
        }

        public void BuildOneGroup()
        {
            _Logger.Trace($"1>>{GetLogText(30)}");
            _Logger.Debug($"2>>{GetLogText(30)}");
            _Logger.Info( $"3>>{GetLogText(30)}");
            _Logger.Warn( $"4>>{GetLogText(30)}");
            _Logger.Error($"5>>{GetLogText(30)}");
            _Logger.Fatal($"6>>{GetLogText(30)}");
        }

        public void BuildSingle()
        {
            _Logger.Info($">[文字日志:]>{LogText1}");
        }

        public void BuildGroups(int count, string text = "")
        {
            BuildGroupLogs(count, text);
        }

        private Timer _loopTimer = new Timer();
        private bool _loop1 = true;
        private long _count = 0;
        private Thread _thread;

        public void BuildLoop1Millisecond1Log()
        {
            _count = 0;
            _loop1 = true;
            _thread = new Thread(() =>
            {
                while (_loop1)
                {
                    _Logger.Warn($"{_count}>[每1毫秒2.1条日志:]>{LogText1}");
                    _Logger.Info($"{_count}>[每1毫秒2.2条日志:]>{LogText1}");
                    _count++;
                    Thread.Sleep(1);
                }
            }) {IsBackground = true};
            _thread.Start();
        }

        public void StopLoop1Millisecond1Log()
        {
            _loop1 = false;
            _thread.Abort();
        }

        public void BuildLoop1Millisecond20GroupLog()
        {
            _count = 0;
            _loopTimer = new Timer();
            _loopTimer.Tick += (s, e) =>
            {
                BuildGroups(10, $"{_count}每1毫秒生成10组日志");
                _count++;
            };
            _loopTimer.Interval = 1;
            _loopTimer.Start();
        }

        public void StopLoop1Millisecond20GroupLog()
        {
            _loopTimer.Stop();
        }

    }
}
