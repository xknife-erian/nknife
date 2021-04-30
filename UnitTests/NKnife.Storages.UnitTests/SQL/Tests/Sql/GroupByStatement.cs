using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Sql;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Sql
{

	
	public class GroupByStatement
	{

		[Fact]
		
		public void GroupBySimpleList1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);

			g.Append(false, "a", "b", "c");
			string result = g.GetSql();
			string sql = "[a], [b], [c]";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void GroupBySimpleList2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.Append(false, "a", "b", "c");

			string result = g.GetSql();
			string sql = "[a], [b], [c]";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void GroupBySimpleList3()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.Append(true, "a", "b", "c");

			string result = g.GetSql();
			string sql = "[a], [b], [c]";
			Assert.Equal(sql, result);
			Assert.Equal(c.Count, g.Count);
		}

		[Fact]
		
		public void GroupBySimpleListAlias()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);

			g.Append(false, "a", "b", "c");
			string result = g.GetSql("t");
			string sql = "[t].[a], [t].[b], [t].[c]";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void GroupByAggregation1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.FuncMax("sm");

			string result1 = g.GetSql();
			string sql1 = "[sm]";
			string result2 = c.GetSql();
			string sql2 = "MAX([sm])";

			Assert.Equal(sql1, result1);
			Assert.Equal(sql2, result2);
			Assert.Equal(c.Count, g.Count);
		}

		[Fact]
		
		public void GroupByAggregation2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.FuncMin("sm");

			string result1 = g.GetSql();
			string sql1 = "[sm]";
			string result2 = c.GetSql();
			string sql2 = "MIN([sm])";

			Assert.Equal(sql1, result1);
			Assert.Equal(sql2, result2);
			Assert.Equal(c.Count, g.Count);
		}

		[Fact]
		
		public void GroupByAggregation3()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.FuncCount("sm");

			string result1 = g.GetSql();
			string sql1 = "[sm]";
			string result2 = c.GetSql();
			string sql2 = "COUNT([sm])";

			Assert.Equal(sql1, result1);
			Assert.Equal(sql2, result2);
			Assert.Equal(c.Count, g.Count);
		}

		[Fact]
		
		public void GroupByAggregation4()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.FuncSum("sm");

			string result1 = g.GetSql();
			string sql1 = "[sm]";
			string result2 = c.GetSql();
			string sql2 = "SUM([sm])";

			Assert.Equal(sql1, result1);
			Assert.Equal(sql2, result2);
			Assert.Equal(c.Count, g.Count);
		}

		[Fact]
		
		public void GroupByAggregationMany()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.FuncSum("sm", "asm");
			g.FuncMax("mx", "amx");
			g.FuncMin("mn", "amn");
			g.FuncCount("fg", "acn");

			string result1 = g.GetSql();
			string sql1 = "[sm], [mx], [mn], [fg]";
			string result2 = c.GetSql();
			string sql2 = "SUM([sm]) as 'asm', MAX([mx]) as 'amx', MIN([mn]) as 'amn', COUNT([fg]) as 'acn'";

			Assert.Equal(sql1, result1);
			Assert.Equal(sql2, result2);
			Assert.Equal(c.Count, g.Count);
		}

		[Fact]
		
		public void GroupByRaw1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.Raw("[t].[column]");

			string result = g.GetSql();
			string sql = "[t].[column]";

			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void GroupByRaw2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.Raw("[t].[column]", "[g].[guid]");

			string result = g.GetSql();
			string sql = "[t].[column], [g].[guid]";

			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void GroupByAlias1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.Append(false, "c1");
			g.SetTableAlias("oh2");
			g.Append(false, "c2");
			g.SetTableAlias("oh3");
			g.Append(false, "c3");
			g.SetTableAlias();
			g.Append(false, "c4");
			g.Raw("[t].[column]");

			string result = g.GetSql("t");
			string sql = "[t].[c1], [oh2].[c2], [oh3].[c3], [t].[c4], [t].[column]";

			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void GroupByAlias2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ColumnsListAggregation c = new ColumnsListAggregation(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			GroupByList g = new GroupByList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter, c);
			g.Append(false, "c1");
			g.SetTableAlias("oh2");
			g.Append(false, "c2");
			g.SetTableAlias("oh3");
			g.Append(false, "c3");
			g.SetTableAlias();
			g.Append(false, "c4");
			g.Raw("[t].[column]");

			string result = g.GetSql();
			string sql = "[c1], [oh2].[c2], [oh3].[c3], [c4], [t].[column]";

			Assert.Equal(sql, result);
		}

	}

}