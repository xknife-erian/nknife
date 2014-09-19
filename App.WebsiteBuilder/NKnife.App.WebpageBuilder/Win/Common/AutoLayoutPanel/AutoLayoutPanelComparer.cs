using System.Collections.Generic;
using System.Reflection;
using System;

namespace Jeelu.Win
{
    //internal class AutoLayoutPanelAttPairComparer : IComparer<AutoAttributeData>
    //{
    //    static IComparer<AutoAttributeData> Comparer = null;
    //    static public IComparer<AutoAttributeData> CreateComparer()
    //    {
    //        if (Comparer == null)
    //        {
    //            Comparer = new AutoLayoutPanelAttPairComparer();
    //        }
    //        return Comparer;
    //    }

    //    private AutoLayoutPanelAttPairComparer() { }

    //    public int Compare(
    //        AutoAttributeData x,
    //        AutoAttributeData y)
    //    {
    //        return x.Attribute.GroupBoxIndex - y.Attribute.GroupBoxIndex;
    //    }
    //}
    //ʵ�ֱȽ���������ķ��� by lisuye on 2008��5��29��
    internal class AutoLayoutPanelCtrPairComparer : IComparer<KeyValuePair<int, ValueControl>>
    {
        static IComparer<KeyValuePair<int, ValueControl>> Comparer = null;
        static public IComparer<KeyValuePair<int, ValueControl>> CreateComparer()
        {
            if (Comparer == null)
            {
                Comparer = new AutoLayoutPanelCtrPairComparer();
            }
            return Comparer;
        }

        private AutoLayoutPanelCtrPairComparer() { }

        public int Compare(
            KeyValuePair<int, ValueControl> x,
            KeyValuePair<int, ValueControl> y)
        {
            return x.Key - y.Key;
        }
    }
}
