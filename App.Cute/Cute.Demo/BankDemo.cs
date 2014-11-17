using System;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Kernel.IoC;
using NKnife.IoC;

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

            Console.WriteLine("1.从使用方向(界面)传入的参数包括：【订户】想要预约的【用户】的【队列】");
            const string ASKER = "xyz-asker";
            const string USER_ID = "abc-user";
            const string QUEUE_ID = "mn-queue";

            Console.WriteLine("1.1.找到属于这个【用户】的BookingActivity");
            var activity = DI.Get<UserPool>()[USER_ID].BookingActivity;

            Console.WriteLine("1.2.将传入的参数封装到IActiveParams中");
            var param = activity.Find().Pack(ASKER, USER_ID, QUEUE_ID);

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
            const string ASKER = "xyz-asker";
            const string USER_ID = "abc-user";
            const string QUEUE_ID = "mn-queue";

            var user = DI.Get<UserPool>()[USER_ID];

            //由【用户】进行预约的分配，返出一个交易
            ITransaction tran;
            var complete = user.Assign(QUEUE_ID, out tran);
            if (tran == null)
                return;//如果没有预约，退出

            var queue = DI.Get<ServiceQueuePool>()[QUEUE_ID];
            var pipeline = user.Pipelines[queue.PipelineId];
            var node = pipeline.FindLast(tran.Owner);
            if (node != null)
            {
                var activityKind = node.Value;

                var activity = DI.Get<ActivityPool>()[activityKind];
                var param = activity.Find().Pack(ASKER, USER_ID, QUEUE_ID, tran);
                ITransaction transaction;
                activity.Ask(param, out transaction);
                Console.WriteLine(transaction);
            }
            Console.WriteLine();
            Console.WriteLine();
        }


    }
}
