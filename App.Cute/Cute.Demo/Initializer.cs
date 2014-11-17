using System;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Implement.Industry.Bank;
using NKnife.App.Cute.Kernel.IoC;
using NKnife.IoC;

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

            if (!DI.Get<UserPool>().ContainsKey(user.Id))
                DI.Get<UserPool>().Add(user.Id, user);
            var userPool = DI.Get<UserPool>();
            Console.WriteLine(userPool.Count);
        }
    }
}