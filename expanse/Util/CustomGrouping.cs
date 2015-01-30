using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Expanse.Util
{
    public class CustomGrouping<K, T> : IGrouping<K, T>
    {
        public K Key { get; set; }
        public IEnumerable<T> Elements;
        public IEnumerator<T> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}
