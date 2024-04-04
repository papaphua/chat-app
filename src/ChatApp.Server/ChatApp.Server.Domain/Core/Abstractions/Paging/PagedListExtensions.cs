using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Domain.Core.Abstractions.Paging;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source,
        PagedParameters parameters)
        where T : class
    {
        var totalCount = source.Count();
        var items = await source
            .Skip((parameters.CurrentPage - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedList<T>(items, totalCount, parameters.CurrentPage, parameters.PageSize);
    }

    public static PagedList<TItem> ToPagedList<TItem, TFrom>(this IEnumerable<TItem> items, PagedList<TFrom> fromList)
        where TItem : class
        where TFrom : class
    {
        var list = new PagedList<TItem>
        {
            PagedData = new PagedData
            {
                TotalCount = fromList.PagedData.TotalCount,
                PageSize = fromList.PagedData.PageSize,
                CurrentPage = fromList.PagedData.CurrentPage,
                TotalPages = fromList.PagedData.TotalPages
            }
        };

        list.AddRange(items);

        return list;
    }
}