package net.xknife.web.utilities;

import javax.inject.Singleton;

import com.google.inject.ImplementedBy;

/**
 * 从queryString或者form过来的参数进行安全清洗 开发者可以自己重新注入 SafeParameter 的实现，来实现适合自己的安全参数
 * 
 */
@ImplementedBy(SafeParameter.DefaultSafeParameter.class)
public interface SafeParameter
{
	/**
	 * 将原始参数值安全编码后返回
	 * 
	 * @param parameterValue
	 *            原始参数值
	 * @return 安全参数值
	 */
	String encoding(String parameterValue);

	@Singleton
	public static class DefaultSafeParameter implements SafeParameter
	{
		@Override
		public String encoding(final String parameterValue)
		{
			return parameterValue;
		}
	}

}
