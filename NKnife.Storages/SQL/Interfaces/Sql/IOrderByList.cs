using System.Collections.Generic;

namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IOrderByList
	{

		IFormatter Parameters { get; }

		IEnumerable<IOrderBy> Expressions { get; }

		string GetSql(string tableAlias = "");

		int Count { get; }

		void Clear();

		string TableAlias { get; set; }

		IOrderByList SetTableAlias(string tableAlias = "");

		IOrderByList Append(IOrderBy expression);

		IOrderByList Raw(string rawSql);

		IOrderByList Ascending(params string[] columns);

		IOrderByList Descending(params string[] columns);

	}

}