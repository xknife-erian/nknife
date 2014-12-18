using System;
using System.Collections.Generic;

namespace NKnife.Protocol
{
    public interface IProtocolFamily<T>
    {
        string FamilyName { get; set; }

        IProtocolCommandParser<T> CommandParser { get; set; }

        //void Add(IProtocol<T> protocol);

        IProtocol<T> Build(T command);

        IProtocol<T> Parse(T command, T datagram);

        /// <summary>根据当前实例生成协议的原生字符串表达
        /// </summary>
        T Generate(IProtocol<T> protocol);

        void AddPackerGetter(Func<string, IProtocolPacker<T>> func);

        void AddPackerGetter(string command, Func<string, IProtocolPacker<T>> func);
    }
}
