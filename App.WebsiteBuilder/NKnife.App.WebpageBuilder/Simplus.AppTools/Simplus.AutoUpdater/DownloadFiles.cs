using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Jeelu.SimplusSoftwareUpdate
{
    /// <summary>
    /// 下载需更新的文件列表
    /// </summary>
    public class DownloadFiles
    {
        const string NodeNameFolder = "folder";
        const string NodeNameFile = "file";

        private XmlDocument _serverFilesDoc;
        public double LastVersion { get; private set; }
        private double _currentVersion;
        private string _tempFolder;
        public string RootUrl { get; private set; }
        public FileData[] UpdateFiles { get; private set; }
        public int AllFilesCount
        {
            get
            {
                return UpdateFiles.Length;
            }
        }
        public int DownloadedFilesCount{ get; private set; }
        Download _download = new Download();
        public bool IsEnd { get; private set; }
        public long DownloadBytesCount { get; private set; }
        public long AllBytesCount { get; private set; }

        private Thread _thread;
        
        Action _callbackDoEnd;
        Action<Exception> _callbackError;

        public DownloadFiles(XmlDocument serverFilesDoc,double currentVersion,string tempFolder)
        {
            IsEnd = false;
            _serverFilesDoc = serverFilesDoc;
            _currentVersion = currentVersion;
            _tempFolder = tempFolder;

            Init();
        }

        private void Init()
        {
            ///找到根Url
            RootUrl = _serverFilesDoc.DocumentElement.GetAttribute("rootUrl");

            ///获取服务器上最新的版本号
            LastVersion = double.Parse(_serverFilesDoc.DocumentElement.GetAttribute("lastVersion"));
            if (LastVersion > _currentVersion)
            {
                ///解析版本清单列表
                List<FileData> list = new List<FileData>();
                ParseFileList(_serverFilesDoc.DocumentElement, "", list);
                UpdateFiles = list.ToArray();
            }
        }

        /// <summary>
        /// 异步的形式执行
        /// </summary>
        /// <param name="callbackDoEnd"></param>
        /// <param name="callbackError"></param>
        public void BeginRun(Action callbackDoEnd, Action<Exception> callbackError)
        {
            Debug.Assert(callbackDoEnd != null);
            Debug.Assert(callbackError != null);

            _callbackDoEnd = callbackDoEnd;
            _callbackError = callbackError;

            _thread = new Thread(new ThreadStart(BeginRunCore));
            _thread.Start();
        }

        private void BeginRunCore()
        {
            try
            {
                RunCore();
            }
            catch (Exception ex)
            {
                if (_callbackError != null)
                {
                    _callbackError(ex);
                }
                return;
            }
            if (this._callbackDoEnd != null)
            {
                _callbackDoEnd();
            }
        }

        public bool Run()
        {
            try
            {
                RunCore();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        private void RunCore()
        {
            try
            {
                ///遍历下载文件列表
                foreach (FileData file in UpdateFiles)
                {
                    string url = RootUrl + file.Path;
                    string localFile = Path.Combine(_tempFolder, file.Path);
                    _download.Run(url, localFile);

                    ///统计已下载的文件数和字节数
                    DownloadedFilesCount++;
                    DownloadBytesCount += file.Size;
                }
            }
            finally
            {
                IsEnd = true;
            }
        }

        private void ParseFileList(XmlElement folderEle, string path, List<FileData> list)
        {
            foreach (XmlNode node in folderEle.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;

                ///获取相对路径
                string name = ele.GetAttribute("name");
                string namePath = string.IsNullOrEmpty(path) ? name : path + @"/" + name;

                ///递归处理文件夹
                if (ele.Name == NodeNameFolder)
                {
                    ParseFileList(ele, namePath,list);
                }
                ///将文件添加到列表中
                else if (ele.Name == NodeNameFile)
                {
                    ///比较版本，若当前版本小于服务器文件版本，则添加到需下载的列表
                    double version = double.Parse(ele.GetAttribute("version"));
                    if (_currentVersion < version)
                    {
                        long size = long.Parse(ele.GetAttribute("size"));
                        list.Add(new FileData(namePath, size));

                        ///统计需下载文件列表的总大小
                        this.AllBytesCount += size;
                    }
                }
                else
                {
                    Debug.Fail("未处理的类型：" + ele.Name);
                }
            }
        }
    }
}
