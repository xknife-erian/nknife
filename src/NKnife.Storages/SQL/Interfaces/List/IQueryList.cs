using System.Collections.Generic;
using NKnife.Storages.SQL.Interfaces.Queries;

namespace NKnife.Storages.SQL.Interfaces.List
{

	public interface IQueryList
	{

		IEnumerable<IStatement> Queries { get; }

		int Count { get; }

		void Clear();

		IQueryList Append(params IStatement[] queries);

		string GetSql();

	}

}