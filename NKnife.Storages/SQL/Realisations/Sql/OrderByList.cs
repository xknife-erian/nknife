using System.Collections.Generic;
using System.Text;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class OrderByList : IOrderByList
	{

		private readonly List<IOrderBy> _expressions;

		public IFormatter Parameters { get; private set; }

		public IEnumerable<IOrderBy> Expressions
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

		public string TableAlias { get; set; }

		public OrderByList(IFormatter parameters)
		{
			this._expressions = new List<IOrderBy>();
			this.Parameters = parameters;
		}

		public void Clear()
		{
			this._expressions.Clear();
		}

		public IOrderByList Append(IOrderBy expression)
		{
			this._expressions.Add(expression);
			return this;
		}

		public IOrderByList Ascending(params string[] columns)
		{
			foreach(string column in columns)
			{
				IOrderBy expression = OrderBy.Ascending(column, this.TableAlias);
				this.Append(expression);
			}
			return this;
		}

		public IOrderByList Descending(params string[] columns)
		{
			foreach (string column in columns)
			{
				IOrderBy expression = OrderBy.Descending(column, this.TableAlias);
				this.Append(expression);
			}
			return this;
		}

		public IOrderByList SetTableAlias(string tableAlias = "")
		{
			this.TableAlias = tableAlias;
			return this;
		}

		public IOrderByList Raw(string rawSql)
		{
			IOrderBy expression = OrderBy.Raw(rawSql);
			this.Append(expression);
			return this;
		}

		public string GetSql(string tableAlias = "")
		{
			StringBuilder sb = new StringBuilder();

			bool sep = false;
			foreach (IOrderBy expression in this._expressions)
			{
				if (sep)
					sb.Append(", ");
				else
					sep = true;
				if (expression.IsRaw)
					sb.Append(expression.Column);
				else
					sb.Append(SuperSql.FormatColumn(expression.Column, this.Parameters, string.IsNullOrEmpty(expression.TableAlias) ? tableAlias : expression.TableAlias) + " " + expression.GetDirection());
			}

			return sb.ToString();
		}

		public override string ToString()
		{
			return this.GetSql();
		}

	}

}