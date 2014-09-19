using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Server
{
    public class FileService
    {
        public static bool FileSave(byte[] content,string filePath)
        {
            try
            {
                string fileUrlPath=Path.GetDirectoryName(filePath);
                Directory.CreateDirectory(fileUrlPath);
                File.WriteAllBytes(filePath, content); 
                return true;
            }
            catch(Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                return false;
            }
        }

        public static bool FileDelete(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                return false;
            }
        }
        /// <summary>
        /// 移动文件到新位置 (标注：原位置不在有此文件)
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        /// <returns></returns>
        public static bool FileMove(string sourceFilePath, string targetFilePath)
        {
            if (!File.Exists(sourceFilePath)) return false;
            string targetPath = Path.GetDirectoryName(targetFilePath);
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            try
            {
                if (File.Exists(targetFilePath))
                {
                    File.Delete(targetFilePath);
                }
                File.Move(sourceFilePath, targetFilePath);//不提供目录的创建，只能在同一目录下
                return true;
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                return false;
            }
          
        }
        /// <summary>
        /// 将现在文件复制到新文件里 (如果有同名的文件，则就将其删除)
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        /// <returns></returns>
        public static bool FileCopy(string sourceFilePath, string targetFilePath)
        {
            if (!File.Exists(sourceFilePath)) return false;
            string targetPath = Path.GetDirectoryName(targetFilePath);
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            try
            {
                if (File.Exists(targetFilePath))
                {
                    File.Delete(targetFilePath);
                }
                File.Copy(sourceFilePath, targetFilePath); //不提供目录的创建，只能在同一目录下
                return true;
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                return false;
            }
            
        }

        //以上只是文件与文件的操作，以下将是文件与目录的操作，如将某个文件拷贝到目录下

    }
}
