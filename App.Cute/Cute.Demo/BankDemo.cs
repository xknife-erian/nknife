using System;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Kernel.IoC;

namespace Cute.Demo
{
    class BankDemo
    {
        public static void Run()
        {
            Initializer.Run();
            Run01();
            //Run02();
        }

        private static void Run01()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            var user = Core.Singleton<UserPool>();

            Console.WriteLine("1.从使用方向(界面)传入的参数包括：【订户】想要预约的【用户】的【队列】");
            const string asker = "xyz-asker";
            const string userId = "abc-user";
            const string queueId = "mn-queue";
            Console.WriteLine("1.1.找到属于这个【用户】的BookingActivity");
            var activity = user[userId].BookingActivity;
            Console.WriteLine("1.2.将传入的参数封装到IActiveParams中");
            var param = activity.Find().Parse(asker, userId, queueId);
            Console.WriteLine("1.3.执行Activity，得到相应的Transcation信息");
            ITransaction transaction;
            activity.Ask(param, out transaction);
            Console.WriteLine(transaction);
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void Run02()
        {
            //2.当预约可以被激活时，传入参数：可实施预约事件的时间轴
            const string asker = "xyz-asker";
            const string timeaxisId = "counter-1#";
            const string userId = "abc-user";
            const string queueId = "mn-queue";

            var user = Core.Singleton<UserPool>()[userId];

            //由【用户】进行预约的分配，返出一个交易
            ITransaction tran;
            var complete = user.Assign(timeaxisId, out tran);
            if (tran == null)
                return;//如果没有预约，退出

            var queue = Core.Singleton<ServiceQueuePool>()[queueId];
            var pipeline = user.Pipelines[queue.PipelineId];
            var activityKind = pipeline.FindLast(tran.Owner).Value;

            var activity = Core.Singleton<ActivityPool>()[activityKind];
            var param = activity.Find().Parse(asker, userId, queueId, tran);
            ITransaction transaction;
            activity.Ask(param, out transaction);
            Console.WriteLine(transaction);
            Console.WriteLine();
            Console.WriteLine();
        }


    }
}
