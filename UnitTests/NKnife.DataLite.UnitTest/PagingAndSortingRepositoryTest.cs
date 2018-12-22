using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.DataLite.UnitTest.Entities;
using NUnit.Framework;

namespace NKnife.DataLite.UnitTest
{
    [TestFixture]
    public class PagingAndSortingRepositoryTest
    {
        private static CompanyRepository BuildRepository()
        {
            var cr = new CompanyRepository();
            cr.DeleteAll();
            return cr;
        }

        public class CompanyComparer : IComparer<Company>
        {
            #region Implementation of IComparer<in string>

            /// <summary>比较两个对象并返回一个值，该值指示一个对象小于、等于还是大于另一个对象。</summary>
            /// <returns>
            /// 一个有符号整数，
            /// 指示 <paramref name="x" /> 与 <paramref name="y" /> 的相对值，如下表所示。
            /// 值含义小于零<paramref name="x" /> 小于 <paramref name="y" />。
            /// 零<paramref name="x" /> 等于 <paramref name="y" />。
            /// 大于零<paramref name="x" /> 大于 <paramref name="y" />。
            /// </returns>
            /// <param name="x">要比较的第一个对象。</param>
            /// <param name="y">要比较的第二个对象。</param>
            public int Compare(Company x, Company y)
            {
                StringComparer sc = StringComparer.CurrentCulture;
                if (x != null && y != null)
                    return sc.Compare(x.Id, y.Id);
                return 0;
            }

            #endregion
        }

        private readonly Expression<Func<Company, bool>> _expression = (company => company.Id.StartsWith("xxx")); 

        [Test]
        public void Find01()
        {
            var cr = BuildRepository();

            for (int i = 0; i < 100; i++)
            {
                var company = new Company();
                company.Id = Guid.NewGuid().ToString("N");
                if (i >= 50)//设计50条符合要求的记录
                    company.Id = $"xxx{i}{company.Id}";
                cr.Save(company);
            }

            //页码为0，每页12条记录
            var pageable = new Pageable<Company>(0, 12, new CompanyComparer(), _expression);
            var page = cr.FindMulti(pageable);

            page.Number.Should().Be(0);//页码为0

            page.Size.Should().Be(12);//每页12条记录
            page.SizeOfElements.Should().Be(12);//实际12条记录

            page.Content.Count.Should().Be(12);//记录集合中有12条记录
            page.TotalElements.Should().Be(50);//一共有50条符合要求的记录
        }
    }
}
