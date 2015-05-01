﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace NKnife.Wrapper
{
    public class PcInterfaces
    {
        /// <summary>
        /// 获取当前计算机的串口列表
        /// </summary>
        /// <returns>以大写字符串表示串口列表</returns>
        public static StringCollection GetComList()
        {
            var list = new StringCollection();
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\SerialComm");
            if (keyCom != null)
            {
                string[] comArray = keyCom.GetValueNames();
                foreach (string com in comArray)
                {
                    var c = (string)keyCom.GetValue(com);
                    list.Add(c);
                }
            }
            return list;
        }
    }
}
