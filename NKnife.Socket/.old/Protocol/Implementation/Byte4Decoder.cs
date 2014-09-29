﻿using System;
using System.Collections.Generic;
using NKnife.Compress;
using NKnife.Utility;
using NLog;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Sockets;

namespace SocketKnife.Protocol.Implementation
{
    /// <summary>
    ///     一个最常用的 字符数组 => 字符串 转换器。
    /// </summary>
    public class Byte4Decoder : IDatagramDecoder
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        public virtual bool NeedReverse
        {
            get { return true; }
        }

        /// <summary>
        ///     是否在头部用4字节描述协议体的长度
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has length on head; otherwise, <c>false</c>.
        /// </value>
        public bool HasLengthOnHead
        {
            get { return true; }
        }

        /// <summary>
        ///     解码。将字节数组解析成字符串。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="done">已完成解码的数组的长度.</param>
        /// <returns></returns>
        public string[] Execute(byte[] data, out int done)
        {
            done = 0;
            var results = new List<string>();
            try
            {
                bool beginProcess = true;
                while (beginProcess)
                {
                    if (results.Count > 1)
                        _Logger.Trace("粘包处理,总长度:{0},已解析:{1},得到结果:{2}", data.Length, done, results.Count);
                    beginProcess = ExecuteSubMethod(data, done, results, NeedReverse, ref done);
                }
                return results.ToArray();
            }
            catch (Exception e)
            {
                _Logger.Warn("解码转换异常", e);
                return new string[0];
            }
        }

        private bool ExecuteSubMethod(byte[] data, int index, List<string> results, bool needReverse, ref int done)
        {
            if (UtilityCollection.IsNullOrEmpty(data))
                return false;
            if (data.Length <= 4)
                return false;
            var protocolBytes = new byte[] {};
            try
            {
                var lenArray = new byte[4];
                Buffer.BlockCopy(data, index, lenArray, 0, 4);
                if (needReverse)
                    Array.Reverse(lenArray);
                int protocolLength = BitConverter.ToInt32(lenArray, 0);//返回由字节数组中指定位置的四个字节转换来的 32 位有符号整数。
                if (index + 4 + protocolLength > data.Length) //这时又出现了半包现象
                {
                    _Logger.Trace("处理粘包时出现半包:起点:{0},计算得到的长度:{1},源数据长度:{2}", index, protocolLength, data.Length);
                    return false;
                }

                protocolBytes = new byte[protocolLength];
                Buffer.BlockCopy(data, index + 4, protocolBytes, 0, protocolLength);
            }
            catch (Exception e)
            {
                _Logger.Error("解码异常", e);
            }

            if (!UtilityCollection.IsNullOrEmpty(protocolBytes))
            {
                string tidyString = TidyString(protocolBytes);
                results.Add(tidyString);
            }
            done = index + 4 + protocolBytes.Length;

            return data.Length > done;
        }

        protected virtual string TidyString(byte[] protocol)
        {
            if (CompressHelper.IsCompressed(protocol)) //采用Gzip进行了压缩
            {
                byte[] decompress = CompressHelper.Decompress(protocol);
                return UtilityString.TidyUTF8(decompress);
            }
            return UtilityString.TidyUTF8(protocol);
        }
    }
}