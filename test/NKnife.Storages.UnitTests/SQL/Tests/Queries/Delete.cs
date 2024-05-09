using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Queries
{
    public class Delete
    {
        [Fact]
        public void QueryDeleteAliasWhere()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var d = new Delete<Author>("td");
            d.Where.Equal("p1").Less("p2").IsNULL("p3");

            var result = d.GetSql();
            var sql = "DELETE FROM [tab_authors] as [td] WHERE [td].[p1]=@p1 AND [td].[p2]<@p2 AND [td].[p3] IS NULL;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryDeleteSimpleEmpty()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var d = new Delete<Author>();

            var result = d.GetSql();
            var sql = "DELETE FROM [tab_authors];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QueryDeleteSimpleWhere()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var d = new Delete<Author>();
            d.Where.Equal("p1").Less("p2").IsNULL("p3");

            var result = d.GetSql();
            var sql = "DELETE FROM [tab_authors] WHERE [p1]=@p1 AND [p2]<@p2 AND [p3] IS NULL;";
            Assert.Equal(sql, result);
        }
    }
}