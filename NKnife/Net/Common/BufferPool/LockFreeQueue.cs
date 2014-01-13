using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace Gean.Net.Common.BufferPool
{
	[StructLayout(LayoutKind.Explicit)]
	struct LockFreeQueueVars
	{
		[FieldOffset(64)]
		public Int64 Head;
		[FieldOffset(128)]
		public Int64 Tail;
		[FieldOffset(192)]
		public bool HasDequeuePredicate;
		[FieldOffset(256)]
		public Int32 Padding;
	}

	/// <summary>
	/// Non-blocking queue implementation from:
	///		Simple, Fast, and Practical Non-Blocking and Blocking Concurrent Queue Algorithms
	///		Maged M. Michael, Michael L. Scott
	///		http://www.cs.rochester.edu/u/scott/papers/1996_PODC_queues.pdf
	/// </summary>
	class LockFreeQueue<T>
	{
		private readonly LockFreeItem<T>[] _Array;
		private LockFreeQueueVars _QueueVars;
		private Predicate<T> _DequeuePredicate;

		public LockFreeQueue(LockFreeItem<T>[] array1, Int32 enqueueFromDummy, Int32 enqueueCount)
		{
			if (enqueueCount <= 0)
				throw new ArgumentOutOfRangeException(@"enqueueCount", @"Queue must include at least one dummy element");

			_Array = array1;
			_QueueVars.Head = enqueueFromDummy;
			_QueueVars.Tail = enqueueFromDummy + enqueueCount - 1;

			for (Int32 i = 0; i < enqueueCount - 1; i++)
				_Array[i + enqueueFromDummy]._Next = enqueueFromDummy + i + 1;
			_Array[_QueueVars.Tail]._Next = 0xFFFFFFFFL;
		}

		public Predicate<T> DequeuePredicate
		{
			get { return _DequeuePredicate; }
			set
			{
				_DequeuePredicate = value;
				_QueueVars.HasDequeuePredicate = _DequeuePredicate != null;
			}
		}

		public void Enqueue(Int32 index)
		{
		    unchecked
			{
				_Array[index]._Next |= 0xFFFFFFFFL;
			    UInt64 tail1;
			    UInt64 xchg;
			    for (; ; )
				{
					tail1 = (UInt64)Interlocked.Read(ref _QueueVars.Tail);
					var next1 = (UInt64)Interlocked.Read(ref _Array[tail1 & 0xFFFFFFFFUL]._Next);

					if (tail1 == (UInt64)_QueueVars.Tail)
					{
						if ((next1 & 0xFFFFFFFFUL) == 0xFFFFFFFFUL)
						{
							xchg = ((next1 + 0x100000000UL) & 0xFFFFFFFF00000000UL) | ((UInt64)(UInt32)index);
							var next2 = (UInt64)Interlocked.CompareExchange(ref _Array[tail1 & 0xFFFFFFFFUL]._Next, (Int64)xchg, (Int64)next1);
							if (next2 == next1)
								break;
						}
						else
						{
							xchg = ((tail1 + 0x100000000UL) & 0xFFFFFFFF00000000UL) | (next1 & 0xFFFFFFFFUL);
							Interlocked.CompareExchange(ref _QueueVars.Tail, (Int64)xchg, (Int64)tail1);
						}
					}
				}

				xchg = ((tail1 + 0x100000000UL) & 0xFFFFFFFF00000000UL) | ((UInt64)(UInt32)index);
				Interlocked.CompareExchange(ref _QueueVars.Tail, (Int64)xchg, (Int64)tail1);
			}
		}

		public Int32 Dequeue()
		{
			UInt64 head1, head2, tail1, next1, xchg;
			Int32 index;

			unchecked
			{
				for (; ; )
				{
					head1 = (UInt64)Interlocked.Read(ref _QueueVars.Head);
					tail1 = (UInt64)Interlocked.Read(ref _QueueVars.Tail);
					next1 = (UInt64)Interlocked.Read(ref _Array[head1 & 0xFFFFFFFFUL]._Next);

					if (head1 == (UInt64)_QueueVars.Head)
					{
						if ((head1 & 0xFFFFFFFFUL) == (tail1 & 0xFFFFFFFFUL))
						{
							if ((next1 & 0xFFFFFFFFUL) == 0xFFFFFFFFUL)
								return -1;

							xchg = ((tail1 + 0x100000000UL) & 0xFFFFFFFF00000000UL) | (next1 & 0xFFFFFFFFUL);
							Interlocked.CompareExchange(ref _QueueVars.Tail, (Int64)xchg, (Int64)tail1);
						}
						else
						{
							T value = _Array[next1 & 0xFFFFFFFFUL]._Value;

							if (_QueueVars.HasDequeuePredicate && DequeuePredicate(value) == false)
								return -1;

							xchg = ((head1 + 0x100000000UL) & 0xFFFFFFFF00000000UL) | (next1 & 0xFFFFFFFFUL);
							head2 = (UInt64)Interlocked.CompareExchange(ref _QueueVars.Head, (Int64)xchg, (Int64)head1);
							if (head2 == head1)
							{
								index = (Int32)(head1 & 0xFFFFFFFFUL);
								_Array[index]._Value = value;
								return index;
							}
						}
					}
				}
			}
		}
	}
}
