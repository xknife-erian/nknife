using System;
using NKnife.Storages.SQL.Interfaces.Queries;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Linq
{

	public static partial class Linq
	{

		public static IStatementInsert Columns(this IStatementInsert q, Func<IColumnsListSimple, IColumnsListSimple> f)
		{
			f.Invoke(q.Columns);
			return q;
		}

		public static IStatementInsert Values(this IStatementInsert q, Func<IValueList, IValueList> f)
		{
			f.Invoke(q.Values);
			return q;
		}

	}

}