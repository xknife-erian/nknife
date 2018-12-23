namespace NKnife.Protocol
{
    public static class ProtocolExtension
    {
        /// <summary>
        ///     增加一个对象做为协议数据
        /// </summary>
        public static void AddTag<T>(this IProtocol<T> protocol, object value)
        {
            protocol.Tags.Add(value);
        }

        /// <summary>
        ///     清除所有做为协议数据的对象。
        /// </summary>
        public static void ClearTag<T>(this IProtocol<T> protocol)
        {
            protocol.Tags.Clear();
        }

        /// <summary>
        ///     移除指定索引的协议数据
        /// </summary>
        public static void RemoveTag<T>(this IProtocol<T> protocol, int index)
        {
            protocol.Tags.RemoveAt(index);
        }

        /// <summary>
        ///     设置命令字参数.
        /// </summary>
        public static void SetCommandParam<T>(this IProtocol<T> protocol, T obj)
        {
            protocol.CommandParam = obj;
        }

        /// <summary>
        ///     清除命令字参数
        /// </summary>
        public static void ClearCommandParam<T>(this IProtocol<T> protocol)
        {
            protocol.CommandParam = default(T);
        }

        /// <summary>
        ///     增加固定信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        public static void AddInfo<T>(this IProtocol<T> protocol, string key, T value)
        {
            protocol.Information.Add(key, value);
        }

        /// <summary>
        ///     获取指定的信息，不做异常处理，如果key不存在则会抛出异常
        /// </summary>
        public static T GetInfo<T>(this IProtocol<T> protocol, string key)
        {
            return protocol.Information[key];
        }

        /// <summary>
        ///     移除指定的信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        public static void RemoveInfo<T>(this IProtocol<T> protocol, string key)
        {
            protocol.Information.Remove(key);
        }

        /// <summary>
        ///     清除所有信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        public static void ClearInfo<T>(this IProtocol<T> protocol)
        {
            protocol.Information.Clear();
        }
    }
}