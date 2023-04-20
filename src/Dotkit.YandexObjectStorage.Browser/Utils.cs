using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal static class Utils
    {
        public static void DoBackground(Action backgroundAction, Action? onSuccessfully = null, Action<Exception?>? onError = null)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                backgroundAction();
            })
            .ContinueWith((t) =>
            {
                if (t.IsCompletedSuccessfully && onSuccessfully != null) onSuccessfully();
                else if (onError != null) onError(t.Exception);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static void DoBackground<T>(Func<T> backgroundFunc, Action<T>? onSuccessfully = null, Action<Exception?>? onError = null)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                return backgroundFunc();
            })
            .ContinueWith((t) =>
            {
                if (t.IsCompletedSuccessfully && onSuccessfully != null) onSuccessfully(t.Result);
                else if (onError != null) onError(t.Exception);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static string EllipsisString(this string rawString, int maxLength = 30, char delimiter = '\\')
        {
            maxLength -= 3; //account for delimiter spacing

            if (rawString.Length <= maxLength)
            {
                return rawString;
            }

            string final = rawString;
            List<string> parts;

            int loops = 0;
            while (loops++ < 100)
            {
                parts = rawString.Split(delimiter).ToList();
                parts.RemoveRange(parts.Count - 1 - loops, loops);
                if (parts.Count == 1)
                {
                    return parts.Last();
                }

                parts.Insert(parts.Count - 1, "...");
                final = string.Join(delimiter.ToString(), parts);
                if (final.Length < maxLength)
                {
                    return final;
                }
            }

            return rawString.Split(delimiter).ToList().Last();
        }
    }
}
