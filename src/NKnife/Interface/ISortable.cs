using System;
using System.Collections.Generic;

namespace NKnife.Interface
{
    public interface ISortable<T>
    {
        /// <summary>
        ///     Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1"></see> using the default
        ///     comparer.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The default comparer
        ///     <see cref="P:System.Collections.Generic.Comparer`1.Default"></see> cannot find an implementation of the
        ///     <see cref="T:System.IComparable`1"></see> generic interface or the <see cref="T:System.IComparable"></see>
        ///     interface for type <paramref name="T">T</paramref>.
        /// </exception>
        void Sort();

        /// <summary>
        ///     Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1"></see> using the specified
        ///     comparer.
        /// </summary>
        /// <param name="comparer">
        ///     The <see cref="T:System.Collections.Generic.IComparer`1"></see> implementation to use when
        ///     comparing elements, or null to use the default comparer
        ///     <see cref="P:System.Collections.Generic.Comparer`1.Default"></see>.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="comparer">comparer</paramref> is null, and the
        ///     default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"></see> cannot find implementation of
        ///     the <see cref="T:System.IComparable`1"></see> generic interface or the <see cref="T:System.IComparable"></see>
        ///     interface for type <paramref name="T">T</paramref>.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The implementation of <paramref name="comparer">comparer</paramref> caused
        ///     an error during the sort. For example, <paramref name="comparer">comparer</paramref> might not return 0 when
        ///     comparing an item with itself.
        /// </exception>
        void Sort(IComparer<T> comparer);

        /// <summary>
        ///     Sorts the elements in the entire <see cref="T:System.Collections.Generic.List`1"></see> using the specified
        ///     <see cref="T:System.Comparison`1"></see>.
        /// </summary>
        /// <param name="comparison">The <see cref="T:System.Comparison`1"></see> to use when comparing elements.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="comparison">comparison</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The implementation of <paramref name="comparison">comparison</paramref>
        ///     caused an error during the sort. For example, <paramref name="comparison">comparison</paramref> might not return 0
        ///     when comparing an item with itself.
        /// </exception>
        void Sort(Comparison<T> comparison);

        /// <summary>
        ///     Sorts the elements in a range of elements in <see cref="T:System.Collections.Generic.List`1"></see> using the
        ///     specified comparer.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range to sort.</param>
        /// <param name="count">The length of the range to sort.</param>
        /// <param name="comparer">
        ///     The <see cref="T:System.Collections.Generic.IComparer`1"></see> implementation to use when
        ///     comparing elements, or null to use the default comparer
        ///     <see cref="P:System.Collections.Generic.Comparer`1.Default"></see>.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index">index</paramref> is less than 0.   -or-
        ///     <paramref name="count">count</paramref> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="index">index</paramref> and
        ///     <paramref name="count">count</paramref> do not specify a valid range in the
        ///     <see cref="T:System.Collections.Generic.List`1"></see>.   -or-   The implementation of
        ///     <paramref name="comparer">comparer</paramref> caused an error during the sort. For example,
        ///     <paramref name="comparer">comparer</paramref> might not return 0 when comparing an item with itself.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="comparer">comparer</paramref> is null, and the
        ///     default comparer <see cref="P:System.Collections.Generic.Comparer`1.Default"></see> cannot find implementation of
        ///     the <see cref="T:System.IComparable`1"></see> generic interface or the <see cref="T:System.IComparable"></see>
        ///     interface for type <paramref name="T">T</paramref>.
        /// </exception>
        void Sort(int index, int count, IComparer<T> comparer);
    }
}