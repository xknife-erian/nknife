using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            db.book.DeleteAll();

            var stopwatch = new Stopwatch();

            stopwatch.Restart();
            db.book.Insert(id: "X00001", name: string.Format("Abcdefg{0}", 777), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00002", name: string.Format("Abcdefg{0}", 888), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00003", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00004", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00005", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00006", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00007", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00008", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00008", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            db.book.Insert(id: "X00009", name: string.Format("Abcdefg{0}", 999), page: 50);
            stopwatch.Stop();
            Console.WriteLine("1 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            Console.WriteLine("------------");

            stopwatch.Restart();
            var book = db.book.FindById("X00005");
            if (book != null)
                Write(book);
            else
                Console.WriteLine("Has error! Don't find target record!");
            stopwatch.Stop();
            Console.WriteLine("1 record found! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            book = db.book.FindById("X00001");
            if (book != null)
                Write(book);
            else
                Console.WriteLine("Has error! Don't find target record!");
            stopwatch.Stop();
            Console.WriteLine("1 record found! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            book = db.book.FindById("X00009");
            if (book != null)
                Write(book);
            else
                Console.WriteLine("Has error! Don't find target record!");
            stopwatch.Stop();
            Console.WriteLine("1 record found! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            book = db.book.FindById("X00004");
            if (book != null)
                Write(book);
            else
                Console.WriteLine("Has error! Don't find target record!");
            stopwatch.Stop();
            Console.WriteLine("1 record found! process {0}ms.", stopwatch.ElapsedMilliseconds);

            Console.WriteLine("------------");

            stopwatch.Restart();
            int count = 500;
            for (int i = 0; i < count; i++)
            {
                var bookid = string.Format("A{0}", i.ToString().PadLeft(5, '0'));
                db.book.Insert(id: bookid, name: string.Format("Steve{0}", i), page: 50);
                Console.Write('.');
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("{1} record created! process {0}ms.", stopwatch.ElapsedMilliseconds, count);

            Console.ReadKey();
        }

        private static void Write(dynamic book)
        {
            Console.WriteLine(string.Format("[{0}],{1},{2}", book.id, book.name, book.page));
        }
    }
}
