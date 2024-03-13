namespace ChatApp.Server.Domain.Core.Abstractions.Paging;

public abstract class PagedParameters
{
    public abstract int PageSize { get; init; }

    public int CurrentPage { get; set; }
}