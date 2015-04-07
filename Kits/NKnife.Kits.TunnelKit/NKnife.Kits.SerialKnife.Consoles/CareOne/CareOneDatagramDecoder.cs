using System.Collections.Generic;
using System.Text;
using NKnife.Converts;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneDatagramDecoder : BaseDatagramDecoder<CareSaying>
    {
        public const byte LEAD = 0x09;

        public override CareSaying[] Execute(byte[] data, out int finishedIndex)
        {
            finishedIndex = 0;
            var css = new List<CareSaying>();
            bool hasData = true;
            while (hasData)
            {
                if (data[finishedIndex] == LEAD)
                {
                    int length;
                    CareSaying cs;
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

        protected virtual bool ParseSingle(byte[] data, int index, out int length, out CareSaying cs)
        {
            cs = new CareSaying();
            length = 0;
            if (data[index] != 0x09)
                return false;
            cs.GpibAddress = data[index + 1];
            cs.Command = data[index + 3];
            cs.Length = UtilityConvert.ConvertTo<short>(data[index + 2]);
            cs.Content = Encoding.ASCII.GetString(data, index + 3 + 2, length - 2);
            length = 3 + length;
            return true;
        }
    }
}