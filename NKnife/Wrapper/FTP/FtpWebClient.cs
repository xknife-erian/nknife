using System;
using System.Net;

namespace NKnife.Wrapper.FTP
{
    /// <summary>����WebClient��֧��FTP����
    /// </summary>
    public class FtpWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            var req = (FtpWebRequest)base.GetWebRequest(address);
            if (req != null)
                req.UsePassive = false;
            return req;
        }
    }
}