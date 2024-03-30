namespace ChatApp.Server.Application.Core.Abstractions;

public interface IEmailService
{
    Task SendMessageAsync(MessageTemplate template);
}