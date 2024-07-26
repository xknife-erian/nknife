using NLog;
using NLog.Config;
using NLog.Targets;

namespace NKnife.NLog.Target.Socket.WebSocket
{
    /// <summary>
    /// NLog的WebSocket服务木比奥
    /// </summary>
    [Target("WebSocket")]
    public class WebSocketTarget : TargetWithLayout
    {
        /// <summary>
        /// 服务监听端口
        /// </summary>
        [RequiredParameter]
        public string Port { get; set; } = "5000";

        /// <summary>
        /// WebSocket绑定的路径
        /// </summary>
        [RequiredParameter]
        public string WsPath { get; set; } = "";

        /// <summary>
        /// WebSocket服务对象
        /// </summary>
        private WebSocketServer _webSocketServer;

        /// <summary>
        /// 连接的用户列表
        /// </summary>
        private List<UserToken> _userTokens = new List<UserToken>();

        /// <summary>
        /// 在开始写入日志前初始化目标
        /// </summary>
        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            _webSocketServer = new WebSocketServer(int.Parse(this.Port), this.WsPath);
            _webSocketServer.Opening += WebSocketServerOnOpening;
            _webSocketServer.Closed += WebSocketServerOnClosed;
            _webSocketServer.Listen();
#if DEBUG
            Console.WriteLine($"WebSocketServer已启动：{_webSocketServer.Uri}");
#endif
        }

        /// <summary>
        /// 关闭目标以释放任何初始化的资源
        /// </summary>
        protected override void CloseTarget()
        {
            _webSocketServer.Opening -= WebSocketServerOnOpening;
            _webSocketServer.Closed -= WebSocketServerOnClosed;

            base.CloseTarget();
        }

        /// <summary>
        /// 推送日志
        /// </summary>
        /// <param name="logEvent">日志信息</param>
        protected override void Write(LogEventInfo logEvent)
        {
            foreach (var token in _userTokens)
            {
                _webSocketServer.SendAsync(token, this.Layout.Render(logEvent));
            }
        }

        private void WebSocketServerOnClosed(UserToken userToken, byte[] data)
        {
            _userTokens.Remove(userToken);
#if DEBUG
            Console.WriteLine($"{DateTime.Now} 用户[{userToken.RemoteAddress}]退出，现有用户数:{_userTokens.Count}");
#endif
        }

        private void WebSocketServerOnOpening(UserToken userToken, byte[] data)
        {
            _userTokens.Add(userToken);
#if DEBUG
            Console.WriteLine($"{DateTime.Now} 用户[{userToken.RemoteAddress}]登录，现有用户数:{_userTokens.Count}");
#endif
        }
    }
}