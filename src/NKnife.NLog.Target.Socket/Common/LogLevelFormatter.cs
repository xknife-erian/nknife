using MessagePack;
using MessagePack.Formatters;
using NLog;

namespace NKnife.NLog.Target.Socket.Common;

public class LogLevelFormatter : IMessagePackFormatter<LogLevel>
{
    public void Serialize(ref MessagePackWriter writer, LogLevel value, MessagePackSerializerOptions options)
    {
        writer.Write(value.Name);
    }

    public LogLevel Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        string? logLevelName = reader.ReadString();

        if(string.IsNullOrEmpty(logLevelName))
            return LogLevel.Info;

        return LogLevel.FromString(logLevelName);
    }
}