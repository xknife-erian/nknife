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

            // add by fenggy 2008-06-12 为了得到最近打开的图片文件,和本地页面文件
            public const string RecentOpenImageFiles = @"recentOpenImageFiles";
            public const string RecentOpenLocalPageFiles = @"recentOpenLocalPageFiles";
            //-------------------------
            static Dictionary<string, List<RecentFileInfo>> _recentFiles = new Dictionary<string, List<RecentFileInfo>>();
            static string _configFilePath;

            /// <summary>
            /// 最近的列表显示的最大数量
            /// </summary>
            static public int MaxShowCount { get { return SoftwareOption.General.RecentUseItem; } }

            /// <summary>
            /// 最近的列表实际保存的最大数量
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

            //TODO:朱才:最近打开的文件
            static public RecentFileInfo[] GetFiles(string recentType)
            {
                ///取出这种类型的列表(类型目前是RecentOpenProjects和RecentOpenFiles两种)
                List<RecentFileInfo> list;
                if (!_recentFiles.TryGetValue(recentType, out list))
                {
                    list = new List<RecentFileInfo>();
                    _recentFiles.Add(recentType, list);
                }

                ///根据最大长度构造数组
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
            /// 添加文件路径
            /// </summary>
            /// <param name="recentType">保存的最近记录的类型。请使用当前类的常量</param>
            /// <param name="filePath">文件路径</param>
            static public void AddFilePath(string recentType, string filePath)
            {
                List<RecentFileInfo> list;
                if (!_recentFiles.TryGetValue(recentType, out list))
                {
                    list = new List<RecentFileInfo>();
                    _recentFiles.Add(recentType, list);
                }

                ///若第一个就是这个文件，则不处理
                if (list.Count != 0 && list[0].FilePath.Equals(filePath,StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }

                ///若列表有此文件，先删除
                for (int i = 0; i < list.Count; i++)
                {
                    RecentFileInfo info = list[i];
                    if (info.FilePath.Equals(filePath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }

                ///若列表长度大于最大值，删除最后一个
                if (list.Count >= MaxSaveCount)
                {
                    list.RemoveRange(MaxSaveCount - 1, list.Count - MaxSaveCount + 1);
                }

                ///插入到第一个
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