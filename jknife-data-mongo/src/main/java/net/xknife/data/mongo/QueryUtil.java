package net.xknife.data.mongo;

import org.mongojack.DBQuery;
import org.mongojack.DBQuery.Query;

public class QueryUtil
{
	public static Query page()
	{
		return null;
	}

	/**
	 * 登陆校验的Query封装
	 * 
	 * @param k
	 * @param v
	 * @param password
	 * @return
	 */
	public static Query login(final String k, final String v, final String password)
	{
		return DBQuery.is(k, v).is("password", password);
	}

	public static Query id(final String id)
	{
		return DBQuery.is("_id", id);
	}
}
