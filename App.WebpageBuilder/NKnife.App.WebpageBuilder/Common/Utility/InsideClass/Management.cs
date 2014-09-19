using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Jeelu
{
    static public partial class Utility
    {
        /// <summary>
        /// 表示公共信息模型 (CIM) 管理类。主要是对本机的一些信息的管理，如Mac地址，IP地址等。
        /// </summary>
        public static class Management
        {
            /// <summary>
            /// 获得本机的Mac地址
            /// </summary>
            public static string GetMacAddress()
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo["IPEnabled"].ToString() == "True")
                    {
                        return mo["MacAddress"].ToString();
                    }
                }
                return null;
            }
        }
    }
}
