using System.Collections.Generic;

namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IGroupByList : IAggregateFunctions<IGroupByList>
	{

		IFormatter Parameters { get; }

		IColumnsListAggregation Columns { get; }

		IEnumerable<IGroupBy> Expressions { get; }

		string GetSql(string tableAlias = "");

		int Count { get; }

		void Clear();

		string TableAlias { get; set; }

		IGroupByList SetTableAlias(string tableAlias = "");

		IGroupByList Append(IGroupBy expression, bool copyToColumns = false);

		IGroupByList Append(bool copyToColumns = false, params string[] columns);

		IGroupByList AppendWithColumn(IGroupBy expression, string column, string columnAlias, string prefix = "", string postfix = "");

		IGroupByList Raw(params string[] rawSql);

	}

}