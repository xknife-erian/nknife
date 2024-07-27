using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using Microsoft.Extensions.ObjectPool;
using NLog;

namespace NKnife.NLog.Target.Socket.Common
{
    /// <summary>
    ///     表示日志记录的类。目的是将NLog的 <see cref="LogEventInfo" /> 转换为简化的可序列化对象。<br />
    ///     一是为了减少序列化的数据量，二是为了避免序列化 <see cref="LogEventInfo" /> 时出现循环引用的问题。
    /// </summary>
    [MessagePackObject]
    public record LogRecord
    {
        private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
        {
            WriteIndented = false
        };

        private static readonly MessagePackSerializerOptions s_messagePackSerializerOptions = MessagePackSerializerOptions.Standard.WithResolver(CompositeResolver.Create(
            new IMessagePackFormatter[] {new LogLevelFormatter()},
            new IFormatterResolver[] {StandardResolver.Instance, ContractlessStandardResolver.Instance}
        ));

        static LogRecord()
        {
            // 为了避免序列化时出现循环引用的问题，需要设置以下选项。
            s_jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //s_jsonSerializerOptions.Converters.Add(new StackTraceConverter());
        }

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
        [Key(0)] public DateTime TimeStamp { get; set; }

        /// <summary>
        ///     获取或设置日志记录的级别。
        /// </summary>
        [Key(1)] public LogLevel Level { get; set; } = LogLevel.Info;

        /// <summary>
        ///     获取或设置日志记录的异常。
        /// </summary>
        [Key(4)] public Exception? Exception { get; set; }

        /// <summary>
        ///     获取或设置日志记录的记录器名称。
        /// </summary>
        [Key(2)] public string? LoggerName { get; set; }

        /// <summary>
        ///     获取或设置日志记录的格式化消息。
        /// </summary>
        [Key(3)] public string FormattedMessage { get; set; } = string.Empty;

        /// <summary>
        ///     获取或设置日志记录的堆栈跟踪。
        /// </summary>
        [Key(5)] public StackTrace? StackTrace { get; set; }

        public override string ToString()
        {
            return ToJson();
        }

        public string ToJson()
        {
            var json = $"{JsonSerializer.Serialize(this, s_jsonSerializerOptions)}{Terminator}";
            return json;
        }

        public async Task<byte[]> ToBinaryAsync()
        {
            using var memoryStream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync(memoryStream, this, s_messagePackSerializerOptions);
            memoryStream.Write(TerminatorBytes, 0, TerminatorBytes.Length); // 添加终止符
            return memoryStream.ToArray();
        }

        public static LogRecord? FromJson(string json)
        {
            return JsonSerializer.Deserialize<LogRecord>(json, s_jsonSerializerOptions);
        }
    }

    /// <summary>
    /// TODO: 优化内存流的池化, 没有测试，暂未启用
    /// </summary>
    public class MemoryStreamPool : ObjectPool<MemoryStream>
    {
        private static readonly IPooledObjectPolicy<MemoryStream> s_policy = new DefaultPooledObjectPolicy<MemoryStream>();
        private readonly int _initialCapacity;

        private readonly ObjectPool<MemoryStream> _pool = new DefaultObjectPool<MemoryStream>(s_policy);

        public MemoryStreamPool(int initialCapacity = 1024)
        {
            _initialCapacity = initialCapacity;
        }

        public override MemoryStream Get()
        {
            return _pool.Get() ?? new MemoryStream(_initialCapacity);
        }

        public override void Return(MemoryStream? stream)
        {
            stream ??= new MemoryStream(_initialCapacity);

            if(stream.Length > _initialCapacity)
            {
                // 如果流容量较大，则不重用。
                return;
            }

            // 清空流的内容以便下次使用。
            stream.SetLength(0);
        }
    }

    public class LogLevelFormatter : IMessagePackFormatter<LogLevel>
    {
        public void Serialize(ref MessagePackWriter writer, LogLevel value, MessagePackSerializerOptions options)
        {
            writer.Write(value.Name);
        }

        public LogLevel Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var name = reader.ReadString();
            return LogLevel.FromString(name);
        }
    }
}