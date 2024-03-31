namespace ChatApp.Server.Infrastructure.SmsService;

public sealed class SmsOptions
{
    public string AccountSid { get; set; } = default!;

    public string AuthToken { get; set; } = default!;

    public string FromNumber { get; set; } = default!;
}