using System;
using System.IO;
using System.Text;
using System.Threading;
using NKnife.AutoUpdater.Common;
using NKnife.Utility;

namespace NKnife.AutoUpdater
{
    internal static class Program
    {
        #region 模拟参数(测试使用)

        /// <summary>正常调用参数
        /// </summary>
        private static string[] StarterArgs
        {
            get
            {
                var args = new[]
                               {
                                   "-caller:\"D:\\__Updater Test__\\UDemoMainCaller.exe\"",
                                   "-updating:83760355-d2dd-4110-8184-91bc6d7fe0d4",
                               };
                return args;
            }
        }

        /// <summary>当更新完自身时，启动Copy自身到原始目录中去
        /// </summary>
        private static string[] CopyArgs
        {
            get
            {
                var args = new[]
                               {
                                   @"-caller:D:\__Updater Test__\UpdaterCaller.exe:3.13.13.13232",
                                   @"-copy:D:\__Updater Test__\1\Updater.exe^D:\__Updater Test__\2\Pansoft.Updater.exe"
                               };
                return args;
            }
        }

        /// <summary>更新完自身并Copy结束后，再次启动更新器
        /// </summary>
        private static string[] UpdateAndDeleteTempArgs
        {
            get
            {
                var args = new[]
                               {
                                   @"-caller:D:\__Updater Test__\UpdaterCaller.exe",
                                   @"-updating:96eaade3-9df6-4600-b1f1-8547fe639dd0",
                                   @"-deltemp:D:\__Updater Test__\1",
                               };
                return args;
            }
        }

        #endregion

        private static readonly AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);

        [STAThread]
        private static void Main(params string[] args)
        {
#if DEBUG
            if (args == null || args.Length <= 0)
                //测试使用
                args = CopyArgs;
            Currents.Me.UserApplicationDataPath = @"D:\__Updater Test__\_UserApplicationDataPath\";
#endif
            if (File.Exists("PansoftUpdater.exe"))
            {
                File.Delete("PansoftUpdater.exe");
                Logger.WriteLine("删除历史生成的Updater更新器程序");
            }

            if (!UtilityCollection.IsNullOrEmpty(args)) //如果有参数传入，将进入自动更新状态
            {
                Currents.Me.Args = args;

                //将传入的参数简单的还原一下，打印到日志备查
                var sb = new StringBuilder("启动参数:");
                foreach (string s in args)
                    sb.Append(s).Append(' ');
                Logger.WriteLine(sb.ToString());

                var appThread = new AppThread();
                appThread.UpdaterThreadProcessedEvent += UpdaterThreadProcessedEvent;

                var updaterWorkThread = new Thread(appThread.UpdaterThreadProcess) {IsBackground = true};
                updaterWorkThread.Start();

                _AutoResetEvent.WaitOne();
            }
            Console.ReadKey();
        }

        private static void UpdaterThreadProcessedEvent(object sender, EventArgs e)
        {
            Logger.IsRun = false;
            _AutoResetEvent.Set();
        }
    }
}