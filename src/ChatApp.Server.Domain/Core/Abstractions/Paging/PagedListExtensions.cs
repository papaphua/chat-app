using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Domain.Core.Abstractions.Paging;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source,
        PagedParameters parameters)
        where T : IEntity
    {
        var totalCount = source.Count();
        var items = await source
            .Skip((parameters.CurrentPage - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedList<T>(items, totalCount, parameters.CurrentPage, parameters.PageSize);
    }

    public static PagedList<T> AsPagedList<T>(this List<T> source,
        PagedParameters parameters)
        where T : IEntity
    {
        return new PagedList<T>(source, source.Count, parameters.CurrentPage, parameters.PageSize);
    }
}