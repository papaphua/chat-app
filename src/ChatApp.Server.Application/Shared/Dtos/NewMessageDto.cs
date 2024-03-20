namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class NewMessageDto
{
    public string? Content { get; set; }

    public List<NewResourceDto> Attachments { get; set; } = default!;
}