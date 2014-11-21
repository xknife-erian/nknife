package net.xknife.inject;

import java.util.Collection;
import java.util.List;

import net.xknife.lang.widgets.ClassFinder;

import com.google.common.collect.Lists;
import com.google.inject.AbstractModule;
import com.google.inject.Guice;
import com.google.inject.Injector;
import com.google.inject.Key;
import com.google.inject.Module;

/**
 * 面向Guice的快速助手类。基本上是Guice的{@link Injector}的轻量封装。
 * 
 * @author lukan@p-an.com 2013年11月14日
 */
public class DI
{
	static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(DI.class);

	static
	{
		_Modules = Lists.newArrayList();
	}

	protected static Injector _Injector;

	protected static List<Module> _Modules;

	public static void clear()
	{
		_Modules.clear();
		_Injector = null;
	}

	public static <T> T getInstance(final Class<T> type)
	{
		return _Injector.getInstance(type);
	}

	public static <T> T getInstance(final Key<T> key)
	{
		return _Injector.getInstance(key);
	}

	public static <T> T injectMembers(final T instance)
	{
		_Injector.injectMembers(instance);
		return instance;
	}

	public static void registerModule(final Module... modules)
	{
		for (Module module : modules)
		{
			_Modules.add(module);
		}
	}

	public static void registerModules(final Collection<Module> modules)
	{
		_Modules.addAll(modules);
	}

	public static void buildInjector()
	{
		if (_Injector == null)
		{
			Module[] modules = new Module[_Modules.size()];
			modules = _Modules.toArray(modules);
			_Logger.debug(String.format("共%s个Modulue被注册", modules.length));
			_Injector = Guice.createInjector(modules);
		}
	}

	/**
	 * 自动载入类路径下所有的面向 {@link Guice} 的 {@link Module}。<br />
	 * 使用该方法，有几个约定请务必注意：<br />
	 * 1. 所有的Module的实现类均应继承自AbstactModule；<br />
	 * 2. 实现类的类名必须包含有“Module”字样；<br />
	 * 3. (非强制约定1:每个Java项目的一般包含一个多或少量 {@link Module}，均放在该项目的根下，便于查看，该包内用到的绑定一般均在该包内实现)<br />
	 * 4. (非强制约定2:部份无法规划到具体项目的 {@link Module}，均放在v-kernel项目的modules包下)<br />
	 */
	public static void loadModules()
	{
		List<Class<AbstractModule>> moduleList = ClassFinder.find(AbstractModule.class, null, false, "Module");
		_Modules = Lists.newArrayList();
		for (int i = 0; i < moduleList.size(); i++)
		{
			Class<AbstractModule> moduleClass = moduleList.get(i);
			try
			{
				Module module = moduleClass.newInstance();
				_Modules.add(module);
				_Logger.debug(String.format("Guice的Module(%s)：%s", i, module.getClass().getSimpleName()));
			}
			catch (InstantiationException e)
			{
				_Logger.warn("Guice.Module Load异常." + moduleClass.getName(), e);
			}
			catch (IllegalAccessException e)
			{
				_Logger.warn("Guice.Module Load异常." + moduleClass.getName(), e);
			}
		}
	}

}