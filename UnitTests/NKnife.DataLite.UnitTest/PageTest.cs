using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NKnife.DataLite.UnitTest.Entities;
using NUnit.Framework;
using System.Linq.Expressions;
using FluentAssertions;

namespace NKnife.DataLite.UnitTest
{
    [NUnit.Framework.TestFixture]
    public class PageTest
    {
        private static readonly Expression<Func<Meter, bool>> _expression = (meter => string.IsNullOrEmpty(meter.Id));

        [Test]
        public void ConstructorTest()
        {
            var comparerMock = new Mock<IComparer<Meter>>();
            var list = new List<Meter>();

            // 一般情况
            var pageable = new Pageable<Meter>(0, 15, comparerMock.Object, _expression);
            var page = new Page<Meter>(pageable, list, 15);
            page.Should().NotBeNull();
            page.TotalPages.Should().Be(1);
            page.TotalElements.Should().Be(15);

            // 有null值传入
            Action action = () => new Page<Meter>(null, list, 15);
            //action.ShouldThrow<ArgumentNullException>().Where(e => e.Message.Contains("页请求不能为空"));

            pageable = new Pageable<Meter>(0, 15, comparerMock.Object, _expression);
            action = () => new Page<Meter>(pageable, null, 15);
            //action.ShouldThrow<ArgumentNullException>().Where(e => e.Message.Contains("页项目集合不能为空"));

            // 对构造函数进行测试。尤其是Total计算进行测试。

            // 当分页请求，页码100，每页30时：
            pageable = new Pageable<Meter>(100, 30, comparerMock.Object, _expression);
            page = new Page<Meter>(pageable, list);//未给总数量时
            page.Should().NotBeNull();
            page.TotalPages.Should().Be(100);
            page.TotalElements.Should().Be(3000);

            // 当分页请求，页码100，每页30时，共有1000条记录时：
            pageable = new Pageable<Meter>(5, 20, comparerMock.Object, _expression);
            page = new Page<Meter>(pageable, list, 1000);
            page.Should().NotBeNull();
            page.TotalPages.Should().Be(50);
            page.TotalElements.Should().Be(1000);
        }
    }
}
