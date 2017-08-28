using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BetterConsole
{
    public static class StringExtensions
    {
        public static T To<T>(this string source)
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(source);
        }

        public static T? ToN<T>(this string source) where T : struct
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                try
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(source);
                }
                catch
                {
                    return default(T);
                }
            }
            return null;
        }
    }
}
