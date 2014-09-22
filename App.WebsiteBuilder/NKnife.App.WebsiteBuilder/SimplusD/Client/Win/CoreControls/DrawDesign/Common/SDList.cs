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
        /// ������Ӽ��Ϸ���
        /// </summary>
        /// <param name="collection">����ӵļ���</param>
        /// <param name="isRepeat">�Ƿ�����ظ�(����T��Equals���ж�)</param>
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