using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public partial class XhtmlAtts
    {
        public class Style : XhtmlAttribute
        {
            public Style(CssSection value)
                : base("style", value.ToString())
            {
            }
        }
    }
}
