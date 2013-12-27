package net.xknife.lang.widgets;

import java.io.IOException;
import java.lang.annotation.Annotation;
import java.lang.reflect.Modifier;
import java.util.List;

import org.apache.commons.lang3.ClassUtils;

import com.google.common.base.Strings;
import com.google.common.collect.ImmutableSet;
import com.google.common.collect.Lists;
import com.google.common.reflect.ClassPath;
import com.google.common.reflect.ClassPath.ClassInfo;

/**
 * 从类路径中查找符合指定条件的类型
 * 
 * @author lukan@p-an.com 2013年11月14日
 */
public class ClassFinder
{
	private static ImmutableSet<ClassInfo> _ClassinfoSet;

	static
	{
		ClassPath classPath = null;
		try
		{
			classPath = ClassPath.from(Thread.currentThread().getContextClassLoader());
		}
		catch (IOException e)
		{
		}
		_ClassinfoSet = classPath.getTopLevelClasses();
	}

	/**
	 * 查找指定条件的类型集合
	 * 
	 * @param base
	 *            待查找类型的基类（超类或接口）
	 * @param annotation
	 *            类型上修饰的注解
	 * @param containAbstract
	 *            是否包含抽象类
	 * @param nameFilter
	 *            名称的过滤（包含的字符串）
	 * @return 符合指定条件的类型
	 */
	public static <T> List<Class<T>> find(final Class<T> base, final Class<? extends Annotation> annotation, final boolean containAbstract, final String nameFilter)
	{
		if ((base == null) && (annotation == null))
		{
			return null;
		}
		List<Class<T>> clazzList = Lists.newArrayList();
		for (ClassInfo classInfo : _ClassinfoSet)
		{
			String classInfoName = classInfo.getName();
			if (!Strings.isNullOrEmpty(nameFilter))
			{
				if (!classInfoName.contains(nameFilter))
				{
					continue;
				}
			}
			Class<?> clazz = classInfo.load();
			boolean isDecorate = true;
			if (annotation != null)
			{
				isDecorate = clazz.isAnnotationPresent(annotation);
			}
			if (isDecorate)
			{
				boolean isBase = true;
				if (base != null)
				{
					isBase = ClassUtils.isAssignable(clazz, base);
				}
				if (isBase)
				{
					if (!containAbstract)
					{
						if (Modifier.isAbstract(clazz.getModifiers()))
						{
							continue;
						}
					}
					@SuppressWarnings("unchecked")
					Class<T> clazzResult = (Class<T>) clazz;
					clazzList.add(clazzResult);
				}
			}
		}
		return clazzList;
	}
}
