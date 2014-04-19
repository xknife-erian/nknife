package net.xknife.web;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.*;
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
        try {
            URL realUrl = new URL(url + "?" + param);
            // 打开和URL之间的连接
            URLConnection connection = realUrl.openConnection();
            setRequestProperty(connection);
            // 建立实际的连接
            connection.connect();

            result = read(connection.getInputStream());
        } catch (Exception e) {
            _Logger.error("发送GET请求出现异常！" + e.getStackTrace());
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
            setRequestProperty(connection);
            setDoIO(connection);

            out = print(connection.getOutputStream(), param);
            result = read(connection.getInputStream());
        } catch (Exception e) {
            _Logger.error("发送 POST 请求出现异常！" + e.getStackTrace());
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
    private void setRequestProperty(URLConnection connection)
    {
        connection.setRequestProperty("accept", "*/*");
        connection.setRequestProperty("connection", "Keep-Alive");
        connection.setRequestProperty("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)");
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
