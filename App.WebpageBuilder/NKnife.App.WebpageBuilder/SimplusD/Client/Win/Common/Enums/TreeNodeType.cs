using System;

namespace Jeelu.SimplusD.Client.Win
{
    [Flags]
    public enum TreeNodeType
    {
        None = 0,
        SiteManager = 0x1,

        Favorite = 0x2,

        RootChannel = 0x4,
        Channel = 0x8,
        ChannelFolder = 0x10,

        ResourceFile = 0x20,
        ResourceFolder = 0x40,
        ResourceRoot = 0x80,

        Page = 0x100,

        Tmplt = 0x200,
        TmpltFolder = 0x400,
        TmpltRootFolder=0x800,

        Delete = 0x1000,
        Link = 0x2000,

        FileOutside = 0x4000,
        FolderOutsite = 0x8000,

        DataFolder = ResourceFolder | ChannelFolder | TmpltFolder,


        Snip, //add by fenggy on 2008-6-2 为了在模板视图中打开页面片
        SnipPart //add by fenggy on 2008-6-2 为了在模板视图中打开PART


        ////todo:
        //All = TreeNodeType.Root | TreeNodeType.Favorite | TreeNodeType.RecycleBin | TreeNodeType.RootChannel
        //    | TreeNodeType.Channel | TreeNodeType.ChannelFolder | TreeNodeType.ResourceFile
        //    | TreeNodeType.ResourceFolder | TreeNodeType.ResourceRoot | TreeNodeType.Page
        //    || TreeNodeType.Tmplt = 0x400,
        //TmpltFolder = 0x800,
        //TmpltRootFolder=0x1000,

        //Delete = 0x2000,
        //Link = 0x4000,

        //FileOutside = 0x8000,
        //FolderOutsite = 0x10000,
    }
}
