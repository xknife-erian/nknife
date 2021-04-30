namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IJoinItem
	{

		bool IsRaw { get; }

		string Column { get; set; }

		string Value { get; set; }

	}

}