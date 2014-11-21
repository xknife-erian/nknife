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

    /**
     * @param actionUrl 上传地址
     * @param FileName  上传文件路径
     * @return
     * @throws IOException
     */
    public String upload(String actionUrl, String FileName) throws IOException
    {
        // 产生随机分隔内容
        String BOUNDARY = java.util.UUID.randomUUID().toString();

        String PREFIX = "--", LINE = "\r\n";
        String MULTIPART_FROM_DATA = "multipart/form-data";
        String CHARSET = "UTF-8";

        // 定义URL实例
        URL uri = new URL(actionUrl);
        HttpURLConnection conn = (HttpURLConnection) uri.openConnection();
        // 设置从主机读取数据超时
        conn.setReadTimeout(10 * 1000);
        setDoIO(conn);
        conn.setUseCaches(false);
        conn.setRequestMethod("POST");
        // 维持长连接
        conn.setRequestProperty("connection", "keep-alive");
        conn.setRequestProperty("Charset", "UTF-8");
        // 设置文件类型
        conn.setRequestProperty("Content-Type", MULTIPART_FROM_DATA + ";boundary=" + BOUNDARY);

        // 创建一个新的数据输出流，将数据写入指定基础输出流
        DataOutputStream outStream = new DataOutputStream(conn.getOutputStream());
        // 发送文件数据
        if (FileName != null)
        {
            // 构建发送字符串数据
            StringBuilder sb1 = new StringBuilder();
            sb1.append(PREFIX);
            sb1.append(BOUNDARY);
            sb1.append(LINE);
            sb1.append("Content-Disposition: form-data; name=\"file\"; filename=\"" + FileName + "\"" + LINE);
            sb1.append("Content-Type: application/octet-stream;chartset=" + CHARSET + LINE);
            sb1.append(LINE);
            // 写入到输出流中
            outStream.write(sb1.toString().getBytes());
            // 将文件读入输入流中
            InputStream is = new FileInputStream(FileName);
            byte[] buffer = new byte[1024];
            int len = 0;
            // 写入输出流
            while ((len = is.read(buffer)) != -1)
            {
                outStream.write(buffer, 0, len);
            }
            is.close();
            // 添加换行标志
            outStream.write(LINE.getBytes());
        }
        // 请求结束标志
        byte[] end_data = (PREFIX + BOUNDARY + PREFIX + LINE).getBytes();
        outStream.write(end_data);
        // 刷新发送数据
        outStream.flush();

        String result = "";

        // 得到响应码
        int res = conn.getResponseCode();
        if (res == 200)
        {
            InputStream in = conn.getInputStream();
            result = read(in);
        }
        _Logger.info(String.format("上传结果状态码 %d (返回 %s)", res, result));
        return result;
    }
}
