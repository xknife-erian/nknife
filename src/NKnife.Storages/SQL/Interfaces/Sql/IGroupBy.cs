namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IGroupBy
	{

		string Column { get; set; }

		string TableAlias { get; set; }

		bool IsRaw { get; set; }

	}

}