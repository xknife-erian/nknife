using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class JThumbnailView : ListView
    {
        private BackgroundWorker myWorker = new BackgroundWorker();

        public event EventHandler OnLoadComplete;
 
        //public delegate void MyEventHandler(object sender, MyEventArgs e);

        private int thumbNailSize = 95;
        public int ThumbNailSize
        {
            get { return thumbNailSize; }
            set { thumbNailSize = value; }
        }

        private Color thumbBorderColor = Color.Wheat;
        public Color ThumbBorderColor
        {
            get { return thumbBorderColor; }
            set { thumbBorderColor = value; }
        }

        public bool IsLoading
        {
            get { return myWorker.IsBusy; }
        }


        private delegate void SetThumbnailDelegate(Image image);
        private void SetThumbnail(Image image)
        {
            if (Disposing) return;

            if (this.InvokeRequired)
            {
                SetThumbnailDelegate d = new SetThumbnailDelegate(SetThumbnail);
                this.Invoke(d, new object[] { image });
            }
            else
            {
                LargeImageList.Images.Add(image); //Images[i].repl 
                int index = LargeImageList.Images.Count - 1;
                Items[index - 1].ImageIndex = index;
            }
        }

        private bool canLoad = false;
        public bool CanLoad
        {
            get { return canLoad; }
            set { canLoad = value; }
        }


        private string folderName = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
        public string FolderName
        {
            get { return folderName; }
            set
            {
                if (!Directory.Exists(value))
                    throw new DirectoryNotFoundException();
                folderName = value;
                if (CanLoad)
                    ReLoadItems();
            }
        }


        public JThumbnailView()
        {
            IContainer components = new Container();
            ImageList il = new ImageList();
            il.ImageSize = new Size(thumbNailSize, thumbNailSize);
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.TransparentColor = Color.White;
            LargeImageList = il;
            components.Add(myWorker);
            myWorker.WorkerSupportsCancellation = true;
            myWorker.DoWork += new DoWorkEventHandler(bwLoadImages_DoWork);
         
            myWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myWorker_RunWorkerCompleted);
        }

        void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnLoadComplete != null)
                OnLoadComplete(this, new EventArgs());
        }

        public Image GetThumbNail(string fileName)
        {
            return GetThumbNail(fileName, thumbNailSize, thumbNailSize, thumbBorderColor);
        }

        /// <summary>
        /// modified by fenggy 2008-06-12 缩略图模仿系统的方式,但文件夹下如果有图像,和普通文件夹一样
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <param name="penColor"></param>
        /// <returns></returns>
        public static Image GetThumbNail(string fileName, int imgWidth, int imgHeight, Color penColor)
        {
            Bitmap bmp;
            int BreviaryMapX = imgWidth;
            int BreviaryMapY = imgHeight;
            try
            {
                //临时写法，到时候读配置文件
                string _strImageType = ".jpg,.png,.gif,.bmp";

                if (File.Exists(fileName))
                {//文件缩略图
                    if (_strImageType.Contains(Path.GetExtension(fileName).ToLower()))
                    {
                        bmp = new Bitmap(fileName);
                    }
                    else
                    {
                        bmp = MyIcon.GetSystemIcon(fileName, GetSystemIconType.PathLarge).ToBitmap();
                    }
                }
                else if (Directory.Exists(fileName))
                {//文件夹缩略图
                    bmp = MyIcon.GetSystemIcon(fileName, GetSystemIconType.PathLarge).ToBitmap();
                }
                else
                {
                    bmp = new Bitmap(imgWidth, imgHeight); //If we cant load the image, create a blank one with ThumbSize
                }
            }
            catch
            {
                bmp = new Bitmap(imgWidth, imgHeight);
            }


            imgWidth = bmp.Width > imgWidth ? imgWidth : bmp.Width;
            imgHeight = bmp.Height > imgHeight ? imgHeight : bmp.Height;

            //Bitmap retBmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format64bppPArgb);

            //Graphics grp = Graphics.FromImage(retBmp);


            int tnWidth = imgWidth, tnHeight = imgHeight;

            if (bmp.Width > bmp.Height)
                tnHeight = (int)(((float)bmp.Height / (float)bmp.Width) * tnWidth);
            else if (bmp.Width < bmp.Height)
                tnWidth = (int)(((float)bmp.Width / (float)bmp.Height) * tnHeight);

            //int iLeft = (imgWidth / 2) - (tnWidth / 2);
            //int iTop = (imgHeight / 2) - (tnHeight / 2);

            //grp.PixelOffsetMode = PixelOffsetMode.None;
            //grp.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //grp.DrawImage(bmp, iLeft, iTop, tnWidth, tnHeight);

            Pen pn = new Pen(penColor, 1); //Color.Wheat
            //grp.DrawRectangle(pn, 0, 0, retBmp.Width - 1, retBmp.Height - 1);

            Bitmap b = new Bitmap(BreviaryMapX, BreviaryMapY);
            Graphics g = Graphics.FromImage(b);

            g.DrawRectangle(pn, 0, 0, BreviaryMapX - 1, BreviaryMapY - 1);
            //Point pt = new Point((int)((BreviaryMapX - tnWidth) / 2), (int)(((BreviaryMapY - tnHeight) / 2)));
            int ix = (BreviaryMapX - tnWidth) / 2;
            int iy = (BreviaryMapY - tnHeight) / 2;
            g.DrawImage(bmp, new Rectangle(ix, iy, tnWidth, tnHeight), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            //g.Save();
            g.Dispose();

            return b;
        }

        private void AddDefaultThumb()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(LargeImageList.ImageSize.Width, LargeImageList.ImageSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Graphics grp = Graphics.FromImage(bmp);
            Brush brs = new SolidBrush(Color.White);
            Rectangle rect = new Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1);
            grp.FillRectangle(brs, rect);
            Pen pn = new Pen(Color.Wheat, 1);

            grp.DrawRectangle(pn, 0, 0, bmp.Width - 1, bmp.Height - 1);
            LargeImageList.Images.Add(bmp);
        }

        private void bwLoadImages_DoWork(object sender, DoWorkEventArgs e)
        {
            if (myWorker.CancellationPending) return;
            string[] fileList = (string[])e.Argument;

            foreach (string fileName in fileList)
                SetThumbnail(GetThumbNail(fileName));
        }

        public void LoadItems(string[] fileList)
        {
            if ((myWorker != null) && (myWorker.IsBusy))
                myWorker.CancelAsync();

            BeginUpdate();
            Items.Clear();
            LargeImageList.Images.Clear();
            AddDefaultThumb();

            foreach (string fileName in fileList)
            {
                ListViewItem liTemp = Items.Add(System.IO.Path.GetFileName(fileName));
                liTemp.ImageIndex = 0;
                liTemp.Tag = fileName;
            }

            EndUpdate();
            if (myWorker != null)
            {
                if (!myWorker.CancellationPending)
                    myWorker.RunWorkerAsync(fileList);
            }
        }

        private void ReLoadItems()
        {
            string strFilter = "*.jpg;*.png;*.gif;*.bmp";

            List<string> fileList = new List<string>();
            string[] arExtensions = strFilter.Split(';');

            foreach (string filter in arExtensions)
            {
                string[] strFiles = Directory.GetFiles(folderName, filter);
                fileList.AddRange(strFiles);
            }

            fileList.Sort();
            LoadItems(fileList.ToArray());
            
        }
        public void LoadItemsmy()
        {
            this.Items.Add(new ListViewItem(new string[] { "3" }, this.LargeImageList.Images.Add(GetThumbnaillFromImageFile("d:\\1.jpg", 95, 95), Color.Transparent)));
        }
        private Image GetThumbnaillFromImageFile(string fileName, int width, int imgHeight)
        {

            Image image = Image.FromFile(fileName);
            Graphics g = null;
            Bitmap bmp = null;
            float w = image.PhysicalDimension.Width;
            float h = image.PhysicalDimension.Height;
            float nw = 0;
            float nh = 0;
            if (w > h)
            {
                nw = width;
                nh = h / w * nw;
            }
            else
            {
                nh = width;
                nw = w / h * nh;
            }
            try
            {
                Image sImg = image.GetThumbnailImage((int)nw - 2, (int)nh - 2, null, new IntPtr());
                bmp = new Bitmap(width, width);
                g = Graphics.FromImage(bmp);
                Brush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
                Pen pen = new Pen(brush, 1);
                g.DrawRectangle(pen, 0, 0, width - 2, width - 2);
                g.DrawImage(sImg, ((int)(width - nw) / 2), ((int)(width - nh) / 2), (int)nw - 2, (int)nh - 2);
                g.Save();
            }
            finally
            {
                image.Dispose();
                g.Dispose();
            }
            return bmp;

        }
        
        #region 获取系统图标

        static class MyIcon
        {
            /// <summary>
            /// 获取此文件(或扩展名)的系统图标
            /// </summary>
            public static System.Drawing.Icon GetSystemIcon(string filePath, GetSystemIconType type)
            {
                System.Drawing.Icon tempIcon = null;
                try
                {
                    switch (type)
                    {
                        case GetSystemIconType.ExtensionSmall:
                            if (filePath[0] != '.')
                                tempIcon = GetSmallIcon(System.IO.Path.GetExtension(filePath));
                            else
                                tempIcon = GetSmallIcon(System.IO.Path.GetFullPath(filePath));
                            break;
                        case GetSystemIconType.ExtensionLarge:
                            if (filePath[0] != '.')
                                tempIcon = GetLargeIcon(System.IO.Path.GetExtension(filePath));
                            else
                                tempIcon = GetLargeIcon(System.IO.Path.GetFullPath(filePath));
                            break;
                        case GetSystemIconType.PathSmall:
                            tempIcon = GetSmallIconForPath(System.IO.Path.GetFullPath(filePath));
                            break;
                        case GetSystemIconType.PathLarge:
                            tempIcon = GetLargeIconForPath(System.IO.Path.GetFullPath(filePath));
                            break;
                        default:
                            throw new ArgumentException("未知的参数:" + type);
                    }
                }
                catch
                {
#if DEBUG
                    throw;
#endif
                }
                return tempIcon;
            }

            private const uint SHGFI_ICON = 0x100;
            private const uint SHGFI_LARGEICON = 0x0;
            private const uint SHGFI_SMALLICON = 0x1;
            private const uint SHGFI_USEFILEATTRIBUTES = 0x10;

            [DllImport("shell32.dll")]
            private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref   SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

            [StructLayout(LayoutKind.Sequential)]
            private struct SHFILEINFO
            {
                public IntPtr hIcon;
                public IntPtr iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            }
            private static System.Drawing.Icon GetSmallIcon(string strExtension)
            {
                SHFILEINFO shinfo = new SHFILEINFO();

                IntPtr hImgSmall = SHGetFileInfo(strExtension, 0, ref   shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON | SHGFI_USEFILEATTRIBUTES);

                return System.Drawing.Icon.FromHandle(shinfo.hIcon);
            }
            private static System.Drawing.Icon GetLargeIcon(string strExtension)
            {
                SHFILEINFO shinfo = new SHFILEINFO();

                IntPtr hImgSmall = SHGetFileInfo(strExtension, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON | SHGFI_USEFILEATTRIBUTES);

                return System.Drawing.Icon.FromHandle(shinfo.hIcon);
            }
            private static System.Drawing.Icon GetSmallIconForPath(string path)
            {
                try
                {
                    SHFILEINFO shinfo = new SHFILEINFO();

                    IntPtr hImgSmall = SHGetFileInfo(path, 0, ref   shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);

                    return System.Drawing.Icon.FromHandle(shinfo.hIcon);
                }
                catch (System.Exception)
                {
                    return System.Drawing.SystemIcons.Application;
                }
            }
            private static System.Drawing.Icon GetLargeIconForPath(string path)
            {
                SHFILEINFO shinfo = new SHFILEINFO();

                IntPtr hImgSmall = SHGetFileInfo(path, 0, ref   shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
                System.Drawing.Icon myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);

                return myIcon;
            }
        }

        enum GetSystemIconType
        {
            ExtensionSmall,
            ExtensionLarge,
            PathSmall,
            PathLarge,
        }
        
        #endregion

    }
}
