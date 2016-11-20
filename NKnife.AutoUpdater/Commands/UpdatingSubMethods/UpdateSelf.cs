using System;
using System.IO;
using System.Xml;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;
using NKnife.Compress;

namespace NKnife.AutoUpdater.Commands.UpdatingSubMethods
{
    /// <summary>更新器的自我更新
    /// </summary>
    internal class UpdateSelf
    {
        public bool Run(IUpdaterFileVerify fileVerify, XmlNode indexXml, bool isDebug = false, FileInfo debugFile = null)
        {
            string updaterFileName;
            if (fileVerify.VerifyUpdater(indexXml, out updaterFileName))
            {
                Logger.WriteLine("探索到更新器自我需要更新。");
                //下载“自已”
                FileInfo self;
                try
                {
                    self = !isDebug ? Currents.Me.FileGetter.GetTargetFile(updaterFileName) : debugFile;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(string.Format("更新器文件{0}下载未成功。", updaterFileName), e);
                    return false;
                }
                if (self == null)
                {
                    Logger.WriteLine(string.Format("更新器文件{0}下载未成功。", updaterFileName));
                    return false;
                }
                //解压自己
                if (!string.IsNullOrWhiteSpace(self.Extension))
                {
                    if (self.Extension.ToLower() == ".zip")
                    {
                        Logger.WriteLine("开始解压更新器压缩包文件");
                        ZipUtil.UnZipFiles(self.FullName, Path.GetDirectoryName(self.FullName), "", true);
                        Logger.WriteLine("解压更新器压缩包文件已完成");
                        self = Currents.Me.GetLocalFile(Currents.UPDATER_FILE_NAME);
                        if (!self.Exists)
                        {
                            Logger.WriteLine(string.Format("更新器压缩包中未找到更新器。"));
                            return false;
                        }
                    }
                }
                new CopySelf().Run(self);
            }
            else
            {
                Logger.WriteLine("探索到更新器自我不需要更新。");
            }
            return true;
        }
    }
}