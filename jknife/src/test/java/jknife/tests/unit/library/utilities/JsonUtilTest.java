package jknife.tests.unit.library.utilities;

import java.io.IOException;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import net.xknife.jsonknife.JsonKnife;

import org.joda.time.DateTime;
import org.junit.Test;
import org.junit.runner.RunWith;

import com.fasterxml.jackson.core.JsonFactory;
import com.mycila.junit.concurrent.Concurrency;
import com.mycila.junit.concurrent.ConcurrentJunitRunner;

import static org.junit.Assert.*;

@RunWith(ConcurrentJunitRunner.class)
@Concurrency(value = 15)
public class JsonUtilTest
{
	public void testGetFactory()
	{
		for (int i = 0; i < 500; i++)
		{
			JsonFactory factory = JsonKnife.getFactory();
			assertNotNull(factory);
		}
	}

	@Test
	public void test0() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test1() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test2() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test3() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test4() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test5() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test6() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test7() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test8() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test9() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test10() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test11() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test12() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test13() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test14() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void test15() throws Throwable
	{
		testGetFactory();
	}

	@Test
	public void testToJson()
	{
		String json = null;
		try
		{
			json = JsonKnife.toJson(new TestBean());
		}
		catch (IOException e)
		{
			fail(e.getMessage());
		}
		assertTrue(json.length() > 2);
	}

	class TestBean
	{
		private String name;
		private String id;
		private boolean isOld;
		private int age;
		private float pay;
		private UUID uuid;
		private List<DateTime> times;
		private Date[] dates;

		public String getName()
		{
			return name;
		}

		public void setName(final String name)
		{
			this.name = name;
		}

		public String getId()
		{
			return id;
		}

		public void setId(final String id)
		{
			this.id = id;
		}

		public boolean isOld()
		{
			return isOld;
		}

		public void setOld(final boolean isOld)
		{
			this.isOld = isOld;
		}

		public int getAge()
		{
			return age;
		}

		public void setAge(final int age)
		{
			this.age = age;
		}

		public float getPay()
		{
			return pay;
		}

		public void setPay(final float pay)
		{
			this.pay = pay;
		}

		public UUID getUuid()
		{
			return uuid;
		}

		public void setUuid(final UUID uuid)
		{
			this.uuid = uuid;
		}

		public List<DateTime> getTimes()
		{
			return times;
		}

		public void setTimes(final List<DateTime> times)
		{
			this.times = times;
		}

		public Date[] getDates()
		{
			return dates;
		}

		public void setDates(final Date[] dates)
		{
			this.dates = dates;
		}
	}

}
