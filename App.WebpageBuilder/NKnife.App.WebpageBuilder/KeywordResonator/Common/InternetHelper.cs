using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Threading;
using System.Net;

namespace Jeelu.KeywordResonator
{
    public static class InternetHelper
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        private static int ThreadMax = 30;
        private static int _threadI;
        /// <summary>
        /// 抓取最大尝试数
        /// </summary>
        private static int TimeoutMaxCount = 100;
        private static List<string> pageCodeList = new List<string>();

        public static void GetWebPage(string urlListFile)
        {
            XmlReader reader = XmlReader.Create(urlListFile);
            List<string> urls = new List<string>();
            while (reader.Read())///用XmlReader方法读取Url配置中所有的Url到List中
            {
                if (reader.IsStartElement())
                {
                    if (!reader.IsEmptyElement && reader.Name == "url")
                    {
                        urls.Add(reader.ReadString());
                    }
                }
            }
            reader.Close();

            foreach (string url in urls)
            {
                ///多线程抓取网页
                while (_threadI >= ThreadMax)
                {
                    Thread.Sleep(500 * 2);
                }

                Thread thread = new Thread(new ParameterizedThreadStart(DoReceived));
                thread.Start(url);
            }
        }
        private static void DoReceived(object state)
        {
            Interlocked.Add(ref _threadI, 1);
            string url = state.ToString();
            WebRequest request = null;
            for (int i = 0; i < TimeoutMaxCount; i++)
            {
                try
                {
                    request = WebRequest.Create(url);
                    break;
                }
                catch
                {
                    Thread.Sleep(500 * 2);
                }
            }
            if (request == null)
            {
                return;
            }
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.Default);
            lock (pageCodeList)
            {
                string allText = reader.ReadToEnd();
                pageCodeList.Add(allText);
            }
            stream.Close();
            reader.Close();
            Interlocked.Add(ref _threadI, -1);
        }

    }
}
