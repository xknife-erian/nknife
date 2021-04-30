namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IOrderBy
	{

		Enums.OrderDirection Direction { get; set; }

		string Column { get; }

		string TableAlias { get; set; }

		bool IsRaw { get; set; }

		string GetDirection();

	}

}