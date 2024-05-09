using System.Collections.Generic;
using System.Text;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class ColumnsList : IColumnsList
	{

		protected readonly List<IColumn> _expressions;

		#region Properties

		public IFormatter Parameters { get; set; }

		public string TableAlias { get; set; }

		public IEnumerable<IColumn> Expressions
		{
			get
			{
				return this._expressions;
			}
		}

		public int Count
		{
			get
			{
				return this._expressions.Count;
			}
		}

		#endregion

		#region Constructor

		public ColumnsList(IFormatter parameters)
		{
			this._expressions = new List<IColumn>();
			this.Parameters = parameters;
		}

		#endregion

		public void Clear()
		{
			this._expressions.Clear();
		}

		public string GetSql(string tableAlias = "")
		{
			if (this.Count == 0)
			{
				return string.IsNullOrEmpty(tableAlias)
					? "*"
					: SuperSql.FormatTableAlias(tableAlias, this.Parameters) + ".*";
			}

			string table;
			StringBuilder sb = new StringBuilder();
			foreach (IColumn column in this.Expressions)
			{
				table = string.IsNullOrEmpty(column.TableAlias) ? tableAlias : column.TableAlias;
				if (sb.Length > 0)
					sb.Append(", ");
				if (!string.IsNullOrEmpty(table) && !column.IsRaw && !column.IsAggregation)
					sb.Append(SuperSql.FormatTableAlias(table, this.Parameters) + '.');
				if (column.IsRaw)
					sb.Append(column.Value);
				else
				{
					sb.Append(column.Prefix);
					sb.Append(SuperSql.FormatColumn(column.Value, this.Parameters));
					sb.Append(column.Postfix);
				}
				if (!string.IsNullOrEmpty(column.Alias))
				{
					sb.Append(" as ");
					sb.Append(this.Parameters.AliasEscape);
					sb.Append(column.Alias);
					sb.Append(this.Parameters.AliasEscape);
				}
			}
			return sb.ToString();
		}

		public override string ToString()
		{
			return this.GetSql();
		}

	}

}