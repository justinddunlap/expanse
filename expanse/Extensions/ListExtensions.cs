using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns the index of first item in the given <paramref name="list"/> that 
        /// satisfies the specified <paramref name="match"/> condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int FirstIndex<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = 0; i < list.Count; i++)
                if (match(list[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// Returns a sequence that contains all of the items in the specified 
        /// <paramref name="list"/> in reverse order. This method is more efficient than 
        /// Enumerable.Reverse when working with lists because it uses list indexes to generate the 
        /// reversed sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> Reverse<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
                yield return list[i];
        }


        /// <summary>
        /// Searches a sorted list for an item for which <paramref name="compare"/> returns 0.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list</typeparam>
        /// <param name="list">The list of items to search</param>
        /// <param name="compare">A delegate that returns a numeric comparison result given a single 
        /// item from the list.</param>
        /// <returns>The index of the matching item in the list, if any, or the bitwise complement 
        /// of the index the item would be inserted at if it was present in the list.</returns>
        public static int BinarySearch<T>(this IList<T> list, Func<T, int> compare)
        {
            int left = 0;
            int right = (0 + list.Count) - 1;
            while (left <= right)
            {
                int idx = left + ((right - left) >> 1);
                int result = compare(list[idx]);
                if (result == 0)
                    return idx;
                if (result < 0)
                    left = idx + 1;
                else
                    right = idx - 1;
            }
            return ~left;
        }

        public static int BinarySearch<T>(this IList<T> list, T searchValue, Comparison<T> compare)
        {
            int left = 0;
            int right = (0 + list.Count) - 1;
            while (left <= right)
            {
                int idx = left + ((right - left) >> 1);
                int result = compare(list[idx], searchValue);
                if (result == 0)
                    return idx;
                if (result < 0)
                    left = idx + 1;
                else
                    right = idx - 1;
            }
            return ~left;
        }

        public static int BinarySearch<TItem, TCompare>(
            this IList<TItem> list, TItem searchValue, Converter<TItem, TCompare> convert, 
            Comparison<TCompare> compare
        )
        {
            var convSearchValue = convert(searchValue);
            int left = 0;
            int right = (0 + list.Count) - 1;
            while (left <= right)
            {
                int idx = left + ((right - left) >> 1);
                int result = compare(convert(list[idx]), convSearchValue);
                if (result == 0)
                    return idx;
                if (result < 0)
                    left = idx + 1;
                else
                    right = idx - 1;
            }
            return ~left;
        }

        public static int BinarySearch<TItem, TMatch>(
            this IList<TItem> list, TMatch match, Converter<TItem, TMatch> convert
        ) where TMatch : IComparable<TMatch>
        {
            int left = 0;
            int right = (0 + list.Count) - 1;
            while (left <= right)
            {
                int idx = left + ((right - left) >> 1);
                int result = convert(list[idx]).CompareTo(match);
                if (result == 0)
                    return idx;
                if (result < 0)
                    left = idx + 1;
                else
                    right = idx - 1;
            }
            return ~left;
        }


        public static int BinarySearch<TItem, TMatch>(
            this IList<TItem> list, TMatch match, Converter<TItem, TMatch> convert, 
            IComparer<TMatch> comparer
        )
        {
            int left = 0;
            int right = (0 + list.Count) - 1;
            while (left <= right)
            {
                int idx = left + ((right - left) >> 1);
                int result = comparer.Compare(convert(list[idx]), match);
                if (result == 0)
                    return idx;
                if (result < 0)
                    left = idx + 1;
                else
                    right = idx - 1;
            }
            return ~left;
        }

        public static int BinarySearch<TItem, TMatch>(
            this IList<TItem> list, TMatch match, Converter<TItem, TMatch> convert, 
            IComparer comparer
        )
        {
            int left = 0;
            int right = (0 + list.Count) - 1;
            while (left <= right)
            {
                int idx = left + ((right - left) >> 1);
                int result = comparer.Compare(convert(list[idx]), match);
                if (result == 0)
                    return idx;
                if (result < 0)
                    left = idx + 1;
                else
                    right = idx - 1;
            }
            return ~left;
        }

        public static TItem SortedFind<TItem, TMatch>(
            this IList<TItem> list, TMatch match, Converter<TItem, TMatch> convert
        ) where TMatch : IComparable<TMatch>
        {
            var idx = BinarySearch<TItem, TMatch>(list, match, convert);
            if (idx < 0)
                return default(TItem);
            else
                return list[idx];
        }

        public static TItem SortedFind<TItem, TMatch>(
            this IList<TItem> list, TMatch match, Converter<TItem, TMatch> convert, 
            IComparer<TMatch> comparer
        )
        {
            var idx = BinarySearch<TItem, TMatch>(list, match, convert, comparer);
            if (idx < 0)
                return default(TItem);
            else
                return list[idx];
        }

        public static TItem SortedFind<TItem, TMatch>(
            this IList<TItem> list, TMatch match, Converter<TItem, TMatch> convert, 
            IComparer comparer
        )
        {
            var idx = BinarySearch<TItem, TMatch>(list, match, convert, comparer);
            if (idx < 0)
                return default(TItem);
            else
                return list[idx];
        }

        public static T SortedFind<T>(this IList<T> list, Func<T, int> compare)
        {
            var idx = BinarySearch(list, compare);
            if (idx < 0)
                return default(T);
            else
                return list[idx];
        }

        public static int SortedInsert<T>(this IList<T> list, T item, Comparison<T> compare)
        {
            int insertIdx = list.BinarySearch(item, compare);
            if (insertIdx < 0)
                insertIdx = ~insertIdx;
            list.Insert(insertIdx, item);
            return insertIdx;
        }

        public static int SortedInsert<TItem, TCompare>(
            this IList<TItem> list, TItem item, Converter<TItem, TCompare> comparisonValueSelector
        ) where TCompare : IComparable<TCompare>
        {
            int insertIdx = list.BinarySearch(comparisonValueSelector(item), comparisonValueSelector);
            if (insertIdx < 0)
                insertIdx = ~insertIdx;
            list.Insert(insertIdx, item);
            return insertIdx;
        }

        public static int SortedInsert<T>(this IList<T> list, T item) where T : IComparable<T>
        {
            int insertIdx = list.BinarySearch(item, (i1, i2) => i1.CompareTo(i2));
            if (insertIdx < 0)
                insertIdx = ~insertIdx;
            list.Insert(insertIdx, item);
            return insertIdx;
        }

        public static int SortedInsert<TItem, TMatch>(
            this IList<TItem> list, TItem item, TMatch match, Converter<TItem, TMatch> converter
        ) where TMatch : IComparable<TMatch>
        {
            int insertIdx = list.BinarySearch(match, converter);
            if (insertIdx < 0)
                insertIdx = ~insertIdx;
            list.Insert(insertIdx, item);
            return insertIdx;
        }

        /*public static bool SortedContainsAny<T>(this IList<T> list, params T[] entries)
        {

        }

        public static bool SortedContainsAll<T>(this IList<T> list, params T[] entries)
        {

        }*/

        public static int FastIndexOf<T>(this IList<T> list, T item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (object.Equals(list[i], null))
                    return i;
            }

            return -1;
        }


        public static int FastIndexOf<T>(this IList<T> list, T item, EqualityComparison<T> compare)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (compare(list[i], item))
                    return i;
            }

            return -1;
        }

        public static bool FastContains<T>(this IList<T> list, T item)
        {
            return list.FastIndexOf(item) != -1;
        }

        public static bool Contains<T>(this IEnumerable<T> seq, T item, EqualityComparison<T> compare)
        {
            foreach(var element in seq)
            {
                if (compare(element, item))
                    return true;
            }
            return false;
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }

        public static void AddRange<T>(this IList<T> list, params T[] range)
        {
            foreach (T item in range)
                list.Add(item);
        }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> range)
        {
            foreach (T item in range)
                list.Add(item);
        }

        public static void AddRanges<T>(this IList<T> list, params IEnumerable<T>[] ranges)
        {
            for (int i = 0; i < ranges.Length; i++)
                list.AddRange(ranges[i]);
        }

        [ThreadStatic]
        static Random rand;

        public static T Random<T>(this IList<T> list)
        {
            if (rand == null)
                rand = new Random();
            return list[rand.Next(0, list.Count)];
        }


        public static bool AddUnique<T>(this ICollection<T> col, T item, IEqualityComparer<T> comparer)
        {

            if (!col.Contains(item, comparer))
            {
                col.Add(item);
                return true;
            }
            return false;
        }

        public static bool AddUnique<T>(this ICollection<T> col, T item, EqualityComparison<T> compare)
        {
            
            if (!col.Contains(item, compare))
            {
                col.Add(item);
                return true;
            }
            return false;
        }

        public static bool AddUnique<T>(this IList<T> list, T item, EqualityComparison<T> compare)
        {
            if (list.FastIndexOf(item, compare) < 0)
            {
                list.Add(item);
                return true;
            }
            return false;
        }

        public static bool AddUnique<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
                return true;
            }
            return false;
        }

        public static bool AddUniqueSorted<T>(this IList<T> list, T item, Func<T, int> compare)
        {
            int idx = list.BinarySearch(compare);
            if (idx < 0)
            {
                idx = ~idx;
                list.Insert(idx, item);
                return true;
            }
            return false;
        }

        public static bool AddUniqueSorted<T>(this IList<T> list, T item, Comparison<T> compare)
        {
            int idx = list.BinarySearch(item, compare);
            if (idx < 0)
            {
                idx = ~idx;
                list.Insert(idx, item);
                return true;
            }
            return false;
        }

        public static IEnumerable<T> Range<T>(this IList<T> list, int start, int count)
        {
            for (int i = start; i < count; i++)
            {
                yield return list[i];
            }
        }

        /// <summary>
        /// Bypasses a specified number of elements in a list and then returns the remaining
        /// elements in an <see cref="System.Collections.Generic.IEnumerable<T>"/> sequence.
        /// Faster than Enumerable.Skip when working with lists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="skipCount"></param>
        /// <returns></returns>
        public static IEnumerable<T> Skip<T>(this IList<T> list, int skipCount)
        {
            for (int i = skipCount; i < list.Count; i++)
            {
                yield return list[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this IList<T> list)
        {
            return list.Count == 0 ? default(T) : list[list.Count - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this IList<T> list)
        {
            return list[list.Count - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this IList<T> list, Predicate<T> predicate, T defaultVal = default(T))
        {
            return Last(list, predicate, false, defaultVal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this IList<T> list, Predicate<T> predicate)
        {
            return Last(list, predicate, true);
        }

        private static T Last<T>(IList<T> list, Predicate<T> predicate, bool throwIfNotFound = true, T defaultVal = default(T))
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var it = list[i];
                if (predicate(it))
                    return it;
            }

            if (throwIfNotFound)
                throw new InvalidOperationException();
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq"></param>
        /// <param name="separator"></param>
        /// <param name="ignoreDefaultElements"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IList<T> seq, T separator, bool ignoreDefaultElements = false, int start = 0, int end = -1)
        {
            if (end == -1) end = seq.Count;
            int startIdx = start;
            var eq = EqualityComparer<T>.Default.AllowNulls();
            bool isDefault = eq.Equals(separator, default(T));
            for (var i = start; i < end; i++)
            {
                if ((isDefault && eq.Equals(seq[i], default(T))) || separator.Equals(seq[i]))
                {
                    if (!ignoreDefaultElements || i != startIdx)
                        yield return seq.Range(startIdx, i - startIdx);
                    startIdx = i + 1;
                }
            }

            if (startIdx < seq.Count - 1)
                yield return seq.Range(startIdx, seq.Count - startIdx);
        }

        //TODO: IndexOf, LastIndexOf
    }

    public delegate bool EqualityComparison<T>(T first, T second);
}
