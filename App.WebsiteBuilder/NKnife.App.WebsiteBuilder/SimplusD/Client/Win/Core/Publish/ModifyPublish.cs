using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 用来修改状态的类
    /// </summary>
    class ModifyPublish
    {
        /// <summary>
        /// 用来修改状态的字典，key是相对路径，value是文件的id
        /// (在DisposeAnyFile里生成，通过构造函数传过来的)
        /// </summary>
        Dictionary<string, string> _publishDictionary;
      
        public ModifyPublish(Dictionary<string, string> publishDictionary)
        {
            _publishDictionary = publishDictionary;
        }

        public void ExecuteModifyPublish(string fileUrl)
        {
            if (Path.GetExtension(fileUrl) != Utility.Const.SdsiteFileExt)
            {
                //如果是模板文件，则将其对应的页面片节点的isModified设为false
                if (Path.GetExtension(fileUrl) == Utility.Const.TmpltFileExt)
                {
                    string tmpltId = _publishDictionary[fileUrl];
                    TmpltXmlDocument tmpltDoc = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(tmpltId);
                    foreach (SnipXmlElement snipEle in tmpltDoc.GetSnipElementList())
                    {
                        snipEle.IsModified = false;
                    }
                    tmpltDoc.Save();
                }

                string eleId = _publishDictionary[fileUrl];
                Service.Sdsite.CurrentDocument.MarkAlreadyPublished(eleId);
            }
        }

        /// <summary>
        /// 发布的处理; 删除属性OldParentFolderId和OldFileName ;删除IsDeleted=true的节点
        /// </summary>
        internal void ExecuteAfterPublish()
        {
            //将所有含有OldparentFolderID，OldFileName的节点的删除属性删除
            XmlNodeList xmlNodeList = Service.Sdsite.CurrentDocument.SelectNodes("//*[@oldParentFolderId|@oldFileName]");
            foreach (SimpleExIndexXmlElement simple in xmlNodeList)
            {
                if (!string.IsNullOrEmpty(simple.OldFileName))
                {
                    simple.RemoveAttribute("oldFileName");
                }
                else
                {
                    simple.RemoveAttribute("oldParentFolderId");
                }
            }

            XmlNodeList xmlFolderList = Service.Sdsite.CurrentDocument.SelectNodes("//folder|//channel|//tmpltRootFolder|//resources");
            foreach (FolderXmlElement folderEle in xmlFolderList)
            {
                Service.Sdsite.CurrentDocument.MarkAlreadyPublished(folderEle);
            }

            XmlNodeList xmlDeleteList = Service.Sdsite.CurrentDocument.SelectNodes("//*[@isDeleted=\"True\"]");
            foreach(SimpleExIndexXmlElement simple in xmlDeleteList)
            {
                simple.ParentNode.RemoveChild(simple);
            }
            
            Service.Sdsite.CurrentDocument.Save();
        }
    }
}
