using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Linq;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Linq
{
    public class LinqDelete
    {
        [Fact]
        public void LinqDeleteSimple()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Delete<Author>();
            var result = q1.GetSql();
            var sql = "DELETE FROM [tab_authors];";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqDeleteSimpleWhere()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Delete<Author>();
            q1.Where(x => x.Equal("a").IsNULL("b"));
            var result = q1.GetSql();
            var sql = "DELETE FROM [tab_authors] WHERE [a]=@a AND [b] IS NULL;";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqDeleteTableAlias()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result = Query<Author>.CreateDelete("t").Where(x => x.Equal("a")).GetSql();
            var sql = "DELETE FROM [tab_authors] as [t] WHERE [t].[a]=@a;";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqQueryDeleteSimpleWhere()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result = Query<Author>.CreateDelete().Where(x => x.Equal("a")).GetSql();
            var sql = "DELETE FROM [tab_authors] WHERE [a]=@a;";
            Assert.Equal(result, sql);
        }
    }
}