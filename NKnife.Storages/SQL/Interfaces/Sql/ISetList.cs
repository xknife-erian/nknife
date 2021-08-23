﻿namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface ISetList
	{

		IFormatter Parameters { get; }

		string GetSql(string tableAlias = "");

		int Count { get; }

		void Clear();

		ISetList Append(ISet expression);

		ISetList Append(params string[] values);

		ISetList AppendValue(string name, string value);

	}

}