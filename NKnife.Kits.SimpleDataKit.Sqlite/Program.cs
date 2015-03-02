using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Simple.Data;

namespace NKnife.Kits.SimpleDataKit.Sqlite
{
    class Program
    {
        private const string FILE = @"d:\simple-data-books.db";
        private static readonly string _connectionString = string.Format("Data Source={0};Version=3;", FILE);

        private static void Main(string[] args)
        {
            Console.WriteLine("==Simple.Data.Sqlite----------");
            if (File.Exists(FILE))
            {
                File.Delete(FILE);
            }

            var db = Database.OpenConnection(_connectionString);
            SQLiteConnection conn = new SQLiteConnection(_connectionString);
            conn.Open();
            db.UseSharedConnection(conn);

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "";
                SQLiteHelper helper = new SQLiteHelper(cmd);

                SQLiteTable tb = new SQLiteTable("book");

                tb.Columns.Add(new SQLiteColumn("id"));
                tb.Columns.Add(new SQLiteColumn("name"));
                tb.Columns.Add(new SQLiteColumn("page", ColType.Integer));

                helper.CreateTable(tb);
                Console.WriteLine("Table:books is created!");
            }

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
            for (int i = 0; i < 50; i++)
            {
                var bookid = string.Format("A{0}", i.ToString().PadLeft(5, '0'));
                db.book.Insert(id: bookid, name: string.Format("Steve{0}", i), page: 50);
                Console.Write('.');
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("50 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }

        private static void Write(dynamic book)
        {
            Console.WriteLine(string.Format("[{0}],{1},{2}", book.id, book.name, book.page));
        }
    }
}
