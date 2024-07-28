using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using NLog;

namespace NKnife.NLog.Target.Socket.Common
{
    public class CustomResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new CustomResolver();

        private CustomResolver() { }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            if(typeof(T) == typeof(LogLevel))
            {
                return (IMessagePackFormatter<T>)new LogLevelFormatter();
            }

            return StandardResolver.Instance.GetFormatter<T>()!;
        }
    }
}