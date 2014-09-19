using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace Jeelu.SimplusSoftwareUpdate
{
    public class UpdateClass
    {
        /// <summary>
        /// 每次发布的时候必须更改此属性，一般设为当前时间
        /// </summary>
        readonly DateTime ThisVersionDate = new DateTime(2008, 5, 21);
        const string UpdateExe = "update.exe";
        const string MessageTitle = "升级程序";

        /// <summary>
        /// 目标的一个互斥体
        /// </summary>
        public Mutex TargetMutex { get; private set; }
        /// <summary>
        /// 当前升级程序的一个互斥体
        /// </summary>
        public Mutex MyMutex { get; private set; }

        /// <summary>
        /// 获取是否用户的点击触发这次检查更新
        /// </summary>
        static public bool IsUserClick { get; private set; }

        /// <summary>
        /// 获取是否自动下载最新文件
        /// </summary>
        static public bool IsAutoDownload { get; private set; }

        /// <summary>
        /// 是否刚刚更新自己
        /// </summary>
        static public bool UpdateSelfJustNow { get; private set; }

        /// <summary>
        /// 版本清单信息的XmlDocument
        /// </summary>
        static public XmlDocument VersionDoc { get; private set; }

        /// <summary>
        /// 更新信息的XmlDocument
        /// </summary>
        static public XmlDocument UpdateInfoDoc { get; private set; }
        static private string _updateInfoFile;

        /// <summary>
        /// 保存将要打开的.sdsite的文件路径
        /// </summary>
        static public string WillOpenSdFile { get; private set; }

        /// <summary>
        /// 当前版本
        /// </summary>
        static public double CurrentVersion;

        /// <summary>
        /// 服务器上的版本
        /// </summary>
        static public double ServerVersion;

        /// <summary>
        /// 更新的状态
        ///// </summary>
        //static public UpdateState State
        //{
        //    get
        //    {
        //        Debug.Assert(UpdateInfoDoc != null);

        //        string strState = UpdateInfoDoc.DocumentElement["update"].GetAttribute("state");
        //        return (UpdateState)Enum.Parse(typeof(UpdateState), strState,true);
        //    }
        //}

        //public void SetUpdateElement(UpdateState state, string setupFile)
        //{
        //    Debug.Assert(UpdateInfoDoc != null);

        //    XmlElement updateEle = UpdateInfoDoc.DocumentElement["update"];
        //    updateEle.SetAttribute("state", state.ToString());
        //    if (state == UpdateState.Downloaded)
        //    {
        //        updateEle.SetAttribute("downloadTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        //        Debug.Assert(!string.IsNullOrEmpty(setupFile));
        //        updateEle.SetAttribute("setupFile", setupFile);
        //    }
        //    UpdateInfoDoc.Save(_updateInfoFile);
        //}

        //public void SetUpdateElement(UpdateState state)
        //{
        //    SetUpdateElement(state, null);
        //}

        public void Run(string[] args)
        {
            ///解析传入的命令参数
            #region 解析命令参数

            IsUserClick = true;
            IsAutoDownload = false;
            UpdateSelfJustNow = false;

            if (args != null && args.Length != 0)
            {
                string prevArgs = "";   //上一个命令

                foreach (string item in args)
                {
                    ///-s表示是不是用户显式点击产生的这次检查更新
                    if (item.Equals("-s", StringComparison.CurrentCultureIgnoreCase))
                    {
                        IsUserClick = false;
                    }
                    ///-a表示有更新版本时自动下载而不需提示
                    else if (item.Equals("-a", StringComparison.CurrentCultureIgnoreCase))
                    {
                        IsAutoDownload = true;
                    }
                    ///-m表示当前程序的运行还在UpdateTemp文件夹里，需要将自己移出来
                    else if (item.Equals("-m", StringComparison.CurrentCultureIgnoreCase))
                    {
                        ///先删除原本的文件
                        string targetPath = Path.Combine(Path.GetDirectoryName(Application.StartupPath), UpdateExe);
                        if (File.Exists(targetPath))
                        {
                            while (true)
                            {
                                try
                                {
                                    File.Delete(targetPath);
                                    break;
                                }
                                catch (Exception)
                                {
                                    Thread.Sleep(1000 * 3);
                                }
                            }
                        }

                        ///将当前文件移到安装文件夹
                        File.Move(Application.ExecutablePath, targetPath);

                        ///用一样的参数(但去掉-m)打开安装文件夹的更新文件
                        List<string> newArgs = new List<string>();
                        newArgs.Add("-u");
                        foreach (string str in args)
                        {
                            if (!str.Equals("-m", StringComparison.CurrentCultureIgnoreCase))
                            {
                                newArgs.Add(str);
                            }
                        }
                        Process.Start(targetPath, string.Join(" ", newArgs.ToArray()));
                        return;
                    }
                    ///-u表示当前程序刚从UpdateTemp文件夹拷出来，此时需要将UpdateTemp里的文件删除
                    else if (item.Equals("-u", StringComparison.CurrentCultureIgnoreCase))
                    {
                        UpdateSelfJustNow = true;
                    }
                    else
                    {
                        ///若前一个是-r，则表示记忆sdsite文件路径
                        if (prevArgs.Equals("-r", StringComparison.CurrentCultureIgnoreCase))
                        {
                            WillOpenSdFile = item;
                        }
                    }

                    ///记录上一个命令
                    prevArgs = item;
                }
            }
            #endregion

            LogService.WriteInfoLog("start in");
            ResetDirectory(MainClass.TempFolder);

            ///若不是刚刚更新自己，则先更新自己
            #region 更新自己

            if (!UpdateSelfJustNow)
            {
                string updateExeVersionUrl = MainClass.UpdateExeVersion;
                XmlDocument UpdateExeVersionDoc = new XmlDocument();
                try
                {
                    LogService.WriteInfoLog("开始下载:" + updateExeVersionUrl);
                    UpdateExeVersionDoc.Load(updateExeVersionUrl);
                    LogService.WriteInfoLog("结束下载:" + updateExeVersionUrl);
                }
                catch (Exception ex)
                {
                    LogService.WriteErrorLog(ex,"url:"+updateExeVersionUrl);
                    if (IsUserClick)
                    {
                        MessageBox.Show("连接服务器失败！", MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    End();
                    return;
                }
                string strLastUpdateDate = UpdateExeVersionDoc.DocumentElement.GetAttribute("lastUpdateDate");
                DateTime lastUpdateDate = DateTime.ParseExact(strLastUpdateDate, "yyyy-MM-dd", null);

                ///比较网上最新版的时间和当前版本时间，不一样则更新
                if (ThisVersionDate.Year != lastUpdateDate.Year
                    || ThisVersionDate.Month != lastUpdateDate.Month
                    || ThisVersionDate.Day != lastUpdateDate.Day)
                {
                    string downloadUrl = UpdateExeVersionDoc.DocumentElement.GetAttribute("downloadUrl");
                    string localFileName = Path.Combine(MainClass.TempFolder, UpdateExe);
                    Download download = new Download();

                    ///下载
                    try
                    {
                        download.Run(downloadUrl, localFileName);

                        ///用一样的参数(但多加一个-m)打开安装文件夹的更新文件
                        List<string> newArgs = new List<string>();
                        newArgs.Add("-m");
                        newArgs.AddRange(args);
                        string argument = string.Join(" ", newArgs.ToArray());
                        Process.Start(localFileName, argument);

                        LogService.WriteInfoLog("更新update.exe成功.\r\n");
                    }
                    catch (Exception ex)
                    {
                        LogService.WriteErrorLog(ex, "url:" + downloadUrl);
                        if (IsUserClick)
                        {
                            MessageBox.Show("升级update程序失败！");
                        }
                        End();
                    }
                    return;
                }
            }

            #endregion

            ///读更新信息XmlDocument
            _updateInfoFile = Path.Combine(Application.StartupPath, "UpdateInfo.xml");
            UpdateInfoDoc = new XmlDocument();
            UpdateInfoDoc.Load(_updateInfoFile);
            XmlElement currentVersionEle = (XmlElement)UpdateInfoDoc.DocumentElement.SelectSingleNode(@"currentVersion");

            ///读出当前版本
            Debug.Assert(currentVersionEle != null);
            CurrentVersion = double.Parse(currentVersionEle.GetAttribute("verion"));

            ///获取服务器上的版本清单
            XmlDocument serverFilesDoc = new XmlDocument();
            try
            {
                LogService.WriteInfoLog("开始下载:" + MainClass.VersionFileAddress);
                serverFilesDoc.Load(MainClass.VersionFileAddress);
                LogService.WriteInfoLog("结束下载:" + MainClass.VersionFileAddress);
            }
            catch(Exception ex)
            {
                LogService.WriteErrorLog(ex, "url:" + MainClass.VersionFileAddress);
                if (IsUserClick)
                {
                    MessageBox.Show("连接服务器获取版本清单失败！", MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                End();
                return;
            }

            ///下载需更新的文件
            DownloadFiles downloadFiles = new DownloadFiles(serverFilesDoc, CurrentVersion, MainClass.TempFolder);
            ServerVersion = downloadFiles.LastVersion;

            if (CurrentVersion >= ServerVersion)
            {
                ///没有更新，当前版本已是最新
                if (IsUserClick)
                {
                    MessageBox.Show("没有更新，当前版本已是最新版。",MessageTitle,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                End();
                return;
            }

            ///有更新，询问用户是否更新
            if (MessageBox.Show("软件有更新，是否下载并安装？", MessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
            {
                End();
                return;
            }

            LogService.WriteInfoLog("有更新并开始下载");
            downloadFiles.BeginRun(CallbackEndRun, CallbackException);

            _progressForm = new ProgressForm(downloadFiles);
            Application.Run(_progressForm);
        }

        private void ResetDirectory(string path)
        {
            try
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            catch
            {
            }
        }

        static public void End(bool mustStartupSd)
        {
            if (mustStartupSd || !IsUserClick)
            {
                ///打开SimplusD.exe
                List<string> argsList = new List<string>();
                argsList.Add("-n");   //-n表示不需要启动更新
                if (!string.IsNullOrEmpty(WillOpenSdFile))
                {
                    argsList.Add("-s \"" + WillOpenSdFile + "\"");   //-s后面跟.sdsite文件路径
                }
                string argument = string.Join(" ", argsList.ToArray());
                string sd = Path.Combine(Application.StartupPath, MainClass.SimplusDExe);
                Process.Start(sd, argument);

                LogService.WriteInfoLog("End();argument:" + argument);
                //MessageBox.Show(sd + "," + argument);
            }
        }
        static public void End()
        {
            End(false);
        }

        ProgressForm _progressForm;

        private void CallbackEndRun()
        {
            try
            {
                /////更改状态
                //SetUpdateElement(UpdateState.Downloaded);

                ///安装下载下来的文件
                Setup setup = new Setup(MainClass.TempFolder, Application.StartupPath);
                if (setup.Run())
                {
                    ///安装成功，更改当前版本
                    XmlElement currentVersionEle = (XmlElement)UpdateInfoDoc.DocumentElement.SelectSingleNode(@"currentVersion");
                    currentVersionEle.SetAttribute("verion", ServerVersion.ToString());

                    ///防止文件为只读
                    if (File.Exists(_updateInfoFile))
                    {
                        File.SetAttributes(_updateInfoFile, FileAttributes.Normal);
                    }
                    UpdateInfoDoc.Save(_updateInfoFile);

                    _progressForm.Invoke(new Action(_progressForm.CallbackEndRun));
                }
                else
                {
                    LogService.WriteInfoLog("安装过程失败。");
                    CallbackException(null);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex, "下载完后的CallbackEndRun()方法异常");
                MessageBox.Show("安装文件失败！", MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void CallbackException(Exception ex)
        {
            LogService.WriteErrorLog(ex, "下载过程抛出未处理异常。CallbackException()方法。");
            _progressForm.Invoke(new Action<Exception>(_progressForm.CallbackException), ex);
            Application.Exit();
        }

        ///// <summary>
        ///// 初始化
        ///// </summary>
        //public void Initialize()
        //{
        //    ///打开一个互斥体
        //    bool createdNew;
        //    TargetMutex = new Mutex(false, "", out createdNew);
        //}
    }

    public enum UpdateState
    {
        HasNoVersion    = 0,
        HasNewVersion   = 1,
        Downloading     = 2, 
        Downloaded      = 3,
    }
}
