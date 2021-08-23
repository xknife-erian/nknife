using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NKnife.ShareResources;
using NKnife.Util;

namespace NKnife.Chinese
{
    /// <summary>
    ///     中国的城市列表。
    /// </summary>
    public class Cities
    {
        private static Func<string, List<City>> _cityConvertFunc;
        public static List<City> Data { get; set; }

        /// <summary>
        /// 城市列表的生成方法，可以给入外界的方法，也可使用内部的资源文件
        /// <code>
        /// JsonConvert.DeserializeObject
        /// </code>
        /// </summary>
        public static Func<string, List<City>> CityConvertFunc
        {
            get => _cityConvertFunc;
            set
            {
                _cityConvertFunc = value;
                if (value != null)
                {
                    var bs = StringResource.CnCities;
                    var city = Encoding.UTF8.GetString(bs).Substring(1);
                    Data?.Clear();
                    Data = _cityConvertFunc.Invoke(city);
                }
            }
        }

        /// <summary>
        ///     随机生成指定数量的城市（省份+城市）
        /// </summary>
        public StringCollection GetRandomCityName(int count)
        {
            if (Data == null || !Data.Any())
                return new StringCollection();
            var indexArray = new int[count];
            for (var i = 0; i < count; i++)
                indexArray[i] = UtilRandom.Random.Next(0, 34);
            var sc = new StringCollection();
            foreach (var i in indexArray)
            {
                var sheng = Data[i];
                var city = sheng.city.Length > 1
                    ? sheng.city[UtilRandom.Random.Next(0, sheng.city.Length - 1)]
                    : sheng.city[0];
                var area = city.area.Count > 1
                    ? city.area[UtilRandom.Random.Next(0, city.area.Count - 1)]
                    : city.area[0];
                sc.Add(sheng.name == city.name
                    ? $"{city.name}{area}".Replace(" ", "")
                    : $"{sheng.name}{city.name}{area}".Replace(" ", ""));
            }

            return sc;
        }

        public class City
        {
            public string name { get; set; }
            public CityItem[] city { get; set; }
        }

        public class CityItem
        {
            public string name { get; set; }
            public List<string> area { get; set; }
        }
    }
}