using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusPagePreviewer
{
    /// <summary>
    /// 不同类型的结构体
    /// </summary>
    public struct HttpType
    {
        ///// <summary>
        ///// 资源扩展名
        ///// </summary>
        //public string AppType;
        /// <summary>
        /// MIME类型
        /// </summary>
        public string MimeType;
    }

    /// <summary>
    /// 具体每种类型的名称及MIME类型
    /// </summary>
    static public class HttpRequestType
    {
        static HttpType anyType;

        /// <summary>
        /// xls类型
        /// </summary>
        public static HttpType Xls
        {
            get
            {
                anyType.MimeType = "application/msexcel";
                return anyType;
            }
        }

        /// <summary>
        /// xla类型
        /// </summary>
        public static HttpType Xla
        {
            get
            {
                anyType.MimeType = "application/msexcel";
                return anyType;
            }
        }

        /// <summary>
        /// hlp类型
        /// </summary>
        public static HttpType Hlp
        {
            get
            {
                anyType.MimeType = "application/mshelp";
                return anyType;
            }
        }

        /// <summary>
        /// chm类型
        /// </summary>
        public static HttpType Chm
        {
            get
            {
                anyType.MimeType = "application/mshelp";
                return anyType;
            }
        }

        /// <summary>
        /// ppt类型
        /// </summary>
        public static HttpType Ppt
        {
            get
            {
                anyType.MimeType = "application/mspowerpoint";
                return anyType;
            }
        }

        /// <summary>
        /// ppz类型
        /// </summary>
        public static HttpType Ppz
        {
            get
            {
                anyType.MimeType = "application/mspowerpoint";
                return anyType;
            }
        }

        /// <summary>
        /// pps类型
        /// </summary>
        public static HttpType Pps
        {
            get
            {
                anyType.MimeType = "application/mspowerpoint";
                return anyType;
            }
        }

        /// <summary>
        /// pot类型
        /// </summary>
        public static HttpType Pot
        {
            get
            {
                anyType.MimeType = "application/mspowerpoint";
                return anyType;
            }
        }

        /// <summary>
        /// doc类型
        /// </summary>
        public static HttpType Doc
        {
            get
            {
                anyType.MimeType = "application/msword";
                return anyType;
            }
        }

        /// <summary>
        /// dot类型
        /// </summary>
        public static HttpType Dot
        {
            get
            {
                anyType.MimeType = "application/msword";
                return anyType;
            }
        }

         /// <summary>
        /// pdf类型
        /// </summary>
        public static HttpType Pdf
        {
            get
            {
                anyType.MimeType = "application/pdf";
                return anyType;
            }
        }

        /// <summary>
        /// ai类型
        /// </summary>
        public static HttpType Ai
        {
            get
            {
                anyType.MimeType = "application/postscript";
                return anyType;
            }
        }

        /// <summary>
        /// eps类型
        /// </summary>
        public static HttpType Eps
        {
            get
            {
                anyType.MimeType = "application/postscript";
                return anyType;
            }
        }

        /// <summary>
        /// ps类型
        /// </summary>
        public static HttpType Ps
        {
            get
            {
                anyType.MimeType = "application/postscript";
                return anyType;
            }
        }

        /// <summary>
        /// rtf类型
        /// </summary>
        public static HttpType Rtf
        {
            get
            {
                anyType.MimeType = "application/rtf";
                return anyType;
            }
        }

        /// <summary>
        /// swf类型
        /// </summary>
        public static HttpType Swf
        {
            get
            {
                anyType.MimeType = "application/x-shockwave-flash";
                return anyType;
            }
        }
        
        /// <summary>
        /// cab类型
        /// </summary>
        public static HttpType Cab
        {
            get
            {
                anyType.MimeType = "application/x-shockwave-flash";
                return anyType;
            }
        }
        
        /// <summary>
        /// zip类型
        /// </summary>
        public static HttpType Zip
        {
            get
            {
                anyType.MimeType = "application/x-zip";
                return anyType;
            }
        }
        
        /// <summary>
        ///mid类型
        /// </summary>
        public static HttpType Mid
        {
            get
            {
                anyType.MimeType = "audio/x-midi";
                return anyType;
            }
        }
        
        /// <summary>
        /// midi类型
        /// </summary>
        public static HttpType Midi
        {
            get
            {
                anyType.MimeType = "audio/x-midi";
                return anyType;
            }
        }

        /// <summary>
        /// wav类型
        /// </summary>
        public static HttpType Wav
        {
            get
            {
                anyType.MimeType = "audio/x-wav";
                return anyType;
            }
        }

        /// <summary>
        /// gif类型
        /// </summary>
        public static HttpType Gif
        {
            get
            {
                anyType.MimeType = "image/gif";
                return anyType;
            }
        }
        
        /// <summary>
        /// jpg类型
        /// </summary>
        public static HttpType Jpg
        {
            get
            {
                anyType.MimeType = "image/jpeg";
                return anyType;
            }
        }

        /// <summary>
        /// jpe类型
        /// </summary>
        public static HttpType Jpe
        {
            get
            {
                anyType.MimeType = "image/jpeg";
                return anyType;
            }
        }

        /// <summary>
        /// jpeg类型
        /// </summary>
        public static HttpType Jpeg
        {
            get
            {
                anyType.MimeType = "image/jpeg";
                return anyType;
            }
        }

        /// <summary>
        /// css类型
        /// </summary>
        public static HttpType Css
        {
            get
            {
                anyType.MimeType = "text/css";
                return anyType;
            }
        }

         /// <summary>
        /// html类型
        /// </summary>
        public static HttpType Html
        {
            get
            {
                anyType.MimeType = "text/html";
                return anyType;
            }
        }

         /// <summary>
        /// htm类型
        /// </summary>
        public static HttpType Htm
        {
            get
            {
                anyType.MimeType = "text/html";
                return anyType;
            }
        }

        /// <summary>
        /// js类型
        /// </summary>
        public static HttpType Js
        {
            get
            {
                anyType.MimeType = "text/javascript";
                return anyType;
            }
        }

        /// <summary>
        /// txt类型
        /// </summary>
        public static HttpType Txt
        {
            get
            {
                anyType.MimeType = "text/plain";
                return anyType;
            }
        }

        /// <summary>
        ///mpeg类型
        /// </summary>
        public static HttpType Mpeg
        {
            get
            {
                anyType.MimeType = "video/mpeg";
                return anyType;
            }
        }

        
        /// <summary>
        ///mpg类型
        /// </summary>
        public static HttpType Mpg
        {
            get
            {
                anyType.MimeType = "video/mpeg";
                return anyType;
            }
        }

         /// <summary>
        ///mpe类型
        /// </summary>
        public static HttpType Mpe
        {
            get
            {
                anyType.MimeType = "video/mpeg";
                return anyType;
            }
        }

        /// <summary>
        ///qt类型
        /// </summary>
        public static HttpType Qt
        {
            get
            {
                anyType.MimeType = "video/quicktime";
                return anyType;
            }
        }
 
        /// <summary>
        ///mov类型
        /// </summary>
        public static HttpType Mov
        {
            get
            {
                anyType.MimeType = "video/quicktime";
                return anyType;
            }
        }
        
        /// <summary>
        ///exe类型
        /// </summary>
        public static HttpType Exe
        {
            get
            {
                anyType.MimeType = "application/octet-stream";
                return anyType;
            }
        }

         /// <summary>
        ///avi类型
        /// </summary>
        public static HttpType Avi
        {
            get
            {
                anyType.MimeType = "application/octet-stream";
                return anyType;
            }
        }

         /// <summary>
        ///xml类型
        /// </summary>
        public static HttpType Xml
        {
            get
            {
                anyType.MimeType = "text/xml";
                return anyType;
            }
        }

         /// <summary>
        ///wma类型
        /// </summary>
        public static HttpType Wma
        {
            get
            {
                anyType.MimeType = "application/octet-stream";
                return anyType;
            }
        }

        /// <summary>
        ///rm类型
        /// </summary>
        public static HttpType Rm
        {
            get
            {
                anyType.MimeType = "application/octet-stream";
                return anyType;
            }
        }

        public static HttpType Wmv
        {
            get
            {
                anyType.MimeType = "application/octet-stream";
                return anyType;
            }
        }

         /// <summary>
        ///mp3类型
        /// </summary>
        public static HttpType Mp3
        {
            get
            {
                anyType.MimeType = "audio/x-mpeg";
                return anyType;
            }
        }

         /// <summary>
        ///rmvb类型
        /// </summary>
        public static HttpType Rmvb
        {
            get
            {
                anyType.MimeType = "application/octet-stream";
                return anyType;
            }
        }

        /// <summary>
        ///au类型
        /// </summary>
        public static HttpType Au
        {
            get
            {
                anyType.MimeType = "audio/basic";
                return anyType;
            }
        }

        /// <summary>
        ///snd类型
        /// </summary>
        public static HttpType Snd
        {
            get
            {
                anyType.MimeType = "audio/basic";
                return anyType;
            }
        }

        /// <summary>
        ///mp2类型
        /// </summary>
        public static HttpType Mp2
        {
            get
            {
                anyType.MimeType = "audio/x-mpeg";
                return anyType;
            }
        }

        /// <summary>
        ///dwg类型
        /// </summary>
        public static HttpType Dwg
        {
            get
            {
                anyType.MimeType = "application/acad";
                return anyType;
            }
        }

        /// <summary>
        ///ccad类型
        /// </summary>
        public static HttpType Ccad
        {
            get
            {
                anyType.MimeType = "application/clariscad";
                return anyType;
            }
        }

        /// <summary>
        ///Viv类型
        /// </summary>
        public static HttpType Viv
        {
            get
            {
                anyType.MimeType = "video/vnd.vivo";
                return anyType;
            }
        }


        /// <summary>
        ///Vivo类型
        /// </summary>
        public static HttpType Vivo
        {
            get
            {
                anyType.MimeType = "video/vnd.vivo";
                return anyType;
            }
        }

    }




}
