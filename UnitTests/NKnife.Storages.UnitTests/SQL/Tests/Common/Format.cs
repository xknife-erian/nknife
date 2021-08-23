using NKnife.Storages.SQL.Common;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Common
{
    public class Format
    {
        [Fact]
        public void FormatAliasColumn()
        {
            var alias = "text for alias";

            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;
            var result1 = SuperSql.FormatColumnAlias(alias);
            Assert.Equal('\'' + alias + '\'', result1);

            // SuperSql.DefaultFormatter = FormatterLibrary.MySql;
            // var result2 = SuperSql.FormatColumnAlias(alias);
            // Assert.Equal('\"' + alias + '\"', result2);
        }

        [Fact]
        public void FormatAliasTable()
        {
            var alias = "tbl";

            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;
            var result1 = SuperSql.FormatTableAlias(alias);
            Assert.Equal('[' + alias + ']', result1);

            // SuperSql.DefaultFormatter = FormatterLibrary.MySql;
            // var result2 = SuperSql.FormatTableAlias(alias);
            // Assert.Equal('`' + alias + '`', result2);
        }

        [Fact]
        public void FormatColumns()
        {
            var column = "date";
            
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;
            var result1 = SuperSql.FormatColumn(column);
            Assert.Equal('[' + column + ']', result1);
            
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;
            var result4 = SuperSql.FormatColumn(column, "t");
            Assert.Equal("[t].[" + column + ']', result4);

            // SuperSql.DefaultFormatter = FormatterLibrary.MySql;
            // var result2 = SuperSql.FormatColumn(column);
            // Assert.Equal('`' + column + '`', result2);
            //
            // SuperSql.DefaultFormatter.EscapeEnabled = false;
            // var result3 = SuperSql.FormatColumn(column);
            // Assert.Equal(column, result3);

        }

        [Fact]
        public void FormatParameter()
        {
            var name = "login";
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result1 = SuperSql.FormatParameter(name);
            Assert.Equal('@' + name, result1);

            // SuperSql.DefaultFormatter = FormatterLibrary.MySql;
            // var result2 = SuperSql.FormatParameter(name);
            // Assert.Equal('?' + name, result2);
        }

        [Fact]
        public void FormatTable()
        {
            var table = "tab_users";
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var result1 = SuperSql.FormatTable(table);
            Assert.Equal('[' + table + ']', result1);

            // SuperSql.DefaultFormatter = FormatterLibrary.MySql;
            // var result2 = SuperSql.FormatTable(table);
            // Assert.Equal('`' + table + '`', result2);
            //
            // SuperSql.DefaultFormatter.EscapeEnabled = false;
            // var result3 = SuperSql.FormatTable(table);
            // Assert.Equal(table, result3);
        }
    }
}