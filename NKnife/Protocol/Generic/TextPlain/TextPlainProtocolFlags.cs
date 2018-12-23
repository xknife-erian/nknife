using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Protocol.Generic.TextPlain
{
    class TextPlainProtocolFlags
    {
        public static char _SplitFlag = '|';
        public static string _InformationSplitFlag = "<~>";

        public static char SplitFlag
        {
            get => _SplitFlag;
            set => _SplitFlag = value;
        }

        public static string InformationSplitFlag
        {
            get => _InformationSplitFlag;
            set => _InformationSplitFlag = value;
        }
    }
}
