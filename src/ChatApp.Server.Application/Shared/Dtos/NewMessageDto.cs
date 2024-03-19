using ChatApp.Server.Application.Core.Attributes;

namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class NewMessageDto
{
    public string? Content { get; set; }

    [RequiredIfNull(nameof(Content))] public List<NewResourceDto> Attachments { get; set; } = default!;
}