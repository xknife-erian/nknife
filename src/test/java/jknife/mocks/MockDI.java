package jknife.mocks;

import java.util.List;

import net.xknife.j.library.DI;

import com.google.inject.Injector;
import com.google.inject.Module;

public class MockDI extends DI
{
	public static List<Module> getModules()
	{
		return _Modules;
	}

	public static Injector getInject()
	{
		return _Injector;
	}
}
