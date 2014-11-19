using System.Text;

namespace NKnife.Protocol.Generic.TextPlain
{
    public class TextPlainPacker : StringProtocolPacker
    {
        public override string Combine(StringProtocolContent c)
        {
            var command = string.Format("{0}{2}{1}{2}", c.Command, c.CommandParam, TextPlainFlag.SplitFlag);
            var sb = new StringBuilder(command);
            foreach (var tag in c.Tags)
            {
                sb.Append(tag).Append(TextPlainFlag.SplitFlag);
            }
            foreach (var info in c.Infomations)
            {
                sb.Append(info.Key)
                    .Append(TextPlainFlag.InfomationSplitFlag)
                    .Append(info.Value)
                    .Append(TextPlainFlag.SplitFlag);
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
