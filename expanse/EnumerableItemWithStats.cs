using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse
{
    public class EnumerableItemWithStats<T>
    {
        public bool IsFirst { get; internal set; }
        public bool IsLast { get; internal set; }
        public bool Value { get; internal set; }
        public T Item { get; internal set; }
    }

}
