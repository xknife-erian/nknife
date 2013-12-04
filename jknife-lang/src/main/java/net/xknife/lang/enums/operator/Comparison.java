package net.xknife.lang.enums.operator;

/**
 * 比较运算符
 * 
 * @author lukan@jeelu.com
 * 
 */
public enum Comparison
{
	/**
	 * 大于
	 */
	MoreThan,
	/**
	 * 小于
	 */
	LessThan,
	/**
	 * 不大于(小于等于)
	 */
	NotMoreThan,
	/**
	 * 不小于(大于等于)
	 */
	NotLessThan,
	/**
	 * 等于
	 */
	Equal,
	/**
	 * 不等于
	 */
	NotEqual,
	/**
	 * 符号：like
	 */
	Like,
	/**
	 * 嘛也不是
	 */
	None,
	/**
	 * 是
	 */
	IS;

	public static String toString(final Comparison co)
	{
		switch (co)
		{
			case IS:
				return "is";
			case MoreThan:
				return ">";
			case LessThan:
				return "<";
			case NotMoreThan:
				return "<=";
			case NotLessThan:
				return ">=";
			case NotEqual:
				return "<>";
			case Like:
				return "LIKE";
			case Equal:
			case None:
			default:
				return "=";
		}
	}

	public static Comparison parse(final String op)
	{
		Comparison co = Comparison.None;
		if ("=".equals(op))
			co = Comparison.Equal;
		else if (">".equals(op))
			co = Comparison.MoreThan;
		else if (">=".equals(op))
			co = Comparison.NotLessThan;
		else if ("<".equals(op))
			co = Comparison.LessThan;
		else if ("<=".equals(op))
			co = Comparison.NotMoreThan;
		else if ("<>".equals(op))
			co = Comparison.NotEqual;
		else if ("like".equals(op))
			co = Comparison.Like;
		return co;
	}
}
