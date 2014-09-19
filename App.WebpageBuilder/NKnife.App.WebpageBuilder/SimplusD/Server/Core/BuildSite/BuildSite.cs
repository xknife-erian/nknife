using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Jeelu.SimplusD.Server
{
    /// <summary>
    /// 上传成功后，处理处理页面生成
    /// </summary>
    public class BuildSite
    {
        ToHtmlHelper toHtmlHelperObj;
        BuildFile buildFile;

        string resStartPath = "";
        string resToPath = "";
        //SdsiteXmlDocument sdsiteDocument;

        /// <summary>
        /// 开始生成网站
        /// </summary>
        /// <returns></returns>
        public bool StartBuild(string partPath, string projName)
        {
            string sdPath = AnyFilePath.GetSdsitesFolderAbsolutePath(partPath);
            string buildPath = AnyFilePath.GetWebAbsolutePath(partPath);
            toHtmlHelperObj = new ToHtmlHelper(sdPath, buildPath);

            resStartPath = sdPath + @"Root\" + @"Resource";
            resToPath = buildPath + @"\Resource";

            buildFile = new BuildFile();
            buildFile.SdsiteDocument = toHtmlHelperObj.SdsiteXmlDocument;
            buildFile.ToHtmlHelperObj = toHtmlHelperObj;

            FolderXmlElement channelRootEle = toHtmlHelperObj.SdsiteXmlDocument.RootChannel;

            //string sdPath = AnyFilePath.GetSdsitesFolderAbsolutePath(partPath) + projName + ".sdsite";
            ////打开网站
            //this.sdsiteDocument = new SdsiteXmlDocument(sdPath);
            //this.sdsiteDocument.Load();
            //buildFile = new BuildFile();
            //buildFile.SdsiteDocument = this.sdsiteDocument;
            //buildFile.BuildFilePath = partPath;
            //FolderXmlElement channelRootEle = this.sdsiteDocument.RootChannel;

            try
            {
                SearchAndProcess(channelRootEle);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionService.WriteExceptionLog(Ex);
                return false;
            }
        }

        private void SearchAndProcess(FolderXmlElement channelRootEle)
        {
            foreach (AnyXmlElement anyXmlEle in channelRootEle)
            {
                 SimpleExIndexXmlElement simpleEle = anyXmlEle as SimpleExIndexXmlElement;

                 switch (simpleEle.DataType )
                 {
                     case DataType.Resources :
                         //直接将资源文件移动与此; 还不够妥当todo:
                         if (simpleEle.IsModified)
                         {
                             DirectoryInfo dic = new DirectoryInfo(resStartPath);
                             dic.MoveTo(resToPath);
                         }
                         break;
                     case DataType.TmpltFolder:
                     case DataType.Folder:
                     case DataType.Channel:
                         SearchAndProcess((FolderXmlElement)simpleEle);
                         break;
                     case DataType.Tmplt:
                         buildFile.BuildTmpltFile(simpleEle);
                         break;
                     case DataType.Page:
                         buildFile.BuildPageFile(simpleEle);
                         break;
                 }
            }
        }//private

    }
}
