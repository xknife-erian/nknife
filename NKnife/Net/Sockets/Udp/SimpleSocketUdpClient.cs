using System.Net;

namespace NKnife.Net.Sockets.Udp
{
    /// <summary>
    /// FileName: SimpleSocketUdpClient.cs
    /// CLRVersion: 4.0.30319.18052
    /// Author: 黄阳 HuangYang@p-an.com
    /// Corporation:p-an
    /// Description:
    /// DateTime: 2013/7/30 星期二 12:12:55
    /// </summary>
    public class SimpleSocketUdpClient
    {
        private IPEndPoint ipLocalPoint; 
        private EndPoint RemotePoint;
        private System.Net.Sockets.Socket mySocket;  
        private bool RunningFlag = false; 
 
        

        private string getIPAddress()  
        {   // 获得本机局域网IP地址   
            IPAddress[] AddressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;  
            if (AddressList.Length < 1)  
            {   return ""; 
            }   
            return AddressList[0].ToString();  
        }  
    }
}
