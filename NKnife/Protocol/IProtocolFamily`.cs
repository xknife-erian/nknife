using System;
using System.Collections.Generic;

namespace NKnife.Protocol
{
    /// <summary>
    /// 一个描述协议的集合，协议族
    /// </summary>
    /// <typeparam name="TOriginal">内容在编程过程所使用的数据形式</typeparam>
    public interface IProtocolFamily<TOriginal>
    {
        string FamilyName { get; set; }

        IProtocolCommandParser<TOriginal> CommandParser { get; set; }

        IProtocol<TOriginal> Build(TOriginal command);

        /// <summary>根据远端得到的数据包解析，将数据填充到本实例中，与Generate方法相对
        /// </summary>
        IProtocol<TOriginal> Parse(TOriginal command, TOriginal datagram);

        /// <summary>根据当前协议实例生成为在传输过程所使用的数据形式
        /// </summary>
        TOriginal Generate(IProtocol<TOriginal> protocol);

        void AddPackerGetter(Func<TOriginal, IProtocolPacker<TOriginal>> func);

        void AddPackerGetter(TOriginal command, Func<TOriginal, IProtocolPacker<TOriginal>> func);
    }
}
