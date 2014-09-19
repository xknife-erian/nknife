using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public class SnipPagePartCollection : IList<SnipPagePart>, ICollection<SnipPagePart>, IEnumerable<SnipPagePart>, IEnumerable
    {
        private List<SnipPagePart> _innerList = new List<SnipPagePart>();

        public IPartContainer ParentContainer { get; internal set; }

        public SnipPagePartCollection(IPartContainer parentContainer)
        {
            ParentContainer = parentContainer;
        }

        #region IList<SnipPagePart> ��Ա

        public int IndexOf(SnipPagePart part)
        {
            return _innerList.IndexOf(part);
        }

        public void Insert(int index, SnipPagePart part)
        {
            if (ParentContainer == part)
            {
                Debug.Fail("������� ziji");
                return;
            }
            if (ParentContainer is SnipPagePart)
            {
                if (!((SnipPagePart)ParentContainer).IsBox)
                {
                    throw new Exception("�����ڴ���Item�ﲻ�����Part");
                }
                if (((SnipPagePart)ParentContainer).PartType == SnipPartType.Path)
                {
                    if (this.Count > 0 || part.PartType != SnipPartType.Static)
                    {
                        MessageService.Show("���м���͵���Partֻ����һ�����ұ����Ǿ�̬�ģ�");
                    }
                }
            }
            ParentContainer.Designer.CmdManager.AddExecInsertPartCommand(part, ParentContainer, index);
        }

        internal void InsertCore(int index, SnipPagePart part)
        {
            part.ParentContainer = ParentContainer;
            part.Level = ParentContainer.Level + 1;

            _innerList.Insert(index, part);

            ///ά��SnipPagePart��Index
            for (int i = index; i < _innerList.Count; i++)
            {
                _innerList[i].Index = i;
            }

            ParentContainer.LayoutParts();
        }

        public void RemoveAt(int index)
        {
            SnipPagePart part = this._innerList[index];
            Remove(part);
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

        #region ICollection<SnipPagePart> ��Ա

        public void Add(SnipPagePart part)
        {
            
            if (ParentContainer == part)
            {
                throw new ArgumentException("����tianjia ziji");
            }

            if (ParentContainer is SnipPagePart)
            {
                if (!((SnipPagePart)ParentContainer).IsBox)
                {
                    throw new Exception("�����ڴ���Item�ﲻ�����Part");
                }
                if (((SnipPagePart)ParentContainer).PartType == SnipPartType.Path)
                {
                    if (this.Count > 0 || part.PartType != SnipPartType.Static)
                    {
                        MessageService.Show("���м���͵���Partֻ����һ�����ұ����Ǿ�̬�ģ�");
                    }
                }
            }
            ParentContainer.Designer.CmdManager.AddExecAddPartCommand(ParentContainer, part);
        }
        internal void AddCore(SnipPagePart part)
        {
            InsertCore(Count, part);
            //part.ParentContainer = ParentContainer;
            //part.Level = ParentContainer.Level + 1;
            //part.Index = _innerList.Count;

            //_innerList.Add(part);

            //ParentContainer.LayoutParts();
        }

        public void Clear()
        {
            ParentContainer.Designer.CmdManager.BeginBatchCommand();
            ParentContainer.Designer.CmdManager.AddExecClearSelectCommand(ParentContainer.Designer);
            ParentContainer.Designer.CmdManager.AddExecClearPartCommand(ParentContainer.Designer);
            ParentContainer.Designer.CmdManager.EndBatchCommand();
        }
        internal void ClearCore()
        {
            ///��part��ParentContainerȡ��
            foreach (SnipPagePart part in _innerList)
            {
                part.ParentContainer = null;
            }

            _innerList.Clear();
            ParentContainer.Designer.Refresh();
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
            get
            {
                return _innerList.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(SnipPagePart part)
        {
            ParentContainer.Designer.CmdManager.AddExecRemovePartCommand(part);
            return true;
        }

        internal void RemoveCore(SnipPagePart part)
        {
            int index = _innerList.IndexOf(part);
            IPartContainer partContainer = part.ParentContainer;
            part.ParentContainer = null;
            bool bl = _innerList.Remove(part);

            if (bl)
            {
                ///ά��SnipPagePart��Index
                for (int i = index; i < _innerList.Count; i++)
                {
                    _innerList[i].Index = i;
                }

                ///���²���
                partContainer.LayoutParts();
            }
        }

        #endregion

        #region IEnumerable<SnipPagePart> ��Ա

        public IEnumerator<SnipPagePart> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable ��Ա

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_innerList).GetEnumerator();
        }

        #endregion

        public SnipPagePart[] ToArray()
        {
            SnipPagePart[] arr = new SnipPagePart[Count];
            for (int i = 0; i < Count; i++)
            {
                arr[i] = this[i];
            }
            return arr;
        }
    }
}
