using ChatApp.Server.Application.Core.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class NewMessageDto
{
    [FromForm(Name = "text")] public string? Content { get; set; }

    [FromForm(Name = "files")]
    [RequiredIfNull(nameof(Content))]
    public List<IFormFile>? Attachments { get; set; } = [];
}