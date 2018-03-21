using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using NKnife.DataLite.Exceptions;
using NKnife.DataLite.UnitTest.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NKnife.DataLite.UnitTest
{
    [TestFixture]
    public class RepositoryBaseTest
    {
        [Test]
        public void RepositoryDirectoryTest1()
        {
            Action action = () => new CompanyRepository(" ");
            action.Should().Throw<DatabaseFileInvalidOperationException>().Where(e => e.Message.Contains("数据库文件路径不能为空"));
        }

        [Test]
        public void RepositoryDirectoryTest2()
        {
            Action action = () => new CompanyRepository(string.Empty);
            action.Should().Throw<DatabaseFileInvalidOperationException>().Where(e => e.Message.Contains("数据库文件路径不能为空"));
        }

        [Test]
        public void RepositoryDirectoryTest3()
        {
            var dir = @"****";
            Action action = () => new CompanyRepository(dir);
            action.Should().Throw<DatabaseFileInvalidOperationException>().Where(e => e.Message.Contains("目录无效"));
        }

        [Test]
        public void RepositoryFileTest1()
        {
            var dir = @"d:\~unittest\db-dir\";
            var file = "litedbfile.dbfile";

            var target = Path.Combine(dir, file);
            using (var cr = new CompanyRepository(target))
            {
                cr.Count.Should().Be(0);
                File.Exists(target).Should().BeTrue();
                cr.RepositoryPath.Should().Be(target);
            }
            File.Delete(target);
            Directory.Delete(dir);
        }

        [Test]
        public void RepositoryFileTest2()
        {
            var dir = @"d:\~unittest\db-dir\";
            var file = "litedbfile";

            var target = Path.Combine(dir, file);
            using (var cr = new CompanyRepository(target))
            {
                cr.Count.Should().Be(0);
                File.Exists(target).Should().BeTrue();
                cr.RepositoryPath.Should().Be(target);
            }
            File.Delete(target);
            Directory.Delete(dir);
        }
    }
}
