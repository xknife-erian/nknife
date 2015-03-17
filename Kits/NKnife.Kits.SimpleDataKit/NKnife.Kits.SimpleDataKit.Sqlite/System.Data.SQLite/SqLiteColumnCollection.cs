﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.SQLite
{

    public class SqliteColumnCollection : IList<SqliteColumnssss>
    {
        readonly List<SqliteColumnssss> _List = new ListSqliteColumnsssns>();

        private void CheckColumnName(string colName)
        {
            if (_List.Any(t => t.ColumnName == colName))
            {
                throw new Exception("Column name of \"" + colName + "\" is already existed.");
            }
        }

        public int IndexOf(SqliteColumnssss item)
        {
            return _List.IndexOf(item);
        }

        public void Insert(int index, SqliteColumnssss item)
        {
            CheckColumnName(item.ColumnName);

            _List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _List.RemoveAt(index);
        }

        public SqliteColumnssss this[int index]
        {
            get
            {
                return _List[index];
            }
            set
            {
                if (_List[index].ColumnName != value.ColumnName)
                {
                    CheckColumnName(value.ColumnName);
                }

                _List[index] = value;
            }
        }

        public void Add(SqliteColumnssss item)
        {
            CheckColumnName(item.ColumnName);

            _List.Add(item);
        }

        public void Clear()
        {
            _List.Clear();
        }

        public bool Contains(SqliteColumnssss item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(SqliteColumnssss[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SqliteColumnssss item)
        {
            return _List.Remove(item);
        }

        public IEnumerator<SqliteColumnssss> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }
    }

}
