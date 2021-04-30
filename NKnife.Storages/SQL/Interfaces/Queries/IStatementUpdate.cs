using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Interfaces.Queries
{

	public interface IStatementUpdate : IStatement, IStatementTableAlias
	{

		ISetList Sets { get; set; }

		IWhereList Where { get; set; }

	}

}