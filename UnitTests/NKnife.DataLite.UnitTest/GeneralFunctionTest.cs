using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NKnife.DataLite.UnitTest.Entities;
using NUnit.Framework;

namespace NKnife.DataLite.UnitTest
{
    [TestFixture]
    public class GeneralFunctionTest
    {
        [Test]
        public void DeleteAllTest()
        {
            var cr = new CompanyRepository();
            cr.DeleteAll();
            cr.Count.Should().Be(0);
        }

        private static CompanyRepository BuildRepository()
        {
            var cr = new CompanyRepository();
            cr.DeleteAll();
            return cr;
        }
    }
}
