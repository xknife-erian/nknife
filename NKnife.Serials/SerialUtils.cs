using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace NKnife.Serials
{
    public static class SerialUtils
    {
        static SerialUtils()
        {
            var list = new List<int>();
            int n = 600;
            for (var i = 1; i <= 10; i++)
            {
                n = n + n;
                list.Add(n);
            }
            list.Add(57600);
            list.Add(128000);
            list.Add(256000);
            list.Add(921600);
            for (int i = 1; i < 16; i = i + 2)
            {
                list.Add(115200*i);
            }
            list.Sort();
            BaudRates = new object[list.Count];
            for (var i = 0; i < list.Count; i++)
                BaudRates[i] = list[i];
            //----------------------------------
            StopBits = new object[]
            {
                System.IO.Ports.StopBits.None,
                System.IO.Ports.StopBits.One,
                System.IO.Ports.StopBits.OnePointFive,
                System.IO.Ports.StopBits.Two
            };
            DataBits = new object[] { System.IO.Ports.DataBits.Five, System.IO.Ports.DataBits.Six, System.IO.Ports.DataBits.Seven, System.IO.Ports.DataBits.Eight };
            Parities = new object[] {Parity.Even, Parity.Mark, Parity.None, Parity.Odd, Parity.Space};
        }


        public static int DefaultBaudRate { get; private set; } = 9600;

        public static StopBits DefaultStopBit { get; private set; } = System.IO.Ports.StopBits.None;

        public static DataBits DefaultDataBit { get; private set; } = System.IO.Ports.DataBits.Eight;

        public static Parity DefaultParity { get; private set; } = Parity.None;

        public static object[] BaudRates { get; }

        public static object[] StopBits { get; private set; }

        public static object[] DataBits { get; private set; }

        public static object[] Parities { get; private set; }

        /*
        private static unsafe string ToHexCore(int len, byte[] array, char* target, bool prefix, IReadOnlyList<int> lookup)
        {
            fixed (byte* source = array)
            {
                var i = 0;
                var pIn = source;
                var pOut = target;

                if (prefix)
                {
                    *pOut++ = (char) 0x30; // '0'
                    *pOut++ = (char) 0x78; // 'x'
                }

                while (i++ < len)
                {
                    *pOut++ = (char) lookup[*pIn >> 4];
                    *pOut++ = (char) lookup[*pIn++ & 0xF];
                }
            }

            return new string(target, 0, len * 2);
        }
        */
    }
}