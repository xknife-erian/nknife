using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace Gean.Net.Common.BufferPool
{
	[StructLayout(LayoutKind.Explicit)]
	struct LockFreeStackVars
	{
		[FieldOffset(0)]
		public Int64 Head;
		[FieldOffset(64)]
		public Int32 Padding;
	}

	class LockFreeStack<T>
	{
		private LockFreeStackVars _StackVars;
		private readonly LockFreeItem<T>[] _Array;

		public LockFreeStack(LockFreeItem<T>[] array1, Int32 pushFrom, Int32 pushCount)
		{
			_Array = array1;
			_StackVars.Head = pushFrom;
			for (Int32 i = 0; i < pushCount - 1; i++)
				_Array[i + pushFrom]._Next = pushFrom + i + 1;
			if (pushFrom >= 0)
				_Array[pushFrom + pushCount - 1]._Next = 0xFFFFFFFFL;
		}

		public Int32 Pop()
		{
			unchecked
			{
				var head1 = (UInt64)Interlocked.Read(ref _StackVars.Head);
				for (; ; )
				{
					var index = (Int32)head1;
					if (index < 0)
						return -1;

					var xchg = (UInt64)Thread.VolatileRead(ref _Array[index]._Next) & 0xFFFFFFFFUL | head1 & 0xFFFFFFFF00000000UL;
					var head2 = (UInt64)Interlocked.CompareExchange(ref _StackVars.Head, (Int64)xchg, (Int64)head1);

					if (head1 == head2)
						return index;

					head1 = head2;
				}
			}
		}

		public void Push(Int32 index)
		{
			unchecked
			{
				var head1 = (UInt64)Interlocked.Read(ref _StackVars.Head);
				for (; ; )
				{
					_Array[index]._Next = (Int64)((UInt64)_Array[index]._Next & 0xFFFFFFFF00000000L | head1 & 0xFFFFFFFFL);

					var xchg = (UInt64)(head1 + 0x100000000 & 0xFFFFFFFF00000000 | (UInt32)index);
					var head2 = (UInt64)Interlocked.CompareExchange(ref _StackVars.Head, (Int64)xchg, (Int64)head1);

					if (head1 == head2)
						return;

					head1 = head2;
				}
			}
		}

		public int Length
		{
			get
			{
				int length = 0;
				for (var i = (Int32)_StackVars.Head; i >= 0; i = (int)_Array[i]._Next)
					length++;
				return length;
			}
		}
	}
}
