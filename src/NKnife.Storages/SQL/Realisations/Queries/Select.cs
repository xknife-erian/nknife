using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Enums;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Queries;
using NKnife.Storages.SQL.Interfaces.Sql;
using NKnife.Storages.SQL.Realisations.Sql;
using NKnife.Storages.Util;

namespace NKnife.Storages.SQL.Realisations.Queries
{
    public class Select<T> : IStatementSelect
    {
        public Select(string tableAlias = "") : this(SuperSql.DefaultFormatter, tableAlias)
        {
        }

        public Select(IFormatter parameters, string tableAlias = "")
        {
            Query = SqlQuery.Select;
            Formatter = parameters;
            TableAlias = tableAlias;
            Columns = new ColumnsListAggregation(Formatter);
            Join = new JoinList(Formatter);
            Where = new WhereList(Formatter);
            OrderBy = new OrderByList(Formatter);
            GroupBy = new GroupByList(Formatter, Columns);
        }

        public IFormatter Formatter { get; set; }

        public SqlQuery Query { get; }

        public string TableAlias { get; set; }

        public IColumnsListAggregation Columns { get; set; }

        public IJoinList Join { get; set; }

        public IWhereList Where { get; set; }

        public IGroupByList GroupBy { get; set; }

        public IOrderByList OrderBy { get; set; }

        public string GetSql(bool endOfStatement = true)
        {
            var table = Reflection.GetTableName<T>();

            var result = TemplateLibrary.Select;
            result.Append(SnippetLibrary.Table(table, TableAlias));
            result.Append(SnippetLibrary.Columns(Columns.GetSql(TableAlias)));

            if (Join.Count > 0)
            {
                var joinTable = string.IsNullOrEmpty(TableAlias) ? table : TableAlias;
                result.Append(SnippetLibrary.Join(Join.GetSql(joinTable)));
            }

            if (Where.Count > 0)
                result.Append(SnippetLibrary.Where(Where.GetSql(TableAlias)));
            if (GroupBy.Count > 0)
                result.Append(SnippetLibrary.GroupBy(GroupBy.GetSql(TableAlias)));
            if (OrderBy.Count > 0)
                result.Append(SnippetLibrary.OrderBy(OrderBy.GetSql(TableAlias)));

            return result.GetSql(endOfStatement);
        }

        public override string ToString()
        {
            return GetSql();
        }

        public static Select<T> SelectAll(params string[] columns)
        {
            var result = new Select<T>();
            result.Columns.Append(columns);
            return result;
        }

        public static Select<T> SelectWherePK(params string[] columns)
        {
            return SelectWherePK(SuperSql.DefaultFormatter, columns);
        }

        public static Select<T> SelectWherePK(IFormatter parameters, params string[] columns)
        {
            var pk = Reflection.GetPrimaryKey<T>();
            var result = new Select<T>(parameters);
            result.Columns.Append(columns);
            result.Where.Equal(pk);
            return result;
        }
    }
}