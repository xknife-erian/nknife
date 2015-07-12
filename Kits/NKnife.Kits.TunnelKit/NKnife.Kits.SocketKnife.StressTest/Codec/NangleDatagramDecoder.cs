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

        private const int LENGTH_BYTE_COUNT = 2;
        private const int TARGET_BYTE_COUNT = 4;
        private const int COMMAND_BYTE_COUNT = 2;
        private const int CHK_BYTE_COUNT = 1;

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
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == FirstHeadByte) //头部第一个字节吻合
                {
                    if (i + 1 < data.Length) //有可能有头部第二个字节
                    {
                        if (data[i + 1] == SecondHeadByte) //头部第二个字节吻合
                        {
                            i++; //第二个字节吻合，则i递加

                            //判断从当前的头部起，剩下的数据有没有至少一条完整的数据（长度够）
                            //i当前应指向头部第二个字节
                            if (i + LENGTH_BYTE_COUNT + TARGET_BYTE_COUNT + COMMAND_BYTE_COUNT + CHK_BYTE_COUNT <
                                data.Length)
                            {
                                //剩下的数据，长度足够容纳一条最小的（空数据）datagram
                                //先取长度
                                int len = GetLenghFromTwoBytesToInt(new[] {data[i + 1], data[i + 2]});
                                //根据长度判断，剩下的数据长度能否容纳完整的datagram（根据len计算）
                                if (i + LENGTH_BYTE_COUNT + len < data.Length)
                                {
                                    for (int j = 0; j < LENGTH_BYTE_COUNT + len; j++)
                                    {
                                        tempDataGram.Add(data[i+1+j]);
                                    }

                                    int tempFirstHeadIndex = tempDataGram.IndexOf(FirstHeadByte);
                                    if (tempFirstHeadIndex > -1)
                                        //有问题了，不应该出现头字符，如果出现了，说明这条数据本身就不完整了，不需要去校验了，直接抛弃
                                    {
                                        i += tempFirstHeadIndex;
                                        finishedIndex = i;
                                    }
                                    else
                                    {
                                        byte[] item;
                                        if (VerifyDataGram(tempDataGram, out item))
                                        {
                                            results.Add(item);
                                        }
                                        i += tempDataGram.Count;

                                        finishedIndex = i + 1;
                                    }
                                    tempDataGram.Clear();
                                }
                            }
                        }
                    }
                }
            }
            return results.ToArray();
        }

        /// <summary>
        /// 验证数据的正确性，校验通过后去掉头部2个字节的长度域和尾部1个字节的校验和域
        /// </summary>
        /// <param name="tempDataGram"></param>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public bool VerifyDataGram(List<byte> tempDataGram, out byte[] tempData)
        {
            int len = tempDataGram.Count;
            var source = tempDataGram.ToArray();
            if (!VerifyLenAndChk(source))
            {
                tempData = new byte[] {};
                return false;
            }
            tempData = new byte[len - 3];
            Array.Copy(source,2,tempData,0,len-3); //去掉头两字节的长度和最后一个字节的校验和
            return true;
        }

        public bool VerifyLenAndChk(byte[] source)
        {
            int len = source.Length;
            if (len < 9) //至少要包含帧长度2+目的地址长度4+命令字长度2+校验和1
                return false;
            byte[] lenByte = {source[0],source[1]};
            byte chk = source[len - 1];


            if (GetLenghFromTwoBytesToInt(lenByte) != len - 2) //长度不正确
                return false;
            int sum = 0;
            for (int i = 2; i < len-1; i++) //不包含头两位的长度，和最后一位的校验和
            {
                sum += source[i];
            }
            if(chk != (sum % 255)) //校验和不正确
                return false;
            return true;
        }

        private int GetLenghFromTwoBytesToInt(byte[] bytes)
        {
            if (bytes.Length != 2)
                return 0; //长度不正确，返回0长度
            return bytes[0]*255 + bytes[1];
        }
    }
}
