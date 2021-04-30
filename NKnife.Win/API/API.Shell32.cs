using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NKnife.Win
{
    public sealed partial class Api
    {
        /// <summary>
        /// Wraps necessary Shell32.dll structures and functions required to retrieve Icon Handles using SHGetFileInfo. Code
        /// courtesy of MSDN Cold Rooster Consulting case study.This code has been left largely untouched from that in the CRC example.
        /// The main changes have been moving the icon reading code over to the IconReader type.
        /// </summary>
        public class Shell32
        {
            public const int MaxPath = 256;

            // Browsing for directory.
            public const uint BifReturnonlyfsdirs = 0x0001;
            public const uint BifDontgobelowdomain = 0x0002;
            public const uint BifStatustext = 0x0004;
            public const uint BifReturnfsancestors = 0x0008;
            public const uint BifEditbox = 0x0010;
            public const uint BifValidate = 0x0020;
            public const uint BifNewdialogstyle = 0x0040;
            public const uint BifUsenewui = (BifNewdialogstyle | BifEditbox);
            public const uint BifBrowseincludeurls = 0x0080;
            public const uint BifBrowseforcomputer = 0x1000;
            public const uint BifBrowseforprinter = 0x2000;
            public const uint BifBrowseincludefiles = 0x4000;
            public const uint BifShareable = 0x8000;

            public const uint SHGFI_ICON = 0x000000100; // get icon
            public const uint ShgfiDisplayname = 0x000000200; // get display name 
            public const uint ShgfiTypename = 0x000000400; // get type name
            public const uint ShgfiAttributes = 0x000000800; // get attributes
            public const uint ShgfiIconlocation = 0x000001000; // get icon location
            public const uint ShgfiExetype = 0x000002000; // return exe type
            public const uint ShgfiSysiconindex = 0x000004000; // get system icon index
            public const uint SHGFI_LINK_OVERLAY = 0x000008000; // put a link overlay on icon
            public const uint ShgfiSelected = 0x000010000; // show icon in selected state
            public const uint ShgfiAttrSpecified = 0x000020000; // get only specified attributes
            public const uint SHGFI_LARGE_ICON = 0x000000000; // get large icon
            public const uint SHGFI_SMALL_ICON = 0x000000001; // get small icon
            public const uint SHGFI_OPEN_ICON = 0x000000002; // get open icon
            public const uint ShgfiShelliconsize = 0x000000004; // get shell size icon
            public const uint ShgfiPidl = 0x000000008; // pszPath is a pidl
            public const uint SHGFI_USE_FILE_ATTRIBUTES = 0x000000010; // use passed dwFileAttribute
            public const uint ShgfiAddoverlays = 0x000000020; // apply the appropriate overlays
            public const uint ShgfiOverlayindex = 0x000000040; // Get the index of the overlay  

            public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
            public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

            [DllImport("Shell32.dll")]
            public static extern IntPtr SHGetFileInfo(
                string pszPath,
                uint dwFileAttributes,
                ref Shfileinfo psfi,
                uint cbFileInfo,
                uint uFlags
                );

            #region Nested type: BROWSEINFO

            [StructLayout(LayoutKind.Sequential)]
            public struct Browseinfo
            {
                public IntPtr hwndOwner;
                public IntPtr pidlRoot;
                public IntPtr pszDisplayName;
                [MarshalAs(UnmanagedType.LPTStr)] public string lpszTitle;
                public uint ulFlags;
                public IntPtr lpfn;
                public int lParam;
                public IntPtr iImage;
            }

            #endregion

            #region Nested type: ITEMIDLIST

            [StructLayout(LayoutKind.Sequential)]
            public struct Itemidlist
            {
                public Shitemid mkid;
            }

            #endregion

            #region Nested type: SHFILEINFO

            [StructLayout(LayoutKind.Sequential)]
            public struct Shfileinfo
            {
                public const int Namesize = 80;
                public IntPtr hIcon;
                public int iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxPath)] public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Namesize)] public string szTypeName;
            };

            #endregion

            #region Nested type: SHITEMID

            [StructLayout(LayoutKind.Sequential)]
            public struct Shitemid
            {
                public ushort cb;
                [MarshalAs(UnmanagedType.LPArray)] public byte[] abID;
            }

            #endregion
        }
    }
}