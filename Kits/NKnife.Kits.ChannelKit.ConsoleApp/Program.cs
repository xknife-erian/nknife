using System;
using NKnife.Channels.Serials;
using NKnife.Interface;
using NKnife.Jobs;

namespace NKnife.Kits.ChannelKit.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss,fff}: 启动NKnife.Channel基本测试...");
            Console.WriteLine();
            var map = SerialUtils.LocalSerialPorts;
            Console.WriteLine("LocalSerialPorts:");
            foreach (var kv in map) Console.WriteLine($"  + {kv.Key}\t:{kv.Value}");
            StartSync(1);
            Console.ReadLine();
        }

        private static void StartSync(ushort port)
        {
            var config = new SerialConfig(port);
            var channel = new SerialChannel(config);
            channel.IsSynchronous = true;
            channel.JobManager = new JobManager();
            channel.JobManager.Pool = GetSyncJobPool();
            if (channel.Open())
                channel.SyncListen();

            channel.Close();
        }

        private static IJobPool GetSyncJobPool()
        {
            var pool = new SerialQuestionPool();
            pool.AddRange(new[]
            {
                BuildSerialAsk(0x00),
                BuildSerialAsk(0x01),
                BuildSerialAsk(0x02),
                BuildSerialAsk(0x03),
                BuildSerialAsk(0x04),
                BuildSerialAsk(0x05)
            });
            return pool;
        }

        private static SerialQuestion BuildSerialAsk(byte command)
        {
            var q1 = new SerialQuestion(new byte[] { command }, true, 100);
            q1.Answered += (s, e) =>
            {
                //当同步时，每次对话所产生应答数据可通过Ask得到
                Console.WriteLine($"Answered: {e.Item.ToHexString()}");
            };
            return q1;
        }

        private static void StartAsync(ushort port)
        {
            var config = new SerialConfig(port);
            var channel = new SerialChannel(config);
            channel.DataArrived += (s, e) =>
            {
                //当同步时，应答数据可通过Channel得到
                Console.WriteLine($"DataArrived: {e.Item.ToHexString()}");
            };
            channel.IsSynchronous = false;
            channel.JobManager = new JobManager();
            channel.JobManager.Pool = new SerialQuestionPool();
            channel.JobManager.Pool.AddRange(new[]
            {
                new SerialQuestion(new byte[] {0x00}, true, 100),
                new SerialQuestion(new byte[] {0x01}, true, 100),
                new SerialQuestion(new byte[] {0x02}, true, 100),
                new SerialQuestion(new byte[] {0x03}, true, 100),
                new SerialQuestion(new byte[] {0x04}, true, 100)
            });
            if (channel.Open())
                channel.AsyncListen();

            channel.Close();
        }
    }
}