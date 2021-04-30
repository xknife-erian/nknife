using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NKnife.Upgrade4Github.Util.Download.Enums;
using NKnife.Upgrade4Github.Util.Download.Interfaces;
using DownloadProgressChangedEventArgs = NKnife.Upgrade4Github.Util.Download.Events.DownloadProgressChangedEventArgs;
using ProgressChangedEventHandler = NKnife.Upgrade4Github.Util.Download.Events.ProgressChangedEventHandler;

namespace NKnife.Upgrade4Github.Util.Download
{
    /// <summary>
    ///     Contains methods to help downloading
    /// </summary>
    public class HttpDownloader : IDownloader
    {
        #region Variables

        /// <summary>
        ///     当下载完成时发生
        /// </summary>
        public event EventHandler DownloadCompleted;

        /// <summary>
        ///     当下载被取消时发生
        /// </summary>
        public event EventHandler DownloadCancelled;

        /// <summary>
        ///     当下载进度发生变化时发生
        /// </summary>
        public event ProgressChangedEventHandler DownloadProgressChanged;

        /// <summary>
        ///     当响应头接收到时触发，例如ContentLength，ResumeAbility
        /// </summary>
        public event EventHandler HeadersReceived;

        private HttpWebRequest _req;
        private HttpWebResponse _resp;
        private Stream _stream;
        private FileStream _file;
        private Stopwatch _stopWatch;
        private readonly AsyncOperation _operation;
        private int _bytesReceived, _progress, _speedBytes;
        private DownloadState _state;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets content size of the file
        /// </summary>
        public long ContentSize { get; private set; }

        /// <summary>
        ///     Gets the total bytes count received
        /// </summary>
        public long BytesReceived => _bytesReceived;

        /// <summary>
        ///     Gets the current download speed in bytes
        /// </summary>
        public int SpeedInBytes { get; private set; }

        /// <summary>
        ///     获取当前下载进度的百分比值。
        /// </summary>
        public int Progress
        {
            get => _progress;
            private set
            {
                _progress = value;
                _operation.Post(delegate
                {
                    DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs(_progress, SpeedInBytes));
                }, null);
            }
        }

        /// <summary>
        ///     获取将在下载过程启动时下载的源URL。
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        ///     获取下载过程完成后将保存文件的目标路径。
        /// </summary>
        public string DestPath { get; set; }

        /// <summary>
        ///     如果源支持将范围设置为请求，则返回true（如果未返回false）
        /// </summary>
        public bool AcceptRange { get; private set; }

        /// <summary>
        ///     获取报告下载过程状态。
        /// </summary>
        public DownloadState State
        {
            get => _state;
            private set
            {
                _state = value;
                if (_state == DownloadState.Completed && DownloadCompleted != null)
                    _operation.Post(delegate
                    {
                        DownloadCompleted?.Invoke(this, EventArgs.Empty);
                    }, null);
                else if (_state == DownloadState.Cancelled && DownloadCancelled != null)
                    _operation.Post(delegate
                    {
                        DownloadCancelled?.Invoke(this, EventArgs.Empty);
                    }, null);
            }
        }

        #endregion

        #region Constructor, Destructor, Download Procedure

        /// <summary>
        ///     构造函数。初始化HttpDownloader类，实现<see cref="IDownloader"/>。
        /// </summary>
        public HttpDownloader()
        {
            Reset();
            _operation = AsyncOperationManager.CreateOperation(null);
        }

        /// <summary>
        ///     构造函数。初始化HttpDownloader类，实现<see cref="IDownloader"/>。
        /// </summary>
        /// <param name="url">Url源路径</param>
        /// <param name="destPath">下载文件保存路径</param>
        public HttpDownloader(string url, string destPath)
            :this()
        {
            FileUrl = url;
            DestPath = destPath;
        }

        ~HttpDownloader()
        {
            Cancel();
        }

        private void Download(int offset, bool overWriteFile)
        {
            #region Send Request, Get Response

            try
            {
                _req = WebRequest.Create(FileUrl) as HttpWebRequest;
                if (_req == null)
                    return;
                _req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
                _req.AddRange(offset);
                _req.AllowAutoRedirect = true;
                _resp = _req.GetResponse() as HttpWebResponse;
                if (_resp == null)
                    return;
                _stream = _resp.GetResponseStream();
                if (!overWriteFile)
                {
                    ContentSize = _resp.ContentLength;
                    AcceptRange = GetAcceptRangeHeaderValue();
                    if (HeadersReceived != null)
                        _operation.Post(delegate { HeadersReceived(this, EventArgs.Empty); }, null);
                }
            }
            catch (Exception)
            {
                _state = DownloadState.Completed;
                return;
            }

            #endregion

            if (overWriteFile)
            {
                _file = File.Open(DestPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                FileMode mode = FileMode.Create;
                if (File.Exists(DestPath))
                    mode = FileMode.Truncate;
                _file = new FileStream(DestPath, mode, FileAccess.Write);
            }

            var bytesRead = 0;
            _speedBytes = 0;
            var buffer = new byte[4096];
            _stopWatch.Reset();
            _stopWatch.Start();

            #region Get the data to the buffer, write it to the file

            while (_stream != null && (bytesRead = _stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if ((_state == DownloadState.Cancelled) | (_state == DownloadState.Paused)) 
                    break;
                _state = DownloadState.Downloading;
                _file.Write(buffer, 0, bytesRead);
                _file.Flush();
                _bytesReceived += bytesRead;
                _speedBytes += bytesRead;
                Progress = _progress = (int) (_bytesReceived * 100.0 / ContentSize);
                SpeedInBytes = (int) (_speedBytes / 1.0 / _stopWatch.Elapsed.TotalSeconds);
            }

            #endregion

            _stopWatch.Reset();
            CloseResources();
            Thread.Sleep(100);
            if (_state == DownloadState.Downloading)
            {
                _state = DownloadState.Completed;
                State = _state;
            }
        }

        #endregion

        #region Start, Pause, Stop, Resume

        /// <summary>
        ///     Starts the download async
        /// </summary>
        public async Task StartAsync()
        {
            if ((_state != DownloadState.Started) & (_state != DownloadState.Completed) & (_state != DownloadState.Cancelled))
                return;

            _state = DownloadState.Started;
            await Task.Run(() => { Download(0, false); });
        }

        /// <summary>
        ///     Pauses the download process
        /// </summary>
        public void Pause()
        {
            if (!AcceptRange)
                throw new Exception("This download process cannot be paused because it doesn't support ranges");
            if (State == DownloadState.Downloading)
                _state = DownloadState.Paused;
        }

        /// <summary>
        ///     Resumes the download, only if the download is paused
        /// </summary>
        public async Task ResumeAsync()
        {
            if (State != DownloadState.Paused) return;
            _state = DownloadState.Started;
            await Task.Run(() => { Download(_bytesReceived, true); });
        }

        /// <summary>
        ///     Cancels the download and deletes the uncompleted file which is saved to destination
        /// </summary>
        public void Cancel()
        {
            if ((_state == DownloadState.Completed) | (_state == DownloadState.Cancelled) | (_state == DownloadState.ErrorOccured)) return;
            if (_state == DownloadState.Paused)
            {
                Pause();
                _state = DownloadState.Cancelled;
                Thread.Sleep(100);
                CloseResources();
            }

            _state = DownloadState.Cancelled;
        }

        #endregion

        #region FromGithub Methods

        private void Reset()
        {
            _progress = 0;
            _bytesReceived = 0;
            SpeedInBytes = 0;
            _stopWatch = new Stopwatch();
        }

        private void CloseResources()
        {
            _resp?.Close();
            _file?.Close();
            _stream?.Close();
            if (DestPath != null && (_state == DownloadState.Cancelled) | (_state == DownloadState.ErrorOccured))
                try
                {
                    File.Delete(DestPath);
                }
                catch
                {
                    throw new Exception("There is an error unknown. This problem may cause because of the file is in use");
                }
        }

        private bool GetAcceptRangeHeaderValue()
        {
            for (var i = 0; i < _resp.Headers.Count; i++)
                if (_resp.Headers.AllKeys[i].Contains("Range"))
                    return _resp.Headers[i].Contains("byte");
            return false;
        }

        private string GetFileNameFromUrl()
        {
            return Path.GetFileName(new Uri(FileUrl).AbsolutePath);
        }

        #endregion
    }
}