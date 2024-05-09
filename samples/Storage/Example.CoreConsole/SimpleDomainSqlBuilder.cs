using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ConsoleUi;
using Dapper;
using Example.Common;
using NKnife.Storages;
using YamlDotNet.Serialization;

namespace Example.CoreConsole
{
    class SimpleDomainSqlBuilder
    {
        public void Run(IMenuUserInterface c)
        {
            var dbType = DatabaseType.SqLite;
            DomainSqlHelper.AddCommonReadConn(dbType, "Data Source=Z:\\FT-water-zone.sqlite;");
            DomainSqlHelper.AddCommonWriteConn(dbType, "Data Source=Z:\\FT-water-zone.sqlite;");

            var domains = new[]
            {
                typeof(Book),
                typeof(BuyingRecord),
                typeof(Person),
                typeof(Publisher)
            };
            var option = new DomainSqlConfig();
            foreach (var domain in domains)
            {
                var array = new List<object>();
                if (typeof(Person) == domain)
                    array = GetPersonDefaultData();
                else if (typeof(Publisher) == domain)
                    array = GetPublisherDefaultData();
                var domainSql = DomainSqlHelper.Build(domain, dbType, array.ToArray());
                option.SqlMap.Add(domainSql.TypeName, domainSql);
            }

            var s = $"..{Path.DirectorySeparatorChar}";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s, s, s, $"config{Path.DirectorySeparatorChar}", "DomainSqlConfig.yaml");
            if (!File.Exists(path))
                c.Error($"文件不存在：{path}");
            c.Debug(path);
            using (var tw = new StreamWriter(path))
            {
                var serializer = new Serializer();
                serializer.Serialize(tw, option);
            }
        }

        private List<object> GetPublisherDefaultData()
        {
            var ps = new List<object>();
            var publisher = new Publisher
            {
                Id = Guid.NewGuid().ToString(), 
                Name = "美国特别不靠谱出版社", 
                Year = 2020, 
                Scale = Scale.Small
            };
            ps.Add(publisher);
            return ps;
        }

        private List<object> GetPersonDefaultData()
        {
            var ps = new List<object>();
            var person1 = new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "李子微",
                Address = "回龙观龙锦苑四区1号楼",
                Email = "abcd@gmail.com",
                HasCollection = false,
            };
            ps.Add(person1); 
            var person2 = new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "赵晓燕",
                Address = "天通苑东四区18号楼1单元",
                Email = "zhaoxiangyan@qq.com",
                HasCollection = true,
            };
            ps.Add(person2);
            return ps;
        }
    }
}
