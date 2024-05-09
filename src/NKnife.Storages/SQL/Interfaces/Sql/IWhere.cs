namespace NKnife.Storages.SQL.Interfaces.Sql
{
	public interface IWhere
	{

		Enums.WhereLogic Logic { get; }

		Enums.WhereType Type { get; }

		Enums.Parenthesis Parenthesis { get; }

		string Prefix { get; set; }

		string Postfix { get; set; }

		string TableAlias { get; set; }

		bool IsColumn { get; set; }

		bool IsRaw { get; set; }

		string Column { get; set; }

		string Value { get; set; }

	}

}