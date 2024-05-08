using System;
using System.IO;
using NKnife.Downloader;
using NKnife.Downloader.Interfaces;

namespace Example.Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dir = AppDomain.CurrentDomain.BaseDirectory;

            var url1 = "https://gitee.com/xknife/meterknife/releases/download/v1.3.1.10903/MKL.v1.3.1.10903.zip";
            var name1 = "demo1.txt";
            IDownloader downloader = new HttpDownloader(url1, Path.Combine(dir, name1));
            downloader.DownloadProgressChanged += (s, e) => { Console.WriteLine($"-/----{e.Progress}----/{e.Speed}"); };
            downloader.DownloadCompleted += (s, e) =>
            {
                Console.WriteLine();
                Console.WriteLine("Completed!");
                StartQueue();
            };
            downloader.StartAsync();

            Console.ReadKey();
        }

        private static void StartQueue()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;

            var url2 = "https://gitee.com/xknife/meterknife/archive/refs/tags/v1.3.1.10903.zip";
            var url3 = "https://gitee.com/xknife/meterknife/archive/refs/tags/v1.3.1.10903.tar.gz";
            var name2 = "release2.db";
            var name3 = "release3.db";

            DownloadQueue downloadQueue = new DownloadQueue();
            downloadQueue.QueueCompleted += (s, e) => { Console.WriteLine("Completed!"); };
            downloadQueue.QueueElementCompleted += (s, e) => { Console.WriteLine($"{e.Index}-Completed."); };
            downloadQueue.QueueElementStartedDownloading += (s, e) => { Console.WriteLine("QueueElementStartedDownloading"); };
            downloadQueue.QueueProgressChanged += (s, e) => { Console.WriteLine("QueueProgressChanged"); };

            downloadQueue.Add(url2, Path.Combine(dir, name2));
            downloadQueue.Add(url3, Path.Combine(dir, name3));
            downloadQueue.StartAsync();
        }
    }
}
