using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace Jeelu.SimplusSoftwareUpdate
{
    /// <summary>
    /// 一个提供下载文件的类
    /// </summary>
    public class Download
    {
        /// <summary>
        /// 获取内容长度
        /// </summary>
        public int ContentLength 
        { get; private set; }

        /// <summary>
        /// 获取是否不知道内容的实际长度
        /// </summary>
        public bool UnknownContentLength { get; private set; }

        /// <summary>
        /// 获取已下载的数量
        /// </summary>
        public int DownloadBytesCount { get; private set; }

        /// <summary>
        /// 获取是否下载结束(不管是异常导致结束还是正常下载结束，都为true)
        /// </summary>
        public bool IsEnd { get; private set; }

        /// <summary>
        /// 获取开始运行下载的时间
        /// </summary>
        public DateTime StartTime { get; private set; }

        string _downloadUrl;
        string _localFile;
        Action<string> _callbackEndRun;
        Action<Exception> _callbackCatchException;
        Thread _thread;

        public Download()
        {
        }

        public void BeginRun(string url, string localFile, Action<string> callbackEndRun, Action<Exception> callbackCatchException)
        {
            ///检查输入参数
            Debug.Assert(!string.IsNullOrEmpty(url));
            Debug.Assert(!string.IsNullOrEmpty(localFile));
            Debug.Assert(callbackEndRun != null);

            ///初始化参数
            this._downloadUrl = url;
            this._localFile = localFile;
            this._callbackEndRun = callbackEndRun;
            this._callbackCatchException = callbackCatchException;

            ///启动一个线程，开始运行
            _thread = new Thread(new ThreadStart(BeginRunCore));
            _thread.IsBackground = true;
            _thread.Start();
        }

        private void BeginRunCore()
        {
            string localFile;
            try
            {
                localFile = RunCore();
            }
            catch (Exception ex)
            {
                if (_callbackCatchException != null)
                {
                    _callbackCatchException(ex);
                }
                return;
            }
            if (this._callbackEndRun != null)
            {
                _thread.IsBackground = false;
                _callbackEndRun(localFile);
            }
        }

        private string RunCore()
        {
            try
            {
                ///初始化成员属性
                ContentLength = 0;
                UnknownContentLength = false;
                DownloadBytesCount = 0;
                StartTime = DateTime.Now;
                IsEnd = false;

                string fileName = Path.GetFileName(_downloadUrl);

                ///连接服务器
                WebRequest request = WebRequest.Create(_downloadUrl);
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    ///下载到本地文件
                    ContentLength = (int)response.ContentLength;
                    if (ContentLength == 0)
                    {
                        ///若不知道长度，则用一个大数字:1024M
                        UnknownContentLength = true;
                        ContentLength = 1024 * 1024 * 1024;
                    }

                    byte[] buffer = new byte[1024];

                    ///创建目录
                    string parentFolder = Path.GetDirectoryName(_localFile);
                    if (!Directory.Exists(parentFolder))
                    {
                        Directory.CreateDirectory(parentFolder);
                    }

                    ///写文件
                    using (FileStream fileStream = new FileStream(_localFile, FileMode.Create, FileAccess.ReadWrite))
                    {
                        while (DownloadBytesCount < ContentLength)
                        {
                            int m = stream.Read(buffer, 0, Math.Min(buffer.Length, ContentLength - DownloadBytesCount));
                            ///若未读出数据，则认为已经读取完毕，跳出
                            if (m <= 0)
                            {
                                break;
                            }
                            fileStream.Write(buffer, 0, m);
                            DownloadBytesCount += m;
                        }

                        ///接收完毕，关闭流
                        fileStream.Flush();
                        fileStream.Close();
                        stream.Close();
                        response.Close();
                    }
                }

                return _localFile;
            }
            finally
            {
                IsEnd = true;
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url">下载的地址</param>
        /// <param name="localFilePath">本地的存储路径</param>
        /// <returns>返回下载的本地文件</returns>
        public string Run(string url, string localPath)
        {
            this._downloadUrl = url;
            this._localFile = localPath;
            return RunCore();
        }
    }
}
