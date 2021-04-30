using System.Runtime.InteropServices;

namespace NKnife.App.XPath
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct Verbose
    {
        public static readonly string childText;
        public static readonly string attrText;
        public static readonly string selfNodeText;
        public static readonly string parentNodeText;

        public static string GetPositionText(int Pos)
        {
            return ("position()=" + Pos);
        }

        static Verbose()
        {
            childText = "child::";
            attrText = "attribute::";
            selfNodeText = "self::node()";
            parentNodeText = "parent::node()";
        }
    }
}