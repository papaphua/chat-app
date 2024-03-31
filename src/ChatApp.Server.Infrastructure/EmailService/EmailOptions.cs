namespace ChatApp.Server.Infrastructure.EmailService;

public sealed class EmailOptions
{
    public string ApiKey { get; set; } = default!;

    public string SenderEmail { get; set; } = default!;
}