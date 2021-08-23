using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Interfaces.Queries
{

	public interface IStatementDelete : IStatement, IStatementTableAlias
	{

		IWhereList Where { get; set; }

	}

}