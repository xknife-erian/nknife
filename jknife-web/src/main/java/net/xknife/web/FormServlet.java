package net.xknife.web;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import net.xknife.json.JsonKnife;

import javax.servlet.http.HttpServletRequest;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;

/**
 * 传统的页面提交数据是通过form方式提交是格式是xxx=yyyy$aaa=bbbb时的Servlet的基类
 * Created by erianlu on 14-2-28.
 */
public abstract class FormServlet<T> extends WebapiServlet<T>
{
    private static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(FormServlet.class);

    /**
     * 将URL信息中的参数信息(json格式)转换成当前servlet的参数pojo类型
     *
     * @param request
     * @return
     * @throws com.fasterxml.jackson.core.JsonParseException
     * @throws com.fasterxml.jackson.databind.JsonMappingException
     * @throws java.io.IOException
     */
    @Override
    protected T getParams(final HttpServletRequest request) throws JsonParseException, JsonMappingException, IOException
    {
        String queryInfo = null;
        try
        {
            queryInfo = URLDecoder.decode(request.getQueryString(), "utf-8");
            logRequest(request,queryInfo);
        }
        catch (UnsupportedEncodingException e)
        {
            _Logger.warn(String.format("参数解码异常:%s", request.getQueryString()), e);
        }
        return JsonKnife.getMapper().readValue(queryInfo, getParamsType());
    }
}
