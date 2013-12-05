package net.xknife.data;

import net.xknife.data.enums.operator.Arithmetic;

/**
 * 一个简单公式的字符串表达。可以用在如Sql语句中。
 * 
 */
public class Formula
{
	private final StringBuilder _stringBuilder = new StringBuilder();

	public Formula(final String field)
	{
		_stringBuilder.append(field);
	}

	public Formula append(final Arithmetic ao, final String field)
	{
		_stringBuilder.append(getArithmeticOperator(ao)).append(field);
		return this;
	}

	public StringBuilder toStringBuilder()
	{
		return _stringBuilder;
	}

	public static String getArithmeticOperator(final Arithmetic ao)
	{
		switch (ao)
		{
			case Add:
				return "+";
			case Division:
				return "/";
			case Minus:
				return "-";
			case Modular:
				return "%";
			case Multiplication:
				return "*";
			default:
				break;
		}
		return null;
	}
}
