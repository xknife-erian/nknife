using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class DataNode : BaseTreeNode
    {
        public DataNode(string filePath,bool isFolder)
        {
            FilePath = filePath;
            IsFolder = isFolder;
            this.Text = Path.GetFileName(filePath);
        }

        /// <summary>
        /// 此节点对应的路径(有可能是文件或文件夹)
        /// </summary>
        public string FilePath { get; internal set; }

        /// <summary>
        /// 此节点对应的路径是否存在
        /// </summary>
        public bool IsExist
        {
            get
            {
                if (IsFolder)
                {
                    return Directory.Exists(FilePath);
                }
                else
                {
                    return File.Exists(FilePath);
                }
            }
        }

        /// <summary>
        /// 此节点是否对应的文件夹
        /// </summary>
        public bool IsFolder { get; private set; }
    }
}
