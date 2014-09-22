using System;
using System.Collections.Generic;
using System.Data;

namespace Jeelu.Billboard
{
    public class AdHelper
    {
        /// <summary>
        /// 关键词字典，目前存在的问题：
        /// 尚未将每个广告的权重处理进入，故此结构依旧需要修改
        /// </summary>
        public Dictionary<string, List<long>> AdInvertedIndex
        {
            get { return this._AdInvertedIndex; }
            private set { this._AdInvertedIndex = value; }
        }
        private Dictionary<string, List<long>> _AdInvertedIndex = new Dictionary<string, List<long>>();


        /// <summary>
        /// 初始化广告及相应的关键词
        /// 由于会取全部的广告（不包括未完成及已删除的广告）重新处理，所以会较慢
        /// </summary>
        public void Initialize()
        {
            Dictionary<string, List<long>> tempIndex = ReadAd(DbHelper.GetAdvertisements());
            lock (_AdInvertedIndex)
            {
                _AdInvertedIndex.Clear();
                _AdInvertedIndex = tempIndex;
            }
        }

        /// <summary>
        /// 增量更新广告
        /// 由于只会取少量广告，所以会比较快
        /// </summary>
        public void Increase()
        {
            Dictionary<string, List<long>> tempIndex = ReadAd(DbHelper.GetIncreaseAdvertisements());
            lock (_AdInvertedIndex)
            {
                foreach (KeyValuePair<string, List<long>> item in tempIndex)
                {
                    List<long> adInIndex;
                    if (_AdInvertedIndex.TryGetValue(item.Key, out adInIndex))
                    {
                        foreach (var ad in item.Value)
                        {
                            if (!adInIndex.Contains(ad))
                            {
                                adInIndex.Add(ad);
                            }
                        }
                    }
                    else
                    {
                        adInIndex = item.Value;
                        _AdInvertedIndex.Add(item.Key, item.Value);
                    }
                }
            }
        }

        /// <summary>
        /// 更新广告到_keywords
        /// </summary>
        /// <param name="dataTable"></param>
        private static Dictionary<string, List<long>> ReadAd(DataTable dataTable)
        {
            string[] SplitStrings = new string[] { ",", "，", " ", "　", "|", "_", "." };
            List<long> removeList = new List<long>();
            Dictionary<string, List<long>> tempIndex = new Dictionary<string, List<long>>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                long id = (long)dataRow[0];
                if (dataRow[1] != System.DBNull.Value)
                {
                    string keywords = (string)dataRow[1];
                    //TODO: 优化
                    string[] keywordArray = keywords.Split(SplitStrings, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in keywordArray)
                    {
                        List<long> value;
                        if (!tempIndex.TryGetValue(item, out value))
                        {
                            value = new List<long>();
                            tempIndex.Add(item, value);
                        }
                        value.Add(id);
                    }
                }
                if (Convert.ToInt32(dataRow[2]) == 6 && !removeList.Contains(id))
                {
                    removeList.Add(id);
                }
            }
            DbHelper.UpdateAdState(removeList);
            return tempIndex;
        }

    }
}
