using System.ComponentModel;

namespace ConsoleWizard
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
                    return null;
                }
            }

            return null;
        }
    }
}