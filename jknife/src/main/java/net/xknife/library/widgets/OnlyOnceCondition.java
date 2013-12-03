package net.xknife.library.widgets;

import java.util.concurrent.atomic.AtomicBoolean;

import net.xknife.library.exception.VTechnicalException;

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

	public void check() throws VTechnicalException
	{
		if (!hasChecked.compareAndSet(false, true))
		{
			throw new VTechnicalException(message);
		}
	}

}
