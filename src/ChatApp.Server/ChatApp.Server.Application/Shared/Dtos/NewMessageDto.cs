using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class NewMessageDto
{
    public string? Content { get; set; }

    public List<IFormFile> Attachments { get; set; } = default!;
}