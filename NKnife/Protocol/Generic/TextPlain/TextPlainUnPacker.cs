using System;
using System.Globalization;

namespace NKnife.Protocol.Generic.TextPlain
{
    public class TextPlainUnPacker : StringProtocolUnPacker
    {
        public override void Execute(StringProtocolContent content, string data, string family, string command)
        {
            string[] array = data.Split(new[] { TextPlainFlag.SplitFlag }, StringSplitOptions.RemoveEmptyEntries);
            content.Command = command;
            if (array.Length > 1)
            {
                content.CommandParam = array[1];
            }
            if (array.Length > 2)
            {
                for (int i = 2; i < array.Length; i++)
                {
                    var v = array[i];
                    if (!v.Contains(TextPlainFlag.InfomationSplitFlag))
                    {
                        content.AddTag(v);
                    }
                    else
                    {
                        var vam = v.Split(new[] { TextPlainFlag.InfomationSplitFlag }, StringSplitOptions.RemoveEmptyEntries);
                        if (vam.Length == 2)
                        {
                            content.AddInfo(vam[0], vam[1]);
                        }
                    }
                }
            }
        }
    }
}