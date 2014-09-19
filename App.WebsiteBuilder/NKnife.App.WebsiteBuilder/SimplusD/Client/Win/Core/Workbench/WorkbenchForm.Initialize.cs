using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using Jeelu.Win;
using System.Reflection;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class WorkbenchForm : Form
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();

            //设置属性
            this.Text = StringParserService.Parse("${res:SimplusD.name}");
            this.Name = "WorkbenchForm";
            this.IsMdiContainer = true;
            this.WindowState = defaultWindowState;
            this.KeyPreview = true;
            this.MinimumSize = new Size(640, 480);
            this.AllowDrop = true;
            this.ImeMode = ImeMode.On;

            //TabNavigationForm
            _tabNavigationForm = new TabNavigationForm(this);

            //DockPanel的初始化
            MainDockPanel.Dock = DockStyle.Fill;
            MainDockPanel.DockTopPortion = 150;
            MainDockPanel.DockBottomPortion = 120;
            MainDockPanel.DockLeftPortion = 100;
            MainDockPanel.DockRightPortion = 260;

            this.FormClosing += new FormClosingEventHandler(WorkbenchForm_FormClosing);
            this.FormClosed += new FormClosedEventHandler(WorkbenchForm_FormClosed);

            this.ResumeLayout(false);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize(string sdsite)
        {
            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            _willOpenFile = sdsite;

            CssResources.Initialize();

            //初始化的顺序是有一定规则的
            Service.Util.Initialize();
            Service.Property.Initialize();
            PathService.Initialize(Application.StartupPath);
            Utility.Pinyin.Initialize(Path.Combine(PathService.SoftwarePath, "pinyin.mb"));
            ResourceService.Initialize();
            StringParserService.Initialize(ResourceService.GetResourceText);
            SoftwareOption.Load();
            ResourcesReader.InitializeResources("Configuration", SoftwareOption.General.ApplicationLanguage, null);
            Service.FileBinding.Initialize();
            Service.DesignData.Load(PathService.Config_PadLayout);
            Service.RecentFiles.Initialize();
            Service.SiteDataManager.Initialize();
            Service.ListView.InitColumn();
            SiteResourceService.Initialize(SelectResource, GetResourcePath,GetResourceAbsPath, GetResourceUrl,ImportResourceFile);
            AutoLayoutPanel.Initialize(PathService.CL_DataSources_Folder);

            LayoutConfiguration.LoadLayoutConfiguration();
            _mainForm = new WorkbenchForm();

            CssUtility.Initialize(_mainForm.SelectImageResource);
            ////初始化的顺序是有一定规则的
            Service.User.Initialize(_mainForm.ShowLoginForm);
            Service.WebView.Initialize(_mainForm.SinglePagePublish);
            MessageService.Initialize(_mainForm);
            ToolbarManager.Initialize(_mainForm);
            MenuStripManager.Initialize(_mainForm);
            StatusBarManager.Initialize(_mainForm);
            FindAndReplaceForm.Initialize(_mainForm);
            //ResultsPad.Initialize(_mainForm);
            MenuStateManager.Initialize();

            Service.StatusBar.Initialize(StatusBarManager.CurrentStatusStrip);
            Service.Workbench.Initialize(
                _mainForm,
                _mainForm.OpenWorkDocument,
                _mainForm.NavigationUrl,
                _mainForm.MainTreePad.TreeViewExPad.RefreshSiteTreeData,
                ShowDialogForCreateProject,
                ShowDialogForOpenProject,
                _mainForm.GotoTree,
                _mainForm.ActivateForm);

            ///监听项目的打开事件
            Service.Project.ProjectOpened += new EventHandler(ProjectService_ProjectOpened);
            Service.Project.ProjectClosing += new EventHandler(ProjectService_ProjectClosing);
            Service.Project.ProjectClosed += new EventHandler(ProjectService_ProjectClosed);

            OnWorkbenchCreated();

            _mainForm.OnActiveWorkspaceTypeChanged(new WorkspaceTypeEventArgs(WorkspaceType.Default));
            _mainForm.MainDockPanel.ActiveDocumentChanged += new EventHandler(_mainForm.MainDockPanel_ActiveDocumentChanged);

            if (Initialized != null)
            {
                Initialized(null, EventArgs.Empty);
            }
        }

        public void UnInitialize()
        {
            //做一些清理工作
            DeHideAllPad();

            if (_needSavePadLayout)
            {
                this.MainDockPanel.SaveAsXml(PathService.Config_LayoutConfig);
            }
        }
    }
}
