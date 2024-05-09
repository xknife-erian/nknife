using System;
using NKnife.Storages.SQL.Realisations.Queries;
using NKnife.Storages.UnitTests.SQL.DataBase;
using Xunit;

namespace NKnife.Storages.UnitTests.SQL.Tests.List
{
    public class QueryList
    {
        [Fact]
        public void QueryListSimple()
        {
            var l = new Storages.SQL.Realisations.List.QueryList();
            var a1 = Query<Author>.CreateSelect();
            var a2 = Query<Author>.CreateInsert();
            var a3 = Query<Author>.CreateUpdate();
            var a4 = Query<Author>.CreateDelete();
            l.Append(a1, a2, a3, a4);

            var sql = string.Format("{1}{0}{2}{0}{3}{0}{4}", Environment.NewLine, a1.GetSql(), a2.GetSql(), a3.GetSql(), a4.GetSql());

            Assert.Equal(4, l.Count);
            Assert.Equal(sql, l.GetSql());
        }
    }
}