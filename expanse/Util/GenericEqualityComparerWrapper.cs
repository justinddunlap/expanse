using System.Collections;
using System.Collections.Generic;

namespace Expanse.Util
{
    public class GenericEqualityComparerWrapper<T> : IEqualityComparer<T>
    {
        IEqualityComparer comparer;
        public GenericEqualityComparerWrapper(IEqualityComparer comparer)
        {
            this.comparer = comparer;
        }

        public bool Equals(T x, T y)
        {
            return comparer.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(obj);
        }
    }
}
