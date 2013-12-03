package net.xknife.webknife;

import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;

import javax.servlet.http.HttpServletRequest;

import net.xknife.jsonknife.JsonKnife;
import net.xknife.library.DI;
import net.xknife.webknife.interfaces.IController;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.google.inject.ConfigurationException;
import com.google.inject.Key;
import com.google.inject.ProvisionException;
import com.google.inject.name.Names;

/**
 * 本项目中的webapi的抽象基类。
 * 
 * @author lukan@jeelu.com 2013-8-22
 */
public abstract class WebapiServlet<T> extends BaseWebapiServlet<T>
{
	private static final long serialVersionUID = 221407847843999547L;
	static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(WebapiServlet.class);

	/**
	 * 本项目的API的基础运行函数。本项目约定，绝大多数的API均以POST请求，参数以Json方式传递。
	 */
	@Override
	protected void runPost(final HttpServletRequest request, final PrintWriter writer)
	{
		String servletPath = request.getServletPath();
		String[] paths = servletPath.split("/");
		String moduleName = paths[1];
		String controlerName = paths[2];
		String methodName = paths[3];

		String controlerFullName = String.format("%s/%s", moduleName, controlerName);
		String queryInfo = null;
		try
		{
			queryInfo = URLDecoder.decode(request.getQueryString(), "utf-8");
		}
		catch (UnsupportedEncodingException e)
		{
			_Logger.warn(String.format("参数解码异常:%s", request.getQueryString()), e);
		}
		try
		{
			IController controler = DI.getInstance(Key.get(IController.class, Names.named(controlerFullName)));
			T params = getParams(queryInfo);// 转换参数为pojo对象
			controler.process(methodName, params, writer);// 根据找到的controler执行相应的方法
		}
		catch (ConfigurationException e)
		{
			_Logger.warn(String.format("未通过Guice配置指定的IControler:%s", controlerFullName), e);
		}
		catch (ProvisionException e)
		{
			_Logger.warn(String.format("IControler:%s 实例失败", controlerFullName), e);
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
	 * @param queryInfo
	 * @return
	 * @throws JsonParseException
	 * @throws JsonMappingException
	 * @throws IOException
	 */
	@Override
	protected T getParams(final String queryInfo) throws JsonParseException, JsonMappingException, IOException
	{
		return JsonKnife.getMapper().readValue(queryInfo, getParamsType());
	}

	/**
	 * 获取参数pojo类型。在最后的实现类中实现。
	 * 
	 * @return
	 */
	protected abstract Class<T> getParamsType();
}
