using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Jeelu;
using System.Xml;
using System.Security.Cryptography;
using Jeelu.SimplusD.Server;
using System.Diagnostics;
using Jeelu.Data;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Server
{
    class ClientThread
    {
        #region 字段定义

        bool _isRealUser;   //该用户是否为合法用户
        int _fileCount;
        int _Total;  //该用户要发送的文件数
        string _userFilePath;
        string _projectName;
        string _changedProjName; //不管项目名是否同名，都将返回一个值
        NetworkStream _ns;

        #region 为记录日志需要的字段
        string _publisherID;
        public string _publisherIP;
        string _startTime;
        string _receivedTime;
        string _buildedTime;
        string _projectID;
        string _softwareVersion;
        string _projectVersion = "1.0"; //此处暂没处理
        string _serverVersion = Application.ProductVersion;
        int _action;                    //此次执行的动作; 1-new,2-modify,3-delete
        int _uploadFileBytesAmount;     //上传文件的总字节

        int _buildFileAmount = 0;       //生成文件数量
        #endregion

        #region 记录用户及项目的相关信息的字段
        int _size = 0;
        int _sizeOnDisk = 0;
        string _projectSavedPath = "";
        string _projectBuildedPath = "";
        int _buildedFileAmount = 0;
        int _buildedSize = 0;
        int _buildedSizeOnDisk = 0;

        DateTime _deleteTime;
        #endregion

        #endregion

        public TcpClient Client { get; private set; }

        public ClientThread(TcpClient tcpClient)
        {
            Client = tcpClient;
        }

        /// <summary>
        /// 接收到客户端请求后的程序起始点(通常在单独的线程中)
        /// </summary>
        public void ThreadCallback()
        {
            //此信息写入日志
            _startTime = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();

            DisposeUserMethod.Client = Client;
            _ns = Client.GetStream();
            GetMessage();
        }

        public void GetMessage()
        {
            //接收不同类型的消息包
            MessageBag msgBag = ReceiveClientMessage(_ns);

            //解析不同的消息包，并回应客户端
            AnalyzeClientMessage(_ns, msgBag);
        }

        /// <summary>
        /// 接收客户端的消息包
        /// </summary>
        /// <param name="stream"></param>
        private MessageBag ReceiveClientMessage(NetworkStream stream)
        {
            LogService.WriteServerRunLog(LogLevel.Info, "接收来自" + _publisherIP + "的不同类型的消息包");

            byte[] buffer = Utility.Stream.ReadStream(stream, MessageHead.HeadLength);

            MessageHead head = MessageHead.Parse(buffer);
            if (head.BodyLength > 0)
            {
                byte[] bytesBody = Utility.Stream.ReadStream(stream, head.BodyLength);
                MessageType type = (MessageType)head.Type;
                switch (type)
                {
                    case MessageType.User:
                        if (head.State == 1)
                        {
                            return new FirstMessageBag(bytesBody);
                        }
                        else
                        {
                            //2用户退出时 | //3由CGI发送的UserID
                            return new MessageBag(head, bytesBody);
                        }
                    case MessageType.File:
                        //文件是独体，没有其它状态
                        return new FileMessageBag(bytesBody);
                    default:
                        return new MessageBag(head, bytesBody);
                }
            }
            return new MessageBag(head);
        }

        /// <summary>
        /// 解析客户端发过来的消息包
        /// </summary>
        /// <param name="bag"></param>
        private void AnalyzeClientMessage(NetworkStream stream, MessageBag bag)
        {
            LogService.WriteServerRunLog(LogLevel.Info, "解析来自" + _publisherIP + "的不同类型的消息包");

            MessageType type = (MessageType)bag.Head.Type;
            switch (type)
            {
                case MessageType.User:
                    if (bag.Head.State == 1) //处理客户端传来的第一个消息包
                    {
                        FirstMessageBag firstBag = (FirstMessageBag)bag;
                        DealWithUserMessage(stream, firstBag);
                    }
                    else if (bag.Head.State == 2)  //用户退出,用户切换
                    {
                        string getUserId = Encoding.UTF8.GetString(bag.BytesBody);

                        if (CommonService.RecordUserDic.ContainsKey(getUserId))
                        {
                            CommonService.RecordUserDic.Remove(getUserId);
                        }
                    }
                    else if (bag.Head.State == 3)  //由CGI发送的UserID
                    {
                        DealWithCGIMessage(stream, bag);
                    }
                    break;
                case MessageType.File:
                    FileMessageBag fileBag = (FileMessageBag)bag;
                    DealWithFileMessage(stream, fileBag);
                    break;
                default:
                    Debug.Fail("未知在MessageType:" + type);
                    break;
            }
        }

        /// <summary>
        /// 处理用户类型消息
        /// </summary>
        private void DealWithUserMessage(NetworkStream stream, FirstMessageBag firstMsgBag)
        {
            if (DisposeUserMethod.RegistUser(
                firstMsgBag.UserName,
                firstMsgBag.UserPassport,
                firstMsgBag.FilesCount,
                firstMsgBag.SiteName,
                firstMsgBag.SiteId,
                out _Total, out _userFilePath, out _projectName, out _changedProjName, out _action))
            {
                //此信息写入日志
                _publisherID = firstMsgBag.UserName;
                _projectID = firstMsgBag.SiteId;
                _softwareVersion = firstMsgBag.SdVersion;
                _projectSavedPath = Path.Combine(AnyFilePath.SdSiteFilePath, _userFilePath);
                _projectBuildedPath = Path.Combine(AnyFilePath.SdWebFilePath, _userFilePath);

                //向客户端发送消息，表明验证是否成功
                SendResponseMessage(stream, MessageType.User, string.Empty, 1);
                _isRealUser = true;
                GetMessage();
            }
            else
            {
                SendResponseMessage(stream, MessageType.User, string.Empty, 0);
                DisposeUserMethod.DeleteUser();
            }
        }

        /// <summary>
        /// 处理文件类型消息
        /// </summary>
        private void DealWithFileMessage(NetworkStream stream, FileMessageBag fileMsgBag)
        {
            //处理直接发送file包的情况(未经过用户验证)
            if (!_isRealUser)
            {
                return;
            }

            ///将当前上传文件加入待显示的字典
            string fileUrl = fileMsgBag.FileUrl;
            DisposeUserMethod.SetUserCurrentFile(fileUrl);

            ///接收文件
            byte[] fileContent = fileMsgBag.FileBody;

            if (FileSave(fileContent, fileUrl))
            {
                /// 向客户端发送文件url，表示此文件接收成功
                SendResponseMessage(stream, MessageType.FileReceive, fileUrl, 2);

                /// 还没接收完，继续接收
                if (_fileCount != _Total)
                {
                    GetMessage();
                }
                /// 接收完全，返回OK
                else
                {
                    //此信息写入日志
                    _receivedTime = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();


                    ///给客户端发送OK消息，表示所有文件接收完成
                    SendResponseMessage(stream, MessageType.FileReceive, string.Empty, 1);

                    LogService.WriteServerRunLog(LogLevel.Info, "来自" + _publisherIP + "的所有文件接收完成,开始整理接收的网站");

                    //将接收到的文件复制到相应的位置
                    DisposeSdsite disposeSdsite = new DisposeSdsite();

                    if (!disposeSdsite.ExecuteSdsite(_userFilePath, _projectName))
                    {
                        SendResponseMessage(stream, MessageType.FileTidy, string.Empty, 0);
                        return;
                    }
                    LogService.WriteServerRunLog(LogLevel.Info, "来自" + _publisherIP + "的sdsite文件已整理完毕,开始生成可浏览的网站");

                    //接收完毕，开始生成
                    //try
                    //{
                    //    //初始一下当前网站的根路径
                    //    string initPath = AnyFilePath.GetWebAbsolutePath(_userFilePath);
                    //    ToHtmlDirectoryService.InitializePath(initPath);

                    //    BuildSite buildSite = new BuildSite();

                    //    if (!buildSite.StartBuild(_userFilePath, _projectName))
                    //    {
                    //        SendResponseMessage(stream, MessageType.SiteBuild, string.Empty, 0);
                    //        return;
                    //    }
                    //}
                    //catch (Exception e)
                    //{
                    //    ExceptionService.WriteExceptionLog(e);
                    //    SendResponseMessage(stream, MessageType.SiteBuild, string.Empty, 0);
                    //    return;
                    //}

                    LogService.WriteServerRunLog(LogLevel.Info, "来自" + _publisherIP + "的sdsite文件,其网站生成完毕");

                    //此信息写入日志
                    _buildedTime = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();


                    //生成成功   返回消息;不管网站是否有同名，都将返回一个值(空，或是新网站名，或是原本的网站名)
                    SendResponseMessage(stream, MessageType.SiteBuild, _changedProjName, 1);

                    DisposeUserMethod.DeleteUser();
                    Client.Client.Close();

                    RecordLogToTable();

                    RecordInfoToTable(disposeSdsite);
                }
            }
        }

        /// <summary>
        /// 处理CGI返回的消息; 接收的是userid,同时在返回一个guid
        /// </summary>
        /// <param name="cgiGag"></param>
        private void DealWithCGIMessage(NetworkStream stream, MessageBag cgiBag)
        {
            string userID = Encoding.UTF8.GetString(cgiBag.BytesBody);
            if (CommonService.RecordUserDic.ContainsKey(userID))
            {
                //如果帐户已存在，则删除，重新创建
                CommonService.RecordUserDic.Remove(userID);
            }
            string passport = CommonService.CreateNewGuid();

            //将其记录在服务器端
            CommonService.RecordUserDic.Add(userID, passport);

            //返回一个新的guid ;
            MessageHead head = new MessageHead((int)MessageType.User, 0, 3);
            MessageBag bag = new MessageBag(head);
            byte[] buffer = bag.ToBytes();
            stream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 返回消息到客户端
        /// </summary>
        /// <param name="_ns"></param>
        /// <param name="type">消息类型</param>
        /// <param name="message">要返回的消息</param>
        /// <param name="state">此消息类型的哪种状态</param>
        private void SendResponseMessage(NetworkStream stream, MessageType type, string message, int state)
        {
            switch (type)
            {
                case MessageType.None:
                    break;
                case MessageType.User:
                    if (state == 3)
                    {
                        //表示与CGI进行交互，此时要返回一个新的guid，作为通行证
                        ResponseFileMessage(stream, type, message, state);
                    }
                    else
                    {
                        //成功或失败
                        ResponseMessage(stream, type, state);
                    }
                    break;
                case MessageType.FileReceive:
                    if (state == 2)
                    {
                        //单个文件接收完成后，将其名称返回到客户端
                        ResponseFileMessage(stream, type, message, state);
                    }
                    else
                    {
                        ResponseMessage(stream, type, state);
                    }
                    break;
                case MessageType.FileTidy:
                    ResponseMessage(stream, type, state);
                    break;
                case MessageType.SiteBuild:
                    ResponseFileMessage(stream, type, message, state);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 发送无消息体的消息到客户端
        /// </summary>
        /// <param name="type"></param>
        private void ResponseMessage(NetworkStream stream, MessageType type, int state)
        {
            try
            {
                MessageHead head = new MessageHead((int)type, 0, state);
                MessageBag bag = new MessageBag(head);
                byte[] buffer = bag.ToBytes();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                DisposeUserMethod.DeleteUser();
                Client.Client.Close();
            }
        }

        /// <summary>
        /// 发送有消息体的消息到客户端 例如：文件接收成功，返回文件名到客户端
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="type">返回消息类型</param>
        /// <param name="message">要返回的消息体</param>
        /// <param name="state">此消息类型的哪种状态</param>
        private void ResponseFileMessage(NetworkStream stream, MessageType type, string message, int state)
        {
            try
            {
                MessageHead head = new MessageHead((int)type, 0, state);
                byte[] bodyByte = Encoding.UTF8.GetBytes(message);
                MessageBag bag = new MessageBag(head, bodyByte);

                byte[] buffer = bag.ToBytes();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                DisposeUserMethod.DeleteUser();
                Client.Client.Close();
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        private bool FileSave(byte[] fileContent, string fileUrl)
        {
            try
            {
                ///保存到临时文件夹
                ///modified by zhangling on 2008年6月12日
                string _fileUrl = Path.Combine(AnyFilePath.GetTempFolderAbsolutePath(_userFilePath), fileUrl);
                FileService.FileSave(fileContent, _fileUrl);

                ///接收到的文件数+1
                _fileCount += 1;

                ///总结接收文件的字节数
                _uploadFileBytesAmount += fileContent.Length;
                return true;
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                return false;
            }
        }

        /// <summary>
        /// 将一些用户数据记录到服务器端的日志里
        /// </summary>
        private void RecordLogToTable()
        {
            //查看此属性里是否存在单引号,如有,则错误应为空;否则正确
            if (_projectID.IndexOf("'") != -1)
            {
                //将其替换成其它符号
                _projectID = _projectID.Replace("'", "#");
            }

            if (_projectName.IndexOf("'") != -1)
            {
                //将其替换成其它符号
                _projectName = _projectName.Replace("'", "#");
            }

            string sql = @"
            insert into site_publish_info( 
                    Publisher,
                    PublisherIP,
                    StartTime,
                    ReceivedTime,
                    BuildedTime,"
                + "ProjectID,ProjectName,SoftwareVersion,ProjectVersion,ServerVersion,Action,UploadFileBytesAmount,"
                + "UploadFileAmount,BuildFileAmount) values(?publisherID,?publisherIP,?_startTime,?_receivedTime,?_buildedTime,?_projectID,?_projectName,?_softwareVersion,?_projectVersion,?_serverVersion,?_action,?_uploadFileBytesAmount,?_Total,?_buildFileAmount)";
            try
            {
                int result = DataAccess.SExecuteNonQuery(sql, _publisherID, _publisherIP, _startTime, _receivedTime, _buildedTime, _projectID, _projectName, _softwareVersion, _projectVersion, _serverVersion, _action, _uploadFileBytesAmount, _Total, _buildFileAmount);
            }
            catch (Exception ex)
            {
                LogService.WriteServerRunLog(LogLevel.Fail, ex.GetType().FullName + ":" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 记录用户及项目的相关数据到表中
        /// </summary>
        private void RecordInfoToTable(DisposeSdsite sdsite)
        {
            string sql = "select * from project where ProjectID=?_id";
            object value = DataAccess.SExecuteScalar(sql, _projectID);

            if (value != null)
            {
                //在其中一表中修改其相应的记录

                string updateSql = "update project set Publisher=?_publisher,PublisherIP=?_publisherIp,ProjectName=?name," 
                    +"SoftwareVersion=?_softwareVersion,ProjectVersion=?_projectVersion,ServerVersion=?_serverVersion,"
                    + "ProjectUpdateTime=?_startTime,IncludeFileAmount=?_includeFileAmount,Size=?_size,SizeOnDisk=?_sizeOnDisk,"
                    + "TempletAmount=?_templetAmount,ChannelAmount=?_channelAmount,PageAmount=?_pageAmount,ResourceFileAmount=?_resourceFileAmount,"
                    + "ProjectSavedPath=?_projectSavedPath,ProjectBuildedPath=?_projectBuildedPath,BuildedFileAmount=?_buildedFileAmount,"
                    + "BuildedSize=?_buildedSize,BuildedSizeOnDisk=?_buildedSizeOnDisk where ProjectID=?_projectID";
                try
                {
                    int i = DataAccess.SExecuteNonQuery(updateSql, _publisherID, _publisherIP, _projectName, _softwareVersion, _projectVersion, 
                        _serverVersion,_startTime, sdsite._includeFileAmount, _size, _sizeOnDisk, sdsite._templetAmount, sdsite._channelAmount,
                        sdsite._pageAmount, sdsite._resourceFileAmount, _projectSavedPath, _projectBuildedPath, _buildedFileAmount, _buildedSize, _buildedSizeOnDisk, _projectID);
                }
                catch (Exception ex)
                {
                    LogService.WriteServerRunLog(LogLevel.Fail, ex.GetType().FullName + ":" + ex.Message + "\r\n" + ex.StackTrace);
                }

            }
            else
            {
                string insertSql = "insert into project(Publisher,PublisherIP,ProjectID,ProjectName,SoftwareVersion,ProjectVersion,ServerVersion,"
                    + "ProjectCreateTime,ProjectPublishTime,ProjectUpdateTime,ProjectDeleteTime,IncludeFileAmount,Size,SizeOnDisk,TempletAmount,"
                    + "ChannelAmount,PageAmount,ResourceFileAmount,ProjectSavedPath,ProjectBuildedPath,BuildedFileAmount,BuildedSize,BuildedSizeOnDisk)"
                    + " values(?rr,?ab,?aa,?a,?b,?c,?d,?ii,?e,?f,?g,?h,?r,?u,?i,?o,?p,?m,?z,?x,?k,?j,?y)";
                try
                {
                    int i = DataAccess.SExecuteNonQuery(insertSql, _publisherID, _publisherIP, _projectID, _projectName, _softwareVersion,
                        _projectVersion, _serverVersion, sdsite._projCreateTime, sdsite._publishTime, _startTime, _deleteTime, sdsite._includeFileAmount,
                        _size, _sizeOnDisk, sdsite._templetAmount, sdsite._channelAmount, sdsite._pageAmount, sdsite._resourceFileAmount, _projectSavedPath,
                        _projectBuildedPath, _buildedFileAmount, _buildedSize, _buildedSizeOnDisk);
                }
                catch (Exception ex)
                {
                    LogService.WriteServerRunLog(LogLevel.Fail, ex.GetType().FullName + ":" + ex.Message + "\r\n" + ex.StackTrace);
                }
            }
        }


    }
}