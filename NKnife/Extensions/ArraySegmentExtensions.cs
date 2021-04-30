using System.IO;
using Enumerable = System.Linq.Enumerable;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class ArraySegmentExtensions
    {
        public static T At<T>(this ArraySegment<T> src, int index)
        {
            if (src.Array == null || index < 0)
                throw new ArgumentNullException();
            return src.Array[src.Offset + index];
        }

        public static int IndexOf<T>(this ArraySegment<T> src, T b)
        {
            if (src.Array == null)
                throw new ArgumentNullException();
            if (!Enumerable.Contains(src, b)) return -1;
            for (var i = 0; i < src.Count; i++)
                if (src.Array[src.Offset + i].Equals(b))
                    return i;
            return -1;
        }

        public static int IndexOf<T>(this ArraySegment<T> src, T[] b)
        {
            if (src.Array == null || b == null || b.Length <= 0)
                throw new ArgumentNullException();
            for (var i = 0; i < src.Count - b.Length; i++)
            {
                var has = true;
                for (var j = 0; j < b.Length; j++)
                {
                    var srcIndex = src.Offset + i;
                    if (!src.Array[srcIndex + j].Equals(b[j]))
                    {
                        has = false;
                        break;
                    }
                }

                if (has)
                    return i;
            }

            return -1;
        }

        public static int LastIndexOf<T>(this ArraySegment<T> src, T b)
        {
            if (src.Array == null)
                throw new ArgumentNullException();
            if (!Enumerable.Contains(src, b)) return -1;
            for (var i = src.Count - 1; i >= 0; i--)
                if (src.Array[src.Offset + i].Equals(b))
                    return i;
            return -1;
        }

        /// <summary>
        ///     Creates an array segment referencing this array.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="offset">
        ///     The offset in this array where the segment begins. Must be in the range
        ///     <c>[0, <paramref name="array" />.Length]</c>.
        /// </param>
        /// <param name="count">
        ///     The length of the segment. Must be in the range
        ///     <c>[0, <paramref name="array" />.Length - <paramref name="offset" />]</c>.
        /// </param>
        /// <returns>A new array segment.</returns>
        public static ArraySegment<T> AsArraySegment<T>(this T[] array, int offset, int count)
        {
            return new ArraySegment<T>(array, offset, count);
        }

        /// <summary>
        ///     Creates an array segment referencing this array.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="offset">
        ///     The offset in this array where the segment begins. Defaults to <c>0</c> (the beginning of the
        ///     array). Must be in the range <c>[0, <paramref name="array" />.Length]</c>.
        /// </param>
        /// <returns>A new array segment.</returns>
        public static ArraySegment<T> AsArraySegment<T>(this T[] array, int offset = 0)
        {
            return new ArraySegment<T>(array, offset, array.Length - offset);
        }

        /// <summary>
        ///     Creates an <see cref="ArraySegmentReader{T}" /> over this array segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <returns>A new <see cref="ArraySegmentReader{T}" />.</returns>
        public static ArraySegmentReader<T> CreateArraySegmentReader<T>(this ArraySegment<T> segment)
        {
            return new ArraySegmentReader<T>(segment);
        }

        /// <summary>
        ///     Creates a new array segment by taking a number of elements from the beginning of this array segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <param name="count">
        ///     The number of elements in the new array segment. This must be in the range
        ///     <c>[0, <paramref name="segment" />.Count]</c>.
        /// </param>
        /// <returns>The new array segment.</returns>
        public static ArraySegment<T> Take<T>(this ArraySegment<T> segment, int count)
        {
            return new ArraySegment<T>(segment.Array, segment.Offset, count);
        }

        /// <summary>
        ///     Creates a new array segment by skipping a number of elements from the beginning of this array segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <param name="count">
        ///     The number of elements to skip. This must be in the range
        ///     <c>[0, <paramref name="segment" />.Count]</c>.
        /// </param>
        /// <returns>The new array segment.</returns>
        public static ArraySegment<T> Skip<T>(this ArraySegment<T> segment, int count)
        {
            return new ArraySegment<T>(segment.Array, segment.Offset + count, segment.Count - count);
        }

        /// <summary>
        ///     Creates a new array segment by skipping a number of elements and then taking a number of elements from this array
        ///     segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <param name="skipCount">
        ///     The number of elements to skip. This must be in the range
        ///     <c>[0, <paramref name="segment" />.Count]</c>.
        /// </param>
        /// <param name="takeCount">
        ///     The number of elements in the new array segment. This must be in the range
        ///     <c>[0, <paramref name="segment" />.Count - <paramref name="skipCount" />]</c>.
        /// </param>
        /// <returns>The new array segment.</returns>
        public static ArraySegment<T> Slice<T>(this ArraySegment<T> segment, int skipCount, int takeCount)
        {
            return new ArraySegment<T>(segment.Array, segment.Offset + skipCount, takeCount);
        }

        /// <summary>
        ///     Creates a new array segment by skipping a number of elements and then taking a number of elements from this array
        ///     segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <param name="skipCount">
        ///     The number of elements to skip. This must be in the range
        ///     <c>[0, <paramref name="segment" />.Count]</c>.
        /// </param>
        /// <returns>The new array segment.</returns>
        public static ArraySegment<T> Slice<T>(this ArraySegment<T> segment, int skipCount)
        {
            return new ArraySegment<T>(segment.Array, segment.Offset + skipCount, segment.Count - skipCount);
        }

        /// <summary>
        ///     Creates a new array segment by taking a number of elements from the end of this array segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <param name="count">
        ///     The number of elements in the new array segment. This must be in the range
        ///     <c>[0, <paramref name="segment" />.Count]</c>.
        /// </param>
        /// <returns>The new array segment.</returns>
        public static ArraySegment<T> TakeLast<T>(this ArraySegment<T> segment, int count)
        {
            return segment.Skip(segment.Count - count);
        }

        /// <summary>
        ///     Creates a new array segment by skipping a number of elements from the end of this array segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <param name="count">
        ///     The number of elements to skip. This must be in the range
        ///     <c>[0, <paramref name="segment" />.Count]</c>.
        /// </param>
        /// <returns>The new array segment.</returns>
        public static ArraySegment<T> SkipLast<T>(this ArraySegment<T> segment, int count)
        {
            return segment.Take(segment.Count - count);
        }

        /// <summary>
        ///     Copies the elements in this array segment into a destination array segment. The copy operation will not overflow
        ///     the bounds of the segments; it will copy <c>min(segment.Count, destination.Count)</c> elements.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The source array segment.</param>
        /// <param name="destination">The detsintation array segment.</param>
        public static void CopyTo<T>(this ArraySegment<T> segment, ArraySegment<T> destination)
        {
            Array.Copy(segment.Array, segment.Offset, destination.Array, destination.Offset, segment.Count < destination.Count ? segment.Count : destination.Count);
        }

        /// <summary>
        ///     Copies the elements in this array segment into a destination array.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <param name="array">The destination array. May not be <c>null</c>.</param>
        /// <param name="arrayIndex">
        ///     The index in the destination array at which to begin copying. Defaults to <c>0</c>. Must be
        ///     greater than or equal to <c>0</c>.
        /// </param>
        public static void CopyTo<T>(this ArraySegment<T> segment, T[] array, int arrayIndex = 0)
        {
            Array.Copy(segment.Array, segment.Offset, array, arrayIndex, segment.Count);
        }

        /// <summary>
        ///     Creates a new array containing the elements in this array segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the array.</typeparam>
        /// <param name="segment">The array segment.</param>
        /// <returns>The new array.</returns>
        public static T[] ToArray<T>(this ArraySegment<T> segment)
        {
            var ret = new T[segment.Count];
            segment.CopyTo(ret);
            return ret;
        }

        //-------------------------

        /// <summary>
        ///     Creates a <see cref="MemoryStream" /> over this array segment. Multiple streams may be created for the same array
        ///     and array segment, but if one of them writes then any buffering will cause inconsistent views.
        /// </summary>
        /// <param name="segment">The array segment.</param>
        /// <param name="writable">A value indicating whether the stream is writable. Defautls to <c>true</c>.</param>
        /// <returns>A new <see cref="MemoryStream" />.</returns>
        public static MemoryStream CreateStream(this ArraySegment<byte> segment, bool writable = true)
        {
            return new MemoryStream(segment.Array, segment.Offset, segment.Count, writable);
        }

        /// <summary>
        ///     Creates a <see cref="BinaryReader" /> over this array segment.
        /// </summary>
        /// <param name="segment">The array segment.</param>
        /// <returns>A new <see cref="BinaryReader" />.</returns>
        public static BinaryReader CreateBinaryReader(this ArraySegment<byte> segment)
        {
            return new BinaryReader(segment.CreateStream(false));
        }

        /// <summary>
        ///     Creates a <see cref="BinaryWriter" /> over this array segment.
        /// </summary>
        /// <param name="segment">The array segment.</param>
        /// <returns>A new <see cref="BinaryWriter" />.</returns>
        public static BinaryWriter CreateBinaryWriter(this ArraySegment<byte> segment)
        {
            return new BinaryWriter(segment.CreateStream());
        }
    }
}