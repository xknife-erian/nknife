using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NKnife.Util;

namespace NKnife.Win.Forms.Common.Icons
{
    /// <summary>
    ///     Provides static methods to read system icons for both folders and files.
    /// </summary>
    public class IconReader
    {
        #region FolderType enum

        /// <summary>
        ///     Options to specify whether folders should be in the open or closed state.
        /// </summary>
        public enum FolderType
        {
            /// <summary>
            ///     Specify open folder.
            /// </summary>
            Open = 0,

            /// <summary>
            ///     Specify closed folder.
            /// </summary>
            Closed = 1
        }

        #endregion

        #region IconSize enum

        /// <summary>
        ///     Options to specify the size of icons to return.
        /// </summary>
        public enum IconSize
        {
            /// <summary>
            ///     Specify large icon - 32 pixels by 32 pixels.
            /// </summary>
            Large = 0,

            /// <summary>
            ///     Specify small icon - 16 pixels by 16 pixels.
            /// </summary>
            Small = 1
        }

        #endregion

        /// <summary>
        ///     Returns an icon for a given file - indicated by the name parameter.
        /// </summary>
        /// <param name="name">Pathname for file.</param>
        /// <param name="size">Large or small</param>
        /// <param name="linkOverlay">Whether to include the link icon</param>
        /// <returns>System.Drawing.Icon</returns>
        public static Icon GetFileIcon(string name, IconSize size, bool linkOverlay)
        {
            var shfi = new NKnife.Win.Api.Shell32.Shfileinfo();
            var flags = NKnife.Win.Api.Shell32.SHGFI_ICON | NKnife.Win.Api.Shell32.SHGFI_USE_FILE_ATTRIBUTES;

            if (linkOverlay)
                flags += NKnife.Win.Api.Shell32.SHGFI_LINK_OVERLAY;

            /* Check the size specified for return. */
            if (IconSize.Small == size)
                flags += NKnife.Win.Api.Shell32.SHGFI_SMALL_ICON;
            else
                flags += NKnife.Win.Api.Shell32.SHGFI_LARGE_ICON;

            NKnife.Win.Api.Shell32.SHGetFileInfo(name,
                NKnife.Win.Api.Shell32.FILE_ATTRIBUTE_NORMAL,
                ref shfi,
                (uint) Marshal.SizeOf(shfi),
                flags);

            // Copy (clone) the returned icon to a new object, thus allowing us to clean-up properly
            var icon = (Icon) Icon.FromHandle(shfi.hIcon).Clone();
            Api.User32.DestroyIcon(shfi.hIcon);// Cleanup
            return icon;
        }

        /// <summary>
        ///     Used to access system folder icons.
        /// </summary>
        /// <param name="size">Specify large or small icons.</param>
        /// <param name="folderType">Specify open or closed FolderType.</param>
        /// <returns>System.Drawing.Icon</returns>
        public static Icon GetFolderIcon(IconSize size, FolderType folderType)
        {
            // Need to add size check, although errors generated at present!
            var flags = NKnife.Win.Api.Shell32.SHGFI_ICON | NKnife.Win.Api.Shell32.SHGFI_USE_FILE_ATTRIBUTES;

            if (FolderType.Open == folderType) flags += NKnife.Win.Api.Shell32.SHGFI_OPEN_ICON;

            if (IconSize.Small == size)
                flags += NKnife.Win.Api.Shell32.SHGFI_SMALL_ICON;
            else
                flags += NKnife.Win.Api.Shell32.SHGFI_LARGE_ICON;

            // Get the folder icon
            var shfi = new NKnife.Win.Api.Shell32.Shfileinfo();
            NKnife.Win.Api.Shell32.SHGetFileInfo(null,
                NKnife.Win.Api.Shell32.FILE_ATTRIBUTE_DIRECTORY,
                ref shfi,
                (uint) Marshal.SizeOf(shfi),
                flags);

            Icon.FromHandle(shfi.hIcon); // Load the icon from an HICON handle

            // Now clone the icon, so that it can be successfully stored in an ImageList
            var icon = (Icon) Icon.FromHandle(shfi.hIcon).Clone();

            Api.User32.DestroyIcon(shfi.hIcon); // Cleanup
            return icon;
        }

        /// <summary>
        ///     刷新一个文件树所对应的所有文件与文件夹的图标
        /// </summary>
        /// <param name="treeNodes">一个文件树.</param>
        /// <param name="imageList">所有文件与文件夹的图标集合，引用关系.</param>
        public static void RefreshSourceFolderFileTreeNodeIcon(IEnumerable treeNodes, ImageList imageList)
        {
            if (imageList == null)
                imageList = new ImageList();
            foreach (TreeNode treeNode in treeNodes)
            {
                var path = treeNode.ToolTipText;
                if (string.IsNullOrWhiteSpace(path))
                    continue;
                if (UtilFile.IsDirectory(path)) //当是目录时
                {
                    if (!imageList.Images.ContainsKey(FolderType.Open.ToString()))
                    {
                        var closedIcon = GetFolderIcon(IconSize.Small, FolderType.Closed);
                        var openIcon = GetFolderIcon(IconSize.Small, FolderType.Open);
                        imageList.Images.Add(FolderType.Closed.ToString(), closedIcon);
                        imageList.Images.Add(FolderType.Open.ToString(), openIcon);
                    }

                    treeNode.SelectedImageKey = FolderType.Open.ToString();
                    treeNode.ImageKey = FolderType.Closed.ToString();
                    if (treeNode.Nodes.Count > 0) //判断是否有子目录，进行递归
                        RefreshSourceFolderFileTreeNodeIcon(treeNode.Nodes, imageList);
                }
                else if (File.Exists(path)) //当是文件时
                {
                    var ext = Path.GetExtension(path);
                    if (string.IsNullOrWhiteSpace(ext) || ext.Equals(".exe"))
                        ext = path; //可执行文件的图标都不一样；没有扩展名的文件的图标就不讲了,只能浪费一些内存了
                    ext = ext.ToLower();
                    if (!imageList.Images.ContainsKey(ext))
                    {
                        Icon icon;
                        if (File.Exists(path))
                            icon = GetFileIcon(path, IconSize.Small, false);
                        else
                            icon = GetFolderIcon(IconSize.Small, FolderType.Closed);
                        imageList.Images.Add(ext, icon);
                    }

                    treeNode.ImageKey = ext;
                    treeNode.StateImageKey = ext;
                    treeNode.SelectedImageKey = ext;
                }
            }
        }
    }
}