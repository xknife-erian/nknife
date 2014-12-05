using NKnife.Protocol.Generic;

namespace NKnife.Protocol
{
    public static class ProtocolExtension
    {
        /// <summary>
        ///     增加一个对象做为协议数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="value">The value.</param>
        public static void AddTag(this StringProtocol content, object value)
        {
            content.Tags.Add(value);
        }

        /// <summary>
        ///     清除所有做为协议数据的对象。
        /// </summary>
        public static void ClearTag(this StringProtocol content)
        {
            content.Tags.Clear();
        }

        /// <summary>
        ///     移除指定索引的协议数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="index">The index.</param>
        public static void RemoveTag(this StringProtocol content, int index)
        {
            content.Tags.RemoveAt(index);
        }

        /// <summary>
        ///     设置命令字参数.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="obj">The obj.</param>
        public static void SetCommandParam(this StringProtocol content, string obj)
        {
            content.CommandParam = obj;
        }

        /// <summary>
        ///     清除命令字参数
        /// </summary>
        public static void ClearCommandParam(this StringProtocol content)
        {
            content.CommandParam = null;
        }

        /// <summary>
        ///     增加固定信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddInfo(this StringProtocol content, string key, string value)
        {
            content.Infomations.Add(key, value);
        }

        /// <summary>
        /// 获取指定的信息，不做异常处理，如果key不存在则会抛出异常
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetInfo(this StringProtocol content, string key)
        {
            return content.Infomations[key];
        }

        /// <summary>
        ///     移除指定的信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key">The key.</param>
        public static void RemoveInfo(this StringProtocol content, string key)
        {
            content.Infomations.Remove(key);
        }

        /// <summary>
        ///     清除所有信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        public static void ClearInfo(this StringProtocol content)
        {
            content.Infomations.Clear();
        }
    }
}