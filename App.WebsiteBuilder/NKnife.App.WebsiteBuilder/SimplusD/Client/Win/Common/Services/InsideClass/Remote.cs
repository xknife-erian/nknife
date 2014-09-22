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
            /// ���SimplusD�İ汾���£����и��£�������
            /// </summary>
            static public void CheckUpdate(bool display)
            {
                //�������°汾�ļ����鿴�Ƿ��б仯
                WebClient client = new WebClient();
                byte[] downloadData = client.DownloadData(Service.Property.Properties.GetProperty("checkUpdateAddress").ToString() + "LastVersionList.xml");
                string downloadStr = Utility.Convert.BytesToString(downloadData, Encoding.UTF8);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(downloadStr);

                int lastMajorVersion = int.Parse(doc.DocumentElement.Attributes["lastMajorVersion"].Value);
                int lastMinorVersion = int.Parse(doc.DocumentElement.Attributes["lastMinorVersion"].Value);

                //�Ƚ����°汾�͵�ǰ�汾
                if (lastMajorVersion == ThisMajorVersion && lastMinorVersion == ThisMinorVersion)
                {
                    if (display)
                    {
                        MessageBox.Show("��ʹ�õ��������°汾��");
                    }
                    return;
                }

                List<string> needUpdateFileList = new List<string>();
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        int num = int.Parse(node.Attributes["num"].Value);

                        //����汾�Ų����ڵ�ǰ�汾��������
                        if (num <= ThisMinorVersion)
                        {
                            continue;
                        }

                        //�汾�Ŵ��ڵ�ǰ�汾�ģ����������ص��ļ��б�
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

                //�����ļ��б����ص�tempforupdate
                UpdateFileList(needUpdateFileList.ToArray());

                if (display)
                {
                    MessageBox.Show("������ɣ�");
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