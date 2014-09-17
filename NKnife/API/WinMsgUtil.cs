using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NKnife.Platform.Win32
{
    /// <summary>
    ///     WM_COPYDATA消息所要求的数据结构
    /// </summary>
    public struct CopyDataStruct
    {
        public int cbData;
        public IntPtr dwData;

        [MarshalAs(UnmanagedType.LPStr)] public string lpData;
    }

    /// <summary>
    ///     本类封装了一些进程间通讯的细节
    /// </summary>
    public class WinMsgUtil
    {
        public const int WM_COPYDATA = 0x004A;

        //通过窗口的标题来查找窗口的句柄
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        //在DLL库中的发送消息函数
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage
            (
            int hWnd, // 目标窗口的句柄 
            int Msg, // 在这里是WM_COPYDATA
            int wParam, // 第一个消息参数
            ref CopyDataStruct lParam // 第二个消息参数
            );

        /// <summary>
        ///     发送消息，只能传递一个自定义的消息ID和消息字符串，想传一个结构，但没成功 目标进程名称，如果有多个，则给每个都发送
        ///     自定义数据，可以通过这个来决定如何解析下面的strMsg传递的消息，是一个字符串
        /// </summary>
        /// <param name="toWndHandler"></param>
        /// <param name="strMsg"></param>
        public static void SendMessage(int toWndHandler, string strMsg)
        {
            string s = toWndHandler.ToString();
            if (strMsg == null) return;

            int toWindowHandler = FindWindow(null, "置顶自动观测站服务"); //获取目标窗口句柄方法一

            //获取目标窗口句柄方法二
            //Process[] foundProcess = Process.GetProcessesByName("TopInfo.Metevation.Controller.Services.exe");
            //foreach (Process p in foundProcess)
            //{
            //    toWndHandler = p.MainWindowHandle.ToInt32();
            //}

            // SystemEventLog.Log.WriteEntry(s + "获得句柄aaa" + toWndHandler);

            CopyDataStruct cds;
            cds.dwData = (IntPtr) 100; //这里可以传入一些自定义的数据，但只能是4字节整数     
            cds.lpData = strMsg; //消息字符串
            cds.cbData = Encoding.Default.GetBytes(strMsg).Length + 1; //注意，这里的长度是按字节来算的

            //发送方的窗口的句柄, 由于本系统中的接收方不关心是该消息是从哪个窗口发出的，所以就直接填0了
            int fromWindowHandler = 0;
            SendMessage(toWndHandler, WM_COPYDATA, fromWindowHandler, ref cds);
        }

        /// 接收消息，得到消息字符串
        /// System.Windows.Forms.Message m
        /// 接收到的消息字符串
        public static string ReceiveMessage(ref Message m)
        {
            var cds = (CopyDataStruct) m.GetLParam(typeof (CopyDataStruct));
            return cds.lpData;
        }
    }
}