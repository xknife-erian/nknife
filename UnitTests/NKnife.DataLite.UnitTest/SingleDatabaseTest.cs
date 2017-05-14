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

            bikes.Db.Should().Be(meters.Db);
            meters.Db.Should().Be(bikes.Db);

            bikes.DeleteAll();
            meters.DeleteAll();

            bikes.Count.Should().Be(0);
            meters.Count.Should().Be(0);

            for (int i = 0; i < 100; i++)
            {
                var bike = new Bike();
                bike.Id = i;
                bike.ProductionDate = DateTime.Now;
                bikes.Save(bike).Should().BeTrue();

                var meter = new Meter();
                meter.Id = $"{i}";
                meters.Save(meter).Should().BeTrue();

                bikes.Count.Should().Be(i + 1);
                meters.Count.Should().Be(i + 1);
            }
            bikes.Count.Should().Be(100);
            meters.Count.Should().Be(100);
        }
    }
}
