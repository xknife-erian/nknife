using System.Collections.Generic;
using System.Text;
using NKnife.Storages.SQL.Common;
using NKnife.Storages.SQL.Enums;
using NKnife.Storages.SQL.Exceptions;
using NKnife.Storages.SQL.Interfaces;
using NKnife.Storages.SQL.Interfaces.Sql;

namespace NKnife.Storages.SQL.Realisations.Sql
{
    public class WhereList : IWhereList
    {
        private readonly List<IWhere> _expressions;

        #region Construcor

        public WhereList(IFormatter parameters)
        {
            _expressions = new List<IWhere>();
            Parameters = parameters;
        }

        #endregion

        public override string ToString()
        {
            return GetSql();
        }

        #region Properties

        public IFormatter Parameters { get; }

        public WhereLogic LogicOperator { get; private set; } = WhereLogic.And;

        public bool HasOpenedParenthesis { get; private set; }

        public int Level { get; private set; }

        public string TableAlias { get; set; }

        public IEnumerable<IWhere> Expressions => _expressions;

        #endregion

        #region Logic operators

        public IWhereList And()
        {
            LogicOperator = WhereLogic.And;
            return this;
        }

        public IWhereList Or()
        {
            LogicOperator = WhereLogic.Or;
            return this;
        }

        public IWhereList AndNot()
        {
            LogicOperator = WhereLogic.AndNot;
            return this;
        }

        #endregion

        #region Expressions List

        public int Count => _expressions.Count;

        public IWhereList Append(IWhere expression)
        {
            _expressions.Add(expression);
            return this;
        }

        public IWhereList SetTableAlias(string tableAlias = "")
        {
            TableAlias = tableAlias;
            return this;
        }

        public void Clear()
        {
            _expressions.Clear();
        }

        private void CreateExpression(WhereType type, string column, string value, string prefix = "", string postfix = "")
        {
            IWhere exp = new Where(type, LogicOperator)
            {
                Column = column,
                IsColumn = true,
                IsRaw = false,
                Value = value,
                Prefix = prefix,
                Postfix = postfix,
                TableAlias = TableAlias
            };
            Append(exp);
        }

        private void CreateParenthesis(Parenthesis parenthesis, string value = "")
        {
            IWhere exp = new Where(WhereType.None, LogicOperator, parenthesis)
            {
                Value = value,
                TableAlias = TableAlias
            };
            Append(exp);
        }

        #endregion

        #region Expressions

        public IWhereList Raw(string rawSql)
        {
            IWhere exp = new Where(WhereType.Raw, LogicOperator)
            {
                IsColumn = false,
                IsRaw = true,
                Value = rawSql,
                TableAlias = string.Empty
            };
            Append(exp);
            return this;
        }

        public IWhereList In(string column, params string[] rawSql)
        {
            var sb = new StringBuilder();
            foreach (var expression in rawSql)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(expression);
            }

            CreateExpression(WhereType.In, column, " IN (" + sb + ")");
            return this;
        }

        public IWhereList EqualValue(string column, string value)
        {
            CreateExpression(WhereType.Equal, column, '=' + value);
            return this;
        }

        public IWhereList Equal(params string[] columns)
        {
            foreach (var expression in columns)
                EqualValue(expression, Parameters.Parameter + expression);
            return this;
        }

        public IWhereList NotEqualValue(string column, string value)
        {
            CreateExpression(WhereType.NotEqual, column, "!=" + value);
            return this;
        }

        public IWhereList NotEqual(params string[] columns)
        {
            foreach (var expression in columns)
                NotEqualValue(expression, Parameters.Parameter + expression);
            return this;
        }

        public IWhereList EqualLessValue(string column, string value)
        {
            CreateExpression(WhereType.EqualLess, column, "<=" + value);
            return this;
        }

        public IWhereList EqualLess(params string[] columns)
        {
            foreach (var expression in columns)
                EqualLessValue(expression, Parameters.Parameter + expression);
            return this;
        }

        public IWhereList EqualGreaterValue(string column, string value)
        {
            CreateExpression(WhereType.EqualGreater, column, ">=" + value);
            return this;
        }

        public IWhereList EqualGreater(params string[] columns)
        {
            foreach (var expression in columns)
                EqualGreaterValue(expression, Parameters.Parameter + expression);
            return this;
        }

        public IWhereList LessValue(string column, string value)
        {
            CreateExpression(WhereType.Less, column, "<" + value);
            return this;
        }

        public IWhereList Less(params string[] columns)
        {
            foreach (var expression in columns)
                LessValue(expression, Parameters.Parameter + expression);
            return this;
        }

        public IWhereList GreaterValue(string column, string value)
        {
            CreateExpression(WhereType.Less, column, ">" + value);
            return this;
        }

        public IWhereList Greater(params string[] columns)
        {
            foreach (var expression in columns)
                GreaterValue(expression, Parameters.Parameter + expression);
            return this;
        }

        public IWhereList IsNULL(params string[] columns)
        {
            foreach (var expression in columns) CreateExpression(WhereType.IsNULL, expression, " IS NULL");
            return this;
        }

        public IWhereList IsNotNULL(params string[] columns)
        {
            foreach (var expression in columns) CreateExpression(WhereType.IsNotNULL, expression, " IS NOT NULL");
            return this;
        }

        public IWhereList Between(string name, string begin, string end)
        {
            var value = $" BETWEEN {Parameters.Parameter}{begin} AND {Parameters.Parameter}{end}";
            CreateExpression(WhereType.Between, name, value);
            return this;
        }

        public IWhereList NotBetween(string name, string begin, string end)
        {
            var value = $" NOT BETWEEN {Parameters.Parameter}{begin} AND {Parameters.Parameter}{end}";
            CreateExpression(WhereType.NotBetween, name, value);
            return this;
        }

        public IWhereList Like(string name, string pattern)
        {
            var value = $"{Parameters.Parameter}{name} LIKE {pattern}";
            CreateExpression(WhereType.Like, "", value);
            return this;
        }

        public IWhereList NotLike(string name, string pattern)
        {
            var value = $"{Parameters.Parameter}{name} NOT LIKE {pattern}";
            CreateExpression(WhereType.NotLike, "", value);
            return this;
        }

        #endregion

        #region Parenthesis

        public IWhereList OpenParenthesis(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                CreateParenthesis(Parenthesis.OpenParenthesis);
                HasOpenedParenthesis = true;
                Level++;
            }

            return this;
        }

        public IWhereList CloseParenthesis(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                if (Level == 0)
                    throw new ParenthesisExpectedException();

                CreateParenthesis(Parenthesis.CloseParenthesis);
                Level--;
                HasOpenedParenthesis = Level > 0;
            }

            return this;
        }

        public IWhereList RawParenthesis(string rawSql)
        {
            OpenParenthesis();
            Raw(rawSql);
            CloseParenthesis();
            return this;
        }

        #endregion

        #region Render SQL

        public string GetSql(string tableAlias = "")
        {
            var sb = new StringBuilder();

            bool logic = false, lastparenthesis = false;
            foreach (var expression in _expressions)
            {
                if (logic && !lastparenthesis && expression.Parenthesis != Parenthesis.CloseParenthesis)
                {
                    sb.Append(' ');
                    sb.Append(GetSqlCurrentLogic(expression.Logic));
                    sb.Append(' ');
                }
                else
                {
                    logic = true;
                }

                if (expression.Parenthesis == Parenthesis.OpenParenthesis)
                {
                    sb.Append('(');
                    lastparenthesis = true;
                }
                else if (expression.Parenthesis == Parenthesis.CloseParenthesis)
                {
                    sb.Append(')');
                    lastparenthesis = false;
                }
                else
                {
                    if (expression.IsColumn)
                        sb.Append(SuperSql.FormatColumn(expression.Column, string.IsNullOrEmpty(expression.TableAlias) ? tableAlias : expression.TableAlias));

                    sb.Append(expression.Value);
                    lastparenthesis = false;
                }
            }

            return sb.ToString();
        }

        private string GetSqlCurrentLogic(WhereLogic logic)
        {
            switch (logic)
            {
                case WhereLogic.AndNot:
                    return "AND NOT";
                case WhereLogic.Or:
                    return "OR";
                default:
                case WhereLogic.And:
                    return "AND";
            }
        }

        #endregion
    }
}