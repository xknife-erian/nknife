using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Queries;
using NKnife.Storages.SQL.Interfaces.Sql;
using NKnife.Storages.SQL.Interfaces.Templates;
using NKnife.Storages.SQL.Realisations.Sql;
using NKnife.Storages.Util;

namespace NKnife.Storages.SQL.Realisations.Queries
{

	public class Update<T> : IStatementUpdate
	{

		public IFormatter Formatter { get; set; }

		public Enums.SqlQuery Query { get; private set; }

		public string TableAlias { get; set; }

		public ISetList Sets { get; set; }

		public IWhereList Where { get; set; }

		public Update(bool autoMapping = false, string tableAlias = "") : this(SuperSql.DefaultFormatter, autoMapping, tableAlias)
		{
		}

		public Update(IFormatter parameters, bool autoMapping = false, string tableAlias = "")
		{
			this.Query = Enums.SqlQuery.Update;
			this.Formatter = parameters;
			this.TableAlias = tableAlias;
			this.Sets = new SetList(this.Formatter);
			this.Where = new WhereList(this.Formatter);

			if (autoMapping)
				this.Mapping();
		}

		private void Mapping()
		{
			bool ignore;
			string columnName;
			Type type = typeof(T);
			foreach (PropertyInfo property in type.GetProperties())
			{
				ignore = false;
				columnName = property.Name.ToLower();

				foreach (Attribute attribute in property.GetCustomAttributes())
				{
					if (attribute is IgnoreUpdateAttribute)
						ignore = true;
					if (attribute is ColumnAttribute clm)
						columnName = clm.Name.ToLower();
				}

				if (!ignore)
				{
					this.Sets.AppendValue(columnName, this.Formatter.Parameter + columnName);
				}
			}
		}

		public string GetSql(bool EndOfStatement = true)
		{
			string table = Reflection.GetTableName<T>();

			ITemplate result = TemplateLibrary.Update;
			result.Append(SnippetLibrary.Table(table, this.TableAlias));
			result.Append(SnippetLibrary.Sets(this.Sets.GetSql(this.TableAlias)));
			if (this.Where.Count > 0)
				result.Append(SnippetLibrary.Where(this.Where.GetSql(this.TableAlias)));

			return result.GetSql(EndOfStatement);
		}

		public override string ToString()
		{
			return this.GetSql();
		}

		public static Insert<T> InsertWithMapping(params string[] parameters)
		{
			Insert<T> result = new Insert<T>();
			result.AppendParameters(parameters);
			return result;
		}

		public static Insert<T> InsertWithoutMapping(params string[] parameters)
		{
			Insert<T> result = new Insert<T>(false);
			result.AppendParameters(parameters);
			return result;
		}

	}

}