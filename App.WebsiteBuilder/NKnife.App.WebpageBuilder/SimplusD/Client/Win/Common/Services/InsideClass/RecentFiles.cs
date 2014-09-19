using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        public static class RecentFiles
        {
            public const string RecentOpenProjects = @"recentOpenProjects";
            public const string RecentOpenFiles = @"recentOpenFiles";

            // add by fenggy 2008-06-12 Ϊ�˵õ�����򿪵�ͼƬ�ļ�,�ͱ���ҳ���ļ�
            public const string RecentOpenImageFiles = @"recentOpenImageFiles";
            public const string RecentOpenLocalPageFiles = @"recentOpenLocalPageFiles";
            //-------------------------
            static Dictionary<string, List<RecentFileInfo>> _recentFiles = new Dictionary<string, List<RecentFileInfo>>();
            static string _configFilePath;

            /// <summary>
            /// ������б���ʾ���������
            /// </summary>
            static public int MaxShowCount { get { return SoftwareOption.General.RecentUseItem; } }

            /// <summary>
            /// ������б�ʵ�ʱ�����������
            /// </summary>
            static readonly public int MaxSaveCount = 30;

            static public void Initialize()
            {
                _configFilePath = PathService.Config_RecentFiles;
                XmlDocument config = new XmlDocument();
                if (File.Exists(_configFilePath))
                {
                    try
                    {
                        config.Load(_configFilePath);
                        foreach (XmlNode node in config.DocumentElement.ChildNodes)
                        {
                            List<RecentFileInfo> list = new List<RecentFileInfo>();
                            _recentFiles.Add(node.Name, list);

                            foreach (XmlNode n in node.ChildNodes)
                            {
                                string filePath = n.LastChild.Value;
                                DateTime dt = DateTime.ParseExact(n.Attributes["lastUpdateTime"].Value, "yyyy-MM-dd HH:mm:ss", null);
                                list.Add(new RecentFileInfo(filePath, dt));
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Fail(ex.Message);
                        File.Delete(_configFilePath);
                    }
                }
            }

            //TODO:���:����򿪵��ļ�
            static public RecentFileInfo[] GetFiles(string recentType)
            {
                ///ȡ���������͵��б�(����Ŀǰ��RecentOpenProjects��RecentOpenFiles����)
                List<RecentFileInfo> list;
                if (!_recentFiles.TryGetValue(recentType, out list))
                {
                    list = new List<RecentFileInfo>();
                    _recentFiles.Add(recentType, list);
                }

                ///������󳤶ȹ�������
                int length = Math.Min(list.Count, MaxShowCount);
                RecentFileInfo[] arrs = new RecentFileInfo[length];
                list.CopyTo(0, arrs, 0, arrs.Length);
                return arrs;
            }

            static public void DeleteFilePath(string recentType, string filePath)
            {
                List<RecentFileInfo> list = _recentFiles[recentType];
                for (int i = 0; i < list.Count; i++)
                {
                    RecentFileInfo info = list[i];
                    if (info.FilePath == filePath)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
                Save();
            }

            static public void ClearFilePath(string recentType)
            {
                _recentFiles[recentType].Clear();
                Save();
            }

            /// <summary>
            /// ����ļ�·��
            /// </summary>
            /// <param name="recentType">����������¼�����͡���ʹ�õ�ǰ��ĳ���</param>
            /// <param name="filePath">�ļ�·��</param>
            static public void AddFilePath(string recentType, string filePath)
            {
                List<RecentFileInfo> list;
                if (!_recentFiles.TryGetValue(recentType, out list))
                {
                    list = new List<RecentFileInfo>();
                    _recentFiles.Add(recentType, list);
                }

                ///����һ����������ļ����򲻴���
                if (list.Count != 0 && list[0].FilePath.Equals(filePath,StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }

                ///���б��д��ļ�����ɾ��
                for (int i = 0; i < list.Count; i++)
                {
                    RecentFileInfo info = list[i];
                    if (info.FilePath.Equals(filePath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }

                ///���б��ȴ������ֵ��ɾ�����һ��
                if (list.Count >= MaxSaveCount)
                {
                    list.RemoveRange(MaxSaveCount - 1, list.Count - MaxSaveCount + 1);
                }

                ///���뵽��һ��
                list.Insert(0, new RecentFileInfo(filePath, DateTime.Now));

                Save();
            }

            static void Save()
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8"" ?><files></files>");
                foreach (KeyValuePair<string, List<RecentFileInfo>> kv in _recentFiles)
                {
                    XmlElement typeElement = doc.CreateElement(kv.Key);
                    doc.DocumentElement.AppendChild(typeElement);

                    foreach (RecentFileInfo recent in kv.Value)
                    {
                        XmlElement fileElement = doc.CreateElement("file");
                        fileElement.SetAttribute("lastUpdateTime", recent.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        XmlText text = doc.CreateTextNode(recent.FilePath);

                        fileElement.AppendChild(text);
                        typeElement.AppendChild(fileElement);
                    }
                }

                doc.Save(_configFilePath);

                if (FileChanged != null)
                {
                    FileChanged(null, EventArgs.Empty);
                }
            }

            static public event EventHandler FileChanged;
        }
    }
    public class RecentFileInfo
    {
        readonly public string FilePath;
        public DateTime LastUpdateTime;
        public RecentFileInfo(string filePath, DateTime lastUpdateTime)
        {
            Debug.Assert(!string.IsNullOrEmpty(filePath));

            this.FilePath = filePath;
            this.LastUpdateTime = lastUpdateTime;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            RecentFileInfo recentInfo = (RecentFileInfo)obj;

            return FilePath.Equals(recentInfo.FilePath,StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return FilePath.GetHashCode();
        }
    }
}