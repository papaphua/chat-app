using ChatApp.Server.Domain.Core.Abstractions.Paging;

namespace ChatApp.Server.Domain.Shared;

public sealed class MemberParameters : PagedParameters
{
    public override int PageSize { get; init; } = 20;
}