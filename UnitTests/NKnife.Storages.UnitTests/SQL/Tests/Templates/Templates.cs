using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces.Templates;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Templates
{

	
	public class Templates
	{

		[Fact]
		
		public void TemplateSelect()
		{
			ITemplate t = TemplateLibrary.Select;
			t.Append(SnippetLibrary.Table("users"),
				SnippetLibrary.Columns("*"),
				SnippetLibrary.Where("age>=18"),
				SnippetLibrary.OrderBy("age ASC"));
			string sql = t.GetSql();
			Assert.Equal(sql, "SELECT * FROM [users] WHERE age>=18 ORDER BY age ASC;");
		}

		[Fact]
		
		public void TemplateUpdate()
		{
			ITemplate t = TemplateLibrary.Update;
			t.Append(SnippetLibrary.Table("users"),
				SnippetLibrary.Sets("[a]=@a,[b]=@b,[c]=@c"),
				SnippetLibrary.Where("age>=18"));
			string sql = t.GetSql();
			Assert.Equal(sql, "UPDATE [users] SET [a]=@a,[b]=@b,[c]=@c WHERE age>=18;");
		}

		[Fact]
		
		public void TemplateInsert()
		{
			ITemplate t = TemplateLibrary.Insert;
			t.Append(SnippetLibrary.Table("users"),
				SnippetLibrary.Columns("[a],[b],[c]"),
				SnippetLibrary.Values("@a,@b,@c"));
			string sql = t.GetSql();
			Assert.Equal(sql, "INSERT INTO [users]([a],[b],[c]) VALUES(@a,@b,@c);");
		}

		[Fact]
		
		public void TemplateDelete()
		{
			ITemplate t = TemplateLibrary.Delete;
			t.Append(SnippetLibrary.Table("users"),
				SnippetLibrary.Where("[id]=@id"));
			string sql = t.GetSql();
			Assert.Equal(sql, "DELETE FROM [users] WHERE [id]=@id;");
		}

		[Fact]
		
		public void TemplateEndOfStatementSelect()
		{
			ITemplate t = TemplateLibrary.Select;
			t.Append(SnippetLibrary.Table("users"),
				SnippetLibrary.Columns("*"),
				SnippetLibrary.Where("age>=18"),
				SnippetLibrary.OrderBy("age ASC"));
			string sql = t.GetSql(false);
			Assert.Equal(sql, "SELECT * FROM [users] WHERE age>=18 ORDER BY age ASC");
		}

	}

}
