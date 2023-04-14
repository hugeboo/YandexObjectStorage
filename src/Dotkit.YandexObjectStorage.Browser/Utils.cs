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
    }
}
