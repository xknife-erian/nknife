﻿using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace NKnife.Channels.Serial
{
    public class SerialUtils
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
            for (int i = 1; i < 16; i = i + 2)
            {
                list.Add(115200*i);
            }
            list.Sort();
            DefaultBaudRate = (ushort) list.IndexOf(9600);
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
            DataBits = new object[] {5, 6, 7, 8};
            Parities = new object[] {Parity.Even, Parity.Mark, Parity.None, Parity.Odd, Parity.Space};
        }

        /// <summary>
        ///     本机串口,Key是串口名称,value是串口的说明(串口制造商信息等)
        /// </summary>
        public static Dictionary<string, string> LocalSerialPorts { get; } = new Dictionary<string, string>();

        public static ushort DefaultBaudRate { get; private set; }

        public static ushort DefaultStopBit { get; private set; } = 1;

        public static ushort DefaultDataBit { get; private set; } = 3;

        public static ushort DefaultParity { get; private set; } = 2;

        public static object[] BaudRates { get; }

        public static object[] StopBits { get; private set; }

        public static object[] DataBits { get; private set; }

        public static object[] Parities { get; private set; }

        /// <summary>
        ///     找到本机的所有串口
        /// </summary>
        public static void RefreshSerialPorts()
        {
            LocalSerialPorts.Clear();
            var list = SerialPort.GetPortNames();
            //调用WMI，获取Win32_PnPEntity，即所有设备
//            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
//            {
//                var hardInfos = searcher.Get();
//                foreach (var serialName in list)
//                {
//                    var des = "";
//                    foreach (var hardInfo in hardInfos)
//                    {
//                        var name = hardInfo.Properties["Name"].Value;
//                        if (name != null)
//                        {
//                            if (NameContains(name.ToString(), serialName)) //筛选
//                                des = $"{name} - {hardInfo.Properties["Manufacturer"].Value}"; //获取制造商
//                        }
//                    }
//                    LocalSerialPorts.Add(serialName, des);
//                }
//                searcher.Dispose();
//            }
        }

        private static bool NameContains(string name, string serialName)
        {
            if (name.Contains(serialName))
            {
                var index = name.IndexOf(serialName, StringComparison.Ordinal);
                if (index > 0 && name[index - 1].Equals('('))
                {
                    if (!name[index + serialName.Length].ToString().IsNumeric())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}