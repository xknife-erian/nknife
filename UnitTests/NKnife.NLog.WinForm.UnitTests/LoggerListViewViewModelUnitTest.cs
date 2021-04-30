
using System.Collections.ObjectModel;
using FluentAssertions;
using Xunit;

namespace NKnife.NLog.WinForm.UnitTests
{
    public class LoggerListViewViewModelUnitTest
    {
        [Fact]
        public void SizeCollectionTest1()
        {
            ObservableRangeCollection<int> collection = new ObservableRangeCollection<int>();
            for (int i = 1; i <= 9; i++)
            {
                collection.Add(i);
            }
            int size = 10;
            LoggerListViewViewModel.SizeCollection(collection, size);
            collection.Count.Should().Be(9);
        }
        [Fact]
        public void SizeCollectionTest2()
        {
            ObservableRangeCollection<int> collection = new ObservableRangeCollection<int>();
            for (int i = 1; i <= 10; i++)
            {
                collection.Add(i);
            }
            int size = 10;
            LoggerListViewViewModel.SizeCollection(collection, size);
            collection.Count.Should().Be(size);
        }
        [Fact]
        public void SizeCollectionTest3()
        {
            ObservableRangeCollection<int> collection = new ObservableRangeCollection<int>();
            for (int i = 1; i <= 11; i++)
            {
                collection.Add(i);
            }
            int size = 10;
            LoggerListViewViewModel.SizeCollection(collection, size);
            collection.Count.Should().Be(size);
        }

        [Fact]
        public void SizeCollectionTest4()
        {
            ObservableRangeCollection<int> collection = new ObservableRangeCollection<int>();
            for (int i = 1; i <= 9999; i++)
            {
                collection.Add(i);
            }
            int size = 10;
            LoggerListViewViewModel.SizeCollection(collection, size);
            collection.Count.Should().Be(size);
        }
        [Fact]
        public void SizeCollectionTest5()
        {
            ObservableRangeCollection<int> collection = new ObservableRangeCollection<int>();
            for (int i = 1; i <= 1; i++)
            {
                collection.Add(i);
            }
            int size = 10;
            LoggerListViewViewModel.SizeCollection(collection, size);
            collection.Count.Should().Be(1);
        }
        [Fact]
        public void SizeCollectionTest6()
        {
            ObservableRangeCollection<int> collection = new ObservableRangeCollection<int>();
            int size = 10;
            LoggerListViewViewModel.SizeCollection(collection, size);
            collection.Count.Should().Be(0);
        }
    }
}
