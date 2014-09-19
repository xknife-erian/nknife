using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Jeelu;
using Jeelu.SimplusD;
using System.Windows.Forms;

namespace Jeelu.SimplusPagePreviewer
{
    internal class Perviewer
    {
        public string Index { get; private set; }

        public static string Port { get; private set; }

        public string ProjectName
        {
            get { return PathService.ProjectName; }
        }
        /// <summary>
        /// 启动预览
        /// </summary>
        public void Run()
        {
            TcpListener listener = ClientService.CreatTcpListener();
            Port = ClientService.Port;
            Jeelu.SimplusD.PathService.Initialize(Application.StartupPath);
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream ns = client.GetStream();
                StreamReader srs = new StreamReader(ns);
                string received = "";//接收的GET请求
                received += srs.ReadLine();//获得GET请求
                this.Index = StringService.GetRequest(received);//获取请求字符川
                string resquestType = StringService.GetRequestType(this.Index);//获取请求类型
                //如果发送php和shtml请求，说明sdsite被改编重新打开sdsite实例，则重新打开sdsite
                if (resquestType == "php" || resquestType == "shtml" || string.IsNullOrEmpty(resquestType))
                {
                    ///SdsiteService.OpenSdsite(PathService.AbsolutePath);
                    SiteResourceService.Initialize
                        (null,
                        GetResourceAbsolutePath.GetResourceAbsPath,
                        GetResourceAbsolutePath.GetResourcePath,
                        GetResourceAbsolutePath.GetResourceUrl,
                        null);
                }
                //处理除不同类型请求
                HttpType(this.Index, resquestType, ns);
            }
        }

        /// <summary>
        /// 处理各种类型请求
        /// </summary>
        private void HttpType(string index, string resquestType, NetworkStream ns)
        {
            switch (resquestType)
            {
                case "xls":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Xls.MimeType, ns);
                    break;
                case "xla":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Xla.MimeType, ns);
                    break;
                case "hlp":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Hlp.MimeType, ns);
                    break;
                case "chm":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Chm.MimeType, ns);
                    break;
                case "ppt":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Ppt.MimeType, ns);
                    break;
                case "ppz":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Ppz.MimeType, ns);
                    break;
                case "pps":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Pps.MimeType, ns);
                    break;
                case "pot":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Pot.MimeType, ns);
                    break;
                case "doc":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Doc.MimeType, ns);
                    break;
                case "dot":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Dot.MimeType, ns);
                    break;
                case "pdf":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Pdf.MimeType, ns);
                    break;
                case "ai":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Ai.MimeType, ns);
                    break;
                case "eps":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Eps.MimeType, ns);
                    break;
                case "ps":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Ps.MimeType, ns);
                    break;
                case "rtf":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Rtf.MimeType, ns);
                    break;
                case "swf":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Swf.MimeType, ns);
                    break;
                case "cab":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Cab.MimeType, ns);
                    break;
                case "zip":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Zip.MimeType, ns);
                    break;
                case "au":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Au.MimeType, ns);
                    break;
                case "snd":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Snd.MimeType, ns);
                    break;
                case "mid":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Mid.MimeType, ns);
                    break;
                case "midi":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Midi.MimeType, ns);
                    break;
                case "mp2":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Mp2.MimeType, ns);
                    break;
                case "wav":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Wav.MimeType, ns);
                    break;
                case "gif":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Gif.MimeType, ns);
                    break;
                case "jpg":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Jpg.MimeType, ns);
                    break;
                case "jpe":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Jpe.MimeType, ns);
                    break;
                case "jpeg":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Jpeg.MimeType, ns);
                    break;
                case "css":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Css.MimeType, ns);
                    break;
                case "shtml":
                    DisposeAnyTypeRequest.AnyShtmlHeadBody(index, ns);
                    break;
                case "php":
                    DisposeAnyTypeRequest.PhpHeadBody(index, ns);
                    break;
                case "html":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Html.MimeType, ns);
                    break;
                case "htm":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Htm.MimeType, ns);
                    break;
                case "js":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Js.MimeType, ns);
                    break;
                case "txt":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Txt.MimeType, ns);
                    break;
                case "dwg":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Dwg.MimeType, ns);
                    break;
                case "ccad":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Ccad.MimeType, ns);
                    break;
                case "mpeg":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Mpeg.MimeType, ns);
                    break;
                case "mpg":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Mpg.MimeType, ns);
                    break;
                case "mpe":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Mpe.MimeType, ns);
                    break;
                case "qt":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Qt.MimeType, ns);
                    break;
                case "mov":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Mov.MimeType, ns);
                    break;
                case "viv":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Viv.MimeType, ns);
                    break;
                case "vivo":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Vivo.MimeType, ns);
                    break;
                case "exe":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Exe.MimeType, ns);
                    break;
                case "avi":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Avi.MimeType, ns);
                    break;
                case "xml":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Xml.MimeType, ns);
                    break;
                case "wma":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Wma.MimeType, ns);
                    break;
                case "rm":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Rm.MimeType, ns);
                    break;
                case "mp3":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Mp3.MimeType, ns);
                    break;
                case "rmvb":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Rmvb.MimeType, ns);
                    break;
                case "wmv":
                    DisposeAnyTypeRequest.OtherHeadBody(index, HttpRequestType.Wmv.MimeType, ns);
                    break;
                default:
                    if (string.IsNullOrEmpty(resquestType))
                    {
                        ///频道
                        string newIndex = null;
                        if (string.IsNullOrEmpty(index))
                        {
                            newIndex = "index.shtml";
                        }
                        else
                        {
                            newIndex = index.EndsWith(@"/") ? index + "index.shtml" : index + "/index.shtml";
                        }
                        DisposeAnyTypeRequest.AnyShtmlHeadBody(newIndex, ns);
                        //DisposeAnyTypeRequest.ChannelHeadBody(index, ns);
                    }
                    break;
            }
        }
    }
}