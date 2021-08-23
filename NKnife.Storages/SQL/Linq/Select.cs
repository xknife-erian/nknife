using System;
using NKnife.Storages.SQL.Interfaces.Queries;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Linq
{

	public static partial class Linq
	{

		public static IStatementSelect Columns(this IStatementSelect q, Func<IColumnsListAggregation, IColumnsListAggregation> f)
		{
			f.Invoke(q.Columns);
			return q;
		}

		public static IStatementSelect Where(this IStatementSelect q, Func<IWhereList, IWhereList> f)
		{
			f.Invoke(q.Where);
			return q;
		}

		public static IStatementSelect OrderBy(this IStatementSelect q, Func<IOrderByList, IOrderByList> f)
		{
			f.Invoke(q.OrderBy);
			return q;
		}

		public static IStatementSelect GroupBy(this IStatementSelect q, Func<IGroupByList, IGroupByList> f)
		{
			f.Invoke(q.GroupBy);
			return q;
		}

		public static IStatementSelect Join(this IStatementSelect q, Func<IJoinList, IJoin> f)
		{
			f.Invoke(q.Join);
			return q;
		}

	}

}