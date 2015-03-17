using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    public class SqliteTables
    {
        public string TableName = "";
        private SqLiteColumnCollection _Columns = new SqLiteColumnCollection();

        public SqLiteColumnCollection Columns
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