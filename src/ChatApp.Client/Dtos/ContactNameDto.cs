using System.ComponentModel.DataAnnotations;

namespace ChatApp.Client.Dtos;

public sealed class ContactNameDto
{
    [Required] public string FirstName { get; set; } = default!;

    public string? LastName { get; set; }
}