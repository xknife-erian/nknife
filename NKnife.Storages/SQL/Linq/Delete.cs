using System;
using NKnife.Storages.SQL.Interfaces.Queries;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Linq
{

	public static partial class Linq
	{

		public static IStatementDelete Where(this IStatementDelete q, Func<IWhereList, IWhereList> f)
		{
			f.Invoke(q.Where);
			return q;
		}

	}

}