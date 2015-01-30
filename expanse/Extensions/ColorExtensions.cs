using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class ColorExtensions
    {
        public static int ToBgra(this Color color)
        {
            return (color.B << 24) | (color.G << 16) | (color.B << 8) | color.A;
        }

        public static string ToHtmlColor(this Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        public static int ToWin32Color(this Color color)
        {
            return ColorTranslator.ToWin32(color);
        }

        public static int ToOleColor(this Color color)
        {
            return ColorTranslator.ToOle(color);
        }
    }
}
