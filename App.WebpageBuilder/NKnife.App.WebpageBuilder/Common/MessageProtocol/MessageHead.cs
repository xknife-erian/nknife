using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public class MessageHead
    {
        /// <summary>
        /// 消息头的长度，应该是永远不变的
        /// </summary>
        public readonly static int HeadLength = 36;

        /// <summary>
        /// 消息类型
        /// </summary>
        public int Type { get; private set; }

        /// <summary>
        /// 消息的命令
        /// </summary>
        public int Cmd { get; private set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        public int State { get; private set; }

        /// <summary>
        /// 消息体的长度
        /// </summary>
        public int BodyLength { get; internal set; }

        public MessageHead(int type, int cmd, int state)
            : this(type, cmd, state, -1)
        {
        }
        private MessageHead(int type, int cmd, int state, int length)
        {
            this.Type = type;
            this.Cmd = cmd;
            this.State = state;
            this.BodyLength = length;
        }

        /// <summary>
        /// 此字符串仅用来测试时浏览，最终的byte[]不会用此字符串来生成
        /// </summary>
        public override string ToString()
        {
            return Type + "-" + Cmd + "-" + State + "-" + BodyLength;
        }

        internal byte[] ToBytes()
        {
            byte[] bytes = new byte[HeadLength];

            ///将成员转换成byte数组
            byte[] bytesType = BitConverter.GetBytes(Type);
            byte[] bytesCmd = BitConverter.GetBytes(Cmd);
            byte[] bytesState = BitConverter.GetBytes(State);
            byte[] bytesLength = BitConverter.GetBytes(BodyLength);

            ///合并到一个byte数组中
            int currentInsertIndex = 0;
            Array.Copy(bytesType, 0, bytes, currentInsertIndex, bytesType.Length);
            currentInsertIndex += bytesType.Length;
            Array.Copy(bytesCmd, 0, bytes, currentInsertIndex, bytesCmd.Length);
            currentInsertIndex += bytesCmd.Length;
            Array.Copy(bytesState, 0, bytes, currentInsertIndex, bytesState.Length);
            currentInsertIndex += bytesState.Length;
            Array.Copy(bytesLength, 0, bytes, currentInsertIndex, bytesLength.Length);

            return bytes;
        }

        static public MessageHead Parse(byte[] bytes)
        {
            ///一段一段的解析
            int currentInsertIndex = 0;
            int type = BitConverter.ToInt32(bytes, currentInsertIndex);
            currentInsertIndex += 4;
            int cmd = BitConverter.ToInt32(bytes, currentInsertIndex);
            currentInsertIndex += 4;
            int state = BitConverter.ToInt32(bytes, currentInsertIndex);
            currentInsertIndex += 4;
            int length = BitConverter.ToInt32(bytes, currentInsertIndex);

            return new MessageHead(type, cmd, state, length);
        }
    }
}
