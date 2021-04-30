using System.Collections.Generic;
using NKnife.Storages.SQL.Interfaces.Queries;

namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IColumnsList
	{

		IFormatter Parameters { get; }

		IEnumerable<IColumn> Expressions { get; }

		string GetSql(string tableAlias = "");

		int Count { get; }

		void Clear();

		string TableAlias { get; set; }

	}

	public interface IColumnsList<out T> where T: IColumnsList
	{

		T Append(IColumn expression);

		T Append(params string[] names);

		T AppendAlias(string name, string alias, string prefix = "", string postfix = "", bool isAggregation = false);

		T RawValue(string rawSql, string alias = "");

		T Raw(params string[] rawSql);

		T SubQuery(IStatementSelect select, string alias = "");

		T SetTableAlias(string tableAlias = "");

	}

	public interface IColumnsListSimple : IColumnsList, IColumnsList<IColumnsListSimple>
	{
	}

	public interface IColumnsListAggregation : IColumnsList, IColumnsList<IColumnsListAggregation>, IAggregateFunctions<IColumnsListAggregation>
	{
	}

}