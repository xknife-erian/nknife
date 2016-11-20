using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NKnife.AutoUpdater.BugFix;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Getter;
using NKnife.AutoUpdater.Interfaces;
using NKnife.Utility;

namespace NKnife.AutoUpdater
{
    /// <summary>类似全局参数的容器。单建实例。
    /// </summary>
    internal class Currents
    {
        #region 成员变量

        public const string INDEX_FILE_NAME = "UpdateIndex.xml";
        public const string UPDATER_OPTION_FILE_NAME = "UpdaterOption.xml";
        public const string UPDATER_FILE_NAME = "Pansoft.Updater.exe";
        private static IUpdaterFileGetter<FileInfo> _FileGetter;
        private static string _UpdaterOptionFile;
        private static string _IndexFile;
        private string[] _Args;
        private UpdaterOption _UpdaterOption;

        private static string _UserApplicationDataPath;

        #endregion

        #region 一些常用的属性

        /// <summary>呼叫更新程序的主程序
        /// </summary>
        /// <value>
        /// The call executing.
        /// </value>
        public string CallExecuting { get; set; }

        /// <summary>更新器(本程序)的版本
        /// </summary>
        public Version UpdaterVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        /// <summary>主程序版本，由Call Command负责赋值
        /// </summary>
        public Version CallerVersion { get; set; }

        /// <summary>IGetter正在工作的文件
        /// </summary>
        /// <value>
        /// The name of the current file.
        /// </value>
        public string CurrentFileName { get; set; }

        /// <summary>应用程序的启动参数
        /// </summary>
        public string[] Args
        {
            get { return _Args; }
            internal set
            {
                var argList = new List<string>(value.Length);
                if (value.Length > 0)
                    BugFixer.Reset(value, argList); //老版本的自动更新器，会有参数中的路径中的空格解析错误的Bug，故在此做修理
                _Args = argList.ToArray();
            }
        }

        /// <summary>更新器选项
        /// </summary>
        public UpdaterOption Option
        {
            get
            {
                if (_UpdaterOption == null)
                {
                    string content = File.ReadAllText(UpdaterOptionFile);
                    _UpdaterOption = UtilitySerialize.Deserialize<UpdaterOption>(content);
                }
                return _UpdaterOption;
            }
        }

        /// <summary>远程文件获取器
        /// </summary>
        public IUpdaterFileGetter<FileInfo> FileGetter
        {
            get { return _FileGetter ?? (_FileGetter = new FtpGetter()); }
        }

        /// <summary>用作当前非漫游用户使用的应用程序特定数据的公共储存库路径。
        /// </summary>
        /// <value>The user application data path.</value>
        public string UserApplicationDataPath
        {
            get
            {
                if (String.IsNullOrEmpty(_UserApplicationDataPath))
                {
                    const Environment.SpecialFolder folder = Environment.SpecialFolder.ApplicationData;
                    string path = Environment.GetFolderPath(folder);
                    string namespaceStr = typeof (Currents).Namespace;
                    if (String.IsNullOrWhiteSpace(namespaceStr))
                        namespaceStr = "Pansoft.Updater";
                    string subpath = namespaceStr.Replace('.', '\\');
                    _UserApplicationDataPath = Path.Combine(path, subpath);
                    if (!Directory.Exists(_UserApplicationDataPath))
                        UtilityFile.CreateDirectory(_UserApplicationDataPath);
                }
                return _UserApplicationDataPath;
            }
            set { _UserApplicationDataPath = value; }
        }

        /// <summary>本程序的选项文件
        /// </summary>
        public string UpdaterOptionFile
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_UpdaterOptionFile))
                {
                    var files = UtilityFile.SearchDirectory(AppDomain.CurrentDomain.BaseDirectory, UPDATER_OPTION_FILE_NAME);
                    if (files != null && files.Count > 0)
                        _UpdaterOptionFile = files[0];
                }
                return _UpdaterOptionFile;
            }
        }

        /// <summary>下载得到Updater的文件列表文件
        /// </summary>
        public string IndexFile
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_IndexFile))
                    _IndexFile = Path.Combine(UserApplicationDataPath, INDEX_FILE_NAME);
                return _IndexFile;
            }
        }

        /// <summary>下载到的文件所存放临时路径,在Updating类的Run方法的最开始部分进行全局的维护
        /// </summary>
        /// <value>
        /// The temp directory.
        /// </value>
        public string TempDirectory { get; set; }

        #endregion

        #region Helper方法

        /// <summary>根据指定的文件的相对路径合并出一个临时路径下的文件全名
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public FileInfo GetLocalFile(string filename)
        {
            return new FileInfo(Path.Combine(UserApplicationDataPath, TempDirectory, filename));
        }

        #endregion

        #region 单件实例

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static Currents Me
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly Currents Instance;

            static Singleton()
            {
                Instance = new Currents();
            }
        }

        private Currents()
        {
        }

        #endregion
    }
}