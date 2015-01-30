using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Util
{
    public class WindowItem<T>
    {
        T[] allItems;

        public WindowItem(int idx, int beforeCount, int afterCount, T[] items)
        {
            allItems = items;
            Index = idx;
            Current = items[beforeCount];
            BeforeCount = beforeCount;
            AfterCount = afterCount;
            Prev = beforeCount == 0 ? default(T) : items[BeforeCount - 1];
            Next = afterCount == 0 ? default(T) : items[BeforeCount + 1];
        }

        protected static IEnumerable<T> GetRange(T[] items, int startIdx, int length)
        {
            for (int i = startIdx; i < startIdx + length; i++)
                yield return items[i];
        }

        /// <summary>
        /// Gets the number of elements inside this window that occurred before the current element.
        /// </summary>
        public int BeforeCount { get; protected set; }
        /// <summary>
        /// Gets the number of elements inside this window that occur after the current element.
        /// </summary>
        public int AfterCount { get; protected set; }
        /// <summary>
        /// Gets the index of the current element.
        /// </summary>
        public int Index { get; protected set; }
        /// <summary>
        /// Gets the current element (the element this window instance is for).
        /// </summary>
        public T Current { get; protected set; }
        /// <summary>
        /// Gets the element before the current element.
        /// </summary>
        public T Prev { get; protected set; }
        /// <summary>
        /// Gets the element after the current element.
        /// </summary>
        public T Next { get; protected set; }
        /// <summary>
        /// Gets the elements in the window that fall before the current element.
        /// </summary>
        public IEnumerable<T> Before { get { return GetRange(allItems, 0, BeforeCount); } }
        /// <summary>
        /// Gets the elements in the window that fall after the current element.
        /// </summary>
        public IEnumerable<T> After { get { return GetRange(allItems, BeforeCount + 1, AfterCount); } }
        /// <summary>
        /// Gets all of the elements in the window, including the elements before the current element,
        /// the current element, and the elements after the current element.
        /// </summary>
        public IEnumerable<T> All { get { return allItems; } }
    }
}
