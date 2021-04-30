using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using NKnife.Chinese;
using Xunit;

namespace NKnife.UnitTests.Chinese
{
    public class CitiesTest
    {
        [Fact]
        public void CityTest1()
        {
            //传入解析方法
            Cities.CityConvertFunc += JsonConvert.DeserializeObject<List<Cities.City>>;
            Cities.Data.Should().NotBeNull();
            Cities.Data.Count.Should().Be(34);//34个省份
        }

        [Fact]
        public void GetRandomCityNameTest1()
        {
            Cities.CityConvertFunc += JsonConvert.DeserializeObject<List<Cities.City>>;
            var cities = new Cities();
            var count = 10000;
            var cs = cities.GetRandomCityName(count);
            cs.Count.Should().Be(count);
        }
    }
}
