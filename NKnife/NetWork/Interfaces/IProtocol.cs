using System.Collections.Generic;
using NKnife.NetWork.Protocol;

namespace NKnife.NetWork.Interfaces
{
    /// <summary>描述一个通讯会话中一次交易的内容的封装。进出协议的将都是组好的字符串，字节数组的解码与编码在通讯器中完成。
    /// </summary>
    public interface IProtocol
    {
        /// <summary>本协议的命令字.
        /// </summary>
        /// <value>The command.</value>
        string Command { get; set; }

        /// <summary>本协议的家族.
        /// </summary>
        /// <value>The family.</value>
        string Family { get; }

        /// <summary>本协议的工具接口集合
        /// </summary>
        ProtocolTools Tools { get; set; }

        /// <summary>协议的具体内容.
        /// </summary>
        /// <value>The content.</value>
        IProtocolContent Content { get; set; }

        /// <summary>通过索引的方式快速获得数据,与 Get 方法一致
        /// </summary>
        string this[string key] { get; }

        /// <summary>获取第一个数据(往往协议中数据不多，当只有一个数据时用该属性比较方便)
        /// </summary>
        /// <value>The first data.</value>
        KeyValuePair<string, string> FirstData { get; }

        /// <summary>根据指定的键获取数据
        /// </summary>
        string Get(string key);

        /// <summary>根据远端得到的数据包解析，将数据填充到本实例中
        /// </summary>
        /// <param name="datagram">The datas.</param>
        void Parse(string datagram);

        /// <summary>根据当前实例生成协议的原生字符串表达
        /// </summary>
        string Protocol();

        /// <summary>设置命令字参数.
        /// </summary>
        /// <param name="obj">The obj.</param>
        void SetCommandParam(string obj);

        /// <summary>清除命令字参数
        /// </summary>
        void ClearCommandParam();

        /// <summary>增加类似键值对样式的数据
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void AddData(string key, string value);

        /// <summary>移除指定键值的数据
        /// </summary>
        /// <param name="key">The key.</param>
        void RemoveData(string key);

        /// <summary>
        /// 清除所有数据
        /// </summary>
        void ClearData();

        /// <summary>
        /// 增加固定信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void AddInfo(string key, string value);

        /// <summary>
        /// 清除所有信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        void ClearInfo();

        /// <summary>
        /// 移除指定的信息。Info:协议制定时确认必须携带的数据,如:时间,交易ID等
        /// </summary>
        /// <param name="key">The key.</param>
        void RemoveInfo(string key);

        /// <summary>
        /// 增加一个对象做为协议数据
        /// </summary>
        /// <param name="value">The value.</param>
        void AddTag(object value);

        /// <summary>
        /// 清除所有做为协议数据的对象。
        /// </summary>
        void ClearTag();

        /// <summary>
        /// 移除指定索引的协议数据
        /// </summary>
        /// <param name="index">The index.</param>
        void RemoveTag(int index);
    }
}