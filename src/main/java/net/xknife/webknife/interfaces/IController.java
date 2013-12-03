package net.xknife.webknife.interfaces;

import java.io.IOException;
import java.io.Writer;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Map;

import net.xknife.jsonknife.JsonKnife;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.google.common.collect.Maps;

public interface IController
{
	public abstract int process(String methodName, Object params, Writer writer);

	public abstract class AbstractController implements IController
	{
		protected final Map<String, Method> _MethodMap = Maps.newHashMap();

		protected void fillServletMethodMap(final Map<String, Method> map)
		{
			Method[] methods = this.getClass().getMethods();
			for (Method method : methods)
			{
				// 判断方法是否被@WebapiMethod修饰
				boolean isApi = method.isAnnotationPresent(WebapiMethod.class);
				if (!isApi)
				{
					continue;
				}
				WebapiMethod webapi = method.getAnnotation(WebapiMethod.class);
				map.put(webapi.value(), method);
			}
		}

		@Override
		public int process(final String methodName, final Object params, final Writer writer)
		{
			if (_MethodMap.size() <= 0)
			{
				fillServletMethodMap(_MethodMap);
			}
			if (!_MethodMap.containsKey(methodName))
			{
				return 0;
			}
			Method method = _MethodMap.get(methodName);
			Object result = null;
			try
			{
				result = method.invoke(this, params);
			}
			catch (IllegalAccessException e)
			{
				return -10;
			}
			catch (IllegalArgumentException e)
			{
				return -11;
			}
			catch (InvocationTargetException e)
			{
				return -12;
			}
			try
			{
				JsonGenerator jsonGenerator = JsonKnife.getFactory().createGenerator(writer);
				jsonGenerator.writeObject(result);
				jsonGenerator.flush();
				jsonGenerator.close();
			}
			catch (JsonProcessingException e)
			{
				return -2;
			}
			catch (IOException e)
			{
				return -1;
			}
			return 1;
		}
	}
}
