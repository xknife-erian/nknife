using System;
using System.Threading;
using System.Windows.Forms;

namespace Jeelu.SimplusSoftwareUpdate
{
    public class MainClass
    {
        /// <summary>
        /// SimplusD.exe可执行文件名
        /// </summary>
        static public readonly string SimplusDExe = "SimplusD!.exe";
        /// <summary>
        /// 软件名称
        /// </summary>
        static public readonly string MainSoftwareName = "SimplusD!";

        /// <summary>
        /// 版本文件的地址
        /// </summary>
        static public readonly string VersionFileAddress = "http://update.jeelu.com:8080/FilesVersion.xml";
        /// <summary>
        /// update文件本身的更新信息地址
        /// </summary>
        static public readonly string UpdateExeVersion = "http://update.jeelu.com:8080/UpdateExeVersion.xml";
        /// <summary>
        /// 临时文件夹
        /// </summary>
        static public readonly string TempFolder = "UpdateTemp";

        [STAThread]
        static void Main(string[] args)
        {
            //打开软件更新的互斥体
            bool createdNew;
            Mutex mutex = new Mutex(true, "SimplusD_Update", out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("已经有更新程序在运行！", MainSoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                LogService.Initialize();
                UpdateClass update = new UpdateClass();
                update.Run(args);
            }
            finally
            {
                LogService.Close();
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}
