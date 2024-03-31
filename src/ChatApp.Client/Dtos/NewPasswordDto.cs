namespace ChatApp.Client.Dtos;

public sealed class NewPasswordDto
{
    public string CurrentPassword { get; set; } = default!;

    public string NewPassword { get; set; } = default!;
}