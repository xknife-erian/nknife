package net.xknife.library.utilities;

import java.lang.reflect.Array;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Enumeration;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Queue;

import net.xknife.library.widgets.Sber;

/**
 * Java的一些不易归类的静态方法集。 这些帮助函数让 Java 的某些常用功能变得更简单。
 * 
 * @author lukan@jeelu.com
 */
public abstract class MiscUtil
{
	/**
	 * 将一个对象添加成为一个数组的第一个元素，从而生成一个新的数组
	 * 
	 * @param e
	 *            对象
	 * @param eles
	 *            数组
	 * @return 新数组
	 */
	@SuppressWarnings("unchecked")
	public static <T> T[] arrayFirst(final T e, final T[] eles)
	{
		try
		{
			if ((null == eles) || (eles.length == 0))
			{
				T[] arr = (T[]) Array.newInstance(e.getClass(), 1);
				arr[0] = e;
				return arr;
			}
			T[] arr = (T[]) Array.newInstance(eles.getClass().getComponentType(), eles.length + 1);
			arr[0] = e;
			for (int i = 0; i < eles.length; i++)
			{
				arr[i + 1] = eles[i];
			}
			return arr;
		}
		catch (NegativeArraySizeException e1)
		{
			throw MiscUtil.wrapThrow(e1);
		}
	}

	/**
	 * 将一个对象添加成为一个数组的最后一个元素，从而生成一个新的数组
	 * 
	 * @param e
	 *            对象
	 * @param eles
	 *            数组
	 * @return 新数组
	 */
	@SuppressWarnings("unchecked")
	public static <T> T[] arrayLast(final T[] eles, final T e)
	{
		try
		{
			if ((null == eles) || (eles.length == 0))
			{
				T[] arr = (T[]) Array.newInstance(e.getClass(), 1);
				arr[0] = e;
				return arr;
			}
			T[] arr = (T[]) Array.newInstance(eles.getClass().getComponentType(), eles.length + 1);
			for (int i = 0; i < eles.length; i++)
			{
				arr[i] = eles[i];
			}
			arr[eles.length] = e;
			return arr;
		}
		catch (NegativeArraySizeException e1)
		{
			throw MiscUtil.wrapThrow(e1);
		}
	}

	/**
	 * 将集合变成指定类型的数组
	 * 
	 * @param coll
	 *            集合对象
	 * @param eleType
	 *            数组元素类型
	 * @return 数组
	 */
	public static Object collection2array(final Collection<?> coll, final Class<?> eleType)
	{
		if (null == coll)
		{
			return null;
		}
		Object re = Array.newInstance(eleType, coll.size());
		int i = 0;
		for (Object name : coll)
		{
			Array.set(re, i++, name);
		}
		return re;
	}

	/**
	 * 将集合变成数组，数组的类型为集合的第一个元素的类型。如果集合为空，则返回 null
	 * 
	 * @param coll
	 *            集合对象
	 * @return 数组
	 */
	@SuppressWarnings("unchecked")
	public static <E> Object collection2array(final Collection<E> coll)
	{
		if ((null == coll) || (coll.size() == 0))
		{
			return null;
		}
		Class<E> eleType = (Class<E>) MiscUtil.first(coll).getClass();
		return collection2array(coll, eleType);
	}

	/**
	 * 将集合变成 ArrayList
	 * 
	 * @param coll
	 *            集合对象
	 * @return 列表对象
	 */
	public static <E> List<E> collection2list(final Collection<E> coll)
	{
		return collection2list(coll, null);
	}

	/**
	 * 将集合编程变成指定类型的列表
	 * 
	 * @param coll
	 *            集合对象
	 * @param classOfList
	 *            列表类型
	 * @return 列表对象
	 */
	public static <E> List<E> collection2list(final Collection<E> coll, final Class<List<E>> classOfList)
	{
		if (coll instanceof List<?>)
		{
			return (List<E>) coll;
		}
		List<E> list;
		try
		{
			list = (null == classOfList ? new ArrayList<E>(coll.size()) : classOfList.newInstance());
		}
		catch (Exception e)
		{
			throw MiscUtil.wrapThrow(e);
		}
		for (E e : coll)
		{
			list.add(e);
		}
		return list;
	}

	/**
	 * 判断一个数组内是否包括某一个对象。 它的比较将通过 equals(Object,Object) 方法
	 * 
	 * @param array
	 *            数组
	 * @param ele
	 *            对象
	 * @return true 包含 false 不包含
	 */
	public static <T> boolean contains(final T[] array, final T ele)
	{
		if (null == array)
		{
			return false;
		}
		for (T e : array)
		{
			if (equals(e, ele))
			{
				return true;
			}
		}
		return false;
	}

	/**
	 * 返回一个集合对象的枚举对象。实际上就是对 Iterator 接口的一个封装
	 * 
	 * @param col
	 *            集合对象
	 * @return 枚举对象
	 */
	public static <T> Enumeration<T> enumeration(final Collection<T> col)
	{
		final Iterator<T> it = col.iterator();
		return new Enumeration<T>()
		{

			public boolean hasMoreElements()
			{
				return it.hasNext();
			}

			public T nextElement()
			{
				return it.next();
			}
		};
	}

	/**
	 * 判断两个对象是否相等。 这个函数用处是:
	 * <ul>
	 * <li>可以容忍 null
	 * <li>可以容忍不同类型的 Number
	 * <li>对数组，集合， Map 会深层比较
	 * </ul>
	 * 当然，如果你重写的 equals 方法会优先
	 * 
	 * @param a1
	 *            比较对象1
	 * @param a2
	 *            比较对象2
	 * @return 是否相等
	 */
	@SuppressWarnings("unchecked")
	public static boolean equals(final Object a1, final Object a2)
	{
		if (a1 == a2)
		{
			return true;
		}
		if ((a1 == null) || (a2 == null))
		{
			return false;
		}
		if (a1.equals(a2))
		{
			return true;
		}
		if (a1 instanceof Number)
		{
			return (a2 instanceof Number) && a1.toString().equals(a2.toString());
		}
		if ((a1 instanceof Map) && (a2 instanceof Map))
		{
			Map<?, ?> m1 = (Map<?, ?>) a1;
			Map<?, ?> m2 = (Map<?, ?>) a2;
			if (m1.size() != m2.size())
			{
				return false;
			}
			for (Entry<?, ?> e : m1.entrySet())
			{
				Object key = e.getKey();
				if (!m2.containsKey(key) || !equals(m1.get(key), m2.get(key)))
				{
					return false;
				}
			}
			return true;
		}
		else if (a1.getClass().isArray())
		{
			if (a2.getClass().isArray())
			{
				int len = Array.getLength(a1);
				if (len != Array.getLength(a2))
				{
					return false;
				}
				for (int i = 0; i < len; i++)
				{
					if (!equals(Array.get(a1, i), Array.get(a2, i)))
					{
						return false;
					}
				}
				return true;
			}
			else if (a2 instanceof List)
			{
				return equals(a1, MiscUtil.collection2array((List<Object>) a2, Object.class));
			}
			return false;
		}
		else if (a1 instanceof List)
		{
			if (a2 instanceof List)
			{
				List<?> l1 = (List<?>) a1;
				List<?> l2 = (List<?>) a2;
				if (l1.size() != l2.size())
				{
					return false;
				}
				int i = 0;
				for (Object name : l1)
				{
					if (!equals(name, l2.get(i++)))
					{
						return false;
					}
				}
				return true;
			}
			else if (a2.getClass().isArray())
			{
				return equals(MiscUtil.collection2array((List<Object>) a1, Object.class), a2);
			}
			return false;
		}
		else if ((a1 instanceof Collection) && (a2 instanceof Collection))
		{
			Collection<?> c1 = (Collection<?>) a1;
			Collection<?> c2 = (Collection<?>) a2;
			if (c1.size() != c2.size())
			{
				return false;
			}
			return c1.containsAll(c2) && c2.containsAll(c1);
		}
		return false;
	}

	/**
	 * 获取集合中的第一个元素，如果集合为空，返回 null
	 * 
	 * @param coll
	 *            集合
	 * @return 第一个元素
	 */
	public static <T> T first(final Collection<T> coll)
	{
		if ((null == coll) || coll.isEmpty())
		{
			return null;
		}
		return coll.iterator().next();
	}

	/**
	 * 获得表中的第一个名值对
	 * 
	 * @param map
	 *            表
	 * @return 第一个名值对
	 */
	public static <K, V> Entry<K, V> first(final Map<K, V> map)
	{
		if ((null == map) || map.isEmpty())
		{
			return null;
		}
		return map.entrySet().iterator().next();
	}

	/**
	 * 判断当前系统是否为Windows
	 * 
	 * @return true 如果当前系统为Windows系统
	 */
	public static boolean isWin()
	{
		try
		{
			String os = System.getenv("OS");
			return (os != null) && (os.indexOf("Windows") > -1);
		}
		catch (Throwable e)
		{
			return false;
		}
	}

	/**
	 * 将多个数组，合并成一个数组。如果这些数组为空，则返回 null
	 * 
	 * @param arys
	 *            数组对象
	 * @return 合并后的数组对象
	 */
	@SuppressWarnings("unchecked")
	public static <T> T[] merge(final T[]... arys)
	{
		Queue<T> list = new LinkedList<T>();
		for (T[] ary : arys)
		{
			if (null != ary)
			{
				for (T e : ary)
				{
					if (null != e)
					{
						list.add(e);
					}
				}
			}
		}
		if (list.isEmpty())
		{
			return null;
		}
		Class<T> type = (Class<T>) list.peek().getClass();
		return list.toArray((T[]) Array.newInstance(type, list.size()));
	}

	/**
	 * Throw an IllegalArgumentException if the argument is null, otherwise just return the argument. Useful for assignment as in this.thing =
	 * Utils.notNull(thing);
	 * 
	 * @param <T>
	 *            The type of the thing
	 * @param t
	 *            The thing to check for nullness.
	 */
	public static <T> T notNull(final T t)
	{
		if (t == null)
		{
			throw new IllegalArgumentException("This object MUST be non-null.");
		}
		return t;
	}

	/**
	 * Throw an IllegalArgumentException if the argument is null, otherwise just return the argument. Useful for assignment as in this.thing =
	 * Utils.notNull(thing);
	 * 
	 * @param <T>
	 *            The type of the thing
	 * @param t
	 *            The thing to check for nullness.
	 * @param message
	 *            The message to put in the exception if it is null
	 */
	public static <T> T notNull(final T t, final String message)
	{
		if (t == null)
		{
			throw new IllegalArgumentException(message);
		}
		return t;
	}

	/**
	 * 将字符串解析成 boolean 值，支持更多的字符串
	 * <ul>
	 * <li>1 | 0
	 * <li>yes | no
	 * <li>on | off
	 * <li>true | false
	 * </ul>
	 * 
	 * @param s
	 * @return 布尔值
	 */
	public static boolean parseBoolean(String s)
	{
		if ((null == s) || (s.length() == 0))
		{
			return false;
		}
		if (s.length() > 5)
		{
			return true;
		}
		if ("0".equals(s))
		{
			return false;
		}
		s = s.toLowerCase();
		return !"false".equals(s) && !"off".equals(s) && !"no".equals(s);
	}

	/**
	 * 测试一个对象是否 == Null。
	 * 
	 * @param owner
	 *            被测试对象拥有者，一般在实例方法中，直接用“this”即可。
	 * @param row
	 *            代码所在行号,便于查找。
	 * @param testObject
	 *            被测试的对象
	 */
	public static void testIsNull(final Object owner, final int row, final Object testObject)
	{
		Sber sber = Sber.ME();
		sber.append(">>> 测试,代码约在").append(row).append("行。在:").append(owner.getClass().getSimpleName()).append("类中。经测试,目标:");
		if (null != testObject)
		{
			sber.append("不为Null。-->> ").append(testObject.getClass().getSimpleName());
		}
		else
		{
			sber.append("为Null。");
		}
		System.out.println(sber.toString());
	}

	/**
	 * 将字符数组强制转换成字节数组。如果字符为双字节编码，则会丢失信息
	 * 
	 * @param cs
	 *            字符数组
	 * @return 字节数组
	 */
	public static byte[] toBytes(final char[] cs)
	{
		byte[] bs = new byte[cs.length];
		for (int i = 0; i < cs.length; i++)
		{
			bs[i] = (byte) cs[i];
		}
		return bs;
	}

	/**
	 * 将整数数组强制转换成字节数组。整数的高位将会被丢失
	 * 
	 * @param is
	 *            整数数组
	 * @return 字节数组
	 */
	public static byte[] toBytes(final int[] is)
	{
		byte[] bs = new byte[is.length];
		for (int i = 0; i < is.length; i++)
		{
			bs[i] = (byte) is[i];
		}
		return bs;
	}

	public static Throwable unwrapThrow(final Throwable e)
	{
		if (e == null)
		{
			return null;
		}
		if (e instanceof InvocationTargetException)
		{
			InvocationTargetException itE = (InvocationTargetException) e;
			if (itE.getTargetException() != null)
			{
				return unwrapThrow(itE.getTargetException());
			}
		}
		if (e.getCause() != null)
		{
			return unwrapThrow(e.getCause());
		}
		return e;
	}

	/**
	 * 用运行时异常包裹抛出对象，如果抛出对象本身就是运行时异常，则直接返回。
	 * <p>
	 * 如果是 InvocationTargetException，那么将其剥离，只包裹其 TargetException
	 * 
	 * @param e
	 *            抛出对象
	 * @return 运行时异常
	 */
	public static RuntimeException wrapThrow(final Throwable e)
	{
		if (e instanceof RuntimeException)
		{
			return (RuntimeException) e;
		}
		if (e instanceof InvocationTargetException)
		{
			return wrapThrow(((InvocationTargetException) e).getTargetException());
		}
		return new RuntimeException(e);
	}

	/**
	 * 将抛出对象包裹成运行时异常，并增加自己的描述
	 * 
	 * @param e
	 *            抛出对象
	 * @param fmt
	 *            格式
	 * @param args
	 *            参数
	 * @return 运行时异常
	 */
	public static RuntimeException wrapThrow(final Throwable e, final String fmt, final Object... args)
	{
		return new RuntimeException(String.format(fmt, args), e);
	}

	/**
	 * 对Thread.sleep(long)的简单封装,不抛出任何异常
	 * 
	 * @param millisecond
	 *            休眠时间
	 */
	public static void sleep(final long millisecond)
	{
		try
		{
			if (millisecond > 0)
			{
				Thread.sleep(millisecond);
			}
		}
		catch (Throwable e)
		{
		}
	}

	/**
	 * 输入的字符串是否是一个数字
	 * 
	 * @param number
	 * @return
	 */
	public static boolean isNumber(final String number)
	{
		try
		{
			Float.parseFloat(number);
		}
		catch (Exception e)
		{
			return false;
		}
		return true;
	}

	public final static char[] UUIDChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'r' };
}
