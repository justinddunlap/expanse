using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Copies a specified number of bytes from a source array starting at a particular
        /// offset to a destination array starting at a particular offset.
        /// 
        /// Syntatic sugar for <see cref="System.Buffer.BlockCopy"/>.
        /// </summary>
        /// <param name="source">The source buffer.</param>
        /// <param name="sourceOffset">The zero-based byte offset into <paramref name="source"/>.
        /// </param>
        /// <param name="dest">The destination buffer.</param>
        /// <param name="destOffset">The zero-based byte offset into <paramref name="dest"/>.</param>
        /// <param name="byteCount">The number of bytes to copy.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="dest"/> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="source"/> or <paramref name="dest"/> is not an array of primitives. 
        /// -or- 
        /// The number of bytes in <paramref name="source"/> is less than <paramref name="srcOffset"/> 
        /// plus <paramref name="byteCount"/>. 
        /// -or- 
        /// The number of bytes in <paramref name="dest"/> is less than <paramref name="destOffset"/> 
        /// plus <paramref name="byteCount"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="sourceOffset"/>, <paramref name="destOffset"/>, or 
        /// <paramref name="byteCount"/> is less than 0.
        /// </exception>
        public static void BlockCopy(
            this Array source, int sourceOffset, Array dest, int destOffset, int byteCount
        )
        {
            Buffer.BlockCopy(source, sourceOffset, dest, destOffset, byteCount);
        }

        /// <summary>
        /// Returns the number of bytes in the specified array of primitive values.
        /// </summary>
        /// <param name="arr">An array of primitive values.</param>
        /// <returns>The number of bytes in the array.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="arr"/> is null
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The array is not an array of primitives
        /// </exception>
        /// <exception cref="System.OverflowException">
        /// The array's size in bytes is larger than 2 GB
        /// </exception>
        public static int ByteLength(this Array arr)
        {
            return Buffer.ByteLength(arr);
        }
        
        /// <summary>
        /// Sets a range of elements in an array to the default value for the array type - 
        /// for example, zero, false, or null.
        /// </summary>
        /// <param name="arr">The array to clear elements of.</param>
        /// <param name="start">The starting index of the </param>
        /// <param name="length"></param>
        public static void Clear(this Array arr, int start = 0, int length = -1)
        {
            if (length == -1) 
                length = arr.Length - start;
            Array.Clear(arr, start, length);
        }


        public static void CopyTo(this Array source, Array dest, int length)
        {
            Array.Copy(source, dest, length);
        }

        public static void CopyTo(
            this Array source, int sourceIndex, Array dest, int destIndex = 0, int length = -1
        )
        {
            if (length == -1)
                length = Math.Min(source.Length - sourceIndex, dest.Length - destIndex);
            Array.Copy(source, sourceIndex, dest, destIndex, length);
        }

        public static void CopyTo(this Array source, Array dest, long length)
        {
            Array.Copy(source, dest, length);
        }

        public static void CopyTo(
            this Array source, long sourceIndex, Array dest, long destIndex = 0, long length = -1
        )
        {
            if (length == -1)
                length = Math.Min(source.Length - sourceIndex, dest.Length - destIndex);
            Array.Copy(source, sourceIndex, dest, destIndex, length);
        }

        public static byte GetByte(this Array arr, int byteIndex)
        {
            return Buffer.GetByte(arr, byteIndex);
        }

        public static void SetByte(this Array arr, int byteIndex, byte value)
        {
            Buffer.SetByte(arr, byteIndex, value);
        }

        public static void ReverseInPlace(this Array arr, int start = 0, int length = -1)
        {
            if (length == -1)
                length = arr.Length - start;
            Array.Reverse(arr, start, length);
        }

        public static void Sort(this Array arr, int start, Int32 length, IComparer comparer)
        {
            Array.Sort(arr, start, length, comparer);
        }

        public static void Sort(
            this Array arr, Array items, int start, int length, IComparer comparer
        )
        {
            Array.Sort(arr, items, start, length, comparer);
        }
    }
}
