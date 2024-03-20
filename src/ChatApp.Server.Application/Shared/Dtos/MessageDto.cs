namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class MessageDto
{
    public Guid Id { get; set; }
    
    public Guid SenderId { get; set; }
    
    public string? Content { get; set; }

    public DateTime Timestamp { get; set; }

    public List<AttachmentDto> Attachments { get; set; } = default!;

    public List<ReactionDto> Reactions { get; set; } = default!;
}