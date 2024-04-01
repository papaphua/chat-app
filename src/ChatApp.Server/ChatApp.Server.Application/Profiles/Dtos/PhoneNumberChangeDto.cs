namespace ChatApp.Server.Application.Profiles.Dtos;

public sealed class PhoneNumberChangeDto
{
    public string PhoneNumber { get; set; } = default!;

    public string Token { get; set; } = default!;
}