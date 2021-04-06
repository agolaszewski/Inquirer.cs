using System.ComponentModel;

namespace InquirerCS
{
    internal static class StringExtensions
    {
        internal static T To<T>(this string source)
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(source);
        }

        internal static T? ToN<T>(this string source) where T : struct
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                try
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(source);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}
