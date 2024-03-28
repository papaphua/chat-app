namespace ChatApp.Identity.Options;

public sealed class SendGridOptions
{
    public string ApiKey { get; set; } = default!;
    
    public string SenderEmail { get; set; } = default!;
}