package net.xknife.web.utilities;

import javax.servlet.ServletContext;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * 管理一个客户端请求的生命周期
 */
public interface BeatContext
{
	/**
	 * MVC 中的Model, 以key,value形式存放，可以由Controller传个View
	 * 
	 * @return 当前model
	 */
	Model getModel();

	/**
	 * 返回本次调用的 {@link HttpServletRequest}对象
	 * 
	 * @return 当前请求
	 */
	HttpServletRequest getRequest();

	/**
	 * 返回本次调用的 {@link HttpServletResponse}对象
	 * 
	 * @return 当前response
	 */
	HttpServletResponse getResponse();

	/**
	 * 得到ServletContext信息
	 * 
	 * @return 当前ServletContext
	 */
	ServletContext getServletContext();

	/**
	 * 获得客户端的信息
	 * 
	 * @return 客户端信息
	 */
	ClientContext getClient();
}