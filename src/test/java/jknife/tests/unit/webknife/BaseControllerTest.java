package jknife.tests.unit.webknife;

import jknife.mocks.MockController;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class BaseControllerTest
{
	private MockController _MockControler;

	@Before
	public void setUp() throws Exception
	{
		_MockControler = new MockController();
		_MockControler.fillServletMethodMap();
	}

	@After
	public void tearDown() throws Exception
	{
	}

	@Test
	public void testFillServletMethodMap()
	{
		assertEquals(3, _MockControler.getMap().size());
	}

}
