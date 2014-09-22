using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Diagnostics;

namespace Jeelu
{
    public partial class XhtmlSection : XhtmlElement
    {
        public XhtmlTagElement CreateXhtmlTag(string localname, params XhtmlAttribute[] attributes)
        {
            XhtmlTagElement xhtmla = new XhtmlTagElement();
            xhtmla.InitializeComponent(this, localname, attributes);
            return xhtmla;
        }

        /// <summary>
        /// 创建一个Text节点
        /// </summary>
        /// <param name="text">Text节点的文本</param>
        public XhtmlText CreateXhtmlText(string text)
        {
            XhtmlText xhtmlText = new XhtmlText(this, text);
            return xhtmlText;
        }
        /// <summary>
        /// 创建一个注释节点
        /// </summary>
        /// <param name="text">注释文本</param>
        public XhtmlComment CreateXhtmlComment(string text)
        {
            XhtmlComment xhtmlComment = new XhtmlComment(this, text);
            return xhtmlComment;
        }
        /// <summary>
        /// 创建一个XhtmlCData节点
        /// </summary>
        /// <param name="text">XhtmlCData文本</param>
        public XhtmlCData CreateXhtmlCData(string cdata)
        {
            XhtmlCData xhtmlComment = new XhtmlCData(this, cdata);
            return xhtmlComment;
        }
        /// <summary>
        /// 创建内部类ShtmlInclude（带参数,一般是指包含一个模板的PHP页面）：它是一个Comment节点，具体是Shtml的包含语句。
        /// 类似[!--#include virtual="/templates/073df79febbe4f6aa0087f4574289a5c.shtml"--]语句
        /// </summary>
        /// <param name="path">路径，前后都应有"/"符号</param>
        /// <param name="file">文件</param>
        public XhtmlComment.ShtmlInclude CreateXhtmlCommentShtml(CGIObject cgi)
        {
            XhtmlComment.ShtmlInclude shtmlInclude = new XhtmlComment.ShtmlInclude(this, cgi.ToString());
            return shtmlInclude;
        }
        /// <summary>
        /// 创建内部类ShtmlInclude（带参数,一般是指包含一个模板的PHP页面）：它是一个Comment节点，具体是Shtml的包含语句。
        /// 类似[!--#include virtual="/templates/073df79febbe4f6aa0087f4574289a5c.shtml"--]语句
        /// </summary>
        /// <param name="path">路径，前后都应有"/"符号</param>
        /// <param name="file">文件</param>
        public XhtmlComment.ShtmlInclude CreateXhtmlCommentShtml(string path, string file)
        {
            XhtmlComment.ShtmlInclude shtmlInclude = new XhtmlComment.ShtmlInclude(this, path, file);
            return shtmlInclude;
        }
        /// <summary>
        /// 创建内部类ShtmlInclude（带参数,一般是指包含一个模板的PHP页面）：它是一个Comment节点，具体是Shtml的包含语句。
        /// 类似[!--#include virtual="/templates/073df79febbe4f6aa0087f4574289a5c.shtml"--]语句
        /// </summary>
        /// <param name="fullFile">文件全名，含相对路径的全名</param>
        public XhtmlComment.ShtmlInclude CreateXhtmlCommentShtml(string fullFile)
        {
            XhtmlComment.ShtmlInclude shtmlInclude = new XhtmlComment.ShtmlInclude(this, fullFile);
            return shtmlInclude;
        }

        /* 以下代码是自动生成器生成 */
        public XhtmlTags.A CreateXhtmlA(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.A xhtmla = new XhtmlTags.A();
            xhtmla.InitializeComponent(this, "a", attributes);
            return xhtmla;
        }
        public XhtmlTags.B CreateXhtmlB(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.B xhtmlb = new XhtmlTags.B();
            xhtmlb.InitializeComponent(this, "b", attributes);
            return xhtmlb;
        }
        public XhtmlTags.Base CreateXhtmlBase(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Base xhtmlbase = new XhtmlTags.Base();
            xhtmlbase.InitializeComponent(this, "base", attributes);
            return xhtmlbase;
        }
        public XhtmlTags.Body CreateXhtmlBody(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Body xhtmlbody = new XhtmlTags.Body();
            xhtmlbody.InitializeComponent(this, "body", attributes);
            return xhtmlbody;
        }
        public XhtmlTags.Br CreateXhtmlBr(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Br xhtmlbr = new XhtmlTags.Br();
            xhtmlbr.InitializeComponent(this, "br", attributes);
            return xhtmlbr;
        }
        public XhtmlTags.Button CreateXhtmlButton(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Button xhtmlbutton = new XhtmlTags.Button();
            xhtmlbutton.InitializeComponent(this, "button", attributes);
            return xhtmlbutton;
        }
        public XhtmlTags.Dd CreateXhtmlDd(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Dd xhtmldd = new XhtmlTags.Dd();
            xhtmldd.InitializeComponent(this, "dd", attributes);
            return xhtmldd;
        }
        public XhtmlTags.Div CreateXhtmlDiv(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Div xhtmldiv = new XhtmlTags.Div();
            xhtmldiv.InitializeComponent(this, "div", attributes);
            return xhtmldiv;
        }
        public XhtmlTags.Dl CreateXhtmlDl(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Dl xhtmldl = new XhtmlTags.Dl();
            xhtmldl.InitializeComponent(this, "dl", attributes);
            return xhtmldl;
        }
        public XhtmlTags.Dt CreateXhtmlDt(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Dt xhtmldt = new XhtmlTags.Dt();
            xhtmldt.InitializeComponent(this, "dt", attributes);
            return xhtmldt;
        }
        public XhtmlTags.Font CreateXhtmlFont(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Font xhtmlfont = new XhtmlTags.Font();
            xhtmlfont.InitializeComponent(this, "font", attributes);
            return xhtmlfont;
        }
        public XhtmlTags.H1 CreateXhtmlH1(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.H1 xhtmlh1 = new XhtmlTags.H1();
            xhtmlh1.InitializeComponent(this, "h1", attributes);
            return xhtmlh1;
        }
        public XhtmlTags.H2 CreateXhtmlH2(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.H2 xhtmlh2 = new XhtmlTags.H2();
            xhtmlh2.InitializeComponent(this, "h2", attributes);
            return xhtmlh2;
        }
        public XhtmlTags.H3 CreateXhtmlH3(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.H3 xhtmlh3 = new XhtmlTags.H3();
            xhtmlh3.InitializeComponent(this, "h3", attributes);
            return xhtmlh3;
        }
        public XhtmlTags.H4 CreateXhtmlH4(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.H4 xhtmlh4 = new XhtmlTags.H4();
            xhtmlh4.InitializeComponent(this, "h4", attributes);
            return xhtmlh4;
        }
        public XhtmlTags.H5 CreateXhtmlH5(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.H5 xhtmlh5 = new XhtmlTags.H5();
            xhtmlh5.InitializeComponent(this, "h5", attributes);
            return xhtmlh5;
        }
        public XhtmlTags.H6 CreateXhtmlH6(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.H6 xhtmlh6 = new XhtmlTags.H6();
            xhtmlh6.InitializeComponent(this, "h6", attributes);
            return xhtmlh6;
        }
        public XhtmlTags.Head CreateXhtmlHead(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Head xhtmlhead = new XhtmlTags.Head();
            xhtmlhead.InitializeComponent(this, "head", attributes);
            return xhtmlhead;
        }
        public XhtmlTags.Hr CreateXhtmlHr(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Hr xhtmlhr = new XhtmlTags.Hr();
            xhtmlhr.InitializeComponent(this, "hr", attributes);
            return xhtmlhr;
        }
        public XhtmlTags.Html CreateXhtmlHtml(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Html xhtmlhtml = new XhtmlTags.Html();
            xhtmlhtml.InitializeComponent(this, "html", attributes);
            return xhtmlhtml;
        }
        public XhtmlTags.Iframe CreateXhtmlIframe(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Iframe xhtmliframe = new XhtmlTags.Iframe();
            xhtmliframe.InitializeComponent(this, "iframe", attributes);
            return xhtmliframe;
        }
        public XhtmlTags.Img CreateXhtmlImg(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Img xhtmlimg = new XhtmlTags.Img();
            xhtmlimg.InitializeComponent(this, "img", attributes);
            return xhtmlimg;
        }
        public XhtmlTags.Input CreateXhtmlInput(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Input xhtmlinput = new XhtmlTags.Input();
            xhtmlinput.InitializeComponent(this, "input", attributes);
            return xhtmlinput;
        }
        public XhtmlTags.Li CreateXhtmlLi(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Li xhtmlli = new XhtmlTags.Li();
            xhtmlli.InitializeComponent(this, "li", attributes);
            return xhtmlli;
        }
        public XhtmlTags.Link CreateXhtmlLink(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Link xhtmllink = new XhtmlTags.Link();
            xhtmllink.InitializeComponent(this, "link", attributes);
            return xhtmllink;
        }
        public XhtmlTags.Meta CreateXhtmlMeta(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Meta xhtmlmeta = new XhtmlTags.Meta();
            xhtmlmeta.InitializeComponent(this, "meta", attributes);
            return xhtmlmeta;
        }
        public XhtmlTags.Noscript CreateXhtmlNoscript(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Noscript xhtmlnoscript = new XhtmlTags.Noscript();
            xhtmlnoscript.InitializeComponent(this, "noscript", attributes);
            return xhtmlnoscript;
        }
        public XhtmlTags.Object CreateXhtmlObject(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Object xhtmlobject = new XhtmlTags.Object();
            xhtmlobject.InitializeComponent(this, "object", attributes);
            return xhtmlobject;
        }
        public XhtmlTags.Ol CreateXhtmlOl(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Ol xhtmlol = new XhtmlTags.Ol();
            xhtmlol.InitializeComponent(this, "ol", attributes);
            return xhtmlol;
        }
        public XhtmlTags.Option CreateXhtmlOption(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Option xhtmloption = new XhtmlTags.Option();
            xhtmloption.InitializeComponent(this, "option", attributes);
            return xhtmloption;
        }
        public XhtmlTags.P CreateXhtmlP(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.P xhtmlp = new XhtmlTags.P();
            xhtmlp.InitializeComponent(this, "p", attributes);
            return xhtmlp;
        }
        public XhtmlTags.Script CreateXhtmlScript(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Script xhtmlscript = new XhtmlTags.Script();
            xhtmlscript.InitializeComponent(this, "script", attributes);
            return xhtmlscript;
        }
        public XhtmlTags.Select CreateXhtmlSelect(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Select xhtmlselect = new XhtmlTags.Select();
            xhtmlselect.InitializeComponent(this, "select", attributes);
            return xhtmlselect;
        }
        public XhtmlTags.Span CreateXhtmlSpan(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Span xhtmlspan = new XhtmlTags.Span();
            xhtmlspan.InitializeComponent(this, "span", attributes);
            return xhtmlspan;
        }
        public XhtmlTags.Style CreateXhtmlStyle(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Style xhtmlstyle = new XhtmlTags.Style();
            xhtmlstyle.InitializeComponent(this, "style", attributes);
            return xhtmlstyle;
        }
        public XhtmlTags.Table CreateXhtmlTable(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Table xhtmltable = new XhtmlTags.Table();
            xhtmltable.InitializeComponent(this, "table", attributes);
            return xhtmltable;
        }
        public XhtmlTags.Tbody CreateXhtmlTbody(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Tbody xhtmltbody = new XhtmlTags.Tbody();
            xhtmltbody.InitializeComponent(this, "tbody", attributes);
            return xhtmltbody;
        }
        public XhtmlTags.Td CreateXhtmlTd(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Td xhtmltd = new XhtmlTags.Td();
            xhtmltd.InitializeComponent(this, "td", attributes);
            return xhtmltd;
        }
        public XhtmlTags.Tfoot CreateXhtmlTfoot(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Tfoot xhtmltfoot = new XhtmlTags.Tfoot();
            xhtmltfoot.InitializeComponent(this, "tfoot", attributes);
            return xhtmltfoot;
        }
        public XhtmlTags.Th CreateXhtmlTh(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Th xhtmlth = new XhtmlTags.Th();
            xhtmlth.InitializeComponent(this, "th", attributes);
            return xhtmlth;
        }
        public XhtmlTags.Title CreateXhtmlTitle(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Title xhtmltitle = new XhtmlTags.Title();
            xhtmltitle.InitializeComponent(this, "title", attributes);
            return xhtmltitle;
        }
        public XhtmlTags.Tr CreateXhtmlTr(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Tr xhtmltr = new XhtmlTags.Tr();
            xhtmltr.InitializeComponent(this, "tr", attributes);
            return xhtmltr;
        }
        public XhtmlTags.Tt CreateXhtmlTt(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Tt xhtmltt = new XhtmlTags.Tt();
            xhtmltt.InitializeComponent(this, "tt", attributes);
            return xhtmltt;
        }
        public XhtmlTags.Ul CreateXhtmlUl(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Ul xhtmlul = new XhtmlTags.Ul();
            xhtmlul.InitializeComponent(this, "ul", attributes);
            return xhtmlul;
        }

        public XhtmlTags.Flash CreateXhtmlFlash(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.Flash xhtmlFlash = new XhtmlTags.Flash();
            xhtmlFlash.InitializeComponent(this, "object", attributes);
            return xhtmlFlash;
        }

        public XhtmlTags.MP3 CreateXhtmlMP3(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.MP3 xhtmlMP3 = new XhtmlTags.MP3();
            xhtmlMP3.InitializeComponent(this, "object", attributes);
            return xhtmlMP3;
        }

        public XhtmlTags.WMA CreateXhtmlWMA(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.WMA xhtmlWMA = new XhtmlTags.WMA();
            xhtmlWMA.InitializeComponent(this, "object", attributes);
            return xhtmlWMA;
        }

        public XhtmlTags.AVI CreateXhtmlAVI(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.AVI xhtmlAVI = new XhtmlTags.AVI();
            xhtmlAVI.InitializeComponent(this, "object", attributes);
            return xhtmlAVI;
        }

        public XhtmlTags.WMV CreateXhtmlWMV(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.WMV xhtmlWMV = new XhtmlTags.WMV();
            xhtmlWMV.InitializeComponent(this, "object", attributes);
            return xhtmlWMV;
        }

        public XhtmlTags.RMVB_RM CreateXhtmlRMVB_RM(params XhtmlAttribute[] attributes)
        {
            XhtmlTags.RMVB_RM xhtmlRMVB_RM = new XhtmlTags.RMVB_RM();
            xhtmlRMVB_RM.InitializeComponent(this, "object", attributes);
            return xhtmlRMVB_RM;
        }

        public class CGIObject
        {
            public CGIObject() { }
            public CGIObject(string cgiName, string cgiPath, string joinChar, params KeyValuePair<string, string>[] paramArr)
            {
                this.CGIName = cgiName;
                this.CGIPath = cgiPath;
                this.ParameArr = paramArr;
                this.JoinChar = joinChar;
            }

            public string CGIPath { get; set; }
            public string CGIName { get; set; }
            public string JoinChar { get; set; }
            public KeyValuePair<string, string>[] ParameArr { get; set; }

            public override bool Equals(object obj)
            {
                CGIObject co = (CGIObject)obj;
                if (this.CGIName.Equals(co.CGIName))
                {
                    return false;
                }
                if (this.CGIPath.Equals(co.CGIPath))
                {
                    return false;
                }
                if (this.ParameArr.Equals(co.ParameArr))
                {
                    return false;
                }
                return true;
            }
            public override int GetHashCode()
            {
                return this.CGIName.GetHashCode() ^ this.CGIPath.GetHashCode() ^ this.ParameArr.GetHashCode();
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.CGIPath).Append(this.CGIName).Append(this.JoinChar);
                foreach (KeyValuePair<string,string> kv in this.ParameArr)
                {
                    sb.Append(kv.Key).Append("=").Append(kv.Value).Append("&");
                }
                return sb.ToString().TrimEnd('&');
            }
        }

    }

}
