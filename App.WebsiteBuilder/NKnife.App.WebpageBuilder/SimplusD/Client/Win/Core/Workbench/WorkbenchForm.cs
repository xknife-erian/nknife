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

        #region �ڲ�����

        /// <summary>
        /// �������ɼ�״̬
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
                    //TODO:����ҳ���һ Lisuye
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
        /// ��ȡ���·��
        /// </summary>
        static string GetResourcePath(string id)
        {
            FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)Service.Sdsite.CurrentDocument.GetElementById(id);
            return fileEle.RelativeFilePath;
        }

        /// <summary>
        /// ��ȡ����·��
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
        /// ��srcFile������resourcePathָ������Դ�ļ�����,������Դ�ļ�ID
        /// </summary>
        static string ImportResourceFile(string folderId, string srcFile)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            FileSimpleExXmlElement fileEle = doc.CreateFileElement(folderId, srcFile);
            return fileEle.Id;
        }

        /// <summary>
        /// ��srcFile������System��Դ�ļ�����,������Դ�ļ�ID
        /// </summary>
        static string ImportResourceFile(string srcFile)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            FileSimpleExXmlElement fileEle = doc.CreateFileElement(doc.Resources.SystemFolder, srcFile);
            return fileEle.Id;
        }

        static void ShowDialogForCreateProject()
        {
            ///�����Ŀ���ڴ�״̬�����ȹر���Ŀ
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
        /// �򿪴�������վ�Ĵ���
        /// </summary>
        private static void OpenCreateNewProjectForm()
        {

            ///�򿪴�����վ�Ĵ���
            CreateNewProjectForm newProjectForm = new CreateNewProjectForm();
            newProjectForm.ShowDialog();

            ///���ѡ��ġ�ʹ���򵼡����򵯳���
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
            dlg.Filter = "SimplusD��Ŀ�ļ�(*.sdsite)|*.sdsite";
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
        /// һ��ҳ��ķ���
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
                Debug.Fail("��Formδʵ��IDockContent�ӿڡ�");
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

        #region public����

        public void HideAllPad()
        {
            if (!_isHideAllPad)
            {
                _isHideAllPad = true;

                //�������Ŀɼ�״̬
                UpdatePagState();

                //ֻ�б����ǿɼ��ģ��������� 
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

                //ֻ�б����ǿɼ��ģ���������
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

            //todo:���ҽ������δ����
            //ResultsPad result = new ResultsPad();
            //result.Show(MainDockPanel, DockState.DockBottom);
        }

        #endregion

        #region override�������������¼�

        protected override void OnLoad(EventArgs e)
        {
            if (File.Exists(PathService.Config_LayoutConfig))
            {
                if (this.MainDockPanel.Contents.Count > 0)
                {
                    throw new Exception("�ؼ�DockPanel�ѳ�ʼ����");
                }
                try
                {
                    this.MainDockPanel.LoadFromXml(PathService.Config_LayoutConfig, new DeserializeDockContent(DockLoadHandler));
                }
                catch (Exception)
                {
                    //��������쳣,ɾ��ԭ�����ļ�
                    File.Delete(PathService.Config_LayoutConfig);
                    ResetAllPad();
                }
            }
            else
            {
                ResetAllPad();
            }

            base.OnLoad(e);

            ///Ŀǰ���������������������Ŀ
            if (!string.IsNullOrEmpty(_willOpenFile))
            {
                string fileName = _willOpenFile;
                string extension = Path.GetExtension(fileName);

                switch (extension.ToLower())
                {
                    case Utility.Const.SdsiteFileExt:
                        ///����Ŀ
                        Service.Project.OpenProject(fileName);
                        break;

                    case Utility.Const.TmpltFileExt:
                        ///��ģ��
                        MessageBox.Show("Ŀǰ�����ܵ�����ģ�塣", "SimplusD!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case Utility.Const.PageFileExt:
                        ///��ҳ��
                        MessageBox.Show("Ŀǰ�����ܵ�����ҳ�档","SimplusD!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                ///����ѡ������Ӧ����
                switch (SoftwareOption.General.StartInterfaceState)
                {
                    case "loadRecentSite":
                        ///����������ص���վ
                        RecentFileInfo[] recentFiles = Service.RecentFiles.GetFiles(Service.RecentFiles.RecentOpenProjects);
                        if (recentFiles.Length != 0)
                        {
                            Service.Project.OpenProject(recentFiles[0].FilePath);
                        }
                        break;
                    case "showNewForm":
                        ///��ʾ�½���Ŀ�Ի���
                        OpenCreateNewProjectForm();
                        break;
                    case "showOpenForm":
                        ///��ʾ����Ŀ�Ի���
                        ShowDialogForOpenProject();
                        break;
                    case "showNothingEnvi":
                        ///��ʾ�ջ���  ��������
                        
                        break;
                    case "showStartPage":
                        ///����ʼҳ
                        Service.Workbench.OpenWorkDocument(WorkDocumentType.StartupPage, Service.SiteDataManager.StartupPageId);
                        break;
                    default:
                        break;
                }
                
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //����Ctrl+Tab��
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
            ///��������ϽǵĲ���йر�ʱ,Ӧ�ùر���Ŀ
            if (m.Msg == 16)
            {
                ///�����Ŀ�Ǵ�״̬����ر���Ŀ
                if (Service.Project.IsProjectOpened)
                {
                    Service.Project.CloseProject();

                    ///�����Ȼ���ڴ�״̬�����ʾ����"ȡ��"
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
            ///�����Ŀδ�رգ����ȹر���Ŀ
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
            ///��ʼ����������
            TreePad.TreePadSingle().InitializeSiteTreeData();
            TmpltViewPad.TreePadSingle().InitializeSiteTreeData();

            ///�������Ľڵ��
            TreePad.TreePadSingle().TreeViewExPad.MyTree.OpenItems =
                Service.Sdsite.DesignDataDocument.GetTreeOpenItems();

            MainForm.Text = Path.GetFileNameWithoutExtension(Service.Project.SdsiteMainFilePath) + " - " +
                StringParserService.Parse("${res:SimplusD.name}");

            Service.Sdsite.SdsiteClosing += new EventHandler(SdsiteXmlDocument_SdsiteClosing);
        }

        static void SdsiteXmlDocument_SdsiteClosing(object sender, EventArgs e)
        {
            ///�������ڵ�Ĵ�״̬
            Service.Sdsite.DesignDataDocument.SetTreeOpenItems(TreePad.TreePadSingle().TreeViewExPad.MyTree.OpenItems);

            Service.Sdsite.SdsiteClosing -= new EventHandler(SdsiteXmlDocument_SdsiteClosing);
        }

        static void ProjectService_ProjectClosing(object sender, EventArgs e)
        {
        }

        static void ProjectService_ProjectClosed(object sender, EventArgs e)
        {
            Service.Sdsite.CloseSdsite();

            ///�ͷ���������
            TreePad.TreePadSingle().TreeViewExPad.UnloadTreeData();

            ///�ͷ�ģ����ͼ������ ADD BY fenggy on 2008��6��2��
            TmpltViewPad.TreePadSingle().tmpltTreeViewExPad.UnloadTreeData();

            MainForm.Text = StringParserService.Parse("${res:SimplusD.name}");
        }

        void MainDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            ///����Service.Workbench���ֵ
            if (this.ActiveView == null)
            {
                InternalService.SetActiveWorkDocument(WorkDocumentType.None, "", null);
            }
            else
            {
                InternalService.SetActiveWorkDocument(this.ActiveView.WorkDocumentType, this.ActiveView.Id, this.ActiveView);
            }

            ///����Service.Workbench��ActiveWorkDocumentChanged�¼�
            InternalService.OnActiveWorkDocumentChanged(new EventArgs<Form>(this.ActiveView));

            ///�����������
            MainPropertyPad.RefreshAutoPanel();

            ///����Ԥ��������
            MainPreviewPad.RefreshWebBrowser();
        }

        void IGetProperties_PropertiesChanged(object sender, EventArgs e)
        {
            
        }

        void igetprops_PropertiesChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region �Զ�����¼�

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
