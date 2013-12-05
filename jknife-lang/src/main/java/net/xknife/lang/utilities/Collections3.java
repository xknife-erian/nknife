package net.xknife.lang.utilities;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
// import org.apache.commons.beanutils.PropertyUtils;
import org.apache.commons.lang3.StringUtils;

/**
 * Collections工具集.
 * 
 * 在JDK的Collections和Guava的Collections2后, 命名为Collections3.
 * 
 * 函数主要由两部分组成，一是自反射提取元素的功能，二是源自Apache Commons Collection, 争取不用在项目里引入它。
 * 
 */
<<<<<<< HEAD
public class Collections3
{
=======
public class Collections3 {

	/**
	 * 提取集合中的对象的两个属性(通过Getter函数), 组合成Map.
	 * 
	 * @param collection 来源集合.
	 * @param keyPropertyName 要提取为Map中的Key值的属性名.
	 * @param valuePropertyName 要提取为Map中的Value值的属性名.
	 */
	// public static Map extractToMap(final Collection collection, final String keyPropertyName,
	// final String valuePropertyName) {
	// Map map = new HashMap(collection.size());
	//
	// try {
	// for (Object obj : collection) {
	// map.put(PropertyUtils.getProperty(obj, keyPropertyName),
	// PropertyUtils.getProperty(obj, valuePropertyName));
	// }
	// } catch (Exception e) {
	// throw Reflections.convertReflectionExceptionToUnchecked(e);
	// }
	//
	// return map;
	// }

	/**
	 * 提取集合中的对象的一个属性(通过Getter函数), 组合成List.
	 * 
	 * @param collection 来源集合.
	 * @param propertyName 要提取的属性名.
	 */
	// public static List extractToList(final Collection collection, final String propertyName) {
	// List list = new ArrayList(collection.size());
	//
	// try {
	// for (Object obj : collection) {
	// list.add(PropertyUtils.getProperty(obj, propertyName));
	// }
	// } catch (Exception e) {
	// throw Reflections.convertReflectionExceptionToUnchecked(e);
	// }
	//
	// return list;
	// }

	/**
	 * 提取集合中的对象的一个属性(通过Getter函数), 组合成由分割符分隔的字符串.
	 * 
	 * @param collection 来源集合.
	 * @param propertyName 要提取的属性名.
	 * @param separator 分隔符.
	 */
	// public static <T> String extractToString(final Collection<T> collection, final String propertyName, final String separator)
	// {
	// @SuppressWarnings("rawtypes")
	// List list = extractToList(collection, propertyName);
	// return StringUtils.join(list, separator);
	// }
>>>>>>> 51e27eee47256de0ec3adc8187bc6339e87d5728

	/**
	 * 转换Collection所有元素(通过toString())为String, 中间以 separator分隔。
	 */
<<<<<<< HEAD
	public static String convertToString(final Collection<?> collection, final String separator)
=======
	public static <T> String convertToString(final Collection<T> collection, final String separator)
>>>>>>> 51e27eee47256de0ec3adc8187bc6339e87d5728
	{
		return StringUtils.join(collection, separator);
	}

	/**
	 * 转换Collection所有元素(通过toString())为String, 每个元素的前面加入prefix，后面加入postfix，如<div>mymessage</div>。
	 */
<<<<<<< HEAD
	public static String convertToString(final Collection<?> collection, final String prefix, final String postfix)
=======
	public static <T> String convertToString(final Collection<T> collection, final String prefix, final String postfix)
>>>>>>> 51e27eee47256de0ec3adc8187bc6339e87d5728
	{
		StringBuilder builder = new StringBuilder();
		for (Object o : collection)
		{
			builder.append(prefix).append(o).append(postfix);
		}
		return builder.toString();
	}

	/**
	 * 判断是否为空.
	 */
<<<<<<< HEAD
	public static boolean isEmpty(final Collection<?> collection)
=======
	public static <T> boolean isEmpty(final Collection<T> collection)
>>>>>>> 51e27eee47256de0ec3adc8187bc6339e87d5728
	{
		return ((collection == null) || collection.isEmpty());
	}

	/**
	 * 判断是否为空.
	 */
<<<<<<< HEAD
	public static boolean isNotEmpty(final Collection<?> collection)
=======
	public static <T> boolean isNotEmpty(final Collection<T> collection)
>>>>>>> 51e27eee47256de0ec3adc8187bc6339e87d5728
	{
		return ((collection != null) && !(collection.isEmpty()));
	}

	/**
	 * 取得Collection的第一个元素，如果collection为空返回null.
	 */
	public static <T> T getFirst(final Collection<T> collection)
	{
		if (isEmpty(collection))
		{
			return null;
		}

		return collection.iterator().next();
	}

	/**
	 * 获取Collection的最后一个元素 ，如果collection为空返回null.
	 */
	public static <T> T getLast(final Collection<T> collection)
	{
		if (isEmpty(collection))
		{
			return null;
		}

		// 当类型为List时，直接取得最后一个元素 。
		if (collection instanceof List)
		{
			List<T> list = (List<T>) collection;
			return list.get(list.size() - 1);
		}

		// 其他类型通过iterator滚动到最后一个元素.
		Iterator<T> iterator = collection.iterator();
		while (true)
		{
			T current = iterator.next();
			if (!iterator.hasNext())
			{
				return current;
			}
		}
	}

	/**
	 * 返回a+b的新List.
	 */
	public static <T> List<T> union(final Collection<T> a, final Collection<T> b)
	{
		List<T> result = new ArrayList<T>(a);
		result.addAll(b);
		return result;
	}

	/**
	 * 返回a-b的新List.
	 */
	public static <T> List<T> subtract(final Collection<T> a, final Collection<T> b)
	{
		List<T> list = new ArrayList<T>(a);
		for (T element : b)
		{
			list.remove(element);
		}

		return list;
	}

	/**
	 * 返回a与b的交集的新List.
	 */
	public static <T> List<T> intersection(final Collection<T> a, final Collection<T> b)
	{
		List<T> list = new ArrayList<T>();

		for (T element : a)
		{
			if (b.contains(element))
			{
				list.add(element);
			}
		}
		return list;
	}
}
