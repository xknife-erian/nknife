using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    public partial class CreateNewProjectForm : BaseForm
    {
        public CreateNewProjectForm()
        {
            InitializeComponent();
            //this.ShowStatusBar = false;

            if (!this.DesignMode)
            {
                ///添加创建网站项目的路径
                this.pathTBox.BeginUpdate();
                RecentFileInfo[] list = Service.RecentFiles.GetFiles("recentCreateProjectFolder");
                foreach (RecentFileInfo recent in list)
                {
                    this.pathTBox.Items.Add(recent.FilePath);
                }
                if (pathTBox.Items.Count > 0)
                {
                    pathTBox.SelectedIndex = 0;
                }
                else
                {
                    string defaultSitesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        "SimplusD Sites");
                    if (!Directory.Exists(defaultSitesPath))
                    {
                        Directory.CreateDirectory(defaultSitesPath);
                    }

                    this.pathTBox.Text = defaultSitesPath;
                }
                this.pathTBox.EndUpdate();

                this.selectListView.SmallImageList = new ImageList();
                this.selectListView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
                this.selectListView.SmallImageList.ImageSize = new Size(16, 16);

                this.selectListView.LargeImageList = new ImageList();
                this.selectListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
                this.selectListView.LargeImageList.ImageSize = new Size(32, 32);

                TreeNode rootNode = new TreeNode(StringParserService.Parse("${res:SimplusD.all}"));
                rootNode.Expand();
                siteTypeTreeView.Nodes.Add(rootNode);

                ///读取配置文件以获取可以创建的类型
                XmlReader xmlReader = XmlReader.Create(PathService.CL_SiteType);
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "item")
                    {
                        TreeNode treeNode = new TreeNode(xmlReader.GetAttribute("text"));
                        rootNode.Nodes.Add(treeNode);
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "basetype")
                    {
                        SiteType siteType = new BaseSiteType(xmlReader.GetAttribute("name"),
                            xmlReader.GetAttribute("text"), xmlReader.GetAttribute("imageKey"));

                        ///初始化ListView
                        ListViewItem site = new ListViewItem(siteType.Text);
                        site.Selected = (xmlReader.GetAttribute("default") == "true");
                        if (!this.selectListView.LargeImageList.Images.ContainsKey(siteType.ImageKey))
                        {
                            this.selectListView.LargeImageList.Images.Add(siteType.ImageKey,
                                ResourceService.GetResourceImage(siteType.ImageKey));
                            this.selectListView.SmallImageList.Images.Add(siteType.ImageKey,
                                ResourceService.GetResourceImage(siteType.ImageKey));
                        }
                        site.ImageKey = siteType.ImageKey;
                        //site.ImageKey = "main.newproject.emptyproject";
                        site.Tag = siteType;
                        site.Group = this.selectListView.Groups[0];
                        this.selectListView.Items.Add(site);
                    }
                }
                xmlReader.Close();
            }
        }

        string _path;
        public string ProjectPath
        {
            get { return _path; }
        }

        string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
        }

        private string _projectFile;

        public string ProjectFile
        {
            get { return _projectFile; }
        }

        private bool _isShowGuide = SoftwareOption.Site.UseSiteGuide ;
        public bool IsShowGuide
        {
            get { return _isShowGuide; }
        }

        public bool IsShowIcon
        {
            get { return SoftwareOption.Site.ShowSmallIcon ; }
        }

        private void browserBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            dlg.SelectedPath = pathTBox.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pathTBox.Text = dlg.SelectedPath;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            _path = pathTBox.Text;
            _projectName = txtProjectName.Text;

            string strCaption = StringParserService.Parse("${res:SimplusD.name}");
            ///检查输入值
            if (string.IsNullOrEmpty(txtProjectName.Text.Trim()))
            {
                MessageBox.Show("项目名不能为空。", strCaption,MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProjectName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(pathTBox.Text.Trim()))
            {
                MessageBox.Show("路径不能为空。");
                pathTBox.Focus();
                return;
            }
            if (pathTBox.Text.IndexOf(":") == -1)
            {
                MessageBox.Show("项目路径格式不正确：" + pathTBox.Text, strCaption,MessageBoxButtons.OK, MessageBoxIcon.Error);
                pathTBox.Focus();
                pathTBox.Select();
                return;
            }
            string strRootPath = Directory.GetDirectoryRoot(pathTBox.Text);
            if (!Directory.Exists(strRootPath))
            {
                MessageBox.Show("所指定的位置在无效或只读的磁盘上，或者包含为系统保留的设备名", strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (Directory.Exists(pathTBox.Text))
                {
                    Directory.CreateDirectory(pathTBox.Text.Trim());
                }
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("项目路径格式不正确：" + pathTBox.Text, strCaption);
                pathTBox.Focus();
                pathTBox.Select();
                return;
            }
            catch (Exception ex)
            {
                Service.Exception.ShowException(ex);
                pathTBox.Focus();
                pathTBox.Select();
                return;
            }
            if (File.Exists(
                Path.Combine(Path.Combine(pathTBox.Text.Trim(),txtProjectName.Text.Trim()), txtProjectName.Text.Trim() + ".sdsite")))
            {
                MessageBox.Show("项目已存在。", strCaption);
                txtProjectName.Focus();
                txtProjectName.Select();
                return;
            }

            if (this.selectListView.SelectedItems.Count > 0)
            {
                ((SiteType)this.selectListView.SelectedItems[0].Tag).GetSiteCreator().
                    CreateSite(pathTBox.Text, txtProjectName.Text);
                _projectFile = Path.Combine(pathTBox.Text, txtProjectName.Text + @"\" + txtProjectName.Text + ".sdsite");
                this.DialogResult = DialogResult.OK;
                _isShowGuide = this.isStartupSiteWiz.Checked;
                this.Close();

                ///记录下“网站路径”
                Service.RecentFiles.AddFilePath("recentCreateProjectFolder", pathTBox.Text);

                ///保存当前的图标显示状态
                SoftwareOption.Site.ShowSmallIcon = smallIconToolStripButton.Checked;
                SoftwareOption.Site.UseSiteGuide = isStartupSiteWiz.Checked;
                SoftwareOption.Save();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            largeIconToolStripButton.Image = ResourceService.GetResourceImage("main.newproject.largeicon");
            smallIconToolStripButton.Image = ResourceService.GetResourceImage("main.newproject.smallicon");

            //largeIconToolStripButton.ToolTipText = GetTextResource("largeIconTitle");
            //smallIconToolStripButton.ToolTipText = GetTextResource("smallIconTitle");

            txtProjectName.Focus();
            txtProjectName.SelectAll();
            //selectListView.LargeImageList = selectListView.SmallImageList = ResourceService.MainImageList;
            this.isStartupSiteWiz.Checked = _isShowGuide;
            if (IsShowIcon)
            {
                smallIconToolStripButton.Checked = true;
                largeIconToolStripButton.Checked  = false;
                this.selectListView.View = View.SmallIcon ;
            }
            else
            {
                largeIconToolStripButton.Checked = true;
                smallIconToolStripButton.Checked = false;
                this.selectListView.View = View.LargeIcon ;
            }
        }

        private void largeIconToolStripButton_Click(object sender, EventArgs e)
        {
            smallIconToolStripButton.Checked = false;
            largeIconToolStripButton.Checked = true;

            this.selectListView.View = View.LargeIcon;
        }

        private void smallIconToolStripButton_Click(object sender, EventArgs e)
        {
            largeIconToolStripButton.Checked = false;
            smallIconToolStripButton.Checked = true;

            this.selectListView.View = View.SmallIcon;
        }

        bool isEnabled = true;
        private void selectListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectListView.SelectedIndices.Count == 0)
            {
                Utility.Form.SetEnabled(bottomPanel, false, cancelBtn);
                isEnabled = false;
            }
            else if (!isEnabled)
            {
                Utility.Form.SetEnabled(bottomPanel, true, cancelBtn);
            }
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            okBtn.Enabled = (txtProjectName.Text.Trim().Length > 0 && pathTBox.Text.Trim().Length > 0) ? true : false;
        }
        private void CreateNewProjectForm_Load(object sender, EventArgs e)
        {
            okBtn.Enabled = false;
        }
   }
}