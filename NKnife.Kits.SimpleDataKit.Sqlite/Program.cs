using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Simple.Data;

namespace NKnife.Kits.SimpleDataKit.Sqlite
{
    class Program
    {
        private const string CONNECTION_STRING = @"Data Source=d:\simple-data-books.db;Version=3;";

        private static void Main(string[] args)
        {
            Console.WriteLine("==Simple.Data.Sqlite----------");

            var db = Database.OpenConnection(CONNECTION_STRING);
            SQLiteConnection conn = new SQLiteConnection(CONNECTION_STRING);
            conn.Open();
            db.UseSharedConnection(conn);

            //SELECT COUNT(*) FROM sqlite_master where type='table' and name='Student';

            var isExist = db.sqlite_master.FindByTypeAndName("table", "book");

            var book = db.book.FindById("a001");

            Console.WriteLine(book.id);
            Console.WriteLine(book.name);
            Console.WriteLine(book.page);

            for (int i = 0; i < 100; i++)
            {
                var length = db.book.GetCount();
                Console.WriteLine(length);

                var bookid = string.Format("a{0}", length.ToString().PadLeft(5, '0'));
                db.book.Insert(id: bookid, name: string.Format("Steve{0}", length), page: 50);

                book = db.book.FindById(bookid);

                Console.WriteLine(book.id);
                Console.WriteLine(book.name);
                Console.WriteLine(book.page);
            }

            Console.ReadKey();
        }
    }
}
