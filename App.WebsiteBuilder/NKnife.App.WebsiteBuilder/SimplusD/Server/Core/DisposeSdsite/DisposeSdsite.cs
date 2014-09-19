using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Server
{
    /// <summary>
    /// 上传成功后，处理生成前的网站
    /// </summary>
    public class DisposeSdsite
    {
        #region　add by zhangling on 2008年6月23日
        public int _includeFileAmount = 0;
        public int _templetAmount = 0;
        public int _channelAmount = 0;
        public int _pageAmount = 0;
        public int _resourceFileAmount = 0;
        public DateTime _publishTime;
        public DateTime _projCreateTime;
        #endregion

        DealWithFileOrFolder _dealwithFileOrFolder;
        public SdsiteXmlDocument SdsiteDocument { get; private set; }

        /// <summary>
        /// 处理网站的主方法
        /// </summary>
        /// <param name="sdPath">.sdsite的文件夹路径如zha\zhangling\projectname</param>
        /// <param name="projName">.sdsite的名projName</param>
        public bool ExecuteSdsite(string partPath, string projName)
        {
            try
            {
                string sdPath = AnyFilePath.GetTempFolderAbsolutePath(partPath) + projName + ".sdsite";
                //打开网站
                SdsiteDocument = new SdsiteXmlDocument(sdPath);
                SdsiteDocument.Load();

                _dealwithFileOrFolder = new DealWithFileOrFolder();
                _dealwithFileOrFolder.SdsitesPath = partPath;

                #region  add by zhangling on 2008年6月23日
                //得到项目的创建时间
                string _createTime = SdsiteDocument.DocumentElement.GetAttribute("createTime");
                DateTime dt;
                if (DateTime.TryParse(_createTime, out dt))
                {
                    _projCreateTime = DateTime.Parse(_createTime);
                }
                _projCreateTime = dt;
                //得到项目的发布时间
                string _pubTime = SdsiteDocument.DocumentElement.GetAttribute("pubTime");
                DateTime _publishDt;
                if (DateTime.TryParse(_pubTime, out _publishDt))
                {
                    _publishTime = DateTime.Parse(_pubTime);
                }
                _publishTime = _publishDt;
                #endregion

                FolderXmlElement channelRootEle = SdsiteDocument.RootChannel;

                //做相应处理
                SearchAndProcess(channelRootEle);

                ///移动.sdsite
                ///modified by zhangling on 2008年6月12日
                string sourceFilePath = AnyFilePath.GetTempFolderAbsolutePath(partPath) + projName + ".sdsite";
                string targetFilePath = AnyFilePath.GetSdsitesFolderAbsolutePath(partPath) + projName + ".sdsite";
                FileService.FileMove(sourceFilePath, targetFilePath);

                //删除时出错
                FolderService.FolderDelete(AnyFilePath.GetTempFolderAbsolutePath(partPath));

                return true;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteExceptionLog(ex);
                return false;
            }
        }

        // add by zhangling on 2008年6月23日 2008年6月26日　加上了判断条件以统计相应的个数
        private void SearchAndProcess(FolderXmlElement folderEle)
        {
            foreach (AnyXmlElement anyXmlEle in folderEle.ChildNodes)
            {
                SimpleExIndexXmlElement simpleEle = anyXmlEle as SimpleExIndexXmlElement;
                if (simpleEle != null)
                {
                    switch (simpleEle.DataType)
                    {
                        case DataType.Channel:
                            if (!simpleEle.IsDeleted)
                            {
                                _channelAmount = _channelAmount + 1;
                                _includeFileAmount = _includeFileAmount + 1;
                            }
                            SearchAndProcess(simpleEle as FolderXmlElement);
                            break;

                        case DataType.Folder:
                            if (simpleEle.FileName == "System" || !simpleEle.IsDeleted)
                            {
                                _includeFileAmount = _includeFileAmount + 1;
                            }
                            SearchAndProcess(simpleEle as FolderXmlElement);
                            break;

                        case DataType.TmpltFolder:
                        case DataType.Resources:
                            _includeFileAmount = _includeFileAmount + 1;
                            SearchAndProcess(simpleEle as FolderXmlElement);
                            break;

                        case DataType.Tmplt:
                            if (!simpleEle.IsDeleted)
                            {
                                _templetAmount = _templetAmount + 1;
                                _includeFileAmount = _includeFileAmount + 1;
                            }
                            _dealwithFileOrFolder.DealWithFile(simpleEle);
                            break;

                        case DataType.Page:
                            if (!simpleEle.IsDeleted)
                            {
                                _pageAmount = _pageAmount + 1;
                                _includeFileAmount = _includeFileAmount + 1;
                            }
                            _dealwithFileOrFolder.DealWithFile(simpleEle);
                            break;

                        case DataType.File:
                            if (!simpleEle.IsDeleted)
                            {
                                _resourceFileAmount = _resourceFileAmount + 1;
                                _includeFileAmount = _includeFileAmount + 1;
                            }
                            _dealwithFileOrFolder.DealWithFile(simpleEle);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}