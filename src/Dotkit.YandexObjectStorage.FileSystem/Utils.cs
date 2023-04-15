using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public static class Utils
    {
        public static async Task<IEnumerable<T1>> SelectManyAsync<T, T1>(this IEnumerable<T> enumeration, Func<T, Task<IEnumerable<T1>>> func)
        {
            return (await Task.WhenAll(enumeration.Select(func))).SelectMany(s => s);
        }

        public static string? NormalizePathDelimeter(this string? path)
        {
            if (string.IsNullOrWhiteSpace(path)) return path;
            return path.Replace("\\", YClient.PATH_DELIMETER);
        }

        public static string? TrimEndPathDelimeter(this string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !path.EndsWith(YClient.PATH_DELIMETER)) return path;
            return path.TrimEnd(YClient.PATH_DELIMETER[0]);
        }

        public static string? AddEndPathDelimeter(this string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || path.EndsWith(YClient.PATH_DELIMETER)) return path;
            return path + YClient.PATH_DELIMETER;
        }
    }
}
