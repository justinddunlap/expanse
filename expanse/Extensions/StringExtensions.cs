using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class StringExtensions
    {
        public static string Implode<T>(this IEnumerable<T> enumerable, string separator)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var s in enumerable)
            {
                if (first)
                    first = false;
                else
                    sb.Append(separator);
                sb.Append(s);
            }
            return sb.ToString();
        }

        public static string Implode(this IEnumerable<string> enumerable, string separator)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var s in enumerable)
            {
                if (first)
                    first = false;
                else
                    sb.Append(separator);
                sb.Append(s);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns whether the specified string is null or an empty string.
        /// Syntatic sugar for String.IsNullOrEmpty.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Returns whether the specified string is null, empty, or contains only whitespace
        /// characters.
        /// Syntatic sugar for String.IsNullOrWhiteSpace.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Returns a new string instance made up of the specified characters.
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AsString(this char[] chars)
        {
            return new String(chars);
        }

        /// <summary>
        /// Returns a new string instance made up of the specified characters.
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AsString(this IEnumerable<char> chars)
        {
            return new String(chars.ToArray());
        }

        /// <summary>
        /// Truncates the string specified in <paramref name="str"/> to the length specified in <paramref name="maxLen"/>,
        /// optionally appending or overwriting the specified <paramref name="terminator"/> (eg, an ellipsis) at the end of 
        /// the string if the string is longer than <paramref name="maxLen"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLen"></param>
        /// <param name="terminator"></param>
        /// <param name="appendTerminator"></param>
        /// <returns></returns>
        public static string Truncate(this string str, int maxLen, string terminator = null, bool appendTerminator = true)
        {
            if (str == null || str.Length <= maxLen) return str;

            if(terminator == null)
                return new String(str.Take(maxLen).ToArray());
            else if(appendTerminator)
                return new String(str.Take(maxLen).Concat(terminator).ToArray());
            else
                return new String(str.Take(maxLen - terminator.Length).Concat(terminator).ToArray());
        }

        /// <summary>
        /// Converts the words in the specified string to title case using the
        /// current thread's culture.
        /// </summary>
        /// <param name="str">The string to convert to title case.</param>
        /// <returns>The string with all of its words converted to title case.</returns>
        public static string ToTitleCase(this string str)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = culture.TextInfo;
            return textInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Converts the words in the specified string to title case using the
        /// specified <paramref name="culture"/>.
        /// </summary>
        /// <param name="str">The string to convert to title case.</param>
        /// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> 
        /// instance to use to convert the string to title case.</param>
        /// <returns>The string with all of its words converted to title case.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToTitleCase(this string str, CultureInfo culture)
        {
            return culture.TextInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Converts the words in the specified string to title case using the
        /// specified <paramref name="textInfo"/>.
        /// </summary>
        /// <param name="str">The string to convert to title case.</param>
        /// <param name="textInfo">The <see cref="System.Globalization.TextInfo"/> 
        /// instance to use to convert the string to title case.</param>
        /// <returns>The string with all of its words converted to title case.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToTitleCase(this string str, TextInfo textInfo)
        {
            return textInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Returns a string containing all of the characters that fall after the 
        /// first occurrence of <paramref name="l"/> and before the first subsequent
        /// occurrence of <paramref name="r"/> in the source string specified in
        /// <paramref name="str"/>.
        /// </summary>
        /// <param name="str">The source string.</param>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string Between(this string str, string l, string r)
        {
            int lIdx = str.IndexOf(l);
            if (lIdx != -1) lIdx = lIdx + l.Length;
            int rIdx = str.IndexOf(r, lIdx + 1);
            if (lIdx == -1)
            {
                if (rIdx == -1) return str;
                lIdx = 0;
            }
            else if (rIdx == -1)
                rIdx = str.Length;

            return str.Substring(lIdx, rIdx - lIdx);
        }

        public static string Between(this string str, char l, char r)
        {
            int lIdx = str.IndexOf(l);
            int rIdx = str.IndexOf(r, lIdx + 1);
            if (lIdx == -1 && rIdx == -1)
                return str;
            if (rIdx == -1) rIdx = str.Length;

            return str.Substring(lIdx + 1, rIdx - (lIdx + 1));
        }

        public static string Between(this string str, char[] l, char[] r)
        {
            int lIdx = str.IndexOfAny(l);
            int rIdx = str.IndexOfAny(r, lIdx + 1);
            if (lIdx == -1 && rIdx == -1)
                return str;
            if (rIdx == -1) rIdx = str.Length;

            return str.Substring(lIdx + 1, rIdx - (lIdx + 1));
        }

        public static string Between(this string str, string[] l, string[] r)
        {
            var lMatch = str.GetFirstMatch(l);
            int lIdx = lMatch.Index == -1 ? -1 : lMatch.Index + lMatch.Match.Length;
            int rIdx = str.IndexOfAny(r, lIdx < 0 ? 0 : lIdx);

            if (lIdx == -1)
            {
                if (rIdx == -1) return str;
                lIdx = 0;
            }
            else if (rIdx == -1)
            {
                rIdx = str.Length;
            }

            return str.Substring(lIdx, rIdx - lIdx);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Between(this string str, int l, int r)
        {
            return str.Substring(l + 1, r - (l + 1));
        }

        /// <summary>
        /// Returns the portion of <paramref name="str"/> that falls
        /// to the left of the specified character index. 
        /// 
        /// If the index is greater than or equal to the length of the string, 
        /// the whole string is returned. If the index is less than or equal to 
        /// zero, an empty string is returned.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="referenceIndex"></param>
        /// <returns></returns>
        public static string LeftOf(this string str, int referenceIndex)
        {
            if (referenceIndex >= str.Length)
                return str;
            if (referenceIndex <= 0)
                return string.Empty;
            return str.Substring(0, referenceIndex);
        }

        /// <summary>
        /// Returns the portion of <paramref name="str"/> that is to the left 
        /// of the first occurrence of the character specified by <paramref name="c"/>,
        /// or the entire string if the specified character is not present in the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string LeftOf(this string str, char c)
        {
            int idx = str.IndexOf(c);
            if (idx == -1)
                return str;
            return str.Substring(0, idx);
        }

        /// <summary>
        /// Returns the portion of <paramref name="str"/> that is to the left 
        /// of the first occurrence of any of the characters specified in <paramref name="c"/>,
        /// or the entire string if none of the specified characters are present in the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string LeftOf(this string str, params char[] c)
        {
            int idx = str.IndexOfAny(c);
            if (idx == -1)
                return str;
            return str.Substring(0, idx);
        }


        /// <summary>
        /// Returns the portion of <paramref name="str"/> that is to the left 
        /// of the first occurrence of the string specified by <paramref name="s"/>,
        /// or the entire string if <param name="s"/> is not present in <paramref name="str"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string LeftOf(this string str, string s)
        {
            int idx = str.IndexOf(s);
            if (idx == -1)
                return str;
            return str.Substring(0, idx);
        }

        public static int IndexLeftOf(this string str, string s)
        {
            int idx = str.IndexOf(s);
            if (idx == -1)
                return 0;
            return idx - 1;
        }

        public static int IndexLeftOf(this string str, string s, int startIdx)
        {
            int idx = str.IndexOf(s, startIdx);
            if (idx == -1)
                return startIdx;
            return idx - 1;
        }

        public static string LeftOfLast(this string str, char c)
        {
            int idx = str.LastIndexOf(c);
            if (idx == -1)
                return str;
            return str.Substring(0, idx);
        }

        public static string LeftOfLast(this string str, params char[] c)
        {
            int idx = str.LastIndexOfAny(c);
            if (idx == -1)
                return str;
            return str.Substring(0, idx);
        }

        public static string RightOf(this string str, int referenceIndex)
        {
            if (referenceIndex >= str.Length - 1)
                return string.Empty;
            return str.Substring(referenceIndex + 1);
        }

        public static string RightOf(this string str, char c)
        {
            var charIdx = str.IndexOf(c);
            if (charIdx == -1 || charIdx >= str.Length - 1)
                return str;
            return str.Substring(charIdx + 1);
        }

        public static string RightOf(this string str, params char[] c)
        {
            var charIdx = str.IndexOfAny(c);
            if (charIdx == -1 || charIdx >= str.Length - 1)
                return str;
            return str.Substring(charIdx + 1);
        }

        public static string RightOfLast(this string str, char c)
        {
            var charIdx = str.LastIndexOf(c);
            if (charIdx == -1 || charIdx >= str.Length - 1)
                return str;
            return str.Substring(charIdx + 1);
        }

        public static string RightOfLast(this string str, params char[] c)
        {
            var charIdx = str.LastIndexOfAny(c);
            if (charIdx == -1 || charIdx >= str.Length - 1)
                return str;
            return str.Substring(charIdx + 1);
        }

        public static string RightOf(this string str, string s)
        {
            var charIdx = str.IndexOf(s);
            if (charIdx == -1 || charIdx >= str.Length)
                return str;
            return str.Substring(charIdx + s.Length);
        }

        public static int IndexRightOf(this string str, string s)
        {
            var charIdx = str.IndexOf(s);
            if (charIdx == -1 || charIdx >= str.Length)
                return 0;
            return charIdx + s.Length;
        }

        public static int IndexRightOf(this string str, string s, int startIdx)
        {
            var charIdx = str.IndexOf(s, startIdx);
            if (charIdx == -1 || charIdx >= str.Length)
                return startIdx;
            return charIdx + s.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NullIfEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            return str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EndsWith(this string str, string find, bool ignoreCase)
        {
            return str.EndsWith(find, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StartsWith(this string str, string find, bool ignoreCase)
        {
            return str.StartsWith(find, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsOrdinal(this string str, string other, bool ignoreCase)
        {
            return str.Equals(other, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsIg(this string str, string compareWith)
        {
            return str.Equals(compareWith, StringComparison.CurrentCultureIgnoreCase);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsIgInv(this string str, string compareWith)
        {
            return str.Equals(compareWith, StringComparison.InvariantCultureIgnoreCase);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Fmt(this string str, object arg)
        {
            return string.Format(str, arg);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Fmt(this string str, object arg0, object arg1, object arg2)
        {
            return string.Format(str, arg0, arg1, arg2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Fmt(this string str, object arg0, object arg1)
        {
            return string.Format(str, arg0, arg1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Fmt(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Fmt(this string str, IFormatProvider prov, params object[] args)
        {
            return string.Format(prov, str, args);
        }

        public static void AppendJoined(this StringBuilder sb, string sep, IEnumerable<string> strs)
        {
            bool first = true;
            foreach (var str in strs)
            {
                if (first)
                    first = false;
                else
                    sb.Append(sep);
                sb.Append(str);
            }
        }

        public static void AppendJoined(this StringBuilder sb, string sep, IEnumerable<object> objs)
        {
            bool first = true;
            foreach (var obj in objs)
            {
                if (first)
                    first = false;
                else
                    sb.Append(sep);
                sb.Append(obj);
            }
        }

        public static void AppendJoined(this StringBuilder sb, string sep, params string[] strs)
        {
            sb.EnsureCapacity((strs.Length * 5) + sb.Length);
            bool first = true;
            foreach (var str in strs)
            {
                if (first)
                    first = false;
                else
                    sb.Append(sep);
                sb.Append(str);
            }
        }

        public static void AppendJoined(this StringBuilder sb, string sep, params object[] objs)
        {
            sb.EnsureCapacity((objs.Length * 5) + sb.Length);
            bool first = true;
            foreach (var str in objs)
            {
                if (first)
                    first = false;
                else
                    sb.Append(sep);
                sb.Append(str);
            }
        }

        public static void AppendJoined(this StringBuilder sb, string sep, int avgLength, params string[] strs)
        {
            sb.EnsureCapacity((strs.Length * avgLength) + sb.Length);
            bool first = true;
            foreach (var str in strs)
            {
                if (first)
                    first = false;
                else
                    sb.Append(sep);
                sb.Append(str);
            }
        }

        public static void AppendAll(this StringBuilder sb, params string[] strs)
        {
            sb.EnsureCapacity((strs.Length * 5) + sb.Length);
            for (int i = 0; i < strs.Length; i++)
                sb.Append(strs[i]);
        }

        public static void AppendAll(this StringBuilder sb, params object[] objs)
        {
            sb.EnsureCapacity((objs.Length * 5) + sb.Length);
            for (int i = 0; i < objs.Length; i++)
                sb.Append(objs[i]);
        }

        public static void AppendAll(this StringBuilder sb, int avgLength, params string[] strs)
        {
            sb.EnsureCapacity((strs.Length * avgLength) + sb.Length);
            for (int i = 0; i < strs.Length; i++)
                sb.Append(strs[i]);
        }

        public static void AppendAll(this StringBuilder sb, IEnumerable<string> strs)
        {
            foreach (var str in strs)
                sb.Append(str);
        }

        public static void AppendAll(this StringBuilder sb, IEnumerable<object> objs)
        {
            foreach (var obj in objs)
                sb.Append(obj);
        }

        static CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;

        /// <summary>
        /// Returns whether <paramref name="search"/> contains the string specified in <paramref name="match"/>, 
        /// using a simple, high-performance invariant ASCII comparison.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="match"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsSimple(this string search, string match, bool ignoreCase)
        {
            if (match == null)
                return true;
            return IndexOfSimple(search, match, 0, ignoreCase) != -1;
        }

        public static int IndexOfSimple(this string search, string match, int startIdx = 0, bool ignoreCase = false)
        {
            if (match == null) return -1;
            if (match == "") return 0;

            int len = (search.Length - match.Length) + 1;
            char firstMatchChar = ignoreCase ? match[0].ToLowerAsciiInvariant() : match[0];

            if (ignoreCase)
            {
                for (; startIdx < len; startIdx++)
                {
                    if (search[startIdx].ToLowerAsciiInvariant() == firstMatchChar)
                    {
                        for (var j = 1; j < match.Length; j++)
                        {
                            if (search[startIdx + j].ToLowerAsciiInvariant() != match[j].ToLowerAsciiInvariant())
                                goto SearchNextIg; //used as a 'continue' on outer loop
                        }
                        return startIdx;
                    }

                SearchNextIg: ;
                }
            }
            else
            {
                for (; startIdx < len; startIdx++)
                {
                    if (search[startIdx] == firstMatchChar)
                    {
                        for (var j = 1; j < match.Length; j++)
                        {
                            if (search[startIdx + j] != match[j])
                                goto SearchNext; //used as a 'continue' on outer loop
                        }
                        return startIdx;
                    }
                SearchNext: ;
                }
            }

            return -1;
        }

        public static int IndexOfAny(this string test, string[] values, int idx = 0)
        {
            int first = -1;
            foreach (string item in values)
            {
                int i = test.IndexOf(item, idx);
                if (i >= 0)
                {
                    if (first > 0)
                    {
                        if (i < first)
                        {
                            first = i;
                        }
                    }
                    else
                    {
                        first = i;
                    }
                }
            }
            return first;
        }

        public static StringMatch GetFirstMatch(this string test, string[] values, int idx = 0)
        {
            int first = -1;
            string str = null;
            foreach (string item in values)
            {
                int i = test.IndexOf(item, idx);
                if (i >= 0)
                {
                    if (first > 0)
                    {
                        if (i < first)
                        {
                            first = i;
                            str = item;
                        }
                    }
                    else
                    {
                        first = i;
                        str = item;
                    }
                }
            }
            return new StringMatch(first, str);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToLowerAsciiInvariant(this string s)
        {
            var len = s.Length;
            var ch = new char[len];

            for (int i = 0; i < len; i++)
                ch[i] = s[i].ToLowerAsciiInvariant();
            return new string(ch);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char ToLowerAsciiInvariant(this char c)
        {
            if ('A' <= c && c <= 'Z')
                c |= ' ';
            return c;
        }
    }

    public struct StringMatch
    {
        public StringMatch(int idx, string match)
            : this()
        {
            Index = idx;
            Match = match;
        }

        public int Index { get; set; }
        public string Match { get; set; }
    }
}
