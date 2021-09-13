using System;

namespace MarchNote.Application.Configuration.Extensions
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string source, string value)
        {
            return !source.EndsWith(value)
                ? source
                : source.Remove(source.LastIndexOf(value, StringComparison.Ordinal));
        }
    }
}