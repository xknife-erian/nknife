using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class Remote
        {
            static int ThisMajorVersion = 1;
            static int ThisMinorVersion = 1;
            /// <summary>
            /// 检查SimplusD的版本更新，若有更新，则下载
            /// </summary>
            static public void CheckUpdate(bool display)
            {
                //下载最新版本文件，查看是否有变化
                WebClient client = new WebClient();
                byte[] downloadData = client.DownloadData(Service.Property.Properties.GetProperty("checkUpdateAddress").ToString() + "LastVersionList.xml");
                string downloadStr = Utility.Convert.BytesToString(downloadData, Encoding.UTF8);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(downloadStr);

                int lastMajorVersion = int.Parse(doc.DocumentElement.Attributes["lastMajorVersion"].Value);
                int lastMinorVersion = int.Parse(doc.DocumentElement.Attributes["lastMinorVersion"].Value);

                //比较最新版本和当前版本
                if (lastMajorVersion == ThisMajorVersion && lastMinorVersion == ThisMinorVersion)
                {
                    if (display)
                    {
                        MessageBox.Show("您使用的已是最新版本！");
                    }
                    return;
                }

                List<string> needUpdateFileList = new List<string>();
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        int num = int.Parse(node.Attributes["num"].Value);

                        //如果版本号不大于当前版本，不处理
                        if (num <= ThisMinorVersion)
                        {
                            continue;
                        }

                        //版本号大于当前版本的，保存需下载的文件列表
                        string strFileList = node.FirstChild.Value;
                        string[] fileList = strFileList.Split(',');
                        foreach (string fileName in fileList)
                        {
                            if (!needUpdateFileList.Contains(fileName))
                            {
                                needUpdateFileList.Add(fileName);
                            }
                        }
                    }
                }

                //更新文件列表，下载到tempforupdate
                UpdateFileList(needUpdateFileList.ToArray());

                if (display)
                {
                    MessageBox.Show("更新完成！");
                }
            }

            static void UpdateFileList(string[] fileList)
            {
                WebClient client = new WebClient();
                try
                {
                    foreach (string fileName in fileList)
                    {
                        string targetPath = Path.Combine(PathService.TempForUpdate_Folder, fileName);
                        Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                        client.DownloadFile(Service.Property.Properties.GetProperty("checkUpdateAddress").ToString() + fileName,
                            targetPath);
                    }
                }
                //catch (WebException ex)
                //{
                //    Service.Exception.ShowException(ex);
                //}
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}