using System.Collections;

namespace Expanse.Util
{
    public class NullHandlingEqualityComparer : IEqualityComparer
    {
        IEqualityComparer inner;

        public NullHandlingEqualityComparer(IEqualityComparer inner)
        {
            this.inner = inner;
        }

        public bool Equals(object x, object y)
        {
            if (object.ReferenceEquals(x, null)) return object.ReferenceEquals(y, null);
            return inner.Equals(x, y);
        }

        public int GetHashCode(object obj)
        {
            if (object.ReferenceEquals(obj, null)) return 0;
            return inner.GetHashCode(obj);
        }
    }
}
