﻿using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{

	public class JoinItem : IJoinItem
	{

		public bool IsRaw { get; set; }

		public string Column { get; set; }

		public string Value { get; set; }

		public JoinItem(string value) : this(true, string.Empty, value)
		{
		}

		public JoinItem(string column, string value) : this(false, column, value)
		{
		}

		public JoinItem(bool isRaw, string column, string value)
		{
			this.IsRaw = isRaw;
			this.Column = column;
			this.Value = value;
		}

	}

}