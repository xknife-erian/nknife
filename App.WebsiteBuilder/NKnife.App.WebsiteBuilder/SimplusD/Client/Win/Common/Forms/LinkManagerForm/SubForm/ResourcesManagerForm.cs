using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using CustomControls;
using Jeelu.Win;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{

    public partial class ResourcesManagerForm : BaseForm
    {

        #region 构造函数

        /// <summary>
        /// 链接管理调用的构造函数,不支持右键高级设置
        /// </summary>
        public ResourcesManagerForm()
        {
            InitializeComponent();
            CurrentPageIndex = 1;

            ContextMenuStrip menuStrp = new ContextMenuStrip();
            menuStrp.Items.Add(ResourceService.GetResourceText("ResourceManage.HighSetting"), null, ResourceHighSetting_Click);
            resourceFilesListView.ContextMenuStrip = menuStrp;
            resourceFilesListView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }
        /// <summary>
        /// 普通构造函数,插入资源调用,支持右键高级设置
        /// </summary>
        /// <param name="ele"></param>
        public ResourcesManagerForm(XmlElement ele,bool islink)
        {            
            InitializeComponent();
            CurrentPageIndex = 1;
            
            ContextMenuStrip menuStrp = new ContextMenuStrip();
            menuStrp.Items.Add(ResourceService.GetResourceText("ResourceManage.HighSetting"), null, ResourceHighSetting_Click);
            resourceFilesListView.ContextMenuStrip = menuStrp;
            resourceFilesListView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);

            _XmlElement = ele;
            IsInsertLink = islink;

            if (_XmlElement != null && _XmlElement.Attributes.Count > 0)
            {//通知界面 在链接管理打开RES需定位到该文件上

            }
        }
        #endregion

        #region 字段或属性

        private XmlElement _XmlElement;

        private List<MyListItem> resourceFilePaths = null;
        private List<MyListItem> localFilePaths = null;

        private MyListItem myResourceCurrentItem; //资源当前显示的路径
        private MyListItem myPageCurrentItem; //页面当前显示的路径


        /// <summary>
        /// 当前选项卡索引
        /// </summary>
        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get
            {
                return _currentPageIndex;
            }
            set
            {
                mainTabControl.TabPages.Clear();
                _currentPageIndex = value;
                switch (_currentPageIndex)
                {
                    case 1:
                        {
                            #region 资源文件
                            mainTabControl.TabPages.Add(resourceFilesTabPage);

                            InitHistroyOpenFile(historyResToolStripSplitButton, Service.RecentFiles.RecentOpenImageFiles, HistoryOpenImageFile_Click);
                            //resourceFilesListView.FolderName = Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath;

                            resourceFilePaths = new List<MyListItem>();
                            MyListFolderItem myFolderResouce = new MyListFolderItem(Service.Sdsite.CurrentDocument.Resources, null);
                            myResourceCurrentItem = myFolderResouce;
                            resourceFilePaths.Add(myFolderResouce);
                            resourceFilesListView.MultiSelect = false;
                            resourceFilesListView.SmallImageList = new ImageList();
                            resourceFilesListView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
                            resourceFilesListView.SmallImageList.ImageSize = new Size(16, 16);

                            resourceFilesListView.LargeImageList = new ImageList();
                            resourceFilesListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

                            resourceFilesListView.View = View.LargeIcon; //资源文件初始打开为缩略图
                            myResourceCurrentItem.ListShowView = View.LargeIcon;
                            myResourceCurrentItem.BreviaryMap = true;

                            InitListView(myFolderResouce, resourceFilesListView, _resourceCurrentType);
                            #endregion
                        }
                        break;
                    case 2:
                        {
                            #region 本地页面
                            mainTabControl.TabPages.Add(localpagesTabPage);
                            InitHistroyOpenFile(historyPageToolStripSplitButton, Service.RecentFiles.RecentOpenLocalPageFiles, HistoryOpenImageFile_Click);

                            localFilePaths = new List<MyListItem>();
                            MyListFolderItem myPageResouce = new MyListFolderItem(Service.Sdsite.CurrentDocument.RootChannel, null);
                            myPageResouce.BreviaryMap = false;
                            myPageCurrentItem = myPageResouce;
                            myPageCurrentItem.ListShowView = View.Details;
                            localFilePaths.Add(myPageResouce);
                            localPagesListView.MultiSelect = false;
                            localPagesListView.SmallImageList = new ImageList();
                            localPagesListView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
                            localPagesListView.SmallImageList.ImageSize = new Size(16, 16);

                            localPagesListView.LargeImageList = new ImageList();
                            localPagesListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
                            localPagesListView.LargeImageList.ImageSize = new Size(32, 32);

                            localPagesListView.View = View.Details;
                            InitListView(myPageResouce, localPagesListView, _localPageCurrentType);
                            #endregion
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 插入链接还是插入资源
        /// 插入链接的话，没有高级设置
        /// </summary>
        private bool IsInsertLink
        {
            get;
            set;
        }

        public 
        #endregion

        #region 资源和页面的过滤条件
            //暂时写法
        const string _strAll = "All";
        const string _strImageType = ".jpg,.png,.gif,.bmp";
        
        const string _strFlash = ".swf";
        const string _strAuto = ".mp3,.mdi,.wma,.wav";
        const string _strVido = ".rmvb,.rm,.avi,.wmv";

        const string _strMultimediaType = _strFlash + "," + _strAuto + "," + _strVido;

        string _resourceCurrentType = _strAll;
        string _localPageCurrentType = _strAll;
        #endregion

        #region 资源相关部分

        /// <summary>
        /// 资源的高级设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceHighSetting_Click(object sender, EventArgs e)
        {
            if (resourceFilesListView.SelectedItems.Count != 1) return; //不支持多选

            MyListItem myitem = resourceFilesListView.SelectedItems[0] as MyListItem;
            if (myitem is MyListFileItem)
            {
                BaseForm form = new BaseForm();
                XhtmlTagElement xhtmlElement = null;// new XhtmlTagElement();
                XhtmlSection _section = new XhtmlSection();
                string strHref = "[url: " + ((MyListFileItem)myitem).Element.Id + "]";
                switch (((MyListFileItem)myitem).ItemMediaType)
                {
                    case MediaFileType.None:
                        break;
                    case MediaFileType.Pic:
                        #region 图片
                        {
                            xhtmlElement = _section.CreateXhtmlImg();
                            ((XhtmlTags.Img)xhtmlElement).Src = strHref;
                            form = new InsertPicCodeForm(xhtmlElement, ((MyListFileItem)myitem).Element.AbsoluteFilePath);                            
                            break;
                        }
                        #endregion
                    case MediaFileType.Flash:
                        #region Flash
                        {
                            xhtmlElement = _section.CreateXhtmlFlash();

                            CssSection style = new CssSection();
                            ((XhtmlTags.Flash)xhtmlElement).Builder(style, "", strHref, Xhtml.Align.left, "", -1, -1, "", "", "");
                            form = new InsertFlashCodeForm(xhtmlElement, ((MyListFileItem)myitem).Element.AbsoluteFilePath);
                            
                            break;
                        }
                        #endregion
                    case MediaFileType.Audio:
                        #region Audio
                        {
                            xhtmlElement = _section.CreateXhtmlObject();
                            //((XhtmlTags.Object)xhtmlElement).Src = strHref;
                            form = new InsertAudioCodeForm(xhtmlElement, ((MyListFileItem)myitem).Element.AbsoluteFilePath);
                            break;   
                        }                        
                        #endregion
                    case MediaFileType.Video:
                        #region Video
                        {
                            xhtmlElement = _section.CreateXhtmlObject();
                            //((XhtmlTags.Object)xhtmlElement).Src = strHref;
                            form = new InsertVideoCodeForm(xhtmlElement, ((MyListFileItem)myitem).Element.AbsoluteFilePath);
                            break;
                        }
                        #endregion
                    default:
                        Debug.Fail("未知的type:" + ((MyListFileItem)myitem).ItemMediaType.ToString());
                        break;
                }
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ((MyListFileItem)myitem).XmlAttribute = (XmlElement)xhtmlElement.ToXmlNode();
                }
            }
        }
        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (IsInsertLink || resourceFilesListView.SelectedItems.Count != 1)
            {
                e.Cancel = true;
                return;
            }
            MyListItem myitem = resourceFilesListView.SelectedItems[0] as MyListItem;
            if (!(myitem is MyListFileItem))
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 初始化最近打开的文件
        /// </summary>
        private void InitHistroyOpenFile(ToolStripSplitButton toolStripButton, string RecenType, EventHandler _onclik)
        {
            //打开历史文件,必须根据路径得到她的GUID,如果是资源文件则必须在资源文件夹下,遍历所有的文件,找到
            //和历史文件路径相同的ELEMENT,然后返回GUID
            //也可以在保存的时候,在其后面,在加上别的信息,例如,GUID,总之是为了能快速找到她
            RecentFileInfo[] recentFiles = Service.RecentFiles.GetFiles(RecenType);
            foreach (RecentFileInfo fileInfo in recentFiles)
            {
                toolStripButton.DropDownItems.Add(fileInfo.FilePath, null, _onclik);
            }

        }

        private void HistoryOpenImageFile_Click(object sender, EventArgs e)
        {
           // ((ToolStripMenuItem)sender).Owner.Items.Remove((ToolStripMenuItem)sender);


            // RecentFileInfo[] recentFiles = Service.RecentFiles.GetFiles(Service.RecentFiles.RecentOpenFiles);
            //Service.RecentFiles.AddFilePath(Service.RecentFiles.RecentOpenProjects, _sdsiteMainFilePath);
        }

        private void imageFileFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);
            switch (menuItem.Name)
            {
                case "allFileFilterToolStripMenuItem":
                    _resourceCurrentType = _strAll;
                    break;
                case "imageFileFilterToolStripMenuItem":
                    _resourceCurrentType = _strImageType;
                    break;
                case "medioFileFilterToolStripMenuItem":
                    _resourceCurrentType = _strMultimediaType;
                    break;
                case "documentFileFilterToolStripMenuItem":
                    break;
                case "compressFileFilterToolStripMenuItem":
                    break;

            }
            InitListView(myResourceCurrentItem, resourceFilesListView, _resourceCurrentType);
            SetMenuItemMutex(menuItem);
        }

        /// <summary>
        /// //上一级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parentResToolStripButton_Click(object sender, EventArgs e)
        {
            if (myResourceCurrentItem is MyListFolderItem)
            {
                resourceFilesListView.LargeImageList.Images.Clear();
                resourceFilesListView.SmallImageList.Images.Clear();

                MyListItem myCurrent = (myResourceCurrentItem as MyListFolderItem).FolderItemParant;

                if (!myCurrent.BreviaryMap)
                {
                    resourceFilesListView.LargeImageList.ImageSize = new Size(32, 32); //不是缩略图的恢复大小
                }
                if (myCurrent.FolderItemParant == null)
                {
                    parentResToolStripButton.Enabled = false; //到根了
                    InitListView(resourceFilePaths[0], resourceFilesListView, _resourceCurrentType);
                    myResourceCurrentItem = resourceFilePaths[0];
                }
                else
                {
                    myResourceCurrentItem = myCurrent;
                    InitListView(myCurrent, resourceFilesListView, _resourceCurrentType);
                }
                SetResourceMenuCheckState(myResourceCurrentItem);
            }
        }

        private void largeIconViewResToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);

            resourceFilesListView.LargeImageList.Images.Clear();
            resourceFilesListView.SmallImageList.Images.Clear();

            switch (menuItem.Name)
            {
                case "thumbnailViewResToolStripMenuItem":
                    //显示方式为缩略图
                    myResourceCurrentItem.BreviaryMap = true;
                    myResourceCurrentItem.ListShowView = View.LargeIcon;
                    break;
                case "largeIconViewResToolStripMenuItem":
                    resourceFilesListView.LargeImageList.ImageSize = new Size(32, 32);
                    resourceFilesListView.View = View.LargeIcon;
                    myResourceCurrentItem.BreviaryMap = false;
                    myResourceCurrentItem.ListShowView = View.LargeIcon;
                    break;
                case "smallIconViewResToolStripMenuItem":
                    resourceFilesListView.View = View.SmallIcon;
                    myResourceCurrentItem.ListShowView = View.SmallIcon;
                    break;
                case "listViewResToolStripMenuItem":
                    resourceFilesListView.View = View.List;
                    myResourceCurrentItem.BreviaryMap = false;
                    break;
                case "detailsViewResToolStripMenuItem":
                    resourceFilesListView.View = View.Details;
                    myResourceCurrentItem.BreviaryMap = false;
                    break;
            }
            if (myResourceCurrentItem != null && myResourceCurrentItem is MyListFolderItem && !myResourceCurrentItem.BreviaryMap)
            {//保存该层文件夹下的显示模式
                (myResourceCurrentItem as MyListFolderItem).ListShowView = resourceFilesListView.View;
            }
            //else if (myResourceCurrentItem.BreviaryMap)
            //{//为缩略图模式
            //    resourceFilesListView.Items.Clear();
            //}

            InitListView(myResourceCurrentItem, resourceFilesListView, _resourceCurrentType);

            SetMenuItemMutex(menuItem);
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resourceFilesListView_DoubleClick(object sender, EventArgs e)
        {
            if (resourceFilesListView.SelectedItems.Count > 0)
            {
                MyListItem myitem = resourceFilesListView.SelectedItems[0] as MyListItem;
                if (myitem is MyListFileItem)
                {
                    #region 选择的是文件

                    Service.RecentFiles.AddFilePath(Service.RecentFiles.RecentOpenImageFiles, myitem.Element.AbsoluteFilePath);
                    XmlElement ele = null;
                    if (((MyListFileItem)myitem).XmlAttribute != null)
                    { //进行了高级设置
                        ele = ((MyListFileItem)myitem).XmlAttribute;
                    }
                    else
                    {//没有设置过,设置默认的属性
                        #region 默认属性
                        switch (((MyListFileItem)myitem).ItemMediaType)
                        {
                            case MediaFileType.None:
                                break;
                            case MediaFileType.Pic:
                                ele = (XmlElement)InsertPicCodeForm.GetDefaultPicHtml(myitem.Element.AbsoluteFilePath,myitem.Element.Id).ToXmlNode();
                                break;
                            case MediaFileType.Flash:
                                break;
                            case MediaFileType.Audio:
                                break;
                            case MediaFileType.Video:
                                break;
                            default:
                                Debug.Fail("未知的type:" + ((MyListFileItem)myitem).ItemMediaType.ToString());
                                break;
                        }
                        #endregion
                    }
                    if (ele != null)
                    {
                        foreach (XmlAttribute att in ele.Attributes)
                        {
                            _XmlElement.SetAttribute(att.Name, att.Value);
                        }
                    }
                    this.DialogResult = DialogResult.OK;

                    #endregion
                }
                else
                {
                    #region 选择文件夹

                    MyListItem item = ItemContains(resourceFilePaths, myitem);
                    if (item == null)
                    {
                        resourceFilePaths.Add(myitem); //没有打开过,添加
                    }
                    else
                    {
                        myitem = item;
                    }
                    myResourceCurrentItem = myitem;
                    resourceFilesListView.SmallImageList.Images.Clear();
                    resourceFilesListView.LargeImageList.Images.Clear();
                    InitListView(myResourceCurrentItem, resourceFilesListView, _resourceCurrentType);
                    parentResToolStripButton.Enabled = true;// (myitem.Element.AbsoluteFilePath != Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath);
                    SetResourceMenuCheckState(myResourceCurrentItem);

                    #endregion
                }

            }
        }
        /// <summary>
        /// 根据当前文件夹显示的方式,设置菜单是否选中
        /// </summary>
        private void SetResourceMenuCheckState(MyListItem myitem)
        {
            ToolStripDropDownButton toolStripButton = (ToolStripDropDownButton)resourceFilesToolStrip.Items["viewsResToolStripDropDownButton"];

            string strCheckName = "";
            if (myitem.ListShowView == View.LargeIcon && myitem.BreviaryMap)
            {
                strCheckName = "thumbnailViewResToolStripMenuItem";
            }
            else if (myitem.ListShowView == View.LargeIcon && !myitem.BreviaryMap)
            {
                strCheckName = "largeIconViewResToolStripMenuItem";
            }
            else if (myitem.ListShowView == View.SmallIcon)
            {
                strCheckName = "smallIconViewResToolStripMenuItem";
            }
            else if (myitem.ListShowView == View.List)
            {
                strCheckName = "listViewResToolStripMenuItem";
            }
            else if (myitem.ListShowView == View.Details)
            {
                strCheckName = "detailsViewResToolStripMenuItem";
            }
            foreach (ToolStripItem item in toolStripButton.DropDownItems)
            {
                if (item is ToolStripMenuItem && item.Name.Equals(strCheckName))
                {
                    (item as ToolStripMenuItem).Checked = true;
                }
                else
                {
                    (item as ToolStripMenuItem).Checked = false;
                }
            }
        }

        private void deleteResToolStripButton_Click(object sender, EventArgs e)
        {
            if (resourceFilesListView.SelectedItems.Count <= 0) return;

            if (MessageService.Show("${res:Tree.msg.delete}", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            MyListItem deleItem = resourceFilesListView.SelectedItems[0] as MyListItem;
            doc.DeleteItem(deleItem.Element);
            resourceFilesListView.Items.Remove(deleItem);

        }
        private void resourceFilesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                deleteResToolStripButton_Click(null, EventArgs.Empty);
            }
        }
        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {//导入资源
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            MyListItem importFolder = null;
            string openfileFilter = "";

            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);
            switch (menuItem.Name)
            {
                case "imageToolStripMenuItem":
                    openfileFilter = "PIC files (*.jpg,*.gif)|*.jpg;*.gif;";
                    break;
                case "medioFileToolStripMenuItem":
                    openfileFilter = "Audio files (*.mp3,*.mdi,*.wma,*.wav)|*.mp3;*.mid;*.wma;*.wav;|";
                    openfileFilter += "Media files (*.rmvb,*.rm,*.avi,*.wmv)|*.rmvb;*.rm;*.avi;*.wmv;|";
                    openfileFilter += "Flash files (.swf)|*.swf;";
                    break;
                case "documentFileToolStripMenuItem":
                    openfileFilter = "doc files (*.txt,*.doc)|*.txt;*.doc;";
                    break;
                case "compressFileNewToolStripMenuItem":
                    openfileFilter = "Compress files (*.rar,*.zip)|*.rar;*.zip;";
                    break;
            }

            FormOpenFileDialog OpenFile = new FormOpenFileDialog();
            OpenFile.OpenDialog.Multiselect = true;
            OpenFile.OpenDialog.Filter = openfileFilter;
            if (OpenFile.ShowDialog(this) == DialogResult.OK)
            {
                string[] fileNames = OpenFile.OpenDialog.FileNames;
                foreach (string srcFilePath in fileNames)
                {
                    if (resourceFilesListView.SelectedItems.Count > 0 && resourceFilesListView.SelectedItems[0] is MyListFolderItem)
                    {
                        importFolder = (resourceFilesListView.SelectedItems[0] as MyListItem);
                        MyListItem item = ItemContains(resourceFilePaths, importFolder);
                        if (item != null)
                        {
                            importFolder = item;
                        }
                        parentResToolStripButton.Enabled = true;
                        myResourceCurrentItem = importFolder;

                    }
                    else
                    {
                        importFolder = myResourceCurrentItem;
                    }
                    FileSimpleExXmlElement fileEle = doc.CreateFileElement(importFolder._element as FolderXmlElement, srcFilePath);
                }
                if (importFolder != null)
                    InitListView(importFolder, resourceFilesListView, _resourceCurrentType);
            }
        }

        private void generalPageAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (myType != PageType.Home)
            //{
            //    NewPageForm newPage = new NewPageForm(parentEle, myType);
            //    newPage.ShowDialog();
            //}
            //else
            //{

            //}
            PageType pageType = PageType.None;
            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);
            switch (menuItem.Name)
            {
                case "generalPageAddToolStripMenuItem":
                    pageType = PageType.General;
                    break;
                case "indexPageAddToolStripMenuItem":
                    pageType = PageType.Home;
                    break;
                case "productPageAddToolStripMenuItem":
                    pageType = PageType.Product;
                    break;
                case "knowledgePageAddToolStripMenuItem":
                    pageType = PageType.Knowledge;
                    break;
                case "hrPageToolAddStripMenuItem":
                    pageType = PageType.Hr;
                    break;
                case "inviteBiddingAddPageToolStripMenuItem":
                    pageType = PageType.InviteBidding;
                    break;
                case "projectPageAddToolStripMenuItem":
                    pageType = PageType.Project;
                    break;
            }
            Form newPage;
            if (pageType == PageType.Home)
            {
                newPage = new NewHomePageForm(myPageCurrentItem.Element as FolderXmlElement, PageType.Home);
            }
            else
            {
                newPage = new NewPageForm(myPageCurrentItem.Element as FolderXmlElement, pageType);
            }
            if (newPage.ShowDialog() == DialogResult.OK)
            {
                InitListView(myPageCurrentItem, localPagesListView, _localPageCurrentType);
            }


        }
        #endregion

        #region 资源文件,和页面公用部分
        /// <summary>
        /// 加载项目
        /// </summary>
        /// <param name="initPath"></param>
        private void InitListView(MyListItem folderEle, ListViewEx listView, string FilterType)
        {
            listView.Items.Clear();

            // listView.FolderName = folderEle.Element.AbsoluteFilePath;

            CreateHeader(listView); //列表头
            //加载子文件夹
            foreach (XmlNode node in folderEle._element.ChildNodes)
            {
                if (node is FolderXmlElement && !(node is TmpltFolderXmlElement) && !(node is ResourcesXmlElement))
                {
                    FolderXmlElement folder = (FolderXmlElement)node;
                    if (folder.IsDeleted)
                    {
                        continue;
                    }

                    MyListFolderItem mylvi = new MyListFolderItem(folder, folderEle);
                    if (mainTabControl.SelectedIndex != 0)
                    {
                        mylvi.BreviaryMap = false;
                    }
                    string folderPath = folder.AbsoluteFilePath.Substring(0, folder.AbsoluteFilePath.Length - 1);
                    mylvi.Text = Path.GetFileName(folderPath);
                    mylvi.Text = folder.Title;

                    listView.Items.Add(mylvi);
                }
            }
            //加载子文件
            InitListViewSubFile(folderEle._element.ChildNodes, listView, FilterType);


            if (folderEle.BreviaryMap && folderEle.ListShowView == View.LargeIcon)
            {//缩略图

                listView.LargeImageList.Images.Clear();
                listView.LargeImageList.ImageSize = new Size(96, 96);
                foreach (MyListItem lvitem in listView.Items)
                {
                    // if (lvitem is MyFileItem1)
                    {
                        string fullPath = lvitem.Element.AbsoluteFilePath;
                        lvitem.ImageIndex = listView.LargeImageList.Images.Add(listView.GetThumbNail(fullPath), Color.Transparent);
                    }
                }
            }
            else
            {
                listView.LargeImageList.ImageSize = new Size(32, 32);
                //加载图标
                foreach (MyListItem lvitem in listView.Items)
                {
                    string fullPath = lvitem.Element.AbsoluteFilePath;
                    KeyValuePair<string, Image> keyImg = GetImageAndKey(fullPath, GetSystemIconType.ExtensionSmall);
                    if (!listView.SmallImageList.Images.ContainsKey(keyImg.Key))
                    {
                        listView.SmallImageList.Images.Add(keyImg.Key, keyImg.Value);
                    }
                    KeyValuePair<string, Image> keyLargeImg = GetImageAndKey(fullPath, GetSystemIconType.ExtensionLarge);
                    if (!listView.LargeImageList.Images.ContainsKey(keyLargeImg.Key))
                    {
                        listView.LargeImageList.Images.Add(keyLargeImg.Key, keyLargeImg.Value);
                    }
                    lvitem.ImageKey = keyImg.Key;
                }
            }

            if (folderEle is MyListFolderItem)
            {
                listView.View = folderEle.ListShowView;
            }
        }

        private void InitListViewSubFile(XmlNodeList xmlNode, ListViewEx listView, string FlterType)
        {
            //加载子文件
            foreach (XmlNode node in xmlNode)
            {
                if (node is FileSimpleExXmlElement)
                {
                    FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)node;

                    string exfile = Path.GetExtension(fileEle.AbsoluteFilePath).ToLower();
                    if (FlterType != _strAll && !FlterType.Contains(exfile)) continue;

                    MyListFileItem myFileItem = new MyListFileItem(fileEle);
                    if (fileEle.IsDeleted)
                    {
                        continue;
                    }
                    myFileItem.ItemMediaType = GetListItemType(exfile);
                    myFileItem.Element = fileEle;
                    myFileItem.Text = fileEle.Title;
                    listView.Items.Add(myFileItem);
                }
                else if (node is PageSimpleExXmlElement)
                {
                    PageSimpleExXmlElement fileEle = (PageSimpleExXmlElement)node;

                    if (FlterType != _strAll && fileEle.PageType.ToString() != FlterType) continue;
                    MyListFileItem myFileItem = new MyListFileItem(fileEle);
                    if (fileEle.IsDeleted)
                    {
                        continue;
                    }
                    myFileItem.Element = fileEle;
                    myFileItem.Text = fileEle.Title;
                    listView.Items.Add(myFileItem);
                }
            }
        }
        /// <summary>
        /// 根据扩展名得到ITEM类型
        /// </summary>
        /// <param name="exfile"></param>
        /// <returns></returns>
        private MediaFileType GetListItemType(string exfile)
        {
            MediaFileType fileType = MediaFileType.None;

            if (String.IsNullOrEmpty(exfile)) return fileType;
            else if (_strImageType.Contains(exfile.ToLower()))
            {
                fileType = MediaFileType.Pic;
            }
            else if (_strFlash.Contains(exfile.ToLower()))
            {
                fileType = MediaFileType.Flash;
            }
            else if (_strAuto.Contains(exfile.ToLower()))
            {
                fileType = MediaFileType.Audio;
            }
            else if (_strVido.Contains(exfile.ToLower()))
            {
                fileType = MediaFileType.Video;
            }
            return fileType;
        }

        /// <summary>
        /// 判断item是否在列表中
        /// </summary>
        /// <returns></returns>
        private MyListItem ItemContains(List<MyListItem> listPaths, MyListItem myItem)
        {
            foreach (MyListItem item in listPaths)
            {
                //本来是判断绝对路径,删除时将对应listPaths中项目也删除,但遍历的时候,List中内容以便,
                //编译器就报错,说List中内容已变,暂时不删除List中内容,但这样有可能路径就相同了,所以这里用ID
                if (item.Element.Id == myItem.Element.Id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 窗前类表头，暂时做法
        /// </summary>
        /// <param name="listView"></param>
        private void CreateHeader(ListViewEx listView)
        {
            listView.Columns.Clear();
            listView.Columns.Add("文件名", 160, HorizontalAlignment.Left);
            listView.Columns.Add("文件大小", 60, HorizontalAlignment.Left);
            listView.Columns.Add("文件类型", 60, HorizontalAlignment.Left);
            listView.Columns.Add("创建时间", 120, HorizontalAlignment.Left);
            listView.Columns.Add("访问时间", 200, HorizontalAlignment.Left);
        }

        /// <summary>
        /// 设置菜单互斥
        /// </summary>
        private void SetMenuItemMutex(ToolStripMenuItem menuItem)
        {
            foreach (ToolStripItem item in ((ToolStripDropDownButton)menuItem.OwnerItem).DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    (item as ToolStripMenuItem).Checked = false;
                }
            }
            menuItem.Checked = true;
        }

        #endregion

        #region 加载图标
        Dictionary<string, Image> _dicSystemFileSmallIcon = new Dictionary<string, Image>(); //小图标
        Dictionary<string, Image> _dicSystemFileLaregeIcon = new Dictionary<string, Image>(); //大图标

        ///<summary>
        ///根据文件路径获取其系统图标. （支持两种）
        ///</summary>
        KeyValuePair<string, Image> GetImageAndKey(string path, GetSystemIconType iconType)
        {
            if (iconType == GetSystemIconType.ExtensionSmall)
            {
                return GetImageAndKey(path, _dicSystemFileSmallIcon, iconType);
            }
            else if (iconType == GetSystemIconType.ExtensionLarge)
            {
                return GetImageAndKey(path, _dicSystemFileLaregeIcon, iconType);
            }
            return new KeyValuePair<string, Image>(null, null);
        }
        KeyValuePair<string, Image> GetImageAndKey(string path, Dictionary<string, Image> fileIcon, GetSystemIconType iconType)
        {
            string extension = Path.GetExtension(path);

            //无扩展名时特殊处理
            if ((!Utility.File.IsDirectory(path)) && extension == "")
            {
                string strSign = "?noextension";

                if (fileIcon.ContainsKey(strSign))
                {
                    return new KeyValuePair<string, Image>(strSign, fileIcon[strSign]);
                }
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(strSign, iconType);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        fileIcon.Add(strSign, img);
                        return new KeyValuePair<string, Image>(strSign, img);
                    }
                    else
                    {
                        return new KeyValuePair<string, Image>(null, null);
                    }
                }
            }
            ///如果是文件夹,.exe文件,.lnk文件,则找其具体的图标
            else if (Utility.File.IsDirectory(path) || extension == ".exe" || extension == ".lnk")
            {
                if (iconType == GetSystemIconType.ExtensionLarge)
                {
                    iconType = GetSystemIconType.PathLarge;
                }
                else if (iconType == GetSystemIconType.ExtensionSmall)
                {
                    iconType = GetSystemIconType.PathSmall;
                }
                Icon ico = Service.Icon.GetSystemIcon(path, iconType);
                if (ico != null)
                {
                    Image img = ico.ToBitmap();
                    if (!fileIcon.ContainsKey(path))
                        fileIcon.Add(path, img);
                    return new KeyValuePair<string, Image>(path, img);
                }
                else
                {
                    return new KeyValuePair<string, Image>(null, null);
                }
            }
            ///其它的找扩展名的图标
            else
            {
                if (fileIcon.ContainsKey(extension))
                {
                    return new KeyValuePair<string, Image>(extension, fileIcon[extension]);
                }
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(extension, iconType);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        fileIcon.Add(extension, img);
                        return new KeyValuePair<string, Image>(extension, img);
                    }
                    else
                    {
                        return new KeyValuePair<string, Image>(null, null);
                    }
                }
            }
        }

        #endregion 

        #region 页面相关部分

        private void generalPageFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);
            switch (menuItem.Name)
            {
                case "generalPageFilterToolStripMenuItem":
                    _localPageCurrentType = PageType.General.ToString();
                    break;
                case "indexPageFilterToolStripMenuItem":
                    _localPageCurrentType = PageType.Home.ToString();
                    break;
                case "productPageFilterToolStripMenuItem":
                    _localPageCurrentType = PageType.Product.ToString();
                    break;
                case "knowledgePageFilterToolStripMenuItem":
                    _localPageCurrentType = PageType.Knowledge.ToString();
                    break;
                case "hrPageFilterToolStripMenuItem":
                    _localPageCurrentType = PageType.Hr.ToString();
                    break;
                case "inviteBiddingPageFilterToolStripMenuItem":
                    _localPageCurrentType = PageType.InviteBidding.ToString();
                    break;
                case "projectPageFilterToolStripMenuItem":
                    _localPageCurrentType = PageType.Project.ToString();
                    break;
                case "allPageFilterToolStripMenuItem":
                    _localPageCurrentType = _strAll;
                    break;

            }
            InitListView(myPageCurrentItem, localPagesListView, _localPageCurrentType);
            SetMenuItemMutex(menuItem);
        }

        private void localPagesListView_DoubleClick(object sender, EventArgs e)
        {
            if (localPagesListView.SelectedItems.Count > 0)
            {
                MyListItem myitem = localPagesListView.SelectedItems[0] as MyListItem;
                if (myitem is MyListFileItem)
                {
                    //模拟使用过的图片
                    //string df = myitem.Element.Title;
                    //string sf = Path.GetDirectoryName(myitem.Element.AbsoluteFilePath);
                    Service.RecentFiles.AddFilePath(Service.RecentFiles.RecentOpenLocalPageFiles, myitem.Element.AbsoluteFilePath);

                    XmlDocument xmlDoc = new XmlDocument();
                    XmlElement xmlEle = xmlDoc.CreateElement("Page");
                    ((MyListFileItem)myitem).XmlAttribute = xmlEle;

                    string strUrl = "[url:]" + ((MyListFileItem)myitem).Element.Id;
                    ((MyListFileItem)myitem).XmlAttribute.SetAttribute("src", strUrl);

                    if (((MyListFileItem)myitem).XmlAttribute != null)
                    { //进行了高级设置
                        XmlElement ele = ((MyListFileItem)myitem).XmlAttribute;
                        foreach (XmlAttribute att in ele.Attributes)
                        {
                            _XmlElement.SetAttribute(att.Name, att.Value);
                        }
                    }

                    this.DialogResult = DialogResult.OK;

                }
                else
                {
                    MyListItem item = ItemContains(localFilePaths, myitem);
                    if (item == null)
                    {
                        localFilePaths.Add(myitem); //没有打开过,添加
                    }
                    else
                    {
                        myitem = item;
                    }
                    myPageCurrentItem = myitem;
                    InitListView(myPageCurrentItem, localPagesListView, _localPageCurrentType);
                    parentPageToolStripButton.Enabled = true;// (myitem.Element.AbsoluteFilePath != Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath);

                }
                SetLocalPageMenuCheckState(myPageCurrentItem);

            }
        }

        private void parentPageToolStripButton_Click(object sender, EventArgs e)
        {//页面上一级
            if (myPageCurrentItem is MyListFolderItem)
            {
                MyListItem myCurrent = (myPageCurrentItem as MyListFolderItem).FolderItemParant;

                if (myCurrent.FolderItemParant == null)
                {
                    parentPageToolStripButton.Enabled = false; //到根了
                    InitListView(localFilePaths[0], localPagesListView, _localPageCurrentType);
                    myPageCurrentItem = localFilePaths[0];
                }
                else
                {
                    myPageCurrentItem = myCurrent;
                    InitListView(myCurrent, localPagesListView, _localPageCurrentType);
                }
                SetLocalPageMenuCheckState(myPageCurrentItem);
            }
        }

        private void largeIconViewPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);
            switch (menuItem.Name)
            {
                case "largeIconViewPageToolStripMenuItem":
                    localPagesListView.View = View.LargeIcon;
                    break;
                case "smallIconViewPageToolStripMenuItem":
                    localPagesListView.View = View.SmallIcon;
                    break;
                case "listViewPageToolStripMenuItem":
                    localPagesListView.View = View.List;
                    break;
                case "detailsViewPageToolStripMenuItem":
                    localPagesListView.View = View.Details;
                    break;
            }
            if (myPageCurrentItem != null && myPageCurrentItem is MyListFolderItem)
            {//保存该层文件夹下的显示模式
                (myPageCurrentItem as MyListFolderItem).ListShowView = localPagesListView.View;
            }
            SetMenuItemMutex(menuItem);
        }

        /// <summary>
        /// 根据当前文件夹显示的方式,设置菜单是否选中
        /// </summary>
        private void SetLocalPageMenuCheckState(MyListItem myitem)
        {
            ToolStripDropDownButton toolStripButton = (ToolStripDropDownButton)localpagesToolStrip.Items["viewsPageToolStripDropDownButton"];

            string strCheckName = "";
            if (myitem.ListShowView == View.LargeIcon)
            {
                strCheckName = "largeIconViewPageToolStripMenuItem";
            }
            else if (myitem.ListShowView == View.SmallIcon)
            {
                strCheckName = "smallIconViewPageToolStripMenuItem";
            }
            else if (myitem.ListShowView == View.List)
            {
                strCheckName = "listViewPageToolStripMenuItem";
            }
            else if (myitem.ListShowView == View.Details)
            {
                strCheckName = "detailsViewPageToolStripMenuItem";
            }
            foreach (ToolStripItem item in toolStripButton.DropDownItems)
            {
                if (item is ToolStripMenuItem && item.Name.Equals(strCheckName))
                {
                    (item as ToolStripMenuItem).Checked = true;
                }
                else
                {
                    (item as ToolStripMenuItem).Checked = false;
                }
            }
        }

        private void deletePageToolStripButton_Click(object sender, EventArgs e) 
        {
            if (localPagesListView.SelectedItems.Count <= 0) return;
            if (MessageService.Show("${res:Tree.msg.delete}", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            MyListItem deleItem = localPagesListView.SelectedItems[0] as MyListItem;
            doc.DeleteItem(deleItem.Element);
            localPagesListView.Items.Remove(deleItem);
        }

        private void localPagesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                deletePageToolStripButton_Click(null,EventArgs.Empty);
            }
        }

        #endregion  

        #region BUTTON事件

        private void okButton_Click(object sender, EventArgs e)
        {//暂时做法
            switch (CurrentPageIndex)
            {
                case 1:
                    {
                        #region 资源文件
                        resourceFilesListView_DoubleClick(null, EventArgs.Empty);
                        return;
                        #endregion
                    }
                case 2:
                    {
                        #region 本地页面
                        localPagesListView_DoubleClick(null, EventArgs.Empty);
                        return;
                        #endregion
                    }                
            }
            this.DialogResult = DialogResult.OK;
        }

        #endregion

    }

    #region 链接和Email的结构

    //public class LinkImage : XhtmlTags.Img
    //{

    //}

    //public class LinkObject : XhtmlTags.Object
    //{ 
    
    //}
    /// <summary>
    /// 外部链接结构
    /// </summary>
    public class InsertLinkCode// : XhtmlTags.A
    {
        public string LinkText;
        public string LinkAdress;
        public string LinkBookMark;
        public string LinkTarget;
        public string LinkAccessKey;
        public string LinkTabIndex;
        public string LinkTitle;

        public object MyProperty { get; set; }

        XhtmlSection _section = new XhtmlSection();
        XhtmlTags.A _aTag;
        public InsertLinkCode()
        {
            NewMethod();

        }

        public InsertLinkCode(string url, string title, string text, Xhtml.Target target, int tabindex, char accesskey)
        {
            NewMethod();

        }
        public InsertLinkCode(string url, string bookmark, string title, string text, Xhtml.Target target, int tabindex, char accesskey)
        {

            NewMethod();

        }
        private void NewMethod()
        {
            _aTag = _section.CreateXhtmlA();
            _section.AppendChild(_aTag);
        }

        public static InsertLinkCode Parse(string str)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(str);
                foreach (XmlNode item in doc.Attributes)
                {
                    string key = item.LocalName;
                    string value = item.Value;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static InsertLinkCode Parse(XhtmlTagElement tag)
        {
            return null;
        }
        public override string ToString()
        {
            //if (LinkImage != null)
            //{

            //}
            //_aTag.Builder();
            return _section.InnerXml;
        }
        public XhtmlTags.A ToXhtml()
        {
            //_aTag.Builder();

            return _aTag;
        }
        //public string LinkCodeToHtml() { return ""; }
    }

    public class eeEmail// : InsertLinkCode
    {
        //public Email(string email, string EmailText)
        //    : base("mailto:" + email, EmailText, null, null, null, null)
        //{

        //}
    }

    /// <summary>
    /// 电子邮件结构
    /// </summary>
    public struct InsertEmailCode
    {
        public string EmailText;
        public string EamilSubject;

        //public string EamilToHtml() { return ""; }
    }

    #endregion

}