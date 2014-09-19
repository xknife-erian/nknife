using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 判断文件是否发布，把需要发布的加入发布列表，
    /// 以路径为key存储一个需发布文件的Dictionary，供修改状态用
    /// </summary>
    class DisposeAnyFile
    {
        //UpFileXmlDoc _upFileXmlDoc;此类撤销
        List<string> _upFileList;
        /// <summary>
        /// 用来修改状态的字典，key是相对路径，value是文件的id
        /// </summary>
        Dictionary<string, string> _publishDictionary;

        public DisposeAnyFile(List<string> upFileList,
            Dictionary<string, string> publishDictionary)
        {
            //_upFileXmlDoc = upFileXmlDoc;
            _upFileList = upFileList;
            _publishDictionary = publishDictionary;
        }

        /// <summary>
        /// 判断文件是否需要发布，将需要发布的加入发布列表
        /// </summary>
        internal virtual void ExecuteAnyFilePublish(SimpleExIndexXmlElement fileEle)
        {
            //if (fileEle.IsDeleted == false && fileEle.IsModified == true
            //    && fileEle.IsPublish == true && fileEle.IsExclude == false)
            if (fileEle.IsDeleted == false && fileEle.IsModified == true && fileEle.IsPublish == true)
            {
                ///加入发布列表
                //_upFileXmlDoc.AddFileItem(fileEle.RelativeFilePath);
                _upFileList.Add(fileEle.RelativeFilePath);

                ///加入修改状态的字典
                _publishDictionary.Add(fileEle.RelativeFilePath, fileEle.Id);
            }
        }
    }
}
