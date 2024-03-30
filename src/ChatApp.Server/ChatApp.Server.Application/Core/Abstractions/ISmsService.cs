namespace ChatApp.Server.Application.Core.Abstractions;

public interface ISmsService
{
    Task SendMessageAsync(MessageTemplate template);
}