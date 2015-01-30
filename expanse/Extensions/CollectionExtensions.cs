using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class CollectionExtensions
    {
        public static bool Any<T>(this ICollection<T> col)
        {
            return col.Count > 0;
        }

        /*public static bool ContainsAny<T>(this ICollection<T> col, params T[] entries)
        {

        }

        public static bool ContainsAll<T>(this ICollection<T> col, params T[] entries)
        {

        }*/

        public static bool IsNullOrEmpty<T>(this ICollection<T> col)
        {
            return col == null || col.Count == 0;
        }
    }
}
