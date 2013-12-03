package net.xknife.library.random;

/**
 */
public class StringGenerator implements IRandom<String>
{
	private static CharGenerator _CharGenerator;

	/**
	 * @param max
	 *            必须大于0
	 */
	public StringGenerator(final int max)
	{
		_Max = max;
		_Min = 1;
	}

	/**
	 * 
	 * @param min
	 *            必须大于0
	 * @param max
	 *            必须不小于min
	 */
	public StringGenerator(final int min, final int max)
	{
		_Max = max;
		_Min = min;
	}

	private int _Max;

	private int _Min;

	/**
	 * 
	 * @param min
	 *            必须大于0
	 * @param max
	 *            必须不小于min
	 */
	public void setup(final int max, final int min)
	{
		_Max = max;
		_Min = min;
	}

	/**
	 * 根据设置的max和min的长度,生成随机字符串.
	 * <p/>
	 * 若max或min小于0,则返回null
	 * 
	 * @return 生成的字符串
	 */
	@Override
	public String next()
	{
		if ((_Max <= 0) || (_Min <= 0))
		{
			return null;
		}
		if (_CharGenerator == null)
		{
			_CharGenerator = new CharGenerator();
		}
		char[] buf = new char[Randoms.random(_Min, _Max)];
		for (int i = 0; i < buf.length; i++)
		{
			buf[i] = _CharGenerator.next();
		}
		return new String(buf);
	}

}
