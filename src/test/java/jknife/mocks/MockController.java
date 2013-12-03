package jknife.mocks;

import java.lang.reflect.Method;
import java.util.List;
import java.util.Map;

import net.xknife.webknife.interfaces.WebapiMethod;
import net.xknife.webknife.interfaces.IController.AbstractController;

import org.apache.commons.dbutils.QueryLoader;
import org.joda.time.DateTime;

import com.google.common.collect.Maps;

public class MockController extends AbstractController
{
	private final Map<String, Method> _MethodMap = Maps.newHashMap();

	public Map<String, Method> getMap()
	{
		return _MethodMap;
	}

	public void fillServletMethodMap()
	{
		super.fillServletMethodMap(_MethodMap);
	}

	@WebapiMethod("method1")
	public String method1(final List<DateTime> times)
	{
		return null;
	}

	@WebapiMethod("method2")
	public void method2(final List<DateTime> times)
	{
	}

	@WebapiMethod("method3")
	public QueryLoader method3()
	{
		return null;
	}
}
