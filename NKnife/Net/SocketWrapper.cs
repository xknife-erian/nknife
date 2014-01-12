using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NKnife.Net
{
    public class SocketWrapper
    {
        //与服务器的连接
        public TcpClient TcpClient;
        //与服务器数据交互的流通道
        public NetworkStream Stream;

        //构造函数
        public SocketWrapper(string hostStr, string hostPort)
        {
            //创建一个客户端套接字，它是Login的一个公共属性，
            TcpClient = new TcpClient {ReceiveTimeout = 5000, SendTimeout = 5000};
            //向指定的IP地址的服务器发出连接请求
            TcpClient.Connect(IPAddress.Parse(hostStr),
                Int32.Parse(hostPort));
            //获得与服务器数据交互的流通道（NetworkStream)
            Stream = TcpClient.GetStream();
            Stream.ReadTimeout = 5000;
            Stream.WriteTimeout = 5000;
        }

        //发送命令串
        public bool SendStr(string cmd)
        {
            try
            {
                //将字符串转化为字符数组
                Byte[] outbytes = Encoding.Default.GetBytes(
                    cmd.ToCharArray());
                Stream.Write(outbytes, 0, outbytes.Length);
                //_Logger.Info( "发送：" + cmd);
                return true;
            }
            catch (Exception err)
            {
                //_Logger.Warn( "程序出现异常.", err);
                return false;
            }
        }

    }
}
