package net.xknife.data.enums;

/**
 * @author lukan@jeelu.com
 * 
 */
public enum DataType
{
	/**
	 * 固定长度的字符串
	 */
	CHAR,
	/**
	 * 通常用来表示最多为255个字符的变量长度字符串
	 */
	VARCHAR,
	/**
	 * 从 0 到 255 的整型数据。存储大小为 1 字节。
	 */
	TINYINT,
	/**
	 * 整数，从-32000到 +32000范围
	 */
	SMALLINT,
	/**
	 * 整数，从-2000000000 到 +2000000000 范围
	 */
	INT,
	/**
	 * 不能用SMALLINT 或 INT描述的超大整数。
	 */
	BIGINT,
	/**
	 * 单精度浮点型数据,存储小数数据,例如:测量，温度
	 */
	FLOAT,
	/**
	 * 双精度浮点型数据,需要双精度存储的小数数据,例如:科学数据
	 */
	DOUBLE,
	/**
	 * 用户自定义精度的浮点型数据。以特别高的精度存储小数数据。如:货币数额，科学数据
	 */
	DECIMAL,
	/**
	 * TEXT可以接受文本输入，VARCHAR只能接受255个字符，但是TEXT可以用来存储超量的数据
	 */
	TEXT,
	/**
	 * 日期
	 */
	DATE,
	/**
	 * 日期时间
	 */
	DATETIME,
	/**
	 * 记录即时时间
	 */
	TIMESTAMP,

	// ----------oracle--------------
	NUMBER, VARCHAR2, INTEGER
}
