namespace ChatApp.Client.Core.Paging;

public sealed class PagedResponse<T> where T : class
{
    public List<T> Items { get; set; } = [];
    public PagedData PagedData { get; set; } = default!;
}