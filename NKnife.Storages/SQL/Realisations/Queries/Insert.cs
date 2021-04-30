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

	public class Insert<T> : IStatementInsert
	{

		public IFormatter Formatter { get; set; }

		public Enums.SqlQuery Query { get; private set; }


		public string TableAlias { get; set; }

		public IColumnsListSimple Columns { get; set; }

		public IValueList Values { get; set; }

		public Insert(bool autoMapping = false, string tableAlias = "") : this(SuperSql.DefaultFormatter, autoMapping, tableAlias)
		{
		}

		public Insert(IFormatter formatter, bool autoMapping = false, string tableAlias = "")
		{
			this.Query = Enums.SqlQuery.Insert;
			this.Formatter = formatter;
			this.Columns = new ColumnsListSimple(this.Formatter);
			this.Values = new ValueList(this.Formatter);

			if (autoMapping)
				this.Mapping();
		}

		private void Mapping()
		{
			bool ignore;
			string columnName, defaultValue;
			Type type = typeof(T);
			foreach (PropertyInfo property in type.GetProperties())
			{
				ignore = false;
				defaultValue = string.Empty;
				columnName = property.Name.ToLower();

				foreach (Attribute attribute in property.GetCustomAttributes())
				{
					if (attribute is IgnoreInsertAttribute)
						ignore = true;
					if (attribute is InsertDefaultAttribute insertDefault)
						defaultValue = insertDefault.DefaultValue;
					if (attribute is ColumnAttribute clm)
						columnName = clm.Name.ToLower();
				}

				if (!ignore)
				{
					this.Columns.Append(columnName);
					if (defaultValue == string.Empty)
						this.Values.Append(this.Formatter.Parameter + columnName);
					else
						this.Values.Append(defaultValue);
				}
			}
		}

		public IStatementInsert AppendParameters(params string[] parameters)
		{
			this.Columns.Append(parameters);
			this.Values.AppendParameters(parameters);
			return this;
		}

		public string GetSql(bool EndOfStatement = true)
		{
			string table = Reflection.GetTableName<T>();

			ITemplate result = TemplateLibrary.Insert;
			result.Append(SnippetLibrary.Table(table));
			result.Append(SnippetLibrary.Columns(this.Columns.GetSql(this.TableAlias)));
			result.Append(SnippetLibrary.Values(this.Values.GetSql()));

			return result.GetSql(EndOfStatement);
		}

		public override string ToString()
		{
			return this.GetSql();
		}

		public static Insert<T> Mapping(params string[] parameters)
		{
			Insert<T> result = new Insert<T>(true);
			result.AppendParameters(parameters);
			return result;
		}

		public static Insert<T> WithoutMapping(params string[] parameters)
		{
			Insert<T> result = new Insert<T>(false);
			result.AppendParameters(parameters);
			return result;
		}

	}

}