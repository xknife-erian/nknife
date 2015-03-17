using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NKnife.Utility;
using Simple.Data;

namespace NKnife.Kits.SimpleDataKit.Sqlite
{
    class Program
    {
        private const string FILE01 = @"d:\simple-data-books.db";
        private const string FILE02 = ":memory:";
        private static readonly string _connectionString = string.Format("Data Source={0};Version=3", FILE01);

        private static void Main(string[] args)
        {
            var rand = new UtilityRandom();

            Console.WriteLine("==Simple.Data.Sqlite----------");
            if (File.Exists(FILE01))
            {
                File.Delete(FILE01);
            }

            var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            var db = Database.OpenConnection(_connectionString);
            db.UseSharedConnection(conn);

            using (var cmd = new SQLiteCommand())
            {
                cmd.Connection = conn;
                var helper = new SqliteHelpers(cmd);

                var tb = new SqliteTables("book");

                tb.Columns.Add(new SqliteColumnssss("id"));
                tb.Columns.Add(new SqliteColumnssss("name"));
                tb.Columns.Add(new SqliteColumnssss("page", SqliteColumnType.Integer));

                helper.CreateTable(tb);
                Console.WriteLine("Table:books is created!");

                tb = new SqliteTables("voltage");

                tb.Columns.Add(new SqliteColumnssss("id", true));
                tb.Columns.Add(new SqliteColumnssss("value", SqliteColumnType.Decimal));
                tb.Columns.Add(new SqliteColumnssss("time", SqliteColumnType.DateTime));

                helper.CreateTable(tb);
                Console.WriteLine("Table:voltage is created!");
            }

            db.StopUsingSharedConnection();
            conn.Close();

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
                WriteBook(book);
            else
                Console.WriteLine("Has error! Don't find target record!");
            stopwatch.Stop();
            Console.WriteLine("1 record found! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            book = db.book.FindById("X00001");
            if (book != null)
                WriteBook(book);
            else
                Console.WriteLine("Has error! Don't find target record!");
            stopwatch.Stop();
            Console.WriteLine("1 record found! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            book = db.book.FindById("X00009");
            if (book != null)
                WriteBook(book);
            else
                Console.WriteLine("Has error! Don't find target record!");
            stopwatch.Stop();
            Console.WriteLine("1 record found! process {0}ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            book = db.book.FindById("X00004");
            if (book != null)
                WriteBook(book);
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

            Console.WriteLine("------------");

            // ++++用ADO.net原生的进行写操作进行效率比较
            stopwatch.Restart();
            for (int i = 0; i < 50; i++)
            {
                using (var cmd = new SQLiteCommand())
                {
                    cmd.Connection = conn;
                    var bookid = string.Format("B{0}", i.ToString().PadLeft(5, '0'));
                    cmd.CommandText = string.Format("INSERT INTO 'book' VALUES ('{0}', 'SQLiteC{1}', {1});", bookid, i);
                    var j = SqliteUtil.ExecuteNonQuery(cmd);
                    Console.Write(j);
                }
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("50 record created! process {0}ms.", stopwatch.ElapsedMilliseconds);

            // ++++向电压采集值表格填充数据
            Console.WriteLine("------------");
            const int COUNT = 500;
            var vs = new List<double>(COUNT);
            for (int i = 0; i < COUNT; i++)
            {
                var m = rand.Next(100000000, 999999999);
                double n = ((double)m)/1000000000;
                vs.Add(n);
            }

            stopwatch.Restart();
            for (int i = 0; i < COUNT; i++)
            {
                db.voltage.Insert(value: 1 + vs[i], time: DateTime.Now);
                if (i%20 == 0)
                    Console.Write('.');
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine("{1} record created! process {0}ms.", stopwatch.ElapsedMilliseconds, COUNT);

            //下面做一些高级查询试验
            Console.WriteLine("------------");
            stopwatch.Restart();
            var list = db.voltage.All().Select(db.voltage.value, db.voltage.time).Where(db.voltage.value > 1.7).OrderBy(db.voltage.value);
            stopwatch.Stop();
            int size = 0;
            foreach (dynamic voltage in list)
            {
                WriteVoltage(voltage);
                size++;
            }
            Console.WriteLine("{1} record found! process {0}ms.", stopwatch.ElapsedMilliseconds, size);

            Console.WriteLine("------------");
            stopwatch.Restart();
            list = db.voltage.All().Select(db.voltage.value, db.voltage.time)
                .Where(db.voltage.value > 1.2 && db.voltage.value < 1.3).OrderByDescending(db.voltage.value);
            stopwatch.Stop();
            size = 0;
            foreach (dynamic voltage in list)
            {
                WriteVoltage(voltage);
                size++;
            }
            Console.WriteLine("{1} record found! process {0}ms.", stopwatch.ElapsedMilliseconds, size);

            Console.ReadKey();
        }

        private static void WriteVoltage(dynamic voltage)
        {
            Console.WriteLine("{1}, {0}", voltage.value, voltage.time);
        }

        private static void WriteBook(dynamic book)
        {
            Console.WriteLine(string.Format("[{0}],{1},{2}", book.id, book.name, book.page));
        }
    }
}
