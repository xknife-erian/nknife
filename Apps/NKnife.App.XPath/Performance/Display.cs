using System;

namespace NKnife.App.XPath.Performance
{
    public class Display
    {
        private static int _Indent;

        public static int Indent { set; get; }

        public static void Show(string Message)
        {
            for (var i = 0; i < (_Indent*4); i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(Message);
        }

        public static void Show(int PreIndent, string Message, int PostIndent)
        {
            _Indent += PreIndent;
            if (_Indent < 0)
            {
                _Indent = 0;
            }
            var num = _Indent*4;
            _Indent += PostIndent;
            if (_Indent < 0)
            {
                _Indent = 0;
            }
            for (var i = 0; i < num; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(Message);
        }
    }
}