package net.xknife.web.utilities;

import java.util.Map;

import com.google.common.collect.Maps;
import com.google.inject.ImplementedBy;

/**
 * Model，MVC中的M， 用于将Controller中的数据传递给View
 * 
 */
@ImplementedBy(Model.DefaultModel.class)
public interface Model
{
	/**
	 * 增加一个属性
	 * 
	 * @param attributeName
	 *            属性名称
	 * @param attributeValue
	 *            属性值
	 */
	Model add(String attributeName, Object attributeValue);

	/**
	 * 根据属性名得到属性值
	 * 
	 * @param attributeName
	 *            属性名称
	 * @return 对应的属性值
	 */
	Object get(String attributeName);

	/**
	 * Return the model map. Never returns <code>null</code>. To be called by application code for modifying the model.
	 */
	Map<String, Object> getModel();

	/**
	 * 批量增加属性
	 * 
	 * @param attributes
	 *            属性map
	 */
	Model addAll(Map<String, ?> attributes);

	/**
	 * 判断是否包含属性名
	 * 
	 * @param attributeName
	 *            需要查找的属性
	 * @return 是否包含
	 */
	boolean contains(String attributeName);

	/**
	 * MVC 中的Model, 以key,value形式存放，可以由Controller传个View
	 * 
	 * @author renjun
	 * 
	 */
	public static class DefaultModel implements Model
	{
		/** Model Map */
		private final Map<String, Object> data = Maps.newConcurrentMap();

		/**
		 * 增加一个属性
		 * 
		 * @param attributeName
		 *            属性名称
		 * @param attributeValue
		 *            属性值
		 */

		@Override
		public Model add(final String attributeName, final Object attributeValue)
		{
			data.put(attributeName, attributeValue);
			return this;
		}

		/**
		 * 根据属性名得到属性值
		 * 
		 * @param attributeName
		 *            属性名称
		 * @return 对应的属性值
		 */

		@Override
		public Object get(final String attributeName)
		{
			return data.get(attributeName);
		}

		/**
		 * Return the model map. Never returns <code>null</code>. To be called by application code for modifying the model.
		 */

		@Override
		public Map<String, Object> getModel()
		{
			return data;
		}

		/**
		 * 批量增加属性
		 * 
		 * @param attributes
		 */

		@Override
		public Model addAll(final Map<String, ?> attributes)
		{
			data.putAll(attributes);
			return this;
		}

		/**
		 * 判断是否包含属性名
		 * 
		 * @param attributeName
		 *            需要查找的属性
		 * @return
		 */

		@Override
		public boolean contains(final String attributeName)
		{
			return data.containsKey(attributeName);
		}
	}
}
