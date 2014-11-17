using System;
using System.Collections;
using System.Collections.Generic;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Base.Common
{
    public class TransactionCollection : IList<ITransaction>
    {
        protected List<ITransaction> _Transactions = new List<ITransaction>();
 
        public IEnumerator<ITransaction> GetEnumerator()
        {
            return _Transactions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ITransaction item)
        {
            _Transactions.Add(item);
        }

        public void AddRange(IEnumerable<ITransaction> transactions)
        {
            _Transactions.AddRange(transactions);
        }

        public void Clear()
        {
            _Transactions.Clear();
        }

        public bool Contains(ITransaction item)
        {
            return _Transactions.Contains(item);
        }

        public void CopyTo(ITransaction[] array, int arrayIndex)
        {
            _Transactions.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITransaction item)
        {
            return _Transactions.Remove(item);
        }

        public int RemoveAll(Predicate<ITransaction> match)
        {
            return _Transactions.RemoveAll(match);
        }

        public void RemoveRange(int index, int count)
        {
            _Transactions.RemoveRange(index, count);
        }

        public int Count
        {
            get { return _Transactions.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<ITransaction>) _Transactions).IsReadOnly; }
        }

        public int IndexOf(ITransaction item)
        {
            return _Transactions.IndexOf(item);
        }

        public int LastIndexOf(ITransaction item)
        {
            return _Transactions.LastIndexOf(item);
        }

        public void Insert(int index, ITransaction item)
        {
            _Transactions.Insert(index, item);
        }

        public void InsertRange(int index, IEnumerable<ITransaction> transactions)
        {
            _Transactions.InsertRange(index, transactions);
        }

        public void RemoveAt(int index)
        {
            _Transactions.RemoveAt(index);
        }

        public ITransaction this[int index]
        {
            get { return _Transactions[index]; }
            set { _Transactions[index] = value; }
        }

        public void Sort()
        {
            _Transactions.Sort();
        }

        public void Sort(Comparison<ITransaction> comparison)
        {
            _Transactions.Sort(comparison);
        }

        public void Sort(IComparer<ITransaction> comparer)
        {
            _Transactions.Sort(comparer);
        }

        public void Reverse()
        {
            _Transactions.Reverse();
        }

        public void Reverse(int index, int count)
        {
            _Transactions.Reverse(index, count);
        }
    }
}