using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.DataLite.UnitTest.Entities;
using NKnife.DataLite.UnitTest.SingleDatabase;
using NUnit.Framework;

namespace NKnife.DataLite.UnitTest
{
    [TestFixture]
    public class SingleDatabaseTest
    {
        [Test]
        public void SingleDbTest1()
        {
            var bikes = new BikeRepository();
            var meters = new MeterRepository();
            bikes.RepositoryPath.Should().Be(meters.RepositoryPath);
            meters.RepositoryPath.Should().Be(bikes.RepositoryPath);

            bikes.DatabaseToTest.Should().Be(meters.DatabaseToTest);
            meters.DatabaseToTest.Should().Be(bikes.DatabaseToTest);

            bikes.DeleteAll();
            meters.DeleteAll();

            bikes.Count.Should().Be(0);
            meters.Count.Should().Be(0);

            for (int i = 1; i <= 100; i++)
            {
                var meter = new Meter();
                meter.Id = $"{i}";
                meters.Save(meter).Should().BeTrue();

                var bike = new Bike();
                bike.Id = i;
                bike.ProductionDate = DateTime.Now;
                bikes.Save(bike).Should().BeTrue();

                bikes.Count.Should().Be(i);
                meters.Count.Should().Be(i);
            }
            bikes.Count.Should().Be(100);
            meters.Count.Should().Be(100);
        }
    }
}
