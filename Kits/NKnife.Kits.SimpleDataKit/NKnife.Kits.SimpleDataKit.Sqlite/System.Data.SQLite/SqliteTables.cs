using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    public class SqliteTables
    {
        public string TableName = "";
        private SqliteColumnCollection _Columns = new SqliteColumnCollection();

        public SqliteColumnCollection Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }

        public SqliteTables()
        { }

        public SqliteTables(string name)
        {
            TableName = name;
        }
    }
}