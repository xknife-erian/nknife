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
            data[0] = 0x08; //ǰ����
            data[1] = UtilityConvert.ConvertTo<byte>(saying.GpibAddress); //��ַ
            data[2] = UtilityConvert.ConvertTo<byte>(count); //���ݳ���
            data[3] = saying.Command; //������

            int begin = 4;
            if (saying.Command == 0xA0) //�Ƿ�����������
            {
                data[begin] = saying.SubCommand;
                begin++;
            }
            Buffer.BlockCopy(content, 0, data, begin, content.Length); //����

            return data;
        }
    }
}