// Copyright (C) 2010 OfficeSIP Communications
// This source is subject to the GNU General Public License.
// Please see Notice.txt for details.

using System;
using System.Threading;

namespace Gean.Net.Common.BufferPool
{
	public interface ILockFreePoolItem
	{
		bool IsPooled { set; }
		void SetDefaultValue();
	}

	public interface ILockFreePool<T> : IDisposable
	{
		T Get();
		void Put(ref T value);
		void Put(T value);
		int Queued { get; }
		int Created { get; }
	}

	public class LockFreePool<T> : ILockFreePool<T>
		where T : class, ILockFreePoolItem, IDisposable, new()
	{
		private readonly LockFreeItem<T>[] _Array;
		private readonly LockFreeStack<T> _Empty;
		private readonly LockFreeStack<T> _Full;
		private Int32 _Created;

		public LockFreePool(int size)
		{
			_Array = new LockFreeItem<T>[size];

			_Full = new LockFreeStack<T>(_Array, -1, -1);
			_Empty = new LockFreeStack<T>(_Array, 0, _Array.Length);
		}

		public void Dispose()
		{
			for (; ; )
			{
				int index = _Full.Pop();

				if (index < 0)
					break;

				_Array[index]._Value.Dispose();
				_Array[index]._Value = default(T);
			}
		}

		public T Get()
		{
			T result = default(T);

			int index = _Full.Pop();
			if (index >= 0)
			{
				result = _Array[index]._Value;
				_Array[index]._Value = default(T);

				_Empty.Push(index);
			}
			else
			{
				result = new T();
				result.SetDefaultValue();

				Interlocked.Increment(ref _Created);
			}

			result.IsPooled = false;
			return result;
		}

		public void Put(ref T value)
		{
			Put(value);
			value = null;
		}

		public void Put(T value)
		{
			value.IsPooled = true;

			int index = _Empty.Pop();
			if (index >= 0)
			{
				value.SetDefaultValue();

				_Array[index]._Value = value;

				_Full.Push(index);
			}
			else
			{
				value.Dispose();
#if DEBUG
				throw new Exception(@"BufferPool too small");
#endif
			}
		}

		public int Queued
		{
			get { return _Full.Length; }
		}

		public int Created
		{
			get { return _Created; }
		}
	}
}
