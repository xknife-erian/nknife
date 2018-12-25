using System;
using System.Collections.Generic;
using System.Linq;
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
            SerialUtils.RefreshSerialPorts();
            var map = SerialUtils.LocalSerialPorts;
            Console.WriteLine("LocalSerialPorts:");
            foreach (var kv in map)
                Console.WriteLine($"  + {kv.Key}\t:{kv.Value}");
            var port = GetPort();
            StartSync(port);
            Console.ReadLine();
        }

        private static ushort GetPort()
        {
            Console.WriteLine("请输入端口号：");
            var line = Console.ReadLine();
            ushort port = 0;
            while (!ushort.TryParse(line, out port))
            {
                Console.WriteLine("请输入正确的端口号：");
                line = Console.ReadLine();
            }
            return port;
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
                BuildSerialAsk(0x05),
                new FF()
            });
            return pool;
        }

        private static SerialQuestion BuildSerialAsk(byte command)
        {
            var q1 = new SerialQuestion(new[] {command}, true, 100)
            {
                Verify = Verify
            };
            q1.Answered += (s, e) =>
            {
                //当同步时，每次对话所产生应答数据可通过Ask得到
                Console.WriteLine($"{DateTime.Now:HH:mm:ss,fff}:Answered: {e.Item.ToHexString()}");
            };
            return q1;
        }

        private static bool Verify(IJob job)
        {
            if (job is SerialQuestion q)
            {
                var data = q.Data.ToArray();
                var answer = q.Answer.ToArray();
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] != answer[i])
                        return false;
                }
            }
            return true;
        }

        // ReSharper disable once InconsistentNaming
        public class FF : SerialQuestion
        {
            public FF() 
                : base(new byte[] {0xFF}, true, 50)
            {
                Verify = job => Answer.Equals(Data);
            }
        }

        private static void StartAsync(ushort port)
        {
            var config = new SerialConfig(port);
            var channel = new SerialChannel(config);
            channel.DataArrived += (s, e) =>
            {
                //当同步时，应答数据可通过Channel得到
                Console.WriteLine($"{DateTime.Now:HH:mm:ss,fff}:DataArrived: {e.Item.ToHexString()}");
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