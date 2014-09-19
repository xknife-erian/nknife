using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Jeelu.SimplusD.Server
{
    public class FolderService
    {
        public static bool FolderDelete(string directoryPath)
        {
            try
            {
                Directory.Delete(directoryPath, true);
                return true;
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                return false;
            }
        }

        public static bool FolderMove(string sourceDirectoryPath, string targetDirectoryPath)
        {
            try
            {
                //当sourceDirectoryPath 与targetDirectoryPath路径相同时，则会出现问题
                Directory.Move(sourceDirectoryPath, targetDirectoryPath);
                return true;
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                return false;
            }
        }

        //此处将要被删除
        /// <summary>
        /// 获得节点的源父文件夹filePath
        /// </summary>
        public static string GetElementSourcePath(SimpleExIndexXmlElement simpleEle)
        {
            return simpleEle.OldRelativeFilePath;
        }

        /// <summary>
        /// 获得节点的源父文件夹Url
        /// </summary>
        public static string GetElementSourceUrlPath(SimpleExIndexXmlElement simpleEle)
        {
            return simpleEle.OldRelativeUrl;
        }
    }
}
