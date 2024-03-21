using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Api.Requests;

public sealed class NewGroupRequest
{
    [Required] public string Name { get; set; } = default!;

    public IFormFile? File { get; set; }
}