﻿using System.Collections.Generic;

namespace NKnife.Storages.SQL.Interfaces.Sql
{

	public interface IWhereList
	{

		IFormatter Parameters { get; }

		IEnumerable<IWhere> Expressions { get; }

		string GetSql(string tableAlias = "");

		int Count { get; }

		void Clear();

		IWhereList Append(IWhere expression);

		string TableAlias { get; set; }

		IWhereList SetTableAlias(string tableAlias = "");

		//Flags
		Enums.WhereLogic LogicOperator { get; }

		//Parenthesis
		int Level { get; }

		bool HasOpenedParenthesis { get; }

		IWhereList OpenParenthesis(int count = 1);

		IWhereList CloseParenthesis(int count = 1);

		IWhereList RawParenthesis(string rawSql);

		//Logic
		IWhereList And();

		IWhereList Or();

		IWhereList AndNot();

		//Exp eq
		IWhereList Raw(string rawSql);

		IWhereList In(string column, params string[] rawSql);

		IWhereList Equal(params string[] columns);

		IWhereList EqualValue(string column, string value);

		IWhereList NotEqual(params string[] columns);

		IWhereList NotEqualValue(string column, string value);

		//Exp less/greater
		IWhereList EqualGreater(params string[] columns);

		IWhereList EqualGreaterValue(string column, string value);

		IWhereList EqualLess(params string[] columns);

		IWhereList EqualLessValue(string column, string value);

		IWhereList Greater(params string[] columns);

		IWhereList GreaterValue(string column, string value);

		IWhereList Less(params string[] columns);

		IWhereList LessValue(string column, string value);

		//Exp null
		IWhereList IsNULL(params string[] columns);

		IWhereList IsNotNULL(params string[] columns);

		//Exp between/like
		IWhereList Between(string column, string begin, string end);

		IWhereList NotBetween(string column, string begin, string end);

		IWhereList Like(string column, string pattern);

		IWhereList NotLike(string column, string pattern);

	}

}