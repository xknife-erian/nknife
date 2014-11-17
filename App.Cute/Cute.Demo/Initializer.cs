using System;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Implement.Industry.Bank;
using NKnife.App.Cute.Kernel.IoC;

namespace Cute.Demo
{
    class Initializer
    {
        public static void Run()
        {
            var user = new UserAsBank
            {
                Id = "abc-user", 
                BookingActivity = new LocaleByQueueMachineBookingActivity()
            };

            if (!Core.Singleton<UserPool>().ContainsKey(user.Id))
                Core.Singleton<UserPool>().Add(user.Id, user);
            var userPool = Core.Singleton<UserPool>();
            Console.WriteLine(userPool.Count);
        }
    }
}