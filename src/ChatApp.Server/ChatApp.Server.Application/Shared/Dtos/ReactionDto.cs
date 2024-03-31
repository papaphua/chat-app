using ChatApp.Server.Domain.Core;

namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class ReactionDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid MessageId { get; set; }

    public ReactionType Type { get; set; }
}