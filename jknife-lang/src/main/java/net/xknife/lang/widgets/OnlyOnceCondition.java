package net.xknife.lang.widgets;

import java.util.concurrent.atomic.AtomicBoolean;

import net.xknife.lang.exception.TechnicalException;

/**
 * 保证只执行一次
 * 
 * @author renjun
 */
public class OnlyOnceCondition
{
	public static OnlyOnceCondition create(final String message)
	{
		return new OnlyOnceCondition(message);
	}

	private final String message;

	private OnlyOnceCondition(final String message)
	{
		this.message = message;
	}

	private final AtomicBoolean hasChecked = new AtomicBoolean(false);

	public void check() throws TechnicalException
	{
		if (!hasChecked.compareAndSet(false, true))
		{
			throw new TechnicalException(message);
		}
	}

}
