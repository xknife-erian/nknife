using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SDList<T> : List<T>
    {
        /// <summary>
        /// 重载添加集合方法
        /// </summary>
        /// <param name="collection">待添加的集合</param>
        /// <param name="isRepeat">是否可以重复(调用T的Equals来判断)</param>
        public void addRange(IEnumerable<T> collection,bool isRepeat)
        {
            if (isRepeat)
            {
                this.AddRange(collection);
            }
            else
            {
                bool isExist = false;
                foreach (T t in collection)
                {
                    isExist = false;
                    foreach (T t2 in this)
                    {
                        if (t.Equals(t2))
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        this.Add(t);
                    }
                }
            }
        }
    }
}