using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.MessageTemplates;
using NLog.Targets;
using NLog.Time;
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
            {    //声明配置
                _config.SetListenIPHosts(new IPHost(int.Parse(Port)))
                       .SetMaxCount(5)
                       .ConfigureContainer(c => { })
                       .SetTcpDataHandlingAdapter(() => new TerminatorPackageAdapter("\t\r\n"))
                       .ConfigurePlugins(a => { a.UseReconnection(100, true, 100); });   //断线重连
                _tcpService.Setup(_config);                                              //载入配置
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
            Task.Run(() =>
            {
                try
                {
                    var clients = _tcpService.GetClients();

                    if(!clients.Any())
                        return;
                    var    record = new LogRecord(logEvent);
                    string json   = JsonConvert.SerializeObject(record, Formatting.None);
                    byte[] data   = Encoding.UTF8.GetBytes($"{json}\t\r\n");
                    SendAsync(data).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        protected virtual async Task SendAsync(byte[] data)
        {
            try
            {
                await Task.WhenAll(_tcpService.GetClients().Select(client => client.SendAsync(data)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public record LogRecord
        {
            public LogRecord(LogEventInfo logEventInfo)
            {
                TimeStamp = logEventInfo.TimeStamp;
                Level = logEventInfo.Level;
                Exception = logEventInfo.Exception;
                LoggerName = logEventInfo.LoggerName;
                FormattedMessage = logEventInfo.FormattedMessage;
                StackTrace = logEventInfo.StackTrace;
            }

            public DateTime TimeStamp { get; set; }

            public global::NLog.LogLevel Level { get; set; }

            public Exception? Exception { get; set; }

            public string? LoggerName { get; set; }

            public string FormattedMessage { get; set; }

            public StackTrace StackTrace { get; set; }
        }
    }
}