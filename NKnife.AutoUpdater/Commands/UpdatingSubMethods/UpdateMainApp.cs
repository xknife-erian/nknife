using System;
using System.IO;
using System.Xml;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;
using NKnife.Compress;

namespace NKnife.AutoUpdater.Commands.UpdatingSubMethods
{
    /// <summary>主程序的更新
    /// </summary>
    internal class UpdateMainApp
    {
        public bool Run(IUpdaterFileVerify fileVerify, XmlNode indexXml, bool isDebug = false, FileInfo debugFile = null)
        {
            string mainAppfile;
            if (fileVerify.VerifyMainApplication(indexXml, out mainAppfile))
            {
                Logger.WriteLine("探索到主程序需要更新。");
                //下载“自已”
                FileInfo self = null;
                try
                {
                    self = !isDebug ? Currents.Me.FileGetter.GetTargetFile(mainAppfile) : debugFile;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(string.Format("主程序压缩文件{0}下载未成功。", mainAppfile), e);
                    return false;
                }
                if (self == null)
                {
                    Logger.WriteLine(string.Format("主程序压缩文件{0}下载未成功。", mainAppfile));
                    return false;
                }
                //解压自己
                if (!string.IsNullOrWhiteSpace(self.Extension))
                {
                    if (self.Extension.ToLower() == ".zip")
                    {
                        string outDir = Path.GetDirectoryName(Currents.Me.CallExecuting);
                        Logger.WriteLine("开始解压主程序压缩文件");
                        CommandRoute.WaitTimeout = CommandRoute.WaitTimeout + (int) (self.Length/2000);
                        try
                        {
                            ZipUtil.UnZipFiles(self.FullName, outDir, "", true);
                        }
                        catch (Exception e)
                        {
                            Logger.WriteLine("解压压缩文件异常:", e);
                        }
                        Logger.WriteLine("主程序压缩文件解压完成");
                    }
                }
            }
            else
            {
                Logger.WriteLine("探索到主程序不需要更新。");
            }
            return true;
        }
    }
}