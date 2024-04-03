using ChatApp.Server.Domain.Core.Abstractions.Paging;

namespace ChatApp.Server.Domain.Contacts;

public sealed class ContactParameters : PagedParameters
{
    public override int PageSize { get; init; } = 10;
}