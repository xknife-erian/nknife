using System;
using System.Globalization;

namespace NKnife.Protocol.Generic.TextPlain
{
    public class TextPlainUnPacker : StringProtocolUnPacker
    {
        public override void Execute(StringProtocolContent content, string data, string family, string command)
        {
            string[] array = data.Split(new[] {'|', '#', '@'}, StringSplitOptions.RemoveEmptyEntries);
            content.Command = command;
            if (array.Length > 1)
                content.CommandParam = array[1];
            if (array.Length > 2)
            {
                for (int i = 2; i < array.Length; i++)
                {
                    content.AddInfo(i.ToString(CultureInfo.InvariantCulture), array[i]);
                }
            }
        }
    }
}