package net.xknife.lang.utilities;

import com.google.common.base.Strings;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * 字符串操作的帮助函数
 * 
 * @author lukan@jeelu.com
 */
public abstract class StringHelper
{
	/**
	 * 复制字符
	 * 
	 * @param c
	 *            字符
	 * @param num
	 *            数量
	 * @return 新字符串
	 */
	public static String dup(final char c, final int num)
	{
		if ((c == 0) || (num < 1))
		{
			return "";
		}
		StringBuilder sb = new StringBuilder(num);
		for (int i = 0; i < num; i++)
		{
			sb.append(c);
		}
		return sb.toString();
	}

	/**
	 * 复制字符
	 * 
	 * @param c
	 *            字符
	 * @param num
	 *            数量
	 * @return 新字符串
	 */
	public static String dup(final String c, final int num)
	{
		if (Strings.isNullOrEmpty(c) || (num < 1))
		{
			return "";
		}
		StringBuilder sb = new StringBuilder(num);
		for (int i = 0; i < num; i++)
		{
			sb.append(c);
		}
		return sb.toString();
	}

	/**
	 * 将字符串首字母大写
	 * 
	 * @param s
	 *            字符串
	 * @return 首字母大写后的新字符串
	 */
	public static String capitalize(final CharSequence s)
	{
		if (null == s)
		{
			return null;
		}
		int len = s.length();
		if (len == 0)
		{
			return "";
		}
		char char0 = s.charAt(0);
		if (Character.isUpperCase(char0))
		{
			return s.toString();
		}
		return new StringBuilder(len).append(Character.toUpperCase(char0)).append(s.subSequence(1, len)).toString();
	}

	/**
	 * 将字符串首字母小写
	 * 
	 * @param s
	 *            字符串
	 * @return 首字母小写后的新字符串
	 */
	public static String lowerFirst(final CharSequence s)
	{
		if (null == s)
		{
			return null;
		}
		int len = s.length();
		if (len == 0)
		{
			return "";
		}
		char c = s.charAt(0);
		if (Character.isLowerCase(c))
		{
			return s.toString();
		}
		return new StringBuilder(len).append(Character.toLowerCase(c)).append(s.subSequence(1, len)).toString();
	}

	/**
	 * 检查两个字符串的忽略大小写后是否相等.
	 * 
	 * @param s1
	 *            字符串A
	 * @param s2
	 *            字符串B
	 * @return true 如果两个字符串忽略大小写后相等,且两个字符串均不为null
	 */
	public static boolean equalsIgnoreCase(final String s1, final String s2)
	{
		return s1 == null ? s2 == null : s1.equalsIgnoreCase(s2);
	}

	/**
	 * 检查两个字符串是否相等.
	 * 
	 * @param s1
	 *            字符串A
	 * @param s2
	 *            字符串B
	 * @return true 如果两个字符串相等,且两个字符串均不为null
	 */
	public static boolean equals(final String s1, final String s2)
	{
		return s1 == null ? s2 == null : s1.equals(s2);
	}

	/**
	 * 去掉字符串前后空白
	 * 
	 * @param cs
	 *            字符串
	 * @return 新字符串
	 */
	public static String trim(final CharSequence cs)
	{
		if (null == cs)
		{
			return null;
		}
		if (cs instanceof String)
		{
			return ((String) cs).trim();
		}
		int length = cs.length();
		if (length == 0)
		{
			return cs.toString();
		}
		int l = 0;
		int last = length - 1;
		int r = last;
		for (; l < length; l++)
		{
			if (!Character.isWhitespace(cs.charAt(l)))
			{
				break;
			}
		}
		for (; r > l; r--)
		{
			if (!Character.isWhitespace(cs.charAt(r)))
			{
				break;
			}
		}
		if (l > r)
		{
			return "";
		}
		else if ((l == 0) && (r == last))
		{
			return cs.toString();
		}
		return cs.subSequence(l, r + 1).toString();
	}

	/**
	 * 将字符串按半角逗号，拆分成数组，空元素将被忽略
	 * 
	 * @param s
	 *            字符串
	 * @return 字符串数组
	 */
	public static String[] splitIgnoreBlank(final String s)
	{
		return StringHelper.splitIgnoreBlank(s, ",");
	}

	/**
	 * 根据一个正则式，将字符串拆分成数组，空元素将被忽略
	 * 
	 * @param s
	 *            字符串
	 * @param regex
	 *            正则式
	 * @return 字符串数组
	 */
	public static String[] splitIgnoreBlank(final String s, final String regex)
	{
		if (null == s)
		{
			return null;
		}
		String[] ss = s.split("\\" + regex);
		List<String> list = new LinkedList<String>();
		for (String st : ss)
		{
			if (Strings.isNullOrEmpty(st))
			{
				continue;
			}
			list.add(trim(st));
		}
		return list.toArray(new String[list.size()]);
	}

	/**
	 * 将一个整数转换成最小长度为某一固定数值的十进制形式字符串
	 * 
	 * @param d
	 *            整数
	 * @param width
	 *            宽度
	 * @return 新字符串
	 */
	public static String fillDigit(final int d, final int width)
	{
		return StringHelper.alignRight(String.valueOf(d), width, '0');
	}

	/**
	 * 将一个整数转换成最小长度为某一固定数值的十六进制形式字符串
	 * 
	 * @param d
	 *            整数
	 * @param width
	 *            宽度
	 * @return 新字符串
	 */
	public static String fillHex(final int d, final int width)
	{
		return StringHelper.alignRight(Integer.toHexString(d), width, '0');
	}

	/**
	 * 将一个整数转换成最小长度为某一固定数值的二进制形式字符串
	 * 
	 * @param d
	 *            整数
	 * @param width
	 *            宽度
	 * @return 新字符串
	 */
	public static String fillBinary(final int d, final int width)
	{
		return StringHelper.alignRight(Integer.toBinaryString(d), width, '0');
	}

	/**
	 * 将一个整数转换成固定长度的十进制形式字符串
	 * 
	 * @param d
	 *            整数
	 * @param width
	 *            宽度
	 * @return 新字符串
	 */
	public static String toDigit(final int d, final int width)
	{
		return StringHelper.cutRight(String.valueOf(d), width, '0');
	}

	/**
	 * 将一个整数转换成固定长度的十六进制形式字符串
	 * 
	 * @param d
	 *            整数
	 * @param width
	 *            宽度
	 * @return 新字符串
	 */
	public static String toHex(final int d, final int width)
	{
		return StringHelper.cutRight(Integer.toHexString(d), width, '0');
	}

	/**
	 * 将一个整数转换成固定长度的二进制形式字符串
	 * 
	 * @param d
	 *            整数
	 * @param width
	 *            宽度
	 * @return 新字符串
	 */
	public static String toBinary(final int d, final int width)
	{
		return StringHelper.cutRight(Integer.toBinaryString(d), width, '0');
	}

	/**
	 * 保证字符串为一固定长度。超过长度，切除，否则补字符。
	 * 
	 * @param s
	 *            字符串
	 * @param width
	 *            长度
	 * @param c
	 *            补字符
	 * @return 修饰后的字符串
	 */
	public static String cutRight(final String s, final int width, final char c)
	{
		if (null == s)
		{
			return null;
		}
		int len = s.length();
		if (len == width)
		{
			return s;
		}
		if (len < width)
		{
			return StringHelper.dup(c, width - len) + s;
		}
		return s.substring(len - width, len);
	}

	/**
	 * 在字符串左侧填充一定数量的特殊字符
	 * 
	 * @param cs
	 *            字符串
	 * @param width
	 *            字符数量
	 * @param c
	 *            字符
	 * @return 新字符串
	 */
	public static String alignRight(final CharSequence cs, final int width, final char c)
	{
		if (null == cs)
		{
			return null;
		}
		int len = cs.length();
		if (len >= width)
		{
			return cs.toString();
		}
		return new StringBuilder().append(dup(c, width - len)).append(cs).toString();
	}

	/**
	 * 在字符串右侧填充一定数量的特殊字符
	 * 
	 * @param cs
	 *            字符串
	 * @param width
	 *            字符数量
	 * @param c
	 *            字符
	 * @return 新字符串
	 */
	public static String alignLeft(final CharSequence cs, final int width, final char c)
	{
		if (null == cs)
		{
			return null;
		}
		int length = cs.length();
		if (length >= width)
		{
			return cs.toString();
		}
		return new StringBuilder().append(cs).append(dup(c, width - length)).toString();
	}

	/**
	 * @param cs
	 *            字符串
	 * @param lc
	 *            左字符
	 * @param rc
	 *            右字符
	 * @return 字符串是被左字符和右字符包裹 -- 忽略空白
	 */
	public static boolean isQuoteByIgnoreBlank(final CharSequence cs, final char lc, final char rc)
	{
		if (null == cs)
		{
			return false;
		}
		int len = cs.length();
		if (len < 2)
		{
			return false;
		}
		int l = 0;
		int last = len - 1;
		int r = last;
		for (; l < len; l++)
		{
			if (!Character.isWhitespace(cs.charAt(l)))
			{
				break;
			}
		}
		if (cs.charAt(l) != lc)
		{
			return false;
		}
		for (; r > l; r--)
		{
			if (!Character.isWhitespace(cs.charAt(r)))
			{
				break;
			}
		}
		return (l < r) && (cs.charAt(r) == rc);
	}

	/**
	 * @param cs
	 *            字符串
	 * @param lc
	 *            左字符
	 * @param rc
	 *            右字符
	 * @return 字符串是被左字符和右字符包裹
	 */
	public static boolean isQuoteBy(final CharSequence cs, final char lc, final char rc)
	{
		if (null == cs)
		{
			return false;
		}
		int length = cs.length();
		return (length > 1) && (cs.charAt(0) == lc) && (cs.charAt(length - 1) == rc);
	}

	/**
	 * 获得一个字符串集合中，最长串的长度
	 * 
	 * @param coll
	 *            字符串集合
	 * @return 最大长度
	 */
	public static int maxLength(final Collection<? extends CharSequence> coll)
	{
		int re = 0;
		if (null != coll)
		{
			for (CharSequence s : coll)
			{
				if (null != s)
				{
					re = Math.max(re, s.length());
				}
			}
		}
		return re;
	}

	/**
	 * 获得一个字符串数组中，最长串的长度
	 * 
	 * @param array
	 *            字符串数组
	 * @return 最大长度
	 */
	public static <T extends CharSequence> int maxLength(final T[] array)
	{
		int re = 0;
		if (null != array)
		{
			for (CharSequence s : array)
			{
				if (null != s)
				{
					re = Math.max(re, s.length());
				}
			}
		}
		return re;
	}

	/**
	 * 对obj进行toString()操作,如果为null返回""
	 * 
	 * @param obj
	 * @return obj.toString()
	 */
	public static String sNull(final Object obj)
	{
		return sNull(obj, "");
	}

	/**
	 * 对obj进行toString()操作,如果为null返回def中定义的值
	 * 
	 * @param obj
	 * @param def
	 *            如果obj==null返回的内容
	 * @return obj的toString()操作
	 */
	public static String sNull(final Object obj, final String def)
	{
		return obj != null ? obj.toString() : def;
	}

	/**
	 * 对obj进行toString()操作,如果为空串返回""
	 * 
	 * @param obj
	 * @return obj.toString()
	 */
	public static String sBlank(final Object obj)
	{
		return sBlank(obj, "");
	}

	/**
	 * 对obj进行toString()操作,如果为空串返回def中定义的值
	 * 
	 * @param obj
	 * @param def
	 *            如果obj==null返回的内容
	 * @return obj的toString()操作
	 */
	public static String sBlank(final Object obj, final String def)
	{
		if (null == obj)
		{
			return def;
		}
		String s = obj.toString();
		return Strings.isNullOrEmpty(s) ? def : s;
	}

	/**
	 * 截去第一个字符
	 * <p>
	 * 比如:
	 * <ul>
	 * <li>removeFirst("12345") => 2345
	 * <li>removeFirst("A") => ""
	 * </ul>
	 * 
	 * @param str
	 *            字符串
	 * @return 新字符串
	 */
	public static String removeFirst(final CharSequence str)
	{
		if (str == null)
		{
			return null;
		}
		if (str.length() > 1)
		{
			return str.subSequence(1, str.length()).toString();
		}
		return "";
	}

	/**
	 * 截去最后一个字符
	 * 
	 * @param str
	 *            字符串
	 * @return 新字符串
	 */
	public static String removeEnd(final CharSequence str)
	{
		if (str == null)
		{
			return null;
		}
		if (str.length() > 1)
		{
			return str.subSequence(0, str.length() - 1).toString();
		}
		return "";
	}

	/**
	 * 如果str中第一个字符和 c一致,则删除,否则返回 str
	 * <p>
	 * 比如:
	 * <ul>
	 * <li>removeFirst("12345",1) => "2345"
	 * <li>removeFirst("ABC",'B') => "ABC"
	 * <li>removeFirst("A",'B') => "A"
	 * <li>removeFirst("A",'A') => ""
	 * </ul>
	 * 
	 * @param str
	 *            字符串
	 * @param c
	 *            第一个个要被截取的字符
	 * @return 新字符串
	 */
	public static String removeFirst(final String str, final char c)
	{
		return (Strings.isNullOrEmpty(str) || (c != str.charAt(0))) ? str : str.substring(1);
	}

	/**
	 * 判断一个字符串数组是否包括某一字符串
	 * 
	 * @param ss
	 *            字符串数组
	 * @param s
	 *            字符串
	 * @return 是否包含
	 */
	public static boolean isin(final String[] ss, final String s)
	{
		if ((null == ss) || (ss.length == 0) || Strings.isNullOrEmpty(s))
		{
			return false;
		}
		for (String w : ss)
		{
			if (s.equals(w))
			{
				return true;
			}
		}
		return false;
	}

	private static Pattern email_Pattern = Pattern.compile("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");

	/**
	 * 检查一个字符串是否为合法的电子邮件地址
	 * 
	 * @param input
	 *            需要检查的字符串
	 * @return true 如果是有效的邮箱地址
	 */
	public static synchronized final boolean isMailAddress(final CharSequence input)
	{
		return email_Pattern.matcher(input).matches();
	}

	/**
	 * 将一个字符串某一个字符后面的字母变成大写，比如
	 * 
	 * <pre>
	 *  upperWord("hello-world", '-') => "helloWorld"
	 * </pre>
	 * 
	 * @param s
	 *            字符串
	 * @param c
	 *            字符
	 * @return 转换后字符串
	 */
	public static String upperWord(final CharSequence s, final char c)
	{
		StringBuilder sb = new StringBuilder();
		int len = s.length();
		for (int i = 0; i < len; i++)
		{
			char ch = s.charAt(i);
			if (ch == c)
			{
				do
				{
					i++;
					if (i >= len)
					{
						return sb.toString();
					}
					ch = s.charAt(i);
				}
				while (ch == c);
				sb.append(Character.toUpperCase(ch));
			}
			else
			{
				sb.append(ch);
			}
		}
		return sb.toString();
	}

    /**
     * 判断是否为汉字
     *
     * @param str
     * @return
     */
    public static boolean isHanZi(String str) {
        char[] chars = str.toCharArray();
        boolean isGBK = false;
        for (int i = 0; i < chars.length; i++) {
            byte[] bytes = ("" + chars[i]).getBytes();
            if (bytes.length == 2) {
                int[] ints = new int[2];
                ints[0] = bytes[0] & 0xff;
                ints[1] = bytes[1] & 0xff;
                if (ints[0] >= 0x81 && ints[0] <= 0xFE && ints[1] >= 0x40 && ints[1] <= 0xFE) {
                    isGBK = true;
                    break;
                }
            }
        }
        return isGBK;
    }

    /**
     * 判断是否为乱码
     *
     * @param str
     * @return
     */
    public static boolean isMessyCode(String str) {
        for (int i = 0; i < str.length(); i++) {
            char c = str.charAt(i);
            // 当从Unicode编码向某个字符集转换时，如果在该字符集中没有对应的编码，则得到0x3f（即问号字符?）
            // 从其他字符集向Unicode编码转换时，如果这个二进制数在该字符集中没有标识任何的字符，则得到的结果是0xfffd
            // System.out.println("--- " + (int) c);
            if ((int) c == 0xfffd) {
                //System.out.println("存在乱码 " + (int) c);
                return true;
            }
        }
        return false;
    }

    /**
     * 判断字符串是否为双整型数字
     *
     * @param str
     * @return
     */
    public static boolean isDouble(String str) {
        if (Strings.isNullOrEmpty(str)) {
            return false;
        }
        Pattern p = Pattern.compile("-*\\d*.\\d*");
        // Pattern p = Pattern.compile("-*"+"\\d*"+"."+"\\d*");
        return p.matcher(str).matches();
    }
    /**
     * 判断字符串是否为整字
     *
     * @param str
     * @return
     */
    public static boolean isNumber(String str) {
        if (Strings.isNullOrEmpty(str)) {
            return false;
        }
        Pattern p = Pattern.compile("-*\\d*");
        return p.matcher(str).matches();
    }

    /**
     * 判断是否为数字
     *
     * @param str
     * @return
     */
    public static boolean isNumeric(String str)
    {
        Pattern pattern = Pattern.compile("[0-9]*");
        Matcher isNum = pattern.matcher(str);
        if( !isNum.matches() ) {
            return false;
        }
        return true;
    }
}
