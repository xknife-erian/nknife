﻿namespace NKnife.Protocol
{
    /// <summary>
    /// 当通讯的一端接收到消息后，将消息进行处理的解析器
    /// </summary>
    public interface IProtocolUnPacker<T>
    {
        /// <summary>获取协议的版本号
        /// </summary>
        /// <value>The version.</value>
        short Version { get; }

        /// <summary>开始执行协议的解析
        /// </summary>
        void Execute(ref IProtocolContent<T> content, T data, string family, T command);
    }
}
