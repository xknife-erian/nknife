using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NKnife.Net
{
    public class CurCmd
    {
        //与服务器的连接
        public TcpClient _TcpClient;
        //与服务器数据交互的流通道
        public NetworkStream _Stream;

        //构造函数
        public CurCmd(string hostStr, string hostPort)
        {
            //创建一个客户端套接字，它是Login的一个公共属性，
            _TcpClient = new TcpClient();
            _TcpClient.ReceiveTimeout = 5000;
            _TcpClient.SendTimeout = 5000;
            //向指定的IP地址的服务器发出连接请求
            _TcpClient.Connect(IPAddress.Parse(hostStr),
                Int32.Parse(hostPort));
            //获得与服务器数据交互的流通道（NetworkStream)
            _Stream = _TcpClient.GetStream();
            _Stream.ReadTimeout = 5000;
            _Stream.WriteTimeout = 5000;

        }

        //发送命令串
        public bool SendStr(string cmd)
        {
            try
            {
                //将字符串转化为字符数组
                Byte[] outbytes = Encoding.Default.GetBytes(
                    cmd.ToCharArray());
                _Stream.Write(outbytes, 0, outbytes.Length);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

    }
}
