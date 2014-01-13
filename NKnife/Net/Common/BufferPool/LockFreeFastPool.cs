// Copyright (C) 2010 OfficeSIP Communications
// This source is subject to the GNU General Public License.
// Please see Notice.txt for details.

using System;
using System.Threading;

namespace Gean.Net.Common.BufferPool
{
	public interface ILockFreePoolItemIndex
	{
		int Index { get; set; }
	}

	/// <summary>
	/// This class is faster than LockFreePool on ~100% theory and ~30% in BufferPoolTest.
	/// But LockFreeFastPool has one major disadvantage, it will not reuse array item slot if pool item lost.
	/// </summary>
	public class LockFreeFastPool<T> : ILockFreePool<T>
		where T : class, ILockFreePoolItem, ILockFreePoolItemIndex, IDisposable, new()
	{
		private readonly LockFreeItem<T>[] _Array;
		private readonly LockFreeStack<T> _Full;
		private Int32 _Created;

		public LockFreeFastPool(int size)
		{
			_Array = new LockFreeItem<T>[size];
			_Full = new LockFreeStack<T>(_Array, -1, -1);
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
			}
			else
			{
				result = new T();
				result.SetDefaultValue();
				result.Index = -1;

				if (_Created < _Array.Length)
				{
					int newIndex = Interlocked.Increment(ref _Created) - 1;
					if (newIndex < _Array.Length)
						result.Index = newIndex;
#if DEBUG
					else
						throw new Exception(@"BufferPool too small");
#endif
				}
			}

			result.IsPooled = false;
			return result;
		}

		public T GetIfSpaceAvailable()
		{
			T result = default(T);

			int index = _Full.Pop();
			if (index >= 0)
			{
				result = _Array[index]._Value;
				_Array[index]._Value = default(T);
			}
			else
			{
				if (_Created >= _Array.Length)
					return default(T);

				int newIndex = Interlocked.Increment(ref _Created) - 1;

				if (newIndex >= _Array.Length)
					return default(T);

				result = new T();
				result.SetDefaultValue();
				result.Index = newIndex;
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

			int index = value.Index;
			if (index >= 0)
			{
				value.SetDefaultValue();

				_Array[index]._Value = value;

				_Full.Push(index);
			}
			else
			{
				value.Dispose();
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
