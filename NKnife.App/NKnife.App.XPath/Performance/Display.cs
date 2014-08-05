using System;

namespace Performance
{

    public class Display
    {
        private static int _Indent = 0;

        public static void Show(string Message)
        {
            for (int i = 0; i < (_Indent * 4); i++)
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
            int num = _Indent * 4;
            _Indent += PostIndent;
            if (_Indent < 0)
            {
                _Indent = 0;
            }
            for (int i = 0; i < num; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(Message);
        }

        public static int Indent { set; get; }
    }
}

