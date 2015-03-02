using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace NKnife.Kits.SimpleDataKit.Mysql
{
    class Program
    {
        private const string CONNECTION_STRING = "Server=localhost;Database=simpledata;Uid=root;Pwd=;";

        private static void Main(string[] args)
        {
            Console.WriteLine("==Simple.Data.Mysql----------");

            var db = Database.OpenConnection(CONNECTION_STRING);

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
