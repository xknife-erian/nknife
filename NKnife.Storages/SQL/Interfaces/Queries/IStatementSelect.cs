using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Interfaces.Queries
{

	public interface IStatementSelect : IStatement, IStatementTableAlias
	{

		IColumnsListAggregation Columns { get; set; }

		IJoinList Join { get; set; }

		IWhereList Where { get; set; }

		IGroupByList GroupBy { get; set; }

		IOrderByList OrderBy { get; set; }

	}

}