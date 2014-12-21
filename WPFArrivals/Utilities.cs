using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFArrivals
{
    public static class Extensions
    {
        public static string ToTitleCase(this string input)
        {
            var ti = CultureInfo.CurrentUICulture.TextInfo;
            return ti.ToTitleCase(input);
        }
    }
}
