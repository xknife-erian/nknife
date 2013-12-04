package net.xknife.library.widgets;

import java.util.Collection;
import java.util.Iterator;

/**
 * 为提高 StringBuilder 的可用性而针对性的封装。事实就是一个 StringBuilder。<br>
 * 类名的命名考虑使用频繁，故简化，未按正规命名规范。
 * 
 * @author lukan@jeelu.com
 */
public class Sber
{
	private final StringBuilder _Builder = new StringBuilder();

	private Sber()
	{

	}

	public static Sber ME()
	{
		Sber sber = new Sber();
		return sber;
	}

	public static Sber ME(final Object obj)
	{
		Sber sber = new Sber();
		sber._Builder.append(obj);
		return sber;
	}

	public static Sber ME(final Object obj, final String split)
	{
		Sber sber = new Sber();
		sber._Builder.append(obj);
		return sber;
	}

	public static Sber ME(final Object obj, final String split, final String wrapper)
	{
		Sber sber = new Sber();
		sber._Builder.append(obj);
		return sber;
	}

	public int length()
	{
		return _Builder.length();
	}

	public Sber insert(final int index, final Object obj)
	{
		_Builder.insert(index, obj);
		return this;
	}

	public Sber appendBlank()
	{
		_Builder.append(' ');
		return this;
	}

	public Sber append(final Object obj)
	{
		_Builder.append(obj);
		return this;
	}

	public Sber append(final String format, final Object... args)
	{
		_Builder.append(String.format(format, args));
		return this;
	}

	/**
	 * 用前后两个包围符包围指定的值，并添加。
	 * 
	 * @param left
	 * @param value
	 * @param right
	 * @return
	 */
	public Sber surround(final String left, final Object value, final String right)
	{
		_Builder.append(left).append(value).append(right);
		return this;
	}

	/**
	 * 用前后两个包围符包围指定的值，并添加。
	 * 
	 * @param left
	 * @param value
	 * @param right
	 * @return
	 */
	public Sber surround(final String left, final String right)
	{
		_Builder.insert(0, left).append(right);
		return this;
	}

	/**
	 * 用双引号包括一个对象
	 * 
	 * @param value
	 * @return
	 */
	public Sber surroundQuotation(final Object value)
	{
		this.surround("\"", value, "\"");
		return this;
	}

	/**
	 * @return 用中括号包围
	 */
	public Sber surroundBracket()
	{
		this.surround("[", "]");
		return this;
	}

	/**
	 * @return 用大括号包围
	 */
	public Sber surroundBrace()
	{
		this.surround("{", "}");
		return this;
	}

	/**
	 * @return 用圆括号包围
	 */
	public Sber surroundParentheses()
	{
		this.surround("(", ")");
		return this;
	}

	/**
	 * @return 移除包围，即左右各一个字符。
	 */
	public Sber trimSurround()
	{
		_Builder.deleteCharAt(0);
		_Builder.deleteCharAt(_Builder.length() - 1);
		return this;
	}

	/**
	 * @return 移除最后一个字符
	 */
	public Sber trimEnd()
	{
		if (_Builder.length() <= 0)
		{
			return this;
		}
		_Builder.deleteCharAt(_Builder.length() - 1);
		return this;
	}

	public Sber trimEnd(final int i)
	{
		if ((_Builder.length() <= 0) || (_Builder.length() < i))
		{
			return this;
		}
		_Builder.delete(_Builder.length() - i - 1, _Builder.length() - 1);
		return this;
	}

	public String end()
	{
		return this.end(1);
	}

	public String end(final int i)
	{
		return _Builder.substring(_Builder.length() - 1 - i);
	}

	public Sber replace(final int start, final int end, final String str)
	{
		_Builder.replace(start, end, str);
		return this;
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString()
	{
		return _Builder.toString();
	}

	/**
	 * 将一个数组的部分元素转换成字符串
	 * <p>
	 * 每个元素之间，都会用一个给定的字符分隔
	 * 
	 * @param offset
	 *            开始元素的下标
	 * @param len
	 *            元素数量
	 * @param c
	 *            分隔符
	 * @param objs
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final int offset, final int len, final Object c, final T[] objs)
	{
		StringBuilder sb = new StringBuilder();
		if ((null == objs) || (len < 0) || (0 == objs.length))
		{
			return sb;
		}

		if (offset < objs.length)
		{
			sb.append(objs[offset]);
			for (int i = 1; (i < len) && ((i + offset) < objs.length); i++)
			{
				sb.append(c).append(objs[i + offset]);
			}
		}
		return sb;
	}

	/**
	 * 将一个数组部分元素拼合成一个字符串
	 * 
	 * @param offset
	 *            开始元素的下标
	 * @param len
	 *            元素数量
	 * @param array
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final int offset, final int len, final T[] array)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < len; i++)
		{
			sb.append(array[i + offset].toString());
		}
		return sb;
	}

	/**
	 * 将一个集合转换成字符串
	 * <p>
	 * 每个元素之间，都会用一个给定的字符分隔
	 * 
	 * @param c
	 *            分隔符
	 * @param coll
	 *            集合
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final Object c, final Collection<T> coll)
	{
		StringBuilder sb = new StringBuilder();
		if ((null == coll) || coll.isEmpty())
		{
			return sb;
		}
		Iterator<T> it = coll.iterator();
		sb.append(it.next());
		while (it.hasNext())
		{
			sb.append(c).append(it.next());
		}
		return sb;
	}

	/**
	 * 将一个整型数组转换成字符串
	 * <p>
	 * 每个元素之间，都会用一个给定的字符分隔
	 * 
	 * @param c
	 *            分隔符
	 * @param vals
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static StringBuilder concat(final Object c, final int[] vals)
	{
		StringBuilder sb = new StringBuilder();
		if ((null == vals) || (0 == vals.length))
		{
			return sb;
		}

		sb.append(vals[0]);
		for (int i = 1; i < vals.length; i++)
		{
			sb.append(c).append(vals[i]);
		}

		return sb;
	}

	/**
	 * 将一个长整型数组转换成字符串
	 * <p>
	 * 每个元素之间，都会用一个给定的字符分隔
	 * 
	 * @param c
	 *            分隔符
	 * @param vals
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static StringBuilder concat(final Object c, final long[] vals)
	{
		StringBuilder sb = new StringBuilder();
		if ((null == vals) || (0 == vals.length))
		{
			return sb;
		}

		sb.append(vals[0]);
		for (int i = 1; i < vals.length; i++)
		{
			sb.append(c).append(vals[i]);
		}

		return sb;
	}

	/**
	 * 将一个数组转换成字符串
	 * <p>
	 * 每个元素之间，都会用一个给定的字符分隔
	 * 
	 * @param c
	 *            分隔符
	 * @param objs
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final Object c, final T[] objs)
	{
		StringBuilder sb = new StringBuilder();
		if ((null == objs) || (0 == objs.length))
		{
			return sb;
		}

		sb.append(objs[0]);
		for (int i = 1; i < objs.length; i++)
		{
			sb.append(c).append(objs[i]);
		}

		return sb;
	}

	/**
	 * 将一个数组所有元素拼合成一个字符串
	 * 
	 * @param objs
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final T[] objs)
	{
		StringBuilder sb = new StringBuilder();
		for (T e : objs)
		{
			sb.append(e.toString());
		}
		return sb;
	}

	/**
	 * 将一个数组所有元素拼合成一个字符串，用指定的连接字符串连接
	 * 
	 * @param objs
	 *            数组
	 * @param split
	 *            连接字符串
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final T[] objs, final String split)
	{
		StringBuilder sb = new StringBuilder();
		for (T e : objs)
		{
			if (sb.length() > 0)
			{
				sb.append(split);
			}
			sb.append(e.toString());
		}
		return sb;
	}

	/**
	 * 将一个数组所有元素拼合成一个字符串，用指定的连接字符串连接
	 * 
	 * @param objs
	 *            数组
	 * @param split
	 *            连接字符串
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final Collection<T> objs, final String split)
	{
		StringBuilder sb = new StringBuilder();
		for (T e : objs)
		{
			if (sb.length() > 0)
			{
				sb.append(split);
			}
			sb.append(e.toString());
		}
		return sb;
	}

	/**
	 * 将一个数组指定数量的元素拼合成一个字符串
	 * 
	 * @param objs
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final T[] objs, final int num)
	{
		StringBuilder sb = new StringBuilder();
		if (num > (objs.length - 1))
		{
			return sb;
		}
		for (int i = 0; i < num; i++)
		{
			sb.append(objs[i].toString());
		}
		return sb;
	}

	/**
	 * 将一个数组所有元素拼合成一个字符串，用指定的连接字符串连接
	 * 
	 * @param objs
	 *            数组
	 * @param split
	 *            连接字符串
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concat(final T[] objs, final int num, final String split)
	{
		StringBuilder sb = new StringBuilder();
		if (num > (objs.length - 1))
		{
			return sb;
		}
		for (int i = 0; i < num; i++)
		{
			if (sb.length() > 0)
			{
				sb.append(split);
			}
			sb.append(objs[i].toString());
		}
		return sb;
	}

	/**
	 * 将一个数组转换成字符串
	 * <p>
	 * 所有的元素都被格式化字符串包裹。 这个格式话字符串只能有一个占位符， %s, %d 等，均可，请视你的数组内容而定
	 * <p>
	 * 每个元素之间，都会用一个给定的字符分隔
	 * 
	 * @param ptn
	 *            格式
	 * @param c
	 *            分隔符
	 * @param objs
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concatBy(final String ptn, final Object c, final T[] objs)
	{
		StringBuilder sb = new StringBuilder();
		for (T obj : objs)
		{
			sb.append(String.format(ptn, obj)).append(c);
		}
		if (sb.length() > 0)
		{
			sb.deleteCharAt(sb.length() - 1);
		}
		return sb;
	}

	/**
	 * 将一个数组转换成字符串
	 * <p>
	 * 所有的元素都被格式化字符串包裹。 这个格式话字符串只能有一个占位符， %s, %d 等，均可，请视你的数组内容而定
	 * 
	 * @param fmt
	 *            格式
	 * @param objs
	 *            数组
	 * @return 拼合后的字符串
	 */
	public static <T> StringBuilder concatBy(final String fmt, final T[] objs)
	{
		StringBuilder sb = new StringBuilder();
		for (T obj : objs)
		{
			sb.append(String.format(fmt, obj));
		}
		return sb;
	}

	public static void main(final String[] args)
	{
	}
}
