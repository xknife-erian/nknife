using System.Collections.Generic;
namespace Jeelu.SimplusD
{
    internal class SdCgiObject : XhtmlSection.CGIObject
    {
        internal SdCgiObject(CgiPlace cgiPlace)
        {
            this.ParameArr = new KeyValuePair<string, string>[3];
            KeyValuePair<string, string> kv1;
            switch (cgiPlace)
            {
                case CgiPlace.Head:
                    kv1 = new KeyValuePair<string, string>("name", "_head.inc");
                    this.ParameArr[0] = kv1;
                    break;

                case CgiPlace.Content:
                    kv1 = new KeyValuePair<string, string>("name", "_content.inc");
                    this.ParameArr[0] = kv1;
                    break;

                case CgiPlace.Keyword:
                    kv1 = new KeyValuePair<string, string>("name", "_keyword.inc");
                    this.ParameArr[0] = kv1;
                    break;

                //case CgiPlace.JeeluFooter:
                //    kv1 = new KeyValuePair<string, string>("name", "_keyword.inc");
                //    this.ParameArr[0] = kv1;
                //    break;

                case CgiPlace.None:
                default:
                    break;
            }
            this.ParameArr[1] = new KeyValuePair<string, string>("dir", "$DOCUMENT_URI");
            this.ParameArr[2] = new KeyValuePair<string, string>("domain", "$SERVER_ADDR");
            this.CGIName = "GetHtml.cgi";
            this.CGIPath = "http://ssi.jeelu.com/py/";
            this.JoinChar = "?";
        }
    }

    internal enum CgiPlace
    {
        Head, Content, Keyword, None,
    }
}
