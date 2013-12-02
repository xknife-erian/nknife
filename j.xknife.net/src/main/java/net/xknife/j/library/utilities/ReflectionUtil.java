package net.xknife.j.library.utilities;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.Method;
import java.net.URL;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.Vector;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

import com.google.common.collect.Lists;

/**
 * 有关反射操作的一些助手类
 * 
 * @author lukan@p-an.com 2013年11月3日
 */
public class ReflectionUtil
{
	private static Class<?> klass = null;

	/**
	 * 给一个接口，返回这个接口的所有实现类
	 * 
	 * @param className
	 * @param jarPath
	 * @return
	 * @throws IOException
	 * @throws ClassNotFoundException
	 */
	public static List<Class<?>> getAllClassByInterfaceFromPackage(final String className, final String jarPath) throws ClassNotFoundException, IOException
	{
		List<Class<?>> returnClassList = new ArrayList<Class<?>>(); // 返回结果
		// 如果不是一个接口，则不做处理
		List<Class<?>> allClass = getPackageClasses(jarPath, className); // 获得当前包下以及子包下的所有类
		if (null != klass)
		{
			// 判断是否是同一个接口
			for (Class<?> c : allClass)
			{
				if (klass.isAssignableFrom(c))
				{
					if (!klass.equals(c))
					{
						returnClassList.add(c);
					}
				}
			}
		}
		return returnClassList;
	}

	/**
	 * 给一个接口，返回这个接口的所有实现类
	 * 
	 * @param className
	 * @param jarPath
	 * @return
	 * @throws IOException
	 * @throws ClassNotFoundException
	 */
	public static List<Class<?>> getAllClassByInterfaceFromJar(final String className, final String jarPath) throws ClassNotFoundException, IOException
	{
		List<Class<?>> returnClassList = new ArrayList<Class<?>>(); // 返回结果
		List<Class<?>> allClass = getJarClasses(jarPath, className); // 获得当前包下以及子包下的所有类
		if (null != klass)
		{
			// 判断是否是同一个接口
			for (int i = 0; i < allClass.size(); i++)
			{
				if (klass.isAssignableFrom(allClass.get(i)))
				{
					if (!klass.equals(allClass.get(i)))
					{
						returnClassList.add(allClass.get(i));
					}
				}
			}
		}
		return returnClassList;
	}

	/**
	 * 获得包中所有类，必须在类的搜索路径中
	 * 
	 * @param packageName
	 *            包名称
	 * @param className
	 *            要查找的类名称
	 * @return
	 * @throws ClassNotFoundException
	 * @throws IOException
	 */
	private static List<Class<?>> getPackageClasses(final String packageName, final String className) throws ClassNotFoundException, IOException
	{
		ClassLoader classLoader = Thread.currentThread().getContextClassLoader();
		String path = packageName.replace('.', '/');
		Enumeration<URL> resources = classLoader.getResources(path);
		List<File> dirs = new ArrayList<File>();
		while (resources.hasMoreElements())
		{
			URL resource = resources.nextElement();
			dirs.add(new File(resource.getFile()));
		}
		ArrayList<Class<?>> classes = new ArrayList<Class<?>>();
		for (File directory : dirs)
		{
			classes.addAll(findClasses(directory, packageName, className));
		}
		return classes;
	}

	/**
	 * 查找包中的类 ，此时包名必须在类的搜索路径中，否则会抛出ClasseNotFoundException异常。
	 * 
	 * @param directory
	 *            包转转化后的file对象
	 * @param packageName
	 *            包名称
	 * @param className
	 *            类名称
	 * @return
	 * @throws ClassNotFoundException
	 */
	private static List<Class<?>> findClasses(final File directory, final String packageName, final String className) throws ClassNotFoundException
	{
		List<Class<?>> classes = Lists.newArrayList();
		if (!directory.exists())
		{
			return classes;
		}
		File[] files = directory.listFiles();
		for (File file : files)
		{
			if (file.isDirectory())
			{
				classes.addAll(findClasses(file, packageName + "." + file.getName(), className));// 递归
			}
			else if (file.getName().endsWith(".class"))
			{
				Class<?> tempObj = Class.forName(packageName + '.' + file.getName().substring(0, file.getName().length() - 6));
				if (file.getName().equals(className + ".class"))
				{
					klass = tempObj;
				}
				classes.add(tempObj);
			}
		}
		return classes;
	}

	/**
	 * 加载jar中的类文件
	 * 
	 * @param jarPath
	 *            jar的路径，可以不在类的搜索路径中
	 * @param className
	 *            类名称
	 * @return
	 * @throws ClassNotFoundException
	 * @throws IOException
	 */
	private static List<Class<?>> getJarClasses(final String jarPath, final String className) throws ClassNotFoundException, IOException
	{
		List<Class<?>> result = new Vector<Class<?>>();
		@SuppressWarnings("resource")
		JarFile jarFile = new JarFile(jarPath);
		Enumeration<?> files = jarFile.entries();
		while (files.hasMoreElements())
		{
			JarEntry entry = (JarEntry) files.nextElement();
			String entryName = entry.getName();
			if (entryName.endsWith(".class"))
			{
				String classNamePackage = entryName.substring(0, entryName.lastIndexOf(".")).replaceAll("/", ".");
				Class<?> c = Class.forName(classNamePackage);
				result.add(c);
				if (entryName.substring(entryName.lastIndexOf(File.separator) + 1).equals(className + ".class"))
				{
					klass = c;
				}
			}
		}
		return result;
	}

	/**
	 * 利用递归找一个类的指定方法，如果找不到，去父亲里面找直到最上层Object对象为止。
	 * 
	 * @param clazz
	 *            目标类
	 * @param methodName
	 *            方法名
	 * @param classes
	 *            方法参数类型数组
	 * @return 方法对象
	 * @throws Exception
	 */
	public static Method getPrivateMethod(final Class<?> clazz, final String methodName, final Class<?>... classes)
	{
		Method method = null;
		try
		{
			method = clazz.getDeclaredMethod(methodName, classes);
		}
		catch (Exception e1)
		{
			try
			{
				method = clazz.getMethod(methodName, classes);
			}
			catch (Exception e2)
			{
				if (clazz.getSuperclass() == null)
				{
					return method;
				}
				else
				{
					method = getPrivateMethod(clazz.getSuperclass(), methodName, classes);
				}
			}
		}
		return method;
	}

	/**
	 * @param obj
	 *            调整方法的对象
	 * @param methodName
	 *            方法名
	 * @param classes
	 *            参数类型数组
	 * @param objects
	 *            参数数组
	 * @return 方法的返回值
	 */
	public static Object invoke(final Object obj, final String methodName, final Class<?>[] classes, final Object[] objects)
	{
		try
		{
			Method method = getPrivateMethod(obj.getClass(), methodName, classes);
			method.setAccessible(true);// 调用 private方法的关键一句话
			return method.invoke(obj, objects);
		}
		catch (Exception e)
		{
			throw new RuntimeException(e);
		}
	}

	public static Object invoke(final Object obj, final String methodName, final Class<?>[] classes)
	{
		return invoke(obj, methodName, classes, new Object[] {});
	}

}
