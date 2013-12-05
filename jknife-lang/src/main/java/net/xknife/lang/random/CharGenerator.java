package net.xknife.lang.random;

import net.xknife.lang.interfaces.IRandom;

public class CharGenerator implements IRandom<Character>
{
	private static final char[] src = "1234567890_ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".toCharArray();

	@Override
	public Character next()
	{
		return src[Math.abs(Randoms.getRandom().nextInt(src.length))];
	}
}
