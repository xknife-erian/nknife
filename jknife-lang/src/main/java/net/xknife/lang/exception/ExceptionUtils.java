package net.xknife.lang.exception;

import java.io.IOException;
import java.io.PrintWriter;
import java.io.StringWriter;

/**
 * Exception处理的工具类
 * 
 * @author lukan@jeelu.com
 * 
 */
public class ExceptionUtils
{
	public static final String defaultPrefixPackage = "net.xknife.lang";

	/**
	 * 得到最原始的异常
	 * 
	 * @param t
	 * @return
	 */
	public static Throwable getRootCause(Throwable t)
	{
		while (t.getCause() != null)
		{
			t = t.getCause();
		}
		return t;
	}

	/**
	 * 得到关注的堆栈信息
	 * 
	 * @param t
	 * @return
	 */
	public static String getCarefulStackTrace(final Throwable t)
	{
		return getCarefulStackTrace(t, defaultPrefixPackage);
	}

	/**
	 * 查找到我们需要注意的StackTrace，将抛弃我们不需要关心的堆栈信息。 具体采用递归的方式查询到我们必须关心的异常信息。
	 * 
	 * @param t
	 * @param packageStart
	 * @return
	 */
	public static String getCarefulStackTrace(final Throwable t, final String packageStart)
	{
		String causeString = null;
		if (t.getCause() != null)
		{
			causeString = getCarefulStackTrace(t.getCause(), packageStart);
		}
		// 1、先将异常类名和Message写出来；
		StringBuffer sb = new StringBuffer(t.getClass().getName());
		sb.append(":").append(t.getMessage()).append("\n");
		// 2、过滤需要关心的堆栈信息
		StackTraceElement[] traceElements = t.getStackTrace();
		boolean lastCareful = true;
		StackTraceElement traceElement;
		for (StackTraceElement traceElement2 : traceElements)
		{
			traceElement = traceElement2;
			if (traceElement.getClassName().startsWith(packageStart))
			{
				if (!lastCareful)
				{
					lastCareful = true;
				}
				sb.append("\tat ").append(traceElement).append("\n");
			}
			else
			{
				if (lastCareful)
				{
					lastCareful = false;
					sb.append("\t ......").append("\n");
				}
			}
		}
		if (causeString != null)
		{
			sb.append("Caused by: ").append(causeString);
		}
		return sb.toString();
	}

	/**
	 * 获得异常信息的堆栈
	 * 
	 * @param t
	 * @return
	 */
	public static String getExceptionStack(final Throwable t)
	{
		String exceptionStack = null;
		if (t != null)
		{
			StringWriter sw = new StringWriter();
			PrintWriter pw = new PrintWriter(sw);
			try
			{
				t.printStackTrace(pw);
				exceptionStack = sw.toString();
			}
			finally
			{
				try
				{
					sw.close();
				}
				catch (IOException e)
				{
				}
				pw.close();
			}
		}
		return exceptionStack;
	}

}
