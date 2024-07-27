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

        /// <summary>
        ///     是否使用Json格式进行传输
        /// </summary>
        /// <returns>true时，使用二进制传输；false时，使用Json字符串传输。默认采用二进制传输。</returns>
        [RequiredParameter]
        public bool UseJson { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            Task.Run(async () =>
            {
                try
                {
                    if(_tcpService.Count <= 0)
                        return;
                    var    record = new LogRecord(logEvent);
                    byte[] data;
                    if(UseJson)
                        data = Encoding.UTF8.GetBytes(record.ToJson());
                    else
                        data = await record.ToBinaryAsync();
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