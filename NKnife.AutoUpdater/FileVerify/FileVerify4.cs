using System;
using System.Collections.Specialized;
using System.Xml;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;

namespace NKnife.AutoUpdater.FileVerify
{
    [UpdateIndexFile(Version = "4")]
    internal class FileVerify4 : IUpdaterFileVerify
    {
        #region Implementation of IUpdaterFileVerify

        /// <summary>从索引文件中获取文件校验器（C#类）的版本号
        /// </summary>
        public bool VerifyIndexFileVerifyVersion(XmlNode updateIndex, out string version)
        {
            version = string.Empty;
            var doc = updateIndex as XmlDocument;
            if (doc != null && doc.DocumentElement != null)
            {
                version = doc.DocumentElement.GetAttribute("Version");
                return true;
            }
            return false;
        }

        /// <summary>根据指定的XML文档的描述检查自动更新程序是否需要更新
        /// </summary>
        public bool VerifyUpdater(XmlNode updateIndex, out string targetFileName)
        {
            targetFileName = string.Empty;
            if (updateIndex != null)
            {
                //<Updater HasNewUpdater="true" Version="2.1205">
                var element = updateIndex.SelectSingleNode("//Updater") as XmlElement;
                if (element != null)
                {
                    //远端服务器中，即索引文件中表达的最新的更新器的版本
                    Version remoteVersion;
                    if (Version.TryParse(element.GetAttribute("Version"), out remoteVersion))
                    {
                        //如果当前的版本(本程序)小于远端的版本
                        if (Currents.Me.UpdaterVersion < remoteVersion)
                        {
                            targetFileName = element.InnerText.Trim();
                            Logger.WriteLine(string.Format("自动更新程序更新包{0}", targetFileName));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>校验主程序是否需要更新，并返回需要更新的压缩包文件
        /// </summary>
        public bool VerifyMainApplication(XmlNode updateIndex, out string targetFileName)
        {
            targetFileName = string.Empty;
            if (updateIndex != null)
            {
                var currVersion = Currents.Me.CallerVersion;

                //第一步，先判断增量更新包是否在可用范围之内
                // <Incremental MinVersion="5.5.1204.5567" MaxVersion="5.5.1205.6146">
                var increEle = updateIndex.SelectSingleNode("//Incremental") as XmlElement;
                if (increEle != null)
                {
                    Version maxVeriosn;
                    Version minVersion;
                    bool max = Version.TryParse(increEle.GetAttribute("MaxVersion"), out maxVeriosn);
                    bool min = Version.TryParse(increEle.GetAttribute("MinVersion"), out minVersion);
                    if (max && min && currVersion < maxVeriosn && currVersion > minVersion)
                    {
                        targetFileName = increEle.InnerText.Trim();
                        Logger.WriteLine(string.Format("当前{1}版本在{2}与{3}之间,增量更新{0}。", targetFileName, currVersion, minVersion, maxVeriosn), true);
                        return true;
                    }
                    Logger.WriteLine("增量更新包不在可用范围之内");
                }

                //第二步，当增量更新不可用时，先判断全量更新包是否可用
                // <Full Version="5.5.1205.6146">
                var fullEle = updateIndex.SelectSingleNode("//Full") as XmlElement;
                if (fullEle != null)
                {
                    Version fullVeriosn;
                    if (Version.TryParse(fullEle.GetAttribute("Version"), out fullVeriosn))
                    {
                        //如果当前版本小于远端的全量版本
                        if (currVersion < fullVeriosn)
                        {
                            targetFileName = fullEle.InnerText.Trim();
                            Logger.WriteLine(string.Format("当前{1}版本小于{2},全量更新{0}", targetFileName, currVersion, fullVeriosn), true);
                            return true;
                        }
                    }
                    Logger.WriteLine("增量更新不可用，全量更新包不可用");
                }
            }
            return false;
        }

        /// <summary>校验是否有Patch文件(补丁程序)，如果有，返回补丁程序的文件的集合
        /// </summary>
        public bool VerifyPatchs(XmlNode updateIndex, out StringCollection patchs)
        {
            patchs = new StringCollection();
            if (updateIndex != null)
            {
                XmlNodeList nodes = updateIndex.SelectNodes("//Patch");
                if (nodes != null && nodes.Count > 0)
                {
                    var currVersion = Currents.Me.CallerVersion;

                    foreach (XmlNode node in nodes)
                    {
                        if (node != null && node.NodeType == XmlNodeType.Element && node.LocalName == "Patch")
                        {
                            var increEle = (XmlElement) node;
                            Version maxVeriosn;
                            Version minVersion;
                            bool max = Version.TryParse(increEle.GetAttribute("MaxVersion"), out maxVeriosn);
                            bool min = Version.TryParse(increEle.GetAttribute("MinVersion"), out minVersion);
                            if (max && min && currVersion < maxVeriosn && currVersion > minVersion)
                                patchs.Add(node.InnerText);
                        }
                    }
                    if (patchs.Count > 0)
                    {
                        Logger.WriteLine(string.Format("有可用Patch文件{0}个", patchs.Count));
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}