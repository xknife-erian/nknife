using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    public class SqliteColumnsss
    {
        public string ColumnName = "";
        public bool PrimaryKey = false;
        public SqliteColumnType ColDataType = SqliteColumnType.Text;
        public bool AutoIncrement = false;
        public bool NotNull = false;
        public string DefaultValue = "";

        public SqliteColumnsss()
        { }

        public SqliteColumnsss(string colName)
        {
            ColumnName = colName;
            PrimaryKey = false;
            ColDataType = SqliteColumnType.Text;
            AutoIncrement = false;
        }

        public SqliteColumnsss(string colName, SqliteColumnType colDataType)
        {
            ColumnName = colName;
            PrimaryKey = false;
            ColDataType = colDataType;
            AutoIncrement = false;
        }

        public SqliteColumnsss(string colName, bool autoIncrement)
        {
            ColumnName = colName;

            if (autoIncrement)
            {
                PrimaryKey = true;
                ColDataType = SqliteColumnType.Integer;
                AutoIncrement = true;
            }
            else
            {
                PrimaryKey = false;
                ColDataType = SqliteColumnType.Text;
                AutoIncrement = false;
            }
        }

        public SqliteColumnsss(string colName, SqliteColumnType colDataType, bool primaryKey, bool autoIncrement, bool notNull, string defaultValue)
        {
            ColumnName = colName;

            if (autoIncrement)
            {
                PrimaryKey = true;
                ColDataType = SqliteColumnType.Integer;
                AutoIncrement = true;
            }
            else
            {
                PrimaryKey = primaryKey;
                ColDataType = colDataType;
                AutoIncrement = false;
                NotNull = notNull;
                DefaultValue = defaultValue;
            }
        }
    }
}
