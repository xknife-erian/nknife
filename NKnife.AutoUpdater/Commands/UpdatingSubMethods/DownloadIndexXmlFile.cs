using System;
using System.IO;
using System.Xml;
using NKnife.AutoUpdater.Common;

namespace NKnife.AutoUpdater.Commands.UpdatingSubMethods
{
    internal class DownloadIndexXmlFile
    {
        public static bool Run(out XmlDocument indexXml, out FileInfo indexfile)
        {
            indexXml = new XmlDocument();
            //下载索引文件
            indexfile = Currents.Me.FileGetter.GetUpdaterIndexFile();
            if (indexfile == null)
                return false;
            XmlElement baseEle = null;
            try
            {
                indexXml.Load(indexfile.FullName);
                baseEle = indexXml.DocumentElement;
                if (baseEle != null)
                    Logger.WriteLine("索引文件下载成功。");
            }
            catch (Exception e)
            {
                Logger.WriteLine("索引文件解析异常.{0}", e);
                return false;
            }

            if (baseEle == null)
            {
                Logger.WriteLine("索引文件异常.");
                return false;
            }
            return true;
        }
    }
}