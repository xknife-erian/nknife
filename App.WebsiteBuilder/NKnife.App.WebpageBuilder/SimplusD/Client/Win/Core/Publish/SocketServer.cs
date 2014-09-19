using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Configuration;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 上传
    /// </summary>
    public class SocketServer
    {
        #region  声明
        TcpClient _tcpClient;
        NetworkStream _stream;
        //modified by zhangling on June 2,2008 此类已撤销
        // UpFileXmlDoc _upFileXmlDoc; 
        List<string> _upFileList;
        ModifyPublish _modifyPublish;
        /// <summary>
        /// 用来修改状态的字典，key是相对路径，value是文件的id(在DisposeAnyFile里生成，通过构造函数传过来的
        /// </summary>
        Dictionary<string, string> _publishDictionary;
        static string strServerIp;
        static string strServerPort;
        IPAddress serverIp;
        int serverPort;
        ProgressStateForm _progressState;
        public int TotleFileBytes { get; private set; }
        public int SendedFileBytes { get; private set; }
        /// <summary>
        /// 发布过程的状态
        /// </summary>
        public PublishState State { get; private set; }
        /// <summary>
        /// 发送数据的线程
        /// </summary>
        public Thread SendThread { get; private set; }
        /// <summary>
        /// 接收服务器返回线程
        /// </summary>
        public Thread ReceiveThread { get; private set; }
        /// <summary>
        /// 是否正在工作着
        /// </summary>
        public bool IsWorking { get; private set; }

        const int OnceBytesCount = 1024;
        #endregion

        #region 构造
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="upFileXmlDoc">上传文件类的引用</param>
        /// <param name="publishDictionary">保存上传文件的集合</param>
        public SocketServer(List<string> upFileList, Dictionary<string, string> publishDictionary)
        {
            /*add a definition by zhangling on May 28,2008
             * 1.上传文件的ip，及端口的读取
             * 2.上传文件的列表的接收
             */
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            strServerIp = config.AppSettings.Settings["ServerIp"].Value;
            strServerPort = config.AppSettings.Settings["ServerPort"].Value;
            serverIp = IPAddress.Parse(strServerIp);
            serverPort = Convert.ToInt32(strServerPort);
            ////modified by zhangling on June 2,2008
            //_upFileXmlDoc = upFileXmlDoc;
            _upFileList = upFileList;
            _publishDictionary = publishDictionary;
            _modifyPublish = new ModifyPublish(_publishDictionary);
        }
        #endregion

        /// <summary>
        /// 发布的入口函数
        /// </summary>
        /// <returns></returns>
        public void Upload()
        {
            IsWorking = true;
            ///建立一个连接
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(serverIp, serverPort);
            }
            catch
            {
                MessageService.Show("无法连接服务器！");
                return;
            }
            _stream = _tcpClient.GetStream();

            ///向服务端发送User型数据包，鉴权，同时给Server传递一些信息(网站id，文件数等)
            if (!SendUserMessage())
            {
                MessageService.Show("用户验证失败！");
                return;
            }

            ///启动接收服务器响应线程
            ReceiveThread = new Thread(new ThreadStart(ReceiveThreadMethod));
            ReceiveThread.IsBackground = true;
            ReceiveThread.Start();

            
            ///按列表开始上传文件
            BeginSendFileMessage();

            ///启动滚动条
            _progressState = new ProgressStateForm(this);
            _progressState.ShowDialog(Service.Workbench.MainForm);
        }

        /// <summary>
        /// 发送用户信息
        /// </summary>
        private bool SendUserMessage()
        {
            #region 参数
            string userName = Service.User.UserID;
            string passport = Service.User.Passport;
            int filesCount = _upFileList.Count;
            string siteName = Service.Sdsite.CurrentDocument.SdsiteName.ToLower();
            string siteGuid = Service.Sdsite.CurrentDocument.DocumentElement.GetAttribute("id");
            #endregion

            FirstMessageBag firstMsgBag = new FirstMessageBag(
                "1.0", userName, passport, filesCount, siteName, siteGuid);

            //此时，需要接收到服务器端验证后的结果。才能断定是否要继续工作
            SendMessageBag(firstMsgBag);
          
            MessageBag firstReceiveMsg = ReceiveServiceMessage(_stream);

            if (firstReceiveMsg.Head.Type == (int)MessageType.User)
            {
                if (firstReceiveMsg.Head.State == 1)
                {
                    return true;
                }
                else if (firstReceiveMsg.Head.State == 0)
                {
                    //用户名的账号及通行证是错误（有可以是此用户在其它的地方也在使用，中途被他人搞坏掉）
                    return false;
                }
                return false;
            }
            else
            {
                return false;
            }
 
        }

        /// <summary>
        /// 发送消息包到服务器
        /// </summary>
        /// <returns></returns>
        private void SendMessageBag(MessageBag message)
        {
            byte[] buffer = message.ToBytes();
            _stream.Write(buffer, 0, buffer.Length);
        }
        private void SendMessageBag(FileMessageBag message)
        {
            byte[] buffer = message.ToBytes();

            int iOffset = 0;
            while (iOffset < buffer.Length)
            {
                int countWillSend = Math.Min(buffer.Length - iOffset, OnceBytesCount);
                _stream.Write(buffer, iOffset, countWillSend);
                iOffset += countWillSend;

                SendedFileBytes += countWillSend;
            }
        }

        /// <summary>
        /// 开始上传文件
        /// </summary>
        private void BeginSendFileMessage()
        {
            ///初始化一些参数,主要是供滚动条使用
            State = PublishState.Init;
            CountTotleFileBytes();
            SendedFileBytes = 0;
            IsWorking = true;

            SendThread = new Thread(new ThreadStart(SendFileMessage));
            SendThread.IsBackground = true;
            SendThread.Start();
        }

        /// <summary>
        /// 按列表上传文件
        /// </summary>
        private void SendFileMessage()
        {
            ///更改发送状态为正在发送中
            State = PublishState.Sending;

            foreach (string upUrl in _upFileList)
            {
                byte[] bytes = GetFileBytes(upUrl);
                try
                {
                    if (bytes != null)
                    {
                        FileMessageBag fileMsgBag = new FileMessageBag(upUrl, bytes);
                        SendMessageBag(fileMsgBag);
                    }
                    else
                    {
                        //返回空，文件不存在
                        new Exception(upUrl + "文件不存在！");
                    }
                }
                catch (Exception ex)
                {
                    CallbackFail(ex);
                }
            }

            ///更改发送状态为发送完成
            State = PublishState.SendFlush;
        }

        /// <summary>
        /// 接收服务器端返回的消息包
        /// </summary>
        private MessageBag ReceiveServiceMessage(NetworkStream stream)
        {
            byte[] buffer = Utility.Stream.ReadStream(stream, MessageHead.HeadLength);
            if (buffer.Length == 0)
            {
                return null;
            }

            MessageHead head = MessageHead.Parse(buffer);
            if (head.BodyLength > 0)
            {
                byte[] bytesBody = Utility.Stream.ReadStream(stream, head.BodyLength);

                return new MessageBag(head, bytesBody);
            }

            return new MessageBag(head);
        }

        /// <summary>
        /// 解析服务器返回的用户验证后的消息包
        /// </summary>
        /// <param name="bag"></param>
        /// <returns></returns>
        private void AnalyzeResponeMessage(MessageBag bag)
        {
            MessageType responseType = (MessageType)bag.Head.Type;
            
            switch (responseType)
            {
                case MessageType.None:
                    break;

                case MessageType.FileReceive:
                    if (bag.Head.State == 0)
                    {
                        IsWorking = false;
                        _progressState.Close();
                        MessageService.Show("服务器接收失败！");
                    }
                    else if (bag.Head.State == 1)
                    {
                        //更改发送状态为发送完成
                        State = PublishState.SendSuccess;
                        CallbackSendEnd();
                    }
                    else if (bag.Head.State == 2)
                    {
                        //服务器端成功接收文件后，会返回文件名
                        //客户端将会改变此对应节点的一些状态
                        byte[] getFileUrl = bag.BytesBody;
                        string fileUrl = Encoding.UTF8.GetString(getFileUrl);

                        _modifyPublish.ExecuteModifyPublish(fileUrl);
                    }
                    break;
                case MessageType.FileTidy:
                    break;

                case MessageType.SiteBuild:
                    if (bag.Head.State == 0)
                    {
                        IsWorking = false;
                        _progressState.Close();
                        if (MessageService.Show("服务器错误！是否将本地版本退回到此次发布前状态？", MessageBoxButtons.OKCancel)
                            == DialogResult.OK)
                        {
                            RollBack();
                        }
                    }
                    else if (bag.Head.State == 1)
                    {
                        IsWorking = false;
                        //更改发送状态为发送完成
                        State = PublishState.AllSuccess;
                        Service.Workbench.ReloadTree();
                        _progressState.Close();
                        Application.DoEvents();

                        AfterBuildSuccess(bag.BytesBody);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 异步读取服务器端返回的消息包
        /// </summary>
        private void ReceiveThreadMethod()
        {
            //_progressState.Refresh();
            while (true)
            {
                try
                {
                    MessageBag messageBag = ReceiveServiceMessage(_stream);
                    if (messageBag == null)
                    {
                        continue;
                    }
                    Service.Workbench.MainForm.BeginInvoke(new Action<MessageBag>(AnalyzeResponeMessage), messageBag);
                    
                    //结束线程
                    if (messageBag.Head.Type == (int)MessageType.SiteBuild)
                    {
                        if (messageBag.Head.State == 1)
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    CallbackFail(ex);
                }
            }
        }

        #region 方法
        /// <summary>
        /// 获取文件的字节
        /// </summary>
        private byte[] GetFileBytes(string relativeUrl)
        {
            //modified by zhangling on2008年6月5日
            //string filePath = Path.Combine(PathService.Site_Root_Folder.Substring(0, PathService.Site_Root_Folder.Length - 6), relativeUrl);
            string filePath = Path.Combine(Service.Project.ProjectPath, relativeUrl);
         
            if (File.Exists(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                return fileBytes;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取需上传文件的字节数
        /// </summary>
        /// <returns></returns>
        private void CountTotleFileBytes()
        {
            //modified by zhucai on 2008年6月5日
            ///获取.sdsite的长度
            //FileInfo sdsite = new FileInfo(Service.Sdsite.CurrentDocument.AbsoluteFilePath);
            //TotleFileBytes = Convert.ToInt32(sdsite.Length);
            
            TotleFileBytes = GetFileLength(Service.Sdsite.CurrentDocument.SdsiteName + Utility.Const.SdsiteFileExt);

            ///获取列表中的文件的长度
            foreach (string fileUrl in _publishDictionary.Keys)
            {
                TotleFileBytes += GetFileLength(fileUrl);
            }
        }

        /// <summary>
        /// 返回单个文件消息包的长度
        /// </summary>
        /// <param name="relativeFile"></param>
        /// <returns></returns>
        private int GetFileLength(string relativeFile)
        {
            //modified by zhangling on 2008年6月5日
            string fileAbsPath = Path.Combine(Service.Project.ProjectPath, relativeFile);
            FileInfo fileInfo = new FileInfo(fileAbsPath);

            ///消息头+文件名长度(4)+文件名占用在字节数+文件占用的字节数
            return MessageHead.HeadLength + 4 + Encoding.UTF8.GetByteCount(relativeFile) + (int)fileInfo.Length;
        }

        #endregion

        #region 服务器返回消息后的处理
        /// <summary>
        /// 生成成功后，是否浏览
        /// </summary>
        private void AfterBuildSuccess(byte[] msgBody)
        {
            if (MessageService.Show("发布成功！\r\n\r\n是否现在浏览？", MessageBoxButtons.OKCancel)
                == DialogResult.OK)
            {
                //判断网站是不有新名返回
                string siteName = "";
                string body = Encoding.UTF8.GetString(msgBody);
                if (string.IsNullOrEmpty(body))
                {
                    siteName = Service.Sdsite.CurrentDocument.SdsiteName;
                }
                else
                {
                    siteName = body;
                }
                string url = @"http://{0}.{1}.{2}";
                url = string.Format(url, siteName, Service.User.UserID, "jeelu.net");
                Process.Start(url);
            }
        }

        #endregion

        /// <summary>
        /// 强制取消
        /// </summary>
        public void Abort()
        {
            if (SendThread != null)
            {
                SendThread.Abort();
            }
            if (ReceiveThread != null)
            {
                ReceiveThread.Abort();
            }
        }

        private void CallbackFail(Exception ex)
        {
            IsWorking = false;
            _progressState.InvokeClose();

            if (ex is ThreadAbortException)
            {
                return;
            }
            MessageService.Show("出现未知错误。\r\n\r\n错误信息:" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CallbackFail(string msg)
        {
            IsWorking = false;
            _progressState.InvokeClose();
            MessageService.Show(msg, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 回滚之前的操作
        /// </summary>
        private void RollBack()
        {
            try
            {
                return;
            }
            catch (Exception)
            {
                MessageService.Show("回滚失败！");
            }
        }

        private void CallbackSendEnd()
        {
            ///上传已成功，做发布后的处理
            _modifyPublish.ExecuteAfterPublish();
        }

    }

    /// <summary>
    /// 发布状态
    /// </summary>
    public enum PublishState
    {
        None,
        Init,
        Sending,
        SendFlush,
        SendSuccess,
        AllSuccess,
    }
}