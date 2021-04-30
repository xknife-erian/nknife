using NKnife.Storages.SQL.Realisations.Queries;

namespace NKnife.Storages.SQL.Interfaces.Queries
{

	public interface IQuery<T>
	{

		Select<T> Select(string tableAlias = "");

		Delete<T> Delete(string tableAlias = "");

		Insert<T> Insert(bool autoMapping = false, string tableAlias = "");

		Update<T> Update(bool autoMapping = false, string tableAlias = "");

	}

}