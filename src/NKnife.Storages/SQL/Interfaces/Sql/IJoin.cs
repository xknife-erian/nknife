﻿using System.Collections.Generic;

namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IJoin
	{

		IFormatter Parameters { get; }

		string Table { get; set; }

		Enums.JoinType Type { get; set; }

		IEnumerable<IJoinItem> Expressions { get; }

		IJoin AppendRaw(string rawSql);

		IJoin Append(IJoinItem item);

		IJoin Append(string sourceColumn, string destColumn);

		string GetSql(string sourceTable);

		int Count { get; }

		void Clear();

	}

}