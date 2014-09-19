using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu
{
    public class MessageBag
    {
        /// <summary>
        /// 消息头
        /// </summary>
        public MessageHead Head { get; private set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] BytesBody { get; set; }

        public MessageBag(MessageHead head)
            : this(head, null)
        {
        }
        public MessageBag(MessageHead head, byte[] bytesBody)
        {
            Debug.Assert(head != null);

            this.Head = head;
            this.BytesBody = (bytesBody == null ? new byte[0] : bytesBody);
        }
        public virtual byte[] ToBytes()
        {
            Debug.Assert(Head != null);

            ///计算body中的长度
            Head.BodyLength = BytesBody.Length;
            if (Head.BodyLength == 0)
            {
                return Head.ToBytes();
            }

            ///将head和body的内容放在一起组成新的byte[]
            byte[] bytes = new byte[MessageHead.HeadLength + Head.BodyLength];
            byte[] bytesHead = Head.ToBytes();
            Array.Copy(bytesHead, 0, bytes, 0, bytesHead.Length);
            Array.Copy(BytesBody, 0, bytes, bytesHead.Length, BytesBody.Length);

            return bytes;
        }
    }
}
