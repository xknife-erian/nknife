package net.xknife.data.mongo;

import org.bson.types.ObjectId;

public class MongoUtil
{
	/**
	 * 获得一个由MongoDb驱动生成的自动ID。她是一个24位的字符串。
	 * 
	 * @return
	 */
	public static String mongoId()
	{
		return ObjectId.get().toString();
	}
}
