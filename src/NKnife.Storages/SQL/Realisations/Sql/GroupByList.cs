﻿using System.Collections.Generic;
using System.Text;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class GroupByList : IGroupByList
	{

		private readonly List<IGroupBy> _expressions;

		public IFormatter Parameters { get; private set; }

		public IColumnsListAggregation Columns { get; private set; }

		public IEnumerable<IGroupBy> Expressions { get; }

		public string TableAlias { get; set; }

		public int Count
		{
			get
			{
				return this._expressions.Count;
			}
		}

		public GroupByList(IFormatter parameters, IColumnsListAggregation columns)
		{
			this._expressions = new List<IGroupBy>();
			this.Parameters = parameters;
			this.Columns = columns;
		}

		public IGroupByList Append(IGroupBy expression, bool copyToColumns = false)
		{
			this._expressions.Add(expression);
			if (copyToColumns)
				this.Columns.Append(expression.Column);
			return this;
		}

		public IGroupByList AppendWithColumn(IGroupBy expression, string column, string columnAlias, string prefix = "", string postfix = "")
		{
			this._expressions.Add(expression);
			this.Columns.AppendAlias(column, columnAlias, prefix, postfix);
			return this;
		}

		public IGroupByList Append(bool copyToColumns = false, params string[] columns)
		{
			foreach (string column in columns)
			{
				GroupBy expression = new GroupBy(column)
				{
					IsRaw = false,
					TableAlias = this.TableAlias,
				};
				this.Append(expression, copyToColumns);
			}
			return this;
		}

		public IGroupByList Raw(params string[] rawSql)
		{
			foreach (string sql in rawSql)
			{
				GroupBy expression = new GroupBy(sql)
				{
					IsRaw = true,
					TableAlias = string.Empty,
				};
				this.Append(expression);
			}
			return this;
		}

		public IGroupByList SetTableAlias(string tableAlias = "")
		{
			this.TableAlias = tableAlias;
			return this;
		}

		public void Clear()
		{
			this._expressions.Clear();
		}

		#region Aggregation

		public IGroupByList FuncMax(string name, string aliasName = "")
		{
			GroupBy expression = new GroupBy(name);
			this.AppendWithColumn(expression, name, aliasName, "MAX(", ")");
			return this;
		}

		public IGroupByList FuncMin(string name, string aliasName = "")
		{
			GroupBy expression = new GroupBy(name);
			this.AppendWithColumn(expression, name, aliasName, "MIN(", ")");
			return this;
		}

		public IGroupByList FuncCount(string name, string aliasName = "")
		{
			GroupBy expression = new GroupBy(name);
			this.AppendWithColumn(expression, name, aliasName, "COUNT(", ")");
			return this;
		}

		public IGroupByList FuncSum(string name, string aliasName = "")
		{
			GroupBy expression = new GroupBy(name);
			this.AppendWithColumn(expression, name, aliasName, "SUM(", ")");
			return this;
		}

		#endregion

		public string GetSql(string tableAlias = "")
		{
			StringBuilder sb = new StringBuilder();

			bool sep = false;
			foreach (IGroupBy expression in this._expressions)
			{
				if (sep)
					sb.Append(", ");
				else
					sep = true;
				if (expression.IsRaw)
					sb.Append(expression.Column);
				else
					sb.Append(SuperSql.FormatColumn(expression.Column, this.Parameters, string.IsNullOrEmpty(expression.TableAlias) ? tableAlias : expression.TableAlias));
			}

			return sb.ToString();
		}

		public override string ToString()
		{
			return this.GetSql();
		}

	}

}