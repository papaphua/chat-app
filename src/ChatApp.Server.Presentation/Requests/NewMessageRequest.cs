using ChatApp.Server.Application.Core.Attributes;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Server.Presentation.Requests;

public sealed class NewMessageRequest
{
    public string? Content { get; set; }

    [RequiredIfNull(nameof(Content))] public List<IFormFile>? Files { get; set; }
}