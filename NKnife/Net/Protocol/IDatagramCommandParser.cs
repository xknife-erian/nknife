﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Protocol
{
    /// <summary>
    /// 从原生消息体中获取命令字
    /// </summary>
    public interface IDatagramCommandParser
    {
        /// <summary>从原生消息体中获取命令字
        /// </summary>
        /// <param name="datagram">The datagram.</param>
        /// <returns></returns>
        string GetCommand(string datagram);
    }
}