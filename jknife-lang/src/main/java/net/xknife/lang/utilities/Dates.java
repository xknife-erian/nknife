package net.xknife.lang.utilities;

import java.sql.Time;
import java.sql.Timestamp;
import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import com.google.common.base.Strings;

/**
 * 用来处理有关时间(含日期)操作的助手类<br>
 * 调用Main方法观看各方法的使用。
 * 
 * @author lukan@jeelu.com
 * @version 3.0
 * @date 2013.09.26
 */
@Deprecated
public class Dates
{
	public static final String General_Format = "yyyy-MM-dd HH:mm:ss:SSS";

	// = 当前时间相关 ===================================================

	/**
	 * 获取现在
	 * 
	 * @return
	 */
	public static Date now()
	{
		Date now = new Date();
		now.setTime(System.currentTimeMillis());
		return now;
	}

	/**
	 * 获取明天的这个时候
	 * 
	 * @return
	 */
	public static Date tomorrow()
	{
		Calendar day = Calendar.getInstance();
		day.add(Calendar.DATE, 1);
		return day.getTime();
	}

	/**
	 * 获取昨天的这个时候
	 * 
	 * @return
	 */
	public static Date yesterday()
	{
		Calendar day = Calendar.getInstance();
		day.add(Calendar.DATE, -1);
		return day.getTime();
	}

	/**
	 * 是否闰年
	 */
	public static boolean isLeapYear(final int year)
	{
		return (((year % 4) == 0) && ((year % 100) != 0)) || ((year % 400) == 0);
	}

	/**
	 * 从一个日期类型(Date)中获取时间类型(Time)
	 * 
	 * @param date
	 * @return
	 */
	public static Time splitTime(final Date date)
	{
		StringBuilder sb = new StringBuilder();
		int hour = Values.getHour(date);
		sb.append(hour).append(":");
		sb.append(Values.getMinute(date)).append(":");
		sb.append(Values.getSecond(date));
		return Time.valueOf(sb.toString());
	}

	public static class Current
	{
		/**
		 * 获取今天即时时间
		 * 
		 * @return
		 */
		public static Time currentTime()
		{
			return splitTime(now());
		}

		/**
		 * 得到当前时间的时间戳
		 * 
		 * @return 当前时间戳
		 */
		public static Timestamp currentTimestamp()
		{
			long curTime = System.currentTimeMillis();
			return new Timestamp(curTime);
		}

		/**
		 * 获取今天是周几? ( 周日为 0 )
		 * 
		 * @return
		 */
		public static int currentDayOfWeek()
		{
			Calendar day = Calendar.getInstance();
			int dayOfWeek = day.get(Calendar.DAY_OF_WEEK) - 1;
			return dayOfWeek;
		}

		/**
		 * 获取当前小时
		 * 
		 * @return
		 */
		public static int currentHour()
		{
			Calendar day = Calendar.getInstance();
			return day.get(Calendar.HOUR_OF_DAY);
		}

		/**
		 * 获取当前分钟
		 * 
		 * @return
		 */
		public static int currentMinute()
		{
			Calendar day = Calendar.getInstance();
			return day.get(Calendar.MINUTE);
		}

		/**
		 * 获取当前月份(实际月份数字，从Java的Calendar类型中获取的月份数字是从零开始)
		 * 
		 * @return
		 */
		public static int currentMonth()
		{
			Calendar day = Calendar.getInstance();
			return day.get(Calendar.MONTH) + 1;
		}

		/**
		 * 获取当前季度
		 * 
		 * @return
		 */
		public static int currentSeason()
		{
			switch (currentMonth())
			{
				case 1:
				case 2:
				case 3:
					return 1;
				case 4:
				case 5:
				case 6:
					return 2;
				case 7:
				case 8:
				case 9:
					return 3;
				case 10:
				case 11:
				case 12:
					return 4;
			}
			return 0;
		}

		/**
		 * 获取当前秒
		 * 
		 * @return
		 */
		public static int currentSecond()
		{
			Calendar day = Calendar.getInstance();
			return day.get(Calendar.SECOND);
		}

		/**
		 * 获取当前天，即今天是几号
		 * 
		 * @return
		 */
		public static int currentToday()
		{
			Calendar day = Calendar.getInstance();
			return day.get(Calendar.DAY_OF_MONTH);
		}

		/**
		 * 获取当前周(面向年而言)
		 * 
		 * @return
		 */
		public static int currentWeek()
		{
			Calendar day = Calendar.getInstance();
			return day.get(Calendar.WEEK_OF_YEAR);
		}

		/**
		 * 获取当前年
		 * 
		 * @return
		 */
		public static int currentYear()
		{
			Calendar day = Calendar.getInstance();
			return day.get(Calendar.YEAR);
		}
	}

	public static class Pass
	{
		/**
		 * 获取当前时间的指定个小时的起始时间（可以为负数，负数为回推时间）
		 */
		public static Date nextHour(final int n)
		{
			Calendar day = Calendar.getInstance();
			day.add(Calendar.HOUR_OF_DAY, n);
			day.set(Calendar.MINUTE, 0);
			day.set(Calendar.SECOND, 0);
			day.set(Calendar.MILLISECOND, 0);
			return day.getTime();
		}
	}

	/**
	 * 计算某个区间的起始天的0点(起始时间)
	 * 
	 * @author lukan@p-an.com 2013-9-26
	 */
	public static class First
	{
		/**
		 * 获取当前小时的开始时间
		 * 
		 * @return
		 */
		public static Date timeOfHour()
		{
			Calendar day = Calendar.getInstance();
			day.set(Calendar.MINUTE, 0);
			day.set(Calendar.SECOND, 0);
			day.set(Calendar.MILLISECOND, 0);
			return day.getTime();
		}

		/**
		 * 获取今天开始时间
		 * 
		 * @return
		 */
		public static Date timeOfToday()
		{
			Calendar day = Calendar.getInstance();
			day.set(Current.currentYear(), Current.currentMonth() - 1, Current.currentToday(), 0, 0, 0);
			return day.getTime();
		}

		/**
		 * 获取今天所在半月度的第一天(一般为1号或15号)
		 * 
		 * @return
		 */
		public static Date halfOfMonth()
		{
			int year = Current.currentYear();
			int month = Current.currentMonth();
			int day = Current.currentToday();
			StringBuilder sb = new StringBuilder(8);
			if (month >= 10)
			{
				sb.append(year);
				sb.append(month);
				if (day <= 15)
				{
					sb.append("01");
				}
				else
				{
					sb.append("16");
				}
				return Parser.parseShort(sb.toString());
			}
			else
			{
				sb.append(year);
				sb.append(0);
				sb.append(month);
				if (day <= 15)
				{
					sb.append("01");
				}
				else
				{
					sb.append("16");
				}
				return Parser.parseShort(sb.toString());
			}
		}

		/**
		 * 获取当前的半年度的第一天
		 * 
		 * @return
		 */
		public static Date firstDayHalfOfYear()
		{
			int m = Current.currentMonth();
			if (m <= 6)
			{
				return Parser.parseShort(Current.currentYear() + "0101");
			}
			else
			{
				return Parser.parseShort(Current.currentYear() + "0701");
			}
		}

		/**
		 * 获取去年的第一天
		 * 
		 * @return
		 */
		public static Date firstDayLastYear()
		{
			StringBuilder sb = new StringBuilder();
			sb.append(Current.currentYear() - 1);
			sb.append("0101000000");
			return Parser.parseSimple(sb.toString());
		}

		/**
		 * 获取当前月的第一天
		 * 
		 * @return
		 */
		public static Date firstDayMonth()
		{
			int year = Current.currentYear();
			int month = Current.currentMonth();
			StringBuilder sb = new StringBuilder(8);
			if (month >= 10)
			{
				sb.append(year);
				sb.append(month);
				sb.append("01");
				return Parser.parseShort(sb.toString());
			}
			else
			{
				sb.append(year);
				sb.append(0);
				sb.append(month);
				sb.append("01");
				return Parser.parseShort(sb.toString());
			}
		}

		/**
		 * 获取当前季的第一天
		 * 
		 * @return
		 */
		public static Date firstDaySeason()
		{
			int i = Current.currentSeason();
			switch (i)
			{
				case 1:
					return Parser.parseSimple(Current.currentYear() + "0101000000");
				case 2:
					return Parser.parseSimple(Current.currentYear() + "0401000000");
				case 3:
					return Parser.parseSimple(Current.currentYear() + "0701000000");
				case 4:
					return Parser.parseSimple(Current.currentYear() + "1001000000");
			}
			return null;
		}

		/**
		 * 获取本周的第一天(星期一)
		 * 
		 * @return
		 */
		public static Date firstDayWeek()
		{
			int offset = -1;
			int w = Current.currentDayOfWeek();
			if (w > 0)
			{
				offset = w - 1;
			}
			else
			{
				offset = 6;
			}
			Calendar day = Calendar.getInstance();
			day.add(Calendar.DAY_OF_MONTH, -offset);
			day.set(day.get(Calendar.YEAR), day.get(Calendar.MONTH), day.get(Calendar.DAY_OF_MONTH), 0, 0, 0);
			return day.getTime();
		}

		/**
		 * 获取今年的第一天。也就是今年的元旦。
		 * 
		 * @return
		 */
		public static Date firstDayYear()
		{
			StringBuilder sb = new StringBuilder();
			sb.append(Current.currentYear());
			sb.append("0101000000");
			return Parser.parseSimple(sb.toString());
		}
	}

	/**
	 * 计算日期(时间)之间的相差值
	 * 
	 * @author lukan@jeelu.com 2013-9-26
	 */
	public static class Differ
	{
		/**
		 * 两个日期相差的天
		 * 
		 * @param left
		 * @param right
		 * @return
		 */
		public static long byDays(final Date left, final Date right)
		{
			long timeDiffMillis = byMillis(left, right);

			// 两个日期相差的天
			return timeDiffMillis / (1000 * 60 * 60 * 24);
		}

		/**
		 * 两个日期相差的小时
		 * 
		 * @param left
		 * @param right
		 * @return
		 */
		public static long byHours(final Date left, final Date right)
		{
			long timeDiffMillis = byMillis(left, right);

			// 两个日期相差的小时
			return timeDiffMillis / (1000 * 60 * 60);
		}

		/**
		 * 两个日期相差的毫秒数
		 * 
		 * @param left
		 * @param right
		 * @return
		 */
		public static long byMillis(final Date left, final Date right)
		{
			Calendar c1 = Calendar.getInstance();
			c1.setTime(left);
			Calendar c2 = Calendar.getInstance();
			c2.setTime(right);

			// 两个日期相差的毫秒数
			return c2.getTimeInMillis() - c1.getTimeInMillis();
		}

		/**
		 * 两个日期相差的分钟
		 * 
		 * @param left
		 * @param right
		 * @return
		 */
		public static long byMinutes(final Date left, final Date right)
		{
			long timeDiffMillis = byMillis(left, right);
			// 两个日期相差的分钟
			return timeDiffMillis / (1000 * 60);
		}

		/**
		 * 两个日期相差的秒数
		 * 
		 * @param left
		 * @param right
		 * @return
		 */
		public static long bySeconds(final Date left, final Date right)
		{
			long timeDiffMillis = byMillis(left, right);
			// 两个日期相差的秒数
			return timeDiffMillis / 1000;
		}
	}

	// = 将时间格式化成一个字符串 ==================================================
	public static class Format
	{
		/**
		 * 从指定Timestamp(时间戳)中得到相应的日期的字符串
		 */
		public static String toTimestamp(final Timestamp datetime)
		{
			SimpleDateFormat sdf = new SimpleDateFormat(General_Format);
			return sdf.format(datetime).toString();
		}

		/**
		 * 将一个指定的类型转换成通用的字符串格式(yyyy-MM-dd HH:mm:ss:SSS)
		 * 
		 * @param date
		 * @return
		 */
		public static String toGeneral(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat(General_Format);
			return sdf.format(date);
		}

		/**
		 * 将一个指定的类型转换成缩写字符串格式(不带毫秒)(yyyyMMddHHmmss)
		 * 
		 * @param date
		 * @return
		 */
		public static String toShort(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
			return sdf.format(date);
		}

		/**
		 * 将一个指定的类型转换成缩写字符串格式(带毫秒)(yyyyMMddHHmmssS)
		 * 
		 * @param date
		 * @return
		 */
		public static String toShortS(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
			return sdf.format(date);
		}

		/**
		 * 将一个指定的类型转换成通用的字符串格式,但不含分隔符(yyyyMMddHHmmssSSS)
		 * 
		 * @param date
		 * @return
		 */
		public static String toGeneralDontContainsSeparatorChar(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmssSSS");
			return sdf.format(date);
		}

		public static String toYearMonthDontContainsSeparatorChar(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMM");
			return sdf.format(date);
		}

		public static String toYearMonthDayDontContainsSeparatorChar(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd");
			return sdf.format(date);
		}

		public static String toYearMonthDay(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
			return sdf.format(date);
		}
	}

	public static class Values
	{
		/**
		 * 从一个指定的日期类型中返回一个年月日的int值
		 * 
		 * @param date
		 * @return
		 */
		public static int getShortDate(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd");
			return Integer.parseInt(sdf.format(date));
		}

		/**
		 * 从一个指定的日期类型中返回一个时分秒的int值
		 * 
		 * @param date
		 * @return
		 */
		public static int getShortTime(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("HHmmss");
			return Integer.parseInt(sdf.format(date));
		}

		/**
		 * 从一个指定的日期类型中返回一个小时的int值
		 * 
		 * @param date
		 * @return
		 */
		public static int getHour(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("HH");
			return Integer.parseInt(sdf.format(date));
		}

		/**
		 * 从一个指定的日期类型中返回一个小时的int值
		 * 
		 * @param date
		 * @return
		 */
		public static int getMinute(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("mm");
			return Integer.parseInt(sdf.format(date));
		}

		/**
		 * 从一个指定的日期类型中返回一个小时的int值
		 * 
		 * @param date
		 * @return
		 */
		public static int getSecond(final Date date)
		{
			SimpleDateFormat sdf = new SimpleDateFormat("ss");
			return Integer.parseInt(sdf.format(date));
		}
	}

	/**
	 * 从字符串中解析时间
	 * 
	 * @author lukan@p-an.com 2013-9-26
	 */
	public static class Parser
	{
		/**
		 * 将常用的时间格式字符串(yyyy-mm-dd HH:mm:ss:SSS)转换为日期对象
		 * 
		 * @param dateStr
		 *            常用的时间格式字符串(yyyy-MM-dd HH:mm:ss:SSS)
		 * @return
		 */
		public static Date general(final String dateStr)
		{
			return parse(dateStr, General_Format);
		}

		/**
		 * 将指定的日期字符串转化为日期对象
		 * 
		 * @param dateStr
		 *            日期字符串
		 * @return java.util.Date
		 */
		public static Date parse(final String dateStr, String format)
		{
			if (Strings.isNullOrEmpty(dateStr))
			{
				return null;
			}
			if (Strings.isNullOrEmpty(format))
			{
				format = General_Format;
			}
			SimpleDateFormat df = new SimpleDateFormat(format);
			try
			{
				Date date = df.parse(dateStr);
				return date;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		/**
		 * 将短时间格式字符串(yyyyMMdd)转换为日期对象
		 * 
		 * @param strDate
		 *            短时间格式字符串(yyyyMMdd)
		 * @return
		 */
		public static Date parseShort(final String strDate)
		{
			SimpleDateFormat formatter = new SimpleDateFormat("yyyyMMdd");
			ParsePosition pos = new ParsePosition(0);
			Date strtodate = formatter.parse(strDate, pos);
			return strtodate;
		}

		/**
		 * 将指定的日期字符串(yyyyMMddHHmmss)转化为日期对象
		 * 
		 * @param dateStr
		 *            日期字符串
		 * @return java.util.Date
		 */
		public static Date parseSimple(final String dateStr)
		{
			return parse(dateStr, "yyyyMMddHHmmss");
		}
	}

	public static void main(final String[] args)
	{
		System.out.println("当前年：" + Current.currentYear());
		System.out.println("当前季：" + Current.currentSeason());
		System.out.println("当前月：" + Current.currentMonth());
		System.out.println("当前周：" + Current.currentWeek());
		System.out.println("周几了：" + Current.currentDayOfWeek());
		System.out.println("当前天：" + Current.currentToday());
		System.out.println("当前时：" + Current.currentHour());
		System.out.println("当前分：" + Current.currentMinute());
		System.out.println("当前秒：" + Current.currentSecond());
		System.out.println("======");
		System.out.println("　去年的第一天：" + First.firstDayLastYear().toLocaleString());
		System.out.println("　今年的第一天：" + First.firstDayYear().toLocaleString());
		System.out.println("现半年的第一天：" + First.firstDayHalfOfYear().toLocaleString());
		System.out.println("当前季的第一天：" + First.firstDaySeason().toLocaleString());
		System.out.println("当前月的第一天：" + First.firstDayMonth().toLocaleString());
		System.out.println("　半月的第一天：" + First.halfOfMonth().toLocaleString());
		System.out.println("当前周的第一天：" + First.firstDayWeek().toLocaleString());
		System.out.println("　　　　　昨天：" + yesterday().toLocaleString());
		System.out.println("　　　今天零时：" + First.timeOfToday().toLocaleString());
		System.out.println("　　　　本小时：" + First.timeOfHour().toLocaleString());
		System.out.println("　　　今天即时：" + now().toLocaleString());
		System.out.println("=========");
		System.out.println("　　　简时间串：" + Format.toShort(now()));
		System.out.println("=========");

		Date dateLeft = Parser.parseSimple("20100312181818");
		System.out.println("dateLeft >> " + dateLeft.toLocaleString());
		Date dateRight = Parser.parseSimple("20100312080808");
		System.out.println("dateRight >> " + dateRight.toLocaleString());
		System.out.println("如果left在right之后，返回false;反之,返回true;");
		System.out.println("dateLeft(左)与dateRight(右)的小时间隔>>>> " + Differ.byHours(dateLeft, dateRight));
		System.out.println("dateLeft(左)的子Time: " + splitTime(dateLeft));
		System.out.println("dateRight(右)的子Time: " + splitTime(dateRight));
		System.out.println("=========");

		System.out.println(splitTime(Parser.general("1998-12-31 00:00:01")));
		System.out.println(splitTime(Parser.general("1998-12-31 10:59:01")));
		System.out.println(splitTime(Parser.general("1998-12-31 11:59:01")));
		System.out.println(splitTime(Parser.general("1998-12-31 12:00:01")));
		System.out.println(splitTime(Parser.general("1998-12-31 13:00:01")));
		System.out.println(splitTime(Parser.general("1998-12-31 23:59:59")));

		System.out.println("=========");
		System.out.println(Values.getShortDate(Parser.general("1998-12-31 23:59:59")));
		System.out.println(Values.getShortDate(Parser.general("1998-12-31 01:59:59")));
	}

}
