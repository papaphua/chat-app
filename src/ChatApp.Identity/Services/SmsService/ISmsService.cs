namespace ChatApp.Identity.Services.SmsService;

public interface ISmsService
{
    Task SendVerificationTokenAsync(string receiver, string token);
}