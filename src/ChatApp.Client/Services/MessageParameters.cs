using ChatApp.Client.Core.Paging;

namespace ChatApp.Client.Services;

public sealed class MessageParameters : PagedParameters
{
    public override int PageSize { get; init; } = 20;
}