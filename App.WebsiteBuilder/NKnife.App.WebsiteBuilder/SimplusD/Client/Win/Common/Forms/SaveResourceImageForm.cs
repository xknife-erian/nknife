using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SaveResourceImageForm : BaseForm
    {
        public SaveResourceImageForm( Image image)
        {
            InitializeComponent();
            _image = image;
            Init();
        }
        
        #region 内部变量

        Image _image;

        System.Drawing.Imaging.ImageFormat _imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg;

        private List<MyListItem> resourceFilePaths = null; //打开过的路径LIST
        private MyListItem myResourceCurrentItem; //资源当前显示的路径

        string _resourceCurrentType = ".jpg,.png,.gif,.bmp"; //暂时写法
        #endregion

        #region 公共属性

        #endregion

        #region 公共方法

        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //panelPreview.Update();
            //panelPreview.Refresh();
            panelPreview.Paint += new PaintEventHandler(panelPreview_Paint);
            //Graphics g = panelPreview.CreateGraphics();

            resourceFilePaths = new List<MyListItem>();
            MyListFolderItem myFolderResouce = new MyListFolderItem(Service.Sdsite.CurrentDocument.Resources, null);
            myResourceCurrentItem = myFolderResouce;
            resourceFilePaths.Add(myFolderResouce);

            listViewFodel.MultiSelect = false;
            listViewFodel.SmallImageList = new ImageList();
            listViewFodel.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            listViewFodel.SmallImageList.ImageSize = new Size(16, 16);

            listViewFodel.LargeImageList = new ImageList();
            listViewFodel.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            listViewFodel.View = View.LargeIcon; //资源文件初始打开为缩略图
            myResourceCurrentItem.ListShowView = View.LargeIcon;
            myResourceCurrentItem.BreviaryMap = true;

            InitListView(myFolderResouce, listViewFodel, _resourceCurrentType);

            comboBoxType.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载项目
        /// </summary>
        /// <param name="initPath"></param>
        private void InitListView(MyListItem folderEle, ListViewEx listView, string FilterType)
        {
            listView.Items.Clear();

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

        /// <summary>
        /// //加载子文件
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="listView"></param>
        /// <param name="FlterType"></param>
        private void InitListViewSubFile(XmlNodeList xmlNode, ListViewEx listView, string FlterType)
        {            
            foreach (XmlNode node in xmlNode)
            {
                if (node is FileSimpleExXmlElement)
                {
                    FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)node;

                    string exfile = Path.GetExtension(fileEle.AbsoluteFilePath).ToLower();
                    if (!FlterType.Contains(exfile)) continue;

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

        void panelPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //g.DrawImageUnscaled(_image, new Point(0, 0));
            int imgW = _image.Width;
            int imgH = _image.Height;
            int panelW = panelPreview.Width;
            int panelH = panelPreview.Height;

            bool isW = (float)((float)imgW / (float)imgH) >= (float)((float)panelW / (float)panelH) ? true : false;
            if (isW)
            {
                if (imgW >= panelW)
                {
                    int h = (int)(panelH * ((float)imgH / (float)imgW));
                    g.DrawImage(_image, new RectangleF(0, 0, panelW, h));
                }
                else
                {
                    g.DrawImage(_image, new Point((int)((panelW - imgW) / 2), (int)((panelH - imgH) / 2)));
                }
            }
            else
            {
                if (imgH >= panelH)
                {
                    int w = (int)(panelW * ((float)imgW / (float)imgH));
                    g.DrawImage(_image, new RectangleF(0, 0, w, panelH));
                }
                else
                {
                    g.DrawImage(_image, new Point((int)((panelW - imgW) / 2), (int)((panelH - imgH) / 2)));
                }
            }
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

        #endregion

        #region 事件响应

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (myResourceCurrentItem == null) return;

            if (listViewFodel.SelectedItems.Count > 0)
            {
                listViewFodel_MouseDoubleClick(null,null);
            }
            else
            {
                if (String.IsNullOrEmpty(textBoxName.Text.Trim()))
                {
                    MessageBox.Show("文件名不能为空");
                    return;
                }
                string strFileName = textBoxName.Text + "." + comboBoxType.Text;
                string strFullpath = Path.Combine(myResourceCurrentItem.Element.AbsoluteFilePath,strFileName);
                if (File.Exists(strFullpath))
                {
                    SaveOrReplaceFile(true,strFileName);
                }
                else
                {
                    SaveOrReplaceFile(false,strFileName);
                }
            }
        }

        /// <summary>
        ///  保存/替换文件
        /// </summary>
        /// <param name="isReplace">true 替换文件 ， false 只保存</param>
        /// <returns>保存/替换成功 返回TRUE 失败 FALSE</returns>
        private bool SaveOrReplaceFile(bool isReplace,string fileName)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;

            if (isReplace)
            {
                DialogResult result = MessageService.Show("${res:Tree.msg.coverFile}",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                    return false;

                //旧文件删除
                foreach (MyListItem item in listViewFodel.Items)
                {
                    if (item is MyListFileItem)
                    {
                        MyListFileItem fileItem = item as MyListFileItem;
                        if (fileName.Equals(fileItem.Element.FileName,StringComparison.CurrentCultureIgnoreCase))
                        {
                            MyListItem deleItem = fileItem as MyListItem;
                            doc.DeleteItem(deleItem.Element);
                            listViewFodel.Items.Remove(deleItem);
                        }
                    }
                }
            }
            //保存新文件
            if (myResourceCurrentItem != null)
            {                
                string strSrcFile = Path.Combine(myResourceCurrentItem.Element.AbsoluteFilePath,fileName);
                _image.Save(strSrcFile, _imgFormat);
                FileSimpleExXmlElement fileEle = doc.CreateFileElement(myResourceCurrentItem.Element as FolderXmlElement, strSrcFile);
                InitListView(myResourceCurrentItem, listViewFodel, _resourceCurrentType);

                this.DialogResult = DialogResult.OK;
            }



            return true;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0)
            {
                _imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                _imgFormat = System.Drawing.Imaging.ImageFormat.Png;
            }
            else if (comboBoxType.SelectedIndex == 2)
            {
                _imgFormat = System.Drawing.Imaging.ImageFormat.Gif;
            }
            else if (comboBoxType.SelectedIndex == 3)
            {
                _imgFormat = System.Drawing.Imaging.ImageFormat.Bmp;
            }
        }

        /// <summary>
        /// 选择过滤方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thumbnailViewResToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);

            listViewFodel.LargeImageList.Images.Clear();
            listViewFodel.SmallImageList.Images.Clear();

            switch (menuItem.Name)
            {
                case "thumbnailViewResToolStripMenuItem":
                    //显示方式为缩略图
                    myResourceCurrentItem.BreviaryMap = true;
                    myResourceCurrentItem.ListShowView = View.LargeIcon;
                    break;
                case "largeIconViewResToolStripMenuItem":
                    listViewFodel.LargeImageList.ImageSize = new Size(32, 32);
                    listViewFodel.View = View.LargeIcon;
                    myResourceCurrentItem.BreviaryMap = false;
                    myResourceCurrentItem.ListShowView = View.LargeIcon;
                    break;
                case "smallIconViewResToolStripMenuItem":
                    listViewFodel.View = View.SmallIcon;
                    myResourceCurrentItem.ListShowView = View.SmallIcon;
                    break;
                case "listViewResToolStripMenuItem":
                    listViewFodel.View = View.List;
                    myResourceCurrentItem.BreviaryMap = false;
                    break;
                case "detailsViewResToolStripMenuItem":
                    listViewFodel.View = View.Details;
                    myResourceCurrentItem.BreviaryMap = false;
                    break;
            }
            if (myResourceCurrentItem != null && myResourceCurrentItem is MyListFolderItem && !myResourceCurrentItem.BreviaryMap)
            {//保存该层文件夹下的显示模式
                (myResourceCurrentItem as MyListFolderItem).ListShowView = listViewFodel.View;
            }
            //else if (myResourceCurrentItem.BreviaryMap)
            //{//为缩略图模式
            //    resourceFilesListView.Items.Clear();
            //}

            InitListView(myResourceCurrentItem, listViewFodel, _resourceCurrentType);

            SetMenuItemMutex(menuItem);
        }

        /// <summary>
        /// 为欲保存的图片选择路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFodel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewFodel.SelectedItems.Count > 0)
            {
                MyListItem myitem = listViewFodel.SelectedItems[0] as MyListItem;
                if (myitem is MyListFileItem)
                {
                    #region 替换该文件

                    if (SaveOrReplaceFile(true,((MyListFileItem)myitem).Element.FileName))
                    {
                        this.DialogResult = DialogResult.OK;
                    }
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
                    listViewFodel.SmallImageList.Images.Clear();
                    listViewFodel.LargeImageList.Images.Clear();
                    InitListView(myResourceCurrentItem, listViewFodel, _resourceCurrentType);
                    parentResToolStripButton.Enabled = true;// (myitem.Element.AbsoluteFilePath != Service.Sdsite.CurrentDocument.Resources.AbsoluteFilePath);
                    SetResourceMenuCheckState(myResourceCurrentItem);

                    #endregion
                }
            }
        }

        /// <summary>
        /// 上一级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parentResToolStripButton_Click(object sender, EventArgs e)
        {
            if (myResourceCurrentItem is MyListFolderItem)
            {
                listViewFodel.LargeImageList.Images.Clear();
                listViewFodel.SmallImageList.Images.Clear();

                MyListItem myCurrent = (myResourceCurrentItem as MyListFolderItem).FolderItemParant;

                if (!myCurrent.BreviaryMap)
                {
                    listViewFodel.LargeImageList.ImageSize = new Size(32, 32); //不是缩略图的恢复大小
                }
                if (myCurrent.FolderItemParant == null)
                {
                    parentResToolStripButton.Enabled = false; //到根了
                    InitListView(resourceFilePaths[0], listViewFodel, _resourceCurrentType);
                    myResourceCurrentItem = resourceFilePaths[0];
                }
                else
                {
                    myResourceCurrentItem = myCurrent;
                    InitListView(myCurrent, listViewFodel, _resourceCurrentType);
                }
                SetResourceMenuCheckState(myResourceCurrentItem);
            }
        }

        #endregion

        #region 自定义事件

        #endregion
    }
}
