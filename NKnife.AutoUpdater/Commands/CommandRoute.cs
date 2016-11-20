using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;

namespace NKnife.AutoUpdater.Commands
{
    /// <summary>升级程序命令路由器
    /// </summary>
    internal class CommandRoute
    {
        private static readonly AutoResetEvent _Reset = new AutoResetEvent(false);

        static CommandRoute()
        {
            WaitTimeout = 6000;
        }

        public static int WaitTimeout { get; set; }

        /// <summary>按已排序的List执行命令的路由
        /// </summary>
        /// <param name="items">The items.</param>
        public static void Route(List<IUpdaterCommand> items)
        {
            if (items == null || items.Count <= 0)
            {
                Logger.WriteLine("无参数，更新程序不执行。返回。");
                return;
            }
            try
            {
                foreach (IUpdaterCommand workItem in items)
                {
                    if (!workItem.Run())
                    {
                        Logger.WriteLine(workItem.GetType().Name + "执行失败。");
                        break;
                    }
                    Logger.WriteLine(workItem.GetType().Name + "执行完成。");
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine("命令序列路由异常。{0}", e);
            }
            CallMainExecuting();
        }

        private static void CallMainExecuting()
        {
            try
            {
                Logger.WriteLine("启动呼叫更新程序的父级主程序.等待:" + WaitTimeout);
                Logger.WriteLine("程序:" + Currents.Me.CallExecuting);
                if (string.IsNullOrWhiteSpace(Currents.Me.CallExecuting))
                    return;

                var arguments = new StringBuilder();
                arguments.Append("-alreadyupdate");

                var thread = new Thread(Run) {IsBackground = true};
                thread.Start(arguments);
                _Reset.WaitOne();
            }
            catch (Exception e)
            {
                Logger.WriteLine("呼叫主程序异常。{0}", e);
            }
        }

        private static void Run(object arguments)
        {
            Thread.Sleep(WaitTimeout);
            Process.Start(Currents.Me.CallExecuting, arguments.ToString());
            _Reset.Set();
        }
    }
}