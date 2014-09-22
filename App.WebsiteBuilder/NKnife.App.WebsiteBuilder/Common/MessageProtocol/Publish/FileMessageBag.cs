using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public class FileMessageBag : MessageBag 
    {
        //public byte[] FileUrlByteCount { get; private set; }
        public int FileUrlByteCount { get; private set; }
        public string FileUrl { get; private set; }
        public byte[] FileBody { get; private set; }

        public FileMessageBag(string upFileUrl,byte[] body)
            : base(new MessageHead((int)MessageType.File, 0, 0))
        {
            FileUrlByteCount = Encoding.UTF8.GetByteCount(upFileUrl);
            FileUrl = upFileUrl;
            FileBody = body;
        }

        public FileMessageBag(byte[] bytesBody)
            :base(new MessageHead((int)MessageType.File,0,0),bytesBody)
        {
            //先取出前四个字节,此存放文件名的长度
            //FileUrlByteCount = new byte[4];
            //Array.Copy(bytesBody, 0, FileUrlByteCount, 0, FileUrlByteCount.Length);

            //得到文件名的长度
            FileUrlByteCount = BitConverter.ToInt32(bytesBody, 0);

            //从第5个字节取出长为nameLength的字节，此存放文件名的字节
            //byte[] fileNameByte = new byte[FileUrlByteCount];
            //Array.Copy(bytesBody, 4, fileNameByte, 0, FileUrlByteCount);

            //返回相应的字符，即文件名
            FileUrl = Encoding.UTF8.GetString(bytesBody, 4, FileUrlByteCount);

            //之后取出消息体
            int fileNameUseLength = FileUrlByteCount + 4;
            int bodylength = bytesBody.Length - fileNameUseLength; // Head.BodyLength - fileNameUseLength;此时Head.BodyLength没有值
            FileBody = new byte[bodylength];
            Array.Copy(bytesBody, fileNameUseLength, FileBody, 0, bodylength);
        }


        public override byte[] ToBytes()
        {
            byte[] nameByte = Encoding.UTF8.GetBytes(FileUrl);
            byte[] bytes = new byte[4 + nameByte.Length + FileBody.Length];
            byte[] fileUrlByteCountBytes = BitConverter.GetBytes(FileUrlByteCount);

            int currentInsertIndex = 0;
            Array.Copy(fileUrlByteCountBytes, 0, bytes, currentInsertIndex, 4);
            currentInsertIndex += 4;
            Array.Copy(nameByte, 0, bytes, currentInsertIndex, nameByte.Length);
            currentInsertIndex += nameByte.Length;
            Array.Copy(FileBody, 0, bytes, currentInsertIndex, FileBody.Length);

            BytesBody = bytes;
            return base.ToBytes();
        }
        
    }
}
