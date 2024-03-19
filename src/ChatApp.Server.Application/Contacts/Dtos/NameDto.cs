using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Application.Contacts.Dtos;

public sealed class NameDto
{
    [Required] public string FirstName { get; set; } = default!;

    public string? LastName { get; set; }
}