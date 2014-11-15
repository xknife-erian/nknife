using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Didaku;
using Didaku.Data;
using Didaku.Data.MongoDb;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Data;
using Didaku.Engine.Timeaxis.Data.Stores;
using Didaku.Engine.Timeaxis.Implement.Industry.Bank;
using Didaku.Engine.Timeaxis.Kernel.IoC;
using Didaku.Wrapper;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Timeaxis.Demo
{
    class DbStoreDemo
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly Stopwatch _Watch = new Stopwatch();
        private static readonly MongoStore<ITransaction, string> _Store = Core.Singleton<Datas>().Transactions;
        private const int DEMO_SIZE = 5 * 10000;

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine();
            _Logger.Info("--采用MongoDb数据持久化，及性能的演示。");
            _Logger.Info("Mongo! Mongo!! Mongo!!! Mongo!!!! Mongo!!!!!");
            Console.WriteLine();

            Console.WriteLine();
            _Watch.Restart();
            var count1 = _Store.Count();
            _Watch.Stop();
            _Logger.Info(string.Format("[0]集合中原数据个数:{0}, 计时:{1}", count1, _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Watch.Restart();
            _Store.Clear();
            _Watch.Stop();
            _Logger.Info(string.Format("[0.1]集合清理完成。计时:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Watch.Restart();
            for (int i = 0; i < 50; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadStore);//多线程进行存储
            }
            _Watch.Stop();
            Console.WriteLine("等待60秒钟......");
            Thread.Sleep(1000*60);
            var threadCount = _Store.Count();
            _Logger.Debug(string.Format("[0.1.9]当前数据量:{0}", threadCount));
            _Logger.Info(string.Format("[0.2]创建测试数据{0}完成。计时:{1}", DEMO_SIZE*10, _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Watch.Restart();
            var count2 = _Store.Count();
            _Watch.Stop();
            _Logger.Info(string.Format("[0.3]集合中数据个数:{0}, 计时:{1}", count2, _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            var array = GetEntities(6);

            _Logger.Info("[1]持久化创建的实体....");
            _Watch.Restart();
            _Store.Add(array);
            _Watch.Stop();
            _Logger.Info("[1]持久化完成。" + _Watch.ElapsedMilliseconds);
            Console.WriteLine();

            _Logger.Info("[2]查询刚才持久化的实体....");
            for (int i = 0; i < array.Length; i++)
            {
                _Watch.Restart();
                var tran = _Store.Find(array[i].Id);
                _Watch.Stop();
                _Logger.Info(string.Format("({0}):{1}, {2}", i, tran, _Watch.ElapsedMilliseconds));
            }
            _Logger.Info("[2]查询完成。");
            Console.WriteLine();

            _Logger.Info("[3]组查询刚才持久化的实体....");
            var ids = new string[array.Length];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = array[i].Id;
            _Watch.Restart();
            var multiTran = _Store.Find(ids);
            _Watch.Stop();
            int k = 0;
            foreach (var tran in multiTran)
            {
                _Logger.Info(string.Format("({0}):{1}", k, tran));
                k++;
            }
            _Logger.Info("[3]组查询完成。" + _Watch.ElapsedMilliseconds);
            Console.WriteLine();

            _Logger.Info("[4]正则查询测试：查询ID的第2位是“a”,第4位是“1”,第6位是“1”的实体....");
            _Watch.Restart();
            var tranlist = _Store.Find(Query.Matches("_id", "^.a.1.1.+$"));
            _Watch.Stop();
            var n = 0;
            foreach (var transaction in tranlist)
            {
                _Logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _Logger.Info(string.Format("[4]查询完成。耗时:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Logger.Info("[5]查询后排序：针对QueueId进行排序....");
            _Watch.Restart();
            var tranlist1 = _Store.Find(Query.Matches("_id", "^.{5}123.+$"), PagerInfo.Empty, SortBy.Ascending("QueueId"), null);
            _Watch.Stop();
            n = 0;
            foreach (var transaction in tranlist1)
            {
                _Logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _Logger.Info(string.Format("[5]查询完成。耗时:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Logger.Info("[6]查询(在2011.2.4-2011.2.5两天内的数据)后排序：针对Time进行排序....");
            _Watch.Restart();
            var begindate = DateTime.Today.SetYear(2011).SetMonth(2).SetDay(4);
            var enddate = DateTime.Today.SetYear(2011).SetMonth(2).SetDay(5);
            var c1 = Query.GT("Time", new BsonDateTime(begindate));
            var c2 = Query.LT("Time", new BsonDateTime(enddate));
            var where = Query.And(c1, c2);
            var tranlist2 = _Store.Find(where, PagerInfo.Empty, SortBy.Ascending("Time"), null);
            _Watch.Stop();
            n = 0;
            foreach (var transaction in tranlist2)
            {
                _Logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _Logger.Info(string.Format("[6]查询完成。耗时:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Logger.Info("[7]查询(在2012.3.3-2012.3.4，既一天内的数据)后按时间排序....");
            _Watch.Restart();
            var begindate1 = DateTime.Today.SetYear(2012).SetMonth(3).SetDay(3);
            var enddate1 = DateTime.Today.SetYear(2012).SetMonth(3).SetDay(4);
            var c11 = Query.GT("Time", new BsonDateTime(begindate1));
            var c12 = Query.LT("Time", new BsonDateTime(enddate1));
            var where1 = Query.And(c11, c12);
            var tranlist3 = _Store.Find(where1, PagerInfo.Empty, SortBy.Ascending("Time"), null);
            _Watch.Stop();
            n = 0;
            foreach (var transaction in tranlist3)
            {
                _Logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _Logger.Info(string.Format("[7]查询完成。耗时:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();
            _Logger.Info(string.Format("[8]当前集合中数据个数:{0}", _Store.Count()));
            Console.WriteLine();
        }

        private static void ThreadStore(object state)
        {
            _Logger.Info(string.Format("[0.1.1]生成数据"));
            var trans = GetEntities(DEMO_SIZE, false);
            _Logger.Info(string.Format("[0.1.2]生成数据完成，准备存储"));
            _Store.Add(trans);
            _Logger.Info(string.Format("[0.1.3]存储完成。"));
            var count = _Store.Count();
            _Logger.Info(string.Format("[0.1.4]集合中数据个数:{0}", count));
        }

        private static ITransaction[] GetEntities(int size, bool isLog = true)
        {
            var array = new ITransaction[size];
            for (int i = 0; i < size; i++)
            {
                var id = Guid.NewGuid().ToString("N");
                var initDate = DateTime.Now
                    .AddYears(-10)
                    .AddMonths(Program.Random.Next(0, (10 - 1) * 12))
                    .AddDays(Program.Random.Next(100, 800))
                    .AddHours(Program.Random.Next(0, 300))
                    .AddMinutes(Program.Random.Next(0, 3000))
                    .AddSeconds(Program.Random.Next(0, 60));
                var tran = new TicketByQueueMachineTransaction
                {
                    Identifier = string.Format("{0}{1}", 'R', i.ToString().PadLeft(3, '0')), 
                    Owner = Program.Random.Next(1000, 3000),
                    User = id.Substring(8, 8).ToUpper(),
                    Queue = id.Substring(0, 6), 
                    Time = initDate
                };
                array[i] = tran;
                if (isLog)
                    _Logger.Info(string.Format("创建的实体:({0}),{1}", i, array[i]));
            }
            return array;
        }

    }
}