package jknife.tests.unit.library;

import jknife.mocks.MockDI;
import net.xknife.j.library.DI;
import net.xknife.j.library.interfaces.ITranslateable;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import com.google.inject.AbstractModule;
import com.google.inject.Key;
import com.google.inject.name.Names;

import static org.junit.Assert.*;

public class DITest
{

	@Before
	public void setUp() throws Exception
	{
	}

	@After
	public void tearDown() throws Exception
	{
		DI.clear();
	}

	@Test
	public void testClear()
	{
		DI.registerModule(new StubEmptyModule(), new StubEmptyModule(), new StubEmptyModule());
		assertEquals(3, MockDI.getModules().size());
		DI.clear();
		assertEquals(0, MockDI.getModules().size());
		assertNull(MockDI.getInject());
	}

	@Test
	public void testGetInstanceClassOfT()
	{
		DI.registerModule(new AbstractModule()
		{
			@Override
			protected void configure()
			{
				binder().bind(ITranslateable.class).to(FakeTranslateImpl.class);
			}
		});
		DI.buildInjector();
		ITranslateable<?> tr = DI.getInstance(ITranslateable.class);
		assertNotNull(tr);
		assertTrue(tr instanceof FakeTranslateImpl);
	}

	@Test
	public void testGetInstanceKeyOfT()
	{
		DI.registerModule(new AbstractModule()
		{
			@Override
			protected void configure()
			{
				binder().bind(ITranslateable.class).annotatedWith(Names.named("1")).to(Fake1.class);
				binder().bind(ITranslateable.class).annotatedWith(Names.named("2")).to(Fake2.class);
				binder().bind(ITranslateable.class).annotatedWith(Names.named("3")).to(Fake3.class);
			}
		});
		DI.buildInjector();
		ITranslateable<?> t1 = DI.getInstance(Key.get(ITranslateable.class, Names.named("1")));
		assertNotNull(t1);
		assertTrue(t1 instanceof Fake1);
		ITranslateable<?> t2 = DI.getInstance(Key.get(ITranslateable.class, Names.named("2")));
		assertNotNull(t2);
		assertTrue(t2 instanceof Fake2);
		ITranslateable<?> t3 = DI.getInstance(Key.get(ITranslateable.class, Names.named("3")));
		assertNotNull(t3);
		assertTrue(t3 instanceof Fake3);
	}

	// //**************************************

	static class Fake1 implements ITranslateable<String>
	{
		@Override
		public String translate()
		{
			return null;
		}

		@Override
		public void translate(final String entity)
		{
		}
	}

	static class Fake2 implements ITranslateable<String>
	{
		@Override
		public String translate()
		{
			return null;
		}

		@Override
		public void translate(final String entity)
		{
		}
	}

	static class Fake3 implements ITranslateable<String>
	{
		@Override
		public String translate()
		{
			return null;
		}

		@Override
		public void translate(final String entity)
		{
		}
	}

	static class FakeTranslateImpl implements ITranslateable<String>
	{
		@Override
		public String translate()
		{
			return null;
		}

		@Override
		public void translate(final String entity)
		{
		}
	}

	class StubEmptyModule extends AbstractModule
	{
		@Override
		protected void configure()
		{
		}
	}
}
