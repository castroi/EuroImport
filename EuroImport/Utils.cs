using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroImport
{
    public static class Utils
    {
        public static string Left(this string value, int length)
        {
            if(string.IsNullOrEmpty(value) == false)
            {
                return value.Substring(0, Math.Min(length, value.Length));
            }
            return string.Empty;
        }

        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                return value.Substring(Math.Max(0, value.Length - length), Math.Min(value.Length,  length));
            }
            return string.Empty;
        }

        public static string TileCase(this string value)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }
    }
}
