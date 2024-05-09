using System.Numerics;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class VectorizedCopyExtension
    {
        private const int LONG_SPAN = sizeof(long);
        private const int LONG_SPAN2 = sizeof(long) + sizeof(long);
        private const int LONG_SPAN3 = sizeof(long) + sizeof(long) + sizeof(long);

        private const int INT_SPAN = sizeof(int);

        // Will be Jit'd to consts https://github.com/dotnet/coreclr/issues/1079
        private static readonly int _VectorSpan = Vector<byte>.Count;
        private static readonly int _VectorSpan2 = Vector<byte>.Count + Vector<byte>.Count;
        private static readonly int _VectorSpan3 = Vector<byte>.Count + Vector<byte>.Count + Vector<byte>.Count;
        private static readonly int _VectorSpan4 = Vector<byte>.Count + Vector<byte>.Count + Vector<byte>.Count + Vector<byte>.Count;

        /// <summary>
        ///     Copies a specified number of bytes from a source array starting at a particular
        ///     offset to a destination array starting at a particular offset, not safe for overlapping data.
        /// </summary>
        /// <param name="src">The source buffer</param>
        /// <param name="srcOffset">The zero-based byte offset into src</param>
        /// <param name="dst">The destination buffer</param>
        /// <param name="dstOffset">The zero-based byte offset into dst</param>
        /// <param name="count">The number of bytes to copy</param>
        /// <exception cref="ArgumentNullException"><paramref name="src" /> or <paramref name="dst" /> is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="srcOffset" />, <paramref name="dstOffset" />, or
        ///     <paramref name="count" /> is less than 0
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The number of bytes in src is less
        ///     than srcOffset plus count.-or- The number of bytes in dst is less than dstOffset
        ///     plus count.
        /// </exception>
        /// <remarks>
        ///     Code must be optimized, in release mode and <see cref="Vector" />.IsHardwareAccelerated must be true for the
        ///     performance benefits.
        /// </remarks>
        public static unsafe void VectorizedCopy(this byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
        {
#if !DEBUG
            // Tests need to check even if IsHardwareAccelerated == false
            // Check will be Jitted away https://github.com/dotnet/coreclr/issues/1079
            if (Vector.IsHardwareAccelerated)
            {
#endif
            if (count > 512 + 64)
            {
                // In-built copy faster for large arrays (vs repeated bounds checks on Vector.ctor?)
                Array.Copy(src, srcOffset, dst, dstOffset, count);
                return;
            }

            if (src == null) throw new ArgumentNullException(nameof(src));
            if (dst == null) throw new ArgumentNullException(nameof(dst));
            if (count < 0 || srcOffset < 0 || dstOffset < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (count == 0) return;
            if (srcOffset + count > src.Length) throw new ArgumentException(nameof(src));
            if (dstOffset + count > dst.Length) throw new ArgumentException(nameof(dst));

            while (count >= _VectorSpan4)
            {
                new Vector<byte>(src, srcOffset).CopyTo(dst, dstOffset);
                new Vector<byte>(src, srcOffset + _VectorSpan).CopyTo(dst, dstOffset + _VectorSpan);
                new Vector<byte>(src, srcOffset + _VectorSpan2).CopyTo(dst, dstOffset + _VectorSpan2);
                new Vector<byte>(src, srcOffset + _VectorSpan3).CopyTo(dst, dstOffset + _VectorSpan3);
                if (count == _VectorSpan4) return;
                count -= _VectorSpan4;
                srcOffset += _VectorSpan4;
                dstOffset += _VectorSpan4;
            }

            if (count >= _VectorSpan2)
            {
                new Vector<byte>(src, srcOffset).CopyTo(dst, dstOffset);
                new Vector<byte>(src, srcOffset + _VectorSpan).CopyTo(dst, dstOffset + _VectorSpan);
                if (count == _VectorSpan2) return;
                count -= _VectorSpan2;
                srcOffset += _VectorSpan2;
                dstOffset += _VectorSpan2;
            }

            if (count >= _VectorSpan)
            {
                new Vector<byte>(src, srcOffset).CopyTo(dst, dstOffset);
                if (count == _VectorSpan) return;
                count -= _VectorSpan;
                srcOffset += _VectorSpan;
                dstOffset += _VectorSpan;
            }

            if (count > 0)
                fixed (byte* srcOrigin = src)
                fixed (byte* dstOrigin = dst)
                {
                    var pSrc = srcOrigin + srcOffset;
                    var dSrc = dstOrigin + dstOffset;

                    if (count >= LONG_SPAN)
                    {
                        var lpSrc = (long*) pSrc;
                        var ldSrc = (long*) dSrc;

                        if (count < LONG_SPAN2)
                        {
                            count -= LONG_SPAN;
                            pSrc += LONG_SPAN;
                            dSrc += LONG_SPAN;
                            *ldSrc = *lpSrc;
                        }
                        else if (count < LONG_SPAN3)
                        {
                            count -= LONG_SPAN2;
                            pSrc += LONG_SPAN2;
                            dSrc += LONG_SPAN2;
                            *ldSrc = *lpSrc;
                            *(ldSrc + 1) = *(lpSrc + 1);
                        }
                        else
                        {
                            count -= LONG_SPAN3;
                            pSrc += LONG_SPAN3;
                            dSrc += LONG_SPAN3;
                            *ldSrc = *lpSrc;
                            *(ldSrc + 1) = *(lpSrc + 1);
                            *(ldSrc + 2) = *(lpSrc + 2);
                        }
                    }

                    if (count >= INT_SPAN)
                    {
                        var ipSrc = (int*) pSrc;
                        var idSrc = (int*) dSrc;
                        count -= INT_SPAN;
                        pSrc += INT_SPAN;
                        dSrc += INT_SPAN;
                        *idSrc = *ipSrc;
                    }

                    while (count > 0)
                    {
                        count--;
                        *dSrc = *pSrc;
                        dSrc += 1;
                        pSrc += 1;
                    }
                }
#if !DEBUG
            }
            else
            {
                Array.Copy(src, srcOffset, dst, dstOffset, count);
                return;
            }
#endif
        }
    }
}