using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class Value : IValue
	{

		public string Expression { get; set; }

		public Value(string expression)
		{
			this.Expression = expression;
		}

	}

}