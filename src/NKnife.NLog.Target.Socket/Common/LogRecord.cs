using MemoryPack;
using MemoryPack.Formatters;
using NLog;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace NKnife.NLog.Target.Socket.Common
{
    /// <summary>
    ///     表示日志记录的类。目的是将NLog的 <see cref="LogEventInfo" /> 转换为简化的可序列化对象。<br />
    ///     一是为了减少序列化的数据量，二是为了避免序列化 <see cref="LogEventInfo" /> 时出现循环引用的问题。
    /// </summary>
    [MemoryPackable]
    public partial record LogRecord
    {
        private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
        {
            WriteIndented = false
        };

        [MemoryPackConstructor]
        public LogRecord() { }

        /// <summary>
        ///     初始化 <see cref="LogRecord" /> 类的新实例。
        /// </summary>
        /// <param name="logEventInfo">日志事件信息。</param>
        public LogRecord(LogEventInfo logEventInfo)
        {
            TimeStamp        = logEventInfo.TimeStamp;
            Level            = logEventInfo.Level;
            Exception        = logEventInfo.Exception;
            LoggerName       = logEventInfo.LoggerName;
            FormattedMessage = logEventInfo.FormattedMessage;
            StackTrace       = logEventInfo.StackTrace;
        }

        /// <summary>
        ///     获取日志记录的终止符。
        /// </summary>
        public static string Terminator => "\r\t\n";

        public static byte[] TerminatorBytes => Encoding.UTF8.GetBytes(Terminator);

        /// <summary>
        ///     获取或设置日志记录的时间戳。
        /// </summary>
        [MemoryPackOrder(0)] public DateTime TimeStamp { get; set; }

        /// <summary>
        ///     获取或设置日志记录的级别。
        /// </summary>
        [MemoryPackOrder(1)][LogLevelFormatter] public LogLevel Level { get; set; } = LogLevel.Info;

        /// <summary>
        ///     获取或设置日志记录的记录器名称。
        /// </summary>
        [MemoryPackOrder(2)] public string? LoggerName { get; set; }

        /// <summary>
        ///     获取或设置日志记录的格式化消息。
        /// </summary>
        [MemoryPackOrder(3)] public string FormattedMessage { get; set; } = string.Empty;

        /// <summary>
        ///     获取或设置日志记录的异常。
        /// </summary>
        [MemoryPackOrder(4)] public Exception? Exception { get; set; }

        /// <summary>
        ///     获取或设置日志记录的堆栈跟踪。
        /// </summary>
        [MemoryPackOrder(5)] public StackTrace? StackTrace { get; set; }

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

        public Task<byte[]> ToBinaryAsync()
        {
            var pack = MemoryPackSerializer.Serialize(this);
            //memoryStream.Write(TerminatorBytes, 0, TerminatorBytes.Length); // 添加终止符

            return Task.FromResult(pack);
        }

        public static LogRecord? FromJson(string json)
        {
            return JsonSerializer.Deserialize<LogRecord>(json, s_jsonSerializerOptions);
        }
    }

    public sealed class LogLevelFormatterAttribute : MemoryPackCustomFormatterAttribute<LogLevel>
    {
        public override IMemoryPackFormatter<LogLevel> GetFormatter()
        {
            return new LogLevelFormatter();
        }

        private class LogLevelFormatter : IMemoryPackFormatter<LogLevel>
        {
            public void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref LogLevel? value)
                where TBufferWriter : class, IBufferWriter<byte>
            {
                if(value != null)
                    writer.WriteValue(value.Name);
            }

            public void Deserialize(ref MemoryPackReader reader, scoped ref LogLevel? value)
            {
                var name = reader.ReadString();
                if(!string.IsNullOrEmpty(name))
                    value = LogLevel.FromString(name);
            }
        }
    }
}