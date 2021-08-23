using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Linq;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Linq
{

	
	public class LinqInsert
	{

		[Fact]
		public void LinqInsertSimple()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			var q1 = new Insert<Author>(false);
			q1.Columns(x => x.Append("a", "b", "c")).Values(x=>x.Append("a", "b", "c"));
			string result = q1.GetSql();
			string sql = "INSERT INTO [tab_authors]([a], [b], [c]) VALUES(a, b, c);";
			Assert.Equal(result, sql);
		}

		[Fact]
		public void LinqQueryInsertSimple()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			string result = Query<Author>.CreateInsert(false).Columns(x => x.Append("a", "b", "c")).Values(x => x.Append("a", "b", "c")).GetSql();
			string sql = "INSERT INTO [tab_authors]([a], [b], [c]) VALUES(a, b, c);";
			Assert.Equal(result, sql);
		}

	}

}