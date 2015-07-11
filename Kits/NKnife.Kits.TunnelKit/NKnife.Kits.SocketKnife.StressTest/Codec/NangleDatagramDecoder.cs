using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Codec
{
    public class NangleDatagramDecoder : BytesDatagramDecoder
    {
        public byte FirstHeadByte { get; set; }
        public byte SecondHeadByte { get; set; }

        public NangleDatagramDecoder()
        {
            FirstHeadByte = 0xAA;
            SecondHeadByte = 0x55;
        }
        public override byte[][] Execute(byte[] data, out int finishedIndex)
        {
            var results = new List<byte[]>();
            finishedIndex = 0;
            var tempDataGram = new List<byte>();
            bool enableNewDataGram = false;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == FirstHeadByte) //头部第一个字节吻合
                {
                    if (i + 1 < data.Length) //有可能有头部第二个字节
                    {
                        if (data[i + 1] == SecondHeadByte) //头部第二个字节吻合
                        {
                            if (tempDataGram.Count > 0) //已经有数据了
                            {
                                byte[] item;
                                if (VerifyDataGram(tempDataGram, out item))
                                {
                                    results.Add(item);
                                    tempDataGram.Clear();
                                }
                                finishedIndex = i - 1;
                            }
                            enableNewDataGram = true;

                        }
                        else //头部第二个字节不吻合
                        {
                            if (enableNewDataGram)
                            {
                                tempDataGram.Add(data[i]);
                            }
                        }
                    }
                    else //不可能有头部第二个字节了
                    {
                        //什么也不做，等新数据进来
                    }
                }
                else
                {
                    if (enableNewDataGram)
                    {
                        tempDataGram.Add(data[i]);
                    }
                }
            }

            return results.ToArray();
        }

        private bool VerifyDataGram(List<byte> tempDataGram, out byte[] tempData)
        {
            int len = tempDataGram.Count;
            var source = tempDataGram.ToArray();
            if (!VerifyLenAndChk(source))
            {
                tempData = new byte[] {};
                return false;
            }
            tempData = new byte[len - 2];
            Array.Copy(source,1,tempData,0,len-2); //去掉第一个字节的长度和最后一个字节的校验和
            return true;
        }

        private bool VerifyLenAndChk(byte[] source)
        {
            int len = source.Length;
            if (len < 2)
                return false;
            byte lenByte = source[0];
            byte chk = source[len - 1];

            if (lenByte != len - 2) //长度不正确
                return false;
            int sum = lenByte;
            for (int i = 0; i < len - 2; i++)
            {
                sum += source[i + 1];
            }
            if(chk != (sum % 255)) //校验和不正确
                return false;
            return true;
        }
    }
}
