using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;
using NKnife.Adapters;
using NKnife.App.PictureTextPicker.Common;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.App.PictureTextPicker.Common.Entities;
using NKnife.App.PictureTextPicker.Views;
using NKnife.Interface;
using NKnife.Ioc;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainForm : Form
    {
        private readonly ILogger _Logger = LogFactory.GetCurrentClassLogger();

        private readonly string _DockPath = Path.Combine(Application.StartupPath, "dockpanel.config");
        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly DockContent _PictureListView = DI.Get<PictureListView>();
        private readonly DockContent _PropertyGridView = DI.Get<PropertyGridView>();
        private readonly DockContent _ProcessResultView = DI.Get<ProcessResultView>();
        private readonly DockContent _LogView = DI.Get<LogView>();

        private readonly IPictureList _PictureList = DI.Get<IPictureList>();
        private IAppOption _AppOption = DI.Get<IAppOption>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, System.EventArgs e)
        {
            InitializeDockPanel();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _DockPanel.SaveAsXml(_DockPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存Dockpanel配置文件失败，" + ex.Message);
                return;
            }
            base.OnFormClosing(e);
        }

        private void InitializeDockPanel()
        {
            MainToolStripContainer.ContentPanel.Controls.Add(_DockPanel);
            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

            try
            {
                var deserialize = new DeserializeDockContent(GetContentForm);
                _DockPanel.LoadFromXml(_DockPath, deserialize);
            }
            catch (Exception)
            {
                // 配置文件不存在或配置文件有问题时 按系统默认规则加载子窗体
                InitializeDefaultViews();
            }

            _Logger.Info("DockPanel初始化完成");
        }

        private IDockContent GetContentForm(string xml)
        {
            if (xml == typeof(PictureListView).ToString())
                return _PictureListView;
            if (xml == typeof(PropertyGridView).ToString())
                return _PropertyGridView;
            if (xml == typeof(ProcessResultView).ToString())
                return _ProcessResultView;
            if (xml == typeof (LogView).ToString())
                return _LogView;
            return null;
        }

        private void InitializeDefaultViews()
        {
            _PictureListView.Show(_DockPanel, DockState.DockLeft);
            _ProcessResultView.Show(_DockPanel, DockState.DockRight);
            _PropertyGridView.Show(_DockPanel, DockState.DockRight);
            _LogView.Show(_DockPanel,DockState.DockBottom);
        }

        #region 菜单
        private void ExitToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// 打开目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDirectoryToolStripMenuItemClick(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();
            folderDlg.ShowDialog();
            var path = folderDlg.SelectedPath;
            if (!string.IsNullOrEmpty(path))
            {
                _AppOption.SetOption("PictureDirectory", path); //设置图片路径
                if (!CreateThumbNailBackgroundWorker.IsBusy)
                {
                    CreateThumbNailBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("系统繁忙，请稍后···");
                }
            }
           
        }

        private void LoadPicturesInFolders()
        {
            var pictureDirectory = _AppOption.GetOption("PictureDirectory", "");
            var pictureType = _AppOption.GetOption("PictureFileType", "*.jpg");
            var di = new DirectoryInfo(pictureDirectory);
            var lstAll = new List<string>();
            var lst = di.EnumerateFiles(pictureType, SearchOption.AllDirectories).Select(file => file.FullName).ToList();
            lstAll.AddRange(lst);

            _PictureList.AddRange(lstAll);
        }

        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            var optionFrm = new AppOptionSetting();
            optionFrm.ShowDialog();
        }

        private void CreateThumbNailBackgroundWorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string pictureDirectory = _AppOption.GetOption("PictureDirectory","");
            int thumbNailWidth = _AppOption.GetOption("ThumbWidth", 180);
            int thumbNailHeight = _AppOption.GetOption("ThumbHeight", 100);
            string pictureType = _AppOption.GetOption("PictureFileType", "*.jpg");
            bool fixThumbSize = _AppOption.GetOption("FixThumbSize", false);
            string thumbNailDirectory = _AppOption.GetOption("ThumbNailDirectory", "");

            ThumbNailHelper.GetSmallPicListMethod(pictureDirectory, thumbNailDirectory, thumbNailWidth, thumbNailHeight, pictureType, fixThumbSize);
        }

        private void CreateThumbNailBackgroundWorkerRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //加载显示到左侧列表
            LoadPicturesInFolders();
        }
        #endregion

        public void ShowPic(string picName)
        {
            string pictureDirectory = _AppOption.GetOption("PictureDirectory", "");
            var pictureDocument = new PictureFrameDocument
            {
                ImageFileName = Path.Combine(pictureDirectory, picName)
            };

            var pictureDocumentView = new PictureDocumentView(pictureDocument)
            {
                Text = picName
            };
            pictureDocumentView.Show(_DockPanel,DockState.Document);
            pictureDocumentView.Activate();


        }
    }
}
