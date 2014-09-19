using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Jeelu.SimplusPagePreviewer
{
    public class ClientService
    {
        public static string Port
        {
            get { return _port.ToString(); }
        }
        private static int _port;

        /// <summary>
        ///随机生成端口号
        /// </summary>
        static private int CreatePort()
        {
            Random rand = new Random();
            int port = rand.Next(1025, 2048);
            return port;
        }

        /// <summary>
        /// 随机生成端口号(最多生成十次) 创建 TcpLister
        /// </summary>
        static public TcpListener CreatTcpListener()
        {
            //随机生成端口号(最多生成十次)
            TcpListener listener = null;
            for (int i = 0; i < 10; i++)
            {
                _port = CreatePort();
                listener = new TcpListener(new IPEndPoint(IPAddress.Any, _port));
                try
                {
                    listener.Start();
                    return listener;
                }
                catch (Exception e)
                {
                    ExceptionService.CreateException(e);
                }
            }
            System.Windows.Forms.MessageBox.Show("过多端口被占用,请检查!");
            Application.Exit();
            return null;
        }

    }
}
