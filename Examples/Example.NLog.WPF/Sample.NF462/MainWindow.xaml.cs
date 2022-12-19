using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NKnife.NLog.WPF;
using NLog;

namespace Sample
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancelLogTask;
        private Task _logTask;

        public MainWindow()
        {
            InitializeComponent();
            _CbAutoScroll_.IsChecked = true;

            _LogCtrl_.ItemAdded += OnLogMessageItemAdded;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var log = LogManager.GetLogger("button");

            var level = LogLevel.Trace;
            if (sender.Equals(btnDebug)) level = LogLevel.Debug;
            if (sender.Equals(btnWarning)) level = LogLevel.Warn;
            if (sender.Equals(btnError)) level = LogLevel.Error;

            log.Log(level, tbLogText.Text);
        }

        private void OnLogMessageItemAdded(object o, EventArgs args)
        {
            // Do what you want :)
            LogEventInfo logInfo = (NLogEvent)args;
            if (logInfo.Level >= LogLevel.Error)
                SystemSounds.Beep.Play();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _LogCtrl_.Clear();
        }

        private void TopScroll_Click(object sender, RoutedEventArgs e)
        {
            _LogCtrl_.ScrollToFirst();
        }

        private void BottomScroll_Click(object sender, RoutedEventArgs e)
        {
            _LogCtrl_.ScrollToLast();
        }

        private void AutoScroll_Checked(object sender, RoutedEventArgs e)
        {
            _LogCtrl_.AutoScrollToLast = true;
        }

        private void AutoScroll_Unchecked(object sender, RoutedEventArgs e)
        {
            _LogCtrl_.AutoScrollToLast = false;
        }

        private void BackgroundSending_Checked(object sender, RoutedEventArgs e)
        {
            _cancelLogTask = new CancellationTokenSource();
            var token = _cancelLogTask.Token;
            _logTask = new Task(SendLogs, token, token);
            _logTask.Start();
        }

        private void BackgroundSending_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_cancelLogTask != null)
                _cancelLogTask.Cancel();
        }

        private void SendLogs(object obj)
        {
            var ct = (CancellationToken)obj;

            var counter = 0;
            var log = LogManager.GetLogger("task");

            log.Debug("Backgroundtask started.");

            while (!ct.WaitHandle.WaitOne(600)) 
                log.Trace($"Messageno {counter++} from backgroudtask.");

            log.Debug("Backgroundtask stopped.");
        }
    }
}