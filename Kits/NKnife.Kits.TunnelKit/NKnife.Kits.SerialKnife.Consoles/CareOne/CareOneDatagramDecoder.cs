using System.Collections.Generic;
using System.Text;
using NKnife.Converts;
using NKnife.Tunnel.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneDatagramDecoder : StringDatagramDecoder
    {
        public const byte Lead = 0x09;

        public override string[] Execute(byte[] data, out int finishedIndex)
        {
            finishedIndex = 0;
            var ps = new List<string>();
            bool hasData = true;
            while (hasData)
            {
                if (data[finishedIndex] == Lead)
                {
                    int length;
                    StringBuilder sb;
                    bool parseSuccess = ParseSingle(data, finishedIndex, out length, out sb);
                    if (!parseSuccess)
                    {
                        hasData = false;
                        continue;
                    }
                    ps.Add(sb.ToString());
                    finishedIndex = finishedIndex + length;
                }
                if (finishedIndex >= data.Length)
                    hasData = false;
            }
            return ps.ToArray();
        }

        protected virtual bool ParseSingle(byte[] data, int index, out int length, out StringBuilder sb)
        {
            sb = new StringBuilder();
            length = 0;
            if (data[index] != 0x09)
                return false;
            var address = UtilityConvert.ConvertTo<int>(data[index + 1]);
            length = UtilityConvert.ConvertTo<int>(data[index + 2]);
            string command = UtilityConvert.BytesToHex(new[] {data[index + 3]}, false);
            string content = Encoding.ASCII.GetString(data, index + 4, length);
            sb.Append(command).Append('`').Append(address).Append('`').Append(content);
            return true;
        }
    }
}