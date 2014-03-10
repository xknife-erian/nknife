package net.xknife.web;

import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;

import javax.servlet.http.HttpServletRequest;

import net.xknife.inject.DI;
import net.xknife.json.JsonKnife;
import net.xknife.web.interfaces.IController;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.google.inject.ConfigurationException;
import com.google.inject.Key;
import com.google.inject.ProvisionException;
import com.google.inject.name.Names;

/**
 * 针对多数Ajax的提交数据为Json时的Servlet的基类。
 * 
 * @author lukan@jeelu.com 2013-8-22
 */
public abstract class WebapiServlet<T> extends BaseWebapiServlet<T>
{
	private static final long serialVersionUID = 221407847843999547L;
	private static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(WebapiServlet.class);

	/**
	 * 本项目的API的基础运行函数。约定，绝大多数的API均以POST请求，参数以Json方式传递。
	 */
	@Override
	protected void runPost(final HttpServletRequest request, final PrintWriter writer)
	{
		String servletPath = request.getServletPath();
		String[] paths = servletPath.split("/");
		String moduleName = paths[1];
		String controllerName = paths[2];
		String methodName = paths[3];

		String controllerFullName = String.format("%s/%s", moduleName, controllerName);
		String queryInfo = "";
        T params = null;

		try
		{
			params = getParams(request);// 转换参数为pojo对象
		}
		catch (ConfigurationException e)
		{
			_Logger.warn(String.format("未通过Guice配置指定的IController:%s", controllerFullName), e);
		}
		catch (ProvisionException e)
		{
			_Logger.warn(String.format("IController:%s 实例失败", controllerFullName), e);
		}
		catch (JsonParseException e)
		{
			_Logger.warn(String.format("参数转换异常:%s", queryInfo), e);
		}
		catch (JsonMappingException e)
		{
			_Logger.warn(String.format("参数转换异常:%s", queryInfo), e);
		}
		catch (IOException e)
		{
			_Logger.warn(String.format("JSON转换器异常:%s", queryInfo), e);
		}
        try
        {
            IController controller = DI.getInstance(Key.get(IController.class, Names.named(controllerFullName)));
            // 根据找到的controller执行相应的方法
            controller.process(methodName, params, writer);
        }
        catch (Exception e)
        {
            _Logger.warn(String.format("IController(%s)异常:%s", controllerFullName, queryInfo), e);
        }
	}

	/**
	 * 本项目WebApi仅支持post方式，较安全，并且数据量较大一些。
	 */
	@Override
	protected void runGet(final HttpServletRequest request, final PrintWriter writer)
	{
		runPost(request, writer);
	}

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
        String queryInfo = null;
        try
        {
            queryInfo = URLDecoder.decode(request.getQueryString(), "utf-8");
        }
        catch (UnsupportedEncodingException e)
        {
            _Logger.warn(String.format("参数解码异常:%s", request.getQueryString()), e);
        }
		return JsonKnife.getMapper().readValue(queryInfo, getParamsType());
	}

	/**
	 * 获取参数pojo类型。在最后的实现类中实现。
	 * 
	 * @return
	 */
	protected abstract Class<T> getParamsType();
}
