using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    public enum DataType
    {
        None            = 0,
        Channel         = 1,
        Tmplt           = 2,
        Page            = 3,
        Resources       = 4,
        Folder          = 5,
        File            = 6,
        TmpltFolder     = 7,
        Site            = 8,
    }

    /*public enum ChannelType
    {
        None            = 0,
        General         = 1,
        Language        = 2,
        Product         = 3,
        Project         = 4,
        InviteBidding   = 5,
        Knowledge       = 6,
        Hr              = 7,
        Root            = 9,
    }*/

    public enum TmpltType
    {
        None            = 0,
        General         = 1,
        Product         = 2,
        Project         = 3,
        InviteBidding   = 4,
        Knowledge       = 5,
        Hr              = 6,
        Home            = 7,
    }
    public enum PageType
    {
        None            = 0,
        General         = 1,
        Product         = 2,
        Project         = 3,
        InviteBidding   = 4,
        Knowledge       = 5,
        Hr              = 6,
        Home            = 7,
    }
    public enum FolderType
    {
        None = 0,
        TmpltFolder = 1,
        ChannelFolder = 2,
        ResourcesFolder = 3,
    }
}
