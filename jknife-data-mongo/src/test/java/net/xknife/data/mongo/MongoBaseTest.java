package net.xknife.data.mongo;

import java.io.File;
import java.util.List;
import java.util.regex.Pattern;

import net.xknife.data.mongo.IMongo.MongoBase;
import net.xknife.lang.widgets.OS;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.mongojack.DBCursor;
import org.mongojack.DBQuery;
import org.mongojack.DBQuery.Query;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.google.common.collect.Lists;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertNull;
import static org.junit.Assert.assertTrue;

/**
 * 
 * @author yangjuntao@jeelu.com 2013年11月21日
 */
public class MongoBaseTest
{
	static String configFilePath = MongoBaseTest.class.getResource("/").getFile() + OS.file_separator() + "Log4j2.xml";
	static Logger _Logger = LoggerFactory.getLogger(MongoBaseTest.class);
	static
	{
		File file = new File(configFilePath);
		System.setProperty("log4j.configurationFile", file.getAbsolutePath());
		_Logger.trace("日志启动");
	}

	private MockBikeMongo _BikeMongo;

	@Before
	public void setUp() throws Exception
	{

		initServer();
	}

	@After
	public void tearDown() throws Exception
	{
	}

	private void initServer()
	{
		_BikeMongo = new MockBikeMongo();
	}

	/**
	 * 添加一个BigBike到数据库，检测是否成功；
	 * 
	 * 根据Id从数据库查找BigBike,检测是否相同；
	 */
	@Test
	public void testAdd_Base()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		boolean isSuccess = _BikeMongo.add(bigBike);
		assertTrue(isSuccess);

		BigBike finded = _BikeMongo.find(bigBike.getId());
		assertEquals(bigBike.getName(), finded.getName());
	}

	/**
	 * 添加一个BigBike到数据库，检测是否成功；
	 * 
	 * 再添加一次相同的对象，检测是否成功；
	 * 
	 * 根据Id从数据库查找BigBike,检测是否相同；
	 */
	@Test
	public void testAdd_TwoSameObject()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		boolean isSuccess = _BikeMongo.add(bigBike);
		assertTrue(isSuccess);

		isSuccess = _BikeMongo.add(bigBike);
		assertFalse(isSuccess);

		BigBike findedBb = _BikeMongo.find(bigBike.getId());
		assertEquals(bigBike, findedBb);
	}

	/**
	 * 
	 * 对实体尽心删除操作，检测再次添加相同实体是否成功。过程如下：
	 * 
	 * 1.添加一个实体，检测是否添加成功；
	 * 
	 * 2.根据实体唯一标识删除实体，检测是否删除成功；
	 * 
	 * 3.再次添加相同实体，检测是否添加成功；
	 * 
	 * 4.根据实体唯一标识查找实体，检测是否理想添加一致；
	 * 
	 * 5.再次添加相同实体，检测是否添加成功；
	 */
	@Test
	public void testAdd_DeleteAndAdd()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		boolean isSuccess = _BikeMongo.add(bigBike);
		assertTrue(isSuccess);

		boolean isDelete = _BikeMongo.delete(bigBike.getId());
		assertTrue(isDelete);

		isSuccess = _BikeMongo.add(bigBike);
		assertTrue(isSuccess);

		BigBike findedBb = _BikeMongo.find(bigBike.getId());
		assertEquals(bigBike, findedBb);

		isSuccess = _BikeMongo.add(bigBike);
		assertFalse(isSuccess);
	}

	/**
	 * 批量添加的测试：//
	 * 
	 * 批量添加实体，检测添加成功的实体个数；
	 * 
	 * 循环：根据需要添加的实体的唯一标识查找到实体，检测添加的实体是否理想；
	 */
	@Test
	public void testAddListOfT()
	{
		List<BigBike> willAddBikes = Lists.newArrayList();
		for (int i = 0; i < 300; i++)
		{
			willAddBikes.add(BigBike.newBigBike(false));
		}
		int addedCount = _BikeMongo.add(willAddBikes);
		assertEquals(willAddBikes.size(), addedCount);

		BigBike finded = null;
		for (int j = 0; j < 300; j++)
		{
			finded = _BikeMongo.find(willAddBikes.get(j).getId());
			assertEquals(finded.getName(), willAddBikes.get(j).getName());
		}
	}

	/**
	 * 根据实体唯一标识删除实体，检测是否删除成功；并对数据库进行回查进一步检测。
	 */
	@Test
	public void testDelete_Base()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		boolean isSuccess = _BikeMongo.add(bigBike);
		assertTrue(isSuccess);

		boolean isDelete = _BikeMongo.delete(bigBike.getId());
		assertTrue(isDelete);

		BigBike findedBb = _BikeMongo.find(bigBike.getId());
		assertNull(findedBb);
	}

	/**
	 * 
	 * 根据唯一标识删除，对剩余数据量进行检测。
	 * 
	 */
	@Test
	public void testDelete_Count()
	{
		_BikeMongo.drop();

		List<BigBike> willAdd = Lists.newArrayList();
		for (int i = 0; i < 300; i++)
		{
			willAdd.add(BigBike.newBigBike(false));
		}
		int addedCount = _BikeMongo.add(willAdd);
		assertEquals(300, addedCount);

		boolean isDeleted = _BikeMongo.delete(willAdd.get(100).getId());
		assertTrue(isDeleted);

		DBCursor<BigBike> finded = _BikeMongo.query();
		assertEquals(299, finded.size());

	}

	/**
	 * 删除符合条件的唯一实体，通过数据库回查进行检测。
	 */
	@Test
	public void testRemove_BaseOne()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		boolean isSuccess = _BikeMongo.add(bigBike);
		assertTrue(isSuccess);

		Query where = DBQuery.is("name", bigBike.getName());

		int removeCount = _BikeMongo.remove(where);
		assertEquals(1, removeCount);
		DBCursor<BigBike> finded = _BikeMongo.query(where);
		assertEquals(0, finded.size());
	}

	/**
	 * 删除符合条件的所有实体，通过数据库回查进行检测。
	 */
	@Test
	public void testRemove_BaseAll()
	{
		_BikeMongo.drop();

		List<BigBike> willAdd = Lists.newArrayList();
		for (int i = 0; i < 30; i++)
		{
			willAdd.add(BigBike.newBigBike());
		}
		int addedCount = _BikeMongo.add(willAdd);
		assertEquals(30, addedCount);

		Query query = DBQuery.is("name", "vyyvyy");

		int removeCount = _BikeMongo.remove(query);
		assertEquals(30, removeCount);
		DBCursor<BigBike> finded = _BikeMongo.query(query);
		assertEquals(0, finded.size());
	}

	/**
	 */
	@Test
	public void testRemove_ByName()
	{
		Query query = DBQuery.is("name", "vyyvyy");
		long oldBikesCount = _BikeMongo.count(query);

		List<BigBike> bigBikes = Lists.newArrayList();
		for (int j = 0; j < 300; j++)
		{
			bigBikes.add(BigBike.newBigBike());
		}
		_BikeMongo.add(bigBikes);

		int removedCount = _BikeMongo.remove(query);
		assertEquals(oldBikesCount + 300, removedCount);

		long yongjiuBikesCount = _BikeMongo.count(query);
		assertEquals(0, yongjiuBikesCount);

	}

	/**
	 * 根据不存在的条件删除数据
	 */
	@Test
	public void testRemove_NotExist()
	{
		Query where = DBQuery.regex("name", Pattern.compile("$bmw"));
		long removedBwmCount = _BikeMongo.remove(where);
		assertEquals(0, removedBwmCount);
	}

	/**
	 * 对一个不存在的实体进行更新，检测是否成功；
	 */
	@Test
	public void testUpdateById_NotExist()
	{
		BigBike baseBigBike = BigBike.newBigBike(false);
		BigBike findedBigBike = _BikeMongo.find(baseBigBike.getId());
		assertNull(findedBigBike);

		baseBigBike.setName("0000_newBigBike");
		BigBike updatedBigBike = _BikeMongo.updateById(baseBigBike.getId(), baseBigBike);
		assertNull(updatedBigBike);
	}

	/**
	 * 对一个存在的实体进行更新，检测是否成功；
	 */
	@Test
	public void testUpdateById_Exist()
	{
		_BikeMongo.drop();

		BigBike baseBigBike = BigBike.newBigBike(false);
		_BikeMongo.add(baseBigBike);

		BigBike willUpdateBigBike = new BigBike();
		willUpdateBigBike.setId(baseBigBike.getId());
		willUpdateBigBike.setName("0000_newBigBike");
		BigBike updatedBigBike = _BikeMongo.updateById(baseBigBike.getId(), willUpdateBigBike);

		assertEquals(baseBigBike.getId(), updatedBigBike.getId());
		assertNotEquals(updatedBigBike.getName(), baseBigBike.getName());
	}

	/**
	 * 对一个存在的实体进行更新，但如果更新后不唯一，检测是否成功；
	 */
	@Test
	public void testUpdateById_ExistAndUpdatedNoUnique()
	{
		_BikeMongo.drop();

		BigBike baseBigBike = BigBike.newBigBike(false);
		_BikeMongo.add(baseBigBike);
		BigBike baseBigBike2 = BigBike.newBigBike(false);
		_BikeMongo.add(baseBigBike2);

		BigBike updatedBigBike = _BikeMongo.updateById(baseBigBike2.getId(), baseBigBike);

		assertNull(updatedBigBike);

	}

	/**
	 * 根据条件批量更新实体，数据库中其实并不存在这样条件实体，根据更新数量检测是否成功；
	 */
	@Test
	public void testUpdate_NotExist()
	{
		_BikeMongo.drop();

		Query notExistWhere = DBQuery.is("name", "$wuuhyou");
		long findedBikes = _BikeMongo.count(notExistWhere);
		assertEquals(0, findedBikes);

		List<BigBike> bigBikes = Lists.newArrayList();
		for (int i = 0; i < 100; i++)
		{
			bigBikes.add(BigBike.newBigBike());
		}
		_BikeMongo.add(bigBikes);
		findedBikes = _BikeMongo.count(DBQuery.is("name", "vyyvyy"));
		assertEquals(100, findedBikes);

		BigBike willUpdateBigBike = new BigBike();
		willUpdateBigBike.setName("0000_newBigBike");
		List<String> findedNewBigBikes = _BikeMongo.update(notExistWhere, willUpdateBigBike);
		assertEquals(0, findedNewBigBikes.size());
	}

	/**
	 * 根据条件批量更新实体，数据库中的确存在这样条件的实体，根据更新数量检测是否成功；
	 */
	@Test
	public void testUpdate_AllExist()
	{
		_BikeMongo.drop();

		Query oldWhere = DBQuery.is("name", "vyyvyy");
		long oldFindedCount = _BikeMongo.count(oldWhere);

		List<BigBike> bigBikes = Lists.newArrayList();
		for (int i = 0; i < 100; i++)
		{
			bigBikes.add(BigBike.newBigBike());
		}
		_BikeMongo.add(bigBikes);

		BigBike willUpdateBike = new BigBike();
		willUpdateBike.setName("vyy");

		List<String> updated = _BikeMongo.update(oldWhere, willUpdateBike);

		assertEquals(100 + oldFindedCount, updated.size());

	}

	/**
	 * 查询一个id为null实体，检测结果是否为Null。
	 */
	@Test
	public void testFind_Null()
	{
		BigBike findedBike = null;
		findedBike = _BikeMongo.find(null);
		assertNull(findedBike);
	}

	/**
	 * 查询一个id为不存的实体，检查结果是否为Null。
	 */
	@Test
	public void testFind_NotExist()
	{
		BigBike findedBike = null;
		findedBike = _BikeMongo.find("aaaaaaaaaaaaaaaaaaaaaaaa");
		assertNull(findedBike);

		findedBike = _BikeMongo.find("abcdef");
		assertNull(findedBike);
	}

	/**
	 * 查询一个id为“”实体，检测结果是否为Null。
	 */
	@Test
	public void testFind_Empty()
	{
		BigBike findedBike = null;
		findedBike = _BikeMongo.find("");
		assertNull(findedBike);
	}

	/**
	 * 查找一个真实存在的实体，检测结果是否正确。
	 */
	@Test
	public void testFind_Base()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		BigBike findedBike = null;

		findedBike = _BikeMongo.find(bigBike.getId());
		assertNull(findedBike);

		_BikeMongo.add(bigBike);
		findedBike = _BikeMongo.find(bigBike.getId());
		assertNotNull(findedBike);
		assertEquals(bigBike.getId(), findedBike.getId());
		assertEquals(bigBike.getName(), findedBike.getName());
		assertEquals(bigBike, findedBike);
	}

	/**
	 * 查找条件为null，检测结果。
	 */
	@Test
	public void testFindOne_Null()
	{
		BigBike findedBike = null;
		findedBike = _BikeMongo.findOne(null);
		assertNull(findedBike);
	}

	/**
	 * 查找一个不存在的实体，检测结果。
	 */
	@Test
	public void testFindOne_NotExist()
	{
		BigBike findedBike = null;
		findedBike = _BikeMongo.findOne(DBQuery.is("name", "sanhuanAsihuan"));
		assertNull(findedBike);
	}

	/**
	 * 查找一个真实存在的实体，检测结果是否正确。
	 */
	@Test
	public void testFindOne_Base()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		BigBike findedBike = null;

		findedBike = _BikeMongo.findOne(DBQuery.is("name", bigBike.getName()));
		assertNull(findedBike);

		_BikeMongo.add(bigBike);
		findedBike = _BikeMongo.findOne(DBQuery.is("name", bigBike.getName()));
		assertNotNull(findedBike);
		assertEquals(bigBike.getId(), findedBike.getId());
		assertEquals(bigBike.getName(), findedBike.getName());
		assertEquals(bigBike, findedBike);
	}

	/**
	 * 查找一个真实存在的实体，检测结果是否正确。
	 */
	@Test
	public void testFindOne_BaseById()
	{
		BigBike bigBike = BigBike.newBigBike(false);
		BigBike findedBike = null;

		findedBike = _BikeMongo.findOne(DBQuery.is("_id", bigBike.getId()));
		assertNull(findedBike);

		_BikeMongo.add(bigBike);
		findedBike = _BikeMongo.findOne(DBQuery.is("_id", bigBike.getId()));
		assertNotNull(findedBike);
		assertEquals(bigBike.getId(), findedBike.getId());
		assertEquals(bigBike.getName(), findedBike.getName());
		assertEquals(bigBike, findedBike);
	}

	/**
	 * 获取数据库中已经存在的数据（M：数据1），批量添加一定数量的数据，根据数量检测添加是否成功；
	 * 
	 * 再次获取数据库中存在的数据并排除“数据1”后的数据（M：数据2），检测期望添加的数据是否与数据2相同。
	 */
	@Test
	public void testQuery()
	{
		List<BigBike> list = Lists.newArrayList();
		for (int i = 0; i < 10; i++)
		{
			list.add(BigBike.newBigBike());
		}
		_BikeMongo.add(list);

		DBCursor<BigBike> findedOldBikes = _BikeMongo.query();
		List<BigBike> oldList = findedOldBikes.toArray();

		List<BigBike> willAddList = Lists.newArrayList();
		for (int i = 0; i < 202; i++)
		{
			willAddList.add(new BigBike());
		}
		int addedCount = _BikeMongo.add(willAddList);
		assertEquals(202, addedCount);

		DBCursor<BigBike> findedNowBikes = _BikeMongo.query();
		List<BigBike> nowList = findedNowBikes.toArray();
		nowList.removeAll(oldList);

		assertEquals(202, nowList.size());

	}

	/**
	 * 根据不存在的条件查询实体 ，检测结果是否正确。
	 */
	@Test
	public void testQueryQuery_Empty()
	{
		List<BigBike> willAddBikes = Lists.newArrayList();
		for (int i = 0; i < 300; i++)
		{
			willAddBikes.add(BigBike.newBigBike(((i % 2) == 0 ? true : false)));
		}
		_BikeMongo.add(willAddBikes);

		DBCursor<BigBike> bikes = null;
		bikes = _BikeMongo.query(DBQuery.is("name", ""));
		assertEquals(0, bikes.size());
	}

	/**
	 * 
	 */
	@Test
	public void testQueryQuery_Base()
	{
		_BikeMongo.drop();

		List<BigBike> willAddBikes = Lists.newArrayList();
		for (int i = 0; i < 300; i++)
		{
			willAddBikes.add(BigBike.newBigBike());
		}
		_BikeMongo.add(willAddBikes);

		DBCursor<BigBike> findedBikes = null;
		findedBikes = _BikeMongo.query(DBQuery.is("name", "vyyvyy"));

		assertEquals(300, findedBikes.size());
		assertTrue(willAddBikes.containsAll(findedBikes.toArray()));
		assertTrue(findedBikes.toArray().containsAll(willAddBikes));
	}

	/**
	 * 
	 */
	@Test
	public void testCountCount()
	{
		Query where$Bike = DBQuery.is("name", "vyyvyy");

		long oldFindedCount = _BikeMongo.count(where$Bike);

		List<BigBike> bigBikes = Lists.newArrayList();
		for (int i = 0; i < 100; i++)
		{
			bigBikes.add(BigBike.newBigBike());
		}
		int addedBikeCount = _BikeMongo.add(bigBikes);
		assertEquals(100, addedBikeCount);

		long nowBikeCount = _BikeMongo.count(where$Bike);
		assertEquals(100 + oldFindedCount, nowBikeCount);
	}

	@Test
	public void testCount()
	{
		long oldcount = _BikeMongo.count();

		List<BigBike> list = Lists.newArrayList();
		for (int i = 0; i < 300; i++)
		{
			list.add(BigBike.newBigBike());
		}
		_BikeMongo.add(list);

		long nowcount = _BikeMongo.count();

		assertEquals(oldcount + list.size(), nowcount);
	}

	public class MockBikeMongo extends MongoBase<BigBike>
	{
		Logger _Logger = LoggerFactory.getLogger(MockBikeMongo.class);

		/**
		 * 用一个模拟的Mongo服务器进行测试。第三方专用的Mongo测试框架。
		 */
		// @Override
		// protected ServerAddress getServer()
		// {
		// MongoServer server = new MongoServer(new MemoryBackend());
		// InetSocketAddress address = server.bind();
		// return new ServerAddress(address);
		// }

		@Override
		protected String getDbName()
		{
			return "test";
		}

		@Override
		protected String getCollectionName()
		{
			return "testCollection";
		}

		@Override
		protected String getSubCollectionName()
		{
			return "testSubCollection";
		}

		@Override
		protected Class<BigBike> getEntityClass()
		{
			return BigBike.class;
		}

		@Override
		protected void ensureIndex()
		{
			collection().ensureIndex("name");
		}

		/**
		 * 测试使用
		 */
		public void drop()
		{
			collection().drop();
		}
	}
}
