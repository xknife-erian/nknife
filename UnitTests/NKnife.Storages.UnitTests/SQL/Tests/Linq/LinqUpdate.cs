using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Linq;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Linq
{
    public class LinqUpdate
    {
        [Fact]
        public void LinqQueryUpdateSimpleWhere()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result = Query<Author>.CreateUpdate().Sets(x => x.AppendValue("count", "123")).Where(x => x.Equal("a")).GetSql();
            var sql = "UPDATE [tab_authors] SET [count]=123 WHERE [a]=@a;";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqUpdateSimpleWhere()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Update<Author>();
            q1.Sets(x => x.AppendValue("name", "value")).Where(x => x.Equal("a").IsNULL("b"));
            var result = q1.GetSql();
            var sql = "UPDATE [tab_authors] SET [name]=value WHERE [a]=@a AND [b] IS NULL;";
            Assert.Equal(result, sql);
        }
    }
}