using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    [Flags]
    public enum ToolbarType
    {
        None            = 0,
        Standard        = 1,
        ContentEdit     = 2,
        HtmlEdit        = 4,
        NoStandard      = 8,
        NoContentEdit   = 16,
        NoHtmlEdit      = 32
    }
}
