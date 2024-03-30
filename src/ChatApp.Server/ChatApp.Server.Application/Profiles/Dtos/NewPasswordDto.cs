namespace ChatApp.Server.Application.Profiles.Dtos;

public sealed class NewPasswordDto
{
    public string OldPassword { get; set; } = default!;

    public string NewPassword { get; set; } = default!;
}