using NKnife.Storages.SQL.Interfaces.Templates;
using NKnife.Storages.SQL.Realisations.Templates;

namespace NKnife.Storages.SQL.Common
{

	public static class TemplateLibrary
	{

		public static ITemplate Select
		{
			get
			{
				string sql = "{{START}}SELECT {{LIMIT_START}}{{OFFSET_START}}{{PRECOLUMNS}}{{COLUMNS}}{{POSTCOLUMNS}} FROM {{TABLE}}{{JOINS}}{{WHERE}}{{GROUPBY}}{{ORDERBY}}{{LIMIT_END}}{{OFFSET_END}}{{END}}";
				return new Template(sql);
			}
		}

		public static ITemplate Insert
		{
			get
			{
				string sql = "{{START}}INSERT INTO {{TABLE}}({{COLUMNS}}) VALUES({{VALUES}}){{END}}";
				return new Template(sql);
			}
		}

		public static ITemplate Delete
		{
			get
			{
				string sql = "{{START}}DELETE FROM {{TABLE}}{{WHERE}}{{END}}";
				return new Template(sql);
			}
		}

		public static ITemplate Update
		{
			get
			{
				string sql = "{{START}}UPDATE {{TABLE}} SET {{SETS}}{{WHERE}}{{END}}";
				return new Template(sql);
			}
		}

		public static ITemplate Truncate
		{
			get
			{
				string sql = "{{START}}TRUNCATE TABLE {{TABLE}}{{END}}";
				return new Template(sql);
			}
		}

		public static ITemplate DropDatabase
		{
			get
			{
				string sql = "{{START}}DROP DATABASE {{DATABASE}}{{END}}";
				return new Template(sql);
			}
		}

		public static ITemplate DropTable
		{
			get
			{
				string sql = "{{START}}DROP TABLE {{DATABASE}}{{END}}";
				return new Template(sql);
			}
		}

	}

}