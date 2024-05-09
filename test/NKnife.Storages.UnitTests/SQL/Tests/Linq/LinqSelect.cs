using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Linq;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Linq
{
    public class LinqSelect
    {
        [Fact]
        public void LinqSelectColumns()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Select<Author>();
            q1.Columns(x => x.Append("a", "b", "c"));
            var result = q1.GetSql();
            var sql = "SELECT [a], [b], [c] FROM [tab_authors];";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqSelectComplexQuery()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Select<Author>();
            q1.Columns(x => x.Append("a", "b", "c").AppendAlias("d", "ddd").FuncMax("price")).Where(w => w.Equal("a", "b", "c").IsNULL("active")).GroupBy(x => x.FuncCount("cnt", "ccc"))
                .OrderBy(x => x.Ascending("created_at"));
            var result = q1.GetSql();
            var sql =
                "SELECT [a], [b], [c], [d] as 'ddd', MAX([price]), COUNT([cnt]) as 'ccc' FROM [tab_authors] WHERE [a]=@a AND [b]=@b AND [c]=@c AND [active] IS NULL GROUP BY [cnt] ORDER BY [created_at] ASC;";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqSelectGroupBy()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Select<Author>();
            q1.GroupBy(x => x.FuncMax("max", "mx").FuncMin("min", "mn").FuncCount("all"));
            var result = q1.GetSql();
            var sql = "SELECT MAX([max]) as 'mx', MIN([min]) as 'mn', COUNT([all]) FROM [tab_authors] GROUP BY [max], [min], [all];";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqSelectJoin()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Select<Author>();
            q1.Join(j => j.LeftJoin("users", "id_user", "id", "u").Append("id_status", "ids")).Join(j => j.FullJoin("cities", "id_city", "idc", "c"));
            var result = q1.GetSql();
            var sql =
                "SELECT * FROM [tab_authors] LEFT JOIN [users] as [u] ON [tab_authors].[id_user]=[u].[id] AND [tab_authors].[id_status]=[u].[ids] FULL JOIN [cities] as [c] ON [tab_authors].[id_city]=[c].[idc];";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqSelectOrderBy()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Select<Author>();
            q1.OrderBy(x => x.Ascending("id").Descending("date"));
            var result = q1.GetSql();
            var sql = "SELECT * FROM [tab_authors] ORDER BY [id] ASC, [date] DESC;";
            Assert.Equal(result, sql);
        }

        [Fact]
        public void LinqSelectWhere()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = new Select<Author>();
            q1.Where(x => x.Equal("s1").IsNULL("s2"));
            var result = q1.GetSql();
            var sql = "SELECT * FROM [tab_authors] WHERE [s1]=@s1 AND [s2] IS NULL;";
            Assert.Equal(result, sql);
        }
    }
}