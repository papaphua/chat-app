namespace ChatApp.Client.Core.Paging;

public abstract class PagedParameters
{
    public abstract int PageSize { get; init; }

    public int CurrentPage { get; set; } = 1;
}