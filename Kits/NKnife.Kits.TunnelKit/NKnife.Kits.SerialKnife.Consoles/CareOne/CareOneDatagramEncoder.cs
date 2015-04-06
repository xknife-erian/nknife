using System;
using System.Text;
using NKnife.Converts;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Generic;
using NKnife.Utility;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneDatagramEncoder : BaseDatagramEncoder<CareSaying>
    {
        public override byte[] Execute(CareSaying saying)
        {
            var content = Encoding.ASCII.GetBytes(saying.Content);
            short count = (saying.Command == 0xA0) ? (short) 2 : (short) 1;
            count += (short) content.Length;

            var data = new byte[content.Length + 3];
            data[0] = 0x08; //前导符
            data[1] = UtilityConvert.ConvertTo<byte>(saying.GpibAddress); //地址
            data[2] = UtilityConvert.ConvertTo<byte>(count); //数据长度
            data[3] = saying.Command; //命令字

            int begin = 4;
            if (saying.Command == 0xA0) //是否有子命令字
            {
                data[begin] = saying.SubCommand;
                begin++;
            }
            Buffer.BlockCopy(content, 0, data, begin, content.Length); //内容

            return data;
        }
    }
}