using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Queries;
using NKnife.Storages.SQL.Interfaces.Sql;
using NKnife.Storages.SQL.Interfaces.Templates;
using NKnife.Storages.SQL.Realisations.Sql;
using NKnife.Storages.Util;

namespace NKnife.Storages.SQL.Realisations.Queries
{

	public class Delete<T> : IStatementDelete
	{

		public IFormatter Formatter { get; set; }

		public Enums.SqlQuery Query { get; private set; }

		public string TableAlias { get; set; }

		public IWhereList Where { get; set; }

		public Delete(string tableAlias = "") : this(SuperSql.DefaultFormatter, tableAlias)
		{
		}

		public Delete(IFormatter parameters, string tableAlias = "")
		{
			this.Query = Enums.SqlQuery.Delete;
			this.TableAlias = tableAlias;
			this.Formatter = parameters;
			this.Where = new WhereList(this.Formatter);
		}

		public string GetSql(bool EndOfStatement = true)
		{
			string table = Reflection.GetTableName<T>();

			ITemplate result = TemplateLibrary.Delete;
			result.Append(SnippetLibrary.Table(table, this.TableAlias));
			if(this.Where.Count > 0)
				result.Append(SnippetLibrary.Where(this.Where.GetSql(tableAlias: this.TableAlias)));

			return result.GetSql(EndOfStatement);
		}

		public override string ToString()
		{
			return this.GetSql();
		}

		public static Delete<T> DeleteAll()
		{
			Delete<T> result = new Delete<T>();
			return result;
		}

	}

}