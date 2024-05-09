using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Queries
{
    public class Insert
    {
        [Fact]
        public void QueryInsertSimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var a = new Insert<Author>(true);

            var result = a.GetSql();
            var sql = "INSERT INTO [tab_authors]([firstname], [lastname]) VALUES(@firstname, @lastname);";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryInsertSimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var a = new Insert<Author>(true);
            a.Columns.Append("created_at", "updated_at");
            a.Values.Append("NOW()", "'2020-01-01 23:45:22'");

            var result = a.GetSql();
            var sql = "INSERT INTO [tab_authors]([firstname], [lastname], [created_at], [updated_at]) VALUES(@firstname, @lastname, NOW(), '2020-01-01 23:45:22');";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryInsertSimple3()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var a = new Insert<Author>(true);
            a.Columns.Append("p1", "p2", "p3");
            a.Values.AppendParameters("p1", "p2", "p3");

            var result = a.GetSql();
            var sql = "INSERT INTO [tab_authors]([firstname], [lastname], [p1], [p2], [p3]) VALUES(@firstname, @lastname, @p1, @p2, @p3);";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryInsertSimple4()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var a = new Insert<Author>(true);
            a.AppendParameters("p1", "p2", "p3");

            var result = a.GetSql();
            var sql = "INSERT INTO [tab_authors]([firstname], [lastname], [p1], [p2], [p3]) VALUES(@firstname, @lastname, @p1, @p2, @p3);";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryInsertSimple5()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var a = new Insert<Author>();
            a.AppendParameters("p1", "p2", "p3");

            var result = a.GetSql();
            var sql = "INSERT INTO [tab_authors]([p1], [p2], [p3]) VALUES(@p1, @p2, @p3);";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryInsertStatic1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result = Insert<Author>.Mapping("p1", "p2", "p3").GetSql();
            var sql = "INSERT INTO [tab_authors]([firstname], [lastname], [p1], [p2], [p3]) VALUES(@firstname, @lastname, @p1, @p2, @p3);";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryInsertStatic2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result = Insert<Author>.WithoutMapping("p1", "p2", "p3").GetSql();
            var sql = "INSERT INTO [tab_authors]([p1], [p2], [p3]) VALUES(@p1, @p2, @p3);";
            Assert.Equal(sql, result);
        }
    }
}