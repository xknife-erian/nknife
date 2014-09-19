using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;
using Jeelu.SimplusD.Client.Win.Internal;

namespace Jeelu.SimplusD.Client.Win
{
    public static class MenuStripManager
    {
        static WorkbenchForm _mainForm;
        /// <summary>
        /// 菜单项的第一级，如"文件","视图"....
        /// </summary>
        internal static Dictionary<string, ToolStripMenuItem> _peakMenuItem = new Dictionary<string, ToolStripMenuItem>();
        static Dictionary<WorkspaceType, MenuStrip> _allMenuStrip = new Dictionary<WorkspaceType, MenuStrip>();
        /// <summary>
        /// 所有菜单项的集合
        /// </summary>
        internal static Dictionary<string, ToolStripMenuItem> _allMenuItem = new Dictionary<string, ToolStripMenuItem>();

        public static event EventHandler Initialized;

        static public void Initialize(WorkbenchForm mainForm)
        {
            _mainForm = mainForm;

            CreateAllMenuStrip();

            //注册主窗体的ActiveWorkspaceTypeChanged事件
            _mainForm.ActiveWorkspaceTypeChanged += delegate(object sender, WorkspaceTypeEventArgs e)
            {
                ChangeTopMenu(e.ActiveWorkspaceType);
            };

            if (Initialized != null)
            {
                Initialized(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 改变当前菜单系统MenuStrip
        /// </summary>
        static private void ChangeTopMenu(WorkspaceType workspaceType)
        {
            //由于打算一直显示所有菜单，所以这里将不改变----为防需求发生变化，暂不删除
            #region
            workspaceType = WorkspaceType.Default;
            //改变为当前工作区的相关菜单
            MenuStrip newMenuStrip;
            if (!_allMenuStrip.TryGetValue(workspaceType, out newMenuStrip))
            {
                newMenuStrip = CreateMenuStrip(workspaceType);
                _allMenuStrip.Add(workspaceType, newMenuStrip);
            }
            _mainForm.TopMenu = newMenuStrip;
            #endregion
        }

        /// <summary>
        /// 创建所有的菜单项
        /// </summary>
        static void CreateAllMenuStrip()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathService.CL_Menu);
            BuildMenuItems(null, doc.DocumentElement.ChildNodes, null);
        }

        /// <summary>
        /// 根据节点列表创建菜单项,有子节点的亦将创建为子菜单项
        /// </summary>
        static void BuildMenuItems(ToolStripMenuItem parentMenuItem, XmlNodeList nodelist, string inputToolbarOwner)
        {
            if (inputToolbarOwner != null && ToolbarManager.ShoudAddSeparator(inputToolbarOwner))
            {
                ToolbarManager.AddToolStripItem(inputToolbarOwner, new ToolStripSeparator());
            }
            foreach (XmlNode node in nodelist)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    //先处理分隔线
                    if (string.Compare(node.Name, "Separator", true) == 0)
                    {
                        //从父菜单项中添加此菜单项
                        parentMenuItem.DropDownItems.Add(new ToolStripSeparator());
                        continue;
                    }

                    //存储Menu.xml中每项的数据
                    string strText = null;
                    string strHotkey = null;
                    Keys shortcut = Keys.None;
                    string strShortcut = null;
                    bool blMore = false;
                    string icoPath = null;

                    string recent = null;
                    bool isToolbar = false;
                    bool isExpand = false;
                    string strToolbarOwner = null;
                    string strToolbarType = null;
                    string strLink = null;
                    bool isMustOpenSite = false;

                    foreach (XmlAttribute att in node.Attributes)
                    {
                        switch (att.Name.ToLower())
                        {
                            case "text":
                                strText = att.Value;
                                break;
                            case "hotkey":
                                strHotkey = string.IsNullOrEmpty(att.Value) ? "" : "(&" + att.Value + ")";
                                break;
                            case "shortcut":
                                shortcut = ParseShortcut(att.Value);
                                strShortcut = att.Value;
                                break;
                            case "more":
                                blMore = (att.Value == "1");
                                break;
                            case "ico":
                                icoPath = att.Value;
                                break;
                            case "istoolbar":
                                isToolbar = (att.Value == "1");
                                break;
                            case "toolbarowner":
                                strToolbarOwner = att.Value;
                                break;
                            case "toolbartype":
                                strToolbarType = att.Value;
                                break;
                            case "link":
                                strLink = att.Value;
                                break;
                            case "expand":
                                isExpand = (att.Value == "1");
                                break;
                            case "recent":
                                recent = att.Value;
                                break;
                            case "ismustopensite":
                                isMustOpenSite = bool.Parse(att.Value);
                                break;
                            default:
                                throw new Exception("未知的类型:" + att.Name);
                        }
                    }

                    //作为Key值,如:MainMenu.file.open
                    string keyId = GetKeyFullName(node);

                    MyMenuItem item = null;
                    if (isExpand)
                    {
                        switch (keyId)
                        {
                            case "MainMenu.page.expandInsert":
                                item = new ExpandInsertJsMenuItem(strText + strHotkey + (blMore ? "..." : ""), keyId);
                                break;
                            case "MainMenu.file.recentProj":
                                item = new MyMenuItem(strText + strHotkey + (blMore ? "..." : ""), keyId);
                                break;
                            case "MainMenu.page.expandInsertHtml":
                                item = new ExpandInserHtmlMenuItem(strText + strHotkey + (blMore ? "..." : ""), keyId);
                                break;
                            default:
                                throw new Exception("expand项中出现未知的菜单项:" + keyId);
                        }
                    }
                    else if (strLink != null)
                    {
                        //带link项的,表示用浏览器打开一个网页或文件
                        item = new LinkMenuItem(strText + strHotkey + (blMore ? "..." : ""),
                             keyId, strLink);
                    }
                    else if (recent != null)
                    {
                        item = new RecentRecordMenuItem(strText + strHotkey + (blMore ? "..." : ""),
                             keyId, recent);
                    }
                    else
                    {
                        item = new MyMenuItem(strText + strHotkey + (blMore ? "..." : ""),
                             keyId, isMustOpenSite);
                    }
                    item.ShortcutKeys = shortcut;
                    if (isToolbar)
                    {
                        item.ImageKey = keyId;
                    }

                    if (item.KeyId == "MainMenu.view.toolBar")
                    {
                        ToolbarManager.ContextMenuItemsCreated += delegate
                        {
                            item.DropDownItems.AddRange(ToolbarManager.ContextMenuItems);
                        };
                    }

                    //构造工具栏按钮
                    ToolStripItem toolbarItem = null;
                    if (isToolbar)
                    {
                        toolbarItem = new ToolStripButton();
                        toolbarItem.ImageKey = keyId;
                        toolbarItem.ToolTipText = strText + (shortcut == Keys.None ? "" : " (" + strShortcut + ")");
                        toolbarItem.Tag = item;
                        toolbarItem.Click += delegate(object sender, EventArgs e)
                        {
                            //将Toolbar按钮的点击映射到MenuItem中
                            ((sender as ToolStripItem).Tag as ToolStripMenuItem).PerformClick();
                        };

                        item.ToolStripItem = toolbarItem;

                        ToolbarManager.AddToolStripItem(inputToolbarOwner, toolbarItem);
                    }

                    if (parentMenuItem == null)
                    {
                        //添加到1级菜单项的容器中
                        _peakMenuItem.Add(node.Name, item);
                    }
                    else
                    {
                        //从父菜单项中添加此菜单项
                        parentMenuItem.DropDownItems.Add(item);

                        ///设置ImageList，以显示出图标
                        item.Owner.ImageList = ResourceService.MainImageList;
                    }

                    //添加到所有菜单项的容器中
                    _allMenuItem.Add(keyId, item);

                    //添加子菜单项
                    if (node.HasChildNodes)
                    {
                        BuildMenuItems(item, node.ChildNodes, inputToolbarOwner == null ? strToolbarOwner : inputToolbarOwner);
                        //if (parentMenuItem == null)
                        //{
                        //    ToolbarManager.AddToolStripItem(strToolbarOwner, new ToolStripSeparator());
                        //}
                    }
                    else if (item.GetType() == typeof(MyMenuItem) && item.KeyId != "MainMenu.view.toolBar")
                    {
                        item.Click += AllMenuClick;
                    }
                }
            }
        }

        //TODO:菜单的执行事件
        static void AllMenuClick(object sender, EventArgs e)
        {
            MyMenuItem item = sender as MyMenuItem;
            if (item == null) return;

            ///检查其Enabled状态
            if (!MenuStateManager.GetAndSetMenuEnabled(item.KeyId))
            {
                return;
            }

            switch (item.KeyId)
            {
                #region 新建页面
                case "MainMenu.file.newFile.page.index":
                    #region
                    {
                        BaseFolderElementNode rootChannelNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewPage(rootChannelNode.Element, PageType.Home);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.page.general":
                    #region
                    {
                        BaseFolderElementNode rootChannelNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewPage(rootChannelNode.Element, PageType.General);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.page.hr":
                    #region
                    {
                        BaseFolderElementNode rootChannelNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewPage(rootChannelNode.Element, PageType.Hr);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.page.inviteBidding":
                    #region
                    {
                        BaseFolderElementNode rootChannelNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewPage(rootChannelNode.Element, PageType.InviteBidding);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.page.knowledge":
                    #region
                    {
                        BaseFolderElementNode rootChannelNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewPage(rootChannelNode.Element, PageType.Knowledge);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.page.product":
                    #region
                    {
                        BaseFolderElementNode rootChannelNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewPage(rootChannelNode.Element, PageType.Product);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.page.project":
                    #region
                    {
                        BaseFolderElementNode rootChannelNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewPage(rootChannelNode.Element, PageType.Project);
                        break;
                    }
                    #endregion
                #endregion
                #region 新建模板

                case "MainMenu.file.newFile.tmplt.index":
                    #region
                    {
                        BaseFolderElementNode tmpltRootNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode.TmpltRootNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewTmplt(tmpltRootNode.Element,TmpltType.Home);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.tmplt.general":
                    #region
                    {
                        BaseFolderElementNode tmpltRootNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode.TmpltRootNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewTmplt(tmpltRootNode.Element, TmpltType.General);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.tmplt.hr":
                    #region
                    {
                        BaseFolderElementNode tmpltRootNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode.TmpltRootNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewTmplt(tmpltRootNode.Element, TmpltType.Hr);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.tmplt.inviteBidding":
                    #region
                    {
                        BaseFolderElementNode tmpltRootNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode.TmpltRootNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewTmplt(tmpltRootNode.Element, TmpltType.InviteBidding);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.tmplt.knowledge":
                    #region
                    {
                        BaseFolderElementNode tmpltRootNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode.TmpltRootNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewTmplt(tmpltRootNode.Element, TmpltType.Knowledge);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.tmplt.product":
                    #region
                    {
                        BaseFolderElementNode tmpltRootNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode.TmpltRootNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewTmplt(tmpltRootNode.Element, TmpltType.Product);
                        break;
                    }
                    #endregion
                case "MainMenu.file.newFile.tmplt.project":
                    #region
                    {
                        BaseFolderElementNode tmpltRootNode = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.SiteManagerNode.RootChannelNode.TmpltRootNode;
                        WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree.NewTmplt(tmpltRootNode.Element, TmpltType.Project);
                        break;
                    }
                    #endregion
                #endregion
                case "MainMenu.file.new":
                    #region
                    {
                        Service.Workbench.ShowDialogForCreateProject();
                        break;
                    }
                    #endregion
                case "MainMenu.file.open":
                    #region
                    {
                        Service.Workbench.ShowDialogForOpenProject();
                        break;
                    }
                    #endregion
                case "MainMenu.file.close":
                    #region
                    if (WorkbenchForm.MainForm.MainDockPanel.ActiveDocument != null)
                    {
                        WorkbenchForm.MainForm.MainDockPanel.ActiveDocument.DockHandler.Close();
                    }
                    break;
                    #endregion
                case "MainMenu.file.closeProj":
                    #region
                    {
                        Service.Project.CloseProject();
                        break;
                    }
                    #endregion
                case "MainMenu.file.save":
                    #region
                    Save();
                    break;

                    #endregion
                case "MainMenu.file.saveAll":
                    #region
                    SaveAll();
                    break;

                    #endregion
                case "MainMenu.file.pub":
                    #region
                    ///先保存全部
                    SaveAll();

                    ///检查并提示网站是否有主页
                    if (!Service.Sdsite.CurrentDocument.RootChannel.HasEffectiveDefaultPage)
                    {
                        if (MessageService.Show("没有为网站设置主页，发布后的网站将没有主页。\r\n\r\n是否确定现在发布？",
                            MessageBoxButtons.OKCancel)
                            == DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    Publish publish = new Publish();
                    publish.ExecutePublish();
                    break;

                    #endregion
                case "MainMenu.file.opennet":
                    #region
                    string url = @"http://{0}.{1}.{2}";
                    url = string.Format(url, Service.Sdsite.CurrentDocument.SdsiteName,
                        Service.User.UserID, "SimplusD.net");
                    Process.Start(url);
                    break;

                    #endregion
                case "MainMenu.file.preview":
                    #region
                    ///先保存
                    SaveAll();
                    Service.WebView.StartupProcess();
                    break;
                    #endregion
                case "MainMenu.file.exit":
                    #region
                    //System.Windows.Forms.Application.Exit();
                    WorkbenchForm.MainForm.Close();
                    break;

                    #endregion
                case "MainMenu.edit.undo":
                    #region
                    {
                        WorkbenchForm.MainForm.ActiveView.Undo();
                        break;
                    }
                    #endregion
                case "MainMenu.edit.redo":
                    #region
                    {
                        WorkbenchForm.MainForm.ActiveView.Redo();
                        break;
                    }

                    #endregion
                case "MainMenu.edit.find":
                    #region
                    {
                        FindOptions.Singler.Reset();
                        //FindAndReplaceForm.Initialize(WorkbenchForm.MainForm);
                        FindAndReplaceForm findForm = new FindAndReplaceForm(true);
                        findForm.Owner = _mainForm;
                        findForm.Show();
                        break;
                    }
                    #endregion
                case "MainMenu.edit.replace":
                    #region
                    FindOptions.Singler.Reset();
                   // FindAndReplacePad.Initialize(WorkbenchForm.MainForm);
                    FindAndReplaceForm replaceForm = new FindAndReplaceForm(false);
                    replaceForm.Owner = _mainForm;
                    replaceForm.Show();
                    break;

                    #endregion
                case "MainMenu.edit.findNext":
                    #region
                    {
                        FindOptions.Singler.Reset();
                        //FindAndReplacePad.Initialize(WorkbenchForm.MainForm);
                        FindAndReplaceForm findForm = new FindAndReplaceForm(true);
                        findForm.Owner = _mainForm;
                        findForm.Show();
                        break;
                    }

                    #endregion
                case "MainMenu.edit.cut":
                    #region
                    {
                        WorkbenchForm.MainForm.ActiveView.Cut();
                        break;
                    }
                    #endregion
                case "MainMenu.edit.copy":
                    #region
                    {
                        WorkbenchForm.MainForm.ActiveView.Copy();
                        break;
                    }
                    #endregion
                case "MainMenu.edit.paste":
                    #region
                    {
                        WorkbenchForm.MainForm.ActiveView.Paste();
                        break;
                    }
                    #endregion
                case "MainMenu.edit.delete":
                    #region
                    {
                        WorkbenchForm.MainForm.ActiveView.Delete();
                        break;
                    }
                    #endregion
                case "MainMenu.edit.selectAll":
                    #region
                    {
                        WorkbenchForm.MainForm.ActiveView.SelectAll();
                        break;
                    }
                    #endregion
                case "MainMenu.edit.option":
                    #region
                    {
                        SoftwareOptionForm form = new SoftwareOptionForm();
                        form.ShowDialog();
                        break;
                    }
                    #endregion
                case "MainMenu.view.wizard":
                    #region
                    WorkbenchForm.MainForm.DeHideAllPad();
                    WorkbenchForm.MainForm.MainWizardPad.Show();
                    break;

                    #endregion
                case "MainMenu.view.property":
                    #region
                    WorkbenchForm.MainForm.DeHideAllPad();
                    WorkbenchForm.MainForm.MainPropertyPad.Show();
                    break;
                    #endregion
                case "MainMenu.view.result":
                    #region
                    WorkbenchForm.MainForm.DeHideAllPad();
                    WorkbenchForm.MainForm.MainResultPad.Show();
                    break;
                    #endregion
                case "MainMenu.view.siteManager":
                    #region
                    WorkbenchForm.MainForm.DeHideAllPad();
                    WorkbenchForm.MainForm.MainTreePad.Show();
                    break;

                    #endregion
                case "MainMenu.view.previewPad":
                    #region
                    WorkbenchForm.MainForm.DeHideAllPad();
                    WorkbenchForm.MainForm.MainPreviewPad.Show();
                    break;

                    #endregion
                case "MainMenu.view.fullScreenShow":
                    #region
                    if (!WorkbenchForm.MainForm.IsHideAllPad)
                    {
                        WorkbenchForm.MainForm.HideAllPad();
                    }
                    else
                    {
                        WorkbenchForm.MainForm.DeHideAllPad();
                    }
                    break;

                    #endregion
                case "MainMenu.site.Manager":
                    #region
                    {
                        Service.Workbench.OpenWorkDocument(WorkDocumentType.Manager, Service.Sdsite.CurrentDocument.RootChannel.Id);
                        break;
                    }
                    #endregion
                case "MainMenu.site.tagMangager":
                    #region
                    {
                        //SiteTagManagerForm form = new SiteTagManagerForm();
                        //form.ShowDialog();
                        break;
                    }
                    #endregion
                case "MainMenu.site.siteProperty":
                    #region
                    {
                        Service.Workbench.OpenWorkDocument(WorkDocumentType.SiteProperty, Service.Sdsite.CurrentDocument.SdsiteName);
                        break;
                    }
                    #endregion
                case "MainMenu.page.addPage":
                    #region
                    {
                        //MyTreeView myTree = WorkbenchForm.MainForm.MainTreePad.TreeViewExPad.MyTree;
                        //NewPageFormNoChan newPage = new NewPageFormNoChan(PageType.None);
                        //newPage.Show();
                        NewPageForm form = new NewPageForm(null,PageType.None);
                        form.ShowDialog();
                        break;
                    }
                    #endregion
                case "MainMenu.page.contentProperty":
                    #region
                    {
                        string pageId=((MdiHtmlDesignForm)WorkbenchForm.MainForm.ActiveMdiChild).PageId;
                        PagePropertyForm pageTextPropertyForm = new PagePropertyForm(pageId);
                        pageTextPropertyForm.Show();
                        break;
                    }
                #endregion
                case "MainMenu.tmplt.addTmplt":
                    #region
                    {
                        break;
                    }
                    #endregion
                case "MainMenu.report.ADStat":
                    #region
                    #endregion
                case "MainMenu.window.resetWindowLayout":
                    #region
                    {
                        WorkbenchForm.MainForm.ResetAllPad();
                        break;
                    }
                    #endregion
                case "MainMenu.window.closeAllWindow":
                    #region
                    {
                        Service.Workbench.CloseAllForm();
                        break;
                    }
                    #endregion
                case "MainMenu.user.logout":
                    #region
                    {
                        Service.User.Logout();
                        break;
                    }
                    #endregion
                case "MainMenu.help.help":
                    #region
                    Process.Start(Path.Combine(PathService.CHS_Folder,"SimplusD！Help.chm"));
                    break;

                    #endregion
                case "MainMenu.help.checkUpdate":
                    #region
                    Service.Remote.CheckUpdate(true);
                    break;

                    #endregion
                case "MainMenu.help.aboutJeelu":
                    #region
                    //AboutJeelu aboutJL = new AboutJeelu();
                    //aboutJL.ShowDialog();
                    break;

                    #endregion
                case "MainMenu.help.aboutSimplusD":
                    #region
                    SimplusDAboutForm aboutSD = new SimplusDAboutForm();
                    aboutSD.ShowDialog();
                    break;

                    #endregion
                default:
                    Service.Exception.ShowDefaultException(new Exception("未知的KeyId:" + item.KeyId));
                    break;
            }
        }

        static private void Save(IDockContent form)
        {
            if (form is BaseEditViewForm)
            {
                BaseEditViewForm view = (BaseEditViewForm)form;
                if (view.IsModified)
                {
                    view.Save();

                    ///两个文件触发这个事件，还有个地方是EditView.cs
                    InternalService.OnWorkDocumentSaved(
                        new WorkDocumentEventArgs(view.WorkDocumentType, view.Id));
                }
            }
        }
        static private void Save()
        {
            Save(WorkbenchForm.MainForm.MainDockPanel.ActiveDocument);
        }

        static private void SaveAll()
        {
            foreach (IDockContent idock in WorkbenchForm.MainForm.MainDockPanel.Documents)
            {
                if (idock.DockHandler.Form is MdiSnipDesignerForm)
                {
                    Save(idock);
                }
            }

            foreach (IDockContent idock in WorkbenchForm.MainForm.MainDockPanel.Documents)
            {
                if (!(idock.DockHandler.Form is MdiSnipDesignerForm))
                {
                    Save(idock);
                }
            }
        }

        /// <summary>
        /// 根据节点名,获取作为key值的字符串,形式如下:MainMenu.file.new.project
        /// </summary>
        static string GetKeyFullName(XmlNode node)
        {
            if (node.ParentNode == null || node.ParentNode.NodeType == XmlNodeType.Document)
            {
                return node.Name;
            }
            else
            {
                return GetKeyFullName(node.ParentNode) + "." + node.Name;
            }
        }

        static Keys ParseShortcut(string strShortcut)
        {
            if (string.IsNullOrEmpty(strShortcut))
            {
                return Keys.None;
            }

            Keys temp = Keys.None;
            string[] strs = strShortcut.Split('+');
            foreach (string str in strs)
            {
                switch (str)
                {
                    case "Del":
                        temp |= Keys.Delete;
                        break;
                    case "Ctrl":
                        temp |= Keys.Control;
                        break;
                    default:
                        try
                        {
                            temp |= (Keys)Enum.Parse(typeof(Keys), str);
                        }
                        catch (Exception ex)
                        {
                            Service.Exception.ShowException(ex);
                        }
                        break;
                }
            }

            return temp;
        }

        /// <summary>
        /// 创建菜单系统MenuStrip
        /// </summary>
        static private MenuStrip CreateMenuStrip(WorkspaceType workspaceType)
        {
            MenuStrip outMenuStrip = new MenuStrip();
            outMenuStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
            switch (workspaceType)
            {
                case WorkspaceType.Default:
                    foreach (ToolStripMenuItem item in _peakMenuItem.Values)
                    {
                        outMenuStrip.Items.Add(item);
                    }
                    //outMenuStrip.Items.AddRange(new ToolStripItem[] { 
                    //    _peakMenuItem["file"],
                    //    _peakMenuItem["edit"],
                    //    _peakMenuItem["view"],
                    //    _peakMenuItem["site"],
                    //    _peakMenuItem["page"],
                    //    _peakMenuItem["tmplt"],
                    //    _peakMenuItem["report"],
                    //    _peakMenuItem["window"],
                    //    _peakMenuItem["user"],
                    //    _peakMenuItem["community"],
                    //    _peakMenuItem["help"]});
                    //outMenuStrip.MdiWindowListItem = _peakMenuItem["window"];
                    break;
                case WorkspaceType.Content:
                    break;
                case WorkspaceType.Page:
                    break;
                case WorkspaceType.HelpPage:
                    break;
                default:
                    throw new Exception("未知参数:" + workspaceType);
            }

            ///处理“窗口”菜单
            //ToolStripMenuItem windowMenuItem = _peakMenuItem["window"];
            //windowMenuItem.DropDownOpening += new EventHandler(windowMenuItem_DropDownOpening);

            _mainForm.Controls.Add(outMenuStrip);
            outMenuStrip.ImageList = ResourceService.MainImageList;

            return outMenuStrip;
        }
    }
}
