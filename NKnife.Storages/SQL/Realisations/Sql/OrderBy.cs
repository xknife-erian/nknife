using NKnife.Storages.SQL.Enums;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class OrderBy : IOrderBy
	{

		public OrderDirection Direction { get; set; }

		public string Column { get; set; }

		public string TableAlias { get; set; } = string.Empty;

		public bool IsRaw { get; set; } = false;

		public OrderBy(string column, OrderDirection direction = OrderDirection.ASC)
		{
			this.Column = column;
			this.Direction = direction;
		}

		public string GetDirection()
		{
			switch(this.Direction)
			{
				case OrderDirection.RAW:
					return string.Empty;
				case OrderDirection.DESC:
					return "DESC";
				default:
				case OrderDirection.ASC:
					return "ASC";
			}
		}

		public static IOrderBy Ascending(string column, string tableAlias = "")
		{
			IOrderBy result = new OrderBy(column, OrderDirection.ASC);
			result.IsRaw = false;
			result.TableAlias = tableAlias;
			return result;
		}

		public static IOrderBy Descending(string column, string tableAlias = "")
		{
			IOrderBy result = new OrderBy(column, OrderDirection.DESC);
			result.IsRaw = false;
			result.TableAlias = tableAlias;
			return result;
		}

		public static IOrderBy Raw(string value)
		{
			IOrderBy result = new OrderBy(value, OrderDirection.RAW);
			result.IsRaw = true;
			result.TableAlias = string.Empty;
			return result;
		}

	}

}