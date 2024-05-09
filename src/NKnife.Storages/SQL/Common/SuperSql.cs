using NKnife.Storages.SQL.Interfaces;

namespace NKnife.Storages.SQL.Common
{
    public static class SuperSql
    {
        static SuperSql()
        {
            DefaultFormatter = FormatterLibrary.MsSql;
        }

        public static IFormatter DefaultFormatter { get; set; }

        public static string FormatColumn(string column, string tableAlias = "")
        {
            return FormatColumn(column, DefaultFormatter, tableAlias);
        }

        public static string FormatColumn(string column, IFormatter parameters, string tableAlias = "")
        {
            if (!string.IsNullOrEmpty(tableAlias))
                tableAlias = $"{FormatTableAlias(tableAlias)}.";

            column = parameters.EscapeEnabled
                ? $"{parameters.ColumnEscapeLeft}{column}{parameters.ColumnEscapeRight}"
                : column;

            return tableAlias + column;
        }

        public static string FormatParameter(string column)
        {
            return FormatParameter(column, DefaultFormatter);
        }

        public static string FormatParameter(string column, IFormatter parameters)
        {
            return $"{parameters.Parameter}{column}";
        }

        public static string FormatTable(string tableName)
        {
            return FormatTable(tableName, DefaultFormatter);
        }

        public static string FormatTable(string tableName, IFormatter parameters)
        {
            return parameters.EscapeEnabled
                ? $"{parameters.TableEscapeLeft}{tableName}{parameters.TableEscapeRight}"
                : tableName;
        }

        public static string FormatTableAlias(string value)
        {
            return FormatTableAlias(value, DefaultFormatter);
        }

        public static string FormatTableAlias(string value, IFormatter parameters)
        {
            return $"{parameters.TableEscapeLeft}{value}{parameters.TableEscapeRight}";
        }

        public static string FormatColumnAlias(string value)
        {
            return FormatColumnAlias(value, DefaultFormatter);
        }

        public static string FormatColumnAlias(string value, IFormatter parameters)
        {
            return $"{parameters.AliasEscape}{value}{parameters.AliasEscape}";
        }
    }
}