using System;
using System.Diagnostics;
using System.Threading;
using Common.Logging;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.App.Cute.Datas;
using NKnife.App.Cute.Implement.Industry.Bank;
using NKnife.App.Cute.Kernel.IoC;
using NKnife.Databases;
using NKnife.IoC;

namespace Cute.Demo
{
    class DbStoreDemo
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private static readonly Stopwatch _watch = new Stopwatch();
        private static readonly MongoStore<ITransaction, string> _store = DI.Get<DataService>().Transactions;
        private const int DEMO_SIZE = 5 * 1000;

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine();
            _logger.Info("--采用MongoDb数据持久化，及性能的演示。");
            _logger.Info("Mongo! Mongo!! Mongo!!! Mongo!!!! Mongo!!!!!");
            Console.WriteLine();

            Console.WriteLine();
            _watch.Restart();
            var count1 = _store.Count();
            _watch.Stop();
            _logger.Info(string.Format("[0]集合中原数据个数:{0}, 计时:{1}", count1, _watch.ElapsedMilliseconds));
            Console.WriteLine();

            _watch.Restart();
            _store.Clear();
            _watch.Stop();
            _logger.Info(string.Format("[0.1]集合清理完成。计时:{0}", _watch.ElapsedMilliseconds));
            Console.WriteLine();

            _watch.Restart();
            for (int i = 0; i < 50; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadStore);//多线程进行存储
            }
            _watch.Stop();
            Console.WriteLine("等待60秒钟......");
            Thread.Sleep(1000*60);
            var threadCount = _store.Count();
            _logger.Debug(string.Format("[0.1.9]当前数据量:{0}", threadCount));
            _logger.Info(string.Format("[0.2]创建测试数据{0}完成。计时:{1}", DEMO_SIZE*10, _watch.ElapsedMilliseconds));
            Console.WriteLine();

            _watch.Restart();
            var count2 = _store.Count();
            _watch.Stop();
            _logger.Info(string.Format("[0.3]集合中数据个数:{0}, 计时:{1}", count2, _watch.ElapsedMilliseconds));
            Console.WriteLine();

            var array = GetEntities(6);

            _logger.Info("[1]持久化创建的实体....");
            _watch.Restart();
            _store.Add(array);
            _watch.Stop();
            _logger.Info("[1]持久化完成。" + _watch.ElapsedMilliseconds);
            Console.WriteLine();

            _logger.Info("[2]查询刚才持久化的实体....");
            for (int i = 0; i < array.Length; i++)
            {
                _watch.Restart();
                var tran = _store.Find(array[i].Id);
                _watch.Stop();
                _logger.Info(string.Format("({0}):{1}, {2}", i, tran, _watch.ElapsedMilliseconds));
            }
            _logger.Info("[2]查询完成。");
            Console.WriteLine();

            _logger.Info("[3]组查询刚才持久化的实体....");
            var ids = new string[array.Length];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = array[i].Id;
            _watch.Restart();
            var multiTran = _store.Find(ids);
            _watch.Stop();
            int k = 0;
            if (multiTran != null)
            {
                foreach (var tran in multiTran)
                {
                    _logger.Info(string.Format("({0}):{1}", k, tran));
                    k++;
                }
            }
            _logger.Info("[3]组查询完成。" + _watch.ElapsedMilliseconds);
            Console.WriteLine();

            _logger.Info("[4]正则查询测试：查询ID的第2位是“a”,第4位是“1”,第6位是“1”的实体....");
            _watch.Restart();
            var tranlist = _store.Find(Query.Matches("_id", "^.a.1.1.+$"));
            _watch.Stop();
            var n = 0;
            foreach (var transaction in tranlist)
            {
                _logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _logger.Info(string.Format("[4]查询完成。耗时:{0}", _watch.ElapsedMilliseconds));
            Console.WriteLine();

            _logger.Info("[5]查询后排序：针对QueueId进行排序....");
            _watch.Restart();
            var tranlist1 = _store.Find(Query.Matches("_id", "^.{5}123.+$"), PagerInfo.Empty, SortBy.Ascending("QueueId"), null);
            _watch.Stop();
            n = 0;
            foreach (var transaction in tranlist1)
            {
                _logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _logger.Info(string.Format("[5]查询完成。耗时:{0}", _watch.ElapsedMilliseconds));
            Console.WriteLine();

            _logger.Info("[6]查询(在2011.2.4-2011.2.5两天内的数据)后排序：针对Time进行排序....");
            _watch.Restart();
            var begindate = DateTime.Today.SetYear(2011).SetMonth(2).SetDay(4);
            var enddate = DateTime.Today.SetYear(2011).SetMonth(2).SetDay(5);
            var c1 = Query.GT("Time", new BsonDateTime(begindate));
            var c2 = Query.LT("Time", new BsonDateTime(enddate));
            var where = Query.And(c1, c2);
            var tranlist2 = _store.Find(where, PagerInfo.Empty, SortBy.Ascending("Time"), null);
            _watch.Stop();
            n = 0;
            foreach (var transaction in tranlist2)
            {
                _logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _logger.Info(string.Format("[6]查询完成。耗时:{0}", _watch.ElapsedMilliseconds));
            Console.WriteLine();

            _logger.Info("[7]查询(在2012.3.3-2012.3.4，既一天内的数据)后按时间排序....");
            _watch.Restart();
            var begindate1 = DateTime.Today.SetYear(2012).SetMonth(3).SetDay(3);
            var enddate1 = DateTime.Today.SetYear(2012).SetMonth(3).SetDay(4);
            var c11 = Query.GT("Time", new BsonDateTime(begindate1));
            var c12 = Query.LT("Time", new BsonDateTime(enddate1));
            var where1 = Query.And(c11, c12);
            var tranlist3 = _store.Find(where1, PagerInfo.Empty, SortBy.Ascending("Time"), null);
            _watch.Stop();
            n = 0;
            foreach (var transaction in tranlist3)
            {
                _logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _logger.Info(string.Format("[7]查询完成。耗时:{0}", _watch.ElapsedMilliseconds));
            Console.WriteLine();
            _logger.Info(string.Format("[8]当前集合中数据个数:{0}", _store.Count()));
            Console.WriteLine();
        }

        private static void ThreadStore(object state)
        {
            _logger.Info(string.Format("[0.1.1]生成数据"));
            var trans = GetEntities(DEMO_SIZE, false);
            _logger.Info(string.Format("[0.1.2]生成数据完成，准备存储"));
            _store.Add(trans);
            _logger.Info(string.Format("[0.1.3]存储完成。"));
            var count = _store.Count();
            _logger.Info(string.Format("[0.1.4]集合中数据个数:{0}", count));
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
                    _logger.Info(string.Format("创建的实体:({0}),{1}", i, array[i]));
            }
            return array;
        }

    }
}