package net.xknife.lang.random;

import java.util.Random;

class Randoms
{
	private static Random _Random = new Random(System.currentTimeMillis());

	public static Random getRandom()
	{
		return _Random;
	}

	public static int random(final int min, final int max)
	{
		return _Random.nextInt((max - min) + 1) + min;
	}
}
