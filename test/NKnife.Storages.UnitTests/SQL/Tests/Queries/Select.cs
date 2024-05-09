using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using NKnife.Storages.Util;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Queries
{
    public class Select
    {
        [Fact]
        public void QuerySelectAggregation1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>("a");
            s.Columns.FuncCount("cnt");

            var result = s.GetSql();
            var sql = "SELECT COUNT([cnt]) FROM [tab_authors] as [a];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectAggregation2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>("a");
            s.Columns.FuncMax("m1").FuncMin("m2");

            var result = s.GetSql();
            var sql = "SELECT MAX([m1]), MIN([m2]) FROM [tab_authors] as [a];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectGroupBySimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.GroupBy.Append(false, "country", "city");
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] GROUP BY [country], [city];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectGroupBySimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.GroupBy.Append(true, "country", "city");
            var result = s.GetSql();
            var sql = "SELECT [country], [city] FROM [tab_authors] GROUP BY [country], [city];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectGroupBySimple3()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.GroupBy.FuncMax("sm", "asm");
            var result = s.GetSql();
            var sql = "SELECT MAX([sm]) as 'asm' FROM [tab_authors] GROUP BY [sm];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectHard1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Columns.Append("s1", "s2", "s3").FuncMin("date");
            s.Where.Equal("s1", "s2").IsNotNULL("created_at").IsNULL("activated");
            s.GroupBy.Append(false, "country", "city").FuncCount("lll", "all");
            s.OrderBy.Ascending("age");
            var result = s.GetSql();
            var sql =
                "SELECT [s1], [s2], [s3], MIN([date]), COUNT([lll]) as 'all' FROM [tab_authors] WHERE [s1]=@s1 AND [s2]=@s2 AND [created_at] IS NOT NULL AND [activated] IS NULL GROUP BY [country], [city], [lll] ORDER BY [age] ASC;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectJoinSimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Columns.Append("a1", "a2", "a3");
            s.Join.InnerJoin("users", "u").Append("id_user", "id");
            var result = s.GetSql();
            var sql = "SELECT [a1], [a2], [a3] FROM [tab_authors] INNER JOIN [users] as [u] ON [tab_authors].[id_user]=[u].[id];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectJoinSimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>("t");
            s.Columns.Append("a1");
            s.Join.InnerJoin("users", "u").Append("id_user", "id").Append("id_status", "id2");
            var result = s.GetSql();
            var sql = "SELECT [t].[a1] FROM [tab_authors] as [t] INNER JOIN [users] as [u] ON [t].[id_user]=[u].[id] AND [t].[id_status]=[u].[id2];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectJoinSimple3()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>("t");
            s.Columns.Append("a1");
            s.Join.InnerJoin("users", "u").Append("id_user", "id");
            s.Join.LeftJoin("statuses").Append("id_status", "id2");
            s.Join.RightJoin("profiles", "p").Append("id_profile", "id3");
            var result = s.GetSql();
            var sql =
                "SELECT [t].[a1] FROM [tab_authors] as [t] INNER JOIN [users] as [u] ON [t].[id_user]=[u].[id] LEFT JOIN [statuses] ON [t].[id_status]=[statuses].[id2] RIGHT JOIN [profiles] as [p] ON [t].[id_profile]=[p].[id3];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectJoinSimple4()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Columns.Raw("[tab_authors].*", "[u].*", "[s].*");
            s.Join.LeftJoin("users", "id_user", "id", "u");
            s.Join.LeftJoin("statuses", "id_status", "id", "s");
            var result = s.GetSql();
            var sql =
                "SELECT [tab_authors].*, [u].*, [s].* FROM [tab_authors] LEFT JOIN [users] as [u] ON [tab_authors].[id_user]=[u].[id] LEFT JOIN [statuses] as [s] ON [tab_authors].[id_status]=[s].[id];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleAllColumns()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result = new Select<Author>().GetSql();
            var sql = "SELECT * FROM [tab_authors];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleAllColumnsAlias()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result = new Select<Author>("t").GetSql();
            var sql = "SELECT [t].* FROM [tab_authors] as [t];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleColumnsList()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Columns.Append("a", "b", "c");
            var result = s.GetSql();
            var sql = "SELECT [a], [b], [c] FROM [tab_authors];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleColumnsListAlias()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Columns.Append("a", "b", "c");
            s.Columns.AppendAlias("d", "D");
            var result = s.GetSql();
            var sql = "SELECT [a], [b], [c], [d] as 'D' FROM [tab_authors];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleColumnsListStatic()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = Select<Author>.SelectAll("a", "b", "c");
            var result = s.GetSql();
            var sql = "SELECT [a], [b], [c] FROM [tab_authors];";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleOrderByAsc()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.OrderBy.Ascending("age");
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] ORDER BY [age] ASC;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleOrderByAscDesc()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.OrderBy.Ascending("age").Descending("amount");
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] ORDER BY [age] ASC, [amount] DESC;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleOrderByDesc()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.OrderBy.Descending("age");
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] ORDER BY [age] DESC;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleWhereAnd()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Where.Equal("first_name", "last_name");
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] WHERE [first_name]=@first_name AND [last_name]=@last_name;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleWhereOr()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Where.Equal("position").Or().EqualGreater("age");
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] WHERE [position]=@position OR [age]>=@age;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleWherePK()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>();
            s.Where.Equal(Reflection.GetPrimaryKey<Author>());
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] WHERE [ID]=@ID;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleWherePKAlias()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = new Select<Author>("t");
            s.Where.Equal(Reflection.GetPrimaryKey<Author>());
            var result = s.GetSql();
            var sql = "SELECT [t].* FROM [tab_authors] as [t] WHERE [t].[ID]=@ID;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSimpleWherePKStatic()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var s = Select<Author>.SelectWherePK();
            var result = s.GetSql();
            var sql = "SELECT * FROM [tab_authors] WHERE [ID]=@ID;";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void QuerySelectSubQuery1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var sub = new Select<Book>("b");
            sub.Columns.FuncCount("id");
            sub.Where.EqualValue("id_author", "[a].[id]");

            var query = new Select<Author>("a");
            query.Columns.Raw("[a].*");
            query.Columns.SubQuery(sub, "cnt");
            var result = query.GetSql();
            var sql = "SELECT [a].*, (SELECT COUNT([id]) FROM [tab_books] as [b] WHERE [b].[id_author]=[a].[id]) as 'cnt' FROM [tab_authors] as [a];";
            Assert.Equal(sql, result);
        }
    }
}