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
            logger.Info("测试原生NLog日志记录....（看到本条日志，表示成功）");
            Console.ReadLine();
        }
    }
}
