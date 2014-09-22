using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusPagePreviewer
{

    class HttpHeadInfo
    {
        private string date;

        public string Date
        {
            get { return GetHttpDate(); }
            set { date = value; }
        }
        private string server = "Jeelu.SimplusD.WebView/1.0.1 (Windows)\r\n";

        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        private string accept_Ranges = " bytes\r\n";

        public string Accept_Ranges
        {
            get { return accept_Ranges; }
            set { accept_Ranges = value; }
        }
        private string content_Length;
        /// <summary>
        /// 必输内容 消息长度
        /// </summary>
        public string Content_Length
        {
            get { return content_Length; }
            set { content_Length = value; }
        }
        private string keep_Alive = " timeout=15, max=1000\r\n";

        public string Keep_Alive
        {
            get { return keep_Alive; }
            set { keep_Alive = value; }
        }
        private string connection = " Keep-Alive\r\n";

        public string Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        private string content_Type = " text/html\r\n";
        /// <summary>
        ///必输内容 消息类型
        /// </summary>
        public string Content_Type
        {
            get { return content_Type; }
            set { content_Type = value; }
        }

     
        public string GetHttpHead()
        {
            return "Date:" + GetHttpDate() + "Server:" + this.server + "Accept-Ranges:" + this.accept_Ranges + "Content-Length: " + this.content_Length + "\r\n" + "Keep-Alive:" + this.keep_Alive + "Connection:" + this.connection + "Content-Type: " + this.content_Type + "\r\n\r\n";
        }
        public string GetHttpDate()
        {
            //X-Powered-By: ASP.NET\r\n
            string date_Week = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
            string date_Date = DateTime.Now.Day.ToString().PadLeft(2, '0');
            string date_Month = "";
            switch (DateTime.Now.Month)
            {
                case 1:
                    date_Month = "JAN";
                    break;
                case 2:
                    date_Month = "FEB";
                    break;
                case 3:
                    date_Month = "MAR";
                    break;
                case 4:
                    date_Month = "APR";
                    break;
                case 5:
                    date_Month = "MAY";
                    break;
                case 6:
                    date_Month = "JUN";
                    break;
                case 7:
                    date_Month = "JUL";
                    break;
                case 8:
                    date_Month = "AUG";
                    break;
                case 9:
                    date_Month = "SEP";
                    break;
                case 10:
                    date_Month = "OCT";
                    break;
                case 11:
                    date_Month = "NOV";
                    break;
                case 12:
                    date_Month = "DEC";
                    break;
            }
            string date_Year = DateTime.Now.Year.ToString();
            string date_Time = "00:" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            this.date = date_Week + "," + " " + date_Date + " " + date_Month + " " + date_Year + " " + date_Time + " " + "GMT" + "\r\n";
            return this.date;
        }

    }
}
