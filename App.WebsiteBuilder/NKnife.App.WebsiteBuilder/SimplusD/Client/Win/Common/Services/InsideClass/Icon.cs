using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Resources;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Jeelu.SimplusD.Client.Win
{
    public enum GetSystemIconType
    {
        ExtensionSmall,
        ExtensionLarge,
        PathSmall,
        PathLarge,
    }

    public static partial class Service
    {
        public static class Icon
        {
            static ImageList _imageList = new ImageList();

            static public Image GetImg(string key)
            {
                return _imageList.Images[key];
            }

            static public System.Drawing.Icon GetValidateIcon(bool isSuccess)
            {
                if (isSuccess)
                    return new System.Drawing.Icon(System.IO.Path.Combine(PathService.Icon_Folder, @"right.ico"));
                else
                    return new System.Drawing.Icon(System.IO.Path.Combine(PathService.Icon_Folder, @"wrong.ico"));
            }

            #region 获取系统图标
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
            #endregion

            /* 读出资源文件中的ICO对象
        using (IResourceReader reader = new ResourceReader(PathService.ResourceIcoLib))
        {
            /// 枚举资源文件所有对象
            IDictionaryEnumerator iden = reader.GetEnumerator();
            while (iden.MoveNext())
            {
                _imageList.Images.Add((string)iden.Key, ((Icon)iden.Value).ToBitmap());
            }
        }*/
        }
    }
}