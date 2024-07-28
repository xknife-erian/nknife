using NLog;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using MessagePack;

namespace NKnife.NLog.Target.Socket.Common
{
    /// <summary>
    ///     表示日志记录的类。目的是将NLog的 <see cref="LogEventInfo" /> 转换为简化的可序列化对象。<br />
    ///     一是为了减少序列化的数据量，二是为了避免序列化 <see cref="LogEventInfo" /> 时出现循环引用的问题。
    /// </summary>
    [MessagePackObject]
    public partial record LogRecord
    {
        private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
        {
            WriteIndented = false
        };
        private static readonly MessagePackSerializerOptions s_mpOptions = 
            MessagePackSerializerOptions.Standard
                                        .WithResolver(CustomResolver.Instance);
                                        //.WithCompression(MessagePackCompression.Lz4Block);// 使用Lz4压缩
        public LogRecord() { }

        /// <summary>
        ///     初始化 <see cref="LogRecord" /> 类的新实例。
        /// </summary>
        /// <param name="logEventInfo">日志事件信息。</param>
        public LogRecord(LogEventInfo logEventInfo)
        {
            TimeStamp        = logEventInfo.TimeStamp;
            Level            = logEventInfo.Level;
            Exception        = ExceptionToString(logEventInfo.Exception);
            LoggerName       = logEventInfo.LoggerName;
            FormattedMessage = logEventInfo.FormattedMessage;
            StackTrace       = StackTraceToString(logEventInfo.StackTrace);
        }

        private static string? StackTraceToString(StackTrace? stackTrace)
        {
            var frames = stackTrace?.GetFrames();

            if(frames == null
               || frames.Length <= 0)
                return string.Empty;

            var sb = new StringBuilder();
            foreach (StackFrame frame in frames)
                sb.AppendLine(frame.ToString());

            return sb.ToString();
        }

        private static string? ExceptionToString(Exception? exception)
        {
            if (exception == null)
                return null;

            var sb = new StringBuilder();
            sb.AppendLine(exception.ToString());

            var innerException = exception.InnerException;
            while (innerException != null)
            {
                sb.AppendLine("Inner Exception:");
                sb.AppendLine(innerException.ToString());
                innerException = innerException.InnerException;
            }

            return sb.ToString();
        }

        /// <summary>
        ///     获取日志记录的终止符。
        /// </summary>
        public static string Terminator => "\r\t\n";

        public static byte[] TerminatorBytes => [0x0D, 0x09, 0x0A]; // \r\t\n

        /// <summary>
        ///     获取或设置日志记录的时间戳。
        /// </summary>
        [Key(0)]public DateTime TimeStamp { get; set; }

        /// <summary>
        ///     获取或设置日志记录的级别。
        /// </summary>
        [Key(1)] public LogLevel Level { get; set; } = LogLevel.Info;

        /// <summary>
        ///     获取或设置日志记录的记录器名称。
        /// </summary>
        [Key(2)] public string? LoggerName { get; set; }

        /// <summary>
        ///     获取或设置日志记录的格式化消息。
        /// </summary>
        [Key(3)] public string FormattedMessage { get; set; } = string.Empty;

        /// <summary>
        ///     获取或设置日志记录的异常。
        /// </summary>
        [Key(4)] public string? Exception { get; set; }

        /// <summary>
        ///     获取或设置日志记录的堆栈跟踪。
        /// </summary>
        [Key(5)] public string? StackTrace { get; set; }

        public override string ToString()
        {
            return ToJson();
        }

        public string ToJson()
        {
            var json = JsonSerializer.Serialize(this, s_jsonSerializerOptions);
            json = $"{json}{Terminator}";

            return json;
        }

        public byte[] ToBinary()
        {
            var writer  = new ArrayBufferWriter<byte>();
            MessagePackSerializer.Serialize(typeof(LogRecord), writer, this, s_mpOptions);
            writer.Write(TerminatorBytes);// 添加终止符字节
            return writer.WrittenSpan.ToArray();
        }

        public static LogRecord? FromJson(string json)
        {
            return JsonSerializer.Deserialize<LogRecord>(json, s_jsonSerializerOptions);
        }
    }

}