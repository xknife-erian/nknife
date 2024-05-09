using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class Set : ISet
	{

		public string Name { get; set; }

		public string Value { get; set; }

		public Set(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

	}

}