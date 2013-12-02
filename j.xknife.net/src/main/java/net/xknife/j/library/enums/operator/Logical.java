package net.xknife.j.library.enums.operator;

public enum Logical
{
	/**
	 * 如果一组的比较都为 TRUE，那么就为 TRUE。
	 */
	ALL,

	/**
	 * 如果两个布尔表达式都为 TRUE，那么就为 TRUE。
	 */
	AND,

	/**
	 * 如果一组的比较中任何一个为 TRUE，那么就为 TRUE。
	 */
	ANY,

	/**
	 * 如果操作数在某个范围之内，那么就为 TRUE。
	 */
	BETWEEN,

	/**
	 * 如果子查询包含一些行，那么就为 TRUE。
	 */
	EXISTS,

	/**
	 * 如果操作数等于表达式列表中的一个，那么就为 TRUE。
	 */
	IN,

	/**
	 * 如果操作数与一种模式相匹配，那么就为 TRUE。
	 */
	LIKE,

	/**
	 * 对任何其他布尔运算符的值取反。
	 */
	NOT,

	/**
	 * 如果两个布尔表达式中的一个为 TRUE，那么就为 TRUE。
	 */
	OR,

	/**
	 * 如果在一组比较中，有些为 TRUE，那么就为 TRUE。
	 */
	SOME,
}
