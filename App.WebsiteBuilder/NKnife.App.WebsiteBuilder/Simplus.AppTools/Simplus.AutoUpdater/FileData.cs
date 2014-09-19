using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusSoftwareUpdate
{
    /// <summary>
    /// 表示一个需下载的文件
    /// </summary>
    public class FileData
    {
        public long Size { get; private set; }
        public string Path { get; private set; }
        public FileData(string path, long size)
        {
            this.Path = path;
            this.Size = size;
        }
    }
}
