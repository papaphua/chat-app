namespace ChatApp.Server.Application.Profiles.Dtos;

public sealed class ProfileDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Bio { get; set; }

    public string UserName { get; set; } = default!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public List<AvatarDto> Avatars { get; set; } = default!;
}