using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win.Startup
{
    public class SimplusDMain
    {
        [STAThread()]
        public static void Main(string[] args)
        {
            try
            {
                //检查软件更新是否在进行
                Mutex mutex = new Mutex(false, "SimplusD_Update");
                bool isHasMutex = mutex.WaitOne(100, false);
                if (!isHasMutex)
                {
                    mutex.Close();
                    MessageBox.Show("正在执行软件更新，请在软件更新完成后再启动程序。", "SimplusD!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    mutex.ReleaseMutex();
                    mutex.Close();
                }

                ////检查本地是否已经有下载完毕的更新，若有，则提示安装
                //XmlDocument docUpdateInfo = new XmlDocument();
                //string strUpdateInfoFile = Path.Combine(Application.StartupPath, "UpdateInfo.xml");
                //docUpdateInfo.Load(strUpdateInfoFile);
                //XmlElement eleUpdate = docUpdateInfo.DocumentElement["update"];
                //string updateState = eleUpdate.GetAttribute("state");

                ////已经下载成功，则提示安装
                //if (updateState.Equals("Downloaded", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    if (MessageBox.Show("已经有新版本且已下载完毕。\r\n\r\n是否安装？", "SimplusD!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                //        == DialogResult.OK)
                //    {
                //        string setupFile = eleUpdate.GetAttribute("setupFile");
                //        Process pSetupFile = new Process();
                //        pSetupFile.StartInfo.FileName = setupFile;
                //        pSetupFile.StartInfo.UseShellExecute = true;
                //        pSetupFile.Start();
                //        pSetupFile.Dispose();
                //    }
                //    return;
                //}

                #region 解析命令参数

                bool noUpdate = false;
                string willOpenFile = null;
                if (args != null && args.Length != 0)
                {
                    string prevArgs = "";   //上一个命令
                    foreach (string item in args)
                    {
                        ///-n表示不需要进行软件更新
                        if (item.Equals("-n", StringComparison.CurrentCultureIgnoreCase))
                        {
                            noUpdate = true;
                        }
                        else
                        {
                            //-s后跟.sdsite文件路径
                            if (prevArgs.Equals("-s", StringComparison.CurrentCultureIgnoreCase))
                            {
                                willOpenFile = item;
                            }
                        }

                        ///记录上一个命令
                        prevArgs = item;
                    }
                }

                #endregion

#if DEBUG
#else
                ///调用更新程序
                if (!noUpdate)
                {
                    string strUpdateFile = Path.Combine(Application.StartupPath, "Update.exe");
                    if (File.Exists(strUpdateFile))
                    {
                        List<string> list = new List<string>();
                        list.Add("-s");   //-s表示不是手动点击(update.exe)
                        if (!string.IsNullOrEmpty(willOpenFile))
                        {
                            list.Add("-r \"" + willOpenFile + "\"");   //-r表示记忆sdsite文件路径(update.exe)
                        }

                        Process.Start(strUpdateFile, string.Join(" ", list.ToArray()));
                        return;
                    }
                    else
                    {
                        Debug.Fail("没有找到更新程序:" + Path.GetFileName(strUpdateFile));
                    }
                }
#endif
                //添加信号量，以告诉Update有实例在运行
                Semaphore sema = new Semaphore(100, 100, "SimplusD_MainForm");
                bool hasSema = sema.WaitOne(1000, false);
                if (!hasSema)
                {
                    sema.Close();
                    MessageBox.Show("进程打开过多！", "SimplusD提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    try
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        ///运行MainForm
                        SplashScreenForm.ShowSplashScreen();
#if DEBUG
                        ///仅为了观看欢迎画面
                        //Thread.Sleep(3 * 1000);
#endif
                        WorkbenchForm.Initialized += delegate
                        {
                            SplashScreenForm.SplashScreen.Close();
                        };
                        //初始化WorkbenchForm窗体,保存传入参数,这可能是双击某文件,产生的关联启动了本程序,那么参数就是文件路径
                        WorkbenchForm.Initialize(willOpenFile);
                        Application.Run(WorkbenchForm.MainForm);
                    }
                    finally
                    {
                        sema.Release();
                        sema.Close();
                    }
                }
            }
#if DEBUG
            finally
            {
            }
#else
            catch (Exception exception)
            {
                try
                {
                    string errorMsg = "异常时间:{0}\r\n异常类型:{1}\r\n异常信息:{2}\r\n堆栈信息:\r\n{3}\r\n\r\n";
                    errorMsg = string.Format(errorMsg, DateTime.Now, exception.GetType().FullName,
                        exception.Message, exception.StackTrace);
                    string fileName = Path.Combine(Application.StartupPath, "error.log");
                    File.AppendAllText(fileName, errorMsg, Encoding.UTF8);
                }
                catch { }
                MessageBox.Show("出现异常，程序将终止！","SimplusD!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
#endif
        }
    }
}
