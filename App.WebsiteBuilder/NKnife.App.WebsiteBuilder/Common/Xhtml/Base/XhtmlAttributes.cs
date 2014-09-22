using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Jeelu
{
    public partial class XhtmlAtts
    {
        private XhtmlAtts() { }

        public class Href : XhtmlAttribute
        {
            public Href(string value) : base("href", value) { }
        }
        public class Name : XhtmlAttribute
        {
            public Name(string value) : base("name", value) { }
        }
        public class Title : XhtmlAttribute
        {
            public Title(string value) : base("title", value) { }
        }
        public class Target : XhtmlAttribute
        {
            public Target(Xhtml.Target value)
                : base("target", AttributeHelper.GetValue("target", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Charset : XhtmlAttribute
        {
            public Charset(Xhtml.Charset value)
                : base("charset", AttributeHelper.GetValue("charset", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Class : XhtmlAttribute
        {
            public Class(string value) : base("class", value) { }
        }
        public class Id : XhtmlAttribute
        {
            public Id(string value) : base("id", value) { }
        }
        //public class Style : XhtmlAttribute
        //{
        //    public Style(string value) : base("style", value) { }
        //}
        public class Hreflang : XhtmlAttribute
        {
            public Hreflang(string value) : base("hreflang", value) { }
        }
        public class Accesskey : XhtmlAttribute
        {
            public Accesskey(string value) : base("accesskey", value) { }
        }
        public class Tabindex : XhtmlAttribute
        {
            public Tabindex(string value) : base("tabindex", value) { }
        }
        public class Rel : XhtmlAttribute
        {
            public Rel(string value) : base("rel", value) { }
        }
        public class Rev : XhtmlAttribute
        {
            public Rev(string value) : base("rev", value) { }
        }
        public class Shape : XhtmlAttribute
        {
            public Shape(string value) : base("shape", value) { }
        }
        public class Coords : XhtmlAttribute
        {
            public Coords(string value) : base("coords", value) { }
        }
        public class Dir : XhtmlAttribute
        {
            public Dir(Xhtml.Dir value)
                : base("dir", AttributeHelper.GetValue("dir", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Lang : XhtmlAttribute
        {
            public Lang(Xhtml.Lang value)
                : base("lang", AttributeHelper.GetValue("lang", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Onfocus : XhtmlAttribute
        {
            public Onfocus(string value) : base("onfocus", value) { }
        }
        public class Onblur : XhtmlAttribute
        {
            public Onblur(string value) : base("onblur", value) { }
        }
        public class Onclick : XhtmlAttribute
        {
            public Onclick(string value) : base("onclick", value) { }
        }
        public class Ondblclick : XhtmlAttribute
        {
            public Ondblclick(string value) : base("ondblclick", value) { }
        }
        public class Onmousedown : XhtmlAttribute
        {
            public Onmousedown(string value) : base("onmousedown", value) { }
        }
        public class Onmouseup : XhtmlAttribute
        {
            public Onmouseup(string value) : base("onmouseup", value) { }
        }
        public class Onmouseover : XhtmlAttribute
        {
            public Onmouseover(string value) : base("onmouseover", value) { }
        }
        public class Onmousemove : XhtmlAttribute
        {
            public Onmousemove(string value) : base("onmousemove", value) { }
        }
        public class Onmouseout : XhtmlAttribute
        {
            public Onmouseout(string value) : base("onmouseout", value) { }
        }
        public class Onkeypress : XhtmlAttribute
        {
            public Onkeypress(string value) : base("onkeypress", value) { }
        }
        public class Onkeydown : XhtmlAttribute
        {
            public Onkeydown(string value) : base("onkeydown", value) { }
        }
        public class Onkeyup : XhtmlAttribute
        {
            public Onkeyup(string value) : base("onkeyup", value) { }
        }
        public class Noexternaldata : XhtmlAttribute
        {
            public Noexternaldata(Xhtml.Noexternaldata value)
                : base("noexternaldata", AttributeHelper.GetValue("noexternaldata", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Code : XhtmlAttribute
        {
            public Code(string value) : base("code", value) { }
        }
        public class Codebase : XhtmlAttribute
        {
            public Codebase(string value) : base("codebase", value) { }
        }
        public class Alt : XhtmlAttribute
        {
            public Alt(string value) : base("alt", value) { }
        }
        public class Width : XhtmlAttribute
        {
            public Width(string value) : base("width", value) { }
        }
        public class Height : XhtmlAttribute
        {
            public Height(string value) : base("height", value) { }
        }
        public class Hspace : XhtmlAttribute
        {
            public Hspace(string value) : base("hspace", value) { }
        }
        public class Vspace : XhtmlAttribute
        {
            public Vspace(string value) : base("vspace", value) { }
        }
        public class Align : XhtmlAttribute
        {
            public Align(Xhtml.Align value)
                : base("align", AttributeHelper.GetValue("align", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Cabbase : XhtmlAttribute
        {
            public Cabbase(string value) : base("cabbase", value) { }
        }
        public class Mayscript : XhtmlAttribute
        {
            public Mayscript(string value) : base("mayscript", value) { }
        }
        public class Archive : XhtmlAttribute
        {
            public Archive(string value) : base("archive", value) { }
        }
        public class Value : XhtmlAttribute
        {
            public Value(string value) : base("value", value) { }
        }
        public class Disabled : XhtmlAttribute
        {
            public Disabled(string value) : base("disabled", value) { }
        }
        public class Size : XhtmlAttribute
        {
            public Size(Xhtml.Size value)
                : base("size", AttributeHelper.GetValue("size", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Face : XhtmlAttribute
        {
            public Face(string value) : base("face", value) { }
        }
        public class Color : XhtmlAttribute
        {
            public Color(string value) : base("color", value) { }
        }
        public class Src : XhtmlAttribute
        {
            public Src(string value) : base("src", value) { }
        }
        public class Loop : XhtmlAttribute
        {
            public Loop(Xhtml.Loop value)
                : base("loop", AttributeHelper.GetValue("loop", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Balance : XhtmlAttribute
        {
            public Balance(string value) : base("balance", value) { }
        }
        public class Volume : XhtmlAttribute
        {
            public Volume(string value) : base("volume", value) { }
        }
        public class Delay : XhtmlAttribute
        {
            public Delay(string value) : base("delay", value) { }
        }
        public class Cite : XhtmlAttribute
        {
            public Cite(string value) : base("cite", value) { }
        }
        public class Bgcolor : XhtmlAttribute
        {
            public Bgcolor(string value) : base("bgcolor", value) { }
        }
        public class Background : XhtmlAttribute
        {
            public Background(string value) : base("background", value) { }
        }
        public class Text : XhtmlAttribute
        {
            public Text(string value) : base("text", value) { }
        }
        public class Link : XhtmlAttribute
        {
            public Link(string value) : base("link", value) { }
        }
        public class Vlink : XhtmlAttribute
        {
            public Vlink(string value) : base("vlink", value) { }
        }
        public class Alink : XhtmlAttribute
        {
            public Alink(string value) : base("alink", value) { }
        }
        public class Leftmargin : XhtmlAttribute
        {
            public Leftmargin(string value) : base("leftmargin", value) { }
        }
        public class Topmargin : XhtmlAttribute
        {
            public Topmargin(string value) : base("topmargin", value) { }
        }
        public class Bgproperties : XhtmlAttribute
        {
            public Bgproperties(string value) : base("bgproperties", value) { }
        }
        public class Rightmargin : XhtmlAttribute
        {
            public Rightmargin(string value) : base("rightmargin", value) { }
        }
        public class Bottommargin : XhtmlAttribute
        {
            public Bottommargin(string value) : base("bottommargin", value) { }
        }
        public class Marginwidth : XhtmlAttribute
        {
            public Marginwidth(string value) : base("marginwidth", value) { }
        }
        public class Marginheight : XhtmlAttribute
        {
            public Marginheight(string value) : base("marginheight", value) { }
        }
        public class Onload : XhtmlAttribute
        {
            public Onload(string value) : base("onload", value) { }
        }
        public class Onunload : XhtmlAttribute
        {
            public Onunload(string value) : base("onunload", value) { }
        }
        public class Onerror : XhtmlAttribute
        {
            public Onerror(string value) : base("onerror", value) { }
        }
        public class Onresize : XhtmlAttribute
        {
            public Onresize(string value) : base("onresize", value) { }
        }
        public class Clear : XhtmlAttribute
        {
            public Clear(Xhtml.Clear value)
                : base("clear", AttributeHelper.GetValue("clear", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Type : XhtmlAttribute
        {
            public Type(Xhtml.Type value)
                : base("type", AttributeHelper.GetValue("type", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Valign : XhtmlAttribute
        {
            public Valign(Xhtml.Valign value)
                : base("valign", AttributeHelper.GetValue("valign", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Span : XhtmlAttribute
        {
            public Span(string value) : base("span", value) { }
        }
        public class Datetime : XhtmlAttribute
        {
            public Datetime(string value) : base("datetime", value) { }
        }
        public class Compact : XhtmlAttribute
        {
            public Compact(string value) : base("compact", value) { }
        }
        public class Hidden : XhtmlAttribute
        {
            public Hidden(Xhtml.Hidden value)
                : base("hidden", AttributeHelper.GetValue("hidden", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Autostart : XhtmlAttribute
        {
            public Autostart(Xhtml.Autostart value)
                : base("autostart", AttributeHelper.GetValue("autostart", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Pointsize : XhtmlAttribute
        {
            public Pointsize(string value) : base("pointsize", value) { }
        }
        public class Action : XhtmlAttribute
        {
            public Action(string value) : base("action", value) { }
        }
        public class Method : XhtmlAttribute
        {
            public Method(Xhtml.Method value)
                : base("method", AttributeHelper.GetValue("method", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Enctype : XhtmlAttribute
        {
            public Enctype(Xhtml.Enctype value)
                : base("enctype", AttributeHelper.GetValue("enctype", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Runat : XhtmlAttribute
        {
            public Runat(Xhtml.Runat value)
                : base("runat", AttributeHelper.GetValue("runat", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Onsubmit : XhtmlAttribute
        {
            public Onsubmit(string value) : base("onsubmit", value) { }
        }
        public class Onreset : XhtmlAttribute
        {
            public Onreset(string value) : base("onreset", value) { }
        }
        public class Frameborder : XhtmlAttribute
        {
            public Frameborder(Xhtml.Frameborder value)
                : base("frameborder", AttributeHelper.GetValue("frameborder", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Scrolling : XhtmlAttribute
        {
            public Scrolling(Xhtml.Scrolling value)
                : base("scrolling", AttributeHelper.GetValue("scrolling", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Noresize : XhtmlAttribute
        {
            public Noresize(string value) : base("noresize", value) { }
        }
        public class Bordercolor : XhtmlAttribute
        {
            public Bordercolor(string value) : base("bordercolor", value) { }
        }
        public class Longdesc : XhtmlAttribute
        {
            public Longdesc(string value) : base("longdesc", value) { }
        }
        public class Rows : XhtmlAttribute
        {
            public Rows(string value) : base("rows", value) { }
        }
        public class Cols : XhtmlAttribute
        {
            public Cols(string value) : base("cols", value) { }
        }
        public class Framespacing : XhtmlAttribute
        {
            public Framespacing(string value) : base("framespacing", value) { }
        }
        public class Border : XhtmlAttribute
        {
            public Border(string value) : base("border", value) { }
        }
        public class Profile : XhtmlAttribute
        {
            public Profile(string value) : base("profile", value) { }
        }
        public class Noshade : XhtmlAttribute
        {
            public Noshade(string value) : base("noshade", value) { }
        }
        public class Xmlns : XhtmlAttribute
        {
            public Xmlns(string value) : base("xmlns", value) { }
        }
        public class Xml_lang : XhtmlAttribute
        {
            public Xml_lang(string value) : base("xml:lang", value) { }
        }
        public class Left : XhtmlAttribute
        {
            public Left(string value) : base("left", value) { }
        }
        public class Top : XhtmlAttribute
        {
            public Top(string value) : base("top", value) { }
        }
        public class Pagex : XhtmlAttribute
        {
            public Pagex(string value) : base("pagex", value) { }
        }
        public class Pagey : XhtmlAttribute
        {
            public Pagey(string value) : base("pagey", value) { }
        }
        public class Above : XhtmlAttribute
        {
            public Above(string value) : base("above", value) { }
        }
        public class Below : XhtmlAttribute
        {
            public Below(string value) : base("below", value) { }
        }
        public class Z_index : XhtmlAttribute
        {
            public Z_index(string value) : base("z-index", value) { }
        }
        public class Visibility : XhtmlAttribute
        {
            public Visibility(Xhtml.Visibility value)
                : base("visibility", AttributeHelper.GetValue("visibility", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Clip : XhtmlAttribute
        {
            public Clip(string value) : base("clip", value) { }
        }
        public class Usemap : XhtmlAttribute
        {
            public Usemap(string value) : base("usemap", value) { }
        }
        public class Ismap : XhtmlAttribute
        {
            public Ismap(string value) : base("ismap", value) { }
        }
        public class Dynsrc : XhtmlAttribute
        {
            public Dynsrc(string value) : base("dynsrc", value) { }
        }
        public class Controls : XhtmlAttribute
        {
            public Controls(string value) : base("controls", value) { }
        }
        public class Start : XhtmlAttribute
        {
            public Start(Xhtml.Start value)
                : base("start", AttributeHelper.GetValue("start", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Lowsrc : XhtmlAttribute
        {
            public Lowsrc(string value) : base("lowsrc", value) { }
        }
        public class Onselect : XhtmlAttribute
        {
            public Onselect(string value) : base("onselect", value) { }
        }
        public class Onchange : XhtmlAttribute
        {
            public Onchange(string value) : base("onchange", value) { }
        }
        public class Maxlength : XhtmlAttribute
        {
            public Maxlength(string value) : base("maxlength", value) { }
        }
        public class Readonly : XhtmlAttribute
        {
            public Readonly(string value) : base("readonly", value) { }
        }
        public class Checked : XhtmlAttribute
        {
            public Checked(string value) : base("checked", value) { }
        }
        public class Accept : XhtmlAttribute
        {
            public Accept(Xhtml.Accept value)
                : base("accept", AttributeHelper.GetValue("accept", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class For : XhtmlAttribute
        {
            public For(string value) : base("for", value) { }
        }
        public class Media : XhtmlAttribute
        {
            public Media(Xhtml.Media value)
                : base("media", AttributeHelper.GetValue("media", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Behavior : XhtmlAttribute
        {
            public Behavior(Xhtml.Behavior value)
                : base("behavior", AttributeHelper.GetValue("behavior", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Direction : XhtmlAttribute
        {
            public Direction(Xhtml.Direction value)
                : base("direction", AttributeHelper.GetValue("direction", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Scrollamount : XhtmlAttribute
        {
            public Scrollamount(string value) : base("scrollamount", value) { }
        }
        public class Scrolldelay : XhtmlAttribute
        {
            public Scrolldelay(string value) : base("scrolldelay", value) { }
        }
        public class Truespeed : XhtmlAttribute
        {
            public Truespeed(string value) : base("truespeed", value) { }
        }
        public class Http_equiv : XhtmlAttribute
        {
            public Http_equiv(Xhtml.Http_equiv value)
                : base("http-equiv", AttributeHelper.GetValue("http-equiv", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Content : XhtmlAttribute
        {
            public Content(string value) : base("content", value) { }
        }
        public class Gutter : XhtmlAttribute
        {
            public Gutter(string value) : base("gutter", value) { }
        }
        public class Classid : XhtmlAttribute
        {
            public Classid(string value) : base("classid", value) { }
        }
        public class Codetype : XhtmlAttribute
        {
            public Codetype(string value) : base("codetype", value) { }
        }
        public class Data : XhtmlAttribute
        {
            public Data(string value) : base("data", value) { }
        }
        public class Declare : XhtmlAttribute
        {
            public Declare(string value) : base("declare", value) { }
        }
        public class Standby : XhtmlAttribute
        {
            public Standby(string value) : base("standby", value) { }
        }
        public class Label : XhtmlAttribute
        {
            public Label(string value) : base("label", value) { }
        }
        public class Selected : XhtmlAttribute
        {
            public Selected(string value) : base("selected", value) { }
        }
        public class Valuetype : XhtmlAttribute
        {
            public Valuetype(Xhtml.Valuetype value)
                : base("valuetype", AttributeHelper.GetValue("valuetype", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Language : XhtmlAttribute
        {
            public Language(Xhtml.Language value)
                : base("language", AttributeHelper.GetValue("language", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Defer : XhtmlAttribute
        {
            public Defer(string value) : base("defer", value) { }
        }
        public class Multiple : XhtmlAttribute
        {
            public Multiple(string value) : base("multiple", value) { }
        }
        public class Cellpadding : XhtmlAttribute
        {
            public Cellpadding(string value) : base("cellpadding", value) { }
        }
        public class Cellspacing : XhtmlAttribute
        {
            public Cellspacing(string value) : base("cellspacing", value) { }
        }
        public class Bordercolorlight : XhtmlAttribute
        {
            public Bordercolorlight(string value) : base("bordercolorlight", value) { }
        }
        public class Bordercolordark : XhtmlAttribute
        {
            public Bordercolordark(string value) : base("bordercolordark", value) { }
        }
        public class Datapagesize : XhtmlAttribute
        {
            public Datapagesize(string value) : base("datapagesize", value) { }
        }
        public class Frame : XhtmlAttribute
        {
            public Frame(Xhtml.Frame value)
                : base("frame", AttributeHelper.GetValue("frame", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Rules : XhtmlAttribute
        {
            public Rules(Xhtml.Rules value)
                : base("rules", AttributeHelper.GetValue("rules", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }
        public class Summary : XhtmlAttribute
        {
            public Summary(string value) : base("summary", value) { }
        }
        public class Colspan : XhtmlAttribute
        {
            public Colspan(string value) : base("colspan", value) { }
        }
        public class Rowspan : XhtmlAttribute
        {
            public Rowspan(string value) : base("rowspan", value) { }
        }
        public class Nowrap : XhtmlAttribute
        {
            public Nowrap(string value) : base("nowrap", value) { }
        }
        public class Axis : XhtmlAttribute
        {
            public Axis(string value) : base("axis", value) { }
        }
        public class Headers : XhtmlAttribute
        {
            public Headers(string value) : base("headers", value) { }
        }
        public class Scope : XhtmlAttribute
        {
            public Scope(string value) : base("scope", value) { }
        }
        public class Abbr : XhtmlAttribute
        {
            public Abbr(string value) : base("abbr", value) { }
        }
        public class Wrap : XhtmlAttribute
        {
            public Wrap(Xhtml.Wrap value)
                : base("wrap", AttributeHelper.GetValue("wrap", value.ToString().ToLower(CultureInfo.CurrentCulture))) { }
        }

    }
}
