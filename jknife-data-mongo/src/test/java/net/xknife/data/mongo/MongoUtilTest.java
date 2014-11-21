package net.xknife.data.mongo;

import java.util.ArrayList;

import net.xknife.data.mongo.MongoUtil;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import com.google.common.base.Strings;

import static org.junit.Assert.*;

public class MongoUtilTest
{

	@Before
	public void setUp() throws Exception
	{
	}

	@After
	public void tearDown() throws Exception
	{
	}

	@Test
	public void mongoIdTest()
	{
		int expected = 100000;
		ArrayList<String> ids = new ArrayList<String>(expected);
		for (int i = 0; i < expected; i++)
		{
			ids.add(MongoUtil.mongoId());
		}
		assertEquals(expected, ids.size());
		assertFalse(Strings.isNullOrEmpty(ids.get(expected - 1)));
	}
}
