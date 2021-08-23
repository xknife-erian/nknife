﻿using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{
	public class Where : IWhere
	{

		public Enums.WhereType Type { get; private set; }

		public Enums.WhereLogic Logic { get; private set; }

		public Enums.Parenthesis Parenthesis { get; private set; }

		public string Value { get; set; }

		public bool IsColumn { get; set; }

		public bool IsRaw { get; set; }

		public string Column { get; set; }

		public string TableAlias { get; set; }

		public string Prefix { get; set; }

		public string Postfix { get; set; }

		public Where(Enums.WhereType type, Enums.WhereLogic logic, Enums.Parenthesis parenthesis = Enums.Parenthesis.None)
		{
			this.Type = type;
			this.Logic = logic;
			this.Parenthesis = parenthesis;
		}

	}
}