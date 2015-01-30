using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Util
{
    internal class WrappedAdjacentGrouping<T> : IAdjacentGrouping<T>
    {
        public IEnumerable<T> Wrapped { get; set; }
        public IEnumerator<T> GetEnumerator()
        {
            return Wrapped.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }

    public interface IAdjacentGrouping<T> : IEnumerable<T>
    {

    } 
}
