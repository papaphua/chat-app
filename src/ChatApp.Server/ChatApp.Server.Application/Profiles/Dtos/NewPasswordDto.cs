namespace ChatApp.Server.Application.Profiles.Dtos;

public sealed class NewPasswordDto
{
    public string CurrentPassword { get; set; } = default!;

    public string NewPassword { get; set; } = default!;

    public string ConfirmNewPassword { get; set; } = default!;
}