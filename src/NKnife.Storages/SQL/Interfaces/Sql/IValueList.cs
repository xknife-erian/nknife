﻿namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IValueList
	{

		IFormatter Parameters { get; }

		string GetSql();

		int Count { get; }

		void Clear();

		IValueList Append(IValue expression);

		IValueList Append(params string[] values);

		IValueList AppendParameters(params string[] values);

	}

}