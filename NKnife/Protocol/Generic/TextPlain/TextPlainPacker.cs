using System.Text;

namespace NKnife.Protocol.Generic.TextPlain
{
    public class TextPlainPacker : StringProtocolPacker
    {
        public override string Combine(StringProtocolContent c)
        {
            var command = string.Format("{0}{2}{1}{2}", c.Command, c.CommandParam, TextPlainProtocolFlags.SplitFlag);
            var sb = new StringBuilder(command);
            foreach (var tag in c.Tags)
            {
                sb.Append(tag).Append(TextPlainProtocolFlags.SplitFlag);
            }
            foreach (var info in c.Infomations)
            {
                sb.Append(info.Key)
                    .Append(TextPlainProtocolFlags.InfomationSplitFlag)
                    .Append(info.Value)
                    .Append(TextPlainProtocolFlags.SplitFlag);
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
