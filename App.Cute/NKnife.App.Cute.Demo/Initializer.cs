using System;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Implement.Industry.Bank;
using Didaku.Engine.Timeaxis.Kernel;
using Didaku.Engine.Timeaxis.Kernel.IoC;

namespace Timeaxis.Demo
{
    class Initializer
    {
        public static void Run()
        {
            var user = new UserAsBank();
            user.Id = "abc-user";
            user.BookingActivity = new LocaleByQueueMachineBookingActivity();

            if (!Core.Singleton<UserPool>().ContainsKey(user.Id))
                Core.Singleton<UserPool>().Add(user.Id, user);
            var userPool = Core.Singleton<UserPool>();
            Console.WriteLine(userPool.Count);
        }
    }
}