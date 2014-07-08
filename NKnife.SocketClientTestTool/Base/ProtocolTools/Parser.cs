using SocketKnife.Protocol.Implementation;

namespace NKnife.SocketClientTestTool.Base.ProtocolTools
{
    public class Parser : ProtocolXmlParser
    {
        public override short Version
        {
            get { return 1; }
        }

        public override string XPathDatas
        {
            get { return "DATA"; }
        }
    }
}