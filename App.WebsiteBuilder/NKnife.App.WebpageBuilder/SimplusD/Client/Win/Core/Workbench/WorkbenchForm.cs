using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using System.Xml;
using Jeelu.SimplusD.Client.Win.Internal;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class WorkbenchForm : Form
    {
        private WorkbenchForm()
        {
            MainTmpltViewPad = TmpltViewPad.TreePadSingle();
            this.Controls.Add(MainDockPanel);
            InitializeComponent();
            MainDockPanel.ShowDocumentIcon = true;
            this.Icon = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
        }

        #region 内部方法

        /// <summary>
        /// 更新面板可见状态
        /// </summary>
        void UpdatePagState()
        {
            _propertyPadVisible = MainPropertyPad.DockState != DockState.Hidden;
            _previewPadVisible = MainPreviewPad.DockState != DockState.Hidden;
            _wizardPadVisible = MainWizardPad.DockState != DockState.Hidden;
            _treePadVisible = MainTreePad.DockState != DockState.Hidden;
            _tmpltViewPadVisible = MainTmpltViewPad.DockState != DockState.Hidden;
            _resultPadVisible = MainResultPad.DockState != DockState.Hidden;
        }

        Form OpenWorkDocument(WorkDocumentType type, string id,string ownerId)
        {
            Debug.Assert(!string.IsNullOrEmpty(id));

            Form returnForm = null;
            switch (type)
            {
                case WorkDocumentType.TmpltDesigner:
                    {
                        MdiTmpltDesignForm form = new MdiTmpltDesignForm(id);
                        form.Owner = WorkbenchForm.MainForm;
                        form.Show(MainDockPanel, DockState.Document);
                        returnForm = form;
                        break;
                    }
                case WorkDocumentType.HomePage:
                    {
                        MdiHomePageDesignForm form = new MdiHomePageDesignForm(id);
                        form.Owner = WorkbenchForm.MainForm;
                        form.Show(MainDockPanel, DockState.Document);
                        returnForm = form;
                        break;
                    }
                case WorkDocumentType.HtmlDesigner:
                    {
                        MdiHtmlDesignForm form = new MdiHtmlDesignForm(id);
                        form.Owner = WorkbenchForm.MainForm;
                        form.Show(MainDockPanel, DockState.Document);
                        returnForm = form;
                        break;
                    }
                case WorkDocumentType.SnipDesigner:
                    {
                        MdiSnipDesignerForm form = new MdiSnipDesignerForm(ownerId, id);
                        form.Owner = WorkbenchForm.MainForm;
                        form.Show(MainDockPanel, DockState.Document);
                        returnForm = form;
                        break;
                    }
                case WorkDocumentType.WebBrowser:
                    {
                        returnForm = NavigationUrl(id);
                        break;
                    }
                    //TODO:管理页面合一 Lisuye
                case WorkDocumentType.Manager:
                    {
                        MdiBaseListViewForm form = new MdiBaseListViewForm(id);
                        form.Owner = WorkbenchForm.MainForm;
                        form.Show(WorkbenchForm.MainForm.MainDockPanel, DockState.Document);
                        returnForm = form;
                        break;
                    }
                case WorkDocumentType.Edit:
                    {
                        MdiBaseEditViewForm form = new MdiBaseEditViewForm(id);
                        form.Owner = WorkbenchForm.MainForm;
                        form.Show(WorkbenchForm.MainForm.MainDockPanel, DockState.Document);
                        returnForm = form;
                        break; 
                    }
                case WorkDocumentType.StartupPage:
                    {
                        MdiWelComePageForm form = new MdiWelComePageForm();
                        form.Show(MainDockPanel, DockState.Document);
                        returnForm = form;
                        break;
                    }
                case WorkDocumentType.SiteProperty:
                    {
                        SitePropertyForm form = new SitePropertyForm(id);
                        form.Show(MainDockPanel, DockState.Document);
                        returnForm = form;
                        break;
                    }
                   
                default:
                    break;
            }
            return returnForm;
        }

        Form NavigationUrl(string address)
        {
            MdiWebBrowserViewForm view;
            if (address == StringParserService.Parse("${res:user.login.url}"))
            {
                view = UserLogForm.SinglerForm;
                view.Owner = this;
                view.Show();
            }
            else if (address == StringParserService.Parse("${res:user.register.url}")
                || address == StringParserService.Parse("${res:user.property.url}"))
            {
                view = new UserGeneralForm();
                view.Show(WorkbenchForm.MainForm.MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
            else
            {
                view = new MdiWebBrowserViewForm();
                view.Show(WorkbenchForm.MainForm.MainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
            view.Navigation(address);
            return view;
        }

        IDockContent DockLoadHandler(string persistString)
        {
            if (persistString == typeof(PropertyPad).ToString())
            {
                return MainPropertyPad;
            }
            else if (persistString == typeof(ToolsBoxPad).ToString())
            {
                return MainWizardPad;
            }
            else if (persistString == typeof(ResultsPad).ToString())
            {
                return MainResultPad;
            }
            else if (persistString == typeof(TreePad).ToString())
            {
                return MainTreePad;
            }
            else if (persistString == typeof(PreviewPad).ToString())
            {
                return MainPreviewPad;
            }
            else if (persistString == typeof(TmpltViewPad).ToString())
            {
                return MainTmpltViewPad;
            }

            return null;
        }

        static string SelectResource(MediaFileType type,Form ownerForm)
        {
            //SelectFileForm dlg = new SelectFileForm(type);
            //dlg.BringToFront();
            //if (dlg.ShowDialog(ownerForm) == DialogResult.OK)
            //{
            //    return dlg.FileId;
            //}
            ResourcesManagerForm dlg = new ResourcesManagerForm();
            dlg.BringToFront();
            dlg.ShowDialog(ownerForm);
            return null;
        }
        string SelectImageResource()
        {
            return SelectResource(MediaFileType.Pic, this);
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        static string GetResourcePath(string id)
        {
            FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)Service.Sdsite.CurrentDocument.GetElementById(id);
            return fileEle.RelativeFilePath;
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        static string GetResourceAbsPath(string id)
        {
            FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)Service.Sdsite.CurrentDocument.GetElementById(id);
            return fileEle.AbsoluteFilePath;
        }

        static string GetResourceUrl(string id)
        {
            FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)Service.Sdsite.CurrentDocument.GetElementById(id);
            return fileEle.RelativeUrl;
        }

        /// <summary>
        /// 将srcFile拷贝到resourcePath指定的资源文件夹里,返回资源文件ID
        /// </summary>
        static string ImportResourceFile(string folderId, string srcFile)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            FileSimpleExXmlElement fileEle = doc.CreateFileElement(folderId, srcFile);
            return fileEle.Id;
        }

        /// <summary>
        /// 将srcFile拷贝到System资源文件夹里,返回资源文件ID
        /// </summary>
        static string ImportResourceFile(string srcFile)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            FileSimpleExXmlElement fileEle = doc.CreateFileElement(doc.Resources.SystemFolder, srcFile);
            return fileEle.Id;
        }

        static void ShowDialogForCreateProject()
        {
            ///如果项目处于打开状态，则先关闭项目
            if (Service.Project.IsProjectOpened)
            {
                Service.Project.CloseProject();
                if (Service.Project.IsProjectOpened)
                {
                    return;
                }
            }
            OpenCreateNewProjectForm();

        }
        /// <summary>
        /// 打开创建新网站的窗体
        /// </summary>
        private static void OpenCreateNewProjectForm()
        {

            ///打开创建网站的窗体
            CreateNewProjectForm newProjectForm = new CreateNewProjectForm();
            newProjectForm.ShowDialog();

            ///如果选择的“使用向导”，则弹出向导
            if (!string.IsNullOrEmpty(newProjectForm.ProjectFile))
            {
                Service.Project.OpenProject(newProjectForm.ProjectFile);
                if (newProjectForm.IsShowGuide)
                {
                    SitePropertyForm form = new SitePropertyForm(Guid.NewGuid().ToString());
                    form.Show(WorkbenchForm.MainForm.MainDockPanel, DockState.Document);
                }
            }
        }

        static void ShowDialogForOpenProject()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AddExtension = true;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.DefaultExt = ".sdsite";
            dlg.DereferenceLinks = true;
            dlg.Multiselect = false;
            dlg.Filter = "SimplusD项目文件(*.sdsite)|*.sdsite";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Service.Project.OpenProject(dlg.FileName);
            }
        }

        void SaveAll()
        {
            foreach (IDockContent document in MainForm.MainDockPanel.Documents)
            {
                if (document is BaseEditViewForm)
                {
                    ((BaseEditViewForm)document).Save();
                }
            }
        }

        /// <summary>
        /// 一个页面的发布
        /// </summary>
        string SinglePagePublish()
        {
            string pageName = "";
            switch(Service.Workbench.ActiveWorkDocumentType)
            {
                case WorkDocumentType.HomePage:
                    return  GetPreviewName.GetPageName(Service.Workbench.ActiveId);
                case WorkDocumentType.HtmlDesigner:
                    return  GetPreviewName.GetPageName(Service.Workbench.ActiveId);
                case WorkDocumentType.Edit:
                    return  GetPreviewName.GetPageName(Service.Workbench.ActiveId);
                case WorkDocumentType.TmpltDesigner:
                    return  GetPreviewName.GetTmpltName(Service.Workbench.ActiveId);
                default:
                    return pageName;
            }
        }

        void WindowOpen(string url)
        {
            Process p = new Process();
            p.StartInfo.FileName = url;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }

        bool CheckTreeHasId(string workDocumentId)
        {
            return MainForm.MainTreePad.TreeViewExPad.MyTree.GetElementNode(workDocumentId) != null;
        }

        void GotoTree(string workDocumentId)
        {
            ElementNode node = MainForm.MainTreePad.TreeViewExPad.MyTree.GetElementNode(workDocumentId);
            if (node != null)
            {
                MainForm.MainTreePad.TreeViewExPad.MyTree.CurrentNode = node;
                MainForm.MainTreePad.Activate();
            }
        }

        void ActivateForm(Form form)
        {
            if (form is IDockContent)
            {
                ((IDockContent)form).DockHandler.Activate();
            }
            else
            {
                Debug.Fail("此Form未实现IDockContent接口。");
                form.Activate();
            }
        }

        void ShowLoginForm()
        {
            UserLogForm view = UserLogForm.SinglerForm;
            view.Owner = this;
            view.ShowDialog();
        }

        #endregion

        #region public方法

        public void HideAllPad()
        {
            if (!_isHideAllPad)
            {
                _isHideAllPad = true;

                //更新面板的可见状态
                UpdatePagState();

                //只有本来是可见的，才做处理 
                if (_tmpltViewPadVisible)
                {
                    MainTmpltViewPad.Hide();
                }
                if (_treePadVisible)
                {
                    MainTreePad.Hide();
                }
                if (_propertyPadVisible)
                {
                    MainPropertyPad.Hide();
                }
                if (_resultPadVisible)
                {
                    MainResultPad.Hide();
                }
                if (_wizardPadVisible)
                {
                    MainWizardPad.Hide();
                } 
                if (_previewPadVisible)
                {
                    MainPreviewPad.Hide();
                }
               
            }
        }

        public void DeHideAllPad()
        {
            if (_isHideAllPad)
            {
                _isHideAllPad = false;

                //只有本来是可见的，才做处理
                if (_propertyPadVisible)
                {
                    MainPropertyPad.Show();
                }
                if (_previewPadVisible)
                {
                    MainPreviewPad.Show();
                }
                if (_wizardPadVisible)
                {
                    MainWizardPad.Show();
                }
                if (_resultPadVisible)
                {
                    MainResultPad.Show();
                }
                if (_treePadVisible)
                {
                    MainTreePad.Show();
                }
                if (_tmpltViewPadVisible)
                {
                    MainTmpltViewPad.Show();
                }
            }
        }

        public void ResetAllPad()
        {
            MainWizardPad.Hide();
            MainPropertyPad.Hide();
            MainTreePad.Hide();
            MainPreviewPad.Hide();
            MainTmpltViewPad.Hide();
            MainResultPad.Hide();

            MainWizardPad.Show(MainDockPanel, DockState.DockLeftAutoHide);
            MainPropertyPad.Show(MainDockPanel, DockState.DockBottom);
            MainPreviewPad.Show(MainDockPanel, DockState.DockBottom);
            MainTreePad.Show(MainDockPanel, DockState.DockRight);
            MainTmpltViewPad.Show(MainDockPanel, DockState.DockRight);
            MainResultPad.Show(MainDockPanel, DockState.DockBottom);

            //todo:查找结果窗口未处理
            //ResultsPad result = new ResultsPad();
            //result.Show(MainDockPanel, DockState.DockBottom);
        }

        #endregion

        #region override方法及监听的事件

        protected override void OnLoad(EventArgs e)
        {
            if (File.Exists(PathService.Config_LayoutConfig))
            {
                if (this.MainDockPanel.Contents.Count > 0)
                {
                    throw new Exception("控件DockPanel已初始化。");
                }
                try
                {
                    this.MainDockPanel.LoadFromXml(PathService.Config_LayoutConfig, new DeserializeDockContent(DockLoadHandler));
                }
                catch (Exception)
                {
                    //如果发生异常,删除原配置文件
                    File.Delete(PathService.Config_LayoutConfig);
                    ResetAllPad();
                }
            }
            else
            {
                ResetAllPad();
            }

            base.OnLoad(e);

            ///目前的命令参数都是用来打开项目
            if (!string.IsNullOrEmpty(_willOpenFile))
            {
                string fileName = _willOpenFile;
                string extension = Path.GetExtension(fileName);

                switch (extension.ToLower())
                {
                    case Utility.Const.SdsiteFileExt:
                        ///打开项目
                        Service.Project.OpenProject(fileName);
                        break;

                    case Utility.Const.TmpltFileExt:
                        ///打开模板
                        MessageBox.Show("目前还不能单独打开模板。", "SimplusD!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case Utility.Const.PageFileExt:
                        ///打开页面
                        MessageBox.Show("目前还不能单独打开页面。","SimplusD!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                ///根据选项做相应处理
                switch (SoftwareOption.General.StartInterfaceState)
                {
                    case "loadRecentSite":
                        ///加载最近加载的网站
                        RecentFileInfo[] recentFiles = Service.RecentFiles.GetFiles(Service.RecentFiles.RecentOpenProjects);
                        if (recentFiles.Length != 0)
                        {
                            Service.Project.OpenProject(recentFiles[0].FilePath);
                        }
                        break;
                    case "showNewForm":
                        ///显示新建项目对话框
                        OpenCreateNewProjectForm();
                        break;
                    case "showOpenForm":
                        ///显示打开项目对话框
                        ShowDialogForOpenProject();
                        break;
                    case "showNothingEnvi":
                        ///显示空环境  不做操作
                        
                        break;
                    case "showStartPage":
                        ///打开起始页
                        Service.Workbench.OpenWorkDocument(WorkDocumentType.StartupPage, Service.SiteDataManager.StartupPageId);
                        break;
                    default:
                        break;
                }
                
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //处理Ctrl+Tab键
            if (keyData == (Keys.Tab|Keys.Control))
            {
                _tabNavigationForm.Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            object obj = drgevent.Data.GetData(DataFormats.FileDrop, true);
            if (obj is string[])
            {
                string strFile = ((string[])obj)[0];
                if (strFile.EndsWith(Utility.Const.SdsiteFileExt))
                {
                    drgevent.Effect = DragDropEffects.All;
                }
            }

            base.OnDragEnter(drgevent);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            object obj = drgevent.Data.GetData(DataFormats.FileDrop, true);
            if (obj is string[])
            {
                string strFile = ((string[])obj)[0];
                if (strFile.EndsWith(Utility.Const.SdsiteFileExt))
                {
                    Service.Project.OpenProject(strFile);
                }
            }

            base.OnDragDrop(drgevent);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            ///当点击右上角的叉进行关闭时,应该关闭项目
            if (m.Msg == 16)
            {
                ///如果项目是打开状态，则关闭项目
                if (Service.Project.IsProjectOpened)
                {
                    Service.Project.CloseProject();

                    ///如果仍然处于打开状态，则表示点了"取消"
                    if (Service.Project.IsProjectOpened)
                    {
                        return;
                    }
                }
            }
            else if (m.Msg == Utility.DllImport.WM_WEBVIEWISSTART)
            {
                Service.WebView.WebViewStartuped();
            }
            base.WndProc(ref m);
        }

        protected void WorkbenchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ///如果项目未关闭，则先关闭项目
            if (Service.Project.IsProjectOpened)
            {
                Service.Project.CloseProject();

                if (Service.Project.IsProjectOpened)
                {
                    e.Cancel = true;
                }
            }
        }

        void WorkbenchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnInitialize();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Service.Exception.ShowDefaultException(e.ExceptionObject as Exception);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Service.Exception.ShowDefaultException(e.Exception);
        }

        static void ProjectService_ProjectOpened(object sender, EventArgs e)
        {
            ///初始化树的数据
            TreePad.TreePadSingle().InitializeSiteTreeData();
            TmpltViewPad.TreePadSingle().InitializeSiteTreeData();

            ///设置树的节点打开
            TreePad.TreePadSingle().TreeViewExPad.MyTree.OpenItems =
                Service.Sdsite.DesignDataDocument.GetTreeOpenItems();

            MainForm.Text = Path.GetFileNameWithoutExtension(Service.Project.SdsiteMainFilePath) + " - " +
                StringParserService.Parse("${res:SimplusD.name}");

            Service.Sdsite.SdsiteClosing += new EventHandler(SdsiteXmlDocument_SdsiteClosing);
        }

        static void SdsiteXmlDocument_SdsiteClosing(object sender, EventArgs e)
        {
            ///保存树节点的打开状态
            Service.Sdsite.DesignDataDocument.SetTreeOpenItems(TreePad.TreePadSingle().TreeViewExPad.MyTree.OpenItems);

            Service.Sdsite.SdsiteClosing -= new EventHandler(SdsiteXmlDocument_SdsiteClosing);
        }

        static void ProjectService_ProjectClosing(object sender, EventArgs e)
        {
        }

        static void ProjectService_ProjectClosed(object sender, EventArgs e)
        {
            Service.Sdsite.CloseSdsite();

            ///释放树的数据
            TreePad.TreePadSingle().TreeViewExPad.UnloadTreeData();

            ///释放模板视图树数据 ADD BY fenggy on 2008年6月2日
            TmpltViewPad.TreePadSingle().tmpltTreeViewExPad.UnloadTreeData();

            MainForm.Text = StringParserService.Parse("${res:SimplusD.name}");
        }

        void MainDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            ///设置Service.Workbench里的值
            if (this.ActiveView == null)
            {
                InternalService.SetActiveWorkDocument(WorkDocumentType.None, "", null);
            }
            else
            {
                InternalService.SetActiveWorkDocument(this.ActiveView.WorkDocumentType, this.ActiveView.Id, this.ActiveView);
            }

            ///触发Service.Workbench的ActiveWorkDocumentChanged事件
            InternalService.OnActiveWorkDocumentChanged(new EventArgs<Form>(this.ActiveView));

            ///处理属性面板
            MainPropertyPad.RefreshAutoPanel();

            ///处理预览服务器
            MainPreviewPad.RefreshWebBrowser();
        }

        void IGetProperties_PropertiesChanged(object sender, EventArgs e)
        {
            
        }

        void igetprops_PropertiesChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region 自定义的事件

        protected virtual void OnViewOpened(ViewContentEventArgs e)
        {
            if (ViewOpened != null)
            {
                ViewOpened(this, e);
            }
        }

        protected virtual void OnViewClosed(ViewContentEventArgs e)
        {
            if (ViewClosed != null)
            {
                ViewClosed(this, e);
            }
        }

        protected virtual void OnActiveWorkspaceTypeChanged(WorkspaceTypeEventArgs e)
        {
            if (!Service.Workbench.CloseAllWindowData.ClosingAllWindow && ActiveWorkspaceTypeChanged != null)
            {
                ActiveWorkspaceTypeChanged(this, e);
            }
        }

        public event ViewContentEventHandler ViewOpened;
        public event ViewContentEventHandler ViewClosed;
        public event EventHandler<WorkspaceTypeEventArgs> ActiveWorkspaceTypeChanged;
        public static event EventHandler WorkbenchCreated;
        public static event EventHandler Initialized;
        static void OnWorkbenchCreated()
        {
            if (WorkbenchCreated != null)
            {
                WorkbenchCreated(null, EventArgs.Empty);
            }
        }

        #endregion
    }
}
