using System;
using System.IO;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;

namespace NKnife.AutoUpdater.Commands
{
    /// <summary>面向caller参数，该参数携带主调用程序的路径及全名。以备更新完成调用。
    /// </summary>
    internal class Caller : IUpdaterCommand
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
            get { return 0; }
        }

        /// <summary>执行操作
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            Logger.WriteLine("开始解析-caller参数。");
            if (Param == null || Param.Length <= 0)
            {
                Logger.WriteLine("未传递呼叫者本身的信息或版本信息。");
                return false;
            }
            string param = Param[0].Trim(new[] {'"'});

            var caller = new string[2];
            int n = param.LastIndexOf(':');
            caller[0] = param.Substring(0, n);
            caller[1] = param.Substring(n + 1);

            Logger.WriteLine(string.Format("{0} ** {1}", caller[0], caller[1]));

            if (string.IsNullOrWhiteSpace(caller[0]) || !File.Exists(caller[0]))
            {
                Logger.WriteLine("传递的呼叫者本身的信息有误。" + caller[0]);
                return false;
            }
            Currents.Me.CallExecuting = caller[0];
            Version version;
            if (Version.TryParse(caller[1], out version))
            {
                Logger.WriteLine("主程序版本(传递进来):" + version);
                Currents.Me.CallerVersion = version;
            }
            else
            {
                Logger.WriteLine("主程序版本异常，无法解析。");
                return false;
            }
            return true;
        }

        #endregion
    }
}