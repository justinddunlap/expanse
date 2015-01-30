using System;
using System.Collections.Generic;

namespace Expanse.Util
{
    /// <summary>
    /// An equality comparer that applies a function to each item to be compared
    /// and compares the results to determine whether the items are equal.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TCompare"></typeparam>
    public class FunctionalEqualityComparer<TItem, TCompare> : IEqualityComparer<TItem>
    {
        Func<TItem, TCompare> func;
        IEqualityComparer<TCompare> keyComparer;
        public FunctionalEqualityComparer(Func<TItem, TCompare> func, IEqualityComparer<TCompare> keyComparer = null)
        {
            this.func = func;
            this.keyComparer = keyComparer ?? EqualityComparer<TCompare>.Default;
        }


        #region IEqualityComparer<TSource> Members

        public bool Equals(TItem x, TItem y)
        {
            return keyComparer.Equals(func(x), func(y));
        }

        public int GetHashCode(TItem obj)
        {
            return keyComparer.GetHashCode(func(obj));
        }

        #endregion
    }
}
