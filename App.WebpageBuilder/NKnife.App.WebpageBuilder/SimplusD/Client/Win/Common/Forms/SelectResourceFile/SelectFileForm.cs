using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
//using CustomControls;
using Jeelu.Win;
using CustomControls;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SelectFileForm : BaseForm
    {
        List<SimpleExIndexXmlElement> filePaths = null;
        int pathNum = -1;
        MediaFileType fileType;
        string[] FilterArray ={ "PIC files (*.jpg,*.png,*.gif)|*.jpg;*.png;*.gif",
                                "Flash files (.swf)|*.swf",
                                "Media files (*.rmvb,*.rm,*.avi,*.wmv)|*.rmvb;*.rm;*.avi;*.wmv",
                                "Audio files (*.mp3,*.mdi,*.wma,*.wav)|*.mp3;*.mid;*.wma;*.wav"
                              };
        Dictionary<string, string> picFilter = new Dictionary<string, string>();
        Dictionary<string, string> flashFilter = new Dictionary<string, string>();
        Dictionary<string, string> audioFilter = new Dictionary<string, string>();
        Dictionary<string, string> mediaFilter = new Dictionary<string, string>();

        public MediaFileType FileType
        {
            get { return fileType; }
            set
            {
                switch (value)
                {
                    case MediaFileType.Pic:
                        {
                            Dictionary<string, string>.KeyCollection keyCollent = picFilter.Keys;
                            foreach (string key in keyCollent)
                                FilterComboBox.Items.Add(key);
                            break;
                        }
                    case MediaFileType.Flash:
                        {
                            Dictionary<string, string>.KeyCollection keyCollent = flashFilter.Keys;
                            foreach (string key in keyCollent)
                                FilterComboBox.Items.Add(key);
                            break;
                        }
                    case MediaFileType.Video:
                        {
                            Dictionary<string, string>.KeyCollection keyCollent = mediaFilter.Keys;
                            foreach (string key in keyCollent)
                                FilterComboBox.Items.Add(key);
                            break;
                        }
                    case MediaFileType.Audio:
                        {
                            Dictionary<string, string>.KeyCollection keyCollent = audioFilter.Keys;
                            foreach (string key in keyCollent)
                                FilterComboBox.Items.Add(key);
                            break;
                        }
                }
                fileType = value;
            }
        }

        ToolStripMenuItem importResourcesMenuItem = null;
        public SelectFileForm(MediaFileType mediaFileType)
        {
            InitializeComponent();
            this.listView.FolderName =Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath;
            initFilter();
            FileType = mediaFileType;
            filePaths = new List<SimpleExIndexXmlElement>();
            filePaths.Add(Service.Sdsite.CurrentDocument.Resources);
            pathNum++;
            listView.MultiSelect = false;
            listView.SmallImageList = new ImageList();
            listView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

            backToolStripButton.Image = ResourceService.GetResourceImage("selectResources.prior");
            preToolStripButton.Image = ResourceService.GetResourceImage("selectResources.next");
            upToolStripButton.Image = ResourceService.GetResourceImage("selectResources.up");
            newFolderToolStripButton.Image = ResourceService.GetResourceImage("selectResources.new");

            InitListView(Service.Sdsite.CurrentDocument.Resources);
            ContextMenuStrip SelectFileMemu = new ContextMenuStrip();
            importResourcesMenuItem = new ToolStripMenuItem("导入资源文件");
            SelectFileMemu.Items.Add(importResourcesMenuItem);
            listView.ContextMenuStrip = SelectFileMemu;
            upToolStripButton.Enabled = true;
            SelectFileMemu.ItemClicked += new ToolStripItemClickedEventHandler(SelectFileMemu_ItemClicked);
            selectFileToolStrip.ItemClicked += new ToolStripItemClickedEventHandler(selectFileToolStrip_ItemClicked);
            preToolStripButton.Visible = false;
            upToolStripButton.Enabled = false;
        }

        void initFilter()
        {
            picFilter.Add("PIC files (*.jpg)", ".jpg");
            //picFilter.Add("PIC files (*.png)", ".png");
            picFilter.Add("PIC files (*.gif)", ".gif");
            picFilter.Add("PIC files all", "all");

            flashFilter.Add("Flash files (*.swf)", ".swf");

            mediaFilter.Add("Media files (*.rmvb)", ".rmvb");
            mediaFilter.Add("Media files (*.rm)", ".rm");
            mediaFilter.Add("Media files (*.avi)", ".avi");
            mediaFilter.Add("Media files (*.wmv)", ".wmv");
            mediaFilter.Add("Media files all", "all");

            audioFilter.Add("Media files (*.mp3)", ".mp3");
            audioFilter.Add("Media files (*.mdi)", ".mdi");
            audioFilter.Add("Media files (*.wma)", ".wma");
            audioFilter.Add("Media files (*.wav)", ".wav");
            audioFilter.Add("Media files all", "all");
        }

        void SetBackEnabled()
        {
            backToolStripButton.Enabled = (pathNum > 0);
        }
        void SetPreEnabled()
        {
            preToolStripButton.Enabled = (pathNum < filePaths.Count - 1);
        }

        /// <summary>
        /// 工具栏单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void selectFileToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "backToolStripButton": TranFolder(true); break;
                case "PreToolStripButton": TranFolder(false); break;
                case "upToolStripButton": UpFolder(); break;
                case "ImpResourcesToolStripButton": ImportResources(); break;
                case "newFolderToolStripButton": NewResourcesFolder(); break;
            }
        }

        /// <summary>
        /// 弹出菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectFileMemu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ((ContextMenuStrip)sender).Close();
            ImportResources();
        }

        /// <summary>
        /// 前进或者后退
        /// </summary>
        /// <param name="next"></param>
        private void TranFolder(bool next)
        {
            SimpleExIndexXmlElement initFolder = null;
            if (next)
                initFolder = filePaths[--pathNum];
            else
                initFolder = filePaths[++pathNum];
            InitListView(initFolder);
            SetBackEnabled();
            SetPreEnabled();
            if (initFolder.AbsoluteFilePath != Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath)
            {
                upToolStripButton.Enabled = true;
                backToolStripButton.Enabled = true;
            }
            else
            {
                upToolStripButton.Enabled = false;
                backToolStripButton.Enabled = false;
                filePaths.Clear();
                pathNum = 0;
                filePaths.Add(Service.Sdsite.CurrentDocument.Resources);
            }
        }

        /// <summary>
        /// 到上一级文件目录
        /// </summary>
        private void UpFolder()
        {
            SimpleExIndexXmlElement FolderFullPath = filePaths[pathNum];
            SimpleExIndexXmlElement parentEle = (SimpleExIndexXmlElement)FolderFullPath.ParentNode;
            if (FolderFullPath.AbsoluteFilePath != Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath)
            {
                InitListView(parentEle);
                filePaths.Add(parentEle);
                pathNum++;
            }
            if (parentEle.AbsoluteFilePath != Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath)
                upToolStripButton.Enabled = true;
            else
            {
                upToolStripButton.Enabled = false;
                filePaths.Clear();
                pathNum = 0;
                filePaths.Add(Service.Sdsite.CurrentDocument.Resources);
                SetBackEnabled();
            }
        }

        /// <summary>
        /// 导入资源文件方法
        /// </summary>
        void ImportResources()
        {
            try
            {
                string newFilePath = string.Empty;
                SimpleExIndexXmlElement importFolder = null;
                SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
                FormOpenFileDialog OpenFile = new FormOpenFileDialog();
                #region 筛选处理
                OpenFile.OpenDialog.Multiselect = true;
                string openfileFilter = "";
                switch (fileType)
                {
                    case MediaFileType.Pic: openfileFilter = "PIC files (*.jpg,*.gif)|*.jpg;*.gif;"; break;
                    case MediaFileType.Flash: openfileFilter = "Flash files (.swf)|*.swf"; break;
                    case MediaFileType.Video: openfileFilter = "Media files (*.rmvb,*.rm,*.avi,*.wmv)|*.rmvb;*.rm;*.avi;*.wmv"; break;
                    case MediaFileType.Audio: openfileFilter = "Audio files (*.mp3,*.mdi,*.wma,*.wav)|*.mp3;*.mid;*.wma;*.wav"; break;
                }
                OpenFile.OpenDialog.Filter = openfileFilter;
                #endregion
                if (OpenFile.ShowDialog(this) == DialogResult.OK)
                {
                    string[] fileNames = OpenFile.OpenDialog.FileNames;
                    foreach (string srcFilePath in fileNames)
                    {
                        if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0] is MyFolderItem)
                        {
                            importFolder = (listView.SelectedItems[0] as MyItem)._element;// filePaths[pathNum].AbsoluteFilePath;
                            filePaths.Add(importFolder);
                            pathNum++;
                            backToolStripButton.Enabled = upToolStripButton.Enabled = true;
                        }
                        else
                        {
                            importFolder = filePaths[pathNum];
                            if (importFolder is FileSimpleExXmlElement)
                                importFolder = filePaths[pathNum - 1];
                        }

                        FileSimpleExXmlElement fileEle = doc.CreateFileElement(importFolder as FolderXmlElement, srcFilePath);
                        //#region 复制文件
                        /////将文件名翻译成拼音，生成一个不重名的文件名
                        //string fileName = Path.GetFileName(srcFilePath);
                        //string pinyinFileName = Utility.File.FormatFileName(fileName);
                        //string formatFileName = Utility.File.BuildFileName(importFolder.AbsoluteFilePath, pinyinFileName, false, true);

                        //bool isNoIn = false;//标识是否已经加载的资源文件
                        //if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0] is MyFolderItem)
                        //{
                        //    importFolder = (listView.SelectedItems[0] as MyItem)._element;// filePaths[pathNum].AbsoluteFilePath;
                        //    filePaths.Add(importFolder);
                        //    pathNum++;
                        //    backToolStripButton.Enabled = upToolStripButton.Enabled = true;
                        //}
                        //else
                        //{
                        //    importFolder = filePaths[pathNum];
                        //    if (importFolder is FileSimpleExXmlElement)
                        //        importFolder = filePaths[pathNum - 1];
                        //}

                        //newFilePath = Path.Combine(importFolder.AbsoluteFilePath, formatFileName);

                        //if (File.Exists(newFilePath))
                        //{
                        //    if (MessageBox.Show("您确定要覆盖吗?", "确定", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        //    {
                        //        isNoIn = false;
                        //        File.Copy(srcFilePath, newFilePath, true);
                        //    }
                        //}
                        //else
                        //{
                        //    isNoIn = true;
                        //    File.Copy(srcFilePath, newFilePath);
                        //}
                        
                        //#endregion
                        //#region 加入索引
                        //if (isNoIn)
                        //{
                        //    FileSimpleExXmlElement fileEle = doc.CreateFileElementNoCreateFile(importFolder as FolderXmlElement, formatFileName,fileName);
                        //}
                        //#endregion
                    }
                    if (importFolder != null)
                        InitListView(importFolder);
                }
            }
            catch { }
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        void NewResourcesFolder()
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            FolderXmlElement parentFolderEle = filePaths[pathNum] as FolderXmlElement;
            FolderXmlElement newFolderEle = doc.CreateFolder(parentFolderEle, XmlUtilService.CreateIncreaseFolderTitle(parentFolderEle));
            Directory.CreateDirectory(newFolderEle.AbsoluteFilePath);

            MyFolderItem mylvi = new MyFolderItem(newFolderEle);
            listView.Items.Add(mylvi);

            KeyValuePair<string, Image> keyImg = GetImageAndKey(mylvi.Element.AbsoluteFilePath);
            if (!listView.SmallImageList.Images.ContainsKey(keyImg.Key))
                listView.SmallImageList.Images.Add(keyImg.Key, keyImg.Value);
            mylvi.ImageKey = keyImg.Key;

            mylvi.Selected = true;
            mylvi.BeginEdit();
        }

        /// <summary>
        /// 加载项目
        /// </summary>
        /// <param name="initPath"></param>
        void InitListView(SimpleExIndexXmlElement folderEle)
        {
            listView.Items.Clear();
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            //加载子文件夹
            foreach (XmlNode node in folderEle.ChildNodes)
            {
                if (node is FolderXmlElement)
                {
                    FolderXmlElement folder = (FolderXmlElement)node;
                    if (folder.IsDeleted)
                    {
                        continue;
                    }

                    MyFolderItem mylvi = new MyFolderItem(folder);
                    string folderPath = folder.AbsoluteFilePath.Substring(0, folder.AbsoluteFilePath.Length - 1);
                    mylvi.Text = Path.GetFileName(folderPath);
                    mylvi.Text = folder.Title;
                    listView.Items.Add(mylvi);
                }
            }
            //加载子文件
            foreach (XmlNode node in folderEle.ChildNodes)
            {
                if (node is FileSimpleExXmlElement)
                {
                    FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)node;
                    MyFileItem myFileItem = new MyFileItem(fileEle);
                    if (fileEle.IsDeleted)
                    {
                        continue;
                    }

                    myFileItem.Element = fileEle;
                    myFileItem.Text = Path.GetFileName(fileEle.AbsoluteFilePath);
                    myFileItem.Text = fileEle.Title;

                    string exfile = Path.GetExtension(fileEle.AbsoluteFilePath).ToLower();
                    switch (FileType)
                    {
                        case MediaFileType.Pic:
                            {
                                if (exfile == ".jpg" || exfile == ".jpeg" /*|| exfile == ".png"*/ || exfile == ".gif")
                                {
                                    myFileItem.ItemMediaType = MediaFileType.Pic;
                                    string valuestr = "";
                                    if (!picFilter.TryGetValue(FilterComboBox.Text, out valuestr) || valuestr == "all")
                                    {
                                        listView.Items.Add(myFileItem);
                                    }
                                    else
                                    {
                                        if (exfile == valuestr)
                                            listView.Items.Add(myFileItem);
                                    }
                                }
                                break;
                            }
                        case MediaFileType.Flash:
                            {
                                if (exfile == ".swf")
                                {
                                    myFileItem.ItemMediaType = MediaFileType.Flash;
                                    listView.Items.Add(myFileItem);
                                }
                                break;
                            }
                        case MediaFileType.Audio:
                            {
                                if (exfile == ".mp3" || exfile == ".mdi" || exfile == ".wma" || exfile == "*.wav")
                                {
                                    myFileItem.ItemMediaType = MediaFileType.Audio;
                                    string valuestr = "";
                                    if (!audioFilter.TryGetValue(FilterComboBox.Text, out valuestr) || valuestr == "all")
                                    {
                                        listView.Items.Add(myFileItem);
                                    }
                                    else
                                    {
                                        if (exfile == valuestr)
                                            listView.Items.Add(myFileItem);
                                    }
                                }
                                break;
                            }
                        case MediaFileType.Video:
                            {
                                if (exfile == ".rmvb" || exfile == ".rm" || exfile == ".avi" || exfile == ".wmv")
                                {
                                    myFileItem.ItemMediaType = MediaFileType.Video;
                                    string valuestr = "";
                                    if (!mediaFilter.TryGetValue(FilterComboBox.Text, out valuestr) || valuestr == "all")
                                    {
                                        listView.Items.Add(myFileItem);
                                    }
                                    else
                                    {
                                        if (exfile == valuestr)
                                            listView.Items.Add(myFileItem);
                                    }
                                }
                                break;
                            }
                    }

                }
            }

            //加载图标
            foreach (MyItem lvitem in listView.Items)
            {
                string fullPath = lvitem.Element.AbsoluteFilePath;
                KeyValuePair<string, Image> keyImg = GetImageAndKey(fullPath);
                if (!listView.SmallImageList.Images.ContainsKey(keyImg.Key))
                {
                    listView.SmallImageList.Images.Add(keyImg.Key, keyImg.Value);
                }
                lvitem.ImageKey = keyImg.Key;
            }
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                MyItem myitem = listView.SelectedItems[0] as MyItem;
                if (myitem is MyFileItem)
                    OKBtn.PerformClick();
                else
                {
                    InitListView(myitem.Element);
                    filePaths.Add(myitem.Element);
                    pathNum++;
                    SetBackEnabled();
                    SetPreEnabled();
                    upToolStripButton.Enabled = (myitem.Element.AbsoluteFilePath != Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath);
                }
            }
        }

        /// <summary>
        /// key:ID,value:path
        /// </summary>
        public string FileId
        {
            get
            {
                MyFileItem myitem = null;
                if (listView.Items.Count > 0 && listView.SelectedItems.Count != 0)
                {
                    myitem = listView.SelectedItems[0] as MyFileItem;
                    if (myitem != null)
                        return myitem.Element.Id;
                    else
                        return "";
                }
                else
                    return "";
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                MyItem myItem = listView.SelectedItems[0] as MyItem;
                string fullPath = myItem.Element.AbsoluteFilePath;
                PathTextBox.Text = fullPath.Substring(Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath.Length);
                try
                {
                    ImgpictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    if (!Utility.File.IsDirectory(fullPath))
                    {
                        MyFileItem myfileItem = myItem as MyFileItem;
                        if (myfileItem != null && myfileItem.ItemMediaType == MediaFileType.Pic)
                        {
                            ImgpictureBox.ImageLocation = fullPath;
                        }
                    }
                }
                catch
                {
                }
            }
        }

        #region 加载图标
        Dictionary<string, Image> _dicSystemFileIcon = new Dictionary<string, Image>();

        ///<summary>
        ///根据文件路径获取其系统图标.
        ///</summary>
        Image GetImageIndex(string path)
        {
            return GetImageAndKey(path).Value;
        }
        KeyValuePair<string, Image> GetImageAndKey(string path)
        {
            string extension = Path.GetExtension(path);

            //无扩展名时特殊处理
            if ((!Utility.File.IsDirectory(path)) && extension == "")
            {
                string strSign = "?noextension";

                if (_dicSystemFileIcon.ContainsKey(strSign))
                {
                    return new KeyValuePair<string, Image>(strSign, _dicSystemFileIcon[strSign]);
                }
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(strSign, GetSystemIconType.ExtensionSmall);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        _dicSystemFileIcon.Add(strSign, img);
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
                Icon ico = Service.Icon.GetSystemIcon(path, GetSystemIconType.PathSmall);
                if (ico != null)
                {
                    Image img = ico.ToBitmap();
                    if (!_dicSystemFileIcon.ContainsKey(path))
                        _dicSystemFileIcon.Add(path, img);
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
                if (_dicSystemFileIcon.ContainsKey(extension))
                {
                    return new KeyValuePair<string, Image>(extension, _dicSystemFileIcon[extension]);
                }
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(extension, GetSystemIconType.ExtensionSmall);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        _dicSystemFileIcon.Add(extension, img);
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

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SimpleExIndexXmlElement FolderFullPath = filePaths[pathNum];
            /* if (FilePathID.Value != null)
                 FolderFullPath = FilePathID.Value;
             else
                 FolderFullPath = filePaths[pathNum];*/
            if (FolderFullPath != null)
                InitListView(FolderFullPath);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count>0)
            {
                if (listView.SelectedItems[0] is MyFolderItem)
                    listView_DoubleClick(null, null);
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }

    public class MyItem : ListViewItem
    {
        public SimpleExIndexXmlElement _element = null;

        public SimpleExIndexXmlElement Element
        {
            get { return _element; }
            set { _element = value; }
        }

        public MyItem(SimpleExIndexXmlElement element)
        {
            _element = element;
        }
    }

    public class MyFileItem : MyItem
    {
        public MyFileItem(SimpleExIndexXmlElement element)
            :base(element)
        {
            _element = element;
            this.Text = _element.Title;
        }

        MediaFileType _fileType;
        public MediaFileType ItemMediaType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        public ListViewItemType ItemType
        {
            get
            {
                return ListViewItemType.File;
            }
        }
    }

    public class MyFolderItem : MyItem
    {
        public MyFolderItem(FolderXmlElement element)
            :base(element)
        {
            _element = element;
            this.Text = _element.Title;
        }

        public ListViewItemType ItemType
        {
            get
            {
                return ListViewItemType.Folder;
            }
        }
    }

    public enum ListViewItemType
    {
        Folder,
        File,
    }
}