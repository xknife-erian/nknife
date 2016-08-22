using System.Text;

namespace NKnife.App.XPath
{
    public class BooleanOp
    {
        private readonly StringBuilder xpathStr;

        public BooleanOp()
        {
            xpathStr = new StringBuilder();
        }

        public BooleanOp(string query)
        {
            xpathStr = new StringBuilder();
            xpathStr.Append(query);
        }

        public string And(string query)
        {
            if (!xpathStr.ToString().Equals(""))
            {
                xpathStr.Append(" and ");
            }
            xpathStr.Append(query);
            return xpathStr.ToString();
        }

        public string Or(string query)
        {
            if (!xpathStr.ToString().Equals(""))
            {
                xpathStr.Append(" | ");
            }
            xpathStr.Append(query);
            return xpathStr.ToString();
        }
    }
}