package net.xknife.web;

/**
 * Created by Cripps.Yang on 2014/4/19.
 */
public class HttpRequestTest
{
    public static void main(String[] args)
    {
        HttpRequest request = new HttpRequest();
        //发送 GET 请求
        String s= request.sendGet("http://www.baidu.com/s", "wd=a&tn=baidu&ie=utf-8&f=8&rsv_bp=1&inputT=0&bs=a&rsv_spt=3");
        System.out.println(s);

        //发送 POST 请求
        String sr=request.sendPost("http://www.baidu.com/s", "wd=a&tn=baidu&ie=utf-8&f=8&rsv_bp=1&inputT=0&bs=a&rsv_spt=3");
        System.out.println(sr);
    }
}
