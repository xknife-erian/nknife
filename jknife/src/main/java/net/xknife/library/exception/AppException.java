package net.xknife.library.exception;

/**
 * 本框架应用异常树的根，系统内一般异常的基类。<BR>
 * 在没有特殊情况下，系统内所有创建的异常都是用此异常类。<BR>
 * 在该类之下派生两个类型的异常：<BR>
 * 1. BusinessException <BR>
 * 2. TechnicalException
 */
@SuppressWarnings("serial")
public abstract class AppException extends Exception
{
	/**
	 * @param message
	 *            记录日志的消息
	 */
	public AppException(final String message)
	{
		super(message);
	}

	/**
	 * @param message
	 *            记录日志的消息
	 */
	public AppException(final String message, final int error)
	{
		super(message);
		_ErrotNumber = error;
	}

	/**
	 * @param cause
	 *            引起异常的原始异常
	 * @param message
	 *            记录日志的消息
	 */
	public AppException(final String message, final Throwable cause)
	{
		super(message, cause);
	}

	/**
	 * @param cause
	 *            引起异常的原始异常
	 */
	public AppException(final Throwable cause)
	{
		super(cause);
	}

	protected int _ErrotNumber;

	/**
	 * @return errotNumber
	 */
	public int getErrotNumber()
	{
		return _ErrotNumber;
	}

	/**
	 * @param errotNumber
	 *            要设置的 errotNumber
	 */
	public void setErrotNumber(final int errotNumber)
	{
		_ErrotNumber = errotNumber;
	}
}
