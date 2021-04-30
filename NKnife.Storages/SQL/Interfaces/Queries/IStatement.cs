namespace NKnife.Storages.SQL.Interfaces.Queries
{

	public interface IStatement
	{

		IFormatter Formatter { get; set; }

		Enums.SqlQuery Query { get; }

		string GetSql(bool EndOfStatement = true);

	}

	public interface IStatementTableAlias
	{

		string TableAlias { get; set; }

	}

}