﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Protocol.General
{
    /// <summary>
    /// 一个最常用的回复消息的字节数组生成器,以“ö”结尾，防止粘包
    /// </summary>
    public class GeneralDatagramEncoder : IDatagramEncoder
    {
        public byte[] Execute(string replay, bool isCompress = false)
        {
            byte[] content = Encoding.UTF8.GetBytes(replay);
            byte[] tail = Encoding.UTF8.GetBytes("ö");
            var result = new byte[content.Length + tail.Length];
            Array.Copy(content, result, content.Length);
            Array.Copy(tail, 0, result, content.Length, tail.Length);
            return result;
        }
    }
}