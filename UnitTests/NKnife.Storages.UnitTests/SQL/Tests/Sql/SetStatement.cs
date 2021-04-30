using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Sql;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Sql
{

	
	public class SetStatement
	{

		[Fact]
		
		public void ValuesSimple1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			SetList w = new SetList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			w.Append("a", "b", "c");
			string result = w.GetSql();
			string sql = "[a]=@a, [b]=@b, [c]=@c";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void ValuesSimple2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			SetList w = new SetList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			w.AppendValue("a", "NOW()").AppendValue("b", "100").AppendValue("c", "NULL");
			string result = w.GetSql();
			string sql = "[a]=NOW(), [b]=100, [c]=NULL";
			Assert.Equal(sql, result);
		}

		[Fact]
		
		public void ValuesSimpleAlias()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			SetList w = new SetList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);
			w.AppendValue("a", "NOW()").AppendValue("b", "100").AppendValue("c", "NULL");
			string result = w.GetSql("t");
			string sql = "[t].[a]=NOW(), [t].[b]=100, [t].[c]=NULL";
			Assert.Equal(sql, result);
		}

	}

}
