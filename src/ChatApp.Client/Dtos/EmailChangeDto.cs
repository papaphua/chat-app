namespace ChatApp.Client.Dtos;

public class EmailChangeDto
{
    public string Email { get; set; } = default!;

    public string Token { get; set; } = default!;
}