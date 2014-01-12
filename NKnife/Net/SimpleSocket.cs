using System;
using System.Text;

namespace NKnife.Net
{
    public class SimpleSocketClient
    {
        private SocketWrapper _MyCmd;
        private readonly string _Ip;
        private readonly string _Port;

        public SimpleSocketClient(string ip,string port)
        {
            _Ip = ip;
            _Port = port;
        }

        
        //连接服务器
        public bool ConnectServer()
        {
            try
            {
                //stopFlag = true;
                if (_MyCmd != null)
                {
                    _MyCmd.TcpClient.Close();
                    _MyCmd = null;
                }
                _MyCmd = new SocketWrapper(_Ip, _Port);
                if (_MyCmd != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendStr(string cmd)
        {
            return  _MyCmd.SendStr(cmd);
        }

        //ServerResponse()方法用于接收从服务器发回的信息，
        //根据不同的命令，执行相应的操作
        public string RecvData()
        {
            //定义一个byte数组，用于接收从服务器端发送来的数据，
            //每次所能接收的数据包的最大长度为1024个字节
            byte[] buff = new byte[1024];
            string msg;
            int len;
            try
            {
                if (!_MyCmd.Stream.CanRead)
                {
                    return "";
                }

                //从流中得到数据，并存入到buff字符数组中
                try
                {
                    _MyCmd.Stream.ReadTimeout = 30 * 1000;
                    len = _MyCmd.Stream.Read(buff, 0, buff.Length);
                }
                catch
                {
                    len = 0;
                }

                if (len < 1) //没有收到任何指令的时候
                {
                    return "";
                }

                //将字符数组转化为字符串
                msg = Encoding.Default.GetString(buff, 0, len);
                msg.Trim();

                //关闭连接
                //stopFlag = true;
                _MyCmd.Stream.Close();
                _MyCmd.TcpClient.Close();
                _MyCmd = null;
                return msg;

            }
            catch (Exception err)
            {
                return "";
            }
        }
    }
}
