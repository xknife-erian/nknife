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
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        private const string LOG_TEXT1 =
            "NLog is a flexible and free logging platform for various .NET platforms, including .NET standard. NLog makes it easy to write to several targets. (database, file, console) and change the logging configuration on-the-fly. ";

        public LeftToolboxViewViewModel()
        {
            Application.ApplicationExit += (s, e) =>
            {
                _loopTimer?.Stop();
                StopLoopTask();
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
                s_logger.Trace($"{i}>{text} 1>>{GetLogText(30)}");
                s_logger.Debug($"{i}>{text} 2>>{GetLogText(30)}");
                s_logger.Info ($"{i}>{text} 3>>{GetLogText(30)}");
                s_logger.Warn ($"{i}>{text} 4>>{GetLogText(30)}");
                s_logger.Error($"{i}>{text} 5>>{GetLogText(30)}");
                s_logger.Fatal($"{i}>{text} 6>>{GetLogText(30)}");
            });
        }

        private static string GetLogText(ushort count)
        {
            return Guid.NewGuid().ToString("n").Substring(0, count);
        }

        public void BuildOneGroup()
        {
            s_logger.Trace($"1>>{GetLogText(30)}");
            s_logger.Debug($"2>>{GetLogText(30)}");
            s_logger.Info( $"3>>{GetLogText(30)}");
            s_logger.Warn( $"4>>{GetLogText(30)}");
            s_logger.Error($"5>>{GetLogText(30)}");
            s_logger.Fatal($"6>>{GetLogText(30)}");
        }

        public void BuildSingle()
        {
            s_logger.Info($">[文字日志:]>{LOG_TEXT1}");
        }

        public void BuildGroups(int count, string text = "")
        {
            BuildGroupLogs(count, text);
        }

        private Timer _loopTimer = new Timer();
        private bool _loop1 = true;
        private long _count = 0;
        private CancellationTokenSource _cancellationTokenSource = new();


        public async Task RunLoopTaskAsync()
        {
            _count                   = 0;
            _loop1                   = true;
            _cancellationTokenSource = new();
            var token                = _cancellationTokenSource.Token;
            await Task.Run( () =>
            {
                while (_loop1)
                {
                    token.ThrowIfCancellationRequested();
                    Task.Delay(1, token);
                    s_logger.Warn($"{_count}>[每1毫秒2.1条日志:]>{LOG_TEXT1}");
                    s_logger.Info($"{_count}>[每1毫秒2.2条日志:]>{LOG_TEXT1}");
                    _count++;
                }
            }, token).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    // Handle cancellation
                }
                else if (task.IsFaulted)
                {
                    // Handle exception
                }
            }, token);
        }

        public void StopLoopTask()
        {
            _loop1 = false;
            _cancellationTokenSource.Cancel();
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
