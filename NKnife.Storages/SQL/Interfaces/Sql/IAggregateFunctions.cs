namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IAggregateFunctions<out T>
	{

		T FuncMax(string name, string aliasName = "");

		T FuncMin(string name, string aliasName = "");

		T FuncCount(string name, string aliasName = "");

		T FuncSum(string name, string aliasName = "");

	}

}