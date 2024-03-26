using System.Runtime.CompilerServices;

namespace BHBackup.Download.Extensions;

internal static class AsyncEnumerableExtensions
{

    ///// <remarks>
    ///// See See https://stackoverflow.com/a/63757293/3156906
    ///// </remarks>
    //public static async Task<List<T>> ToListAsync<T>(
    //    this IAsyncEnumerable<T> items,
    //    CancellationToken cancellationToken = default
    //)
    //{
    //    var results = new List<T>();
    //    var enumerable = items.WithCancellation(cancellationToken).ConfigureAwait(false);
    //    return await enumerable.ToListAsync();
    //}

    /// <remarks>
    /// See See https://stackoverflow.com/a/63757293/3156906
    /// </remarks>
    public static async Task<List<T>> ToListAsync<T>(
        this ConfiguredCancelableAsyncEnumerable<T> items
    )
    {
        var results = new List<T>();
        await foreach (var item in items)
        {
            results.Add(item);
        }
        return results;
    }

}
