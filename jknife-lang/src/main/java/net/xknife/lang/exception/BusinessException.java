package net.xknife.lang.exception;

import net.xknife.lang.exception.base.AppException;

/**
 * 此类异常定义为实际应用层面的异常，属业务逻辑方面的异常。<BR>
 * 本异常类以及他的子类当被抛出时，应当是可预见的，也可以被调用方法检测到，并必须应当立即采取措施，而不能再向上抛出。 <BR>
 * <BR>
 * 
 * @author lukan@jeelu.com
 * 
 */
public class BusinessException extends AppException
{
	private static final long serialVersionUID = -9074619142382447421L;

	/**
	 * @param logMessage
	 *            记录日志的消息
	 */
	public BusinessException(final String logMessage)
	{
		super(logMessage);
	}

	/**
	 * @param logMessage
	 *            记录日志的消息
	 */
	public BusinessException(final String logMessage, final int errorNumber)
	{
		super(logMessage, errorNumber);
	}

	/**
	 * @param cause
	 *            引起异常的原始异常
	 * @param logMessage
	 *            记录日志的消息
	 */
	public BusinessException(final String logMessage, final Throwable cause)
	{
		super(logMessage, cause);
	}

	/**
	 * @param cause
	 *            引起异常的原始异常
	 */
	public BusinessException(final Throwable cause)
	{
		super(cause);
	}
}
