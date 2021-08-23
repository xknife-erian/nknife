using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Queries
{
    public class Update
    {
        [Fact]
        public void QueryUpdateAlias()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var u = new Update<Author>(false, "t");
            u.Sets.Append("a");
            u.Sets.AppendValue("d", "1").AppendValue("e", "NOW()");
            u.Where.Equal("id");

            var result = u.GetSql();
            var sql = "UPDATE [tab_authors] as [t] SET [t].[a]=@a, [t].[d]=1, [t].[e]=NOW() WHERE [t].[id]=@id;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryUpdateSimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var u = new Update<Author>();
            u.Sets.Append("a", "b", "c");
            u.Sets.AppendValue("d", "1").AppendValue("e", "NOW()");

            var result = u.GetSql();
            var sql = "UPDATE [tab_authors] SET [a]=@a, [b]=@b, [c]=@c, [d]=1, [e]=NOW();";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryUpdateSimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var u = new Update<Author>();
            u.Sets.Append("a", "b", "c");
            u.Where.Equal("d", "e", "f");

            var result = u.GetSql();
            var sql = "UPDATE [tab_authors] SET [a]=@a, [b]=@b, [c]=@c WHERE [d]=@d AND [e]=@e AND [f]=@f;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void UpdateMapping1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var u = new Update<Author>(true);

            var result = u.GetSql();
            var sql = "UPDATE [tab_authors] SET [created_at]=@created_at, [firstname]=@firstname, [lastname]=@lastname;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void UpdateMapping2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var u = new Update<Book>(true);

            var result = u.GetSql();
            var sql = "UPDATE [tab_books] SET [created_at]=@created_at, [name]=@name, [year]=@year, [id_author]=@id_author, [id_publisher]=@id_publisher, [id_shop]=@id_shop;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void UpdateMapping3()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var u = new Update<Author>(true);
            u.Where.Equal("id").IsNULL("is_activated");

            var result = u.GetSql();
            var sql = "UPDATE [tab_authors] SET [created_at]=@created_at, [firstname]=@firstname, [lastname]=@lastname WHERE [id]=@id AND [is_activated] IS NULL;";

            Assert.Equal(sql, result);
        }
    }
}