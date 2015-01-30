using Expanse.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class ComparerExtensions
    {
        /// <summary>
        /// Given a non-generic IEqualityComparer instance, returns an IEqualityComparer&lt;T&gt; wrapper
        /// for it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEqualityComparer<T> ToGenericComparer<T>(this IEqualityComparer comparer)
        {
            return new GenericEqualityComparerWrapper<T>(comparer);
        }

        /// <summary>
        /// Given an IEqualityComparer instance, returns a wrapper IEqualityComparer instance
        /// that is guaranteed not to throw an exception when null values are passed to it.
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEqualityComparer AllowNulls(this IEqualityComparer comparer)
        {
            return new NullHandlingEqualityComparer(comparer);
        }
    }
}
