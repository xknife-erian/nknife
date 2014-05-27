package net.xknife.web.utilities;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;
import java.util.Map;

/**
 * http GET/POST 请求帮助类。
 *
 * Created by Cripps.Yang on 2014/4/19.
 */
public class HttpRequest
{
    private static final Logger _Logger = LoggerFactory.getLogger(HttpRequest.class);

    /**
     * 向指定URL发送GET方法的请求
     *
     * @param url
     *            发送请求的URL
     * @param param
     *            请求参数，请求参数应该是 name1=value1&name2=value2 的形式。
     * @return URL 所代表远程资源的响应结果
     */
    public String sendGet(String url, String param) {
        String result = "";
        String temp = "";
        try {
            temp = url + "?" + param;
            _Logger.trace(temp);
            URL realUrl = new URL(temp);
            URLConnection connection = realUrl.openConnection();
            setRequestProperty((HttpURLConnection)connection, "GET");
            connection.connect();

            result = read(connection.getInputStream());
        } catch (Exception e) {
            _Logger.error("发送GET请求出现异常！{url="+ temp + "}", e);
        }
        return result;
    }

    /**
     * 向指定 URL 发送POST方法的请求
     *
     * @param url
     *            发送请求的 URL
     * @param param
     *            请求参数，请求参数应该是 name1=value1&name2=value2 的形式。
     * @return 所代表远程资源的响应结果
     */
    public String sendPost(String url, String param) {
        PrintWriter out = null;
        String result = "";
        try {
            URL realUrl = new URL(url);
            // 打开和URL之间的连接
            URLConnection connection = realUrl.openConnection();

            setRequestProperty((HttpURLConnection)connection, "POST");
            setDoIO(connection);

            out = print(connection.getOutputStream(), param);
            result = read(connection.getInputStream());
        } catch (Exception e) {
            _Logger.error("发送 POST 请求出现异常！", e);
        } finally{
            close(null,out);
        }
        return result;
    }

    /**
     * 通过输出流发送请求参数
     *
     * @param outputStream
     * @return
     */
    public PrintWriter print(OutputStream outputStream , String param)
    {
        PrintWriter out = new PrintWriter(outputStream);
        out.print(param);
        out.flush();
        return out;
    }

    /**
     * 通过输入流获取响应信息
     *
     * @param inputStream
     * @return
     */
    public String read(InputStream inputStream)
    {
        String result = "";
        BufferedReader in = new BufferedReader(new InputStreamReader(inputStream));
        String line;
        try
        {
            while ((line = in.readLine()) != null) {
                result += line;
            }
        } catch (IOException e)
        {
            e.printStackTrace();
        } finally
        {
            close(in,null);
        }

        return result;
    }

    /**
     * 发送POST请求必须设置
     *
     * @param connection
     */
    private void setDoIO(URLConnection connection)
    {
        connection.setDoOutput(true);
        connection.setDoInput(true);
    }

    /**
     * 设置通用的请求属性
     *
     * @param connection
     */
    private void setRequestProperty(HttpURLConnection connection, String method) throws ProtocolException
    {
        connection.setRequestProperty("accept", "*/*");
        connection.setRequestProperty("connection", "Keep-Alive");
        connection.setRequestProperty("user-agent", "Mozilla/5.0 (Windows; U; MSIE 7.0; Windows NT 6.0)");
        connection.setRequestMethod(method);
        connection.setConnectTimeout(5000);
        connection.setReadTimeout(5000);
    }

    /**
     * 获取所有响应头字段
     *
     * @param connection
     * @return
     */
    private Map<String, List<String>> getHeaderFields(URLConnection connection)
    {
        Map<String, List<String>> map = connection.getHeaderFields();
        return map;
    }

    /**
     * 流的关闭
     *
     * @param in
     * @param out
     */
    public void close(BufferedReader in,PrintWriter out)
    {
        try
        {
            if(out!=null){
                out.close();
            }
            if(in!=null){
                in.close();
            }
        } catch (IOException e)
        {
            e.printStackTrace();
        }
    }
}
