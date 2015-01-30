using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class NumericExtensions
    {
        /// <summary>
        /// Rounds a double-precision floating-point value to a specified number of fractional 
        /// digits. Syntatic sugar for Math.Round(double, int).
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <returns>The number nearest to <paramref name="value"/> that contains a number of 
        /// fractional digits equal to <paramref name="digits"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this double value, int digits = 0)
        {
            return Math.Round(value, digits);
        }

        /// <summary>
        /// Rounds a decimal value to a specified number of fractional digits.
        /// Syntatic sugar for Math.Round(decimal, int).
        /// </summary>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        /// <returns>The number nearest to <paramref name="value"/> that contains a number of 
        /// fractional digits equal to <paramref name="digits"/>. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Round(this decimal value, int digits = 0)
        {
            return Math.Round(value, digits);
        }

        //Thanks to Jan Low's article on CodeProject: 
        //http://www.codeproject.com/Articles/27340/A-User-Friendly-C-Descriptive-Statistic-Class
        //Used under the CodeProject Open License: http://www.codeproject.com/info/cpol10.aspx
        /// <summary>
        /// Given an array of double-precision floating-point values that are assumed to already be
        /// sorted, returns the value of the percentile bracket specified by <paramref name="p"/>.
        /// </summary>
        /// <param name="sortedData">An already-sorted array of double-precision floating-point 
        /// values.</param>
        /// <param name="p">The percentile bracket to return.</param>
        /// <returns>The value of the percentile bracket specified by <paramref name="p"/>.</returns>
        public static double Percentile(this double[] sortedData, double p)
        {
            if (sortedData.Length == 0)
                return 0;
            if (sortedData.Length == 1)
                return sortedData[0];

            // algo derived from Aczel pg 15 bottom
            if (p >= 100.0d) return sortedData[sortedData.Length - 1];

            double position = (double)(sortedData.Length + 1) * p / 100.0;
            double leftNumber = 0.0d, rightNumber = 0.0d;

            double n = p / 100.0d * (sortedData.Length - 1) + 1.0d;

            if (position >= 1)
            {
                leftNumber = sortedData[(int)System.Math.Floor(n) - 1];
                rightNumber = sortedData[(int)System.Math.Floor(n)];
            }
            else
            {
                leftNumber = sortedData[0]; // first data
                rightNumber = sortedData[1]; // first data
            }

            if (leftNumber == rightNumber)
                return leftNumber;
            else
            {
                double part = n - System.Math.Floor(n);
                return leftNumber + part * (rightNumber - leftNumber);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.Double"/> sequence's median value.
        /// </summary>
        /// <param name="source">The sequence to return a median value from. The sequence 
        /// must contain at least one value.</param>
        /// <param name="isPreSorted">Specifies whether the sequence is already sorted in ascending 
        /// numerical order. If true, the sequence will be used as-is. If false, a copy of the 
        /// sequence will be sorted before the median is calculated.</param>
        /// <returns>The sequence's median value.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The sequence contains no elements.
        /// </exception>
        public static double Median(this IEnumerable<double> source, bool isPreSorted = false)
        {
            IList<double> sortedList = source as IList<double>;
            if (source == null || !isPreSorted)
            {
                sortedList = source.ToArray();
                if (!isPreSorted) Array.Sort<double>((double[])sortedList);
            }

            if (sortedList.Count == 0)
                throw new InvalidOperationException("Cannot compute median for an empty set.");

            int itemIndex = (int)sortedList.Count / 2;

            if (sortedList.Count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / 2;
            }
            else
            {
                // Odd number of items.
                return sortedList[itemIndex];
            }
        }

        /// <summary>
        /// Returns a <see cref="System.Single"/> sequence's median value.
        /// </summary>
        /// <param name="source">The sequence to return a median value from. The sequence 
        /// must contain at least one value.</param>
        /// <param name="isPreSorted">Specifies whether the sequence is already sorted in ascending 
        /// numerical order. If true, the sequence will be used as-is. If false, a copy of the 
        /// sequence will be sorted before the median is calculated.</param>
        /// <returns>The sequence's median value.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The sequence contains no elements.
        /// </exception>
        public static float Median(this IEnumerable<float> source, bool isPreSorted)
        {
            IList<float> sortedList = source as IList<float>;
            if (source == null || !isPreSorted)
            {
                sortedList = source.ToArray();
                if (!isPreSorted) Array.Sort<float>((float[])sortedList);
            }

            if (sortedList.Count == 0)
                throw new InvalidOperationException("Cannot compute median for an empty set.");

            int itemIndex = (int)sortedList.Count / 2;

            if (sortedList.Count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / 2;
            }
            else
            {
                // Odd number of items.
                return sortedList[itemIndex];
            }
        }

        /// <summary>
        /// Returns a <see cref="System.Int32"/> sequence's median value.
        /// </summary>
        /// <param name="source">The sequence to return a median value from. The sequence 
        /// must contain at least one value.</param>
        /// <param name="isPreSorted">Specifies whether the sequence is already sorted in ascending 
        /// numerical order. If true, the sequence will be used as-is. If false, a copy of the 
        /// sequence will be sorted before the median is calculated.</param>
        /// <returns>The sequence's median value.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The sequence contains no elements.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Median(this IEnumerable<int> source, bool isPreSorted = false)
        {
            return Median(source.Select(it => (double)it), isPreSorted);
        }

    }
}
