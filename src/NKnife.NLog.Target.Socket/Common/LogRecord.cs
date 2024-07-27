using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using NLog;

namespace NKnife.NLog.Target.Socket.Common
{
    /// <summary>
    /// 表示日志记录的类。目的是将NLog的 <see cref="LogEventInfo"/> 转换为简化的可序列化对象。<br/>
    /// 一是为了减少序列化的数据量，二是为了避免序列化 <see cref="LogEventInfo"/> 时出现循环引用的问题。
    /// </summary>
    public record LogRecord
    {
        private static readonly JsonSerializerOptions s_jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        /// <summary>
        /// 获取日志记录的终止符。
        /// </summary>
        public static string Terminator => "\t\r\n";

        /// <summary>
        /// 初始化 <see cref="LogRecord"/> 类的新实例。
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
        /// 获取或设置日志记录的时间戳。
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 获取或设置日志记录的级别。
        /// </summary>
        public global::NLog.LogLevel Level { get; set; }

        /// <summary>
        /// 获取或设置日志记录的异常。
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// 获取或设置日志记录的记录器名称。
        /// </summary>
        public string? LoggerName { get; set; }

        /// <summary>
        /// 获取或设置日志记录的格式化消息。
        /// </summary>
        public string FormattedMessage { get; set; }

        /// <summary>
        /// 获取或设置日志记录的堆栈跟踪。
        /// </summary>
        public StackTrace StackTrace { get; set; }

        public override string ToString()
        {
            return ToJson();
        }

        public string ToJson()
        {
            string json = $"{System.Text.Json.JsonSerializer.Serialize(this, s_jsonSerializerOptions)}{Terminator}";
            return json;
        }
    }
}
