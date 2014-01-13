using System;

namespace Gean.Net.Common.BufferPool
{
	public class BufferManager
	{
		private static SmartBufferPool _Pool;

		public static void Initialize(int maxMemoryUsageMb, int initialSizeMb, int extraBufferSizeMb)
		{
			_Pool = new SmartBufferPool(maxMemoryUsageMb, initialSizeMb, extraBufferSizeMb);
		}

		public static void Initialize(int maxMemoryUsageMb)
		{
			_Pool = new SmartBufferPool(maxMemoryUsageMb, maxMemoryUsageMb / 8, maxMemoryUsageMb / 16);
		}

		public static bool IsInitialized()
		{
			return _Pool != null;
		}

		public static ArraySegment<byte> Allocate(int size)
		{
			return _Pool.Allocate(size);
		}

		public static void Free(ref ArraySegment<byte> segment)
		{
			if (segment.IsValid())
			{
				_Pool.Free(segment);
				segment = new ArraySegment<byte>();
			}
		}

		internal static void Free(ArraySegment<byte> segment)
		{
			if (segment.IsValid())
				_Pool.Free(segment);
		}

		public static long MaxMemoryUsage
		{
			get { return _Pool._MaxMemoryUsage; }
		}

		public static int MaxSize
		{
			get { return SmartBufferPool.MaxSize; }
		}
	}
}