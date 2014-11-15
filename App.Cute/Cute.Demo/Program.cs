using System;
using System.Threading;
using Didaku;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Kernel;
using Didaku.Engine.Timeaxis.Kernel.IoC;
using NKnife.Utility;

namespace Timeaxis.Demo
{
    class Program
    {
        private static bool _IsStop;

        static Program()
        {
            Random = new UtilityRandom();
        }

        public static UtilityRandom Random { get; private set; }

        private static void Tip()
        {
            Console.WriteLine();
            Console.WriteLine("请按下方提示按键进入功能或性能演示。不分大小写。");
            Console.WriteLine("=============================================");
            Console.WriteLine("按9，进入采用MongoDb的数据持久化，及性能演示，");
            Console.WriteLine("按b，模拟一条简单的银行的工作流：取票->呼叫->请评价->收信评价->办理完成");
            Console.WriteLine("按x，退出演示，");
            Console.WriteLine("=============================================");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("--Didaku.MongoDb.Store的演示开始------------");

            var initializer = new EnvironmentInitializer();
            initializer.Initialize();

            Console.WriteLine(Core.Singleton<ActivityPool>());
            Console.WriteLine(Core.Singleton<IdentifierGeneratorPool>());
            Console.WriteLine(Core.Singleton<ServiceQueuePool>());
            Console.WriteLine(Core.Singleton<UserPool>());

            Tip();

            while (!_IsStop)
            {
                string key = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(key))
                    key = key.ToLower();
                switch (key)
                {
                    case "9":
                        DbStoreDemo.Run();
                        break;
                    case "b":
                        BankDemo.Run();
                        break;
                    case "x":
                        _IsStop = true;
                        break;
                }
                if (!_IsStop)
                    Tip();
            }

            Console.WriteLine("--Didaku.MongoDb.Store的演示完成------------");
            Console.ReadKey();
        }
    }
}