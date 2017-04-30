using System;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Implement.Industry.Bank;
using NKnife.App.Cute.Kernel;
using NKnife.App.Cute.Kernel.IoC;
using NKnife.IoC;
using NKnife.Utility;

namespace Cute.Demo
{
    class Program
    {
        private static bool _isStop;

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
            DI.Initialize();

            Console.WriteLine();
            Console.WriteLine("--NKnife.App.Cute的演示------------");

            var initializer = new EnvironmentInitializer();
            initializer.Initialize();

            UserPoolDemo();

            Tip();

            while (!_isStop)
            {
                string key = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(key))
                    key = key.ToLower();
                switch (key)
                {
                    case "9":
                        //DbStoreDemo.Run();
                        break;
                    case "b":
                        BankDemo.Run();
                        break;
                    case "x":
                        _isStop = true;
                        break;
                }
                if (!_isStop)
                    Tip();
            }

            Console.WriteLine("--NKnife.App.Cute的演示完成------------");
            Console.ReadKey();
        }

        static void UserPoolDemo()
        {
            for (int i = 0; i < 5; i++)
            {
                var user = new UserAsBank();
                user.Id = Guid.NewGuid().ToString("N").ToUpper();
                user.LoginName = user.Id.Substring(2, 10);
                user.Name = user.Id.Substring(5, 10);
                user.Number = user.Id.Substring(10, 18);
                user.Email = user.Id.Substring(7, 15) + "@icbc.com.cn";
                user.MobilePhone = user.Id.Substring(4, 15);
                DI.Get<UserPool>().Add(user.Id, user);
            }
        }
    }
}