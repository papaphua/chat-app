using ChatApp.Client.Core.Paging;

namespace ChatApp.Client.Services.ContactService;

public sealed class ContactParameters : PagedParameters
{
    public override int PageSize { get; init; } = 10;
}