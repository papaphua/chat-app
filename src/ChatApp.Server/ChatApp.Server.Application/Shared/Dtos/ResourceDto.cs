using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Application.Shared.Dtos;

public sealed class ResourceDto
{
    [Base64String] public string Bytes { get; set; } = default!;
}