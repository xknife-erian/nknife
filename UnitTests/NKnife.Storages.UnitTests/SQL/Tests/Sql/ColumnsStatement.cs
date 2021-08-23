using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Sql;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Sql
{
    public class ColumnsStatement
    {
        [Fact]
        public void ColumnsAggregations1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListAggregation(SuperSql.DefaultFormatter);
            c.FuncCount("all");
            c.FuncMax("cnt", "max_cnt");
            var result = c.GetSql();
            var sql = "COUNT([all]), MAX([cnt]) as 'max_cnt'";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsListSimpleRaw()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.Append("a", "b", "c");
            c.AppendAlias("last_name", "l");
            c.RawValue("(SELECT NOW())");
            c.RawValue("(SELECT 'abc')", "lll");
            c.Append("d");
            var result = c.GetSql("tbl");
            var sql = "[tbl].[a], [tbl].[b], [tbl].[c], [tbl].[last_name] as 'l', (SELECT NOW()), (SELECT 'abc') as 'lll', [tbl].[d]";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsListTableAlias1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.Append("a");
            c.SetTableAlias("t1");
            c.Append("b");
            c.SetTableAlias("t2");
            c.Append("c");
            c.SetTableAlias();
            c.RawValue("(SELECT NOW())");
            c.Append("d");
            var result = c.GetSql("t");
            var sql = "[t].[a], [t1].[b], [t2].[c], (SELECT NOW()), [t].[d]";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsListTableAlias2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.Append("a");
            c.SetTableAlias("t1");
            c.Append("b");
            c.SetTableAlias("t2");
            c.Append("c");
            c.SetTableAlias();
            c.RawValue("(SELECT NOW())");
            c.Append("d");
            var result = c.GetSql();
            var sql = "[a], [t1].[b], [t2].[c], (SELECT NOW()), [d]";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsSimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.Append("a", "b", "c");
            var result = c.GetSql();
            var sql = "[a], [b], [c]";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsSimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.Append("column");
            var result = c.GetSql();
            var sql = "[column]";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsSimple3()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.Append("column1").Append("column2");
            var result = c.GetSql();
            var sql = "[column1], [column2]";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsSimpleAlias1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.AppendAlias("last_name", "l");
            c.AppendAlias("first_name", "f");
            var result = c.GetSql();
            var sql = "[last_name] as 'l', [first_name] as 'f'";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsSimpleAlias2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.AppendAlias("last_name", "l");
            c.AppendAlias("first_name", "f");
            var result = c.GetSql("tbl");
            var sql = "[tbl].[last_name] as 'l', [tbl].[first_name] as 'f'";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ColumnsSimpleEmpty()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var c = new ColumnsListSimple(SuperSql.DefaultFormatter);
            c.Append();
            var result = c.GetSql();
            var sql = "*";
            Assert.Equal(sql, result);
        }
    }
}