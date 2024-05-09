using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.Queries
{
    public class Query
    {
        [Fact]
        public void QueryDelete()
        {
            var q1 = Query<Author>.CreateDelete();
            q1.Where.Equal("id");

            var q2 = new Query<Author>().Delete();
            q2.Where.Equal("id");

            var q3 = new Delete<Author>();
            q3.Where.Equal("id");

            Assert.Equal(q3.GetSql(), q2.GetSql());
            Assert.Equal(q3.GetSql(), q1.GetSql());
        }

        [Fact]
        public void QueryInsert()
        {
            var q1 = Query<Author>.CreateInsert();
            q1.AppendParameters("q1", "q2", "q3");

            var q2 = new Query<Author>().Insert();
            q2.AppendParameters("q1", "q2", "q3");

            var q3 = new Insert<Author>();
            q3.AppendParameters("q1", "q2", "q3");

            Assert.Equal(q3.GetSql(), q2.GetSql());
            Assert.Equal(q3.GetSql(), q1.GetSql());
        }

        [Fact]
        public void QuerySelect()
        {
            var q1 = Query<Author>.CreateSelect();
            q1.Where.Equal("id");

            var q2 = new Query<Author>().Select();
            q2.Where.Equal("id");

            var q3 = new Select<Author>();
            q3.Where.Equal("id");

            Assert.Equal(q3.GetSql(), q2.GetSql());
            Assert.Equal(q3.GetSql(), q1.GetSql());
        }

        [Fact]
        public void QueryUpdate()
        {
            SuperSql.DefaultFormatter = FormatterLibrary.MsSql;

            var q1 = Query<Author>.CreateUpdate();
            q1.Sets.Append("q1", "q2", "q3");

            var q2 = new Query<Author>().Update();
            q2.Sets.Append("q1", "q2", "q3");

            var q3 = new Update<Author>();
            q3.Sets.Append("q1", "q2", "q3");

            Assert.Equal(q3.GetSql(), q2.GetSql());
            Assert.Equal(q3.GetSql(), q1.GetSql());
        }
    }
}