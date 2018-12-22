using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NKnife.DataLite.UnitTest.Entities;

namespace NKnife.DataLite.UnitTest
{
    [TestFixture]
    public class PageableTest
    {
        private static readonly Expression<Func<Bike, bool>> _Expression = (bike => bike.Id == 0); 

        [Test]
        public void BaseTest1()
        {
            var comparerMock = new Mock<IComparer<Bike>>();
            //这是我第一次这样使用。“It.IsAny<T>()”，神一般的设计！景仰！
            comparerMock.Setup(comparer => comparer.Compare(It.IsAny<Bike>(), It.IsAny<Bike>())).Returns(1);

            var pa = new Pageable<Bike>(0, 15, comparerMock.Object, _Expression);
            pa.Comparer.Should().Be(comparerMock.Object);
            pa.Predicate.Should().Be(_Expression);
            pa.HasPrevious.Should().BeFalse();
            pa.Offset.Should().Be(0);
            pa.PageSize.Should().Be(15);
            pa.PageNumber.Should().Be(0);

            pa.First().Comparer.Should().Be(comparerMock.Object);
            pa.First().Predicate.Should().Be(_Expression);
            pa.First().HasPrevious.Should().BeFalse();
            pa.First().Offset.Should().Be(0);
            pa.First().PageSize.Should().Be(15);
            pa.First().PageNumber.Should().Be(0);

            pa.Next().Comparer.Should().Be(comparerMock.Object);
            pa.Next().Predicate.Should().Be(_Expression);
            pa.Next().HasPrevious.Should().BeTrue();
            pa.Next().Offset.Should().Be(15);
            pa.Next().PageSize.Should().Be(15);
            pa.Next().PageNumber.Should().Be(1);

            pa.Previous().Comparer.Should().Be(comparerMock.Object);
            pa.Previous().Predicate.Should().Be(_Expression);
            pa.Previous().HasPrevious.Should().BeFalse();
            pa.Previous().Offset.Should().Be(0);
            pa.Previous().PageSize.Should().Be(15);
            pa.Previous().PageNumber.Should().Be(0);

            pa.PreviousOrFirst().Comparer.Should().Be(comparerMock.Object);
            pa.PreviousOrFirst().Predicate.Should().Be(_Expression);
            pa.PreviousOrFirst().HasPrevious.Should().BeFalse();
            pa.PreviousOrFirst().Offset.Should().Be(0);
            pa.PreviousOrFirst().PageSize.Should().Be(15);
            pa.PreviousOrFirst().PageNumber.Should().Be(0);
        }

        [Test]
        public void BaseTest2()
        {
            var comparerMock = new Mock<IComparer<Bike>>();

            var pa = new Pageable<Bike>(1, 15, comparerMock.Object, _Expression);
            pa.Comparer.Should().Be(comparerMock.Object);
            pa.Predicate.Should().Be(_Expression);
            pa.HasPrevious.Should().BeTrue();
            pa.Offset.Should().Be(15);
            pa.PageSize.Should().Be(15);
            pa.PageNumber.Should().Be(1);

            pa.First().Comparer.Should().Be(comparerMock.Object);
            pa.First().Predicate.Should().Be(_Expression);
            pa.First().HasPrevious.Should().BeFalse();
            pa.First().Offset.Should().Be(0);
            pa.First().PageSize.Should().Be(15);
            pa.First().PageNumber.Should().Be(0);

            pa.Next().Comparer.Should().Be(comparerMock.Object);
            pa.Next().Predicate.Should().Be(_Expression);
            pa.Next().HasPrevious.Should().BeTrue();
            pa.Next().Offset.Should().Be(30);
            pa.Next().PageSize.Should().Be(15);
            pa.Next().PageNumber.Should().Be(2);

            pa.Previous().Comparer.Should().Be(comparerMock.Object);
            pa.Previous().Predicate.Should().Be(_Expression);
            pa.Previous().HasPrevious.Should().BeFalse();
            pa.Previous().Offset.Should().Be(0);
            pa.Previous().PageSize.Should().Be(15);
            pa.Previous().PageNumber.Should().Be(0);

            pa.PreviousOrFirst().Comparer.Should().Be(comparerMock.Object);
            pa.PreviousOrFirst().Predicate.Should().Be(_Expression);
            pa.PreviousOrFirst().HasPrevious.Should().BeFalse();
            pa.PreviousOrFirst().Offset.Should().Be(0);
            pa.PreviousOrFirst().PageSize.Should().Be(15);
            pa.PreviousOrFirst().PageNumber.Should().Be(0);
        }

        [Test]
        public void BaseTest3()
        {
            var comparerMock = new Mock<IComparer<Bike>>();

            var pa = new Pageable<Bike>(4, 15, comparerMock.Object, _Expression);
            pa.Comparer.Should().Be(comparerMock.Object);
            pa.Predicate.Should().Be(_Expression);
            pa.HasPrevious.Should().BeTrue();
            pa.Offset.Should().Be(60);
            pa.PageSize.Should().Be(15);
            pa.PageNumber.Should().Be(4);

            pa.First().Comparer.Should().Be(comparerMock.Object);
            pa.First().Predicate.Should().Be(_Expression);
            pa.First().HasPrevious.Should().BeFalse();
            pa.First().Offset.Should().Be(0);
            pa.First().PageSize.Should().Be(15);
            pa.First().PageNumber.Should().Be(0);

            pa.Next().Comparer.Should().Be(comparerMock.Object);
            pa.Next().Predicate.Should().Be(_Expression);
            pa.Next().HasPrevious.Should().BeTrue();
            pa.Next().Offset.Should().Be(75);
            pa.Next().PageSize.Should().Be(15);
            pa.Next().PageNumber.Should().Be(5);

            pa.Previous().Comparer.Should().Be(comparerMock.Object);
            pa.Previous().Predicate.Should().Be(_Expression);
            pa.Previous().HasPrevious.Should().BeTrue();
            pa.Previous().Offset.Should().Be(45);
            pa.Previous().PageSize.Should().Be(15);
            pa.Previous().PageNumber.Should().Be(3);

            pa.PreviousOrFirst().Comparer.Should().Be(comparerMock.Object);
            pa.PreviousOrFirst().Predicate.Should().Be(_Expression);
            pa.PreviousOrFirst().HasPrevious.Should().BeTrue();
            pa.Previous().Offset.Should().Be(45);
            pa.Previous().PageSize.Should().Be(15);
            pa.Previous().PageNumber.Should().Be(3);
        }
    }
}
