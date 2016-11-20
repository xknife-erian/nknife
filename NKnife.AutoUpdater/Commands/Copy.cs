using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;
using NKnife.Utility;

namespace NKnife.AutoUpdater.Commands
{
    internal class Copy : IUpdaterCommand
    {
        #region IUpdaterCommand Members

        /// <summary>命令行参数名
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>命令行参数的参数
        /// </summary>
        /// <value>
        /// The param.
        /// </value>
        public string[] Param { get; set; }

        /// <summary>执行顺序
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public short Order
        {
            get { return 4; }
        }

        /// <summary>执行操作
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            Logger.WriteLine("开始执行Copy自动升级器操作");
            if (Param == null)
                Logger.WriteLine("Copy自动升级器时指令无参数");
            if (Param == null)
            {
                return false;
            }
            string src = Param[0].Trim(new[] {'"'});

            if (!File.Exists(src))
            {
                Logger.WriteLine(string.Format("更新程序执行自COPY时，源文件{0}不存在。", src));
            }
            string target = Param[1].Trim(new[] {'"'});
            string targetDir = new FileInfo(target).DirectoryName;
            if (targetDir != null && !Directory.Exists(targetDir))
            {
                Logger.WriteLine(string.Format("更新程序执行自COPY时，目标目录{0}不存在。将被创建。", targetDir));
                UtilityFile.CreateDirectory(targetDir);
            }
            Logger.WriteLine(src);
            Logger.WriteLine(target);
            Logger.WriteLine("准备COPY新的更新程序");
            try
            {
                Thread.Sleep(3000);
                File.Copy(src, target, true);
            }
            catch (Exception e)
            {
                Logger.WriteLine("文件COPY时异常。{0}", e);
                Thread.Sleep(500);
                return false;
            }
            Logger.WriteLine("新的更新程序COPY完成");
            var oldfile = new FileInfo(src);

            var arguments = new StringBuilder();
            arguments.Append(String.Format("-caller:\"{0}\"", Currents.Me.CallExecuting));
            arguments.Append(":").Append(Currents.Me.CallerVersion.ToString());
            arguments.Append(' ');
            arguments.Append(String.Format("-updating:{0}", Guid.NewGuid())).Append(' ');
            arguments.Append(String.Format("-deltemp:\"{0}\"", oldfile.DirectoryName));
            //启动新的更新程序
            Logger.WriteLine("程序:" + target);
            Logger.WriteLine("参数:" + arguments);
            Logger.WriteLine("启动新的更新程序");
            Thread.Sleep(200);
            Process.Start(target, arguments.ToString());
            Environment.Exit(0);
            return true;
        }

        #endregion
    }
}