using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions.Conversions
{
    public static class ConversionExtensions
    {
        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string ToBase64String(this byte[] bytes, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(bytes, options);
        }

        public static string ToBase64String(this byte[] bytes, int start = 0, int length = -1, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(bytes, start, length, options);
        }

        public static int ToBase64CharArray(this byte[] source, int sourceOffset, int length, char[] dest, int destOffset = 0, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64CharArray(source, sourceOffset, length, dest, destOffset, options);
        }
    }
}
