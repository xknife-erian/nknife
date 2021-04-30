using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Queries;

namespace NKnife.Storages.SQL.Realisations.Queries
{

	public class Query<T> : IQuery<T>
	{

		public IFormatter Parameters { get; set; }

		public Query() : this(SuperSql.DefaultFormatter)
		{
		}

		public Query(IFormatter parameters)
		{
			this.Parameters = SuperSql.DefaultFormatter;
		}

		#region Methods

		public Delete<T> Delete(string tableAlias = "")
		{
			return new Delete<T>(this.Parameters, tableAlias);
		}

		public Insert<T> Insert(bool autoMapping = false, string tableAlias = "")
		{
			return new Insert<T>(this.Parameters, autoMapping, tableAlias);
		}

		public Update<T> Update(bool autoMapping = false, string tableAlias = "")
		{
			return new Update<T>(this.Parameters, autoMapping, tableAlias);
		}

		public Select<T> Select(string tableAlias = "")
		{
			return new Select<T>(this.Parameters, tableAlias);
		}

		#endregion

		#region Static methods

		public static Delete<T> CreateDelete(string tableAlias = "")
		{
			return new Delete<T>(SuperSql.DefaultFormatter, tableAlias);
		}

		public static Insert<T> CreateInsert(bool autoMapping = false, string tableAlias = "")
		{
			return new Insert<T>(SuperSql.DefaultFormatter, autoMapping, tableAlias);
		}

		public static Select<T> CreateSelect(string tableAlias = "")
		{
			return new Select<T>(SuperSql.DefaultFormatter, tableAlias);
		}

		public static Update<T> CreateUpdate(bool autoMapping = false, string tableAlias = "")
		{
			return new Update<T>(SuperSql.DefaultFormatter, autoMapping, tableAlias);
		}

		#endregion

	}

}