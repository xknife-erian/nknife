using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public enum WorkspaceType
    {
        Default     = 0,
        Content     = 1,
        Page        = 2,
        HelpPage    = 4,
        All         = Content|Page|HelpPage
    }
}
