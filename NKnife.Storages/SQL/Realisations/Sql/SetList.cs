using System.Collections.Generic;
using System.Text;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class SetList : ISetList
	{

		private readonly List<ISet> _expressions;

		public IFormatter Parameters { get; set; }

		public int Count
		{
			get
			{
				return this._expressions.Count;
			}
		}

		public SetList(IFormatter parameters)
		{
			this._expressions = new List<ISet>();
			this.Parameters = parameters;
		}

		public ISetList Append(ISet expression)
		{
			this._expressions.Add(expression);
			return this;
		}

		public ISetList Append(params string[] values)
		{
			foreach(string value in values)
			{
				this.Append(new Set(value, this.Parameters.Parameter + value));
			}
			return this;
		}

		public ISetList AppendValue(string name, string value)
		{
			ISet set = new Set(name, value);
			this.Append(set);
			return this;
		}

		public void Clear()
		{
			this._expressions.Clear();
		}

		public string GetSql(string tableAlias = "")
		{
			StringBuilder sb = new StringBuilder();
			foreach(ISet value in this._expressions)
			{
				if (sb.Length > 0)
					sb.Append(", ");
				sb.Append(SuperSql.FormatColumn(value.Name, this.Parameters, tableAlias));
				sb.Append('=');
				sb.Append(value.Value);
			}
			return sb.ToString();
		}

	}

}