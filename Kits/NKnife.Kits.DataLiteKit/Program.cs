using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NKnife.DataLite;
using NKnife.Kits.DataLiteKit.Entities;

namespace NKnife.Kits.DataLiteKit
{
    class Program
    {
        public const string DbFile = "DataLiteKit";

        static void Main(string[] args)
        {
            Console.WriteLine($"=====> 开始试验....");

            var companys = new CompanyRepository();
            Console.WriteLine($"=====> CompanyRepository.Count:{companys.Count}");
            companys.DeleteAll();
            Console.WriteLine($"=====> CompanyRepository.Count:{companys.Count}");
            Add(companys);
            Console.WriteLine();
            Console.WriteLine($"=====> CompanyRepository.Count:{companys.Count}");
            Console.WriteLine($"=====> 试验完成....");

            Console.ReadLine();
        }

        private static void Add(CompanyRepository companys)
        {
            for (int i = 0; i < 1000 * 2; i++)
            {
                var c = new Company();
                c.Id = Guid.NewGuid().ToString("N");
                c.Name = $"Name:[{i}{i}{i}]{Guid.NewGuid()}";
                c.Address = $"Address:[{i}{i}{i}]{Guid.NewGuid()}";
                c.WorkerCount = (i + 1) * 10000;
                companys.Save(c);
                if (i % 10 == 0)
                    Console.Write(":");
            }
        }
    }

    public class CompanyRepository : PagingAndSortingRepositoryBase<Company, string>
    {
        public CompanyRepository() 
            : base(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Program.DbFile}"))
        {
        }
    }
}
