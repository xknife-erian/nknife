using System;
using NKnife.Utility;
using NLog;
using SocketKnife.Interfaces;

namespace NKnife.SocketClient.StarterKit.Base.ProtocolTools
{
    /// <summary>
    /// 一个最常用的 字符数组 => 字符串 转换器。
    /// </summary>
    public class Decoder : IDatagramDecoder
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        #region IDatagramDecoder Members

        public bool HasLengthOnHead
        {
            get { return false; }
        }

        public string[] Execute(byte[] data, out int done)
        {
            done = data.Length;
            if (data.Length <= 4)
            {
                _Logger.Warn(string.Format("接收到的字节数组数据有误，长度<=4,{0}", data.Length));
                return new string[] {};
            }
            var replay = new byte[data.Length - 4];
            Array.Copy(data, 4, replay, 0, data.Length - 4); //.Net的Socket.Reciver机制可无需头部的长度描述
            return new[] {UtilityString.TidyUTF8(replay)};
        }

        #endregion
    }
}