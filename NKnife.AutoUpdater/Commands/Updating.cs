using System;
using System.IO;
using System.Xml;
using NKnife.AutoUpdater.Commands.UpdatingSubMethods;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.FileVerify;
using NKnife.AutoUpdater.Interfaces;

namespace NKnife.AutoUpdater.Commands
{
    internal class Updating : IUpdaterCommand
    {
        private static IUpdaterFileVerify FileVerify
        {
            get { return FileVerifyFactory.Instance.GetFileVerify(); }
        }

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
            get { return 3; }
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            Logger.WriteLine("开始执行Update指令...");
            //维护一个全局的临时目录，负责存放下载的文件，参数是Program传递进来的。
            Currents.Me.TempDirectory = Param[0];

            XmlDocument indexXml;
            FileInfo indexfile;
            if (!DownloadIndexXmlFile.Run(out indexXml, out indexfile)) 
                return false;

            //处理更新程序“自已”
            bool selfUpdateCompleted = new UpdateSelf().Run(FileVerify, indexXml);
            if (!selfUpdateCompleted) //当处理更新程序“自已”失败后，直接返回
                return false;

            //处理主程序的更新
            bool mainAppUpdateCompleted = new UpdateMainApp().Run(FileVerify, indexXml);
            if (!mainAppUpdateCompleted)
                Logger.WriteLine(String.Format("主程序的更新不成功。"));

            //处理Patch节点
            bool patchCompleted = new Patchs().Run(FileVerify, indexXml);
            if (!patchCompleted)
                Logger.WriteLine(String.Format("处理Patch节点不成功。"));

            //删除索引文件
            try
            {
                indexfile.Delete();
                Logger.WriteLine(String.Format("删除索引文件成功。"));
            }
            catch (Exception e)
            {
                Logger.WriteLine("删除索引文件异常.{0}", e);
            }
            return true;
        }


        #endregion
    }
}