using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Sql;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Sql
{
    public class WhereStatement
    {
        [Fact]
        public void AndEqualGreater()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.EqualGreater("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a]>=@a AND [b]>=@b AND [c]>=@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndEqualGreaterParam()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.EqualGreaterValue("a", "1");
            w.EqualGreaterValue("b", "2");
            w.EqualGreaterValue("c", "3");
            var result = w.GetSql();
            var sql = "[a]>=1 AND [b]>=2 AND [c]>=3";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndEqualLess()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.EqualLess("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a]<=@a AND [b]<=@b AND [c]<=@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndEqualLessParam()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.EqualLessValue("a", "1");
            w.EqualLessValue("b", "2");
            w.EqualLessValue("c", "3");
            var result = w.GetSql();
            var sql = "[a]<=1 AND [b]<=2 AND [c]<=3";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndEqualParamValue()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.EqualValue("a", "1");
            w.EqualValue("b", "2");
            w.EqualValue("c", "3");
            var result = w.GetSql();
            var sql = "[a]=1 AND [b]=2 AND [c]=3";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndEqualSimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Equal("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a]=@a AND [b]=@b AND [c]=@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndEqualSimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Equal("a", "b", "c");
            var result = w.GetSql("t");
            var sql = "[t].[a]=@a AND [t].[b]=@b AND [t].[c]=@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndGreater()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Greater("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a]>@a AND [b]>@b AND [c]>@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndGreaterParam()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.GreaterValue("a", "1");
            w.GreaterValue("b", "2");
            w.GreaterValue("c", "3");
            var result = w.GetSql();
            var sql = "[a]>1 AND [b]>2 AND [c]>3";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndLess()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Less("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a]<@a AND [b]<@b AND [c]<@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndLessParam()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.LessValue("a", "1");
            w.LessValue("b", "2");
            w.LessValue("c", "3");
            var result = w.GetSql();
            var sql = "[a]<1 AND [b]<2 AND [c]<3";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndNotEqualParamValue()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.NotEqualValue("a", "1");
            w.NotEqualValue("b", "2");
            w.NotEqualValue("c", "3");
            var result = w.GetSql();
            var sql = "[a]!=1 AND [b]!=2 AND [c]!=3";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void AndNotEqualSimple()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.NotEqual("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a]!=@a AND [b]!=@b AND [c]!=@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ComboAndEqualAndNotEqual()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.EqualValue("a", "1");
            w.NotEqualValue("b", "2");
            var result = w.GetSql();
            var sql = "[a]=1 AND [b]!=2";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void ComboAndGreaterAndLess()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Greater("a");
            w.Less("b");
            var result = w.GetSql();
            var sql = "[a]>@a AND [b]<@b";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void InSimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Equal("a");
            w.In("b", "1", "2", "3");
            w.Equal("c");
            var result = w.GetSql();
            var sql = "[a]=@a AND [b] IN (1, 2, 3) AND [c]=@c";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void InSimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.In("id_user", "SELECT id FROM tab_users");
            var result = w.GetSql();
            var sql = "[id_user] IN (SELECT id FROM tab_users)";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void IsNotNullSimple()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.IsNotNULL("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a] IS NOT NULL AND [b] IS NOT NULL AND [c] IS NOT NULL";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void IsNullSimple()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.IsNULL("a", "b", "c");
            var result = w.GetSql();
            var sql = "[a] IS NULL AND [b] IS NULL AND [c] IS NULL";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void RawSimple1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Raw("[a] IS NULL AND [b]=2 AND [c] NOT LIKE '%text%'");
            var result = w.GetSql();
            var sql = "[a] IS NULL AND [b]=2 AND [c] NOT LIKE '%text%'";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void RawSimple2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Raw("[a] NOT LIKE '%text%'");
            w.Equal("id");
            var result = w.GetSql();
            var sql = "[a] NOT LIKE '%text%' AND [id]=@id";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void WhereParenthesis1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.OpenParenthesis();
            w.Or();
            w.IsNULL("a", "b", "c");
            w.CloseParenthesis();
            var result = w.GetSql();
            var sql = "([a] IS NULL OR [b] IS NULL OR [c] IS NULL)";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void WhereParenthesis2()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Equal("id");
            w.OpenParenthesis(2);
            w.IsNULL("a", "b", "c");
            w.CloseParenthesis();
            w.Or();
            w.OpenParenthesis();
            w.And();
            w.IsNULL("d", "e", "f");
            w.CloseParenthesis(2);
            var result = w.GetSql();
            var sql = "[id]=@id AND (([a] IS NULL AND [b] IS NULL AND [c] IS NULL) OR ([d] IS NULL AND [e] IS NULL AND [f] IS NULL))";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void WhereParenthesis3()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.OpenParenthesis(3);
            w.Or();
            w.IsNULL("a", "b");
            w.CloseParenthesis();
            w.And();
            w.OpenParenthesis();
            w.Or();
            w.IsNULL("c", "d");
            w.CloseParenthesis(2);
            w.And();
            w.Less("ls");
            w.CloseParenthesis();
            w.Greater("gr");
            var result = w.GetSql();
            var sql = "((([a] IS NULL OR [b] IS NULL) AND ([c] IS NULL OR [d] IS NULL)) AND [ls]<@ls) AND [gr]>@gr";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void WhereParenthesis4()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.RawParenthesis("[a] IS NULL OR [b] IS NULL");
            var result = w.GetSql();
            var sql = "([a] IS NULL OR [b] IS NULL)";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void WhereParenthesis5()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.RawParenthesis("[a] IS NULL OR [b] IS NULL");
            w.RawParenthesis("[c] IS NULL OR [d] IS NULL");
            var result = w.GetSql();
            var sql = "([a] IS NULL OR [b] IS NULL) AND ([c] IS NULL OR [d] IS NULL)";
            Assert.Equal(sql, result);
        }

        [Fact]
        public void WhereTableAlias1()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var w = new WhereList(SuperSql.DefaultFormatter);
            w.Raw("[a] IS NULL");
            w.SetTableAlias("ttt");
            w.Equal("id");
            w.SetTableAlias("ddd");
            w.Equal("age");
            w.SetTableAlias();
            w.Equal("old_value");
            var result = w.GetSql("old");
            var sql = "[a] IS NULL AND [ttt].[id]=@id AND [ddd].[age]=@age AND [old].[old_value]=@old_value";
            Assert.Equal(sql, result);
        }
    }
}