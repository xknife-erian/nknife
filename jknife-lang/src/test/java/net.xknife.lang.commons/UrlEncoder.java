package net.xknife.lang.commons;

import org.apache.commons.lang3.StringEscapeUtils;

/**
 * 转义字符帮助类
 * Created by yangjuntao@p-an.com on 14-4-18.
 */
public class UrlEncoder
{
    public static void main(String[] args)
    {
        String t = "&lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;\n" +
                "&lt;dataPacket version=&quot;1.0&quot;&gt;\n" +
                "  &lt;transCode&gt;T000102&lt;/transCode&gt;\n" +
                "  &lt;orgCode&gt;0250&lt;/orgCode&gt;\n" +
                "  &lt;machineCode&gt;E04C1248EA&lt;/machineCode&gt;\n" +
                "  &lt;typeFlag&gt;02&lt;/typeFlag&gt;\n" +
                "  &lt;cardNo&gt;6226300120028898&lt;/cardNo&gt;\n" +
                "  &lt;transTime&gt;2014-04-09 11:54:44&lt;/transTime&gt;\n" +
                "  &lt;queueCode&gt;3001&lt;/queueCode&gt;\n" +
                "  &lt;queueLeft&gt;11&lt;/queueLeft&gt;\n" +
                "  &lt;processBeginTime&gt;2014-04-09 16:05:51&lt;/processBeginTime&gt;\n" +
                "  &lt;handTellerNo&gt;01&lt;/handTellerNo&gt;\n" +
                "&lt;/dataPacket&gt;";
        String p = "&amp;";
        String q = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                "<dataPacket version=\"1.0\">\n" +
                "  <transCode>T000101</transCode>\n" +
                "  <orgCode>DHT973</orgCode>\n" +
                "  <machineCode>00E04C1248EA-BFEBFBFF000106CA</machineCode>\n" +
                "  <typeFlag>02</typeFlag>\n" +
                "  <cardNo>6212260200050905468</cardNo>\n" +
                "  <transTime>2014-04-09 12:25:22</transTime>\n" +
                "  <queueCode></queueCode>\n" +
                "  <queueLeft></queueLeft>\n" +
                "</dataPacket>";
        System.out.println(StringEscapeUtils.unescapeHtml4(t));
        System.out.println(StringEscapeUtils.unescapeHtml4(p));
        System.out.println(StringEscapeUtils.unescapeHtml4(q));
        System.out.println(StringEscapeUtils.escapeHtml4(q));
    }
}
