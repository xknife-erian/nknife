using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.WordSegmentor;
using System.IO;
using System.Xml;

namespace Jeelu.Billboard
{
    /// <summary>
    /// 词库文件。Jeelu继承重写一些新的方法
    /// </summary>
    [Serializable]
    public class JDictionary : T_DictFile
    {
        /// <summary>
        /// 词库文件的文件实体信息
        /// </summary>
        public FileInfo FileInfo { get; set; }
        /// <summary>
        /// 词库文件的版本号
        /// </summary>
        public uint Version { get; set; } 
        /// <summary>
        /// 词库所在频道是否是根频道（类别）
        /// </summary>
        public bool IsRootChannel { get; set; }
        /// <summary>
        /// 词库是否有子频道（类别）
        /// </summary>
        public bool HasSubChannel { get; set; }
        /// <summary>
        /// 词典所在频道（类别）
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 保存词库文件为二进制文件
        /// </summary>
        public virtual void Save()
        {
            Dict.SaveToBinFileEx(this.FileInfo.FullName, this);
        }


    }
}
