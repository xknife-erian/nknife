using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Interfaces.Queries
{

	public interface IStatementInsert : IStatement, IStatementTableAlias
	{

		IColumnsListSimple Columns { get; set; }

		IValueList Values { get; set; }

		IStatementInsert AppendParameters(params string[] parameters);

	}

}