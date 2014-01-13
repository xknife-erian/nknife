using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;

namespace Gean.Net.Webs
{
    public class WebRequestBeginGetRequestStream
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public WebRequestBeginGetRequestStream()
        {
            WebRequest myWebRequest = WebRequest.Create("http://www.contoso.com ");

            var myRequestState = new RequestState();
            myRequestState.request = myWebRequest;
            myWebRequest.ContentType = "application/x-www-form-urlencoded ";
            myRequestState.request.Method = "POST ";
            myWebRequest.BeginGetRequestStream(ReadCallback, myRequestState);

            allDone.WaitOne();
            WebResponse myWebResponse = myWebRequest.GetResponse();

            if (myWebResponse != null)
            {
                Stream streamResponse = myWebResponse.GetResponseStream();
                if (streamResponse != null)
                {
                    var streamRead = new StreamReader(streamResponse);
                    var readBuff = new Char[256];
                    int count = streamRead.Read(readBuff, 0, 256);

                    while (count > 0)
                    {
                        var outputData = new String(readBuff, 0, count);
                        Console.Write(outputData);
                        count = streamRead.Read(readBuff, 0, 256);
                    }

                    streamResponse.Close();
                    streamRead.Close();
                }
            }
            if (myWebResponse != null) 
                myWebResponse.Close();
        }

        private static void ReadCallback(IAsyncResult asynchronousResult)
        {
            var myRequestState = (RequestState)asynchronousResult.AsyncState;
            WebRequest myWebRequest = myRequestState.request;

            Stream streamResponse = myWebRequest.EndGetRequestStream(asynchronousResult);

            Console.WriteLine("Please   enter   a   string   to   be   posted: ");
            string postData = Console.ReadLine();
            //   Convert   the   string   into   a   byte   array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //   Write   the   data   to   the   stream.
            streamResponse.Write(byteArray, 0, postData.Length);
            streamResponse.Close();
            allDone.Set();
        }
    }
}