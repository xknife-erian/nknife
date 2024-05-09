using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class GroupBy : IGroupBy
	{

		public string Column { get; set; }

		public string TableAlias { get; set; }

		public bool IsRaw { get; set; }

		public GroupBy(string column, string tableAlias = "")
		{
			this.Column = column;
			this.TableAlias = tableAlias;
		}

	}

}