namespace ChatApp.Identity.Options;

public sealed class TwilioOptions
{
    public string AccountSid { get; set; } = default!;
    
    public string AuthToken { get; set; } = default!;

    public string FromNumber { get; set; } = default!;
}