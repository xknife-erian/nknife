using System;
using System.Net;
using System.Text;
using Gean.Resources;

namespace Gean.Net.Webs
{
    public class WebClientEx : System.Net.WebClient
    {
        private static readonly string[] _MultiUserAgents = GeneralString.HttpUserAgents.Split('~');
        private static readonly Random _Random = new Random();

        public WebClientEx()
        {
            Timeout = 800;
            UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727)";
            CookieContainer = new CookieContainer();
        }

        public CookieContainer CookieContainer { get; set; }

        public string UserAgent { get; set; }

        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            RefreshUserAgent();

            if (request != null)
            {
                if (request.GetType() == typeof (HttpWebRequest))
                {
                    ((HttpWebRequest) request).CookieContainer = CookieContainer;
                    ((HttpWebRequest) request).UserAgent = UserAgent;
                    (request).Timeout = Timeout;
                }
            }
            return request;
        }

        private void RefreshUserAgent()
        {
            UserAgent = _MultiUserAgents[_Random.Next(0, _MultiUserAgents.Length)];
        }
    }
}