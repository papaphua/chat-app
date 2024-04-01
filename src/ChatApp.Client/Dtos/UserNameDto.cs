using System.ComponentModel.DataAnnotations;

namespace ChatApp.Client.Dtos;

public sealed class UserNameDto
{
    [Required] public string UserName { get; set; } = default!;
}