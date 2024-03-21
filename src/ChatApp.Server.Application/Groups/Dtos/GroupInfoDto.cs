using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class GroupInfoDto 
{
    [Required] public string Name { get; set; } = default!;

    public string? Info { get; set; }
}