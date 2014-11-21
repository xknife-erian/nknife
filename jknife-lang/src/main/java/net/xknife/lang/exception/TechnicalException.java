package net.xknife.lang.exception;

import net.xknife.lang.exception.base.AppException;

/**
 * 技术级异常。
 * 
 * @author lukan@jeelu.com
 * 
 */
public class TechnicalException extends AppException
{
	private static final long serialVersionUID = -4476926113138034878L;

	/**
	 * @param message
	 *            记录日志的消息
	 */
	public TechnicalException(final String message)
	{
		super("\r技术问题。" + message);
	}

	/**
	 * @param logMessage
	 *            记录日志的消息
	 */
	public TechnicalException(final String logMessage, final int errorNumber)
	{
		super(logMessage, errorNumber);
	}

	/**
	 * @param cause
	 *            引起异常的原始异常
	 * @param message
	 *            记录日志的消息
	 */
	public TechnicalException(final String message, final Throwable cause)
	{
		super("技术问题。" + message, cause);
	}

	/**
	 * @param cause
	 *            引起异常的原始异常
	 */
	public TechnicalException(final Throwable cause)
	{
		super("技术问题。", cause);
	}

}
