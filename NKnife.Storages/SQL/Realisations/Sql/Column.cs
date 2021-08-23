using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class Column : IColumn
	{

		public string Value { get; set; }

		public string Alias { get; set; }

		public string Prefix { get; set; }

		public string Postfix { get; set; }

		public bool IsRaw { get; set; }

		public bool IsAggregation { get; set; }

		public string TableAlias { get; set; }

	}

}