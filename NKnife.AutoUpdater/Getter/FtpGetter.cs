using System;
using System.IO;
using System.Text;
using System.Threading;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;
using NKnife.FTP;
using NKnife.Utility;

namespace NKnife.AutoUpdater.Getter
{
    internal class FtpGetter : IUpdaterFileGetter<FileInfo>
    {
        private readonly AutoResetEvent _AutoResetEvent;
        private readonly FtpClient _Ftp;

        public FtpGetter()
        {
            _AutoResetEvent = new AutoResetEvent(false);
            if (Currents.Me.Option.Port != 21)
            {
                string u = Currents.Me.Option.Uri;
                u = u.Insert(u.Length - 1, ":");
                Currents.Me.Option.Uri = u.Insert(u.Length - 1, Currents.Me.Option.Port.ToString());
            }
            string uriStr;
            if (!string.IsNullOrWhiteSpace(Currents.Me.Option.RemotePath))
                uriStr = string.Format("{0}/{1}/", Currents.Me.Option.Uri, Currents.Me.Option.RemotePath);
            else
                uriStr = string.Format("{0}/", Currents.Me.Option.Uri);
            var url = new Uri(uriStr);
            _Ftp = new FtpClient(url, Currents.Me.Option.UserName, Currents.Me.Option.Password);
            //_Ftp.DownloadProgressChanged += (s, e) => Logger.WriteLine(string.Format("FtpGetter收到数据：{0}",e.BytesReceived));
            _Ftp.DownloadDataCompleted +=
                (s, e) =>
                    {
                        Logger.WriteLine(string.Format("{0}下载完成.{1}", Currents.Me.CurrentFileName, e.Error));
                        _AutoResetEvent.Set(); //通知异步下载已完成
                    };
        }

        #region IUpdaterFileGetter<FileInfo> Members

        /// <summary>从远程获取更新起始时，比较本地文件与远程文件之间异同的索引文件
        /// </summary>
        /// <returns></returns>
        public FileInfo GetUpdaterIndexFile()
        {
            try
            {
                var filepath = GetRemoteFullPath(Currents.INDEX_FILE_NAME);
                byte[] bs = _Ftp.DownloadFile(filepath);
                var path = Path.GetDirectoryName(Currents.Me.IndexFile);
                if (path != null && !Directory.Exists(path))
                    UtilityFile.CreateDirectory(path);
                StreamWriter fs = File.CreateText(Currents.Me.IndexFile);
                fs.Write(Encoding.UTF8.GetString(bs));
                fs.Flush();
                fs.Close();
                Logger.WriteLine("供更新用的索引文件已下载成功.");
                return new FileInfo(Currents.Me.IndexFile);
            }
            catch (Exception e)
            {
                Logger.WriteLine("比较本地文件与远程文件之间异同异常.{0}", e);
                return null;
            }
        }

        /// <summary>根据指定的文件信息从远程获取指定的文件
        /// </summary>
        /// <param name="targetFile">指定的文件信息，当FTP时，指的是文件在远程的相对路径全名</param>
        /// <returns></returns>
        public FileInfo GetTargetFile(string targetFile)
        {
            string remoteFile = GetRemoteFullPath(targetFile);
            Currents.Me.CurrentFileName = remoteFile;

            if (!_Ftp.FileExist(remoteFile))
            {
                Logger.WriteLine("服务器端文件不存在:" + remoteFile);
                return null;
            }

            var file = Currents.Me.GetLocalFile(targetFile);
            if (file.Exists)
            {
                file.Delete(); //如果目标文件已存在，将其删除
            }
            else
            {
                string dir = file.DirectoryName;
                if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                    UtilityFile.CreateDirectory(dir); //检查目标文件的目录是否存在，不存在则创建
            }
            //设置文件的总大小
            Logger.WriteLine(string.Format("文件的总大小:{0}", _Ftp.GetFileSize(remoteFile)));
            try
            {
                _Ftp.DownloadFileAsync(remoteFile, file.FullName); //异步下载文件
                _AutoResetEvent.WaitOne(); //等待异步下载完成
                return new FileInfo(file.FullName);
            }
            catch (Exception e)
            {
                Logger.WriteLine("FTP异步下载文件异常.{0}", e);
                return null;
            }
        }

        #endregion

        /// <summary>根据整体选项中的相对路径合并得出在FTP远端的文件名
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private static string GetRemoteFullPath(string file)
        {
            string filepath;
            //if (string.IsNullOrWhiteSpace(Currents.Me.Option.RemotePath))
                filepath = file;
            //else
            //    filepath = Currents.Me.Option.RemotePath + "\\" + file;
            return filepath;
        }
    }
}