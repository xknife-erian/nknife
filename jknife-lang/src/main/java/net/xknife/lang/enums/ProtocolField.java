package net.xknife.lang.enums;

public enum ProtocolField
{
	/**
	 * 根节点
	 */
	ROOT("ROOT"),
	/**
	 * 请求命令字
	 */
	REQUEST("REQUEST"),
	/**
	 * 客户端Session
	 */
	SESSION("SESSION"),
	/**
	 * 回复命令字
	 */
	RESPONSE("RESPONSE"),
	/**
	 * 回复状态码
	 */
	STATUS("STATUS"),
	/**
	 * 数据节点
	 */
	DATAS("DATAS"), DATA("DATA"),
	/**
	 * 时间刻度
	 */
	TIMESTAMP("TIMESTAMP");

	private final String displayName;

	private ProtocolField(String display)
	{
		this.displayName = display;
	}

	/**
	 * @return 该节点的真实字符串
	 */
	public String getChar()
	{
		return this.displayName;
	}

	public static ProtocolField fromCode(String display)
	{
		for (ProtocolField type : ProtocolField.values())
		{
			if (type.getChar().equals(display))
			{
				return type;
			}
		}
		throw new IllegalArgumentException("协议节点不存在:" + display);
	}

}
