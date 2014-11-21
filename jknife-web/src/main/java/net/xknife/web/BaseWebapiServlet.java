package net.xknife.web;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

/**
 * 本项目中的webapi的抽象基类。
 * 
 * @author lukan@jeelu.com 2013-8-22
 */
public abstract class BaseWebapiServlet<T> extends HttpServlet
{
	private static final long serialVersionUID = 5870752496794114400L;
	private static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(BaseWebapiServlet.class);

	/**
	 * 收到请求后，打印日志
	 * 
	 * @param request
	 * @param data
	 */
	protected void logRequest(final HttpServletRequest request, final String data)
	{
		_Logger.trace(String.format("请求:%s;%s;%s;Data:%s", request.getProtocol(), request.getMethod(), request.getServletPath(), data));
	}

	@Override
	protected void doGet(final HttpServletRequest request, final HttpServletResponse response) throws ServletException, IOException
	{
		PrintWriter writer = response.getWriter();
		try
		{
			runGet(request, writer);
			writer.flush();
		}
		catch (Exception e)
		{
			_Logger.warn("Servlet处理异常.", e);
		}
		finally
		{
			writer.close();
		}
	}

	@Override
	protected void doPost(final HttpServletRequest request, final HttpServletResponse response) throws ServletException, IOException
	{
		PrintWriter writer = response.getWriter();
		try
		{
			runPost(request, writer);
			writer.flush();
		}
		catch (Exception e)
		{
			_Logger.warn("Servlet处理异常.", e);
		}
		finally
		{
			writer.close();
		}
	}

	protected abstract void runPost(final HttpServletRequest request, final PrintWriter writer);

	protected abstract void runGet(final HttpServletRequest request, final PrintWriter writer);

	protected abstract T getParams(final HttpServletRequest queryInfo) throws JsonParseException, JsonMappingException, IOException;

    protected abstract String getControllerName(final String servletPath);
}
