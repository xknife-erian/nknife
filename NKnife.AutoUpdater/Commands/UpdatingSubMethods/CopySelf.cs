using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using NKnife.AutoUpdater.Common;

namespace NKnife.AutoUpdater.Commands.UpdatingSubMethods
{
    /// <summary>通知新的更新器启动，但仅执行Copy自身的动作
    /// </summary>
    class CopySelf
    {
        /// <summary>通知新的更新器启动，但仅执行Copy自身的动作。
        /// </summary>
        /// <param name="self"></param>
        public void Run(FileSystemInfo self)
        {
            try
            {
                var args = new StringBuilder();
                args.Append(String.Format("-caller:\"{0}\"", Currents.Me.CallExecuting));
                args.Append(":").Append(Currents.Me.CallerVersion.ToString());
                args.Append(' ');
                string target = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, self.Name);
                args.Append(String.Format("-copy:\"{0}\"^\"{1}\"", self.FullName, target));
                Logger.WriteLine("通知新的更新器启动，但仅执行Copy自身的动作。启动新的应用程序:" + args);
                //启动临时目录下新的自动更新程序
                Process.Start(self.FullName, args.ToString());
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Logger.WriteLine(String.Format("无法启动新的升级程序.{0}", e));
            }
        }
    }
}
