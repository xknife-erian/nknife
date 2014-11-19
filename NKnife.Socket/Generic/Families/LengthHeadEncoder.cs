﻿using System;
using System.Text;
using Common.Logging;
using NKnife.Interface;
using NKnife.Tunnel;
using NKnife.Zip;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    /// <summary>
    /// 一个最常用的回复消息的字节数组生成器
    /// </summary>
    public class LengthHeadEncoder : KnifeSocketDatagramEncoder
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region IDatagramEncoder Members

        /// <summary>
        /// 是否启用Gzip压缩
        /// </summary>
        public bool EnabelCompress { get; set; }

        public override byte[] Execute(string replay)
        {
            if (string.IsNullOrWhiteSpace(replay))
            {
                return null;
            }

            //处理是否压缩字符串的字节数组进行发送
            byte[] reBytes;
            if (EnabelCompress)
            {
                var src = Encoding.UTF8.GetBytes(replay);
                reBytes = CompressHelper.Compress(src);
                _logger.Trace(string.Format("发送字符串压缩:原始:{0},压缩后:{1}", src.Length, reBytes.Length));
            }
            else
            {
                reBytes = Encoding.UTF8.GetBytes(replay);
            }
            //得到将要发送的数据的长度
            var head = BitConverter.GetBytes(reBytes.Length);

            var bs = new byte[head.Length + reBytes.Length];
            Array.Copy(head, bs, head.Length);
            Array.Copy(reBytes, 0, bs, head.Length, reBytes.Length);
            return bs;
        }

        #endregion
    }
}