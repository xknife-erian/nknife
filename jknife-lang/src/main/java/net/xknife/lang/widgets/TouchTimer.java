package net.xknife.lang.widgets;

import java.util.concurrent.Executor;
import java.util.concurrent.atomic.AtomicBoolean;

/**
 * 
 * 一个不需要定时线程的定时器，减少线程量
 * 
 * @author renjun
 */
public class TouchTimer
{
	private final long interval;
	private final Runnable run;
	private final Executor executor;

	private volatile long lastTime = 0;
	private final AtomicBoolean isRun = new AtomicBoolean(false);

	public static TouchTimer build(final long interval, final Runnable run, final Executor executor)
	{
		return new TouchTimer(interval, run, executor);
	}

	public TouchTimer(final long interval, final Runnable run, final Executor executor)
	{
		this.interval = interval;
		this.run = run;
		this.executor = executor;
	}

	public void touch()
	{
		long time = System.currentTimeMillis();
		if (isRun.get())
		{
			return;
		}
		if ((time - lastTime) < interval)
		{
			return;
		}
		execute();
		lastTime = time;
	}

	public void execute()
	{
		if (!isRun.compareAndSet(false, true))
		{
			return;
		}
		executor.execute(new Runnable()
		{
			public void run()
			{
				immediateRun();
			}
		});
	}

	public void immediateRun()
	{
		try
		{
			if (isRun.get())
			{
				return;
			}
			executor.execute(run);
		}
		finally
		{
			lastTime = System.currentTimeMillis();
			isRun.set(false);
		}
	}
}
