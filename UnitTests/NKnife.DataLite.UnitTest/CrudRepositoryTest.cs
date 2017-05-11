using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NKnife.DataLite.Exceptions;
using NKnife.DataLite.UnitTest.Entities;
using NUnit.Framework;

namespace NKnife.DataLite.UnitTest
{
    [TestFixture]
    public class CrudRepositoryTest
    {
        private static CompanyRepository BuildRepository()
        {
            var cr = new CompanyRepository();
            cr.DeleteAll();
            return cr;
        }

        [Test]
        public void DeleteAllTest()
        {
            var cr = new CompanyRepository();
            cr.DeleteAll();
            cr.Count.Should().Be(0);
        }

        [Test]
        public void SaveArrayTest1()
        {
            var count = 50;
            var array = new List<Company>();
            for (var i = 0; i < count; i++)
            {
                var company = new Company
                {
                    Id = $"{i}",
                    Address = $"address-{i}",
                    Name = $"name-{i}",
                    WorkerCount = i
                };
                array.Add(company);
            }

            using (var cr = BuildRepository())
            {
                cr.Count.Should().Be(0);
                cr.Save(array).Should().Be(count);
                cr.Count.Should().Be(count);
                for (var i = 0; i < count; i++)
                {
                    var c = cr.FindOne($"{i}");
                    var t = array[i];
                    c.Id.Should().Be(t.Id);
                    c.Address.Should().Be(t.Address);
                    c.Name.Should().Be(t.Name);
                    c.WorkerCount.Should().Be(t.WorkerCount);
                }
            }
        }

        [Test]
        public void SaveArrayTest2()
        {
            var count = 10;
            var array = new List<Company>();
            for (var i = 0; i < count; i++)
            {
                Company company = null;
                if (i % 2 == 0) //有一半为空的对象，Save时应自动跳过
                {
                    company = new Company
                    {
                        Id = $"{i}",
                        Address = $"address-{i}",
                        Name = $"name-{i}",
                        WorkerCount = i
                    };
                }
                array.Add(company);
            }

            using (var cr = BuildRepository())
            {
                cr.Count.Should().Be(0);
                cr.Save(array).Should().Be(count / 2); //只有一半的对象被存储
                cr.Count.Should().Be(count / 2);
            }
        }

        [Test]
        public void SaveArrayTest3()
        {
            var count = 50;
            var array = new List<Company>();
            var id = Guid.NewGuid().ToString();
            for (var i = 0; i < count; i++)
            {
                var company = new Company
                {
                    Id = id,//全部都是重复的ID，故最终存储的效果只有一个document
                    Address = $"address-{i}",
                    Name = $"name-{i}",
                    WorkerCount = i
                };
                array.Add(company);
            }

            using (var cr = BuildRepository())
            {
                cr.Count.Should().Be(0);
                cr.Save(array).Should().Be(1);
                cr.Count.Should().Be(1);

                var c = cr.FindOne(id);
                c.Id.Should().Be(id);

                var t = array[count - 1];//只有最后一个实体
                c.Address.Should().Be(t.Address);
                c.Name.Should().Be(t.Name);
                c.WorkerCount.Should().Be(t.WorkerCount);
            }
        }

        [Test]
        public void SaveTest0()
        {
            using (var cr = BuildRepository())
            {
                Company company = null;
                Action action = () => cr.Save(company);
                action.ShouldThrow<ArgumentByEntityException>().Where(e => e.Message.Contains("实体不能为空"));
            }
        }

        [Test]
        public void SaveTest1()
        {
            using (var cr = BuildRepository())
            {
                var company = new Company();
                company.Id = "companyId";
                company.Address = "companyAddress";
                company.Name = "companyName";
                company.WorkerCount = 123456;
                cr.Save(company).Should().BeTrue();

                var actual = cr.FindOne(company.Id);
                actual.Id.Should().Be(company.Id);
                actual.Address.Should().Be(company.Address);
                actual.Name.Should().Be(company.Name);
                actual.WorkerCount.Should().Be(company.WorkerCount);
            }
        }

        [Test]
        public void SaveTest2()
        {
            using (var cr = BuildRepository())
            {
                cr.Count.Should().Be(0);

                var company1 = new Company();
                company1.Id = "companyId";
                company1.Address = "companyAddress";
                company1.Name = "companyName";
                company1.WorkerCount = 123456;
                cr.Save(company1).Should().BeTrue();
                cr.Count.Should().Be(1);

                var actual = cr.FindOne(company1.Id);
                actual.Address.Should().Be(company1.Address);
                actual.Name.Should().Be(company1.Name);
                actual.WorkerCount.Should().Be(company1.WorkerCount);

                //添加重复ID
                var company2 = new Company();
                company2.Id = "companyId";
                company2.Address = "companyAddress--2";
                company2.Name = "companyName--2";
                company2.WorkerCount = 654321;
                cr.Save(company2).Should().BeFalse(); //当是更新时，返回false
                cr.Count.Should().Be(1);

                actual = cr.FindOne(company2.Id);
                actual.Address.Should().Be(company2.Address);
                actual.Name.Should().Be(company2.Name);
                actual.WorkerCount.Should().Be(company2.WorkerCount);

                //添加重复ID
                var company3 = new Company();
                company3.Id = "companyId";
                company3.Address = "companyAddress--3";
                company3.Name = "companyName--3";
                company3.WorkerCount = 112233;
                cr.Save(company3).Should().BeFalse(); //当是更新时，返回false
                cr.Count.Should().Be(1);

                actual = cr.FindOne(company3.Id);
                actual.Address.Should().Be(company3.Address);
                actual.Name.Should().Be(company3.Name);
                actual.WorkerCount.Should().Be(company3.WorkerCount);

                //添加新ID
                var company0 = new Company();
                company0.Id = "companyId--0";
                company0.Address = "companyAddress--0";
                company0.Name = "companyName--0";
                company0.WorkerCount = 112233;
                cr.Save(company0).Should().BeTrue();
                cr.Count.Should().Be(2);

                actual = cr.FindOne(company0.Id);
                actual.Address.Should().Be(company0.Address);
                actual.Name.Should().Be(company0.Name);
                actual.WorkerCount.Should().Be(company0.WorkerCount);
            }
        }

        [Test]
        public void ExistsAndFindOneTest()
        {
            var count = 50;
            var map = new Dictionary<string, Company>();
            for (var i = 0; i < count; i++)
            {
                var company = new Company
                {
                    Id = Guid.NewGuid().ToString(),
                    Address = $"address-{i}",
                    Name = $"name-{i}",
                    WorkerCount = i
                };
                map.Add(company.Id, company);
            }
            using (var cr = BuildRepository())
            {
                cr.Save(map.Values);
                cr.Count.Should().Be(map.Count);
                foreach (var kv in map)
                {
                    cr.Exists(kv.Key).Should().BeTrue();
                    var company = cr.FindOne(kv.Key);
                    company.Id.Should().Be(kv.Value.Id);
                    company.Address.Should().Be(kv.Value.Address);
                    company.Name.Should().Be(kv.Value.Name);
                    company.WorkerCount.Should().Be(kv.Value.WorkerCount);
                }
            }
        }

        [Test]
        public void FindAllTest1()
        {
            var count = 50;
            var map = new Dictionary<string, Company>();
            for (var i = 0; i < count; i++)
            {
                var company = new Company
                {
                    Id = Guid.NewGuid().ToString(),
                    Address = $"address-{i}",
                    Name = $"name-{i}",
                    WorkerCount = i
                };
                map.Add(company.Id, company);
            }
            using (var cr = BuildRepository())
            {
                cr.Count.Should().Be(0);
                cr.Save(map.Values);
                cr.Count.Should().Be(map.Count);

                var list = cr.FindAll();
                var enumerable = list as Company[] ?? list.ToArray();
                enumerable.Should()
                    .NotBeNull()
                    .And.NotBeEmpty()
                    .And.HaveCount(map.Count);
                foreach (var company in enumerable)
                {
                    map.ContainsKey(company.Id).Should().BeTrue();
                    var old = map[company.Id];
                    old.Address.Should().Be(company.Address);
                    old.Name.Should().Be(company.Name);
                    old.WorkerCount.Should().Be(company.WorkerCount);
                }
            }
        }

        [Test]
        public void FindAllTest2()
        {
            var count = 50;
            var map = new Dictionary<string, Company>();
            for (var i = 0; i < count; i++)
            {
                var company = new Company
                {
                    Id = $"{i}",
                    Address = $"address-{i}",
                    Name = $"name-{i}",
                    WorkerCount = i
                };
                map.Add(company.Id, company);
            }
            using (var cr = BuildRepository())
            {
                cr.Save(map.Values);
                cr.Count.Should().Be(map.Count);
                cr.FindAll().Count().Should().Be(map.Count);

                for (int i = 0; i < count-1; i++)
                {
                    string[] idArray = GetIdArray(i, map);
                    idArray.Should().HaveCount(25);

                    var result = cr.FindAll(idArray).ToArray();//从已存数据中找到指定ID的的数据
                    result.Length.Should().Be(idArray.Length);
                    foreach (var company in result)
                    {
                        var id = company.Id;
                        var src = map[id];
                        company.Id.Should().Be(src.Id);
                        company.Address.Should().Be(src.Address);
                        company.Name.Should().Be(src.Name);
                        company.WorkerCount.Should().Be(src.WorkerCount);
                    }
                }
                
            }
        }

        /// <summary>
        /// 生成一个由25个ID组成的集合，供FindAll方法测试与验证使用
        /// </summary>
        private string[] GetIdArray(int index, Dictionary<string, Company> map)
        {
            var mapkeys = map.Keys.ToArray();
            var array = new List<string>();
            for (int i = 0; i < 25; i++)
            {
                var k = i + index;
                if (k >= mapkeys.Length)
                    k = k - mapkeys.Length;
                var id = mapkeys[k];
                array.Add(id);
            }
            return array.ToArray();
        }

        [Test]
        public void DeleteIdAndFindIdTest()
        {
            var count = 50;
            var map = new Dictionary<string, Company>();
            for (var i = 0; i < count; i++)
            {
                var company = new Company
                {
                    Id = $"{i}",
                    Address = $"address-{i}",
                    Name = $"name-{i}",
                    WorkerCount = i
                };
                map.Add(company.Id, company);
            }
            using (var cr = BuildRepository())
            {
                cr.Save(map.Values);
                cr.Count.Should().Be(map.Count);
                cr.FindAll().Count().Should().Be(map.Count);

                foreach (var kv in map)
                {
                    var id = kv.Key;
                    cr.FindOne(id).Id.Should().Be(id);

                    cr.Delete(id).Should().BeTrue();
                    cr.Exists(id).Should().BeFalse();
                    cr.FindOne(id).Should().BeNull();
                }
                cr.Count.Should().Be(0);
            }
        }
    }
}