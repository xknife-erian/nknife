namespace Gean.Client.XPathTool
{
    using System;
    using System.Text;

    public class BooleanOp
    {
        private StringBuilder xpathStr;

        public BooleanOp()
        {
            this.xpathStr = new StringBuilder();
        }

        public BooleanOp(string query)
        {
            this.xpathStr = new StringBuilder();
            this.xpathStr.Append(query);
        }

        public string And(string query)
        {
            if (!this.xpathStr.ToString().Equals(""))
            {
                this.xpathStr.Append(" and ");
            }
            this.xpathStr.Append(query);
            return this.xpathStr.ToString();
        }

        public string Or(string query)
        {
            if (!this.xpathStr.ToString().Equals(""))
            {
                this.xpathStr.Append(" | ");
            }
            this.xpathStr.Append(query);
            return this.xpathStr.ToString();
        }
    }
}

