using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Server
{
    //create by zhangling on 2008年6月11日
    //处理文件，文件夹
    public class DealWithFileOrFolder
    {
        private string _tempPath;
        private string _sdsitesPath;
        /// <summary>
        /// 传入用户的部分路径(如:zha\zhangling\projectname)，得到sdsites此文件夹的路径
        /// </summary>
        public string SdsitesPath
        {
            set
            {
                _sdsitesPath = AnyFilePath.GetSdsitesFolderAbsolutePath(value);
                _tempPath = AnyFilePath.GetTempFolderAbsolutePath(value);
            }
        }

        /// <summary>
        /// 处理文件
        /// </summary>
        /// <param name="simpleEle"></param>
        public void DealWithFile(SimpleExIndexXmlElement simpleEle)
        {
            /*
             * true 文件有移动;false文件无移动;
             * 为真时，表示此文件是已发布过的文件
             * 为假时，或是新上传的文件或是已修改过的文件
             * 
             */


            //上次发布后此文件的绝对路径
            string fileAbsolutePathPrevious = Path.Combine(_sdsitesPath, simpleEle.OldRelativeFilePath);

            //临时文件下的此文件的路径
            string fileAbsolutePathTemp = Path.Combine(_tempPath, simpleEle.RelativeFilePath);


            if (simpleEle.IsDeletedRecursive)
            {
                //直接找到此文件，然后删除
                FileService.FileDelete(fileAbsolutePathPrevious);
            }
            else if (!simpleEle.IsAlreadyPublished)
            {
                FileService.FileMove(fileAbsolutePathTemp, fileAbsolutePathPrevious);
            }
            else
            {
                if (simpleEle.IsChangedPosition)
                {
                    string fileAbsolutePathCurrent = Path.Combine(_sdsitesPath, simpleEle.RelativeFilePath);

                    if (!simpleEle.IsModified)
                    {
                        //文件没有重命名，并且其内容没有修改，则直接移动文件到新的位置上        
                        FileService.FileMove(fileAbsolutePathPrevious, fileAbsolutePathCurrent);
                    }
                    else
                    {
                        //则直接删除文件，然后，在从临时文件夹下将其文件移动到新的位置上
                        FileService.FileDelete(fileAbsolutePathPrevious);
                        FileService.FileMove(fileAbsolutePathTemp, fileAbsolutePathCurrent);
                    }

                }
                else
                {
                    string fileAbsolutePathCurrent = Path.Combine(_sdsitesPath, simpleEle.RelativeFilePath);

                    if (simpleEle.IsModified)
                    {
                        //修改
                        FileService.FileMove(fileAbsolutePathTemp, fileAbsolutePathCurrent);
                    }
                }
            }
        }
    }
}
