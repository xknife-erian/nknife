package net.xknife.web;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import net.xknife.json.JsonKnife;
import net.xknife.lang.widgets.Sber;

import javax.servlet.http.HttpServletRequest;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;
import java.util.Enumeration;
import java.util.List;

/**
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
        Sber sb = Sber.ME();
        String queryInfo = null;
        Enumeration<String> names =  request.getParameterNames();
        while(names.hasMoreElements())
        {
            String name = names.nextElement();
            String value = request.getParameter(name);
            sb.append("\"").append(name).append("\"").append(":");
            sb.append("\"").append(value).append("\"").append(";");
        }
        sb.surround("{","}");
        queryInfo = sb.toString();
        return JsonKnife.getMapper().readValue(queryInfo, getParamsType());
    }
}
