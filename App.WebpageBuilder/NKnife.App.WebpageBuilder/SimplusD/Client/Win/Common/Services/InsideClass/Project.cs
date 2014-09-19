using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        public static partial class Project
        {
            #region 项目的相关参数
            /// <summary>
            /// 是否处于打开项目状态
            /// </summary>
            static public bool IsProjectOpened
            {
                get { return !string.IsNullOrEmpty(_projectPath); }
            }

            /// <summary>
            /// 项目主管理文件的路径(含项目文件的文件名)
            /// </summary>
            static string _sdsiteMainFilePath;
            /// <summary>
            /// 项目主管理文件的路径(含项目文件的文件名)
            /// </summary>
            static public string SdsiteMainFilePath
            {
                get
                {
                    if (!IsProjectOpened) { throw new System.Exception(_error_NotOpenedProject); }
                    return _sdsiteMainFilePath;
                }
                set { _sdsiteMainFilePath = value; }
            }

            /// <summary>
            /// 项目路径
            /// </summary>
            static string _projectPath;
            /// <summary>
            /// 项目路径
            /// </summary>
            static public string ProjectPath
            {
                get
                {
                    if (string.IsNullOrEmpty(_projectPath)) { throw new System.Exception(_error_NotOpenedProject); }
                    return _projectPath;
                }
            }

            static string _error_NotOpenedProject = "本操作未打开项目。\r\n开发期异常：应处理“未打开项目”的状态，不应暴露给用户。";

            static bool _isModified;
            static public bool IsModified
            {
                get { return _isModified; }
                set { _isModified = value; }
            }

            #endregion

            #region 项目新建，打开，保存，关闭
            /// <summary>
            /// 当项目已打开时发生
            /// </summary>
            static public event EventHandler ProjectOpened;
            /// <summary>
            /// 当项目已关闭时发生
            /// </summary>
            static public event EventHandler ProjectClosed;
            static public event EventHandler ProjectClosing;

            /// <summary>
            /// 打开项目
            /// </summary>
            /// <param name="projectFile">项目文件路径</param>
            static public void OpenProject(string projectFile)
            {
                try
                {
                    ///如果项目是处于打开状态，则先关闭
                    if (IsProjectOpened)
                    {
                        CloseProject();

                        ///若项目仍未关闭，则不做操作(认为是用户点击了取消)
                        if (IsProjectOpened)
                        {
                            return;
                        }
                    }

                    ///检查文件是否存在。不存在则不处理。
                    if (!File.Exists(projectFile))
                    {
                        return;
                    }

                    /// 解析路径
                    _sdsiteMainFilePath = Path.GetFullPath(projectFile);
                    _projectPath = Path.GetDirectoryName(_sdsiteMainFilePath);
                    if (!_projectPath.EndsWith(@"\"))
                    {
                        _projectPath += @"\";
                    }

                    // 打开项目文件
                    //_projectFileStream = File.Open(projectFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    //_projectDoc = new XmlDocument();
                    //_projectDoc.Load(_projectFileStream);

                    //LoadSiteXmlFile();// 打开项目时，载入各个基础的项目XML文件为XmlDocument:deleted:目前在WorkbenchForm里面打开

                    ///检查初始化XmlDocument是否成功
                    if (!Service.Sdsite.CanOpenProject(projectFile, Service.User.UserID))
                    {
                        if (MessageService.Show("请用此项目所属的用户名打开此项目。\r\n\r\n是否现在登陆？", MessageBoxButtons.OKCancel)
                            == DialogResult.Cancel)
                        {
                            _sdsiteMainFilePath = null;
                            _projectPath = null;
                            return;
                        }
                        User.ShowLoginForm();
                    }

                    if (!Service.Sdsite.CanOpenProject(projectFile, Service.User.UserID))
                    {
                        MessageService.Show("请用此项目所属的用户名打开此项目。");
                        _sdsiteMainFilePath = null;
                        _projectPath = null;
                        return;
                    }

                    Service.Sdsite.OpenSdsite(projectFile);

                    if (!Service.Sdsite.IsOpened)
                    {
                        _sdsiteMainFilePath = null;
                        _projectPath = null;
                        return;
                    }

                    PathService.ProjectPath = _projectPath;

                    //Service.ProjectConfig.OpenProject(projectFile);

                    ///载入打开的窗口列表
                    try
                    {
                        Service.Workbench.LoadFormListFromXml(Service.Sdsite.DesignDataDocument.WorkbenchDocuments);
                    }
                    catch { }

                    ///保存进最近打开的网站
                    Service.RecentFiles.AddFilePath(Service.RecentFiles.RecentOpenProjects, _sdsiteMainFilePath);

                    /// 触发ProjectOpened事件
                    if (ProjectOpened != null)
                    {
                        ProjectOpened(null, EventArgs.Empty);
                    }
                }
                catch (System.Exception ex)
                {
                    Service.Exception.ShowException(ex);
                }
            }

            /// <summary>
            /// 保存项目文件
            /// </summary>
            static public void SaveProjectFile()
            {
                ///保存项目的XmlDocument　？？？？？
                //_projectFileStream.Position = 0;
                //_projectFileStream.SetLength(Encoding.UTF8.GetByteCount(_projectDoc.OuterXml));
                //_projectDoc.Save(_projectFileStream);

                ///将IsModified重置为false
                _isModified = false;
            }

            /// <summary>
            /// 关闭项目
            /// </summary>
            static public bool CloseProject()
            {
                ///保存进最近打开的网站
                Service.RecentFiles.AddFilePath(Service.RecentFiles.RecentOpenProjects, _sdsiteMainFilePath);

                ///关闭项目
                //Service.ProjectConfig.SaveProject();

                ///保存打开着的窗口列表
                Service.Workbench.SaveFormListToXml(Service.Sdsite.DesignDataDocument.WorkbenchDocuments);

                if (ProjectClosing != null)
                {
                    ProjectClosing(null, EventArgs.Empty);
                }

                bool isClosed = Service.Workbench.CloseProjectForm();
                if (!isClosed)
                {
                    return false;
                }

                _projectPath = string.Empty;
                _numContent = 0;
                _numPage = 0;
                _numSnip = 0;
                _numTmplt = 0;

                ///触发ProjectClosed事件
                if (ProjectClosed != null)
                {
                    ProjectClosed(null, EventArgs.Empty);
                }

                return true;
            }

            /// <summary>
            /// 创建新的项目文件
            /// </summary>
            static public string CreateProjectFiles(string path, string projectName)
            {
                ///创建文件及文件夹
                string projectPath = Path.Combine(path, projectName);
                string projectSite = Path.Combine(projectPath, projectName + ".sdsite");
                Directory.CreateDirectory(projectPath);

                ///拷贝空项目过来
                string projectFolder = Path.Combine(PathService.CL_BlankProject_Folder, "[projectfolder]");
                string projectFile = Path.Combine(projectFolder, "[project].sdsite");

                Utility.File.DirectoryCopy(projectFolder, projectPath);
                File.Copy(projectFile, projectSite);

                XmlDocument docSdsite = new XmlDocument();
                ///给.sdsite添加一个guid
                docSdsite.Load(projectSite);
                docSdsite.DocumentElement.SetAttribute("id", Guid.NewGuid().ToString("N"));
                docSdsite.Save(projectSite);

                File.SetAttributes(projectSite, FileAttributes.Normal);
                return projectSite;
            }
            #endregion

            #region 项目元素的编号

            static private int _numContent;
            static public int NumContent
            {
                get { return _numContent; }
                set { _numContent = value; }
            }

            static private int _numPage;
            static public int NumPage
            {
                get { return _numPage; }
                set { _numPage = value; }
            }

            static private int _numTmplt;
            static public int NumTmplt
            {
                get { return _numTmplt; }
                set { _numTmplt = value; }
            }

            static private int _numSnip;
            static public int NumSnip
            {
                get { return _numSnip; }
                set { _numSnip = value; }
            }

            #endregion
        }
    }
}