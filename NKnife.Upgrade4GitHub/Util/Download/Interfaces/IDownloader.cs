using System;
using System.Threading.Tasks;
using NKnife.Upgrade4Github.Util.Download.Enums;
using NKnife.Upgrade4Github.Util.Download.Events;

namespace NKnife.Upgrade4Github.Util.Download.Interfaces
{
    public interface IDownloader
    {
        /// <summary>
        ///     当下载完成时发生
        /// </summary>
        event EventHandler DownloadCompleted;

        /// <summary>
        ///     当下载被取消时发生
        /// </summary>
        event EventHandler DownloadCancelled;

        /// <summary>
        ///     当下载进度发生变化时发生
        /// </summary>
        event ProgressChangedEventHandler DownloadProgressChanged;

        /// <summary>
        ///     获取将在下载过程启动时下载的源URL。
        /// </summary>
        string FileUrl { get; set; }

        /// <summary>
        ///     获取下载过程完成后将保存文件的目标路径。
        /// </summary>
        string DestPath { get; set;}

        /// <summary>
        ///     如果源支持将范围设置为请求，则返回true（如果未返回false）
        /// </summary>
        bool AcceptRange { get; }

        /// <summary>
        ///     获取报告下载过程状态。
        /// </summary>
        DownloadState State { get; }


        long ContentSize { get; }
        long BytesReceived { get; }
        int Progress { get; }
        int SpeedInBytes { get; }
        Task StartAsync();
        void Pause();
        Task ResumeAsync();
        void Cancel();
    }
}
