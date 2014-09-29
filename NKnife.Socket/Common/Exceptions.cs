﻿using System;
using System.Runtime.Serialization;
using NKnife.Exceptions;

namespace SocketKnife.Common
{
    /// <summary>
    ///     Socket客户端无法连接时的异常
    /// </summary>
    [Serializable]
    public class SocketClientDisOpenedException : NKnifeException
    {
    }
}