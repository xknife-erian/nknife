using System.Text;
using NKnife.NLog.Target.Socket.Common;
using NLog;
using NLog.Config;
using NLog.Targets;
using TouchSocket.Core;
using TouchSocket.Sockets;

namespace NKnife.NLog.Target.Socket
{
    [Target("SocketServer")]
    public class SocketServerTarget : global::NLog.Targets.Target
    {
        private readonly TouchSocketConfig _config = new();
        private readonly TcpService _tcpService;

        public SocketServerTarget()
        {
            _tcpService = new TcpService();

            try
            { //声明配置
                _config.SetListenIPHosts(new IPHost(int.Parse(Port)))
                       .SetMaxCount(5)
                       .SetTcpDataHandlingAdapter(() => new TerminatorPackageAdapter(LogRecord.Terminator))
                       .ConfigurePlugins(a => { a.UseReconnection(100, true, 100); }); //断线重连
                _tcpService.Setup(_config);                                            //载入配置
                _tcpService.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        ///     服务监听端口
        /// </summary>
        [RequiredParameter]
        public string Port { get; set; } = "10101";

        protected override void Write(LogEventInfo logEvent)
        {
            Task.Run(async () =>
            {
                try
                {
                    if(_tcpService.Count <= 0)
                        return;
                    var record  = new LogRecord(logEvent);
                    var data    = Encoding.UTF8.GetBytes(record.ToJson());
                    var clients = _tcpService.GetClients();
                    await Task.WhenAll(clients.Select(client => client.SendAsync(data)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}