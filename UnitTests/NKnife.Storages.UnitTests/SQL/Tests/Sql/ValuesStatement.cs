using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Sql;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Sql
{

	
	public class ValuesStatement
	{

		[Fact]
		
		public void ValuesSimple1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ValueList w = new ValueList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			w.Append("a", "b", "c");
			string result = w.GetSql();
			string sql = "a, b, c";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void ValuesSimple2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			ValueList w = new ValueList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			w.AppendParameters("a", "b", "c");
			string result = w.GetSql();
			string sql = "@a, @b, @c";
			Assert.Equal(sql, result);
		}

	}

}
