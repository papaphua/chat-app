using System.ComponentModel.DataAnnotations;

namespace ChatApp.Client.Dtos;

public sealed class ResourceDto
{
    [Base64String] public string Bytes { get; set; } = default!;
}