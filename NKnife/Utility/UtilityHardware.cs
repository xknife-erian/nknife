using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Microsoft.Win32;
using NKnife.Entities;

namespace NKnife.Utility
{
    public class UtilityHardware
    {
        private static CPUInfo[] _infos;
        private static string[] _macAddressArray;

        /// <summary>
        ///     获取系统串口数量
        /// </summary>
        /// <returns></returns>
        public static string[] GetSerialCommList()
        {
            var keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            return keyCom?.GetValueNames();
        }

        /// <summary>
        ///     获取指定编号的CPU信息
        /// </summary>
        public static CPUInfo GetCPUInfo(int n = 0)
        {
            if (_infos == null)
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                var collection = searcher.Get();
                _infos = new CPUInfo[collection.Count];
                var i = 0;
                foreach (var o in collection)
                {
                    var mo = (ManagementObject) o;
                    _infos[i] = new CPUInfo();
                    try
                    {
                        object propertyValue;

                        try
                        {
                            propertyValue = mo.GetPropertyValue("ProcessorId");
                            if (propertyValue != null)
                                _infos[i].ProcessorId = propertyValue.ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无ProcessorId属性");
                        }

                        #region 更多属性

                        try
                        {
                            propertyValue = mo.GetPropertyValue("CurrentVoltage");
                            if (propertyValue != null)
                                _infos[i].CurrentVoltage = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无CurrentVoltage属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("ExtClock");
                            if (propertyValue != null)
                                _infos[i].ExtClock = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无ExtClock属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("L2CacheSize");
                            if (propertyValue != null)
                                _infos[i].L2CacheSize = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无L2CacheSize属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("Manufacturer");
                            if (propertyValue != null)
                                _infos[i].Manufacturer = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("Name");
                            if (propertyValue != null)
                                _infos[i].Name = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Name属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("Version");
                            if (propertyValue != null)
                                _infos[i].Version = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无Version属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("LoadPercentage");
                            if (propertyValue != null)
                                _infos[i].LoadPercentage = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无LoadPercentage属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("MaxClockSpeed");
                            if (propertyValue != null)
                                _infos[i].MaxClockSpeed = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无MaxClockSpeed属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("CurrentClockSpeed");
                            if (propertyValue != null)
                                _infos[i].CurrentClockSpeed = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无CurrentClockSpeed属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("AddressWidth");
                            if (propertyValue != null)
                                _infos[i].AddressWidth = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无AddressWidth属性");
                        }
                        try
                        {
                            propertyValue = mo.GetPropertyValue("DataWidth");
                            if (propertyValue != null)
                                _infos[i].DataWidth = propertyValue.ToString();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("无DataWidth属性");
                        }

                        #endregion

                        mo.Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("获取计算机信息异常", e.Message);
                    }
                    i++;
                }
                if (_infos.Length > 0)
                {
                    var mos = new ManagementObjectSearcher("Select * FROM Win32_BaseBoard");
                    foreach (var o in mos.Get())
                    {
                        var mo = (ManagementObject) o;
                        object propertyValue;

                        try
                        {
                            propertyValue = mo.GetPropertyValue("Manufacturer");
                            if (propertyValue != null)
                                foreach (var cpuInfo in _infos)
                                    cpuInfo.CurrBaseBoard.Manufacturer = propertyValue.ToString();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }

                        try
                        {
                            propertyValue = mo.GetPropertyValue("Product");
                            if (propertyValue != null)
                                foreach (var cpuInfo in _infos)
                                    cpuInfo.CurrBaseBoard.Product = propertyValue.ToString();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }

                        try
                        {
                            propertyValue = mo.GetPropertyValue("SerialNumber");
                            if (propertyValue != null)
                                foreach (var cpuInfo in _infos)
                                    cpuInfo.CurrBaseBoard.SerialNumber = propertyValue.ToString().Trim();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("无Manufacturer属性");
                        }
                        mo.Dispose();
                    }
                }
            }
            if (_infos != null && n < _infos.Length)
                return _infos[n];
            return new CPUInfo();
        }

        /// <summary>
        ///     获取CPU编号
        /// </summary>
        public static string GetCPUId(int n = 0)
        {
            var cpuId = GetCPUInfo(n).ProcessorId;
            return !string.IsNullOrWhiteSpace(cpuId) ? cpuId : "CPU0";
        }

        /// <summary>
        ///     获取本机的Mac地址
        /// </summary>
        public static string GetMacAddress(int n = 0)
        {
            if (_macAddressArray == null)
            {
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                var moc = mc.GetInstances();
                var maclist = new List<string>();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject) o;
                    var enable = false;
                    object propertyValue;
                    try
                    {
                        propertyValue = mo.GetPropertyValue("IPEnabled");
                        if (propertyValue != null)
                            bool.TryParse(propertyValue.ToString(), out enable);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("无IPEnabled属性");
                    }
                    if (enable)
                        try
                        {
                            propertyValue = mo.GetPropertyValue("MacAddress");
                            if (propertyValue != null)
                                maclist.Add(propertyValue.ToString());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("无MacAddress属性");
                        }
                    mo.Dispose();
                }
                _macAddressArray = maclist.ToArray();
            }
            if (_macAddressArray != null && n < _macAddressArray.Length)
            {
                var mac = _macAddressArray[n];
                return !string.IsNullOrWhiteSpace(mac) ? mac : "MAC0";
            }
            return "MAC99";
        }

        /// <summary>
        ///     获取第一块硬盘编号
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskId()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            var moc = searcher.Get();
            return (from ManagementObject mo in moc select mo["SerialNumber"].ToString().Trim()).FirstOrDefault();
        }
    }
}