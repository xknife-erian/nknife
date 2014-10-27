using System.Text;

namespace NKnife.Protocol.Generic.TextPlain
{
    public class TextPlainPacker : StringProtocolPacker
    {
        public override string Combine(StringProtocolContent c)
        {
            var command = string.Format("{0}|{1}|", c.Command, c.CommandParam);
            var sb = new StringBuilder(command);
            foreach (var info in c.Infomations)
            {
                sb.Append(info.Value).Append('|');
            }
            return sb.ToString();
        }
    }
}
