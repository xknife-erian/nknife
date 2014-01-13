using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Runtime.InteropServices;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Web;

namespace Gean
{
    /// <summary>
    /// 一些简单的基于网络的小型扩展方法
    /// </summary>
    public sealed class UtilityNet
    {
        #region netapi32.dll
        [DllImport("netapi32.dll", EntryPoint = "NetMessageBufferSend", CharSet = CharSet.Unicode)]
        private static extern int NetMessageBufferSend(
          string servername,
          string msgname,
          string fromname,
          [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 4)] byte[] buf,
          [MarshalAs(UnmanagedType.U4)] int buflen);
        #endregion

        /// <summary>
        /// 系统信使服务:发送消息
        /// </summary>
        /// <param name="fromName">发送人</param>
        /// <param name="toName">接收人(机器名或者IP)</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public static bool MessageBufferSend(string fromName, string toName, string message)
        {
            byte[] buf = Encoding.Unicode.GetBytes(message);
            return NetMessageBufferSend(null, toName, fromName, buf, buf.Length) == 0;
        }

        /// <summary>
        /// 获取本机的子网掩码。
        /// 如果无法获取，将返回Null值。
        /// </summary>
        public static IPAddress GetSubnet()
        {
            return GetNetInformation("IPSubnet");
        }

        /// <summary>
        /// 获取本机的默认网关。
        /// 如果无法获取，将返回Null值。
        /// </summary>
        public static IPAddress GetDefaultIPGateway()
        {
            return GetNetInformation("DefaultIPGateway");
        }

        /// <summary>获取本机IP地址（V4）
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] GetLocalIpv4()
        {
            var localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            var ipCollection = new List<IPAddress>(localIPs);
            foreach (var ip in localIPs)
            {
                if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                    ipCollection.Remove(ip);
            }
            return ipCollection.ToArray();
        }

        /// <summary>获取本机的一些网络设置的相关信息
        /// </summary>
        private static IPAddress GetNetInformation(string ipType)
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementObject nic in nics)
                {
                    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                    {
                        string ipstr = (nic[ipType] as String[])[0];
                        return IPAddress.Parse(ipstr);
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 根据IP地址获得主机名称
        /// </summary>
        /// <param name="ip">主机的IP地址</param>
        /// <returns>主机名称</returns>
        public static string GetHostNameByIp(string ip)
        {
            ip = ip.Trim();
            if (ip == String.Empty)
                return String.Empty;
            try
            {
                // 是否 Ping 的通
                if (NetPing(ip))
                {
                    IPHostEntry host = Dns.GetHostEntry(ip);
                    return host.HostName;
                }
                else
                    return String.Empty;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 根据主机名（域名）获得主机的IP地址
        /// </summary>
        /// <example>
        /// GetIPByDomain("pc001"); GetIPByDomain("www.google.com");
        /// </example>
        /// <param name="hostName">主机名或域名</param>
        /// <returns>主机的IP地址</returns>
        public static string GetIpByHostName(string hostName)
        {
            hostName = hostName.Trim();
            if (hostName == String.Empty)
                return String.Empty;
            try
            {
                IPHostEntry host = Dns.GetHostEntry(hostName);
                return host.AddressList.GetValue(0).ToString();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 是否以200毫秒的间隔时间 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <returns>true 通，false 不通</returns>
        public static bool NetPing(string ip)
        {
            if (String.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException("IPAddress");
            }
            IPAddress ipaddress;
            if (!IPAddress.TryParse(ip, out ipaddress))
            {
                return false;
            }
            return NetPing(ip, 200);
        }

        /// <summary>
        /// 是否 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <param name="timeout">Ping的间隔时间，单位：毫秒</param>
        /// <returns>true 通，false 不通</returns>
        public static bool NetPing(string ip, int timeout)
        {
            Ping ping = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "Hello...";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            PingReply reply = ping.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// [ 仅供Asp.net使用, WinForm无法使用 ] 获取本机的公网IP地址
        /// </summary>
        /// <returns>返回结果集合中的[0]为公网IP地址，后续的为代理地址</returns>
        public static IPAddress[] GetInternetIPAddressByLocalhost()
        {
            List<IPAddress> iplist = new List<IPAddress>();

            string ip;// = HttpContext.Current.Request.UserHostAddress;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            iplist.Add(IPAddress.Parse(ip));

            //有代理
            string agentip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!String.IsNullOrEmpty(agentip))
            {
                if (agentip.IndexOf(".") == -1)
                    agentip = null;
                if (agentip != null)
                {
                    if (agentip.IndexOf("unknow") != -1)
                        agentip = agentip.Replace("unknow", String.Empty);

                    string[] temparyip = agentip.Replace("   ", String.Empty).Replace("'", String.Empty).Split(new char[] { ',', ';' });
                    //过滤代理格式中的非IP和内网IP
                    foreach (string str in temparyip)
                    {
                        if (String.IsNullOrEmpty(str))
                        {
                            if (str.Substring(0, 3) != "10."
                                   && str.Substring(0, 7) != "192.168"
                                   && str.Substring(0, 7) != "172.16.")
                            {
                                IPAddress i;
                                if (IPAddress.TryParse(str, out i))
                                {
                                    iplist.Add(i);
                                }
                            }
                        }
                    }
                }
            }
            return iplist.ToArray();
        }
    }
}
