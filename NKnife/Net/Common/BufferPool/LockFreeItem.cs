using System;

namespace Gean.Net.Common.BufferPool
{
	struct LockFreeItem<T>
	{
		public Int64 _Next;
		public T _Value;

		public new string ToString()
		{
			return string.Format("Next: {0}, Count: {1}, Value: {2}", (Int32)_Next, (UInt32)(_Next >> 32), (_Value == null) ? @"null" : "full");
		}
	}
}
