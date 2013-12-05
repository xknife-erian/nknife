package net.xknife.lang.enums;

import java.util.regex.Pattern;

public class Patterns
{
	public final static Pattern EMAIL = Pattern.compile("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");

	public final static Pattern EMAIL1 = Pattern.compile("(\\d{3}-|\\d{4}-)?(\\d{8}|\\d{7})?"); // 国内电话
	public final static Pattern EMAIL2 = Pattern.compile("^[1-9]*[1-9][0-9]*$"); // 腾讯QQ
	public final static Pattern EMAIL3 = Pattern.compile("^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$"); // url
	public final static Pattern EMAIL4 = Pattern.compile("^\\d+$"); // 非负整数
	public final static Pattern EMAIL5 = Pattern.compile("^[0-9]*[1-9][0-9]*$"); // 正整数
	public final static Pattern EMAIL6 = Pattern.compile("^((-\\d+)|(0+))$"); // 非正整数
	public final static Pattern EMAIL7 = Pattern.compile("^-[0-9]*[1-9][0-9]*$"); // 负整数
	public final static Pattern EMAI8L = Pattern.compile("^-?\\d+$"); // 整数
	public final static Pattern EMA8IL8 = Pattern.compile("^\\d+(\\.\\d+)?$"); // 非负浮点数
	public final static Pattern EM8A8IL8 = Pattern.compile("^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$"); // 正浮点数
	public final static Pattern EMAIL8 = Pattern.compile("^((-\\d+(\\.\\d+)?)|(0+(\\.0+)?))$"); // 非正浮点数
	public final static Pattern EMA22IL888 = Pattern.compile("^(-(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*)))$"); // 负浮点数
	public final static Pattern EMsAIL888 = Pattern.compile("^(-?\\d+)(\\.\\d+)?$"); // 浮点数
	public final static Pattern EM55AIL888 = Pattern.compile("^[A-Za-z]+$"); // 由26个英文字母组成的字符串
	public final static Pattern EM66AIL888 = Pattern.compile("^[A-Z]+$"); // 由26个英文字母的大写组成的字符串
	public final static Pattern E33MAIL888 = Pattern.compile("^[a-z]+$"); // 由26个英文字母的小写组成的字符串
	public final static Pattern EMAI55L888 = Pattern.compile("^[A-Za-z0-9]+$"); // 由数字和26个英文字母组成的字符串
	public final static Pattern EM33AIL888 = Pattern.compile("^\\w+$"); // 由数字、26个英文字母或者下划线组成的字符串
}
