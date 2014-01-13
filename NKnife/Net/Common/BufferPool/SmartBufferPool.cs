using System;
using System.Threading;

namespace Gean.Net.Common.BufferPool
{
    /// <summary>
    /// Lock-free buffer pool implementation, it is more simple solution than classic memory
    /// manager algorithms (e.g. buddy memory manager). But it should use less extra calculation
    /// for allocation and freeing buffers. It like "smart" buffer pool, it slices desired sizes, 
    /// and do not free slices but put it in pool for further using. So big buffer should be sliced for
    /// concrete application.
    /// </summary>
    public class SmartBufferPool
    {
        public const long Kb = 1024;
        public const long Mb = Kb*Kb;
        public const long Gb = Mb*Kb;

        public const int MinSize = 1*1024;
        public const int MaxSize = 256*1024;

        private readonly LockFreeItem<long>[] _Array;
        private readonly byte[][] _Buffers;
        private readonly LockFreeStack<long> _Empty;
        public readonly long _ExtraMemoryUsage;
        public readonly long _InitialMemoryUsage;
        public readonly long _MaxBuffersCount;
        public readonly long _MaxMemoryUsage;
        private readonly LockFreeStack<long>[] _Ready;
        private long _IndexOffset;

        public SmartBufferPool(int maxMemoryUsageMb, int initialSizeMb, int extraBufferSizeMb)
        {
            _InitialMemoryUsage = initialSizeMb*Mb;
            _ExtraMemoryUsage = extraBufferSizeMb*Mb;
            _MaxBuffersCount = (maxMemoryUsageMb*Mb - _InitialMemoryUsage)/_ExtraMemoryUsage;
            _MaxMemoryUsage = _InitialMemoryUsage + _ExtraMemoryUsage*_MaxBuffersCount;

            _Array = new LockFreeItem<long>[_MaxMemoryUsage/MinSize];

            _Empty = new LockFreeStack<long>(_Array, 0, _Array.Length);

            int count = 0;
            while (MaxSize >> count >= MinSize)
                count++;

            _Ready = new LockFreeStack<long>[count];
            for (int i = 0; i < _Ready.Length; i++)
                _Ready[i] = new LockFreeStack<long>(_Array, -1, -1);

            _Buffers = new byte[_MaxBuffersCount][];
            _Buffers[0] = NewBuffer(_InitialMemoryUsage);
        }

        public ArraySegment<byte> Allocate(int size)
        {
            if (size > MaxSize)
                throw new ArgumentOutOfRangeException(@"Too large size");

            size = MinSize << GetBitOffset(size);

            int offset, index;
            if (GetAllocated(size, out index, out offset) == false)
            {
                long copyIndexOffset;
                do
                {
                    copyIndexOffset = Interlocked.Read(ref _IndexOffset);
                    offset = (int) copyIndexOffset;
                    index = (int) (copyIndexOffset >> 32);

                    while (_Buffers[index] == null) Thread.Sleep(0);

                    if ((_Buffers[index].Length - offset) < size)
                    {
                        if (index + 1 >= _Buffers.Length)
                            throw new OutOfMemoryException("Source: BufferManager");

                        if (Interlocked.CompareExchange(
                            ref _IndexOffset, (long) (index + 1) << 32, copyIndexOffset) == copyIndexOffset)
                        {
                            _Buffers[index + 1] = NewBuffer(_ExtraMemoryUsage);
                        }
                        continue;
                    }
                } while (Interlocked.CompareExchange(
                    ref _IndexOffset, copyIndexOffset + size, copyIndexOffset) != copyIndexOffset);
            }

            return new ArraySegment<byte>(_Buffers[index], offset, size);
        }

        public void Free(ArraySegment<byte> segment)
        {
            int bufferIndex = 0;
            while (_Buffers[bufferIndex] != segment.Array)
                bufferIndex++;

            int index = _Empty.Pop();
            _Array[index]._Value = ((long) bufferIndex << 32) + segment.Offset;
            _Ready[GetBitOffset(segment.Count)].Push(index);
        }

        private bool GetAllocated(int size, out int index, out int offset)
        {
            int itemIndex = _Ready[GetBitOffset(size)].Pop();

            if (itemIndex >= 0)
            {
                index = (int) (_Array[itemIndex]._Value >> 32);
                offset = (int) _Array[itemIndex]._Value;
                _Empty.Push(itemIndex);
                return true;
            }
            else
            {
                index = -1;
                offset = -1;
                return false;
            }
        }

        private int GetBitOffset(int size)
        {
            int count = 0;
            while (size >> count > MinSize)
                count++;
            return count;
        }

        private static byte[] NewBuffer(long size)
        {
            var buffer = new byte[size];
            return buffer;
        }
    }
}