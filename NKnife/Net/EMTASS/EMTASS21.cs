//## Project  : ETMASS——Extensible Multi-Thread Asynchronous Socket Server Framework
//## Author   : Hulihui(ehulh@163.com)
//## Creation Date : 2008-10-13
//## Modified Date : 2008-11-09

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NKnife.Net.EMTASS
{
    public class TSocketServerBase<TSession, TDatabase>: IDisposable, IDatabaseEvent, ISessionEvent
        where TSession : TSessionBase, new()
        where TDatabase: TDatabaseBase, new()
    {
        #region  member fields

        private Socket m_serverSocket;
        private bool m_serverClosed = true;
        private bool m_serverListenPaused = false;

        private int m_acceptListenTimeInterval = 25;         // 侦听论询时间间隔(ms)
        private int m_checkSessionTableTimeInterval = 100;   // 清理Timer的时间间隔(ms)
        private int m_checkDatagramQueueTimeInterval = 100;  // 检查数据包队列时间休息间隔(ms)
        private int m_servertPort = 3130;

        private int m_sessionSequenceNo = 0;  // sessionID 流水号
        private int m_sessionCount;
        private int m_receivedDatagramCount;
        private int m_errorDatagramCount;
        private int m_datagramQueueLength;

        private int m_databaseExceptionCount;
        private int m_serverExceptCount;
        private int m_sessionExceptionCount;

        private int m_maxSessionCount = 1024;
        private int m_receiveBufferSize = 16 * 1024;  // 16 K
        private int m_sendBufferSize = 16 * 1024;

        private int m_maxDatagramSize = 1024 * 1024;  // 1M
        private int m_maxSessionTimeout = 60;   // 1 minutes
        private int m_maxListenQueueLength = 16;
        private int m_maxSameIPCount = 64;

        private Dictionary<int, TSession> m_sessionTable; //会话字典
        private Dictionary<string,int> m_OperatorClientTable;  //呼叫器字典
        public Dictionary<string, int> OperatorClientTable
        {
            get
            {
                return m_OperatorClientTable;
            }
        }
        private Dictionary<string, int> m_MainLcdClientTable;  //综合屏字典
        public Dictionary<string, int> MainLcdClientTable
        {
            get
            {
                return m_MainLcdClientTable;
            }
        }
        private Dictionary<string, int> m_TicketClientTable; //取号终端字典
        public Dictionary<string, int> TicketClientTable
        {
            get
            {
                return m_TicketClientTable;
            }
        }
        private Dictionary<string, int> m_PdaClientTable; //Pda字典
        public Dictionary<string, int> PdaClientTable
        {
            get
            {
                return m_PdaClientTable;
            }
        }
        
        private Dictionary<string, int> m_SpeechClientTable; //语音终端字典

        private Dictionary<string, int> m_QCardReaderClientTable; //读卡器终端

        private TDatabase m_databaseObj = null;

        private bool m_disposed = false;

        private ManualResetEvent m_checkAcceptListenResetEvent;
        private ManualResetEvent m_checkSessionTableResetEvent;
        private ManualResetEvent m_checkDatagramQueueResetEvent;

        private Mutex m_ServerMutex;  // 只能有一个服务器
        private BufferManager m_bufferManager;

        #endregion

        #region  public properties

        public bool Closed
        {
            get { return m_serverClosed; }
        }

        public bool ListenPaused
        {
            get { return m_serverListenPaused; }
        }

        public int ServerPort
        {
            get { return m_servertPort; }
            set { m_servertPort = value; }
        }

        public int ServerExceptionCount
        {
            get { return m_serverExceptCount; }
        }

        public int DatabaseExceptionCount
        {
            get { return m_databaseExceptionCount; }
        }

        public int SessionExceptionCount
        {
            get { return m_sessionExceptionCount; }
        }

        public int SessionCount
        {
            get { return m_sessionCount; }
        }

        public int ReceivedDatagramCount
        {
            get { return m_receivedDatagramCount; }
        }

        public int ErrorDatagramCount
        {
            get { return m_errorDatagramCount; }
        }

        public int DatagramQueueLength
        {
            get { return m_datagramQueueLength; }
        }

        [Obsolete("Use AcceptListenTimeInterval instead.")]
        public int LoopWaitTime
        {
            get { return m_acceptListenTimeInterval; }
            set { this.AcceptListenTimeInterval = value; }
        }

        public int AcceptListenTimeInterval
        {
            get { return m_acceptListenTimeInterval; }
            set
            {
                if (value < 0)
                {
                    m_acceptListenTimeInterval = value;
                }
                else
                {
                    m_acceptListenTimeInterval = value;
                }
            }
        }

        public int CheckSessionTableTimeInterval
        {
            get { return m_checkSessionTableTimeInterval; }
            set
            {
                if (value < 10)
                {
                    m_checkSessionTableTimeInterval = 10;
                }
                else
                {
                    m_checkSessionTableTimeInterval = value;
                }
            }
        }

        public int CheckDatagramQueueTimeInterval
        {
            get { return m_checkDatagramQueueTimeInterval; }
            set
            {
                if (value < 10)
                {
                    m_checkDatagramQueueTimeInterval = 10;
                }
                else
                {
                    m_checkDatagramQueueTimeInterval = value;
                }
            }
        }

        public int MaxSessionCount
        {
            get { return m_maxSessionCount; }
        }

        [Obsolete]
        public int MaxSessionTableLength
        {
            get { return m_maxSessionCount; }
            set
            {
                if (value <= 1)
                {
                    m_maxSessionCount = 1;
                }
                else
                {
                    m_maxSessionCount = value;
                }
            }
        }

        public int ReceiveBufferSize
        {
            get { return m_receiveBufferSize; }
        }

        public int SendBufferSize
        {
            get { return m_sendBufferSize; }
        }

        [Obsolete]
        public int MaxReceiveBufferSize
        {
            get { return m_receiveBufferSize; }
            set
            {
                if (value < 1024)
                {
                    m_receiveBufferSize = 1024;
                    m_sendBufferSize = 1024;
                }
                else
                {
                    m_receiveBufferSize = value;
                    m_sendBufferSize = value;
                }
            }
        }
        
        public int MaxDatagramSize
        {
            get { return m_maxDatagramSize; }
            set
            {
                if (value < 1024)
                {
                    m_maxDatagramSize = 1024;
                }
                else
                {
                    m_maxDatagramSize = value;
                }
            }
        }

        public int MaxListenQueueLength
        {
            get { return m_maxListenQueueLength; }
            set
            {
                if (value <= 1)
                {
                    m_maxListenQueueLength = 2;
                }
                else
                {
                    m_maxListenQueueLength = value;
                }
            }
        }

        public int MaxSessionTimeout
        {
            get { return m_maxSessionTimeout; }
            set
            {
                if (value < 120)
                {
                    m_maxSessionTimeout = 120;
                }
                else
                {
                    m_maxSessionTimeout = value;
                }
            }
        }

        public int MaxSameIPCount
        {
            get { return m_maxSameIPCount; }
            set
            {
                if (value < 1)
                {
                    m_maxSameIPCount = 1;
                }
                else
                {
                    m_maxSameIPCount = value;
                }
            }
        }

        [Obsolete("Use SessionCoreInfoCollection instead.")]
        public List<TSessionCoreInfo> SessionCoreInfoList
        {
            get
            {
                List<TSessionCoreInfo> sessionList = new List<TSessionCoreInfo>();
                lock (m_sessionTable)
                {
                    foreach (TSession session in m_sessionTable.Values)
                    {
                        sessionList.Add((TSessionCoreInfo)session);
                    }
                }
                return sessionList;
            }
        }

        public Collection<TSessionCoreInfo> SessionCoreInfoCollection
        {
            get
            {
                Collection<TSessionCoreInfo> sessionCollection = new Collection<TSessionCoreInfo>();
                lock(m_sessionTable)
                {
                    foreach(TSession session in m_sessionTable.Values)
                    {
                        sessionCollection.Add((TSessionCoreInfo)session);
                    }
                }
                return sessionCollection;
            }
        }

        #endregion

        #region  class events

        public event EventHandler ServerStarted;
        public event EventHandler ServerClosed;
        public event EventHandler ServerListenPaused;
        public event EventHandler ServerListenResumed;
        public event EventHandler<TExceptionEventArgs> ServerException;

        public event EventHandler SessionRejected;
        public event EventHandler<TSessionEventArgs> SessionConnected;
        public event EventHandler<TSessionEventArgs> SessionDisconnected;
        public event EventHandler<TSessionEventArgs> SessionTimeout;

        public event EventHandler<TSessionEventArgs> DatagramDelimiterError;
        public event EventHandler<TSessionEventArgs> DatagramOversizeError;
        public event EventHandler<TSessionExceptionEventArgs> SessionReceiveException;
        public event EventHandler<TSessionExceptionEventArgs> SessionSendException;
        public event EventHandler<TSessionEventArgs> DatagramAccepted;
        public event EventHandler<TSessionEventArgs> DatagramError;
        public event EventHandler<TSessionEventArgs> DatagramHandled;
        public event EventHandler<TSessionCmdEventArgs> CmdProcessing;

        public event EventHandler<TExceptionEventArgs> DatabaseOpenException;
        public event EventHandler<TExceptionEventArgs> DatabaseCloseException;
        public event EventHandler<TExceptionEventArgs> DatabaseException;

        public event EventHandler<TExceptionEventArgs> ShowDebugMessage;
        
        #endregion

        #region  class constructor

        public TSocketServerBase()
        {
            this.Initiate(m_maxSessionCount, m_receiveBufferSize, m_sendBufferSize, null);
        }

        public TSocketServerBase(string dbConnectionString)
        {
            this.Initiate(m_maxSessionCount, m_receiveBufferSize, m_sendBufferSize, dbConnectionString);
        }

        public TSocketServerBase(int maxSessionCount, int receiveBufferSize, int sendBufferSize)
        {
            this.Initiate(maxSessionCount, receiveBufferSize, sendBufferSize, null);
        }

        public TSocketServerBase(int maxSessionCount, int receiveBufferSize, int sendBufferSize, string dbConnectionString)
        {
            this.Initiate(maxSessionCount, receiveBufferSize, sendBufferSize, dbConnectionString);
        }

        [Obsolete]
        public TSocketServerBase(int tcpPort, string dbConnectionString)
        {
            m_servertPort = tcpPort;
            this.Initiate(m_maxSessionCount, m_receiveBufferSize, m_sendBufferSize, dbConnectionString);
        }

        private void Initiate(int maxSessionCount, int receiveBufferSize, int sendBufferSize, string dbConnectionString)
        {
            bool canCreateNew;
            m_ServerMutex = new Mutex(true, "510EMTASS_SERVER", out canCreateNew);
            if (!canCreateNew)
            {
                throw new Exception("Can create two or more server!");
            }

            m_maxSessionCount = maxSessionCount;
            m_receiveBufferSize = receiveBufferSize;
            m_sendBufferSize = sendBufferSize;

            m_bufferManager = new BufferManager(maxSessionCount, receiveBufferSize, sendBufferSize);
            m_sessionTable = new Dictionary<int, TSession>();
            m_OperatorClientTable = new Dictionary<string, int>();
            m_MainLcdClientTable = new Dictionary<string, int>();
            m_TicketClientTable = new Dictionary<string, int>();
            m_PdaClientTable = new Dictionary<string, int>();
            m_SpeechClientTable = new Dictionary<string, int>();
            m_QCardReaderClientTable = new Dictionary<string, int>();

            m_checkAcceptListenResetEvent = new ManualResetEvent(true);
            m_checkSessionTableResetEvent = new ManualResetEvent(true);
            m_checkDatagramQueueResetEvent = new ManualResetEvent(true);

            if (dbConnectionString != null)
            {
                m_databaseObj = new TDatabase();
                m_databaseObj.Initiate(dbConnectionString);

                m_databaseObj.DatabaseOpenException += new EventHandler<TExceptionEventArgs>(this.OnDatabaseOpenException);  // 转递数据库事件
                m_databaseObj.DatabaseCloseException += new EventHandler<TExceptionEventArgs>(this.OnDatabaseCloseException);
                m_databaseObj.DatabaseException += new EventHandler<TExceptionEventArgs>(this.OnDatabaseException);
            }
        }


        ~TSocketServerBase()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            if (!m_disposed)
            {
                m_disposed = true;
                this.Close();
                this.Dispose(true);
                GC.SuppressFinalize(this);  // Finalize 不会第二次执行
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)  // 对象正在被显示释放, 不是执行 Finalize()
            {
                m_sessionTable = null;  // 释放托管资源
            }

            if (m_ServerMutex != null)
            {
                m_ServerMutex.Close();
            }

            if (m_checkAcceptListenResetEvent != null)
            {
                m_checkAcceptListenResetEvent.Close();  // 释放非托管资源
            }

            if (m_checkSessionTableResetEvent != null)
            {
                m_checkSessionTableResetEvent.Close();
            }

            if (m_checkDatagramQueueResetEvent != null)
            {
                m_checkDatagramQueueResetEvent.Close();
            }

            if (m_bufferManager != null)
            {
                m_bufferManager.Clear();
            }
        }

        #endregion

        #region  public methods

        public bool Start()
        {
            //if (!m_serverClosed)
            //{
            //    return true;
            //}

            m_serverClosed = true;  // 在其它方法中要判断该字段
            m_serverListenPaused = true;

            this.Close();
            this.ClearCountValues();

            try
            {
                if (m_databaseObj != null)
                {
                    m_databaseObj.Open();
                    if (m_databaseObj.State != ConnectionState.Open)
                    {
                        return false;
                    }
                }
               
                if (!this.CreateServerSocket()) return false;
                m_serverClosed = false;

                //if (!ThreadPool.QueueUserWorkItem(this.CheckDatagramQueue)) return false;
                //if (!ThreadPool.QueueUserWorkItem(this.CheckSessionTable)) return false;
                Thread CheckDatagramThread = new Thread(new ThreadStart(this.CheckDatagramQueue));
                CheckDatagramThread.IsBackground = true;
                CheckDatagramThread.Start();

                Thread CheckSessionThread = new Thread(new ThreadStart(this.CheckSessionTable));
                CheckSessionThread.IsBackground = true;
                CheckSessionThread.Start();

                //if (!ThreadPool.QueueUserWorkItem(this.StartServerListen)) return false;
                Thread ServerListenThread = new Thread(new ThreadStart(this.StartServerListen));
                ServerListenThread.IsBackground = true;
                ServerListenThread.Start();
                m_serverListenPaused = false;

                this.OnServerStarted();
            }
            catch (Exception err)
            {
                this.OnServerException(err);
            }
            return !m_serverClosed;
        }

        public void PauseListen()
        {
            m_serverListenPaused = true;
            this.OnServerListenPaused();
        }

        public void ResumeListen()
        {
            m_serverListenPaused = false;
            this.OnServerListenResumed();
        }

        public void Stop()
        {
            this.Close();
        }

        public void CloseSession(int sessionId)
        {
            TSession session = null;
            lock (m_sessionTable)
            {
                if (m_sessionTable.ContainsKey(sessionId))  // 包含该会话 ID
                {
                    session = (TSession)m_sessionTable[sessionId];
                }
            }

            if (session != null)
            {
                session.SetInactive();
            }
        }

        public void CloseAllSessions()
        {
            lock (m_sessionTable)
            {
                foreach (TSession session in m_sessionTable.Values)
                {
                    session.SetInactive();
                }
            }
        }

        public void SendToSession(int sessionId, string datagramText)
        {
            TSession session = null;
            lock (m_sessionTable)
            {
                if (m_sessionTable.ContainsKey(sessionId))
                {
                    session = (TSession)m_sessionTable[sessionId];
                }
            }

            if (session != null)
            {
                session.SendDatagram(datagramText);
            }
        }

        public void SendToAllSessions(string datagramText)
        {
            lock (m_sessionTable)
            {
                foreach (TSession session in m_sessionTable.Values)
                {
                    session.SendDatagram(datagramText);
                }
            }
        }

        //发送给所有的操作器
        public void SendToAllOperator(string datagramText)
        {
            lock (m_OperatorClientTable)
            {
                foreach (int sessionid in m_OperatorClientTable.Values)
                {
                    SendToSession(sessionid, datagramText);
                }
            }
        }

        public bool HasVirtualOperatorConnected()
        {
            try
            {
                if (m_OperatorClientTable.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //发送给所有的虚拟综合屏
        public void SendToAllMainLcd(string datagramText)
        {
            lock (m_MainLcdClientTable)
            {
                foreach (int sessionid in m_MainLcdClientTable.Values)
                {
                    SendToSession(sessionid, datagramText);
                }
            }
        }

        public bool HasVirtualMainLcdConnected()
        {
            try
            {
                if (m_MainLcdClientTable.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public string GetSessionIP(int sessionID)
        {
            string result = string.Empty;
            TSession session = null; 
            lock (m_sessionTable)
            {
                if (m_sessionTable.ContainsKey(sessionID))
                {
                    session = (TSession)m_sessionTable[sessionID];
                }
            }

            if (session != null)
            {
                result = session.IP;
            }
            return result;
        }

        public void SendToAllWinOperator(string datagramText)
        {
            lock (m_OperatorClientTable)
            {
                foreach (string clientNo in m_TicketClientTable.Keys)
                {
                    SendToWinOperator(clientNo, datagramText);
                }
            }
        }

        public void SendToWinOperator(string clientNo,string datagramText)
        {
            lock (m_OperatorClientTable)
            {
                if (m_OperatorClientTable.ContainsKey(clientNo))
                {
                    int sessionid = m_OperatorClientTable[clientNo];
                    SendToSession(sessionid, datagramText);
                }
            }
        }

        //发送给所有的取号终端
        public void SendToAllTicketClient(string datagramText)
        {
            lock (m_TicketClientTable)
            {
                foreach (int sessionid in m_TicketClientTable.Values)
                {
                    SendToSession(sessionid, datagramText);
                }
            }
        }

        public void SendToTicketClient(string clientNo,string datagramText)
        {
            lock (m_TicketClientTable)
            {
                if (m_TicketClientTable.ContainsKey(clientNo))
                {
                    int sessionid = m_TicketClientTable[clientNo];
                    SendToSession(sessionid, datagramText);
                }
            }
        }

        //发送给所有的PDA
        public void SendToAllPdaClient(string datagramText)
        {
            lock (m_PdaClientTable)
            {
                foreach (int sessionid in m_PdaClientTable.Values)
                {
                    SendToSession(sessionid, datagramText);
                }
            }
        }

        //发送给所有的语音终端

        //发送给指定的语音终端
        public void SendToOneSpeechClient(string clientNo, string datagramText)
        {
            lock (m_SpeechClientTable)
            {
                if (m_SpeechClientTable.ContainsKey(clientNo))
                {
                    int sessionid = m_SpeechClientTable[clientNo];
                    SendToSession(sessionid, datagramText);
                }
            }
        }

        //更新操作器映射表
        public void UpdateOpertiorClientSessionTable(string clientNo,int sessionID)
        {
            lock (m_OperatorClientTable)
            {
                if (m_OperatorClientTable.ContainsKey(clientNo))
                {
                    m_OperatorClientTable[clientNo] = sessionID;
                }
                else
                {
                    m_OperatorClientTable.Add(clientNo, sessionID);
                }
            }
        }

        public void UpdateMainLcdClientSessionTable(string clientNo, int sessionID)
        {
            lock (m_MainLcdClientTable)
            {
                if (m_MainLcdClientTable.ContainsKey(clientNo))
                {
                    m_MainLcdClientTable[clientNo] = sessionID;
                }
                else
                {
                    m_MainLcdClientTable.Add(clientNo, sessionID);
                }
            }
        }

        //更新取号终端映射表
        public void UpdateTicketClientSessionTable(string clientNo, int sessionID)
        {
            lock (m_TicketClientTable)
            {
                if (m_TicketClientTable.ContainsKey(clientNo))
                {
                    m_TicketClientTable[clientNo] = sessionID;
                    //sendMsgInfo(sessionID,15,"测试即时信息");
                }
                else
                {
                    m_TicketClientTable.Add(clientNo, sessionID);
                    SendAdvInfo(sessionID);
                }
            }
        }

        public int GetTicketSessionIDFromClientID(string clientNo)
        {
            lock (m_TicketClientTable)
            {
                if (m_TicketClientTable.ContainsKey(clientNo))
                {
                    return m_TicketClientTable[clientNo];
                }
            }
            return -1;
        }

        public int GetQCardReaderSessionIDFromClientID(string clientNo)
        {
            lock (m_QCardReaderClientTable)
            {
                if (m_QCardReaderClientTable.ContainsKey(clientNo))
                {
                    return m_QCardReaderClientTable[clientNo];
                }
            }
            return -1;
        }

        //更新PDA映射表
        public void UpdatePdaClientSessionTable(string clientNo, int sessionID)
        {
            lock (m_PdaClientTable)
            {
                if (m_PdaClientTable.ContainsKey(clientNo))
                {
                    m_PdaClientTable[clientNo] = sessionID;
                }
                else
                {
                    m_PdaClientTable.Add(clientNo, sessionID);
                }
            }
        }

        //更新语音终端映射表
        public void UpdateSpeechClientSessionTable(string clientNo, int sessionID)
        {
            lock (m_SpeechClientTable)
            {
                if (m_SpeechClientTable.ContainsKey(clientNo))
                {
                    m_SpeechClientTable[clientNo] = sessionID;
                    //sendMsgInfo(sessionID,15,"测试即时信息");
                }
                else
                {
                    m_SpeechClientTable.Add(clientNo, sessionID);
                }
            }
        }

        //更新身份证打印终端
        public void UpdateQCardReaderClientSessionTable(string clientNo, int sessionID)
        {
            lock (m_QCardReaderClientTable)
            {
                if (m_QCardReaderClientTable.ContainsKey(clientNo))
                {
                    m_QCardReaderClientTable[clientNo] = sessionID;
                    //sendMsgInfo(sessionID,15,"测试即时信息");
                }
                else
                {
                    m_QCardReaderClientTable.Add(clientNo, sessionID);
                }
            }
        }

        private string _AdvStr;
        public string AdvStr
        {
            get
            {
                return _AdvStr;
            }
            set
            {
                _AdvStr = value;    
            }
        }
        private void SendAdvInfo(int sessionID)
        {
            SendToSession(sessionID,"SCREENINFO|01|" + _AdvStr + "|@");
        }

        public void sendMsgInfo(int sessionID,int stayInteval,string msg)
        {
            //SendToSession(sessionID, "SCREENINFO|02|胡撒地方asdfsadfsdfasdf--sdfsdf,,|@");
            SendToSession(sessionID, "SCREENINFO|02|" + stayInteval.ToString() + "|" + msg + "|@");
        }

        public void KillSession(int sessionID)
        {
            if (m_serverClosed)
            {
                return;
            }
            lock (m_sessionTable)
            {
                if (m_sessionTable.ContainsKey(sessionID))  // 统一清除
                {
                    m_sessionTable.Remove(sessionID);
                }
                
            }
            
        }

        #endregion

        #region  private methods

        private void Close()
        {
            if (m_serverClosed)
            {
                return;
            }

            m_serverClosed = true;
            m_serverListenPaused = true;

            m_checkAcceptListenResetEvent.WaitOne();  // 等待3个线程
            m_checkSessionTableResetEvent.WaitOne();
            m_checkDatagramQueueResetEvent.WaitOne();

            if (m_databaseObj != null)
            {
                m_databaseObj.Close();
            }

            if (m_sessionTable != null)
            {
                lock (m_sessionTable)
                {
                    foreach (TSession session in m_sessionTable.Values)
                    {
                        session.Close();
                    }
                }
            }

            this.CloseServerSocket();

            if (m_sessionTable != null)  // 清空会话列表
            {
                lock (m_sessionTable)
                {
                    m_sessionTable.Clear();
                }
            }

            this.OnServerClosed();
        }

        private void ClearCountValues()
        {
            m_sessionCount = 0;
            m_receivedDatagramCount = 0;
            m_errorDatagramCount = 0;
            m_datagramQueueLength = 0;

            m_databaseExceptionCount = 0;
            m_serverExceptCount = 0;
            m_sessionExceptionCount = 0;
        }

        private bool CreateServerSocket()
        {
            try
            {
                m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_serverSocket.Bind(new IPEndPoint(IPAddress.Any, m_servertPort));
                m_serverSocket.Listen(m_maxListenQueueLength);

                return true;
            }
            catch (Exception err)
            {
                this.OnServerException(err);
                return false;
            }
        }

        private bool CheckSocketIP(Socket clientSocket)
        {
            IPEndPoint iep = (IPEndPoint)clientSocket.RemoteEndPoint;
            string ip = iep.Address.ToString();

            if (ip.Substring(0, 7) == "127.0.0")   // local machine
            {
                return true;
            }

            lock (m_sessionTable)
            {
                int sameIPCount = 0;
                foreach (TSession session in m_sessionTable.Values)
                {
                    if (session.IP == ip)
                    {
                        sameIPCount++;
                        if (sameIPCount > m_maxSameIPCount)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 侦听客户端连接请求
        /// </summary>
        private void StartServerListen()
        {
            m_checkAcceptListenResetEvent.Reset();
            Socket clientSocket = null;

            while (!m_serverClosed)
            {
                if (m_serverListenPaused)  // pause server
                {
                    this.CloseServerSocket();
                    Thread.Sleep(m_acceptListenTimeInterval);
                    continue;
                }

                if (m_serverSocket == null)
                {
                    this.CreateServerSocket();
                    continue;
                }

                try
                {
                    if (m_serverSocket.Poll(m_acceptListenTimeInterval, SelectMode.SelectRead))
                    {
                        // 频繁关闭、启动时，这里容易产生错误（提示套接字只能有一个）
                        clientSocket = m_serverSocket.Accept();

                        if (clientSocket != null && clientSocket.Connected)
                        {
                            if (m_sessionCount >= m_maxSessionCount || !this.CheckSocketIP(clientSocket))  // 当前列表已经存在该 IP 地址
                            {
                                this.OnSessionRejected(); // 拒绝登录请求
                                this.CloseClientSocket(clientSocket);
                            }
                            else
                            {
                                this.AddSession(clientSocket);  // 添加到队列中, 并调用异步接收方法
                            }
                        }
                        else  // clientSocket is null or connected == false
                        {
                            this.CloseClientSocket(clientSocket);
                        }
                    }
                }
                catch (Exception)  // 侦听连接的异常频繁, 不捕获异常
                {
                    this.CloseClientSocket(clientSocket);
                }
            }

            m_checkAcceptListenResetEvent.Set();
        }

        private void CloseServerSocket()
        {
            if (m_serverSocket == null)
            {
                return;
            }

            try
            {
                lock (m_sessionTable)
                {
                    if (m_sessionTable != null && m_sessionTable.Count > 0)
                    {
                        // 可能结束服务器端的 AcceptClientConnect 的 Poll
//                        m_serverSocket.Shutdown(SocketShutdown.Both);  // 有连接才关
                    }
                }
                m_serverSocket.Close();
            }
            catch (Exception err)
            {
                this.OnServerException(err);
            }
            finally
            {
                m_serverSocket = null;
            }
        }

        /// <summary>
        /// 强制关闭客户端请求时的 Socket
        /// </summary>
        private void CloseClientSocket(Socket socket)
        {
            if (socket != null)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch(Exception) { }  // 强制关闭, 忽略错误
            }
        }

        /// <summary>
        /// 增加一个会话对象
        /// </summary>
        private void AddSession(Socket clientSocket)
        {
            Interlocked.Increment(ref m_sessionSequenceNo);

            TSession session = new TSession();
            session.Initiate(m_maxDatagramSize, m_sessionSequenceNo, clientSocket, m_databaseObj, m_bufferManager);

            session.DatagramDelimiterError += new EventHandler<TSessionEventArgs>(this.OnDatagramDelimiterError);
            session.DatagramOversizeError += new EventHandler<TSessionEventArgs>(this.OnDatagramOversizeError);
            session.DatagramError += new EventHandler<TSessionEventArgs>(this.OnDatagramError);
            session.DatagramAccepted += new EventHandler<TSessionEventArgs>(this.OnDatagramAccepted);
            session.DatagramHandled += new EventHandler<TSessionEventArgs>(this.OnDatagramHandled);
            session.MsgProcessing += new EventHandler<TSessionCmdEventArgs>(this.OnMsgProcessing);
            session.SessionReceiveException += new EventHandler<TSessionExceptionEventArgs>(this.OnSessionReceiveException);
            session.SessionSendException += new EventHandler<TSessionExceptionEventArgs>(this.OnSessionSendException);

            session.ShowDebugMessage += new EventHandler<TExceptionEventArgs>(this.ShowDebugMessage);

            lock (m_sessionTable)
            {
                m_sessionTable.Add(session.ID, session);
            }
            session.ReceiveDatagram();

            this.OnSessionConnected(session);
        }

        /// <summary>
        /// 资源清理线程, 分若干步完成
        /// </summary>
        private void CheckSessionTable()
        {

            m_checkSessionTableResetEvent.Reset();

            //try
            //{
                while (!m_serverClosed)
                {
                    lock (m_sessionTable)
                    {
                        List<int> sessionIDList = new List<int>();
                        TSession[] dataArray = new TSession[m_sessionTable.Values.Count];
                        m_sessionTable.Values.CopyTo(dataArray, 0);
                        for (int i = 0; i < dataArray.Length; i++)
                        {
                            TSession session = dataArray[i];
                            if (m_serverClosed)
                            {
                                break;
                            }

                            if (session.State == TSessionState.Inactive)  // 分三步清除一个 Session
                            {
                                session.Shutdown();  // 第一步: shutdown, 结束异步事件
                            }
                            else if (session.State == TSessionState.Shutdown)
                            {
                                session.Close();  // 第二步: Close
                            }
                            else if (session.State == TSessionState.Closed)
                            {
                                sessionIDList.Add(session.ID);
                                this.DisconnectSession(session);

                            }
                            else // 正常的会话 Active
                            {
                                session.CheckTimeout(m_maxSessionTimeout); // 判超时，若是则标记
                            }
                        }

                        foreach (int id in sessionIDList)  // 统一清除
                        {
                            m_sessionTable.Remove(id);
                        }

                        sessionIDList.Clear();
                    }

                    Thread.Sleep(m_checkSessionTableTimeInterval);
                }
            //}
            //catch
            //{

            //}

            m_checkSessionTableResetEvent.Set();
        }

        /// <summary>
        /// 数据包处理线程
        /// </summary>
        private void CheckDatagramQueue()
        {
            m_checkDatagramQueueResetEvent.Reset();

            while (!m_serverClosed)
            {
                lock (m_sessionTable)
                {
                    foreach (TSession session in m_sessionTable.Values)
                    {
                        if (m_serverClosed)
                        {
                            break;
                        }

                        session.HandleDatagram();
                    }
                }
                Thread.Sleep(m_checkDatagramQueueTimeInterval);
            }

            m_checkDatagramQueueResetEvent.Set();
        }

        private void DisconnectSession(TSession session)
        {
            if (session.DisconnectType == TDisconnectType.Normal)
            {
                this.OnSessionDisconnected(session);
            }
            else if (session.DisconnectType == TDisconnectType.Timeout)
            {
                this.OnSessionTimeout(session);
            }
        }

        /// <summary>
        /// 输出调试信息
        /// </summary>
        private void OnShowDebugMessage(string message)
        {
            if (this.ShowDebugMessage != null)
            {
                TExceptionEventArgs e = new TExceptionEventArgs(message);
                this.ShowDebugMessage(this, e);
            }
        }

        #endregion

        #region  protected virtual methods

        protected virtual void OnDatabaseOpenException(object sender, TExceptionEventArgs e)
        {
            Interlocked.Increment(ref m_databaseExceptionCount);

            EventHandler<TExceptionEventArgs> handler = this.DatabaseOpenException;
            if (handler != null)
            {
                handler(sender, e);  // 转发事件的激发者
            }
        }

        protected virtual void OnDatabaseCloseException(object sender, TExceptionEventArgs e)
        {
            Interlocked.Increment(ref m_databaseExceptionCount);

            EventHandler<TExceptionEventArgs> handler = this.DatabaseCloseException;
            if (handler != null)
            {
                handler(sender, e);  // 转发事件的激发者
            }
        }

        protected virtual void OnDatabaseException(object sender, TExceptionEventArgs e)
        {
            Interlocked.Increment(ref m_databaseExceptionCount);

            EventHandler<TExceptionEventArgs> handler = this.DatabaseException;
            if (handler != null)
            {
                handler(sender, e);  // 转发事件的激发者
            }
        }

        protected virtual void OnSessionRejected()
        {
            EventHandler handler = this.SessionRejected;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual  void OnSessionConnected(TSession session)
        {
            Interlocked.Increment(ref m_sessionCount);

            EventHandler<TSessionEventArgs> handler = this.SessionConnected;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(session);
                handler(this, e);
            }
        }

        protected virtual void OnSessionDisconnected(TSession session)
        {
            Interlocked.Decrement(ref m_sessionCount);

            EventHandler<TSessionEventArgs> handler = this.SessionDisconnected;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(session);
                handler(this, e);
            }
        }

        protected virtual void OnSessionTimeout(TSession session)
        {
            Interlocked.Decrement(ref m_sessionCount);

            EventHandler<TSessionEventArgs> handler = this.SessionTimeout;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(session);
                handler(this, e);
            }
        }

        protected virtual void OnSessionReceiveException(object sender,  TSessionExceptionEventArgs e)
        {
            Interlocked.Decrement(ref m_sessionCount);
            Interlocked.Increment(ref m_sessionExceptionCount);

            EventHandler<TSessionExceptionEventArgs> handler = this.SessionReceiveException;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSessionSendException(object sender, TSessionExceptionEventArgs e)
        {
            Interlocked.Decrement(ref m_sessionCount);
            Interlocked.Increment(ref m_sessionExceptionCount);

            EventHandler<TSessionExceptionEventArgs> handler = this.SessionSendException;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnServerException(Exception err)
        {
            Interlocked.Increment(ref m_serverExceptCount);

            EventHandler<TExceptionEventArgs> handler = this.ServerException;
            if (handler != null)
            {
                TExceptionEventArgs e = new TExceptionEventArgs(err);
                handler(this, e);
            }
        }

        protected virtual void OnServerStarted()
        {
            EventHandler handler = this.ServerStarted;
            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnServerListenPaused()
        {
            EventHandler handler = this.ServerListenPaused;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnServerListenResumed()
        {
            EventHandler handler = this.ServerListenResumed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnServerClosed()
        {
            EventHandler handler = this.ServerClosed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnDatagramDelimiterError(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_receivedDatagramCount);
            Interlocked.Increment(ref m_errorDatagramCount);

            EventHandler<TSessionEventArgs> handler = this.DatagramDelimiterError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramOversizeError(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_receivedDatagramCount);
            Interlocked.Increment(ref m_errorDatagramCount);

            EventHandler<TSessionEventArgs> handler = this.DatagramOversizeError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramAccepted(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_receivedDatagramCount);
            Interlocked.Increment(ref m_datagramQueueLength);

            EventHandler<TSessionEventArgs> handler = this.DatagramAccepted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramError(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_errorDatagramCount);
            Interlocked.Decrement(ref m_datagramQueueLength);

            EventHandler<TSessionEventArgs> handler = this.DatagramError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramHandled(object sender, TSessionEventArgs e)
        {
            Interlocked.Decrement(ref m_datagramQueueLength);

            EventHandler<TSessionEventArgs> handler = this.DatagramHandled;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnMsgProcessing(object sender, TSessionCmdEventArgs e)
        {
            EventHandler<TSessionCmdEventArgs> handler = this.CmdProcessing;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

    }

    /// <summary>
    /// 会话类核心成员
    /// </summary>
    public class TSessionCoreInfo
    {
        #region  member fields

        private int m_id;
        private string m_ip = string.Empty;
        private string m_name = string.Empty;
        private TSessionState m_state = TSessionState.Active;
        private TDisconnectType m_disconnectType = TDisconnectType.Normal;

        private DateTime m_loginTime;
        private DateTime m_lastSessionTime;

        #endregion

        #region  public properties

        public int ID
        {
            get { return m_id; }
            protected set { m_id = value; }
        }

        public string IP
        {
            get { return m_ip; }
            protected set { m_ip = value; }
        }

        /// <summary>
        /// 数据包发送者的名称/编号
        /// </summary>
        public string Name
        {
            get { return m_name; }
            protected set { m_name = value; }
        }

        public DateTime LoginTime
        {
            get { return m_loginTime; }
            protected set 
            { 
                m_loginTime = value;
                m_lastSessionTime = value;
            }
        }

        public DateTime LastSessionTime
        {
            get { return m_lastSessionTime; }
            protected set { m_lastSessionTime = value; }
        }

        public TSessionState State
        {
            get { return m_state; }
            protected set
            {
                lock (this)
                {
                    m_state = value;
                }
            }
        }

        public TDisconnectType DisconnectType
        {
            get { return m_disconnectType; }
            protected set
            {
                lock (this)
                {
                    m_disconnectType = value;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 会话基类(抽象类, 必须实现其 AnalyzeDatagram 方法)
    /// </summary>
    public abstract class TSessionBase: TSessionCoreInfo, ISessionEvent
    {
        #region  member fields

        private Socket m_socket;
        private int m_maxDatagramSize;

        private BufferManager m_bufferManager;               
        
        private int m_bufferBlockIndex;
        private byte[] m_receiveBuffer;
        private byte[] m_sendBuffer;

        private byte[] m_datagramBuffer;

        private TDatabaseBase m_databaseObj;
        private Queue<byte[]> m_datagramQueue;

        #endregion

        #region class events

        public event EventHandler<TSessionExceptionEventArgs> SessionReceiveException;
        public event EventHandler<TSessionExceptionEventArgs> SessionSendException;
        public event EventHandler<TSessionEventArgs> DatagramDelimiterError;
        public event EventHandler<TSessionEventArgs> DatagramOversizeError;
        public event EventHandler<TSessionEventArgs> DatagramAccepted;
        public event EventHandler<TSessionEventArgs> DatagramError;
        public event EventHandler<TSessionEventArgs> DatagramHandled;
        public event EventHandler<TSessionCmdEventArgs> MsgProcessing;
        
        public event EventHandler<TExceptionEventArgs> ShowDebugMessage;

        #endregion

        #region  class constructor
        /// <summary>
        /// 作泛型参数类型时, 必须有无参构造函数
        /// </summary>
        protected TSessionBase() { }

        /// <summary>
        /// 替构造函数初始化对象
        /// </summary>
        public virtual void Initiate(int maxDatagramsize, int id, Socket socket, TDatabaseBase database, BufferManager bufferManager)
        {
            base.ID = id;
            base.LoginTime = DateTime.Now;

            m_bufferManager = bufferManager;            
            m_bufferBlockIndex = bufferManager.GetBufferBlockIndex();

            if (m_bufferBlockIndex == -1)  // 没有空块, 新建
            {
                m_receiveBuffer = new byte[m_bufferManager.ReceiveBufferSize];
                m_sendBuffer = new byte[m_bufferManager.SendBufferSize];
            }
            else
            {
                m_receiveBuffer = m_bufferManager.ReceiveBuffer;
                m_sendBuffer = m_bufferManager.SendBuffer;
            }

            m_maxDatagramSize = maxDatagramsize;

            m_socket = socket;
            m_databaseObj = database;

            m_datagramQueue = new Queue<byte[]>();

            if (m_socket != null)
            {
                IPEndPoint iep = m_socket.RemoteEndPoint as IPEndPoint;
                if (iep != null)
                {
                    base.IP = iep.Address.ToString();
                }
            }
        }

        #endregion

        #region  properties
        public TDatabaseBase DatabaseObj
        {
            get { return m_databaseObj; }
        }

        #endregion

        #region  public methods

        public void Shutdown()
        {
            lock (this)
            {
                if (this.State != TSessionState.Inactive || m_socket == null)  // Inactive 状态才能 Shutdown
                {
                    return;
                }

                this.State = TSessionState.Shutdown;
                try
                {
                    m_socket.Shutdown(SocketShutdown.Both);  // 目的：结束异步事件
                }
                catch (Exception) { }
            }
        }

        public void Close()
        {
            lock (this)
            {
                if (this.State != TSessionState.Shutdown || m_socket == null)  // Shutdown 状态才能 Close
                {
                    return;
                }

                m_datagramBuffer = null;

                if (m_datagramQueue != null)
                {
                    while (m_datagramQueue.Count > 0)
                    {

                        m_datagramQueue.Dequeue();
                    }
                    m_datagramQueue.Clear();
                }

                m_bufferManager.FreeBufferBlockIndex(m_bufferBlockIndex);

                try
                {
                    this.State = TSessionState.Closed;
                    m_socket.Close();
                }
                catch (Exception) { }
            }
        }

        public void SetInactive()
        {
            lock (this)
            {
                if (this.State == TSessionState.Active)
                {
                    this.State = TSessionState.Inactive;
                    this.DisconnectType = TDisconnectType.Normal;
                }
            }
        }

        public void HandleDatagram()
        {
            lock (this)
            {
                if (this.State != TSessionState.Active || m_datagramQueue.Count == 0)
                {
                    return;
                }

                byte[] datagramBytes = m_datagramQueue.Dequeue();
                this.AnalyzeDatagram(datagramBytes);
            }
        }

        public void ReceiveDatagram()
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                try  // 一个客户端连续做连接 或连接后立即断开，容易在该处产生错误，系统不认为是错误
                {
                    // 开始接受来自该客户端的数据
                    int bufferOffset = m_bufferManager.GetReceivevBufferOffset(m_bufferBlockIndex);
                    m_socket.BeginReceive(m_receiveBuffer, bufferOffset, m_bufferManager.ReceiveBufferSize, SocketFlags.None, this.EndReceiveDatagram, this);

                }
                catch (Exception err)  // 读 Socket 异常，准备关闭该会话
                {
                    this.DisconnectType = TDisconnectType.Exception;
                    this.State = TSessionState.Inactive;

                    this.OnSessionReceiveException(err);
                }
            }
        }

        public void SendDatagram(string datagramText)
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                try
                {

                    //int byteLength = Encoding.Default.GetByteCount(datagramText);
                    //if (byteLength <= m_bufferManager.SendBufferSize)
                    //{
                    //    int bufferOffset = m_bufferManager.GetSendBufferOffset(m_bufferBlockIndex);
                    //    Encoding.Default.GetBytes(datagramText, 0, byteLength, m_sendBuffer, bufferOffset);
                    //    m_socket.BeginSend(m_sendBuffer, bufferOffset, byteLength, SocketFlags.None, this.EndSendDatagram, this);
                    //}
                    //else
                    //{
                        byte[] data = Encoding.Default.GetBytes(datagramText);  // 获得数据字节数组
                        m_socket.BeginSend(data, 0, data.Length, SocketFlags.None, this.EndSendDatagram, this);
                    //}
                }
                catch (Exception err)  // 写 socket 异常，准备关闭该会话
                {
                    this.DisconnectType = TDisconnectType.Exception;
                    this.State = TSessionState.Inactive;

                    this.OnSessionSendException(err);
                }
            }
        }

        public void CheckTimeout(int maxSessionTimeout)
        {
            TimeSpan ts = DateTime.Now.Subtract(this.LastSessionTime);
            int elapsedSecond = Math.Abs((int)ts.TotalSeconds);

            if (elapsedSecond > maxSessionTimeout)  // 超时，则准备断开连接
            {
                this.DisconnectType = TDisconnectType.Timeout;
                this.SetInactive();  // 标记为将关闭、准备断开
            }
        }

        #endregion

        #region  private methods

        /// <summary>
        /// 发送数据完成处理函数, iar 为目标客户端 Session
        /// </summary>
        private void EndSendDatagram(IAsyncResult iar)
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                if (!m_socket.Connected)
                {
                    this.SetInactive();
                    return;
                }

                try
                {
                    m_socket.EndSend(iar);
                    iar.AsyncWaitHandle.Close();
                }
                catch (Exception err)  // 写 socket 异常，准备关闭该会话
                {
                    this.DisconnectType = TDisconnectType.Exception;
                    this.State = TSessionState.Inactive;

                    this.OnSessionSendException(err);
                }
            }
        }

        private void EndReceiveDatagram(IAsyncResult iar)
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                if (!m_socket.Connected)
                {
                    this.SetInactive();
                    return;
                }

                try
                {
                    // Shutdown 时将调用 ReceiveData，此时也可能收到 0 长数据包
                    int readBytesLength = m_socket.EndReceive(iar);
                    iar.AsyncWaitHandle.Close();

                    if (readBytesLength == 0)
                    {
                        this.DisconnectType = TDisconnectType.Normal;
                        this.State = TSessionState.Inactive;
                    }
                    else  // 正常数据包
                    {
                        this.LastSessionTime = DateTime.Now;                        
                        // 合并报文，按报文头、尾字符标志抽取报文，将包交给数据处理器
                        this.ResolveSessionBuffer(readBytesLength);
                        this.ReceiveDatagram();  // 继续接收
                    }
                }
                catch (Exception err)  // 读 socket 异常，关闭该会话，系统不认为是错误（这种错误可能太多）
                {
                    if (this.State == TSessionState.Active)
                    {
                        this.DisconnectType = TDisconnectType.Exception;
                        this.State = TSessionState.Inactive;

                        this.OnSessionReceiveException(err);
                    }
                }
            }
        }

        /// <summary>
        /// 拷贝接收缓冲区的数据到数据缓冲区（即多次读一个包文）
        /// </summary>
        private void CopyToDatagramBuffer(int start, int length)
        {
            int datagramLength = 0;
            if (m_datagramBuffer != null)
            {
                datagramLength = m_datagramBuffer.Length;
            }

            Array.Resize(ref m_datagramBuffer, datagramLength + length);  // 调整长度（m_datagramBuffer 为 null 不出错）
            Array.Copy(m_receiveBuffer, start, m_datagramBuffer, datagramLength, length);  // 拷贝到数据包缓冲区
        }

        #endregion

        #region protected methods
        
        /// <summary>
        /// 提取包时与包规则紧密相关，根据实际规则重定义
        /// </summary>
        protected virtual void ResolveSessionBuffer(int readBytesLength)
        {
            //int headDelimiter = 1;
            int tailDelimiter = 1;

            int bufferOffset = m_bufferManager.GetReceivevBufferOffset(m_bufferBlockIndex);
            int start = bufferOffset;   // m_receiveBuffer 缓冲区中包开始位置
            int length = 0;  // 已经搜索的接收缓冲区长度

            int subIndex = bufferOffset;  // 缓冲区下标
            while (subIndex < readBytesLength + bufferOffset)
            {
                if (m_receiveBuffer[subIndex] == '@')  // 数据包的结束字符>
                {
                    length += tailDelimiter;  // 长度包括结束字符“@”

                    this.GetDatagramFromBuffer(start, length); // @前面的为正确格式的包

                    start = subIndex + tailDelimiter;  // 新包起点（一般一次处理将结束循环）
                    length = 0;  // 新包长度
                }
                else  // 即非 < 也非 >， 是一般字符，长度 + 1
                {
                    length++;
                }
                ++subIndex;
            }

            if (length > 0)  // 剩下的待处理串，分两种情况
            {
                int mergedLength = length;
                if (m_datagramBuffer != null)
                {
                    mergedLength += m_datagramBuffer.Length;
                }
                
                // 剩下的包文不超长，转存到包文缓冲区中，待下次处理
                if (mergedLength <= m_maxDatagramSize)
                {
                    this.CopyToDatagramBuffer(start, length);
                }
                else  // 不含首字符或超长
                {
                    this.OnDatagramOversizeError();
                    m_datagramBuffer = null;  // 丢弃全部数据
                }
            }
        }

        /// <summary>
        /// Session重写入口, 基本功能: 
        /// 1) 判断包有效性与包类型(注意：包带起止符号); 
        /// 2) 分解包中的各字段数据
        /// 3) 校验包及其数据有效性
        /// 4) 发送确认消息给客户端(调用方法 SendDatagram())
        /// 5) 存储包数据到数据库中
        /// 6) 存储包原文到数据库中(可选)
        /// 7) 补充字段m_name, 表示数据包发送者的名称/编号
        /// 8) 其它相关方法
        /// </summary>
        protected abstract void AnalyzeDatagram(byte[] datagramBytes);

        protected virtual void GetDatagramFromBuffer(int startPos, int len)
        {
            byte[] datagramBytes;
            if (m_datagramBuffer != null)
            {
                datagramBytes = new byte[len + m_datagramBuffer.Length];
                Array.Copy(m_datagramBuffer, 0, datagramBytes, 0, m_datagramBuffer.Length);  // 先拷贝 Session 的数据缓冲区的数据
                Array.Copy(m_receiveBuffer, startPos, datagramBytes, m_datagramBuffer.Length, len);  // 再拷贝 Session 的接收缓冲区的数据
            }
            else
            {
                datagramBytes = new byte[len];
                Array.Copy(m_receiveBuffer, startPos, datagramBytes, 0, len);  // 再拷贝 Session 的接收缓冲区的数据
            }

            if (m_datagramBuffer != null)
            {
                m_datagramBuffer = null;
            }

            m_datagramQueue.Enqueue(datagramBytes);
        }

        protected virtual void OnDatagramDelimiterError()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramDelimiterError;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramOversizeError()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramOversizeError;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramAccepted()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramAccepted;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramError()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramError;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramHandled()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramHandled;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnMsgProcessing(int sessionID, string clientName, string datagramText)
        {
            EventHandler<TSessionCmdEventArgs> handler = this.MsgProcessing;
            if (handler != null)
            {
                TSessionCmdEventArgs e = new TSessionCmdEventArgs(sessionID, clientName, datagramText);
                handler(this, e);
            }
        }

        protected virtual void OnSessionReceiveException(Exception err)
        {
            EventHandler<TSessionExceptionEventArgs> handler = this.SessionReceiveException;
            if (handler != null)
            {
                TSessionExceptionEventArgs e = new TSessionExceptionEventArgs(err, this);
                handler(this, e);
            }
        }

        protected virtual void OnSessionSendException(Exception err)
        {
            EventHandler<TSessionExceptionEventArgs> handler = this.SessionSendException;
            if (handler != null)
            {
                TSessionExceptionEventArgs e = new TSessionExceptionEventArgs(err, this);
                handler(this, e);
            }
        }

        protected void OnShowDebugMessage(string message)
        {
            if (this.ShowDebugMessage != null)
            {
                TExceptionEventArgs e = new TExceptionEventArgs(message);
                this.ShowDebugMessage(this, e);
            }
        }

        #endregion 
    }

    /// <summary>
    /// 发送和接收公共缓冲区
    /// </summary>
    public sealed class BufferManager
    {
        private byte[] m_receiveBuffer;
        private byte[] m_sendBuffer;

        private int m_maxSessionCount;
        private int m_receiveBufferSize;
        private int m_sendBufferSize;

        private int m_bufferBlockIndex;
        private Stack<int> m_bufferBlockIndexStack;

        public BufferManager(int maxSessionCount, int receivevBufferSize, int sendBufferSize)
        {
            m_maxSessionCount = maxSessionCount;
            m_receiveBufferSize = receivevBufferSize;
            m_sendBufferSize = sendBufferSize;

            m_bufferBlockIndex = 0;
            m_bufferBlockIndexStack = new Stack<int>();

            m_receiveBuffer = new byte[m_receiveBufferSize * m_maxSessionCount];
            m_sendBuffer = new byte[m_sendBufferSize * m_maxSessionCount];
        }

        public int ReceiveBufferSize
        {
            get { return m_receiveBufferSize; }
        }

        public int SendBufferSize
        {
            get { return m_sendBufferSize; }
        }

        public byte[] ReceiveBuffer
        {
            get { return m_receiveBuffer; }
        }

        public byte[] SendBuffer
        {
            get { return m_sendBuffer; }
        }

        public void FreeBufferBlockIndex(int bufferBlockIndex)
        {
            if (bufferBlockIndex == -1)
            {
                return;
            }

            lock (this)
            {
                m_bufferBlockIndexStack.Push(bufferBlockIndex);
            }
        }

        public int GetBufferBlockIndex()
        {
            lock (this)
            {
                int blockIndex = -1;

                if (m_bufferBlockIndexStack.Count > 0)  // 有用过释放的缓冲块
                {
                    blockIndex = m_bufferBlockIndexStack.Pop();
                }
                else
                {
                    if (m_bufferBlockIndex < m_maxSessionCount)  // 有未用缓冲区块
                    {
                        blockIndex = m_bufferBlockIndex++;
                    }
                }

                return blockIndex;
            }
        }

        public int GetReceivevBufferOffset(int bufferBlockIndex)
        {
            if (bufferBlockIndex == -1)  // 没有使用共享块
            {
                return 0;
            }

            return bufferBlockIndex * m_receiveBufferSize;
        }

        public int GetSendBufferOffset(int bufferBlockIndex)
        {
            if (bufferBlockIndex == -1)  // 没有使用共享块
            {
                return 0;
            }

            return bufferBlockIndex * m_sendBufferSize;
        }

        public void Clear()
        {
            lock (this)
            {
                m_bufferBlockIndexStack.Clear();
                m_receiveBuffer = null;
                m_sendBuffer = null;
            }
        }
    }

    /// <summary>
    /// 数据库抽象类, 只给出了几个抽象方法, 派生后需要增加实现
    /// 1) Open方法, 给出具体的SqlConnection/OleDbConnection
    /// 2) 其它抽象方法, 这些实现要在 TSocketServerBase 中调用
    /// 3) 已经给出了两个派生类：TSqlServerBase/TOleDatabaseBase
    /// </summary>
    public abstract class TDatabaseBase: IDatabaseEvent
    {
        private string m_dbConnectionString = string.Empty;
        private DbConnection m_dbConnection;

        public event EventHandler<TExceptionEventArgs> DatabaseOpenException;
        public event EventHandler<TExceptionEventArgs> DatabaseException;
        public event EventHandler<TExceptionEventArgs> DatabaseCloseException;

        /// <summary>
        /// 作泛型参数类型时, 必须有无参构造函数
        /// </summary>
        protected TDatabaseBase() { }

        public ConnectionState State
        {
            get { return m_dbConnection.State; }
        }

        /// <summary>
        /// 在Session抽象方法AnalyzeDatagram中要该连接对象
        /// </summary>
        public virtual DbConnection DbConnection
        {
            get { return m_dbConnection; }
            protected set { m_dbConnection = value; }
        }

        public string DbConnectionString
        {
            get { return m_dbConnectionString; }
        }

        /// <summary>
        /// 1) 替构造函数初始化参数
        /// 2) dbConnectionString 是数据库连接串
        /// </summary>
        public void Initiate(string dbConnectionString)
        {
            m_dbConnectionString = dbConnectionString;
        }

        /// <summary>
        /// 抽象方法, 重写时需要:
        /// 1) 创建具体类型的连接对象
        ///    (1) Ole数据库连接：m_dbConnection = new OleDbConnection();
        ///    (2) SqlServer连接：m_dbConnection = new SqlConnection();
        /// 2) 创建其它与具体连接对象的对象如：SqlCommand/OleDbCommand等
        /// </summary>
        public abstract void Open();

        public virtual void Store(byte[] datagramBytes, TSessionBase session) { }

        public void Close()
        {
            if (m_dbConnection == null)
            {
                return;
            }

            try
            {
                this.Clear();  // 清理派生类的相关资源
                m_dbConnection.Close();
            }
            catch (Exception err)
            {
                this.OnDatabaseCloseException(err);
            }
        }

        /// <summary>
        /// 1) 关闭数据库前清理非连接(Connection)资源
        /// 2) 可在派生类中重写该方法
        /// </summary>
        protected virtual void Clear() { }

        protected virtual void OnDatabaseOpenException(Exception err)
        {
            EventHandler<TExceptionEventArgs> handler = this.DatabaseOpenException;
            if (handler != null)
            {
                TExceptionEventArgs e = new TExceptionEventArgs(err);
                handler(this, e);
            }
        }

        protected virtual void OnDatabaseCloseException(Exception err)
        {
            EventHandler<TExceptionEventArgs> handler = this.DatabaseCloseException;
            if (handler != null)
            {
                TExceptionEventArgs e = new TExceptionEventArgs(err);
                handler(this, e);
            }
        }

        /// <summary>
        /// Session中触发的事件
        /// </summary>
        protected virtual void OnDatabaseException(Exception err)
        {
            EventHandler<TExceptionEventArgs> handler = this.DatabaseException;
            if (handler != null)
            {
                TExceptionEventArgs e = new TExceptionEventArgs(err);
                handler(this, e);
            }
        }

    }

    /// <summary>
    /// SqlServer数据库类, 可以再派生并增加属性与字段
    /// </summary>
    public class TSqlServerBase : TDatabaseBase
    {
        public override DbConnection DbConnection
        {
            get
            {
                SqlConnection dbConn = base.DbConnection as SqlConnection;
                return dbConn;
            }
        }

        public override void Open()
        {
            try
            {
                this.Close();

                base.DbConnection = new SqlConnection(base.DbConnectionString);
                base.DbConnection.Open();
            }
            catch (Exception err)
            {
                this.OnDatabaseOpenException(err);
            }
        }
    }

    /// <summary>
    /// OldDb数据库类, 可以再派生并增加属性与字段
    /// </summary>
    public class TOleDatabaseBase : TDatabaseBase
    {
        public override DbConnection DbConnection
        {
            get
            {
                OleDbConnection dbConn = base.DbConnection as OleDbConnection;
                return dbConn;
            }
        }

        public override void Open()
        {
            try
            {
                this.Close();
                
                base.DbConnection = new OleDbConnection(base.DbConnectionString);
                base.DbConnection.Open();
            }
            catch (Exception err)
            {
                this.OnDatabaseOpenException(err);
            }
        }
    }


    public interface ISessionEvent
    {
        event EventHandler<TSessionExceptionEventArgs> SessionReceiveException;
        event EventHandler<TSessionExceptionEventArgs> SessionSendException;
        event EventHandler<TSessionEventArgs> DatagramDelimiterError;
        event EventHandler<TSessionEventArgs> DatagramOversizeError;
        event EventHandler<TSessionEventArgs> DatagramAccepted;
        event EventHandler<TSessionEventArgs> DatagramError;
        event EventHandler<TSessionEventArgs> DatagramHandled;
    }

    public interface IDatabaseEvent
    {
        event EventHandler<TExceptionEventArgs> DatabaseOpenException;
        event EventHandler<TExceptionEventArgs> DatabaseException;
        event EventHandler<TExceptionEventArgs> DatabaseCloseException;
    }

    public class TExceptionEventArgs: EventArgs
    {
        private string m_exceptionMessage;

        public TExceptionEventArgs(Exception exception)
        {
            m_exceptionMessage = exception.Message;
        }

        public TExceptionEventArgs(string exceptionMessage)
        {
            m_exceptionMessage = exceptionMessage;
        }

        public string ExceptionMessage
        {
            get { return m_exceptionMessage; }
        }
    }

    public class TSessionEventArgs: EventArgs
    {
        TSessionCoreInfo m_sessionBaseInfo;

        public TSessionEventArgs(TSessionCoreInfo sessionCoreInfo)
        {
            m_sessionBaseInfo = sessionCoreInfo;
        }

        public TSessionCoreInfo SessionBaseInfo
        {
            get { return m_sessionBaseInfo; }
        }
    }

    //指令处理
    public class TSessionCmdEventArgs : EventArgs
    {
        int sessionID;
        string _ClientName;
        string cmd;
        

        public TSessionCmdEventArgs(int sessionid,string clientName,string cmdinfo)
        {
            sessionID = sessionid;
            _ClientName = clientName;
            cmd = cmdinfo;
        }

        public string SessionCmdName
        {
            get { return _ClientName; }
        }

        public string SessionCmdInfo
        {
            get { return cmd; }
        }

        public int SessionID
        {
            get { return sessionID; }
        }
    }

    public class TSessionExceptionEventArgs : TSessionEventArgs
    {
        private string m_exceptionMessage;

        public TSessionExceptionEventArgs(Exception exception, TSessionCoreInfo sessionCoreInfo)
            : base(sessionCoreInfo)
        {
            m_exceptionMessage = exception.Message;
        }

        public string ExceptionMessage
        {
            get { return m_exceptionMessage; }
        }
    }

  

    public enum TDisconnectType
    {
        Normal,     // disconnect normally
        Timeout,    // disconnect because of timeout
        Exception   // disconnect because of exception
    }

    public enum TSessionState
    {
        Active,    // state is active
        Inactive,  // session is inactive and will be closed
        Shutdown,  // session is shutdownling
        Closed     // session is closed
    }
}