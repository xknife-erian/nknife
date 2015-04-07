using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Converts;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneDatagramDecoder : BytesDatagramDecoder
    {
        public const byte LEAD = 0x09;

        public override byte[][] Execute(byte[] data, out int finishedIndex)
        {
            finishedIndex = 0;
            var css = new List<byte[]>();
            bool hasData = true;
            while (hasData)
            {
                if (data[finishedIndex] == LEAD)
                {
                    int length;
                    byte[] cs;
                    bool parseSuccess = ParseSingle(data, finishedIndex, out length, out cs);
                    if (!parseSuccess)
                    {
                        hasData = false;
                        continue;
                    }
                    css.Add(cs);
                    finishedIndex = finishedIndex + length;
                }
                if (finishedIndex >= data.Length)
                    hasData = false;
            }
            return css.ToArray();
        }

        /// <summary>
        /// 单条数据的提取
        /// </summary>
        /// <param name="data">整体数据流</param>
        /// <param name="index">开始提取的位置</param>
        /// <param name="length">提取完成的位置</param>
        /// <param name="cs">提取出的内容</param>
        /// <returns>是否提取成功</returns>
        protected virtual bool ParseSingle(byte[] data, int index, out int length, out byte[] cs)
        {
            var sl = UtilityConvert.ConvertTo<short>(data[index + 2]);
            length = 3 + sl;
            cs = new byte[length];
            Buffer.BlockCopy(data, index, cs, 0, length);
            return true;
        }
    }
}