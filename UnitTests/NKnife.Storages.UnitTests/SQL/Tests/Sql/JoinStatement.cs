using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Enums;
using NKnife.Storages.SQL.Realisations.Sql;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Sql
{

	
	public class JoinStatement
	{

		#region Join types

		[Fact]
		
		public void JoinPrimitivesInner()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			Join j = new Join("users");
			j.Append("id_user", "id");

			string result = j.GetSql("t");
			string sql = "INNER JOIN [users] ON [t].[id_user]=[users].[id]";

			Assert.Equal(result, sql);
		}

		[Fact]
		
		public void JoinPrimitivesLeft()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			Join j = new Join("users", type: JoinType.LEFT);
			j.Append("id_user", "id");

			string result = j.GetSql("t");
			string sql = "LEFT JOIN [users] ON [t].[id_user]=[users].[id]";

			Assert.Equal(result, sql);
		}

		[Fact]
		
		public void JoinPrimitivesRight()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			Join j = new Join("users", type: JoinType.RIGHT);
			j.Append("id_user", "id");

			string result = j.GetSql("t");
			string sql = "RIGHT JOIN [users] ON [t].[id_user]=[users].[id]";

			Assert.Equal(result, sql);
		}

		[Fact]
		
		public void JoinPrimitivesFull()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			Join j = new Join("users", type: JoinType.FULL);
			j.Append("id_user", "id");

			string result = j.GetSql("t");
			string sql = "FULL JOIN [users] ON [t].[id_user]=[users].[id]";

			Assert.Equal(result, sql);
		}

		#endregion

		#region Join simple

		[Fact]
		
		public void JoinPrimitives1()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			Join j = new Join("users");
			j.Append("id_user", "id").Append("id_admin", "id");

			string result = j.GetSql("t");
			string sql = "INNER JOIN [users] ON [t].[id_user]=[users].[id] AND [t].[id_admin]=[users].[id]";

			Assert.Equal(result, sql);
		}

		[Fact]
		
		public void JoinPrimitives2()
		{
			NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

			Join j = new Join("users", "u");
			j.Append("id_user", "id").Append("id_admin", "id");

			string result = j.GetSql("t");
			string sql = "INNER JOIN [users] as [u] ON [t].[id_user]=[u].[id] AND [t].[id_admin]=[u].[id]";

			Assert.Equal(result, sql);
		}

		#endregion

		#region Join many tables

		[Fact]
		
		public void JoinSimpleTwoTables()
		{
			JoinList list = new JoinList(NKnife.Storages.SQL.Common.SuperSql.DefaultFormatter);

			Join j1 = new Join("users");
			j1.Append("id_user", "id").Append("id_admin", "id");
			list.Append(j1);

			Join j2 = new Join("profiles", "p", JoinType.LEFT);
			j2.Append("id_profile", "id");
			list.Append(j2);

			string result = list.GetSql("t");
			string sql = "INNER JOIN [users] ON [t].[id_user]=[users].[id] AND [t].[id_admin]=[users].[id] LEFT JOIN [profiles] as [p] ON [t].[id_profile]=[p].[id]";

			Assert.Equal(result, sql);
		}

		#endregion

	}

}