using ChatApp.Server.Application.Shared.Dtos;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class NewGroupDto
{
    public string Name { get; set; } = default!;
    
    public NewResourceDto? Avatar { get; set; }
}