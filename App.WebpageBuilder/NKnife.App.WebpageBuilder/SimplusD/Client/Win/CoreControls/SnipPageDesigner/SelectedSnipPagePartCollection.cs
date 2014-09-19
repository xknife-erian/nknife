using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Jeelu.SimplusD.Client.Win
{
    [Serializable]
    public class SelectedSnipPagePartCollection : IList<SnipPagePart>, ICollection<SnipPagePart>, IEnumerable<SnipPagePart>, IEnumerable
    {
        private List<SnipPagePart> _innerList = new List<SnipPagePart>();
        //private SnipPageDesigner _designer;

        #region 公共属性
        
        /// <summary>
        /// 获取对应的页面片设计器
        /// </summary>
        public SnipPageDesigner Designer { get; private set; }

        #endregion

        #region 构造函数 SelectedSnipPagePartCollection

        public SelectedSnipPagePartCollection(SnipPageDesigner designer)
        {
            Designer = designer;
        }

        #endregion

        class SnipPagePartIndexComparer : IComparer<SnipPagePart>
        {
            static public IComparer<SnipPagePart> Single = new SnipPagePartIndexComparer();

            #region IComparer<SnipPagePart> 成员

            public int Compare(SnipPagePart x, SnipPagePart y)
            {
                if (x.ParentContainer == y.ParentContainer)
                {
                    return x.Index - y.Index;
                }

                SnipPagePart tempX = x;
                SnipPagePart tempY = y;

                while (true)
                {
                    if (tempX.Level == tempY.Level)
                    {
                        if (tempX.ParentContainer == tempY.ParentContainer)
                        {
                            return tempX.Index - tempY.Index;
                        }
                        tempX = (SnipPagePart)tempX.ParentContainer;
                        tempY = (SnipPagePart)tempY.ParentContainer;
                    }
                    else if (tempX.Level > tempY.Level)
                    {
                        if (tempX.ParentContainer == tempY)
                        {
                            return 1;
                        }
                        tempX = (SnipPagePart)tempX.ParentContainer;
                    }
                    else if (tempX.Level < tempY.Level)
                    {
                        if (tempY.ParentContainer == tempX)
                        {
                            return -1;
                        }
                        tempY = (SnipPagePart)tempY.ParentContainer;
                    }
                }
            }

            #endregion
        }

        #region IList<SnipPagePart> 成员

        public int IndexOf(SnipPagePart part)
        {
            return _innerList.IndexOf(part);
        }

        public void Insert(int index, SnipPagePart part)
        {
            throw new Exception("开发期错误：此方法未被支持。");
            //_innerList.Insert(index, part);
            //_innerList.Sort(SnipPagePartIndexComparer.Single);
        }

        public void RemoveAt(int index)
        {
            Remove(_innerList[index]);

            Designer.OnSelectedPartsChanged(EventArgs.Empty);
        }

        public SnipPagePart this[int index]
        {
            get
            {
                return _innerList[index];
            }
            set
            {
                _innerList[index] = value;
            }
        }

        #endregion

        #region ICollection<SnipPagePart> 成员

        public void Add(SnipPagePart part)
        {
            Designer.CmdManager.AddExecSelectPartCommand(part);
        }

        internal void AddCore(SnipPagePart part)
        {
            _innerList.Add(part);
            part.Invalidate();
            _innerList.Sort(SnipPagePartIndexComparer.Single);

            Designer.OnSelectedPartsChanged(EventArgs.Empty);
        }

        public void Clear()
        {
            Designer.CmdManager.AddExecClearSelectCommand(Designer);
        }
        internal void ClearCore()
        {
            _innerList.Clear();
            Designer.Invalidate();

            Designer.OnSelectedPartsChanged(EventArgs.Empty);
        }

        public bool Contains(SnipPagePart part)
        {
            return _innerList.Contains(part);
        }

        public void CopyTo(SnipPagePart[] array, int arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SnipPagePart part)
        {
            Designer.CmdManager.AddExecUnSelectPartCommand(part);
            return true;
        }

        internal void RemoveCore(SnipPagePart part)
        {
            _innerList.Remove(part);
            part.Invalidate();
            _innerList.Sort(SnipPagePartIndexComparer.Single);

            Designer.OnSelectedPartsChanged(EventArgs.Empty);
        }

        #endregion

        #region IEnumerable<SnipPagePart> 成员

        public IEnumerator<SnipPagePart> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_innerList).GetEnumerator();
        }

        #endregion

        public SnipPagePart[] ToArray()
        {
            SnipPagePart[] arrPart = new SnipPagePart[_innerList.Count];
            for (int i = 0; i < _innerList.Count; i++)
            {
                arrPart[i] = _innerList[i];
            }

            return arrPart;
        }
    }
}
