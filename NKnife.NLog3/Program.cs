using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace NKnife.NLog3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("fdfdf");
            Console.ReadLine();
        }
    }
}
