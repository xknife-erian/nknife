using NKnife.NLog.Target.Socket.WebSocket;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NKnife.NLog.Target.Socket
{
    /// <summary>
    ///     NLog的WebSocket服务木比奥
    /// </summary>
    [Target("WebSocket")]
    public class WebSocketTarget : TargetWithLayout
    {
        /// <summary>
        ///     连接的用户列表
        /// </summary>
        private readonly List<UserToken> _userTokens = new();

        /// <summary>
        ///     WebSocket服务对象
        /// </summary>
        private WebSocketServer? _webSocketServer;

        /// <summary>
        ///     服务监听端口
        /// </summary>
        [RequiredParameter]
        public string Port { get; set; } = "10100";

        /// <summary>
        ///     WebSocket绑定的路径
        /// </summary>
        [RequiredParameter]
        public string WebsocketUri { get; set; } = "";

        /// <summary>
        ///     在开始写入日志前初始化目标
        /// </summary>
        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            _webSocketServer = new WebSocketServer(int.Parse(Port), WebsocketUri);
            _webSocketServer.Opening += WebSocketServer_Opening;
            _webSocketServer.Closed += WebSocketServer_Closed;
            _webSocketServer.Listen();
#if DEBUG
            Console.WriteLine($"WebSocketServer已启动：{_webSocketServer.Uri}");
#endif
        }

        /// <summary>
        ///     关闭目标以释放任何初始化的资源
        /// </summary>
        protected override void CloseTarget()
        {
            if (_webSocketServer != null)
            {
                _webSocketServer.Opening -= WebSocketServer_Opening;
                _webSocketServer.Closed -= WebSocketServer_Closed;
            }

            base.CloseTarget();
        }

        /// <summary>
        ///     推送日志
        /// </summary>
        /// <param name="logEvent">日志信息</param>
        protected override void Write(LogEventInfo logEvent)
        {
            foreach (var token in _userTokens)
            {
                _webSocketServer?.SendAsync(token, Layout.Render(logEvent));
            }
        }

        private void WebSocketServer_Closed(UserToken userToken, byte[]? data)
        {
            _userTokens.Remove(userToken);
#if DEBUG
            Console.WriteLine($"{DateTime.Now} 用户[{userToken.RemoteAddress}]退出，现有用户数:{_userTokens.Count}");
#endif
        }

        private void WebSocketServer_Opening(UserToken userToken, byte[]? data)
        {
            _userTokens.Add(userToken);
#if DEBUG
            Console.WriteLine($"{DateTime.Now} 用户[{userToken.RemoteAddress}]登录，现有用户数:{_userTokens.Count}");
#endif
        }
    }
}