using ChatApp.Server.Application.Core.Attributes;

namespace ChatApp.Server.Api.Requests;

public sealed class NewMessageRequest
{
    public string? Content { get; set; }

    [RequiredIfNull(nameof(Content))] public List<IFormFile>? Files { get; set; }
}