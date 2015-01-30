using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Sets the values for a range of keys.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="seq"></param>
        public static void SetRange<K, V>(
            this Dictionary<K, V> dict, IEnumerable<KeyValuePair<K, V>> seq)
        {
            foreach (var item in seq)
                dict[item.Key] = item.Value;
        }

        public static void AddRange<K, V>(
            this Dictionary<K, V> dict, IEnumerable<KeyValuePair<K, V>> seq)
        {
            foreach (var item in seq)
                dict.Add(item.Key, item.Value);
        }

        public static void AddRange<K, V>(
            this Dictionary<K, V> dict, IEnumerable<KeyValuePair<K, V>> seq, 
            Func<KeyValuePair<K, V>, bool> overwritePredicate
        )
        {
            foreach (var item in seq)
            {
                if (!dict.ContainsKey(item.Key))
                {
                    dict.Add(item.Key, item.Value);
                }
                else
                {
                    if (overwritePredicate(item))
                        dict[item.Key] = item.Value;
                }
            }
        }

        /// <summary>
        /// Converts
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<string, string> ToDictionary(this NameValueCollection col)
        {
            return col.Keys.Cast<string>().ToDictionary(k => k, k => col[k]);
        }

        /// <summary>
        /// Converts the specified IDictionary&lt;T&gt; to a new 
        /// <see cref="System.Collections.Specialized.NameValueCollection"/> instance.
        /// </summary>
        /// <param name="seq">The sequence of key-value pairs to add to the returned NameValueCollection.</param>
        /// <returns>A <see cref="System.Collections.Specialized.NameValueCollection"/> containing the
        /// key-value pairs passed in.</returns>
        public static NameValueCollection ToNameValueCollection(
            this IDictionary<string, string>  dict
        )
        {
            var nvc = new NameValueCollection(dict.Count);
            foreach (var kv in dict)
                nvc.Add(kv.Key, kv.Value);
            return nvc;
        }

        /// <summary>
        /// Adds a sequence of key-value pairs to a new <see cref="System.Collections.Specialized.NameValueCollection"/> 
        /// instance and returns the new instance.
        /// </summary>
        /// <param name="seq">The sequence of key-value pairs to add to the returned NameValueCollection.</param>
        /// <returns>A <see cref="System.Collections.Specialized.NameValueCollection"/> containing the
        /// key-value pairs passed in.</returns>
        public static NameValueCollection ToNameValueCollection(this IEnumerable<KeyValuePair<string, string>> seq)
        {
            var nv = new NameValueCollection();
            foreach (var kv in seq)
                nv[kv.Key] = kv.Value;
            return nv;
        }

        public static SortedDictionary<K,V> ToSortedDictionary<K, V>(this IDictionary<K,V> dict)
        {
            return new SortedDictionary<K, V>(dict);
        }

        public static SortedDictionary<K, V> ToSortedDictionary<K, V>(this IDictionary<K, V> dict, IComparer<K> comparer)
        {
            return new SortedDictionary<K, V>(dict, comparer);
        }

        /// <summary>
        /// Converts the specified <see cref="System.Collections.Generic.IDictionary<string, object>"/> instance to an 
        /// <see cref="System.Dynamic.ExpandoObject"/> instance by copying its key-value pairs. Recursively converts any
        /// <see cref="System.Collections.Generic.IDictionary<string, object>"/> values down to the depth specified by
        /// <paramref name="maxDepth"/>.
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="maxDepth"></param>
        /// <returns></returns>
        public static ExpandoObject ToExpando(this IDictionary<string, object> dict, int maxDepth)
        {
            var expando = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)expando;

            if (maxDepth > 1)
            {
                foreach (var kv in dict)
                {

                    var subDict = kv.Value as IDictionary<string, object>;
                    if (subDict != null)
                    {
                        expandoDict[kv.Key] = subDict.ToExpando(maxDepth - 1);
                        continue;
                    }
                    expandoDict[kv.Key] = kv.Value;
                }
            }
            else
            {
                foreach (var kv in dict)
                {
                    expandoDict[kv.Key] = kv.Value;
                }
            }

            return expando;
        }

        public static ExpandoObject ToExpando(this IDictionary<string, object> dict)
        {
            var expando = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)expando;

            foreach (var kv in dict)
            {

                var subDict = kv.Value as IDictionary<string, object>;
                if (subDict != null)
                {
                    expandoDict[kv.Key] = subDict.ToExpando();
                    continue;
                }
                expandoDict[kv.Key] = kv.Value;
            }

            return expando;
        }


        public static IEnumerable<TValue> ValsOrDefault<TKey, TValue>(
            this ILookup<TKey, TValue> lkp, TKey key, IEnumerable<TValue> defaultVal = null
        )
        {
            if (lkp.Contains(key))
                return lkp[key];
            return defaultVal;
        }

        public static TValue ValOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key, TValue defaultVal = default(TValue)
        )
        {
            TValue val;
            if (dict.TryGetValue(key, out val))
                return val;
            return defaultVal;
        }
    }
}
