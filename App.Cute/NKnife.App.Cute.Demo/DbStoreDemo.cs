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
            _Logger.Info("--����MongoDb���ݳ־û��������ܵ���ʾ��");
            _Logger.Info("Mongo! Mongo!! Mongo!!! Mongo!!!! Mongo!!!!!");
            Console.WriteLine();

            Console.WriteLine();
            _Watch.Restart();
            var count1 = _Store.Count();
            _Watch.Stop();
            _Logger.Info(string.Format("[0]������ԭ���ݸ���:{0}, ��ʱ:{1}", count1, _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Watch.Restart();
            _Store.Clear();
            _Watch.Stop();
            _Logger.Info(string.Format("[0.1]����������ɡ���ʱ:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Watch.Restart();
            for (int i = 0; i < 50; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadStore);//���߳̽��д洢
            }
            _Watch.Stop();
            Console.WriteLine("�ȴ�60����......");
            Thread.Sleep(1000*60);
            var threadCount = _Store.Count();
            _Logger.Debug(string.Format("[0.1.9]��ǰ������:{0}", threadCount));
            _Logger.Info(string.Format("[0.2]������������{0}��ɡ���ʱ:{1}", DEMO_SIZE*10, _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Watch.Restart();
            var count2 = _Store.Count();
            _Watch.Stop();
            _Logger.Info(string.Format("[0.3]���������ݸ���:{0}, ��ʱ:{1}", count2, _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            var array = GetEntities(6);

            _Logger.Info("[1]�־û�������ʵ��....");
            _Watch.Restart();
            _Store.Add(array);
            _Watch.Stop();
            _Logger.Info("[1]�־û���ɡ�" + _Watch.ElapsedMilliseconds);
            Console.WriteLine();

            _Logger.Info("[2]��ѯ�ղų־û���ʵ��....");
            for (int i = 0; i < array.Length; i++)
            {
                _Watch.Restart();
                var tran = _Store.Find(array[i].Id);
                _Watch.Stop();
                _Logger.Info(string.Format("({0}):{1}, {2}", i, tran, _Watch.ElapsedMilliseconds));
            }
            _Logger.Info("[2]��ѯ��ɡ�");
            Console.WriteLine();

            _Logger.Info("[3]���ѯ�ղų־û���ʵ��....");
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
            _Logger.Info("[3]���ѯ��ɡ�" + _Watch.ElapsedMilliseconds);
            Console.WriteLine();

            _Logger.Info("[4]�����ѯ���ԣ���ѯID�ĵ�2λ�ǡ�a��,��4λ�ǡ�1��,��6λ�ǡ�1����ʵ��....");
            _Watch.Restart();
            var tranlist = _Store.Find(Query.Matches("_id", "^.a.1.1.+$"));
            _Watch.Stop();
            var n = 0;
            foreach (var transaction in tranlist)
            {
                _Logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _Logger.Info(string.Format("[4]��ѯ��ɡ���ʱ:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Logger.Info("[5]��ѯ���������QueueId��������....");
            _Watch.Restart();
            var tranlist1 = _Store.Find(Query.Matches("_id", "^.{5}123.+$"), PagerInfo.Empty, SortBy.Ascending("QueueId"), null);
            _Watch.Stop();
            n = 0;
            foreach (var transaction in tranlist1)
            {
                _Logger.Info(string.Format("({1}):{0}", transaction, n));
                n++;
            }
            _Logger.Info(string.Format("[5]��ѯ��ɡ���ʱ:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Logger.Info("[6]��ѯ(��2011.2.4-2011.2.5�����ڵ�����)���������Time��������....");
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
            _Logger.Info(string.Format("[6]��ѯ��ɡ���ʱ:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();

            _Logger.Info("[7]��ѯ(��2012.3.3-2012.3.4����һ���ڵ�����)��ʱ������....");
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
            _Logger.Info(string.Format("[7]��ѯ��ɡ���ʱ:{0}", _Watch.ElapsedMilliseconds));
            Console.WriteLine();
            _Logger.Info(string.Format("[8]��ǰ���������ݸ���:{0}", _Store.Count()));
            Console.WriteLine();
        }

        private static void ThreadStore(object state)
        {
            _Logger.Info(string.Format("[0.1.1]��������"));
            var trans = GetEntities(DEMO_SIZE, false);
            _Logger.Info(string.Format("[0.1.2]����������ɣ�׼���洢"));
            _Store.Add(trans);
            _Logger.Info(string.Format("[0.1.3]�洢��ɡ�"));
            var count = _Store.Count();
            _Logger.Info(string.Format("[0.1.4]���������ݸ���:{0}", count));
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
                    _Logger.Info(string.Format("������ʵ��:({0}),{1}", i, array[i]));
            }
            return array;
        }

    }
}