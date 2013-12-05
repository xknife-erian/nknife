package net.xknife.lang.random;

import net.xknife.lang.interfaces.IRandom;

/**
 * 根据一个数组随机产生对象，每个对象只会被取出一次。 当数组耗尽，则一直返回 null
 * 
 */
public class ArrayRandom<T> implements IRandom<T>
{
	private final T[] array;
	private int len;

	public ArrayRandom(final T[] array)
	{
		this.array = array;
		this.len = array.length;
	}

	@Override
	public T next()
	{
		if (this.len <= 0)
		{
			return null;
		}
		if (this.len == 1)
		{
			return this.array[--this.len];
		}
		int index = Randoms.getRandom().nextInt(this.len);
		if (index == (this.len - 1))
		{
			return this.array[--this.len];
		}
		T c = this.array[index];
		this.array[index] = this.array[--this.len];
		return c;
	}

}
