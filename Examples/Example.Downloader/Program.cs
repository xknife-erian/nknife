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

            var url1 = "https://github.com/xknife-erian/nknife.downloader/releases/download/0.1/release1.db";
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

            var url2 = "https://github.com/xknife-erian/nknife.downloader/releases/download/0.1/release2.db";
            var url3 = "https://github.com/xknife-erian/nknife.downloader/releases/download/0.1/release3.db";
            var url4 = "https://github.com/xknife-erian/nknife.downloader/releases/download/0.1/release4.db";
            var url5 = "https://github.com/xknife-erian/nknife.downloader/releases/download/0.1/release5.db";
            var name2 = "release2.db";
            var name3 = "release2.db";
            var name4 = "release2.db";
            var name5 = "release2.db";

            DownloadQueue downloadQueue = new DownloadQueue();
            downloadQueue.QueueCompleted += (s, e) => { Console.WriteLine("Completed!"); };
            downloadQueue.QueueElementCompleted += (s, e) => { Console.WriteLine($"{e.Index}-Completed."); };
            downloadQueue.QueueElementStartedDownloading += (s, e) => { Console.WriteLine("QueueElementStartedDownloading"); };
            downloadQueue.QueueProgressChanged += (s, e) => { Console.WriteLine("QueueProgressChanged"); };

            downloadQueue.Add(url2, Path.Combine(dir, name2));
            downloadQueue.Add(url3, Path.Combine(dir, name3));
            downloadQueue.Add(url4, Path.Combine(dir, name4));
            downloadQueue.Add(url5, Path.Combine(dir, name5));
            downloadQueue.StartAsync();
        }
    }
}
