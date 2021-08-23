using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Sql;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Sql
{

	
	public class OrderByStatement
	{

		[Fact]
		
		public void OrderByASCSimple1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Ascending("a");
			string result = o.GetSql();
			string sql = "[a] ASC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByASCSimple2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Ascending("a", "b", "c");
			string result = o.GetSql();
			string sql = "[a] ASC, [b] ASC, [c] ASC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByDESCSimple1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Descending("a");
			string result = o.GetSql();
			string sql = "[a] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByDESCSimple2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Descending("a", "b", "c");
			string result = o.GetSql();
			string sql = "[a] DESC, [b] DESC, [c] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByASCAndDESC1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Ascending("a");
			o.Descending("b");
			o.Ascending("c");
			o.Descending("d");
			string result = o.GetSql();
			string sql = "[a] ASC, [b] DESC, [c] ASC, [d] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByASCAndDESC2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Ascending("a", "b", "c");
			o.Descending("d", "e", "f");
			string result = o.GetSql();
			string sql = "[a] ASC, [b] ASC, [c] ASC, [d] DESC, [e] DESC, [f] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByASCAndDESCAlias()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Ascending("a");
			o.Descending("b");
			o.Ascending("c");
			o.Descending("d");
			string result = o.GetSql("t");
			string sql = "[t].[a] ASC, [t].[b] DESC, [t].[c] ASC, [t].[d] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByRaw1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Raw("[c] ASC, [d] DESC");
			string result = o.GetSql("t");
			string sql = "[c] ASC, [d] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByRaw2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Raw("[c] ASC").Raw("[d] DESC");
			string result = o.GetSql("t");
			string sql = "[c] ASC, [d] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByTableAlias1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Raw("[a] ASC");
			o.Ascending("as1");
			o.Descending("ds2");
			o.SetTableAlias("ttt");
			o.Ascending("at1");
			o.SetTableAlias("ddd");
			o.Descending("at2");
			o.SetTableAlias();
			o.Descending("b");

			string result = o.GetSql("t");
			string sql = "[a] ASC, [t].[as1] ASC, [t].[ds2] DESC, [ttt].[at1] ASC, [ddd].[at2] DESC, [t].[b] DESC";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void OrderByTableAlias2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			OrderByList o = new OrderByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			o.Raw("[a] ASC");
			o.Ascending("as1");
			o.Descending("ds2");
			o.SetTableAlias("ttt");
			o.Ascending("at1");
			o.SetTableAlias("ddd");
			o.Descending("at2");
			o.SetTableAlias();
			o.Descending("b");

			string result = o.GetSql();
			string sql = "[a] ASC, [as1] ASC, [ds2] DESC, [ttt].[at1] ASC, [ddd].[at2] DESC, [b] DESC";
			Assert.Equal(sql, result);
		}

	}

}