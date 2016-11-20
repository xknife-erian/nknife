using System;
using System.IO;
using System.Threading;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;
using NKnife.Utility;

namespace NKnife.AutoUpdater.Commands
{
    internal class DelTemp : IUpdaterCommand
    {
        #region IUpdaterWorkItem Members

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
            get { return 2; }
        }

        /// <summary>执行操作
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            Logger.WriteLine("DEL_TEMP");
            if (!UtilityCollection.IsNullOrEmpty(Param))
            {
                string dir = Param[0];
                if (Directory.Exists(dir))
                    try
                    {
                        Thread.Sleep(1000);
                        UtilityFile.DeleteDirectory(dir);
                        Logger.WriteLine(string.Format("目录删除成功。{0}", dir));
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(string.Format("删除临时目录异常。{0}", dir), e);
                    }
                else
                    Logger.WriteLine(string.Format("临时目录不存在。{0}", dir));
            }
            else
            {
                Logger.WriteLine("虽然有deltemp关键字，但是未提供临时目录的目录名。");
            }
            return true;
        }

        #endregion
    }
}