package net.xknife.web;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.google.inject.ConfigurationException;
import com.google.inject.Key;
import com.google.inject.ProvisionException;
import com.google.inject.name.Names;
import net.xknife.inject.DI;
import net.xknife.json.JsonKnife;
import net.xknife.lang.widgets.Sber;
import net.xknife.web.interfaces.IController;

import javax.servlet.http.HttpServletRequest;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Enumeration;

/**
 * 传统的页面提交数据是通过Form方式提交是格式是xxx=yyyy$aaa=bbbb时的Servlet的基类
 * @author lukan@jeelu.com 2013-8-22
 */
public abstract class FormServlet<T> extends WebapiServlet<T>
{
	private static final long serialVersionUID = 221407847843999547L;
	private static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(FormServlet.class);

	/**
	 * 将URL信息中的参数信息(json格式)转换成当前servlet的参数pojo类型
	 * 
	 * @param request
	 * @return
	 * @throws JsonParseException
	 * @throws JsonMappingException
	 * @throws IOException
	 */
	@Override
	protected T getParams(final HttpServletRequest request) throws JsonParseException, JsonMappingException, IOException
	{
        Sber sb = Sber.ME();
        String queryInfo = null;
        Enumeration<String> names = request.getParameterNames();
        while (names.hasMoreElements())
        {
            String name = names.nextElement();
            String value = request.getParameter(name);
            sb.append("\"").append(name).append("\"").append(":");
            sb.append("\"").append(value).append("\"").append(",");
        }
        if (sb.length() > 0)
        {
            sb.trimEnd();
        }
        sb.surround("{", "}");
        queryInfo = sb.toString();
        logRequest(request,queryInfo);
        return JsonKnife.getMapper().readValue(queryInfo, getParamsType());
	}

}
