using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NKnife.Upgrade4Github.Base;
using NKnife.Upgrade4Github.Base.GithubDomain;
using NKnife.Upgrade4Github.Util.Download;
using NKnife.Upgrade4Github.Util.Download.Events;
using NKnife.Upgrade4Github.Util.Download.Interfaces;
using NKnife.Upgrade4Github.Util.Zip;
using Octokit;

namespace NKnife.Upgrade4Github.App
{
    /// <summary>
    /// 下载更新包的服务
    /// </summary>
    class UpdateService : IUpdateService
    {
        #region 私有字段

        /// <summary>
        /// 文件http下载组件
        /// </summary>
        private IDownloader _downLoader;
        
        /// <summary>
        /// 更新文件下载后的保存路径
        /// </summary>
        private string _localFileFullPath;

        private string _localFileName;
        private string _localFileDir;

        /// <summary>
        /// 从github获取到的更新信息
        /// </summary>
        private LatestRelease _currentLatestRelease;

        private bool _isRunStart;

        #endregion

        #region 构造函数与初始化
        public UpdateArgs UpdateArgs { get; } = new UpdateArgs();

        /// <summary>
        ///     构造函数
        /// </summary>
        public UpdateService()
        {
            Initialize();
        }

        /// <summary>
        ///     初始化数据
        /// </summary>
        private void Initialize()
        {
            InitializeDownLoader();
        }

        #endregion

        #region 事件函数

        public event EventHandler<UpdateStatusChangedEventArgs> UpdateStatusChanged;

        protected virtual void OnUpdateStatusChanged(UpdateStatusChangedEventArgs e)
        {
            UpdateStatusChanged?.Invoke(this, e);
        }

        #endregion

        #region 公共函数

        /// <summary>
        ///     获取更新信息
        /// </summary>
        public async void GetLatestReleaseInfo()
        {
            await Task.Factory.StartNew(() =>
            {
                if (!FromGithub.TryGetLatestRelease(UpdateArgs.Username, UpdateArgs.Project, out _currentLatestRelease, out var msg))
                {
                    OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.Error, msg));
                    return;
                }
                //为用户界面组装一条更新信息
                var infoBuilder = BuildUpdateTipInfo(_currentLatestRelease);
                OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.GetLatestReleaseCompleted, infoBuilder.ToString()));
            });
        }

        /// <summary>
        /// 为用户界面组装一条更新信息
        /// </summary>
        private StringBuilder BuildUpdateTipInfo(LatestRelease release)
        {
            var infoBuilder = new StringBuilder();
            if (release == null)
                return infoBuilder;
            try
            {
                infoBuilder.AppendLine($"项目：{release.Name}");
                infoBuilder.AppendLine($"版本：{release.TagName}");
                infoBuilder.AppendLine($"日期：{release.PublishedAt}");
            }
            catch (Exception e)
            {
                OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.Error, $"获取更新信息错误。\r\n{e.Message}"));
            }
            infoBuilder.AppendLine("更新记录：").AppendLine($"{release.Body}");

            if (release.Assets != null)
            {
                infoBuilder.AppendLine("------");
                foreach (var item in release.Assets)
                {
                    var size = item.Size / 1024;
                    infoBuilder.AppendLine($"File：{item.Name} ({item.DownloadCount}) Size:{size}Kb");
                }
            }

            return infoBuilder;
        }

        /// <summary>
        ///     开始运行更新程序
        /// </summary>
        public async void StartAsync()
        {
            if (_isRunStart)
                return;
            _isRunStart = true;

            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var name = _currentLatestRelease.Assets[0].Name;
            var url = _currentLatestRelease.Assets[0].BrowserDownloadUrl;
            _localFileDir = dir;
            _localFileName = name;
            _localFileFullPath = Path.Combine(dir, name);
            _downLoader.FileUrl = url;
            _downLoader.DestPath = _localFileFullPath;
            await _downLoader.StartAsync();
        }

        public void Stop()
        {
            _downLoader.Cancel();
        }

        #endregion

        #region 内部函数

        /// <summary>
        ///     初始化下载组件
        /// </summary>
        private void InitializeDownLoader()
        {
            _downLoader = new HttpDownloader();
            _downLoader.DownloadCompleted+= DownLoader_Completed;
            _downLoader.DownloadCancelled += DownLoader_Cancelled;
            _downLoader.DownloadProgressChanged += DownLoader_ProgressChanged;
        }

        private void DownLoader_Completed(object sender, EventArgs ea)
        {
            OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.FileDownloading, "下载完成."));

            //开始更新
            if (!File.Exists(_localFileFullPath)) 
                return;
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                //解压缩文件
                OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.Update, "解压缩文件中"));
                GZip.Decompress(_localFileDir, AppDomain.CurrentDomain.BaseDirectory, _localFileName);

                //删除压缩包
                File.Delete(_localFileFullPath);
            }
            catch (Exception e)
            {
                OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.Error, $"更新压缩包操作错误：{e.Message}"));
            }

            watch.Stop();
            var timespan = watch.Elapsed;
            OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.Update, $"解压缩执行时间：{timespan.TotalMilliseconds}(毫秒)"));
            OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.Done, "更新完成"));
            if (UpdateArgs.Parent.IsAutoRun)
            {
                var strRunPath = AppDomain.CurrentDomain.BaseDirectory + UpdateArgs.Parent.Runner;
                if (!File.Exists(strRunPath))
                    return;
                try
                {
                    Process.Start(strRunPath);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            _isRunStart = false;
        }

        private void DownLoader_Cancelled(object sender, EventArgs e)
        {
            OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.Cancelled, $"取消下载"));
        }

        private void DownLoader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            OnUpdateStatusChanged(new UpdateStatusChangedEventArgs(UpdateStatus.FileDownloading, $"下载中:{e.Progress}%"));
        }

        #endregion

    }
}